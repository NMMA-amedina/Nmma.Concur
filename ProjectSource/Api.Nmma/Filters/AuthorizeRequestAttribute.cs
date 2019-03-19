using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Api.Nmma.Filters
{
    public class AuthorizeRequestAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext context)
        {
            string token = HttpUtility.ParseQueryString(context.Request.RequestUri.Query).Get("token");
            if (!string.IsNullOrWhiteSpace(token))
            {
                if (token != "ABC12345")
                {
                    var response = new HttpResponseMessage
                    {
                        Content =
                            new StringContent("This token is not valid, please refresh token or obtain valid token!"),
                        StatusCode = HttpStatusCode.Unauthorized
                    };
                    throw new HttpResponseException(response);
                }
            }
            else
            {
                var response = new HttpResponseMessage 
                { 
                    Content = new StringContent("You must supply valid token to access method!"), 
                    StatusCode = HttpStatusCode.Unauthorized
                };
                throw new HttpResponseException(response);
            }

            base.OnActionExecuting(context);
        }
    }
}