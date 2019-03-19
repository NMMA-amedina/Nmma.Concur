using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Nmma.Business.Services.Contracts;

namespace Api.Nmma.Controllers
{
    public class IndividualsController : BaseApiController
    {
     readonly IIndividualService _IndividualService;

        /// <summary>
        ///	Default constructor.
        /// </summary>
        /// <param name="IndividualService"></param>
     public IndividualsController(IIndividualService IndividualService)
        {
            _IndividualService = IndividualService;
        }

     [HttpGet]
     public HttpResponseMessage GetRelatedIndByComp(string OrgCode, string Compid)
     {
         var Individuals = new List<Models.Individual>();
         var result = _IndividualService.GetRelatedIndByComp(OrgCode, Compid);
         AutoMapper.Mapper.Map(result, Individuals);

         var response = Request.CreateResponse(HttpStatusCode.OK, result);
         return response;
     }
     [HttpGet]
     public HttpResponseMessage GetIndByName(string OrgCode, string IndSearch)
     {
         var Individuals = new List<Models.AllIndAccount>();
         var result = _IndividualService.GetIndByName(OrgCode, IndSearch);
         AutoMapper.Mapper.Map(result, Individuals);

         var response = Request.CreateResponse(HttpStatusCode.OK, Individuals);
         return response;
     }

    }
}
