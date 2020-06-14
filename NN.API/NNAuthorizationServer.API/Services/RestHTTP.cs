using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace NNAuthorizationServer.API.Services
{
    public class RestHTTP
    {
        private string _API_HOST;
        public RestHTTP()
        {
            //_API_HOST = WebConfigurationManager.AppSettings["PORTAL_API"];
            _API_HOST = "https://localhost:44350/";
        }


        public T HttpPost<T>(RestSharp.RestRequest Req) where T : new()
        {
            try
            {
                RestSharp.RestClient client = new RestSharp.RestClient(_API_HOST);
                var response = client.Execute<T>(Req);

                return response.Data;
            }
            catch
            {
                return default(T);
            }
        }
    }
}
