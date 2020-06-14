using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Core
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
