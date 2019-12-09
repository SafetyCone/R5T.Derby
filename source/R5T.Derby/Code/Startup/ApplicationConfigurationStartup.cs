using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.Richmond;

using R5T.Derby.Extensions;
using R5T.Scotia.Extensions;


namespace R5T.Derby
{
    public class ApplicationConfigurationStartup : ApplicationConfigurationStartupBase
    {
        public ApplicationConfigurationStartup(ILogger<ApplicationConfigurationStartup> logger)
            : base(logger)
        {
        }

        /// <summary>
        /// Adds the default appsettings.json file.
        /// </summary>
        protected override void ConfigureConfigurationBody(IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .AddDefaultAppSettingsJsonFile()
                ;
        }

        /// <summary>
        /// Adds direct configuration-based configuration-name provider and custom user secret files location.
        /// </summary>
        /// <param name="services"></param>
        protected override void ConfigureServicesBody(IServiceCollection services)
        {
            services
                .AddDirectConfigurationBasedConfigurationNameProvider()
                .AddUserSecretFilesRivetLocation()
                ;
        }
    }
}
