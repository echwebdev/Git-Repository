using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNAuthorizationServer.API.Models.Auth
{
    public class BaseResponseModel
    {
        public BaseResponseModel()
        {
            ReturnStatus = new ReturnStatusModel();
        }
        public ReturnStatusModel ReturnStatus { get; set; }
    }
}
