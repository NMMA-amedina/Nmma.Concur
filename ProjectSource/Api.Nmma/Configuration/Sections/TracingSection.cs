using System.Configuration;

namespace Api.Nmma.Configuration
{
	/// <summary>
	///		Contains the tracing section.
	/// </summary>
	public class TracingSection : ConfigurationSection
	{
		const string _root = "webapi/tracing";

		#region Properties
		/// <summary>
		///		Tracing levels to be taken into account.Tracing levels: Level, Debug, Error, Fatal, Info, Off, Warn.
		/// </summary>
		[ConfigurationProperty("levels", DefaultValue = "",  IsRequired = false)]
		public string Levels
		{
			get { return (string)this["levels"]; }
			set { this["levels"] = value; }
		}

		/// <summary>
		///		Gets or sets the mode indicating how the tracing settings are handled.
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
		public TracingSection() : base() { }
		#endregion
	}
}