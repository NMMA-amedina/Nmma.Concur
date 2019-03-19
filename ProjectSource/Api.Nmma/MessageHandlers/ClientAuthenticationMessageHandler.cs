using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Api.Nmma.Configuration;

using Nmma.Business.Services.Contracts;
using Nmma.Domain.Models.WebApi;
using NMMA.Core;
using NMMA.Core.Extensions;
using NMMA.Web.Core.Extensions;

namespace Api.Nmma.MessageHandlers
{
	/// <summary>
	/// 
	/// </summary>
	public class ClientAuthenticationMessageHandler : DelegatingHandler
	{
		IWebApiClientService _service;

		/// <summary>
		///		Default constructor
		/// </summary>
		/// <param name="service"></param>
		public ClientAuthenticationMessageHandler(IWebApiClientService service)
		{
			_service = service;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			ResultArgs result = ClientChecked(request);
			if (!result.Success)
			{
				HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Forbidden);
				if (result.Messages.Count > 0 && GlobalWebApiConfiguration.Configuration.CustomReasonPhrase.Allows(request))
					response.ReasonPhrase = result.Messages[0];
				var tcs = new TaskCompletionSource<HttpResponseMessage>();
				tcs.SetResult(response);
				return tcs.Task;
			}
			return base.SendAsync(request, cancellationToken);
		}

		/// <summary>
		///		Determines whether or not the client is checked.
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		ResultArgs ClientChecked(HttpRequestMessage request)
		{
			if (GlobalWebApiConfiguration.Configuration.ClientAuthentication.Mode.Allows(request))	
			{
				string token = request.GetQueryString("token");
				if (String.IsNullOrWhiteSpace(token))
					return new ResultArgs(false, "Token required");
				Client client = _service.GetByToken(token);
				if (client == null)
					return new ResultArgs(false, "Invalid client");
				if (!client.Token.Equals(token, StringComparison.InvariantCulture))
					return new ResultArgs(false, "Invalid token");
				if (!ClientDomainChecked(client, request))
					return new ResultArgs(false, "Invalid domain");
				if (!ClientIPChecked(client, request))
					return new ResultArgs(false, "Invalid IP");
			}
			return new ResultArgs();
		}

		/// <summary>
		/// 	Determines whether or not the client URL is valid.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		bool ClientDomainChecked(Client client, HttpRequestMessage request)
		{
			if (GlobalWebApiConfiguration.Configuration.ClientAuthentication.CheckDomain)	
			{
				if (!String.IsNullOrWhiteSpace(client.Domain))
				{
					string domain = request.GetClientHost() ?? String.Empty;
					if (client.Domain.Equals(domain, StringComparison.InvariantCultureIgnoreCase))
					{
						if (!domain.EndsWith(client.Domain, StringComparison.InvariantCultureIgnoreCase))
							return false;
						domain = domain.Remove(domain.Length - client.Domain.Length);
						if (domain[domain.Length - 1] != '.')
							return false;
					}
				}
			}
			return true;
		}

		/// <summary>
		///		Determines whether or not the client IP is valid.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		bool ClientIPChecked(Client client, HttpRequestMessage request)
		{
			if (GlobalWebApiConfiguration.Configuration.ClientAuthentication.CheckIP)	
			{
				IPAddress ip1 = client.IPAddress;
				if (ip1 != null)
				{
					IPAddress ip2 = request.GetClientIpAddress();
					return ip2 != null && ip1.Equals(ip2);
				}
			}
			return true;
		}
	}
}