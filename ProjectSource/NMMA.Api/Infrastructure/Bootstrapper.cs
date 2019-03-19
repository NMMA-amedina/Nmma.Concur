using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using NMMA.Api.Models;

using Domain = Nmma.Domain;

namespace NMMA.Api.Infrastructure
{
    public class Bootstrapper
    {
        public static void InitializeAutoMapperConfigurations()
        {
            #region Map external-models to api-models

		    Mapper.CreateMap<Domain.Models.Company, Company>()
				.ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

		    Mapper.CreateMap<Domain.Models.Shows.Show, Show>()
				.ForMember(dest => dest.ShowId, opt => opt.MapFrom(src => src.ShowId))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ActiveFlag, opt => opt.MapFrom(src => src.ActiveFlag));

            Mapper.CreateMap<Domain.Models.Shows.Edition, ShowEdition>()
                .ForMember(dest => dest.ShowEditionId, opt => opt.MapFrom(src => src.EditionId))
                .ForMember(dest => dest.ShowEditionCode, opt => opt.MapFrom(src => src.EditionCode))
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

            #endregion

            #region Map api-models to external-models

            #endregion
        }
    }
}