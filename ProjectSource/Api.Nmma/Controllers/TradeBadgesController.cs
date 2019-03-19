using Nmma.Business.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.Nmma.Configuration;
using Api.Nmma.Models;
using Domain = Nmma.Domain;

namespace Api.Nmma.Controllers
{
    public class TradeBadgesController : BaseApiController 
    {
        //
        // GET: /TradeBadges/

       readonly IShowService _showService;

        /// <summary>
        ///	Default constructor
        /// </summary>
        /// <param name="showService"></param>
       public TradeBadgesController(IShowService showService)
        {
            _showService = showService;
        }

       [HttpGet]
       public HttpResponseMessage TradeBadgeInfo(string IndId, string CompId)
       {
           HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);

           var TradeBadges = new List<Models.TradeBadge >();
           var TradeBadgesResult = _showService.GetTradeBadges(IndId, CompId).ToList();
           if (TradeBadgesResult != null && TradeBadgesResult.Count > 0)
           {
               //TODO - lpw - use automapper

               AutoMapper.Mapper.Map(TradeBadgesResult,TradeBadges);
               response = Request.CreateResponse(HttpStatusCode.OK, TradeBadges);
           }
           else
           {
               if (GlobalWebApiConfiguration.Configuration.CustomReasonPhrase.Allows(Request))
                   response.ReasonPhrase = String.Format("Company and Submitter combination is not valid}");
           }

           return response;
       }
    }
}
