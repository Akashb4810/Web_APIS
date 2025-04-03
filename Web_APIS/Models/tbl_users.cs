using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_APIS.Models
{
    public class tbl_users
    {
        public int? UID { get; set; }
        public Guid LABID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid GlobalUID { get; set; }
        public long? MobileNumber { get; set; }
        public string Emailid { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public bool? ActiveFlag { get; set; }
        public int? CreationUID { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public int? ModifyUID { get; set; }
        public DateTime? ModifyDateTime { get; set; }
    }
}
