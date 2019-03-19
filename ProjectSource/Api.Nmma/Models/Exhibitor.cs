using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Nmma.Models
{
    public class Exhibitor
    {
        public int ExhibitorId { get; set; }
        public string ExhibitorName { get; set; }
        public Company Company { get; set; }
        public int ShowEditionId { get; set; }
        //public string ShowEditionCode { get; set; }
        public string Booth { get; set; }
        public string Building { get; set; }
        public string ExhibitorType { get; set; }
        //public string Status { get; set; }
    }
}