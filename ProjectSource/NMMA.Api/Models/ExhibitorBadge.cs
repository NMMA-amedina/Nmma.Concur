using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NMMA.Api.Models
{
    public class ExhibitorBadge
    {
        public string Booths { get; set; }
        public Nullable<int> EventId { get; set; }
        public string EventDescription { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAcctCode { get; set; }
        public string IndividualAcctCode { get; set; }
        public string IndividualName { get; set; }
        public string ADDRESSLine1 { get; set; }
        public string ADDRESSLine2 { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string POSTALCODE { get; set; }
        public string COUNTRY { get; set; }
        public string PHONE { get; set; }
        public string FAX { get; set; }
        public string WEBADDRESS { get; set; }
        public string EXHIBITORTYPE { get; set; }
        public Nullable<int> Maxbadges { get; set; }
        public Nullable<decimal> AdditionalBadgeprice { get; set; }
        public Nullable<decimal> TotalArea { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMAIL { get; set; }
        public int PaymentStatuscode { get; set; }
        public string PaymentStatus { get; set; }

    }
}