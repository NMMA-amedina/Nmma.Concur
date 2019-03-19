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
    /// <summary>
    /// Exhibitors API.
    /// </summary>
    public class ExhibitorsController : BaseApiController
    {
        readonly IShowService _showService;

        /// <summary>
        ///	Default constructor
        /// </summary>
        /// <param name="showService"></param>
        public ExhibitorsController(IShowService showService)
        {
            _showService = showService;
        }

        //[HttpGet]
        //public HttpResponseMessage Get(int id)
        //{
        //    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);           
        //    return response;
        //}

        [HttpGet]
        public HttpResponseMessage ByEdition(int showEditionId)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);

            var exhibitors = new List<Models.Exhibitor>();
            var exhibitorsResult = _showService.GetExhibitors(showEditionId).ToList();
            if (exhibitorsResult != null && exhibitorsResult.Count > 0)
            {
                //TODO - lpw - use automapper
                exhibitors = LocalMap(exhibitorsResult);
                response = Request.CreateResponse(HttpStatusCode.OK, exhibitors);
            }
            else
            {
                if (GlobalWebApiConfiguration.Configuration.CustomReasonPhrase.Allows(Request))
                    response.ReasonPhrase = String.Format("No exhibitors found for edition {0}", showEditionId);
            }

            return response;
        }

        /// <summary>
        /// Returns list of exhibitors for show edition and company.
        /// </summary>
        /// <param name="showEditionId">Show edition ID.</param>
        /// <param name="companyId">Company ID</param>
        /// <returns>HTTP response message containing list of exhibitors.</returns>
        [HttpGet]
        public HttpResponseMessage ByEditionCompany(int showEditionId, string companyId)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);

            var exhibitors = new List<Models.Exhibitor>();
            var exhibitorsResult = _showService.GetExhibitors(showEditionId, companyId).ToList();
            if (exhibitorsResult != null && exhibitorsResult.Count > 0)
            {
                //TODO - lpw - use automapper
                exhibitors = LocalMap(exhibitorsResult);
                response = Request.CreateResponse(HttpStatusCode.OK, exhibitors);
            }
            else
            {
                if (GlobalWebApiConfiguration.Configuration.CustomReasonPhrase.Allows(Request))
                    response.ReasonPhrase = String.Format("No exhibitors found for edition {0} and company {1}", showEditionId, companyId);
            }

            return response;
        }

        /// added by Ihab Mokbel
        /// 7/8/15
        /// To get all required data for exhibitor badges
        /// 
        [HttpGet]
        public HttpResponseMessage BadgesInfo(string IndId, string CompId, int EvtId)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);

            var exhibitorsBadges = new List<Models.ExhibitorBadge>();
            var exhibitorsBadgesResult = _showService.GetExhibitorsBadges(IndId, CompId, EvtId ).ToList();
            if (exhibitorsBadgesResult != null && exhibitorsBadgesResult.Count > 0)
            {
                //TODO - lpw - use automapper
                exhibitorsBadges = LocalMap(exhibitorsBadgesResult);
                response = Request.CreateResponse(HttpStatusCode.OK, exhibitorsBadges);
            }
            else
            {
                if (GlobalWebApiConfiguration.Configuration.CustomReasonPhrase.Allows(Request))
                    response.ReasonPhrase = String.Format("Exhibitor and Subbmitter combination is not valid for this event}");
            }

            return response;
        }

        
       
        ///Get the exhibiting companies
        [HttpGet]
        public HttpResponseMessage ExhibitorEvents(string IndId)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);

            var exhibitingEvts = new List<Models.ExhibitorBadge>();
            var exhibitingEvtResult = _showService.GetExhibitorEvents(IndId).ToList();
            if (exhibitingEvtResult != null && exhibitingEvtResult.Count > 0)
            {
                //TODO - lpw - use automapper
                exhibitingEvts = LocalMap(exhibitingEvtResult);
                //AutoMapper.Mapper.Map(exhibitingEvtResult, exhibitingEvts);
                response = Request.CreateResponse(HttpStatusCode.OK, exhibitingEvts);
            }
            else
            {
                if (GlobalWebApiConfiguration.Configuration.CustomReasonPhrase.Allows(Request))
                    response.ReasonPhrase = String.Format("No Events Available for logged in user");
            }

            return response;
        }

        ///Get the exhibiting companies
        [HttpGet]
        public HttpResponseMessage ExhibitorContacts(string CompId, int EvtId)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);

            var exhContacts = new List<Models.ExhibitorContact >();
            var exhContactsResult = _showService.GetExhibitorContacts(CompId, EvtId ).ToList();
            if (exhContactsResult != null && exhContactsResult.Count > 0)
            {
                //TODO - lpw - use automapper
               // exhContacts = LocalMap(exhContactsResult);
                AutoMapper.Mapper.Map(exhContactsResult, exhContacts);
                //var respExhContact = exhContacts.
                response = Request.CreateResponse(HttpStatusCode.OK, exhContacts);
            }
            else
            {
                if (GlobalWebApiConfiguration.Configuration.CustomReasonPhrase.Allows(Request))
                    response.ReasonPhrase = String.Format("No Contacts Available for logged in user");
            }

            return response;
        }

        ///Get the exhibiting companies
        [HttpGet]
        public HttpResponseMessage ShowCompanies(int EvtId)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);

            var EvtCompanies = new List<Models.ShowCompanies>();
            var EvtCompaniesResult = _showService.GetShowCompanies (EvtId).ToList();
            if (EvtCompaniesResult != null && EvtCompaniesResult.Count > 0)
            {
                //TODO - lpw - use automapper
                
                AutoMapper.Mapper.Map(EvtCompaniesResult, EvtCompanies);
                response = Request.CreateResponse(HttpStatusCode.OK, EvtCompanies);
            }
            else
            {
                if (GlobalWebApiConfiguration.Configuration.CustomReasonPhrase.Allows(Request))
                    response.ReasonPhrase = String.Format("No Exhibiting Compaies Found");
            }

            return response;
        }
        /// End Ihab Mokbel Modification 7/8/15

        /// <summary>
        /// Searches for exhibitors by show edition and company name.
        /// </summary>
        /// <param name="name">Search name key.</param>
        /// <returns>HTTP response message containing list of exhibitors.</returns>
        [HttpGet]
        public HttpResponseMessage SearchByEditionCompany(int showEditionId, string name)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);
            
            var exhibitors = new List<Models.Exhibitor>();
            var exhibitorsResult = _showService.SearchExhibitors(showEditionId, name).ToList();
            if (exhibitorsResult != null && exhibitorsResult.Count > 0)
            {
                //TODO - lpw - use automapper
                exhibitors = LocalMap(exhibitorsResult);
                response = Request.CreateResponse(HttpStatusCode.OK, exhibitors);
            }
            else
            {
                if (GlobalWebApiConfiguration.Configuration.CustomReasonPhrase.Allows(Request))
                    response.ReasonPhrase = String.Format("No exhibitors found for edition {0} and company name {1}", showEditionId, name);
            }
            

            return response;
        }

        List<Models.Exhibitor > LocalMap(List<Domain.Models.Shows.Exhibitor> source)
        {
            var exhibitor = new List<Models.Exhibitor >();

            foreach (var exh in source)
            {
                if (exh.Booths.Count > 0)
                {
                    foreach (var booth in exh.Booths)
                    {
                        exhibitor.Add(new Models.Exhibitor
                        {
                            ExhibitorId = booth.ExhibitorRefId,
                            ExhibitorName = (!string.IsNullOrWhiteSpace(exh.ShowGuideName)) ? exh.ShowGuideName : exh.CompanyName,
                            Company = new Company { Name = exh.CompanyName, CompanyId = booth.CompanyId },
                            ShowEditionId = booth.EditionId,
                            Booth = booth.Booth,
                            Building = booth.Building,
                            ExhibitorType = booth.ExhibitorType
                        });
                    }
                }
                else
                {
                    exhibitor.Add(new Models.Exhibitor
                    {
                        ExhibitorId = exh.ExhibitorRefId.Value,
                        ExhibitorName = (!string.IsNullOrWhiteSpace(exh.ShowGuideName)) ? exh.ShowGuideName : exh.CompanyName,
                        Company = new Company { Name = exh.CompanyName, CompanyId = exh.CompanyId },
                        ShowEditionId = exh.EditionId,
                        Booth = string.Empty,
                        Building = string.Empty,
                        ExhibitorType = string.Empty
                    });
                }
            }

            return exhibitor;
        }
        /// <summary>
        /// Added by Ihab Mokbel
        /// 7/9/15
        /// Add and populate a list for exhibitor badges
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
       
        List<Models.ExhibitorBadge> LocalMap(List<Domain.Models.Shows.ExhibitorBadge> source)
        {
            var exhibitorbadges = new List<Models.ExhibitorBadge >();

            foreach (var exhbdge in source)
            {
                
                        exhibitorbadges.Add(new Models.ExhibitorBadge 
                        {
                            Booths = exhbdge.Booths  ,
                            EventId = exhbdge.EV290_EVT_ID ,
                            EventDescription = exhbdge.EV200_EVT_DESC ,
                            CompanyName = exhbdge.EV870_NAME , 
                            CompanyAcctCode = exhbdge.EV870_ACCT_CODE ,
                            IndividualAcctCode = exhbdge.ind_code ,
                            IndividualName = exhbdge.ind_name ,
                            ADDRESSLine1 = exhbdge.EV870_ADDRESS_L1 ,
                            ADDRESSLine2 = exhbdge.EV870_ADDRESS_L2 ,
                            CITY = exhbdge.EV870_CITY ,
                            STATE = exhbdge.EV870_STATE ,
                            POSTALCODE = exhbdge.EV870_POSTAL_CODE ,
                            COUNTRY = exhbdge.EV870_COUNTRY ,
                            PHONE = exhbdge.EV870_MAIN_PHONE,
                            FAX = exhbdge.EV870_MAIN_FAX,
                            WEBADDRESS = exhbdge.EV870_WEB_ADDRESS,
                            EXHIBITORTYPE = exhbdge.EV290_EXHIB_TYPE,
                            Maxbadges = exhbdge.Max_badges,
                            AdditionalBadgeprice = exhbdge.Add_Badge_price,
                            TotalArea =exhbdge.total_Area, 
                            FirstName=exhbdge.First_Name ,
                            LastName= exhbdge.Last_Name,
                            EMAIL = exhbdge.EV870_EMAIL_ADDRESS ,
                            PaymentStatuscode=exhbdge.Payment_Status_code, 
                            PaymentStatus=exhbdge.Payment_Status 
                        });
               
                }
            

            return exhibitorbadges;
        }

        List<Models.ExhibitorContact> LocalMap(List<Domain.Models.Shows.ExhibitorBadge> source, bool flag)
        {
            var exhContacts = new List<Models.ExhibitorContact>();

            foreach (var exhbdge in source)
            {

                exhContacts.Add(new Models.ExhibitorContact
                        {

                            IndividualAcctCode = exhbdge.ind_code,
                            IndividualName = exhbdge.ind_name,

                            FirstName = exhbdge.First_Name,
                            LastName = exhbdge.Last_Name,

                        });

            }


            return exhContacts;
        }

       
    }
}