using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.Ives;
using R5T.Ives.Configuration;


namespace R5T.Derby.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDirectConfigurationBasedConfigurationNameProvider(this IServiceCollection services)
        {
            services
                .AddSingleton<IConfigurationNameProvider, DirectConfigurationBasedConfigurationNameProvider>()
                ;

            return services;
        }
    }
}
