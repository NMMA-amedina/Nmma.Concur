using System;

namespace Api.Nmma.Configuration
{
	/// <summary>
	/// 
	/// </summary>
	public static class GlobalWebApiConfiguration
	{
		//private static Lazy<WebApiConfiguration> _configuration = CreateConfiguration();
		static WebApiConfiguration _configuration = CreateConfiguration();

		/// <summary>
		/// 
		/// </summary>
		public static WebApiConfiguration Configuration
		{
			//get { return _configuration.Value; }
			get { return _configuration; }
		}

		//static Lazy<WebApiConfiguration> CreateConfiguration()
		//{
		//	return new Lazy<WebApiConfiguration>(() =>
		//	{
		//		WebApiConfiguration config = new WebApiConfiguration();
		//		return config;
		//	});
		//}

		static WebApiConfiguration CreateConfiguration()
		{
			WebApiConfiguration config = new WebApiConfiguration();
			return config;
		}
	}
}