using DatapacLibrary.DTO.Requests;
using DatapacLibrary.Interfaces;
using DatapacLibrary.Models;
using Hangfire;
using System.Diagnostics.CodeAnalysis;

namespace DatapacLibrary.Services
{
    public class DatapacLibraryService : ILibraryService
    {
        #region init
        private readonly ILogger<DatapacLibraryService> logger;
        private readonly IArticleRepository<Book> bookRepo;
        private readonly IArticleRepository<User> userRepo;
        private readonly IArticleRepository<Checkout> checkoutRepo;
        private readonly IBackgroundJobClient scheduler;
        private readonly IArticleRepository<Notification> notifRepo;
        private readonly INotificationService<Email> emailService;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="context"></param>
        public DatapacLibraryService(ILogger<DatapacLibraryService> logger, IArticleRepository<Book> bookRepo,
            IArticleRepository<User> userRepo, IArticleRepository<Checkout> checkoutRepo, IBackgroundJobClient scheduler, IArticleRepository<Notification> notifRepo, INotificationService<Email> emailService)
        {
            this.logger = logger;
            this.bookRepo = bookRepo;
            this.userRepo = userRepo;
            this.checkoutRepo = checkoutRepo;
            this.scheduler = scheduler;
            this.notifRepo = notifRepo;
            this.emailService = emailService;
        }
        #endregion
        #region BorrowBook
        /// <summary>
        /// Create new checkout for particular book
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        public async Task<Checkout> BorrowBook([DisallowNull] BorrowRequest request)
        {

            // Validate User => Blocked or not
            var user = await userRepo.GetById(request.UserCardNumber) ?? throw new InvalidDataException("User not found");

            if (user.IsBlocked)
            {
                throw new InvalidDataException("Provided user is blocked");
            }

            //Block user in case there is checkout that shoud have been returned
            if (CheckPenalty(request.UserCardNumber))
            {
                user.IsBlocked = true;
                await userRepo.Update(user);

                throw new InvalidDataException("User has been blocked for violating library rules - Unreturned books");
            }

            // Check book whether exists or not
            var book = await bookRepo.GetById(request.BookId) ?? throw new InvalidDataException("Book not found");

            // Create new checkout
            var checkout = await checkoutRepo.Create(new Checkout
            {
                Book = book,
                User = user,
                IsReturned = false,
                StartTime = DateTime.UtcNow.Date,
                EndTime = DateTime.UtcNow.AddDays(7).Date
            });

            logger.LogInformation($"Checkout record has beem created with id: {checkout.Id}");

            // Set scheduled reminder in order to send notification to user 1 day before expiration
            var notificationId = scheduler.Schedule(() => emailService.Send(new Email
            {
                Recipient = checkout.User.Email,
                Body = "You're late, buddy",
                Subject = "Breaking the law"
            }), checkout.EndTime.AddDays(-1)) ?? throw new InvalidDataException("Something went wrong during scheduling process");

            // Store notification
            await notifRepo.Create(new Notification { Id = notificationId, Checkout = checkout, SendAt = checkout.EndTime });


            return checkout;
        }
        #endregion
        #region ReturnBook
        /// <summary>
        /// Return book so the checkout will get dismissed
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        public async Task ReturnBook([DisallowNull] ReturnRequest request)
        {
            // Is there active checkout for this book
            var checkout = await checkoutRepo.GetById(request.CheckoutId) ?? throw new InvalidDataException("Checkout record not found");

            // Does this checkout belong to provided user 
            if (checkout.User.CardNumber != request.UserCardNumber)
            {
                throw new InvalidDataException("Checkout does not belong to provided user");
            }

            // Did lend period expire 
            if (checkout.EndTime.Date < DateTime.UtcNow.Date)
            {
                var user = await userRepo.GetById(request.UserCardNumber) ?? throw new InvalidDataException("User not found");

                // Block user for not returning book on time
                user.IsBlocked = true;
                await userRepo.Update(user);
            }

            // Remove checkout and pending notifications
            await CheckAndDeleteNotifications(checkout);
            await checkoutRepo.Remove(checkout);


        }
        #endregion
        #region PrivateClasses
        /// <summary>
        /// Check whether user has some expired checkouts or not
        /// </summary>
        /// <param name="userCardNumber"></param>
        /// <returns></returns>
        private bool CheckPenalty(int userCardNumber)
        {
            return checkoutRepo.GetAll().Where(item => item.User.CardNumber == userCardNumber && item.EndTime.Date < DateTime.UtcNow.Date).Any();
        }

        /// <summary>
        /// Check pending notifications and delete them
        /// </summary>
        /// <param name="checkout"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private async Task CheckAndDeleteNotifications(Checkout checkout)
        {
            _ = checkout ?? throw new ArgumentNullException(nameof(checkout));

            var notification = notifRepo.GetAll().Where(item => item.Checkout == checkout).FirstOrDefault();
            if (notification != null)
            {
                scheduler.Delete(notification.Id);
                await notifRepo.Remove(notification);
            }
        }
        #endregion
    }
}
