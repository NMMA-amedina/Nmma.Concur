namespace Api.Nmma.Models
{
    /// <summary>
    ///	Show
    /// </summary>
    public class Show
    {
        /// <summary>
        ///	Show ID.
        /// </summary>
        public string ShowId { get; set; }

        /// <summary>
        ///	Show name.
        /// </summary>
        public string Name { get; set; }

        public int ActiveFlag { get; set; }
    }
}