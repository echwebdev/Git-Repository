using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNAuthorizationServer.API.Models.Auth
{
    [Serializable]
    public class UserSessionModel
    {
        public UserSessionModel()
        {
            this.ImpersonationList = new List<KeyValueModel>();
        }
        public long SocialMediaAccountID { get; set; }
        public long UserID { get; set; }
        public string PhotoURL { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string DBName { get; set; }
        public List<KeyValueModel> ImpersonationList { get; set; }
        public long LanguageID { get; set; }
        public string TimeZoneID { get; set; }

    }
}
