using System.Net.Http;
using System.Net;
using System.Net.Http.Formatting;
using System.Web;

using NMMA.Core.Extensions;
//using NMMA.Web.Core.Extensions;

namespace Api.Nmma.Configuration
{
	/// <summary>
	///		 Extensions methods for <see cref="Mode"/>.
	/// </summary>
	public static class ModeExtensions
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		public static bool Allows(this Mode mode, HttpRequest request)
		{
			switch (mode)
			{
				case Mode.LocalOnly:
					return request.IsLocal;
				case Mode.On:
					return true;
				case Mode.RemoteOnly:
					return !request.IsLocal;
				default:
					break;
			}
			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		public static bool Allows(this Mode mode, HttpRequestMessage request)
		{
			switch (mode)
			{
				case Mode.LocalOnly:
					return request.IsLocal();
				case Mode.On:
					return true;
				case Mode.RemoteOnly:
					return !request.IsLocal();
				default:
					break;
			}
			return false;
		}
	}
}