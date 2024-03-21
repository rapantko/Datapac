using DatapacLibrary.DTO;
using DatapacLibrary.DTO.Requests;
using DatapacLibrary.DTO.Responses;
using DatapacLibrary.Interfaces;
using DatapacLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;


namespace DatapacLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BookController : ControllerBase
    {
        #region Init
        private readonly ILogger<BookController> logger;
        private readonly IArticleRepository<Book> bookRepo;
        private readonly ILibraryService library;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="bookRepo"></param>
        /// <param name="userDirectory"></param>
        /// <param name="library"></param>
        public BookController(ILogger<BookController> logger, IArticleRepository<Book> bookRepo, ILibraryService library)
        {
            this.logger = logger;
            this.bookRepo = bookRepo;
            this.library = library;
        }

        #endregion
        #region GetBook

        /// <summary>
        /// Get book reference detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseTypeAttribute(typeof(BookResponse), 200)]
        public async Task<ActionResult<BookResponse>> GetBook(int id)
        {
            try
            {
                var book = await bookRepo.GetById(id);
                return Ok(new BookResponse
                {
                    Author = book!.Author,
                    BookID = book.Id,
                    Title = book.Title
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.StackTrace);
                return BadRequest(new BookResponse { BookID = id, Error = ex.Message });
            }
        }
        #endregion
        #region CreateBook
        /// <summary>
        /// Create new book
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseTypeAttribute(typeof(BookResponse), 200)]
        public async Task<ActionResult<BookResponse>> CreateBook([DisallowNull] CreateBookRequest request)
        {
            try
            {
                var newBook = await bookRepo.Create(new Book { Author = request.Author, Title = request.Title });
                logger.LogInformation($"Book successfully added with id: {newBook.Id}");
                return Ok(new BookResponse
                {
                    BookID = newBook.Id,
                    Author = newBook.Author,
                    Title = newBook.Title
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.StackTrace);
                return BadRequest(new BookResponse { Error = ex.Message });
            }
        }
        #endregion
        #region BorrowBook
        /// <summary>
        /// Borrow a book
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Borrow")]
        public async Task<ActionResult<BorrowResponse>> BorrowBook([DisallowNull] BorrowRequest request)
        {
            try
            {
                var checkout = await library.BorrowBook(request);
                return Ok(new BorrowResponse { CheckoutId = checkout.Id, IsSuccess = true });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.StackTrace);
                return BadRequest(new BorrowResponse { Error = ex.Message });
            }
        }
        #endregion
        #region ReturnBook
        /// <summary>
        /// Return borrowed book
        /// </summary>
        /// <param name="checkout"></param>
        /// <returns></returns>
        [HttpPost("Return")]
        public async Task<ActionResult> ReturnBook([DisallowNull] ReturnRequest request)
        {
            try
            {
                await library.ReturnBook(request);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }
        #endregion
        #region UpdateBook
        /// <summary>
        /// Update book
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseTypeAttribute(204)]
        public async Task<ActionResult> UpdateBook(UpdateBookRequest request)
        {
            try
            {
                // Update existing book
                var book = await bookRepo.Update(new Book { Author = request.Author, Id = request.Id, Title = request.Title });
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.StackTrace);
                return BadRequest(new BookResponse { BookID = request.Id, Error = ex.Message });
            }
        }
        #endregion
        #region DeleteBook
        /// <summary>
        /// Delete book
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseTypeAttribute(204)]
        public async Task<ActionResult> DeleteBook(int id)
        {
            try
            {
                // First step is to get identification for particular book
                var book = await bookRepo.GetById(id);
                // Remove book
                await bookRepo.Remove(book!);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.StackTrace);
                return BadRequest(new BookResponse { BookID = id, Error = ex.Message });
            }
        }
        #endregion
    }
}
