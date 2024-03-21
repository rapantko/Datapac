using DatapacLibrary.DTO;
using DatapacLibrary.DTO.Requests;
using DatapacLibrary.Interfaces;
using DatapacLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;


namespace DatapacLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        #region Init
        private readonly ILogger<UserController> logger;
        private readonly IArticleRepository<User> userDirectory;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="userDirectory"></param>
        public UserController(ILogger<UserController> logger, IArticleRepository<User> userDirectory)
        {
            this.logger = logger;
            this.userDirectory = userDirectory;
        }
        #endregion
        #region GetUser
        /// <summary>
        /// Get user
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        [HttpGet("{cardNumber}")]
        [ProducesResponseTypeAttribute(typeof(UserResponse), 200)]
        public async Task<ActionResult<UserResponse>> GetUser(int cardNumber)
        {
            try
            {
                var user = await userDirectory.GetById(cardNumber);
                return Ok(new UserResponse
                {
                    CardNumber = user!.CardNumber,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.StackTrace);
                return BadRequest(new UserResponse { CardNumber = cardNumber, Error = ex.Message });
            }
        }
        #endregion
        #region CreateUser
        /// <summary>
        ///  Register new user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseTypeAttribute(typeof(UserResponse), 200)]
        public async Task<ActionResult<UserResponse>> CreateUser([DisallowNull] CreateUserRequest request)
        {
            try
            {
                var newUser = await userDirectory.Create(new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email
                });

                logger.LogInformation($"User has been registered with card number: {newUser.CardNumber}");
                return Ok(new UserResponse
                {
                    CardNumber = newUser.CardNumber,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Email = newUser.Email
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.StackTrace);
                return BadRequest(new UserResponse { Error = ex.Message });
            }
        }
        #endregion
        #region UpdateUser
        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseTypeAttribute(204)]
        public async Task<ActionResult> UpdateUser(UpdateUserRequest request)
        {
            try
            {
                // Update existing user
                var user = await userDirectory.Update(new User
                {
                    CardNumber = request.CardNumber,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email
                });

                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.StackTrace);
                return BadRequest(new UserResponse { CardNumber = request.CardNumber, Error = ex.Message });
            }
        }
        #endregion
        #region DeleteUser
        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        [HttpDelete("{cardNumber}")]
        public async Task<ActionResult> DeleteUser(int cardNumber)
        {
            try
            {
                // First step is to get identification for particular user
                var user = await userDirectory.GetById(cardNumber);
                // Remove user
                await userDirectory.Remove(user!);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
