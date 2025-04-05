using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_APIS.Models
{
    public class tbl_users
    {
        public int? UID { get; set; }

        [Required(ErrorMessage = "Lab ID is required")]
        public Guid LABID { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(100, ErrorMessage = "Username must be at most 100 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Password must be at most 100 characters")]
        public string Password { get; set; }

        public Guid GlobalUID { get; set; } = Guid.NewGuid();

        [Range(1000000000, 9999999999, ErrorMessage = "Mobile number must be 10 digits")]
        public long? MobileNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Emailid { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public bool? ActiveFlag { get; set; }

        public int? CreationUID { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public int? ModifyUID { get; set; }

        public DateTime? ModifyDateTime { get; set; }
    }
}
