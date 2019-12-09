using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ShrewsburyFileNames = R5T.Shrewsbury.FileNames;

using R5T.Suebia;


namespace R5T.Derby.Extensions
{
    public static class IConfigurationBuilderExtensions
    {
        /// <summary>
        /// Adds the default appsettings.json file.
        /// </summary>
        public static IConfigurationBuilder AddDefaultAppSettingsJsonFile(this IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.AddJsonFile(ShrewsburyFileNames.DefaultAppSettingsJsonFileName);

            return configurationBuilder;
        }

        public static IConfigurationBuilder AddRivetUserSecretsFile(this IConfigurationBuilder configurationBuilder, IServiceProvider configurationServiceProvider, string secretsFileName)
        {
            var secretsFilePathProvider = configurationServiceProvider.GetRequiredService<ISecretsFilePathProvider>();

            var secretsFilePath = secretsFilePathProvider.GetSecretsFilePath(secretsFileName);

            configurationBuilder.AddJsonFile(secretsFilePath);

            return configurationBuilder;
        }
    }
}
