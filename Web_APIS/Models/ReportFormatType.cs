using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_APIS.Models
{
    public class ReportFormatType
    {
        public int ReportFtypeID { get; set; }
        public string ReportFtypeName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedByUserID { get; set; }
        public int? ModifiedByUserID { get; set; }
        public bool? Deleted { get; set; }
    }
}
