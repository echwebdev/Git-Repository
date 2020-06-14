using DataModels.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Services.Auth
{
    public class RegisterUserInput
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long LanguageID { get; set; }
    }
    public class RegisterUserOutput : BaseResponseModel
    {
        //public RegisterUserOutput()
        //{
        //    UserInfo = new GetUserInfoModel();
        //}
        public string Url { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public int Level { get; set; }
        public DateTime JoinDate { get; set; }
        public IList<string> Roles { get; set; }
        public IList<System.Security.Claims.Claim> Claims { get; set; }
        //public GetUserInfoModel UserInfo { get; set; }
    }

    public class LoginByLoginTokenInput
    {
        public string LoginToken { get; set; }
    }
    public class LoginByLoginTokenOutput : BaseResponseModel
    {
        public LoginByLoginTokenOutput()
        {
            UserSession = new UserSessionModel();
        }
        public UserSessionModel UserSession { get; set; }
        public string AccessToken { get; set; }
    }

    public class LoginUserInput
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public long LanguageID { get; set; }
    }
    public class LoginUserOutput : BaseResponseModel
    {
        public LoginUserOutput()
        {
            UserSession = new UserReturnModel();
        }

        public bool TwoFactorAuthFlag { get; set; }
        public UserReturnModel UserSession { get; set; }
        public string AccessToken { get; set; }
        public string CurrentUserID { get; set; }
    }

    public class ChangePasswordInput 
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }
        public string AccessToken { get; set; }
    }

    public class ChangePasswordOutput: BaseResponseModel
    {    }

    public class ForgotPasswordInput
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class ForgotPasswordOutput : BaseResponseModel
    { }

    public class ResetPasswordInput
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }
        public string ResetCode { get; set; }
    }

    public class ResetPasswordOutput : BaseResponseModel
    { }

    public class GetUserSessionByAccessTokenInput
    {
        public string Username { get; set; }
        public string AccessToken { get; set; }
    }

    public class GetUserSessionByAccessTokenOutput
    {
        public GetUserSessionByAccessTokenOutput()
        {
            UserSession = new UserReturnModel();
        }

        public bool TwoFactorAuthFlag { get; set; }
        public UserReturnModel UserSession { get; set; }
        public string AccessToken { get; set; }
        public string CurrentUserID { get; set; }
    }

    public class GetAllUsersInput
    {
        public string AccessToken { get; set; }
    }
    public class GetAllUsersOutput : BaseResponseModel
    {
        public GetAllUsersOutput()
        {
            Users = new List<GetUserInfoModel>();
        }
        public List<GetUserInfoModel> Users { get; set; }
    }

    
    public class GetUserInfoModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public int Level { get; set; }
        public DateTime JoinDate { get; set; }
        public IList<string> Roles { get; set; }
        public IList<System.Security.Claims.Claim> Claims { get; set; }
    }
    public class GetUserInfoInput
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string AccessToken { get; set; }
    }
    public class GetUserInfoOutput : BaseResponseModel
    {
        public GetUserInfoOutput()
        {
            User = new GetUserInfoModel();
        }
        public GetUserInfoModel User { get; set; }
    }

    public class UpdateUserInfoInput
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string AccessToken { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class UpdateUserInfoOutput : BaseResponseModel
    {
        public UpdateUserInfoOutput()
        {
            User = new GetUserInfoModel();
        }
        public GetUserInfoModel User { get; set; }
    }
    public class DeleteUserInput
    {
        public string Id { get; set; }
        public string AccessToken { get; set; }
    }

    public class DeleteUserOutput : BaseResponseModel
    {
    }

    public class AssignRolesToUserInput
    {
        public string Id { get; set; }
        public List<string> RolesToAssign { get; set; }
        public string AccessToken { get; set; }
    }

    public class AssignRolesToUserOutput : BaseResponseModel
    {
        public string Id { get; set; }
    }

    public class UserModel
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool ActiveFlag { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    #region External Logins
    public class ExternalLoginInput
    {
        public string Provider { get; set; }
        public string Response_Type { get; set; }
        public string Client_id { get; set; }
        public string Redirect_uri { get; set; }
    }

    public class ExternalLoginOutput : BaseResponseModel
    {
        public string external_access_token { get; set; }
    }

    public class RegisterExternalInput
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Provider { get; set; }

        [Required]
        public string ExternalAccessToken { get; set; }
    }

    public class RegisterExternalOutput : BaseResponseModel
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public string Token_Type { get; set; }
        public string Expires_In { get; set; }
    }

    #endregion
}
