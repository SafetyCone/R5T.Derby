using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using R5T.Shrewsbury.Extensions;
using R5T.Suebia;


namespace R5T.Derby.Extensions
{
    public static class IConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddRivetUserSecretsJsonFile(this IConfigurationBuilder configurationBuilder, IServiceProvider configurationServiceProvider, string secretsFileName)
        {
            var secretsFilePathProvider = configurationServiceProvider.GetRequiredService<ISecretsDirectoryFilePathProvider>();

            var secretsFilePath = secretsFilePathProvider.GetSecretsFilePath(secretsFileName);

            configurationBuilder.AddJsonFile(secretsFilePath);

            return configurationBuilder;
        }
    }
}
