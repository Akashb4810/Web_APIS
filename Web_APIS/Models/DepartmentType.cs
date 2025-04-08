using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_APIS.Models
{
    public class DepartmentType
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedByUserID { get; set; }
        public int? ModifiedByUserID { get; set; }
        public string FooterText { get; set; }
        public bool? PrintFooterOnReport { get; set; }
        public bool? Deleted { get; set; }
    }
}
