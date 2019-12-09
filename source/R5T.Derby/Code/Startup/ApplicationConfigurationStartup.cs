using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.Richmond;

using R5T.Derby.Extensions;


namespace R5T.Derby
{
    public class ApplicationConfigurationStartup : ApplicationConfigurationStartupBase
    {
        public ApplicationConfigurationStartup(ILogger<ApplicationConfigurationStartup> logger)
            : base(logger)
        {
        }

        protected override void ConfigureConfigurationBody(IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .AddDefaultAppSettingsJsonFile()
                ;
        }

        protected override void ConfigureServicesBody(IServiceCollection services)
        {
            services
                .AddDirectConfigurationBasedConfigurationNameProvider()
                ;
        }
    }
}
