using System;

namespace NMMA.Api.Models
{
	/// <summary>
	///	User (Individual)
	/// </summary>
	public class User
	{
        /// <summary>
        ///	Individual ID
        /// </summary>
        public string UserId { get; set; }

		/// <summary>
		///	Individual first name
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		///	Individual last name
		/// </summary>
		public string LastName { get; set; }		
	}
}