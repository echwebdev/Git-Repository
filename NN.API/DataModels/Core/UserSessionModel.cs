using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Core
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
        public string UserName { get; set; }
        public string PhotoURL { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public long OrgID { get; set; }
        public string DBName { get; set; }
        public List<KeyValueModel> ImpersonationList { get; set; }
        public long LanguageID { get; set; }
        public string TimeZoneID { get; set; }

    }

    public class UserReturnModel
    {
        public UserReturnModel()
        {
            this.Roles = new List<string>();
            this.Claims = new List<System.Security.Claims.Claim>();
        }
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
    }
}
