
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNAuthorizationServer.API.Models.Auth
{
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

    public class GetAccessTokenByUserNameInput
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        public string grant_type { get; set; }
        public long LanguageID { get; set; }
    }
    public class GetAccessTokenByUserNameOutput : BaseResponseModel
    {
        public GetAccessTokenByUserNameOutput()
        {
            UserSession = new UserReturnModel();
        }

        public bool TwoFactorAuthFlag { get; set; }
        public UserReturnModel UserSession { get; set; }
        public string AccessToken { get; set; }
        public string CurrentUserID { get; set; }
    }

    public class GetAllUsersOutput : BaseResponseModel
    {
        public GetAllUsersOutput()
        {
            Users = new List<UserReturnModel>();
        }
        public List<UserReturnModel> Users { get; set; }
    }

    public class GetUserInfoOutput : BaseResponseModel
    {
        public GetUserInfoOutput()
        {
            User = new UserReturnModel();
        }
        public UserReturnModel User { get; set; }
    }

    public class RolesInUserModel
    {
        public string Id { get; set; }
        public List<string> RolesToAssign { get; set; }
    }
}
