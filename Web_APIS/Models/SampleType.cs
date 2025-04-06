using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_APIS.Models
{
    public class SampleType
    {
        public int SampleId { get; set; }
        public string Sample { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedByUserID { get; set; }
        public int? ModifiedByUserID { get; set; }
    }
}
