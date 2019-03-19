using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Nmma.Models
{
    public class AllCompAccount
    {
        
        public string CompanyAcctCode { get; set; }
        public string CompanyName { get; set; }
        public string ADDRESSLine1 { get; set; }
        public string ADDRESSLine2 { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string POSTALCODE { get; set; }
        public string COUNTRY { get; set; }

    }
}