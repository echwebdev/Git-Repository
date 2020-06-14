using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNAuthorizationServer.API.Models.Auth
{
    public class ReturnStatusModel
    {
        public ReturnStatusModel()
        {
            ReturnCode = 200;
        }

        #region Errors
        public void Access_Denied()
        {
            this.ReturnCode = 500;
            this.ReturnMessage = "Access_Denied";
        }
        public void Invalid_Security_Code()
        {
            this.ReturnCode = 500;
            this.ReturnMessage = "Invalid_Security_Code";
        }
        public void Missing_Required_Field()
        {
            this.ReturnCode = 500;
            this.ReturnMessage = "Missing_Required_Field";
        }
        public void EndDateUlteriorStartDate()
        {
            this.ReturnCode = 500;
            this.ReturnMessage = "EndDate_Ulterior_StartDate";
        }
        public void Internal_Server_Error()
        {
            this.ReturnCode = 500;
            this.ReturnMessage = "Internal_Server_Error";
        }
        public void Unauthorized()
        {
            this.ReturnMessage = "Unauthorized";
            this.ReturnCode = 401;
        }
        public void OK()
        {
            this.ReturnCode = 200;
            this.ReturnMessage = string.Empty;
        }
        #endregion

        public int ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
    }
}
