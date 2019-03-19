using System;

namespace Api.Nmma.Models
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
        /// Show ID
        /// </summary>
        public string ShowId { get; set; }

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