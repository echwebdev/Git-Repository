using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Common
{
    public class AccessTokenHelper
    {
        public string Serialize(DataModels.Core.AccessTokenModel input)
        {
            WebUtil wutil = new WebUtil();

            return wutil.Base64Encode(JsonConvert.SerializeObject(input));
        }
        public DataModels.Core.AccessTokenModel Deserialize(string input)
        {
            try
            {
                WebUtil wUtil = new WebUtil();
                return JsonConvert.DeserializeObject<DataModels.Core.AccessTokenModel>(wUtil.Base64Decode(input.Replace("-", "")));
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
