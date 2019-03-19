using System;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Tracing;
using Autofac.Integration.WebApi;

namespace Api.Nmma.Filters
{
	/// <summary>
	/// 
	/// </summary>
	public class ExceptionHandlingAttribute : ExceptionFilterAttribute // IAutofacExceptionFilter 
	{
		//readonly ITraceWriter _tracer;

		//public ExceptionHandlingAttribute(ITraceWriter tracer)
		//{
		//	_tracer = tracer;
		//}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public override void OnException(HttpActionExecutedContext context)
		{
			try
			{
				ITraceWriter tracer = GlobalConfiguration.Configuration.Services.GetTraceWriter();
				tracer.Fatal(context.Request, context.ActionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName, context.Exception);
			}
			catch (Exception)
			{
			}
		}
	}
}