using NNAuthorizationServer.API.Infrastructure;
using NNAuthorizationServer.API.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using static NNAuthorizationServer.API.Models.AccountBindingModels;
using System.Web.Http.Description;
using NNAuthorizationServer.API.Models.Auth;
using System.Net.Http.Headers;
using NNAuthorizationServer.API.Services;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using NNAuthorizationServer.API.Results;
using System.Security.Claims;
using Newtonsoft.Json.Linq;
using Microsoft.Owin.Security.OAuth;
using Microsoft.AspNet.Identity.Owin;
using NNAuthorizationServer.API.Repository;

namespace NNAuthorizationServer.API.Controllers
{
	[RoutePrefix("api/accounts")]
	public class AccountsController : BaseApiController
    {
		AuthRepository _repo = null;

		public AccountsController()
        {
			_repo = new AuthRepository();
		}
		private IAuthenticationManager Authentication
		{
			get { return Request.GetOwinContext().Authentication; }
		}

		[Authorize(Roles = "Admin")]
		[Route("users")]
		[HttpPost]
		public IHttpActionResult GetUsers()
        {
			return Ok(this.AppUserManager.Users.ToList().Select(u => this.TheModelFactory.Create(u)));
        }

		[Authorize]
		[Route("getUser", Name = "GetUserById")]
		[HttpPost]
		public async Task<IHttpActionResult> GetUser(GetUserBindingModel model)
		{
			var user = await this.AppUserManager.FindByIdAsync(model.Id);

			if (user != null)
			{
				GetUserInfoOutput output = new GetUserInfoOutput();
				output.User = this.TheModelFactory.Create(user);
				return Ok(output);
			}

			return NotFound();
		}

		[Authorize]
		[Route("getUserbyUsername")]
		[HttpPost]
		public async Task<IHttpActionResult> GetUserByName(GetUserBindingModel model)
		{
			var user = await this.AppUserManager.FindByNameAsync(model.Username);

			if (user != null)
			{
				GetUserInfoOutput output = new GetUserInfoOutput();
				output.User = this.TheModelFactory.Create(user);
				return Ok(output);
			}

			return NotFound();
		}

		// register method and send email to confirm
		[AllowAnonymous]
		[Route("create")]
		public async Task<IHttpActionResult> CreateUser(CreateUserBindingModel createUserModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var user = new ApplicationUser()
			{
				UserName = createUserModel.Username,
				Email = createUserModel.Email,
				FirstName = createUserModel.FirstName,
				LastName = createUserModel.LastName,
				Level = 3,
				JoinDate = DateTime.Now.Date,
			};

			IdentityResult addUserResult = await this.AppUserManager.CreateAsync(user, createUserModel.Password);

			if (!addUserResult.Succeeded)
			{
				return GetErrorResult(addUserResult);
			}

			string code = await this.AppUserManager.GenerateEmailConfirmationTokenAsync(user.Id);

			var callbackUrl = new Uri(Url.Link("ConfirmEmailRoute", new { userId = user.Id, code = code }));

			await this.AppUserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

			Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

			return Created(locationHeader, TheModelFactory.Create(user));
		}

		[AllowAnonymous]
		[Route("ConfirmEmail", Name = "ConfirmEmailRoute")]
		[HttpGet]
		public async Task<IHttpActionResult> ConfirmEmail(string userId = "", string code = "")
		{
			if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
			{
				ModelState.AddModelError("", "User Id and Code are required");
				return BadRequest(ModelState);
			}

			IdentityResult result = await this.AppUserManager.ConfirmEmailAsync(userId, code);

			if (result.Succeeded)
			{
				return Ok();
			}
			else
			{
				return GetErrorResult(result);
			}
		}

		[Authorize]
		[HttpPost]
		[Route("ChangePassword")]
		public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			IdentityResult result = await this.AppUserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

			if (!result.Succeeded)
			{
				return GetErrorResult(result);
			}

			return Ok();
		}

		[AllowAnonymous]
		[HttpPost]
		[Route("ForgotPassword")]
		public async Task<IHttpActionResult> ForgotPassword(ForgotPasswordBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
					return BadRequest(ModelState);
                }

				var user = await this.AppUserManager.FindByEmailAsync(model.Email);
				if(user == null && !(await this.AppUserManager.IsEmailConfirmedAsync(user.Id)))
                {
					return BadRequest(ModelState);
                }

				var code = await this.AppUserManager.GeneratePasswordResetTokenAsync(user.Id);
				var callbackUrl = new Uri(Url.Link("ConfirmEmail", new { userId = user.Id, code = code }));

				await this.AppUserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");

				return Ok();
			}
			catch(Exception ex)
            {
				throw new Exception(ex.Message);
            }
        }

		[Authorize]
		[HttpPost]
		[Route("ResetPassword")]
		public async Task<IHttpActionResult> ResetPassword(ResetPasswordBindingModel model)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				var user = await this.AppUserManager.FindByIdAsync(User.Identity.GetUserId());

				if(user == null)
                {
					return NotFound();
                }

				var result = await this.AppUserManager.ResetPasswordAsync(user.Id, model.resetCode, model.NewPassword);
                if (!result.Succeeded)
                {
					return GetErrorResult(result);
					
                }

				//ApplicationDbContext context = new ApplicationDbContext();
				//UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(context);
				//UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(store);
				//string userId = User.Identity.GetUserId();//the current principal user associated with this request
				//string newPassword = model.NewPassword;
				//string hashedNewPassword = UserManager.PasswordHasher.HashPassword(newPassword);
				//ApplicationUser cUser = await store.FindByIdAsync(userId);
				//await store.SetPasswordHashAsync(cUser, hashedNewPassword);
				//await store.UpdateAsync(cUser);
				return Ok();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		[Authorize]
		[HttpPost]
		[Route("update")]
		public async Task<IHttpActionResult> UpdateUser(UpdateUserBindingModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var user = await this.AppUserManager.FindByIdAsync(model.Id);

			if (user != null)
			{
				user.UserName = model.Username;
				user.FirstName = model.FirstName;
				user.LastName = model.LastName;

				IdentityResult updateUserResult = await this.AppUserManager.UpdateAsync(user);

				if (!updateUserResult.Succeeded)
				{
					return GetErrorResult(updateUserResult);
				}

				GetUserInfoOutput output = new GetUserInfoOutput();
				output.User = this.TheModelFactory.Create(user);
				return Ok(output);
			}

			return NotFound();
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		[Route("delete")]
		public async Task<IHttpActionResult> DeleteUser(DeleteUserBindingModel model)
		{

			//Only Admin can delete users
			var appUser = await this.AppUserManager.FindByIdAsync(model.Id);

			if (appUser != null)
			{
				IdentityResult result = await this.AppUserManager.DeleteAsync(appUser);

				if (!result.Succeeded)
				{
					return GetErrorResult(result);
				}

				return Ok();

			}

			return NotFound();
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		[Route("assignRolesToUser")]
		public async Task<IHttpActionResult> AssignRolesToUser(RolesInUserModel model)
		{

			var appUser = await this.AppUserManager.FindByIdAsync(model.Id);

			if (appUser == null)
			{
				return NotFound();
			}

			var currentRoles = await this.AppUserManager.GetRolesAsync(appUser.Id);

			var rolesNotExists = model.RolesToAssign.Except(this.AppRoleManager.Roles.Select(x => x.Name)).ToArray();

			if (rolesNotExists.Count() > 0)
			{

				ModelState.AddModelError("", string.Format("Roles '{0}' does not exixts in the system", string.Join(",", rolesNotExists)));
				return BadRequest(ModelState);
			}

			IdentityResult removeResult = await this.AppUserManager.RemoveFromRolesAsync(appUser.Id, currentRoles.ToArray());

			if (!removeResult.Succeeded)
			{
				ModelState.AddModelError("", "Failed to remove user roles");
				return BadRequest(ModelState);
			}

			IdentityResult addResult = await this.AppUserManager.AddToRolesAsync(appUser.Id, model.RolesToAssign.ToArray());

			if (!addResult.Succeeded)
			{
				ModelState.AddModelError("", "Failed to add user roles");
				return BadRequest(ModelState);
			}

			return Ok("'" + model.Id + "'");
		}

		[Authorize(Roles = "Admin")]
		[HttpPut]
		[Route("user/{id:guid}/assignclaims")]
		public async Task<IHttpActionResult> AssignClaimsToUser([FromUri] string id, [FromBody] List<ClaimBindingModel> claimsToAssign)
		{

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var appUser = await this.AppUserManager.FindByIdAsync(id);

			if (appUser == null)
			{
				return NotFound();
			}

			foreach (ClaimBindingModel claimModel in claimsToAssign)
			{
				if (appUser.Claims.Any(c => c.ClaimType == claimModel.Type))
				{

					await this.AppUserManager.RemoveClaimAsync(id, ExtendedClaimsProvider.CreateClaim(claimModel.Type, claimModel.Value));
				}

				await this.AppUserManager.AddClaimAsync(id, ExtendedClaimsProvider.CreateClaim(claimModel.Type, claimModel.Value));
			}

			return Ok();
		}

		[Authorize(Roles = "Admin")]
		[Route("user/{id:guid}/removeclaims")]
		[HttpPut]
		public async Task<IHttpActionResult> RemoveClaimsFromUser([FromUri] string id, [FromBody] List<ClaimBindingModel> claimsToRemove)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var appUser = await this.AppUserManager.FindByIdAsync(id);

			if (appUser == null)
			{
				return NotFound();
			}

			foreach (ClaimBindingModel claimModel in claimsToRemove)
			{
				if (appUser.Claims.Any(c => c.ClaimType == claimModel.Type))
				{
					await this.AppUserManager.RemoveClaimAsync(id, ExtendedClaimsProvider.CreateClaim(claimModel.Type, claimModel.Value));
				}
			}

			return Ok();
		}

		// login method
		[AllowAnonymous]
		[HttpPost]
		[Route("GetAccessTokenByUserName")]
		[ResponseType(typeof(GetAccessTokenByUserNameOutput))]
		public async Task<IHttpActionResult> GetAccessTokenByUserName(GetAccessTokenByUserNameInput input)
		{
			RestHTTP http = new RestHTTP();
			RestSharp.RestRequest req = new RestSharp.RestRequest("/oauth/token", RestSharp.Method.POST);
			input.grant_type = "password";
			req.AddObject(input);
			
			GetAccessTokenByUserNameOutput output = http.HttpPost<GetAccessTokenByUserNameOutput>(req);

			if (output != null && !string.IsNullOrEmpty(output.AccessToken))
			{
				var user = await this.AppUserManager.FindByNameAsync(input.username);

				if (user != null)
				{
					output.CurrentUserID = user.Id;
					output.TwoFactorAuthFlag = user.TwoFactorEnabled;
					UserReturnModel userSession = this.TheModelFactory.Create(user);
					output.UserSession = new UserReturnModel();
					output.UserSession = userSession;
				}
			}

			return Ok(output);
		}

		// API user session method
		[Authorize]
		[HttpPost]
		[Route("GetUserSessionByAccessToken")]
		[ResponseType(typeof(GetAccessTokenByUserNameOutput))]
		public async Task<IHttpActionResult> GetUserSessionByAccessToken(GetUserSessionByAccessTokenInput input)
		{
			GetAccessTokenByUserNameOutput output = new GetAccessTokenByUserNameOutput();
			var user = await this.AppUserManager.FindByNameAsync(input.Username);

			if (user != null)
			{
				output.CurrentUserID = user.Id;
				UserReturnModel userSession = this.TheModelFactory.Create(user);
				output.UserSession = new UserReturnModel();
				output.UserSession = userSession;
			}
			
			return Ok(output);
		}

		#region External Logins
		// GET api/Account/ExternalLogin
		[OverrideAuthentication]
		[HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
		[AllowAnonymous]
		[Route("ExternalLogin", Name = "ExternalLogin")]
		public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
		{
			string redirectUri = string.Empty;

			if (error != null)
			{
				return BadRequest(Uri.EscapeDataString(error));
			}

			if (!User.Identity.IsAuthenticated)
			{
				return new ChallengeResult(provider, this);
			}

			var redirectUriValidationResult = ValidateClientAndRedirectUri(this.Request, ref redirectUri);

			if (!string.IsNullOrWhiteSpace(redirectUriValidationResult))
			{
				return BadRequest(redirectUriValidationResult);
			}

			ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

			if (externalLogin == null)
			{
				return InternalServerError();
			}

			if (externalLogin.LoginProvider != provider)
			{
				Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
				return new ChallengeResult(provider, this);
			}

			IdentityUser user = await _repo.FindAsync(new UserLoginInfo(externalLogin.LoginProvider, externalLogin.ProviderKey));

			bool hasRegistered = user != null;

			redirectUri = string.Format("{0}#external_access_token={1}&provider={2}&haslocalaccount={3}&external_user_name={4}",
											redirectUri,
											externalLogin.ExternalAccessToken,
											externalLogin.LoginProvider,
											hasRegistered.ToString(),
											externalLogin.UserName);

			return Redirect(redirectUri);

		}

		private string ValidateClientAndRedirectUri(HttpRequestMessage request, ref string redirectUriOutput)
		{

			Uri redirectUri;

			var redirectUriString = GetQueryString(Request, "redirect_uri");

			if (string.IsNullOrWhiteSpace(redirectUriString))
			{
				return "redirect_uri is required";
			}

			bool validUri = Uri.TryCreate(redirectUriString, UriKind.Absolute, out redirectUri);

			if (!validUri)
			{
				return "redirect_uri is invalid";
			}

			var clientId = GetQueryString(Request, "client_id");

			if (string.IsNullOrWhiteSpace(clientId))
			{
				return "client_Id is required";
			}

			var client = _repo.FindClient(clientId);

			if (client == null)
			{
				return string.Format("Client_id '{0}' is not registered in the system.", clientId);
			}

			if (!string.Equals(client.AllowedOrigin, redirectUri.GetLeftPart(UriPartial.Authority), StringComparison.OrdinalIgnoreCase))
			{
				return string.Format("The given URL is not allowed by Client_id '{0}' configuration.", clientId);
			}

			redirectUriOutput = redirectUri.AbsoluteUri;

			return string.Empty;

		}

		private string GetQueryString(HttpRequestMessage request, string key)
		{
			var queryStrings = request.GetQueryNameValuePairs();

			if (queryStrings == null) return null;

			var match = queryStrings.FirstOrDefault(keyValue => string.Compare(keyValue.Key, key, true) == 0);

			if (string.IsNullOrEmpty(match.Value)) return null;

			return match.Value;
		}

		private class ExternalLoginData
		{
			public string LoginProvider { get; set; }
			public string ProviderKey { get; set; }
			public string UserName { get; set; }
			public string ExternalAccessToken { get; set; }

			public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
			{
				if (identity == null)
				{
					return null;
				}

				Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

				if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer) || String.IsNullOrEmpty(providerKeyClaim.Value))
				{
					return null;
				}

				if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
				{
					return null;
				}

				return new ExternalLoginData
				{
					LoginProvider = providerKeyClaim.Issuer,
					ProviderKey = providerKeyClaim.Value,
					UserName = identity.FindFirstValue(ClaimTypes.Name),
					ExternalAccessToken = identity.FindFirstValue("ExternalAccessToken"),
				};
			}
		}

		private async Task<ParsedExternalAccessToken> VerifyExternalAccessToken(string provider, string accessToken)
		{
			ParsedExternalAccessToken parsedToken = null;

			var verifyTokenEndPoint = "";

			if (provider == "Facebook")
			{
				//You can get it from here: https://developers.facebook.com/tools/accesstoken/
				//More about debug_tokn here: http://stackoverflow.com/questions/16641083/how-does-one-get-the-app-access-token-for-debug-token-inspection-on-facebook

				var appToken = "3058390497587092|56486ff5e9af123b45a8fadce341eb91";
				verifyTokenEndPoint = string.Format("https://graph.facebook.com/debug_token?input_token={0}&access_token={1}", accessToken, appToken);
			}
			else
			{
				return null;
			}

			var client = new HttpClient();
			var uri = new Uri(verifyTokenEndPoint);
			var response = await client.GetAsync(uri);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();

				dynamic jObj = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(content);

				parsedToken = new ParsedExternalAccessToken();

				if (provider == "Facebook")
				{
					parsedToken.user_id = jObj["data"]["user_id"];
					parsedToken.app_id = jObj["data"]["app_id"];

					if (!string.Equals(Startup.facebookAuthOptions.AppId, parsedToken.app_id, StringComparison.OrdinalIgnoreCase))
					{
						return null;
					}
				}
			}

			return parsedToken;
		}

		private JObject GenerateLocalAccessTokenResponse(string userName)
		{
			try
			{
				var tokenExpiration = TimeSpan.FromDays(1);

				ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);

				identity.AddClaim(new Claim(ClaimTypes.Name, userName));
				identity.AddClaim(new Claim("role", "user"));

				var props = new AuthenticationProperties()
				{
					IssuedUtc = DateTime.UtcNow,
					ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
				};

				var ticket = new AuthenticationTicket(identity, props);

				var accessToken = Startup.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);

				JObject tokenResponse = new JObject(
											new JProperty("userName", userName),
											new JProperty("access_token", accessToken),
											new JProperty("token_type", "bearer"),
											new JProperty("expires_in", tokenExpiration.TotalSeconds.ToString()),
											new JProperty(".issued", ticket.Properties.IssuedUtc.ToString()),
											new JProperty(".expires", ticket.Properties.ExpiresUtc.ToString())
				);

				return tokenResponse;
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		// POST api/Account/RegisterExternal
		[AllowAnonymous]
		[Route("RegisterExternal")]
		public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
		{
            try
            {
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				var verifiedAccessToken = await VerifyExternalAccessToken(model.Provider, model.ExternalAccessToken);
				if (verifiedAccessToken == null)
				{
					return BadRequest("Invalid Provider or External Access Token");
				}

				ApplicationUser user = await _repo.FindAsync(new UserLoginInfo(model.Provider, verifiedAccessToken.user_id));

				bool hasRegistered = user != null;

				if (hasRegistered)
				{
					return BadRequest("External user is already registered");
				}

				user = new ApplicationUser() { UserName = model.UserName, FirstName = "Elie", LastName = "Inplan", JoinDate = DateTime.Now };

				IdentityResult result = await _repo.CreateAsync(user);
				if (!result.Succeeded)
				{
					return GetErrorResult(result);
				}

				var info = new ExternalLoginInfo()
				{
					DefaultUserName = model.UserName,
					Login = new UserLoginInfo(model.Provider, verifiedAccessToken.user_id)
				};

				result = await _repo.AddLoginAsync(user.Id, info.Login);
				if (!result.Succeeded)
				{
					return GetErrorResult(result);
				}

				//generate access token response
				var accessTokenResponse = GenerateLocalAccessTokenResponse(model.UserName);

				return Ok(accessTokenResponse);
			}
			catch(Exception ex)
            {
				throw new Exception(ex.Message);
            }
		}

		[AllowAnonymous]
		[HttpGet]
		[Route("ObtainLocalAccessToken")]
		public async Task<IHttpActionResult> ObtainLocalAccessToken(string provider, string externalAccessToken)
		{

			if (string.IsNullOrWhiteSpace(provider) || string.IsNullOrWhiteSpace(externalAccessToken))
			{
				return BadRequest("Provider or external access token is not sent");
			}

			var verifiedAccessToken = await VerifyExternalAccessToken(provider, externalAccessToken);
			if (verifiedAccessToken == null)
			{
				return BadRequest("Invalid Provider or External Access Token");
			}

			IdentityUser user = await _repo.FindAsync(new UserLoginInfo(provider, verifiedAccessToken.user_id));

			bool hasRegistered = user != null;

			if (!hasRegistered)
			{
				return BadRequest("External user is not registered");
			}

			//generate access token response
			var accessTokenResponse = GenerateLocalAccessTokenResponse(user.UserName);

			return Ok(accessTokenResponse);

		}
		#endregion
	}
}
