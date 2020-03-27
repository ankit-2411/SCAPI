using SCAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
//using System.Web.Http.;

namespace SCAPI.Controllers
{
    [System.Web.Script.Services.ScriptService]
    //[EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-Custom-Header")]
    public class UserController : ApiController
    {
        /// <summary>
        /// Get list of all the existing users
        /// </summary>
        /// <returns>
        /// If successful, this operation returns the list of all the registered users.
        /// If unsuccessful, an appropriate HTTP error code is returned.
        /// </returns>
        // GET: api/User
        [ResponseType(typeof(IEnumerable<User>))]
        public IHttpActionResult Get()
        {
            try
            {
                var users = DBUtils.GetUsersList();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Get list of existing user by UserId. 
        /// </summary>
        /// <param name="userId">Numeric Id for the targetted user.</param>
        /// <returns>
        /// If successful, this operation returns the registered user with the matching id.
        /// If unsuccessful, an appropriate HTTP error code is returned.
        /// </returns>
        // GET: api/User/5
        [Route("api/User/{userId}")]
        [ResponseType(typeof(User))]
        public IHttpActionResult Get(int userId)
        {
            #region Validations
            if (userId == 0)
            {
                return BadRequest("User Not Found");
            }
            #endregion

            try
            {
                var user = DBUtils.GetSingleUser(userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Adds a new user to the system.
        /// </summary>
        /// <param name="user">Required data object for the user object to be created.</param>
        /// <returns>
        /// If proper object is received, and the user creation is successful, HTTP code for success is returned.
        /// If unsuccessful due to bad data or failures, an appropriate HTTP error code is returned.
        /// </returns>
        // POST: api/User
        [ResponseType(typeof(string))]
        public IHttpActionResult Post([FromBody]UserBase user)
        {
            #region Validations
            if (user == null)
            {
                return BadRequest("Invalid data");
            }
            
            #endregion

            try
            {
                var newUserId = DBUtils.AddUser(user);

                if (newUserId > 0)
                    return Ok($"User created with id: {newUserId}");
                else
                    return InternalServerError(new Exception("Cannot create user!"));
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Updates the user data for the given userId
        /// </summary>
        /// <param name="userId">Numeric Id for the user that needs to be deleted</param>
        /// <param name="user">Required data object for the user to be updated.</param>
        /// <returns>
        /// If user data updates are successful, HTTP code for success is returned.
        /// In cases for invalid userId, bad data in the request, or internal failures, an appropriate HTTP error code is returned. 
        /// </returns>
        // PUT: api/User/5
        [Route("api/User/{userId}")]
        [ResponseType(typeof(HttpStatusCode))]
        public IHttpActionResult Put(int userId, [FromBody]User user)
        {
            #region Validations
            if (userId == 0)
            {
                return BadRequest("UserId required");
            }
            if (user == null)
            {
                return BadRequest("Invalid User data");
            }
            #endregion

            try
            {
                user.UserId = userId;
                var result = DBUtils.UpdateUser(user);

                if (result == DBUtils.ResultStatus.Success)
                    return Ok();
                else if (result == DBUtils.ResultStatus.RecordNotFound)
                    return BadRequest("Invalid UserId");
                else
                    return InternalServerError(new Exception("Cannot update user!"));
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Updates the user data for the provided userId
        /// </summary>
        /// <param name="userId">Numeric Id for the user that needs to be deleted</param>
        /// <returns>
        /// If user data deletion is successful, HTTP code for success is returned.
        /// In cases for invalid userId, or internal failures, an appropriate HTTP error code is returned. 
        /// </returns>
        // DELETE: api/User/5
        [Route("api/User/{userId}")]
        [ResponseType(typeof(HttpStatusCode))]
        public IHttpActionResult Delete(int userId)
        {
            #region Validations
            if (userId == 0)
            {
                return BadRequest("UserId required");
            }
            #endregion

            try
            {
                var result = DBUtils.DeleteUser(userId);

                if (result == DBUtils.ResultStatus.Success)
                    return Ok();
                else if (result == DBUtils.ResultStatus.RecordNotFound)
                    return BadRequest("Invalid UserId");
                else
                    return InternalServerError(new Exception("Cannot delete user!"));
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}
