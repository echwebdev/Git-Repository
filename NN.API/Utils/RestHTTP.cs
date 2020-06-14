using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Utils
{
    public class RestHTTP
    {
        private string _API_HOST;
        public RestHTTP()
        {
            _API_HOST = WebConfigurationManager.AppSettings["PORTAL_API"];
        }


        public T HttpPost<T>(RestSharp.RestRequest Req) where T : new()
        {
            try
            {
                RestSharp.RestClient client = new RestSharp.RestClient(_API_HOST);
                //Req.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                var response = client.Execute<T>(Req);

                return response.Data;

                //dynamic jsonRes = Newtonsoft.Json.JsonConvert.DeserializeObject(response.Content);
                //var rv = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonRes.result.ToString());
                //return rv;
            }
            catch
            {
                return default(T);
            }
        }
    }
}
