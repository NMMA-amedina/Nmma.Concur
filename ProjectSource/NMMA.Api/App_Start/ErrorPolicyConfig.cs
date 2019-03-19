using System;
using System.Configuration;
using System.Web.Configuration;
using System.Web.Http;

namespace NMMA.Api
{
	/// <summary>
	/// 
	/// </summary>
	public class ErrorPolicyConfig
	{
		/// <summary>
		///		Configures whether error details, such as exception messages and stack traces, should be included in error messages.
		///		This configuration uses the customErrors element from the web.config.
		/// </summary>
		/// <param name="config"></param>
		/// <remarks>
		///		IncludeErrorDetailPolicy:
		///			Default: Use the default behavior for the host environment. For ASP.NET hosting, use the value from the customErrors element in the Web.config file. For self-hosting, use the value LocalOnly.
		///			LocalOnly: Only include error details when responding to a local request.
		///			Always: Always include error details.
		///			Never:	Never include error details. 
		///			
		///		customErrors mode:
		///			On: Specifies that custom errors are enabled. If no defaultRedirect attribute is specified, users see a generic error. The custom errors are shown to the remote clients and to the local host.
		///			Off: Specifies that custom errors are disabled. The detailed ASP.NET errors are shown to the remote clients and to the local host.
		///			RemoteOnly: Specifies that custom errors are shown only to the remote clients, and that ASP.NET errors are shown to the local host. This is the default value.
		/// </remarks>
		public static void RegisterPolicy(HttpConfiguration config)
		{
			IncludeErrorDetailPolicy errorDetailPolicy = IncludeErrorDetailPolicy.Never;
			try
			{
				switch (((CustomErrorsSection)ConfigurationManager.GetSection("system.web/customErrors")).Mode)
				{
					case CustomErrorsMode.Off:
						errorDetailPolicy = IncludeErrorDetailPolicy.Always;
						break;
					case CustomErrorsMode.On:
						errorDetailPolicy = IncludeErrorDetailPolicy.Never;
						break;
					case CustomErrorsMode.RemoteOnly:
						errorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;
						break;
					default:
						break;
				}
			}
			catch (Exception)
			{
			}
			GlobalConfiguration.Configuration.IncludeErrorDetailPolicy = errorDetailPolicy; 
		}
	}
}