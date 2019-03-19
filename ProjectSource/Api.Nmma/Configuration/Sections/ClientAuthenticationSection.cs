using System.Configuration;

namespace Api.Nmma.Configuration
{
	/// <summary>
	///		Contains the client authentication section.
	/// </summary>
	public class ClientAuthenticationSection : ConfigurationSection
	{
		const string _root = "webapi/clientAuthentication";

		#region Properties
		/// <summary>
		///		Gets or sets whether the client doamin should be verified.
		/// </summary>
		[ConfigurationProperty("checkDomain", DefaultValue = false, IsRequired = false)]
		public bool CheckDomain
		{
			get { return (bool)this["checkDomain"]; }
			set { this["checkDomain"] = value; }
		}

		/// <summary>
		///		Gets or sets whether the client IP should be verified.
		/// </summary>
		[ConfigurationProperty("checkIP", DefaultValue = false, IsRequired = false)]
		public bool CheckIP
		{
			get { return (bool)this["checkIP"]; }
			set { this["checkIP"] = value; }
		}

		/// <summary>
		///		Gets or sets the mode indicating how the secure web page settings handled.
		/// </summary>
		[ConfigurationProperty("mode", DefaultValue = Mode.On, IsRequired = false)]
		public Mode Mode
		{
			get { return (Mode)this["mode"]; }
			set { this["mode"] = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public static string Root
		{
			get { return _root; }
		}
		#endregion

		#region Constructor
		/// <summary>
		///		Default constructor.
		/// </summary>
		public ClientAuthenticationSection() : base() { }
		#endregion
	}
}