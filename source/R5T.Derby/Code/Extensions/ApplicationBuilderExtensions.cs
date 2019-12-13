using System;

using R5T.Richmond;


namespace R5T.Derby
{
    public static class ApplicationBuilderExtensions
    {
        public static IServiceProvider GetApplicationConfigurationConfigurationServiceProvider(this ApplicationBuilder applicationBuilder)
        {
            var applicationConfigurationConfigurationServiceProvider = applicationBuilder.UseStartup<ApplicationConfigurationConfigurationStartup>();
            return applicationConfigurationConfigurationServiceProvider;
        }

        public static IServiceProvider GetApplicationConfigurationServiceProvider(this ApplicationBuilder applicationBuilder)
        {
            var applicationConfigurationServiceProvider = applicationBuilder.UseStartup<ApplicationConfigurationStartup>(applicationBuilder.GetApplicationConfigurationConfigurationServiceProvider);
            return applicationConfigurationServiceProvider;
        }

        public static IServiceProvider UseStartupWithDerbyConfigurationStartup<TStartup>(this ApplicationBuilder applicationBuilder)
            where TStartup: class, IApplicationStartup
        {
            var serviceProvier = applicationBuilder.UseStartup<TStartup, ApplicationConfigurationStartup>(applicationBuilder.GetApplicationConfigurationConfigurationServiceProvider);
            return serviceProvier;
        }
    }
}
