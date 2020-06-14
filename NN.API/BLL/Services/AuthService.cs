using BLL.Common;
using DataModels.Core;
using DataModels.Services.Auth;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http.Results;

namespace BLL.Services
{
    public class AuthService : BaseService, Interfaces.IAuthService
    {
        private AccessToken _CT;

        public AuthService()
        {
            _CT = new AccessToken();
        }

        //Private Methods
        private LoginByLoginTokenOutput LoginByLoginToken(string accessToken)
        {
            var accessTokenObj = _CT.aToken.Deserialize(accessToken);

            LoginByLoginTokenInput Input = new LoginByLoginTokenInput()
            {
                LoginToken = accessTokenObj.AccessToken
            };
            RestHTTP http = new RestHTTP();
            RestSharp.RestRequest req = new RestSharp.RestRequest("api/auth/LoginByLoginToken", RestSharp.Method.POST);
            req.AddObject(Input);

            var response = http.HttpPost<LoginByLoginTokenOutput>(req);
            return response;
        }


        //Public Methods
        public RegisterUserOutput RegisterUser(RegisterUserInput input)
        {
            try
            {
                RestHTTP http = new RestHTTP();
                RestRequest req = new RestRequest("api/accounts/create", RestSharp.Method.POST);
                req.AddJsonBody(input);
                RegisterUserOutput response = http.HttpPost<RegisterUserOutput>(req);

                return response;
            }
            catch (Exception ex)
            {
                WriteLogFile.Append("RegisterUser : ");
                WriteLogFile.Append(ex.Message);
                WriteLogFile.Append(ex.StackTrace);
            }
            return null;
        }

        public ChangePasswordOutput ChangePassword(ChangePasswordInput input)
        {
            try
            {
                RestHTTP http = new RestHTTP();
                RestRequest req = new RestRequest("api/accounts/ChangePassword", RestSharp.Method.POST);
                req.AddHeader("Content-Type", "application/json");
                req.AddHeader("Authorization", "Bearer " + input.AccessToken);
                req.AddJsonBody(input);
                ChangePasswordOutput response = http.HttpPost<ChangePasswordOutput>(req);

                return response;
            }
            catch (Exception ex)
            {
                WriteLogFile.Append("ConfirmPassword : ");
                WriteLogFile.Append(ex.Message);
                WriteLogFile.Append(ex.StackTrace);
            }
            return null;
        }

        public ForgotPasswordOutput ForgotPassword(ForgotPasswordInput input)
        {
            try
            {
                RestHTTP http = new RestHTTP();
                RestRequest req = new RestRequest("api/accounts/ForgotPassword", RestSharp.Method.POST);
                req.AddHeader("Content-Type", "application/json");
                req.AddJsonBody(input);
                ForgotPasswordOutput response = http.HttpPost<ForgotPasswordOutput>(req);

                return response;
            }
            catch (Exception ex)
            {
                WriteLogFile.Append("ForgotPassword : ");
                WriteLogFile.Append(ex.Message);
                WriteLogFile.Append(ex.StackTrace);
            }
            return null;
        }

        public ResetPasswordOutput ResetPassword(ResetPasswordInput input)
        {
            try
            {
                RestHTTP http = new RestHTTP();
                RestRequest req = new RestRequest("api/accounts/ResetPassword", RestSharp.Method.POST);
                req.AddHeader("Content-Type", "application/json");
                req.AddJsonBody(input);
                ResetPasswordOutput response = http.HttpPost<ResetPasswordOutput>(req);

                return response;
            }
            catch (Exception ex)
            {
                WriteLogFile.Append("ResetPassword : ");
                WriteLogFile.Append(ex.Message);
                WriteLogFile.Append(ex.StackTrace);
            }
            return null;
        }

        public LoginUserOutput LoginUser(LoginUserInput input)
        {
            RestHTTP http = new RestHTTP();
            RestSharp.RestRequest req = new RestSharp.RestRequest("api/accounts/GetAccessTokenByUserName", RestSharp.Method.POST);
            req.AddObject(input);

            RestSharp.RestClient client = new RestSharp.RestClient(WebConfigurationManager.AppSettings["AuthServerAPI"]);
            var response = client.Execute<LoginUserOutput>(req);
            LoginUserOutput result = JsonConvert.DeserializeObject<LoginUserOutput>(response.Content, new ClaimConverter());

            return result;
        }

        public GetUserSessionByAccessTokenOutput GetUserSessionByAccessToken(string accessToken)
        {
            try 
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(accessToken).Payload.ToList();
                var username = token.Where(a => a.Key == "unique_name").Select(b => b.Value).FirstOrDefault();

                //string username = new System.Collections.Generic.Mscorlib_DictionaryDebugView<string, object>(token.Payload).Items[1].Value;
                //var accessTokenObj = _CT.aToken.Deserialize(accessToken);
                GetUserSessionByAccessTokenInput input = new GetUserSessionByAccessTokenInput();
                input.Username = username.ToString();
                RestHTTP http = new RestHTTP();
                RestSharp.RestRequest req = new RestSharp.RestRequest("api/accounts/GetUserSessionByAccessToken", RestSharp.Method.POST);
                req.AddHeader("Authorization", "Bearer " + input.AccessToken);
                req.AddObject(input);

                RestSharp.RestClient client = new RestSharp.RestClient(WebConfigurationManager.AppSettings["AuthServerAPI"]);
                var response = client.Execute<GetUserSessionByAccessTokenOutput>(req);

                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    GetUserSessionByAccessTokenOutput result = JsonConvert.DeserializeObject<GetUserSessionByAccessTokenOutput>(response.Content, new ClaimConverter());
                    result.AccessToken = accessToken;
                    return result;
                }
                else 
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GetAllUsersOutput GetAllUsers(GetAllUsersInput input)
        {
            try
            {
                RestHTTP http = new RestHTTP();
                RestRequest req = null;
                req = new RestRequest("api/accounts/users", RestSharp.Method.POST);
                
                req.AddHeader("Authorization", "Bearer " + input.AccessToken);
                req.RequestFormat = DataFormat.Json;
                RestSharp.RestClient client = new RestSharp.RestClient(WebConfigurationManager.AppSettings["AuthServerAPI"]);
                var response = client.Execute<GetAllUsersOutput>(req);

                GetAllUsersOutput output = new GetAllUsersOutput();
                output.Users = JsonConvert.DeserializeObject<List<GetUserInfoModel>>(response.Content, new ClaimConverter());

                return output;
            }
            catch (Exception ex)
            {
                WriteLogFile.Append("GetAllUsers : ");
                WriteLogFile.Append(ex.Message);
                WriteLogFile.Append(ex.StackTrace);
                return default(GetAllUsersOutput);
            }
        }

        public GetUserInfoOutput GetUserInfo(GetUserInfoInput input)
        {
            try
            {
                RestHTTP http = new RestHTTP();
                RestRequest req = null;
                if (!string.IsNullOrEmpty(input.Id))
                {
                    req = new RestRequest("api/accounts/getUser", RestSharp.Method.POST);
                }
                else if(!string.IsNullOrEmpty(input.Username))
                {
                    req = new RestRequest("api/accounts/getUserByUsername", RestSharp.Method.POST);
                }
                else
                {
                    return default(GetUserInfoOutput); ;
                }
                
                req.AddHeader("Authorization", "Bearer " + input.AccessToken);
                req.RequestFormat = DataFormat.Json;
                req.AddJsonBody(input);

                var response = http.HttpPost<GetUserInfoOutput>(req);
                
                return response;
            }
            catch (Exception ex)
            {
                WriteLogFile.Append("GetUserInfo : ");
                WriteLogFile.Append(ex.Message);
                WriteLogFile.Append(ex.StackTrace);
                return default(GetUserInfoOutput);
            }
        }

        public UpdateUserInfoOutput UpdateUserInfo(UpdateUserInfoInput input)
        {
            try
            {
                RestHTTP http = new RestHTTP();
                RestRequest req = null;
                if (!string.IsNullOrEmpty(input.Id))
                {
                    req = new RestRequest("api/accounts/update", RestSharp.Method.POST);
                }
                
                req.AddHeader("Authorization", "Bearer " + input.AccessToken);
                req.RequestFormat = DataFormat.Json;
                req.AddJsonBody(input);

                var response = http.HttpPost<UpdateUserInfoOutput>(req);

                return response;
            }
            catch (Exception ex)
            {
                WriteLogFile.Append("UpdateUserInfo : ");
                WriteLogFile.Append(ex.Message);
                WriteLogFile.Append(ex.StackTrace);
                return default(UpdateUserInfoOutput);
            }
        }

        public DeleteUserOutput DeleteUser(DeleteUserInput input)
        {
            try
            {
                RestHTTP http = new RestHTTP();
                RestRequest req = new RestRequest("api/accounts/delete", RestSharp.Method.DELETE);
                req.AddHeader("Content-Type", "application/json");
                req.AddHeader("Authorization", "Bearer " + input.AccessToken);
                req.AddJsonBody(input);
                DeleteUserOutput response = http.HttpPost<DeleteUserOutput>(req);

                return response;
            }
            catch (Exception ex)
            {
                WriteLogFile.Append("DeleteUser : ");
                WriteLogFile.Append(ex.Message);
                WriteLogFile.Append(ex.StackTrace);
            }
            return null;
        }

        public AssignRolesToUserOutput AssignRolesToUser(AssignRolesToUserInput input)
        {
            try
            {
                RestHTTP http = new RestHTTP();
                RestRequest req = new RestRequest("api/accounts/assignRolesToUser", RestSharp.Method.POST);
                req.AddHeader("Content-Type", "application/json");
                req.AddHeader("Authorization", "Bearer " + input.AccessToken);
                req.AddJsonBody(input);


                var response = http.HttpPost<object>(req);

                AssignRolesToUserOutput output = new AssignRolesToUserOutput();
                output.Id = JsonConvert.DeserializeObject<string>(response.ToString());

                return output;
            }
            catch (Exception ex)
            {
                WriteLogFile.Append("AssignRolesToUser : ");
                WriteLogFile.Append(ex.Message);
                WriteLogFile.Append(ex.StackTrace);
            }
            return null;
        }

        #region External Logins
        
        public RegisterExternalOutput RegisterExternal(RegisterExternalInput input)
        {
            RestHTTP http = new RestHTTP();
            RestSharp.RestRequest req = new RestSharp.RestRequest("api/accounts/RegisterExternal", RestSharp.Method.POST);
            req.AddJsonBody(input);

            var response = http.HttpPost<RegisterExternalOutput>(req);

            return response;
        }
        #endregion
    }
}
