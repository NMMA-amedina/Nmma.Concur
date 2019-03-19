using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Nmma.Models
{
    public class TradeBadge
    {
        public string IndividualAcctCode { get; set; }
        public string IndividualName { get; set; }
        public string CompanyAcctCode { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email_address { get; set; }
        public string ADDRESSLine1 { get; set; }
        public string ADDRESSLine2 { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string POSTALCODE { get; set; }
        public string COUNTRY { get; set; }
        public string PHONE { get; set; }
        public string FAX { get; set; }
        public string WEBADDRESS { get; set; }
        public string MBRStatus { get; set; }
        public string CreditStatusCode { get; set; }
        public string CreditStatus { get; set; }
    }
}