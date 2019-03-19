using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using UngerboeckSDKPackage20_7_3;
using System.Web.Http;
namespace Api.Nmma.Controllers
{
    public class InitUSIAPIController : BaseApiController
    {
        //
        // GET: /InitUSIAPI/

         public HttpClient  InitializeAPIClient(String astrSDKURI , string  astrDomain ,string astrUngerboeckUser,string astrUngerboeckPassword ,string astrAPIPasscode)
        {
            var mobjUSISDKClient = new HttpClient();
            mobjUSISDKClient.BaseAddress = new Uri(astrSDKURI);  //Base address of the SDK server 
            mobjUSISDKClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                                                                                       Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(
                                                                                                              String.Format("{0}:{1}", astrUngerboeckUser, astrUngerboeckPassword)))); //myUserID:myPassword converted to a Base64String

            mobjUSISDKClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //  These headers are required for initialization
              mobjUSISDKClient.DefaultRequestHeaders.Add("UngerboeckURI", astrDomain);
              mobjUSISDKClient.DefaultRequestHeaders.Add("APIPasscode", astrAPIPasscode);

            return mobjUSISDKClient;
        }

        
        public UngerboeckSDKInitialization GetAPIToken(HttpClient TokenClient, String astrSDKURI, string astrDomain, string astrUngerboeckUser, string astrUngerboeckPassword, string astrAPIPasscode)
        {
           
            TokenClient = InitializeAPIClient(astrSDKURI, astrDomain, astrUngerboeckUser, astrUngerboeckPassword, astrAPIPasscode);
            var myUngerboeckSDKInitializationTask = AwaitRetrieveAPIToken(TokenClient);

            UngerboeckSDKInitialization myUngerboeckSDKInitialization = myUngerboeckSDKInitializationTask.Result;
         
            return myUngerboeckSDKInitialization;
        }
        
        
        public async  System.Threading.Tasks.Task<UngerboeckSDKInitialization > AwaitRetrieveAPIToken(HttpClient httpMyObj)
        {
            
            HttpResponseMessage  response = await httpMyObj.GetAsync("sdk_initialize").ConfigureAwait(false);
            
            if (response.IsSuccessStatusCode) 
            {
                UngerboeckSDKInitialization  myUngerboeckSDKInitializationTask = await response.Content.ReadAsAsync<UngerboeckSDKInitialization>();
                return myUngerboeckSDKInitializationTask;
            }

            return null ;
        }
        
        
                 
    }

    
}
