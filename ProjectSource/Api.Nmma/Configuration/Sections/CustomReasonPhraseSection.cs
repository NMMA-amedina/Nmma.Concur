using System.Configuration;

namespace Api.Nmma.Configuration
{
	/// <summary>
	///		Contains the reason phrase section.
	/// </summary>
	public class CustomReasonPhraseSection : ConfigurationSection
	{
		const string _root = "webapi/customreasonphrase";

		#region Properties
		/// <summary>
		///		Gets or sets the mode indicating how the reason phrase is handled.
		/// </summary>
		[ConfigurationProperty("mode", DefaultValue = Mode.Off, IsRequired = false)]
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
		public CustomReasonPhraseSection() : base() { }
		#endregion
	}
}