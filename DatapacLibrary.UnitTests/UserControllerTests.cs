using DatapacLibrary.Controllers;
using DatapacLibrary.DTO;
using DatapacLibrary.DTO.Requests;
using DatapacLibrary.Interfaces;
using DatapacLibrary.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DatapacLibrary.UnitTests
{
    public class UserControllerTests
    {
        #region GetUser

        [Test]
        public async Task GetUser_HappyPath_ErrorIsEmpty()
        {
            //Arrange
            var logger = A.Fake<ILogger<UserController>>();
            var userRepo = A.Fake<IArticleRepository<User>>();

            UserController userController = new UserController(logger, userRepo);

            A.CallTo(() => userRepo.GetById(A<int>.Ignored)).Returns(new User
            {
                FirstName = "Test",
                LastName = "Test",
                CardNumber = 1,
                Email = "test@test.com",
                IsBlocked = false
            });

            //Act
            var result = await userController.GetUser(1);
            var objectResult = (OkObjectResult)result.Result;
            var userResponse = (UserResponse)objectResult.Value;

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            Assert.IsTrue(objectResult.StatusCode == 200);
            Assert.IsTrue(String.IsNullOrEmpty(userResponse.Error));
        }

        [Test]
        public async Task GetUser_RepoThrowsException_ErrorIsNotEmpty()
        {
            //Arrange
            var logger = A.Fake<ILogger<UserController>>();
            var userRepo = A.Fake<IArticleRepository<User>>();

            UserController userController = new UserController(logger, userRepo);

            A.CallTo(() => userRepo.GetById(A<int>.Ignored)).Throws(new NullReferenceException());

            //Act
            var result = await userController.GetUser(1);
            var objectResult = (BadRequestObjectResult)result.Result;
            var userResponse = (UserResponse)objectResult.Value;


            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            Assert.IsTrue(objectResult.StatusCode == 400);
            Assert.IsTrue(!String.IsNullOrEmpty(userResponse.Error));
        }

        #endregion
        #region CreateUser

        [Test]
        public async Task CreateUser_HappyPath_ErrorIsEmpty()
        {
            //Arrange
            var logger = A.Fake<ILogger<UserController>>();
            var userRepo = A.Fake<IArticleRepository<User>>();
            var request = A.Fake<CreateUserRequest>();

            UserController userController = new UserController(logger, userRepo);

            A.CallTo(() => userRepo.Create(A<User>.Ignored)).Returns(new User
            {
                FirstName = "Test",
                LastName = "Test",
                CardNumber = 1,
                Email = "test@test.com",
                IsBlocked = false
            });

            //Act
            var result = await userController.CreateUser(request);
            var objectResult = (OkObjectResult)result.Result;
            var userResponse = (UserResponse)objectResult.Value;

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            Assert.IsTrue(objectResult.StatusCode == 200);
            Assert.IsTrue(String.IsNullOrEmpty(userResponse!.Error));
        }

        [Test]
        public async Task CreateUser_RepoThrowsException_ErrorIsNotEmpty()
        {
            //Arrange
            var logger = A.Fake<ILogger<UserController>>();
            var userRepo = A.Fake<IArticleRepository<User>>();
            var request = A.Fake<CreateUserRequest>();

            UserController userController = new UserController(logger, userRepo);

            A.CallTo(() => userRepo.Create(A<User>.Ignored)).Throws(new NullReferenceException());

            //Act
            var result = await userController.CreateUser(request);
            var objectResult = (BadRequestObjectResult)result.Result;
            var userResponse = (UserResponse)objectResult.Value;


            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            Assert.IsTrue(objectResult.StatusCode == 400);
            Assert.IsTrue(!String.IsNullOrEmpty(userResponse!.Error));
        }

        #endregion
        #region DeleteUser
        [Test]
        public async Task DeleteUser_HappyPath_ReturnsNoContent()
        {
            //Arrange
            var logger = A.Fake<ILogger<UserController>>();
            var userRepo = A.Fake<IArticleRepository<User>>();
            var request = A.Fake<CreateUserRequest>();

            UserController userController = new UserController(logger, userRepo);

            A.CallTo(() => userRepo.Remove(A<User>.Ignored)).Returns(Task.CompletedTask);

            //Act
            var result = await userController.DeleteUser(1);

            //Assert
            Assert.IsInstanceOf<NoContentResult>(result);


        }

        [Test]
        public async Task DeleteUser_RepoThrowsException_ReturnsBadRequest()
        {
            //Arrange
            var logger = A.Fake<ILogger<UserController>>();
            var userRepo = A.Fake<IArticleRepository<User>>();
            var request = A.Fake<CreateUserRequest>();

            UserController userController = new UserController(logger, userRepo);

            A.CallTo(() => userRepo.Remove(A<User>.Ignored)).Throws(new NullReferenceException());

            //Act
            var result = await userController.DeleteUser(1);


            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion
        #region UpdateUser
        [Test]
        public async Task UpdateUser_HappyPath_ReturnsNoContent()
        {
            //Arrange
            var logger = A.Fake<ILogger<UserController>>();
            var userRepo = A.Fake<IArticleRepository<User>>();
            var request = A.Fake<UpdateUserRequest>();

            UserController userController = new UserController(logger, userRepo);

            A.CallTo(() => userRepo.Update(A<User>.Ignored)).Returns(new User
            {
                FirstName = "Test",
                LastName = "Test",
                CardNumber = 1,
                Email = "test@test.com",
                IsBlocked = false
            });

            //Act
            var result = await userController.UpdateUser(request);

            //Assert
            Assert.IsInstanceOf<NoContentResult>(result);


        }

        [Test]
        public async Task UpdateUser_RepoThrowsException_ReturnsBadRequest()
        {
            //Arrange
            var logger = A.Fake<ILogger<UserController>>();
            var userRepo = A.Fake<IArticleRepository<User>>();
            var request = A.Fake<UpdateUserRequest>();

            UserController userController = new UserController(logger, userRepo);

            A.CallTo(() => userRepo.Update(A<User>.Ignored)).Throws(new NullReferenceException());

            //Act
            var result = await userController.UpdateUser(request);


            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion


    }
}