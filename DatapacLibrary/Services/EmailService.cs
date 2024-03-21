using DatapacLibrary.Interfaces;
using DatapacLibrary.Models;

namespace DatapacLibrary.Services
{
    public class EmailService : INotificationService<Email>
    {
        #region init
        private readonly ILogger<EmailService> logger;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="logger"></param>
        public EmailService(ILogger<EmailService> logger)
        {
            this.logger = logger;
        }
        #endregion

        /// <summary>
        /// This is fake method used for showcase only
        /// </summary>
        /// <param name="notification"></param>
        public void Send(Email notification)
        {
            logger.LogInformation($"Email notification has been sent to {notification.Recipient}");
        }
    }
}
