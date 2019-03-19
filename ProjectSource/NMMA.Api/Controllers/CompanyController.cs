using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Nmma.Business.Services.Contracts;

namespace NMMA.Api.Controllers
{
    /// <summary>
    ///   Company Controller
    /// </summary>
    public class CompanyController : BaseApiController
    {
        readonly ICompanyService _companyService;

        /// <summary>
        ///	Default constructor.
        /// </summary>
        /// <param name="companyService"></param>
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        /// <summary>
        /// Returns company by company ID.
        /// </summary>
        /// <param name="id">Company ID.</param>
        /// <returns>HTTP response containing company instance.</returns>
        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            var company = new Models.Company();
            var result = _companyService.Get(id);
            AutoMapper.Mapper.Map(result, company);

            var response = Request.CreateResponse(HttpStatusCode.OK, company);
            return response;
        }

        /// <summary>
        ///	Returns list of companies an individual is associated with.
        /// </summary>
        /// <param name="id">Individual ID.</param>
        /// <returns>HTTP response containing a list of companies.</returns>
        [HttpGet]
        public HttpResponseMessage ByUser(string id)
        {
            var companies = new List<Models.Company>();
            var result = _companyService.GetEmployersFor(id);
            AutoMapper.Mapper.Map(result, companies);
            
            var response = Request.CreateResponse(HttpStatusCode.OK, companies);
            return response;
        }
    }
}