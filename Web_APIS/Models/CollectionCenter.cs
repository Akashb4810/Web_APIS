using System;namespace Web_APIS.Models
{
    public class CollectionCenter
    {
        public int CenterID { get; set; }
        public string CenterName { get; set; }
        public string Address { get; set; }
        public bool? IsDefaultCenter { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public DateTime? ModifyDateTime { get; set; }
        public Guid? CreationUID { get; set; }
        public Guid? ModifyUID { get; set; }
        public string MobileNumber { get; set; }
        public string Emailid { get; set; }
        public bool? ActiveFlag { get; set; }
        public string CenterShortName { get; set; }
        public int? Labcode { get; set; }
        public int? Interval { get; set; }
        public bool? AutoIncrement { get; set; }
        public int? VisitCodeStart { get; set; }
        public int? VisitCodeLength { get; set; }
        public int? RateMstId { get; set; }
        public bool? VerifyAmtPaidforPrinting { get; set; }
        public string HeaderImage { get; set; }
    }
}
