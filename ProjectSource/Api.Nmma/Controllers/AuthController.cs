using Nmma.Business.Services.Contracts;
using Nmma.Business.Services.Enums;
using Nmma.Business.Services.Messages;
using Nmma.Domain.Models;
using Nmma.Domain.Models.Security;
using Nmma.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Api.Nmma.Controllers
{
    public class AuthController : BaseApiController
    {
        readonly IAuthentication _authenticationService;
        readonly IIndividualService _individualService;

        public AuthController(IAuthentication authenticationService, IIndividualService individualService)
        {
            _authenticationService = authenticationService;
            _individualService = individualService;
        }


        [HttpGet]
        public HttpResponseMessage Authenticate()
        {
            var ticket = new System.Web.Security.FormsAuthenticationTicket(1, "test-user-data", DateTime.Now, DateTime.Now.AddDays(14), true, "test-user-data");
            string encryptedTicket = System.Web.Security.FormsAuthentication.Encrypt(ticket);
            //var cookie = new HttpCookie(COOKIE_NAME, encryptedTicket);
            //HttpContext.Current.Response.Cookies.Add(cookie);

            var cookie = new System.Net.Http.Headers.CookieHeaderValue("test-user-data", "123");
            cookie.Expires = DateTimeOffset.Now.AddDays(2);
            cookie.Domain = Request.RequestUri.Host;
            cookie.Path = "/";
            

            var response = Request.CreateResponse(HttpStatusCode.OK, "user created");
            response.Headers.AddCookies(new System.Net.Http.Headers.CookieHeaderValue[] { cookie });
            return response;
        }

        //TODO - dev - inject AOP to capture all errors
        [HttpGet]
        public HttpResponseMessage IsUserAuthenticated()
        {
            string authName = "";//SecurityService.GetAuthenticatedName();

            var response = Request.CreateResponse(HttpStatusCode.OK, !string.IsNullOrWhiteSpace(authName));

            return response;
        }

        public static string COOKIE_NAME = "NmmaCookie";
        [HttpGet]
        public HttpResponseMessage IsUserAuthenticated2()
        {
            var response = Request.CreateResponse(HttpStatusCode.NotFound, false);

            System.Net.Http.Headers.CookieHeaderValue cookie = this.ControllerContext.Request.Headers.GetCookies(COOKIE_NAME).FirstOrDefault();
            if (cookie != null)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, !string.IsNullOrEmpty(cookie[COOKIE_NAME].Value));
            }

            return response;
        }

        [HttpGet]
        public HttpResponseMessage IsUserAuthenticated3()
        {
            var response = Request.CreateResponse(HttpStatusCode.OK, User.Identity.IsAuthenticated);


            return response;            
        }
    }
}