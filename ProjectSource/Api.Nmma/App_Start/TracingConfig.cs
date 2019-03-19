using System.Web.Http;
using System.Web.Http.Tracing;
using Api.Nmma.Configuration;

namespace Api.Nmma
{
	/// <summary>
	/// 
	/// </summary>
	public class TracingConfig
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="config"></param>
		public static void RegisterTracing(HttpConfiguration config)
		{
			if (GlobalWebApiConfiguration.Configuration.Tracing.Mode != Mode.Off)
				GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new Api.Nmma.Tracing.NLog());
		}
	}
}