using System.Reflection;
using System.Web.Http;

using Autofac;
using Autofac.Integration.WebApi;
using Autofac.Core;

using Nmma.Business.Services.Contracts;
using Nmma.Repositories.Usi.Companies;
using Nmma.Data.BackOffice.Core;
using Nmma.Data.BackOffice.Contexts;
using Api.Nmma.Filters;
using Api.Nmma.Controllers;
using System.Web.Http.Tracing;
using Nmma.Business.Services;

namespace Api.Nmma
{
	/// <summary>
	///	Configures the IoC/DI container.
	/// </summary>
  public static class IoCConfig
	{
		/// <summary>
		///	Registers the IoC/DI container.
		/// </summary>
		/// <param name="config"></param>
		/// <remarks>IoC/DI container used is Autofac. http://autofac.org</remarks>
		public static void RegisterContainer(HttpConfiguration config)
		{
			
			var builder = new ContainerBuilder();
			builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(t => !t.IsAbstract && typeof(ApiController).IsAssignableFrom(t)).InstancePerMatchingLifetimeScope(AutofacWebApiDependencyResolver.ApiRequestTag);
			builder.RegisterAssemblyTypes(typeof(CompanyService).Assembly).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerApiRequest();
			builder.RegisterAssemblyTypes(typeof(CompanyRepository).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerApiRequest();
			builder.RegisterType<UsiContext>().As<IUnitOfWork>().InstancePerApiRequest();
			//builder.RegisterType<ITraceWriter>().As<Api.Nmma.Tracing.NLog>().InstancePerApiRequest();
			//builder.Register(x => new ExceptionHandlingAttribute()).AsWebApiExceptionFilterFor<BaseApiController>().InstancePerApiRequest();
			config.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());
		}
	}
}