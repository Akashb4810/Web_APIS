using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_APIS.Models
{
    public class Test
    {
        public int TestID { get; set; }
        public string TestName { get; set; }
        public string ShortCode { get; set; }
        public int? SampleTypeID { get; set; }
        public int? DepartmentTypeID { get; set; }
        public int? ReportFormatTypeid { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedByUserID { get; set; }
        public int? ModifiedByUserID { get; set; }
        public bool? Deleted { get; set; }
        public bool? CategoryWisePrint { get; set; }
        public bool? IsNABL { get; set; }
        public int? CenterID { get; set; }
    }
}
