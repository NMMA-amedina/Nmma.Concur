using System;

namespace Api.Nmma.Configuration
{
	/// <summary>
	///		The different modes supported for the Web API configuration sections.
	/// </summary>
	public enum Mode
	{
		/// <summary>
		///		Only local requests are to be monitored.
		/// </summary>
		LocalOnly,

		/// <summary>
		///		No monitoring.
		/// </summary>
		Off,

		/// <summary>
		///		Always monitored.
		/// </summary>
		On,

		/// <summary>
		///		Only remote requests are to be monitored.
		/// </summary>
		RemoteOnly
	}
}