using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Nmma.Business.Services.Contracts;
using UngerboeckSDKPackage20_7_3;
using System.Configuration;
using System.Threading.Tasks;
using System.Linq;
using Api.Nmma.Configuration;




namespace Api.Nmma.Controllers
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

        [HttpGet]
        public HttpResponseMessage CompIsMember(string id)
        {
            //var companies = new List<Models.Company>();
            var result = _companyService.IsMember(id);
            //AutoMapper.Mapper.Map(result, companies);
            
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        [HttpGet]
        public HttpResponseMessage GetCompByName(string OrgCode, string CompSearch)
        {
            var Companies = new List<Models.AllCompAccount>();
            var result = _companyService.GetCompByName(OrgCode, CompSearch);
            AutoMapper.Mapper.Map(result, Companies);

            var response = Request.CreateResponse(HttpStatusCode.OK, Companies);
            return response;
        }


        // Using USI API --
        // DId not work since tehir search is limited to 200 values/rows return
        // and it does not offer the class as a searchable item.


        //String SDKURI = ConfigurationManager.AppSettings["USISDKAPIURI"];
        //String Domain = ConfigurationManager.AppSettings["NMMAURI"];
        //String USIUser = ConfigurationManager.AppSettings["USIUSER"];
        //String USIPassword = ConfigurationManager.AppSettings["USIPASWD"];
        //String APIPasscode = ConfigurationManager.AppSettings["USIAPIPASSCODE"];

        ////// Get Company BY Name
        //[HttpGet]
        //public HttpResponseMessage GetCompByName(string OrgCode, string strName)
        //{
        //    HttpClient myHttpClient = new HttpClient();
        //    InitUSIAPIController initAPi = new InitUSIAPIController();

        //    var MyAccount = new List<Models.Company>();
           
        //    var SearchAccount = string.Empty;

        //    UngerboeckSDKInitialization myUngerboeckSDKInitialization = new UngerboeckSDKInitialization();
        //    AllAccountsModel.AccountUserFields myUserField = new UngerboeckSDKPackage20_7_3.AllAccountsModel.AccountUserFields();

        //    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);
        //    myUngerboeckSDKInitialization = initAPi.GetAPIToken(myHttpClient, SDKURI, Domain, USIUser, USIPassword, APIPasscode);
        //    if (myUngerboeckSDKInitialization != null)
        //    {
        //        myHttpClient = initAPi.InitializeAPIClient(SDKURI, Domain, USIUser, USIPassword, APIPasscode);
        //        myHttpClient.DefaultRequestHeaders.Add("Token", myUngerboeckSDKInitialization.Token);
        //        SearchAccount = "substringof('" + strName + "', Name) or startswith('" + strName + "', Name) or endswith('" + strName + "', Name)";
        //        IEnumerable<AllAccountsModel> AccountResult = GetAccountSearch(myHttpClient, myUngerboeckSDKInitialization, OrgCode, SearchAccount);
        //        AutoMapper.Mapper.Map(AccountResult.Where(e => e.Class == "O").ToList(), MyAccount );
        //        response = Request.CreateResponse(HttpStatusCode.OK, MyAccount);
            
                
        //    }
            
        //    else
        //    {
        //        if (GlobalWebApiConfiguration.Configuration.CustomReasonPhrase.Allows(Request))
        //            response.StatusCode = HttpStatusCode.ExpectationFailed;
        //            response.ReasonPhrase = String.Format("Could not obtain USI Token, check your credentials");
        //    }

        //    return response;
           
        //}

        //// the following 2 to be used for all accounts searches
        //public IEnumerable<AllAccountsModel> GetAccountSearch(HttpClient argHttpclient, UngerboeckSDKInitialization MyUsiInit, string OrgCode, string pSearch)
        //{
        //    //var Account = new System.Threading.Tasks.Task<UngerboeckSDKPackage20_7_3.AllAccountsModel>();


        //    var Account = AwaitAccountSearch(argHttpclient, MyUsiInit, OrgCode, pSearch);

        //    return Account.Result;
        //}


        //public async Task<IEnumerable<UngerboeckSDKPackage20_7_3.AllAccountsModel>> AwaitAccountSearch(HttpClient MyHttpClient, UngerboeckSDKInitialization MyUsiInit, string OrgCode, string pSearch)
        //{
        //    //var Account = new System.Threading.Tasks.Task<UngerboeckSDKPackage20_7_3.AllAccountsModel>();
        //    //HttpClient oHttpClient = new HttpClient();
        //    HttpResponseMessage response = await MyHttpClient.GetAsync(MyUsiInit.APIBaseURI + "api/Accounts/" + OrgCode + "?search=" + pSearch ).ConfigureAwait(false);


        //    var Account = await response.Content.ReadAsAsync<IEnumerable<UngerboeckSDKPackage20_7_3.AllAccountsModel>>();

        //    return Account;
        //}
    }
}