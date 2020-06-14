using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Core
{
    [Serializable]
    public class AccessTokenModel
    {

        public AccessTokenModel()
        {
            UserSession = new UserReturnModel();
        }
        public string unique_name { get; set; }
        public string Token { get; set; }
        public string AccessToken { get; set; }
        public DateTime Issued { get; set; }
        public string DBName { get; set; }
        public int LanguageID { get; set; }
        public int UserID { get; set; }
        public UserReturnModel UserSession { get; set; }
    }
}
