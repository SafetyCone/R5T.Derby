using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using R5T.Ives.Configuration;
using R5T.Suebia;

using ShrewsburyFileNames = R5T.Shrewsbury.FileNames;
using ShrewsburyUtilities = R5T.Shrewsbury.Utilities;


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

        public static IConfigurationBuilder AddConfigurationSpecificAppSettingsJsonFile(this IConfigurationBuilder configurationBuilder)
        {
            var intermediateConfiguration = configurationBuilder.Build();

            var configurationName = intermediateConfiguration[DirectConfigurationBasedConfigurationNameProvider.ConfigurationNameConfigurationKey];

            // No validation of configuration name nor conversion to appsettings file-name token. Just direct use.

            var configurationNameSpecificAppSettingsJsonFileName = ShrewsburyUtilities.GetConfigurationNameSpecificAppSettingsJsonFileName(configurationName);

            // No directory path specification, just assume configuration file is in executable directory.

            configurationBuilder.AddJsonFile(configurationNameSpecificAppSettingsJsonFileName);

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
