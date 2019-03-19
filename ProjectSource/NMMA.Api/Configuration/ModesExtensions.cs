using System.Net.Http;
using System.Web;

using NMMA.Web.Core.Extensions;

namespace NMMA.Api.Configuration
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