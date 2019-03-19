using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Nmma.Models
{
    public class Individual
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string LegalName { get; set; }
    }
}