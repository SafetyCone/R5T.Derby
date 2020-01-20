using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.Richmond;
using R5T.Leeds;
using R5T.Lombardy;
using R5T.Macommania.Default;
using R5T.Shrewsbury;
using R5T.Shrewsbury.Default;
using R5T.Shrewsbury.Extensions;

using R5T.Derby.Extensions;


namespace R5T.Derby
{
    public class ApplicationConfigurationStartup : ApplicationConfigurationStartupBase
    {
        public ApplicationConfigurationStartup(ILogger<ApplicationConfigurationStartup> logger)
            : base(logger)
        {
        }

        /// <summary>
        /// Adds default and configuration name-specific appsettings.json files.
        /// </summary>
        protected override void ConfigureConfigurationBody(IConfigurationBuilder configurationBuilder, IServiceProvider configurationServiceProvider)
        {
            configurationBuilder
                .AddDefaultAppSettingsJsonFile(configurationServiceProvider)
                .AddConfigurationSpecificAppSettingsJsonFile(configurationServiceProvider, true) // Make the configuration-name-specific appsettings file optional since all configuration might just be in the default appsettings file.
                ;
        }

        /// <summary>
        /// Adds:
        /// * Machine location-aware custom user secret files location.
        /// * Configuration-based configuration-name provider.
        /// </summary>
        protected override void ConfigureServicesBody(IServiceCollection services)
        {
            services
                .AddSingleton<IStringlyTypedPathOperator, StringlyTypedPathOperator>()

                .UseDefaultExecutableFileDirectoryPathProvider<StringlyTypedPathOperator>()
                .AddSingleton<IAppSettingsDirectoryPathProvider, ExecutableFileDirectoryAppSettingsDirectoryPathProvider>()
                .AddSingleton<IDefaultAppSettingsJsonFileNameProvider, DefaultAppSettingsJsonFileNameProvider>()
                .AddDirectConfigurationBasedConfigurationNameProvider()
                .UseMachineLocationAwareCustomSecretsDirectoryPath()
                ;
        }
    }
}
