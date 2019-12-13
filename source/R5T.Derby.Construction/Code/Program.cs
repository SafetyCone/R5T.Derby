using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.Ives;
using R5T.Richmond;
using R5T.Suebia;


namespace R5T.Derby.Construction
{
    class Program
    {
        static void Main(string[] args)
        {
            //Program.TryApplicationConfigurationConfigurationStartup();
            Program.TryApplicationConfigurationStartup();
        }

        private static void TryApplicationConfigurationStartup()
        {
            var applicationConfigurationServiceProvider = ApplicationBuilder.New().GetApplicationConfigurationServiceProvider();

            var secretsDirectoryPathProvider = applicationConfigurationServiceProvider.GetRequiredService<ISecretsDirectoryPathProvider>();

            var secretsDirectoryPath = secretsDirectoryPathProvider.GetSecretsDirectoryPath();

            Console.WriteLine($"Secrets directory path: {secretsDirectoryPath}");
        }

        private static void TryApplicationConfigurationConfigurationStartup()
        {
            var applicationConfigurationConfigurationServiceProvider = ApplicationBuilder.New().GetApplicationConfigurationConfigurationServiceProvider();

            var configurationNameProvider = applicationConfigurationConfigurationServiceProvider.GetRequiredService<IConfigurationNameProvider>();

            var configurationName = configurationNameProvider.GetConfigurationName();

            Console.WriteLine($"Configuration name: {configurationName}");
        }
    }
}
