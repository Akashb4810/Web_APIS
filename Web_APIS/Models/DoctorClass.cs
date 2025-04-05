
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_APIS.Models
{
    public class DoctorClass
    {
        [Key]
        public int DoctorID { get; set; }

        [Required(ErrorMessage = "Doctor name is required.")]
        [StringLength(100, ErrorMessage = "Doctor name cannot exceed 100 characters.")]
        public string DoctorName { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        [Phone(ErrorMessage = "Invalid mobile number.")]
        [StringLength(15, ErrorMessage = "Mobile number cannot exceed 15 digits.")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string EmailId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Guid? CreatedByUserID { get; set; }

        public Guid? ModifiedByUserID { get; set; }

        public bool? Deleted { get; set; }

        public int? RateListId { get; set; }

        public int? CenterId { get; set; }
    }
}
