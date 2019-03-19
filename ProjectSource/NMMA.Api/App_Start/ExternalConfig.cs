using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nmma.Business;

namespace NMMA.Api
{
    public class ExternalConfig
    {
        public static void RegisterAutoMapperConfig()
        {
            Bootstrapper.InitializeNestedConfigurations();
			NMMA.Api.Infrastructure.Bootstrapper.InitializeAutoMapperConfigurations();
        }
    }
}