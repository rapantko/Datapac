using DatapacLibrary.Controllers;
using DatapacLibrary.DTO;
using DatapacLibrary.DTO.Requests;
using DatapacLibrary.DTO.Responses;
using DatapacLibrary.Interfaces;
using DatapacLibrary.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DatapacLibrary.UnitTests
{
    public class BookControllerTests
    {
        #region GetBook

        [Test]
        public async Task GetBook_HappyPath_ErrorIsEmpty()
        {
            //Arrange
            var logger = A.Fake<ILogger<BookController>>();
            var bookRepo = A.Fake<IArticleRepository<Book>>();
            var library = A.Fake<ILibraryService>();

            BookController bookController = new BookController(logger, bookRepo, library);

            A.CallTo(() => bookRepo.GetById(A<int>.Ignored)).Returns(new Book { Author = "Test", Title = "Test", Id = 1 });

            //Act
            var result = await bookController.GetBook(1);
            var objectResult = (OkObjectResult)result.Result;
            var bookResponse = (BookResponse)objectResult.Value;

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            Assert.IsTrue(objectResult.StatusCode == 200);
            Assert.IsTrue(String.IsNullOrEmpty(bookResponse!.Error));
        }

        [Test]
        public async Task GetBook_RepoThrowsException_ErrorIsNotEmpty()
        {
            //Arrange
            var logger = A.Fake<ILogger<BookController>>();
            var bookRepo = A.Fake<IArticleRepository<Book>>();
            var library = A.Fake<ILibraryService>();

            BookController bookController = new BookController(logger, bookRepo, library);

            A.CallTo(() => bookRepo.GetById(A<int>.Ignored)).Throws(new NullReferenceException());

            //Act
            var result = await bookController.GetBook(1);
            var objectResult = (BadRequestObjectResult)result.Result;
            var bookResponse = (BookResponse)objectResult.Value;


            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            Assert.IsTrue(objectResult.StatusCode == 400);
            Assert.IsTrue(!String.IsNullOrEmpty(bookResponse!.Error));
        }

        #endregion
        #region CreateBook

        [Test]
        public async Task CreateBook_HappyPath_ErrorIsEmpty()
        {
            //Arrange
            var logger = A.Fake<ILogger<BookController>>();
            var bookRepo = A.Fake<IArticleRepository<Book>>();
            var library = A.Fake<ILibraryService>();
            var request = A.Fake<CreateBookRequest>();

            BookController bookController = new BookController(logger, bookRepo, library);

            A.CallTo(() => bookRepo.Create(A<Book>.Ignored)).Returns(new Book { Author = "Test", Title = "Test", Id = 1 });

            //Act
            var result = await bookController.CreateBook(request);
            var objectResult = (OkObjectResult)result.Result;
            var bookResponse = (BookResponse)objectResult.Value;

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            Assert.IsTrue(objectResult.StatusCode == 200);
            Assert.IsTrue(String.IsNullOrEmpty(bookResponse!.Error));
        }

        [Test]
        public async Task CreateBook_RepoThrowsException_ErrorIsNotEmpty()
        {
            //Arrange
            var logger = A.Fake<ILogger<BookController>>();
            var bookRepo = A.Fake<IArticleRepository<Book>>();
            var library = A.Fake<ILibraryService>();
            var request = A.Fake<CreateBookRequest>();

            BookController bookController = new BookController(logger, bookRepo, library);

            A.CallTo(() => bookRepo.Create(A<Book>.Ignored)).Throws(new NullReferenceException());

            //Act
            var result = await bookController.CreateBook(request);
            var objectResult = (BadRequestObjectResult)result.Result;
            var bookResponse = (BookResponse)objectResult.Value;


            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            Assert.IsTrue(objectResult.StatusCode == 400);
            Assert.IsTrue(!String.IsNullOrEmpty(bookResponse!.Error));
        }

        #endregion
        #region BorrowBook

        [Test]
        public async Task BorrowBook_HappyPath_ErrorIsEmpty()
        {
            //Arrange
            var logger = A.Fake<ILogger<BookController>>();
            var bookRepo = A.Fake<IArticleRepository<Book>>();
            var library = A.Fake<ILibraryService>();
            var request = A.Fake<BorrowRequest>();
            var checkout = A.Fake<Checkout>();

            BookController bookController = new BookController(logger, bookRepo, library);

            A.CallTo(() => library.BorrowBook(A<BorrowRequest>.Ignored)).Returns(checkout);

            //Act
            var result = await bookController.BorrowBook(request);
            var objectResult = (OkObjectResult)result.Result;
            var bookResponse = (BorrowResponse)objectResult.Value;

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            Assert.IsTrue(objectResult.StatusCode == 200);
            Assert.IsTrue(String.IsNullOrEmpty(bookResponse!.Error));
        }

        [Test]
        public async Task BorrowBook_RepoThrowsException_ErrorIsNotEmpty()
        {
            //Arrange
            var logger = A.Fake<ILogger<BookController>>();
            var bookRepo = A.Fake<IArticleRepository<Book>>();
            var library = A.Fake<ILibraryService>();
            var request = A.Fake<BorrowRequest>();

            BookController bookController = new BookController(logger, bookRepo, library);

            A.CallTo(() => library.BorrowBook(A<BorrowRequest>.Ignored)).Throws(new NullReferenceException());

            //Act
            var result = await bookController.BorrowBook(request);
            var objectResult = (BadRequestObjectResult)result.Result;
            var bookResponse = (BorrowResponse)objectResult.Value;


            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            Assert.IsTrue(objectResult.StatusCode == 400);
            Assert.IsTrue(!String.IsNullOrEmpty(bookResponse!.Error));
        }

        #endregion
        #region ReturnBook
        [Test]
        public async Task ReturnBook_HappyPath_ReturnsNoContent()
        {
            //Arrange
            var logger = A.Fake<ILogger<BookController>>();
            var bookRepo = A.Fake<IArticleRepository<Book>>();
            var library = A.Fake<ILibraryService>();
            var request = A.Fake<ReturnRequest>();
            var task = Task.CompletedTask;


            BookController bookController = new BookController(logger, bookRepo, library);

            A.CallTo(() => library.ReturnBook(A<ReturnRequest>.Ignored)).Returns(task);

            //Act
            var result = await bookController.ReturnBook(request);

            //Assert
            Assert.IsInstanceOf<NoContentResult>(result);


        }

        [Test]
        public async Task ReturnBook_RepoThrowsException_ReturnsBadRequest()
        {
            //Arrange
            var logger = A.Fake<ILogger<BookController>>();
            var bookRepo = A.Fake<IArticleRepository<Book>>();
            var library = A.Fake<ILibraryService>();
            var request = A.Fake<ReturnRequest>();

            BookController bookController = new BookController(logger, bookRepo, library);

            A.CallTo(() => library.ReturnBook(A<ReturnRequest>.Ignored)).Throws(new NullReferenceException());

            //Act
            var result = await bookController.ReturnBook(request);


            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion
        #region DeleteBook
        [Test]
        public async Task DeleteBook_HappyPath_ReturnsNoContent()
        {
            //Arrange
            var logger = A.Fake<ILogger<BookController>>();
            var bookRepo = A.Fake<IArticleRepository<Book>>();
            var library = A.Fake<ILibraryService>();
            var request = A.Fake<ReturnRequest>();
            var task = Task.CompletedTask;


            BookController bookController = new BookController(logger, bookRepo, library);

            A.CallTo(() => bookRepo.Remove(A<Book>.Ignored)).Returns(task);

            //Act
            var result = await bookController.DeleteBook(1);

            //Assert
            Assert.IsInstanceOf<NoContentResult>(result);


        }

        [Test]
        public async Task DeleteBook_RepoThrowsException_ReturnsBadRequest()
        {
            //Arrange
            var logger = A.Fake<ILogger<BookController>>();
            var bookRepo = A.Fake<IArticleRepository<Book>>();
            var library = A.Fake<ILibraryService>();
            var request = A.Fake<ReturnRequest>();

            BookController bookController = new BookController(logger, bookRepo, library);

            A.CallTo(() => bookRepo.Remove(A<Book>.Ignored)).Throws(new NullReferenceException());

            //Act
            var result = await bookController.DeleteBook(1);


            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region UpdateBook
        [Test]
        public async Task UpdateBook_HappyPath_ErrorIsEmpty()
        {
            //Arrange
            var logger = A.Fake<ILogger<BookController>>();
            var bookRepo = A.Fake<IArticleRepository<Book>>();
            var library = A.Fake<ILibraryService>();
            var request = A.Fake<UpdateBookRequest>();

            BookController bookController = new BookController(logger, bookRepo, library);

            A.CallTo(() => bookRepo.Update(A<Book>.Ignored)).Returns(new Book { Author = "Test", Title = "Test", Id = 1 });

            //Act
            var result = await bookController.UpdateBook(request);


            //Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task UpdateBook_RepoThrowsException_ErrorIsNotEmpty()
        {
            var logger = A.Fake<ILogger<BookController>>();
            var bookRepo = A.Fake<IArticleRepository<Book>>();
            var library = A.Fake<ILibraryService>();
            var request = A.Fake<UpdateBookRequest>();

            BookController bookController = new BookController(logger, bookRepo, library);

            A.CallTo(() => bookRepo.Update(A<Book>.Ignored)).Throws(new NullReferenceException());

            //Act
            var result = await bookController.UpdateBook(request);

            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        #endregion


    }
}