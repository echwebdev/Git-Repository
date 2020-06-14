using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NNAuthorizationServer.API.Entities;
using NNAuthorizationServer.API.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NNAuthorizationServer.API.Repository
{
    public class AuthRepository : IDisposable
    {
        private ApplicationDbContext _ctx;
        private UserManager<ApplicationUser> _userManager;

        public AuthRepository()
        {
            _ctx = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));
        }

        public Client FindClient(string clientId)
        {
            var client = _ctx.Clients.Find(clientId);

            return client;
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {
            try { 

                var existingToken = _ctx.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

                if (existingToken != null)
                {
                    var result = await RemoveRefreshToken(existingToken);
                }

                _ctx.RefreshTokens.Add(token);

                return await _ctx.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            if (refreshToken != null)
            {
                _ctx.RefreshTokens.Remove(refreshToken);
                return await _ctx.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _ctx.RefreshTokens.Remove(refreshToken);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return _ctx.RefreshTokens.ToList();
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            try 
            { 
                //IdentityUser user = await _userManager.FindAsync(userName, password);
                ApplicationUser user = await _userManager.FindAsync(userName, password);
                return user;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region External Logins
        public async Task<ApplicationUser> FindAsync(UserLoginInfo loginInfo)
        {
            ApplicationUser user = await _userManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            try
            {
                var result = await _userManager.CreateAsync(user);

                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(userId, login);

            return result;
        }
        #endregion
        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}