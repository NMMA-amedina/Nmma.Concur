using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nmma.Business;

namespace Api.Nmma
{
    public class ExternalConfig
    {
        public static void RegisterAutoMapperConfig()
        {
            Bootstrapper.InitializeNestedConfigurations();
            Api.Nmma.Infrastructure.Bootstrapper.InitializeAutoMapperConfigurations();
        }
    }
}