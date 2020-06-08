using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.Dacia;
using R5T.Derby.Extensions;
using R5T.Lombardy;
using R5T.Macommania.Default;
using R5T.Richmond;
using R5T.Shrewsbury;
using R5T.Shrewsbury.Default;
using R5T.Shrewsbury.Extensions;


namespace R5T.Derby
{
    public class ApplicationConfigurationConfigurationStartup : ApplicationConfigurationStartupBase
    {
        public ApplicationConfigurationConfigurationStartup(ILogger<ApplicationConfigurationConfigurationStartup> logger)
            : base(logger)
        {
        }

        protected override void ConfigureConfigurationBody(IConfigurationBuilder configurationBuilder, IServiceProvider emptyConfigurationServicesProvider)
        {
            configurationBuilder
                .AddDefaultAppSettingsJsonFile()
                ;
        }

        protected override void ConfigureServicesBody(IServiceCollection services)
        {
            services
                .AddDefaultStringlyTypedPathOperator()
                .AddDefaultExecutableFileDirectoryPathProvider(
                    services.AddDefaultExecutableFilePathProviderAction(),
                    ServiceAction<IStringlyTypedPathOperator>.AlreadyAdded)
                .AddSingleton<IAppSettingsDirectoryPathProvider, ExecutableFileDirectoryAppSettingsDirectoryPathProvider>()
                .AddSingleton<IDefaultAppSettingsJsonFileNameProvider, DefaultAppSettingsJsonFileNameProvider>()
                .AddSingleton<IDefaultAppSettingsJsonFilePathProvider, DefaultAppSettingsJsonFilePathProvider>()
                .AddDirectConfigurationBasedConfigurationNameProvider()
                .AddSingleton<IConfigurationNameSpecificAppSettingsJsonFileNameProvider, ConfigurationNameSpecificAppSettingsJsonFileNameProvider>()
                .AddSingleton<IConfigurationNameSpecificAppSettingsJsonFilePathProvider, ConfigurationNameSpecificAppSettingsJsonFilePathProvider>()
                ;
        }
    }
}
