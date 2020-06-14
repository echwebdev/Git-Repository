using DataModels.Core;
using DataModels.Services.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IAuthService
    {
        //LoginUserOutput GetUserSessionByAccessToken(string AccessToken);
        RegisterUserOutput RegisterUser(RegisterUserInput input);
        LoginUserOutput LoginUser(LoginUserInput input);
        GetUserInfoOutput GetUserInfo(GetUserInfoInput input);
    }
}
