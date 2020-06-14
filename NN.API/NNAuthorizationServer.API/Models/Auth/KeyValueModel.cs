using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNAuthorizationServer.API.Models.Auth
{
    [Serializable]
    public class KeyValueModel
    {
        public long Key { get; set; }
        public string Value { get; set; }
    }
}
