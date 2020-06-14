using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace BLL.Common
{
    public class RestHTTP
    {
        private string _API_HOST;
        public RestHTTP()
        {
            _API_HOST = WebConfigurationManager.AppSettings["AuthServerAPI"];
        }

        public T HttpPost<T>(RestSharp.RestRequest Req) where T : new()
        {
            try
            {
                RestSharp.RestClient client = new RestSharp.RestClient(_API_HOST);
                var response = client.Execute<T>(Req);
                return response.Data;
            }
            catch(Exception ex)
            {
                return default(T);
                throw new Exception(ex.Message);
            }
        }

    }
}
