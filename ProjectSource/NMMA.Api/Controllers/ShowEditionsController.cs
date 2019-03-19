using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NMMA.Api.Configuration;
using NMMA.Api.Models;
using Nmma.Business.Services;
using Nmma.Business.Services.Contracts;
using Nmma.Domain.Models.Shows;

namespace NMMA.Api.Controllers
{
    /// <summary>
    ///		Show editions controller.
    /// </summary>
    public class ShowEditionsController : BaseApiController
    {
        readonly IShowService _showService;

        /// <summary>
        ///	Default constructor
        /// </summary>
        /// <param name="showService"></param>
        public ShowEditionsController(IShowService showService)
        {
            _showService = showService;
        }

        /// <summary>
        /// Returns all current show editions
        /// </summary>
        /// <returns>HTTP response containing list of shows ordered by show start date.</returns>
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var editionsResult = _showService.GetShowEditions();
            var editions = new List<Models.ShowEdition>();
            AutoMapper.Mapper.Map(editionsResult, editions);
            return Request.CreateResponse(HttpStatusCode.OK, editions.OrderBy(s => s.StartDate));
        }

        /// <summary>
        /// Returns show editions by show edition ID.
        /// </summary>
        /// <param name="id">Edition ID</param>
        /// <returns>HTTP response containing single show edition details.</returns>
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);
            Edition edition = _showService.GetShowEdition(id);
            if (edition != null)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, AutoMapper.Mapper.Map(edition, new Models.ShowEdition()));
            }
            else
            {
                if (GlobalWebApiConfiguration.Configuration.CustomReasonPhrase.Allows(Request))
                    response.ReasonPhrase = String.Format("Edition not found. ID {0}", id);
            }
            return response;
        }

        /// <summary>
        ///	Returns a list of current (not closed) editions a company is exhibiting in.
        ///	(lpw - commented out because it was causing a conflict with showeditions/{id}
        /// </summary>
        /// <param name="id">Company ID.</param>
        /// <returns></returns>
        //[HttpGet]
        //public HttpResponseMessage ByCompany(string id)
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK, AutoMapper.Mapper.Map(_showEditionService.GetCurrentShowEditionsByCompany(id), new List<Models.Company>()));
        //}
    }
}