using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http.Tracing;

namespace NMMA.Api.Configuration
{
	/// <summary>
	/// 
	/// </summary>
	public class Tracing
	{
		/// <summary>
		/// 
		/// </summary>
		public IEnumerable<TraceLevel> Levels { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Mode Mode { get; set; }
	}

	/// <summary>
	/// 
	/// </summary>
	public class ClientAuthentication
	{
		/// <summary>
		/// 
		/// </summary>
		public bool CheckDomain { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool CheckIP { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Mode Mode { get; set; }
	}

	/// <summary>
	/// 
	/// </summary>
	public class WebApiConfiguration : IDisposable
	{
		bool _disposed;

		/// <summary>
		/// 
		/// </summary>
		public ClientAuthentication ClientAuthentication { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public Mode CustomReasonPhrase { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public Tracing Tracing { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public WebApiConfiguration()
		{
			ClientAuthenticationSection clientAuthenticationConfiguration = (ClientAuthenticationSection)ConfigurationManager.GetSection(ClientAuthenticationSection.Root) ?? new ClientAuthenticationSection();
			ClientAuthentication = new ClientAuthentication() { CheckDomain = clientAuthenticationConfiguration.CheckDomain, CheckIP = clientAuthenticationConfiguration.CheckIP, Mode = clientAuthenticationConfiguration.Mode };
			CustomReasonPhraseSection customReasonPhraseConfiguration = (CustomReasonPhraseSection)ConfigurationManager.GetSection(CustomReasonPhraseSection.Root) ?? new CustomReasonPhraseSection();
			CustomReasonPhrase = customReasonPhraseConfiguration.Mode;
			TracingSection tracinConfiguration = (TracingSection)ConfigurationManager.GetSection(TracingSection.Root) ?? new TracingSection();
			Tracing = new Tracing() { Levels = Enum.GetNames(typeof(TraceLevel)).Where(x => (tracinConfiguration.Levels ?? String.Empty).Split(',').Select(y => y.Trim()).Any(y => y.Equals(x, StringComparison.InvariantCultureIgnoreCase))).Distinct().Select(x => (TraceLevel)Enum.Parse(typeof(TraceLevel), x)), Mode = tracinConfiguration.Mode };
		}

		/// <summary>
		/// 
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="disposing"></param>
		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				_disposed = true;
				if (disposing)
				{
				}
			}
		}
	}
}