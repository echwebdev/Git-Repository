using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace BLL.Services
{
    public class BaseService
    {
        public string PortalAPI { get; set; }
        public string PortalAPISecurityKey { get; set; }

        public BaseService()
        {
            PortalAPI = WebConfigurationManager.AppSettings["AuthServerAPI"];
            PortalAPISecurityKey = WebConfigurationManager.AppSettings["AuthServerAPI_Security_Key"];
        }
    }
}
