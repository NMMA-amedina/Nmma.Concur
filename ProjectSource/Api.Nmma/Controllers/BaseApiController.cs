using System.Configuration;
using System.Web.Http;
using System.Web.Http.Tracing;

using Api.Nmma.Configuration;
using NMMA.Core.Extensions;

namespace Api.Nmma.Controllers
{
    /// <summary>
    ///		Base Web API controller
    /// </summary>
    public class BaseApiController : ApiController
    {
        readonly ITraceWriter _tracer;

        #region Properties
        /// <summary>
        ///		Trace writer
        /// </summary>
        protected ITraceWriter Tracer
        {
            get { return _tracer; }
        }
        #endregion

        #region Constructor
        /// <summary>
        ///		Default constructor
        /// </summary>
        public BaseApiController()
        {
            _tracer = GlobalConfiguration.Configuration.Services.GetTraceWriter();
        }
        #endregion
    }
}