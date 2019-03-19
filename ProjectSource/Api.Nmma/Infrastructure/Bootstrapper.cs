using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Api.Nmma.Models;
using Domain = Nmma.Domain;

namespace Api.Nmma.Infrastructure
{
    public class Bootstrapper
    {
        public static void InitializeAutoMapperConfigurations()
        {
            #region Map external-models to api-models

            Mapper.CreateMap<Domain.Models.Company, Company>()
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            
            //Mapper.CreateMap<Domain.Models.AllCompAccount, AllCompAccount >()
            //   .ForMember(dest => dest.CompanyAcctCode  , opt => opt.MapFrom(src => src.EV870_ACCT_CODE  ))
            //   .ForMember(dest => dest.CompanyName , opt => opt.MapFrom(src => src.EV870_NAME ))
            //   .ForMember(dest => dest.ADDRESSLine1 , opt => opt.MapFrom(src => src.EV870_ADDRESS_L1))
            //   .ForMember(dest => dest.ADDRESSLine2 , opt => opt.MapFrom(src => src.EV870_ADDRESS_L2 ))
            //   .ForMember(dest => dest.CITY , opt => opt.MapFrom(src => src.EV870_CITY))
            //   .ForMember(dest => dest.STATE , opt => opt.MapFrom(src => src.EV870_STATE ))
            //   .ForMember(dest => dest.POSTALCODE , opt => opt.MapFrom(src => src.EV870_POSTAL_CODE ))
            //   .ForMember(dest => dest.COUNTRY , opt => opt.MapFrom(src => src.EV870_COUNTRY ));

            // not used used company above instead
            Mapper.CreateMap<Domain.Models.Company, AllCompAccount>()
              .ForMember(dest => dest.CompanyAcctCode, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.ADDRESSLine1, opt => opt.MapFrom(src => src.Address.Address1))
              .ForMember(dest => dest.ADDRESSLine2, opt => opt.MapFrom(src => src.Address.Address2 ))
              .ForMember(dest => dest.CITY, opt => opt.MapFrom(src => src.Address.City ))
              .ForMember(dest => dest.STATE, opt => opt.MapFrom(src => src.Address.SubDivisionId ))
              .ForMember(dest => dest.POSTALCODE, opt => opt.MapFrom(src => src.Address.PostalCode ))
              .ForMember(dest => dest.COUNTRY, opt => opt.MapFrom(src => src.Address.CountryId  ));
          
                
                
            Mapper.CreateMap<Domain.Models.AllIndAccount, AllIndAccount>()
              .ForMember(dest => dest.INDACCTCODE, opt => opt.MapFrom(src => src.IND_ACCT_CODE))
              .ForMember(dest => dest.FIRSTNAME , opt => opt.MapFrom(src => src.EV870_FIRST_NAME ))
              .ForMember (dest => dest.LASTNAME , opt => opt.MapFrom(src => src.EV870_LAST_NAME))
              .ForMember(dest => dest.INDNAME, opt => opt.MapFrom(src => src.IND_NAME))
              .ForMember(dest => dest.COMPACCTCODE, opt => opt.MapFrom(src => src.COMP_ACCT_CODE ))
              .ForMember(dest => dest.COMPNAME, opt => opt.MapFrom(src => src.COMP_NAME ))
              .ForMember(dest => dest.ADDRESSLine1, opt => opt.MapFrom(src => src.EV870_ADDRESS_L1))
              .ForMember(dest => dest.ADDRESSLine2, opt => opt.MapFrom(src => src.EV870_ADDRESS_L2))
              .ForMember(dest => dest.CITY, opt => opt.MapFrom(src => src.EV870_CITY))
              .ForMember(dest => dest.STATE, opt => opt.MapFrom(src => src.EV870_STATE))
              .ForMember(dest => dest.POSTALCODE, opt => opt.MapFrom(src => src.EV870_POSTAL_CODE))
              .ForMember(dest => dest.COUNTRY, opt => opt.MapFrom(src => src.EV870_COUNTRY));

		    Mapper.CreateMap<Domain.Models.Shows.Show, Show>()
				.ForMember(dest => dest.ShowId, opt => opt.MapFrom(src => src.ShowId))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ActiveFlag, opt => opt.MapFrom(src => src.ActiveFlag));

            Mapper.CreateMap<Domain.Models.Shows.Edition, ShowEdition>()
                .ForMember(dest => dest.ShowEditionId, opt => opt.MapFrom(src => src.EditionId))
                .ForMember(dest => dest.ShowEditionCode, opt => opt.MapFrom(src => src.EditionCode))
                .ForMember(dest => dest.ShowId, opt => opt.MapFrom(src => src.ShowId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate));

			Mapper.CreateMap<Domain.Models.Individual, User>()
				.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
				.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
				.ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

            Mapper.CreateMap<Domain.Models.Shows.ExhibitorBadge, ExhibitorContact>()
                .ForMember(dest => dest.IndividualAcctCode, opt => opt.MapFrom(src => src.ind_code))
                .ForMember(dest => dest.IndividualName, opt => opt.MapFrom(src => src.ind_name))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.First_Name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Last_Name));

            Mapper.CreateMap<Domain.Models.Shows.ExhibitorBadge, ShowCompanies >()
                .ForMember(dest => dest.CompanyAcctCode , opt => opt.MapFrom(src => src.EV870_ACCT_CODE ))
                .ForMember(dest => dest.CompanyName , opt => opt.MapFrom(src => src.EV870_NAME ));
                       

            Mapper.CreateMap<Domain.Models.Shows.ExhibitorBadge, ExhibitorBadge >();

            Mapper.CreateMap<Domain.Models.Shows.OpShow , OpShow>();

            Mapper.CreateMap<Domain.Models.Shows.TradeBadge, TradeBadge>();

            Mapper.CreateMap<Domain.Models.Individual , Individual>()
               .ForMember(dest => dest.Email , opt => opt.MapFrom(src => src.Email ))
               .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName ))
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName ))
               .ForMember(dest => dest.LegalName, opt => opt.MapFrom(src => src.LegalName ));

            Mapper.CreateMap<UngerboeckSDKPackage20_7_3.AllAccountsModel ,Company >()
                .ForMember(dest => dest.CompanyId , opt => opt.MapFrom(src => src.AccountCode))
               .ForMember(dest => dest.Name , opt => opt.MapFrom(src => src.Name ));
            #endregion

            #region Map api-models to external-models

            #endregion
        }
    }

    public class DomainModelResolvers
    {
        public class AddressResolver : ValueResolver<Domain.Models.Company  , Address>
        {
            protected override Address ResolveCore(Domain.Models.Company source)
            {
                Address result = null;

                if (source != null)
                {
                    result = new Address
                    {
                        Address1 = source.Address.Address1 ,
                        Address2 = source.Address.Address2 ,
                        City = source.Address.City,
                        State = source.Address.SubDivisionId ,
                        PostalCode = source.Address.PostalCode ,
                        Country  = source.Address.Country.Name ,
                    };
                }

                return result;
            }
        }
    }
}