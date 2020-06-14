using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNAuthorizationServer.API.Models.Auth
{
    [Serializable]
    public class AccessTokenModel
    {
        public AccessTokenModel()
        {
            UserSession = new UserReturnModel();
        }
        public string Token { get; set; }
        public string AccessToken { get; set; }
        public DateTime Issued { get; set; }
        public int OrganizationId { get; set; }
        public string DBName { get; set; }
        public int LanguageID { get; set; }
        public string UserID { get; set; }
        public UserReturnModel UserSession { get; set; }
    }
}
