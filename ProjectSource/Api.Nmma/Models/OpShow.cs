using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Nmma.Models
{
    /// <summary>
    ///	opShow
    /// </summary>
    public class OpShow
    {
        public string ShowId { get; set; }
        public int EventId { get; set; }
        public string EventCode { get; set; }
        public string EventName { get; set; }
        public Nullable<System.DateTime> EventSTARTDATE { get; set; }
        public Nullable<System.DateTime> EventSTARTTIME { get; set; }
        public Nullable<System.DateTime> EventEndDate { get; set; }
        public Nullable<System.DateTime> EventEndTime { get; set; }
        public string EventLocation { get; set; }
        public string EventAddress1 { get; set; }
        public string EventCity { get; set; }
        public string EventState { get; set; }
        public string EventPostalCode { get; set; }
        public string EventCountryCode { get; set; }
        public string EventCountry { get; set; }
    }
}