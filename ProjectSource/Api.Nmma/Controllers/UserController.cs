using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
//using System.Web.Mvc;
using Api.Nmma.Configuration;
using Nmma.Business.Services.Contracts;
using Nmma.Business.Services.Enums;
using Nmma.Business.Services.Messages;
using Nmma.Domain.Models;
using Nmma.Domain.Models.Security;
using System.Linq;
using System.Linq.Expressions;
using Nmma.Business.Services;
using Nmma.Web.Core;

namespace Api.Nmma.Controllers
{
    /// <summary>
    ///		User controller
    /// </summary>
    public class UserController : BaseApiController
    {
        readonly IAuthentication _authenticationService;
        readonly ICompanyService _companyService;
        readonly IIndividualService _individualService;
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="authenticationService"></param>
        /// <param name="companyService"></param>
        /// <param name="individualService"></param>
        public UserController(IAuthentication authenticationService, ICompanyService companyService, IIndividualService individualService)
        {
            _authenticationService = authenticationService;
            _companyService = companyService;
            _individualService = individualService;
        }

        /// <summary>
        /// Returns a system user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A user instance.</returns>
        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);
            Individual individual = _individualService.Get(id ?? String.Empty);
            if (individual != null)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, AutoMapper.Mapper.Map(individual, new Models.User()));
            }
            else
            {
                if (GlobalWebApiConfiguration.Configuration.CustomReasonPhrase.Allows(Request))
                    response.ReasonPhrase = "User not found";
            }
            return response;
        }

        /// <summary>
        /// Authenticates a system user.
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns>A user instance.</returns>
        [HttpGet]
        public HttpResponseMessage Auth2(string email, string password)
        {
            //deprecated functionality
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);
            AuthenticationResult authenticationResult = _authenticationService.AuthenticateCustomer(new Credential() { Username = email ?? String.Empty, Password = password ?? String.Empty });
            if (authenticationResult.ErrorCode == AuthenticationErrorCode.None)
            {
                Individual individual = _individualService.Get(authenticationResult.UserAccount.Id ?? String.Empty);
                if (individual != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, AutoMapper.Mapper.Map(individual, new Models.User()));
                    response.Headers.Location = new Uri(new Uri(Request.RequestUri.AbsoluteUri), String.Concat("user/", authenticationResult.UserAccount.Id));
                }
                else
                {
                    if (GlobalWebApiConfiguration.Configuration.CustomReasonPhrase.Allows(Request))
                        response.ReasonPhrase = "User not found";
                }
            }
            else
            {
                if (GlobalWebApiConfiguration.Configuration.CustomReasonPhrase.Allows(Request))
                    response.ReasonPhrase = String.Concat(authenticationResult.ErrorCode.ToString(), " - ", authenticationResult.ErrorMessage);
            }
            return response;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Auth(string email, string password)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);
            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
            {
                var authResult = new AuthenticationResult();
                var authService = new AuthenticationService();
                authResult = await authService.AuthenticateCustomerAsync(new Credential() { Username = email, Password = password });
                if (authResult.ErrorCode == AuthenticationErrorCode.None)
                {
                    Individual individual = _individualService.Get(authResult.UserAccount.Id ?? String.Empty);
                    if (individual != null)
                    {
                        response = Request.CreateResponse(HttpStatusCode.OK, AutoMapper.Mapper.Map(individual, new Models.User()));
                        response.Headers.Location = new Uri(new Uri(Request.RequestUri.AbsoluteUri), String.Concat("user/", authResult.UserAccount.Id));
                    }
                    else
                    {
                        if (GlobalWebApiConfiguration.Configuration.CustomReasonPhrase.Allows(Request))
                            response.ReasonPhrase = "User not found";
                    }
                }
                else
                {
                    if (GlobalWebApiConfiguration.Configuration.CustomReasonPhrase.Allows(Request))
                        response.ReasonPhrase = String.Concat(authResult.ErrorCode.ToString(), " - ", authResult.ErrorMessage);
                }
            }
            return response;
        }

        //[HttpPost]
        //[RequireHttps]
        ////public ActionResult SignIn(LoginViewModel model, string returnUrl)
        //public async Task<ActionResult> SignIn(LoginViewModel model, string returnUrl)
        //{
        //    //http://blog.stephencleary.com/2012/07/dont-block-on-async-code.html
        //    if (ModelState.IsValid)
        //    {
        //        if (!string.IsNullOrWhiteSpace(model.Email) && !string.IsNullOrWhiteSpace(model.Password))
        //        {
        //            var authResult = new AuthenticationResult();
        //            var authService = new AuthenticationService();
        //            authResult = await authService.AuthenticateCustomerAsync(new Credential() { Username = model.Email, Password = model.Password });
        //            if (authResult.ErrorCode == AuthenticationErrorCode.None)
        //            {
        //                SecurityService.Authorize(authResult.UserAccount.Id, model.RememberMe);
        //                return RedirectToLocal(returnUrl);
        //            }
        //        }
        //    }

        //    TempData.Add("InvalidLoginAttempt", "Invalid login attempt.");
        //    //TODO - lpw - potential to merge this with SSO post
        //    return RedirectToAction("signin", "account", new { returnUrl = returnUrl });
        //}

        //[HttpGet("user/{id:int}/companies")]
        //public HttpResponseMessage Companies(string id)
        //{
        //	return Request.CreateResponse(HttpStatusCode.OK, AutoMapper.Mapper.Map(_companyService.GetEmployersFor(id), new List<Models.Company>()));
        //}       

        /// <summary>
        /// API request to return user password by email.
        /// </summary>
        /// <param name="email">Email address to retrieve password for.</param>
        /// <returns>Unencrypted password.</returns>
        [HttpGet]
        public HttpResponseMessage GetPassword(string email)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);
            
            var individual = new Individual{ Email = email };
            var pwd = _individualService.GetPassword(individual);
            if (!string.IsNullOrWhiteSpace(pwd))
            {
                //var result = new MailerService().SendRecoverPasswordMessage(new Individual() { Email = email }, pwd);
                var result = new MailerService().SendResetPasswordMessage(email);
                if (result.Success)
                    response = Request.CreateResponse(HttpStatusCode.OK, "Email Has been sent");
                else
                    response = Request.CreateResponse(HttpStatusCode.InternalServerError , result.Messages.First());
             }
            else
                response.ReasonPhrase = "User email not found";
           
            return response;
           
        }
    }
}