using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DataModels.Services.Auth;

namespace NNResourceServer.API.Controllers
{
    
    [RoutePrefix("api/auth")]
    //[EnableCors("*", "*", "*")]
    public class AuthController : BaseApiController
    {
        private AuthService _authSvc;

        public AuthController()
        {
            _authSvc = new AuthService();
        }

        private void Init()
        {
            BaseInit();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("RegisterUser")]
        [ResponseType(typeof(RegisterUserOutput))]
        public IHttpActionResult RegisterUser(RegisterUserInput input)
        {
            RegisterUserOutput output = _authSvc.RegisterUser(input);
            return Ok(output);
        }

        [Authorize]
        [HttpPost]
        [Route("ChangePassword")]
        [ResponseType(typeof(ChangePasswordOutput))]
        public IHttpActionResult ChangePassword(ChangePasswordInput input)
        {
            Init();
            ChangePasswordOutput output = _authSvc.ChangePassword(input);
            return Ok(output);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ForgotPassword")]
        [ResponseType(typeof(ForgotPasswordOutput))]
        public IHttpActionResult ForgotPassword(ForgotPasswordInput input)
        {
            Init();
            ForgotPasswordOutput output = _authSvc.ForgotPassword(input);
            return Ok(output);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ResetPassword")]
        [ResponseType(typeof(ResetPasswordOutput))]
        public IHttpActionResult ResetPassword(ResetPasswordInput input)
        {
            Init();
            ResetPasswordOutput output = _authSvc.ResetPassword(input);
            return Ok(output);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("LoginUser")]
        [ResponseType(typeof(LoginUserOutput))]
        public IHttpActionResult LoginUser(LoginUserInput input)
        {
            LoginUserOutput output = _authSvc.LoginUser(input);
            return Ok(output);
        }

        [Authorize]
        [HttpPost]
        [Route("GetUser")]
        [ResponseType(typeof(GetUserInfoOutput))]
        [Extensions.Auth]
        public IHttpActionResult GetUserInfo(GetUserInfoInput input)
        {
            Init();
            input.AccessToken = ApiSession.AccessToken;
            GetUserInfoOutput output = _authSvc.GetUserInfo(input);
            return Ok(output);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("GetAllUsers")]
        [ResponseType(typeof(GetAllUsersOutput))]
        [Extensions.Auth]
        public IHttpActionResult GetAllUsers(GetAllUsersInput input)
        {
            Init();
            input.AccessToken = ApiSession.AccessToken;
            GetAllUsersOutput output = _authSvc.GetAllUsers(input);
            return Ok(output);
        }

        [Authorize]
        [HttpPost]
        [Route("UpdateUser")]
        [ResponseType(typeof(UpdateUserInfoOutput))]
        [Extensions.Auth]
        public IHttpActionResult UpdateUserInfo(UpdateUserInfoInput input)
        {
            Init();
            input.AccessToken = ApiSession.AccessToken;
            UpdateUserInfoOutput output = _authSvc.UpdateUserInfo(input);
            return Ok(output);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("DeleteUser")]
        [ResponseType(typeof(DeleteUserOutput))]
        [Extensions.Auth]
        public IHttpActionResult DeleteUser(DeleteUserInput input)
        {
            Init();
            input.AccessToken = ApiSession.AccessToken;
            DeleteUserOutput output = _authSvc.DeleteUser(input);
            return Ok(output);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("RolesInUser")]
        [ResponseType(typeof(AssignRolesToUserOutput))]
        [Extensions.Auth]
        public IHttpActionResult AssignRolesToUser(AssignRolesToUserInput input)
        {
            Init();
            input.AccessToken = ApiSession.AccessToken;
            AssignRolesToUserOutput output = _authSvc.AssignRolesToUser(input);
            return Ok(output);
        }

        #region External Login
        
        [AllowAnonymous]
        [Route("RegisterExternal")]
        [ResponseType(typeof(RegisterExternalOutput))]
        public IHttpActionResult RegisterExternal(RegisterExternalInput input)
        {
            RegisterExternalOutput output = _authSvc.RegisterExternal(input);
            return Ok(output);
        }
        #endregion
    }
}