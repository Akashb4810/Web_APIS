using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_APIS.Models
{
    public class LoginResponse
    {
        public string LABID { get; set; }
        public long? MobileNumber { get; set; }
        public string Emailid { get; set; }
        public string Connection { get; set; }

    }
}
