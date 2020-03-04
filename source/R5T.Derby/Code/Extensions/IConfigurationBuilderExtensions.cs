using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using R5T.Ives;
using R5T.Shrewsbury;
using R5T.Shrewsbury.Extensions;
using R5T.Suebia;

using ShrewsburyUtilities = R5T.Shrewsbury.Utilities;


namespace R5T.Derby.Extensions
{
    public static class IConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddDefaultAndConfigurationSpecificAppSettingsJsonFiles(this IConfigurationBuilder configurationBuilder, IServiceProvider configurationServiceProvider, bool configurationSpecificIsOptional = false)
        {
            configurationBuilder
                .AddDefaultAppSettingsJsonFile(configurationServiceProvider)
                .AddConfigurationSpecificAppSettingsJsonFile(configurationServiceProvider, configurationSpecificIsOptional)
                ;

            return configurationBuilder;
        }

        public static IConfigurationBuilder AddConfigurationSpecificAppSettingsJsonFile(this IConfigurationBuilder configurationBuilder, IServiceProvider configurationServiceProvider, bool optional = false)
        {
            var configurationNameProvider = configurationServiceProvider.GetRequiredService<IConfigurationNameProvider>();

            var configurationName = configurationNameProvider.GetConfigurationName();

            var configurationNameToAppSettingFileTokenConverter = configurationServiceProvider.GetRequiredService<IConfigurationNameToAppSettingsFileNameTokenConverter>();

            var configurationNameAppSettingsFileNameToken = configurationNameToAppSettingFileTokenConverter.ConvertConfigurationNameToAppSettingsFileNameToken(configurationName);

            var configurationNameSpecificAppSettingsFileName = ShrewsburyUtilities.GetConfigurationNameSpecificAppSettingsJsonFileName(configurationNameAppSettingsFileNameToken);

            configurationBuilder.AddAppSettingsDirectoryJsonFile(configurationServiceProvider, configurationNameSpecificAppSettingsFileName, optional);

            return configurationBuilder;
        }

        public static IConfigurationBuilder AddRivetUserSecretsJsonFile(this IConfigurationBuilder configurationBuilder, IServiceProvider configurationServiceProvider, string secretsFileName)
        {
            var secretsFilePathProvider = configurationServiceProvider.GetRequiredService<ISecretsDirectoryFilePathProvider>();

            var secretsFilePath = secretsFilePathProvider.GetSecretsFilePath(secretsFileName);

            configurationBuilder.AddJsonFile(secretsFilePath);

            return configurationBuilder;
        }
    }
}
