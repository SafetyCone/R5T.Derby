using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.Ives;
using R5T.Ives.Configuration;
using R5T.Shrewsbury;
using R5T.Shrewsbury.Default;


namespace R5T.Derby.Extensions
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds services to get a configuration name-specific app-settings file name.
        /// </summary>
        public static IServiceCollection AddDirectConfigurationBasedConfigurationNameProvider(this IServiceCollection services)
        {
            services
                .AddSingleton<IConfigurationNameProvider, DirectConfigurationBasedConfigurationNameProvider>()
                .AddSingleton<IConfigurationNameToAppSettingsFileNameTokenConverter, ConfigurationNameToAppSettingsFileNameTokenConverter>()
                ;

            return services;
        }
    }
}
