using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Nmma.Models
{
    public class AllIndAccount
    {
        public string INDACCTCODE { get; set; }
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public string INDNAME { get; set; }
        public string COMPACCTCODE { get; set; }
        public string COMPNAME { get; set; }
        public string ADDRESSLine1 { get; set; }
        public string ADDRESSLine2 { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string POSTALCODE { get; set; }
        public string COUNTRY { get; set; }
    }
}