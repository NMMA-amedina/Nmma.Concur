using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Api.Nmma.Configuration;
using Nmma.Business.Services.Contracts;
using Nmma.Domain.Models.Shows;

namespace Api.Nmma.Controllers
{
	/// <summary>
	///		Show controller.
	/// </summary>
	public class ShowsController : BaseApiController
	{
		readonly IShowService _showService;

		/// <summary>
		///		Default constructor
		/// </summary>
		/// <param name="showService"></param>
		public ShowsController(IShowService showService)
		{
			_showService = showService;
		}

		/// <summary>
		///		Returns all NMMA shows.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public HttpResponseMessage Get()
		{
			var showsResult = _showService.GetShows();
			var shows = new List<Models.Show>();
			AutoMapper.Mapper.Map(showsResult, shows);
			return Request.CreateResponse(HttpStatusCode.OK, shows.OrderBy(s => s.Name));
		}

		/// <summary>
		///	Returns a NMMA show.
		/// </summary>
		/// <param name="id">Show ID.</param>
		/// <returns>HTTP response message with single show.</returns>
		[HttpGet]
		public HttpResponseMessage Get(string id)
		{
			HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);
			Show show = _showService.GetShow(id ?? String.Empty);
			if (show != null)
			{
				response = Request.CreateResponse(HttpStatusCode.OK, AutoMapper.Mapper.Map(show, new Models.Show()));
			}
			else
			{
				if (GlobalWebApiConfiguration.Configuration.CustomReasonPhrase.Allows(Request))
					response.ReasonPhrase = String.Format("Show not found. ID {0}", id);
			}
			return response;
		}
        /// <summary>
        /// Get Operational show based on show year inculde location and address
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetOPShows(int ShowYear)
        {
            var showsResult = _showService.GetOpCurrentShowsByYear (ShowYear);
            var Opshows = new List<Models.OpShow >();
            AutoMapper.Mapper.Map(showsResult, Opshows);
            return Request.CreateResponse(HttpStatusCode.OK, Opshows);
        }
	}
}
