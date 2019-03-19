using System;

namespace NMMA.Api.Models
{
    /// <summary>
    ///	Show edition
    /// </summary>
    public class ShowEdition
    {
        /// <summary>
        ///	Edition ID
        /// </summary>
        public string ShowEditionId { get; set; }

        /// <summary>
        ///	Edition code
        /// </summary>
        public string ShowEditionCode { get; set; }

        /// <summary>
        ///	Edition name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///	Edition start date
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        ///	Edition end date
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}