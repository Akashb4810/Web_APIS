using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_APIS.Models
{
    public class MstRateList
    {
        public int RateListId { get; set; }
        public string RateTypeName { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class RateListRequestDto
    {
        public int RateListId { get; set; }
        public string RateTypeName { get; set; }
    }
}
