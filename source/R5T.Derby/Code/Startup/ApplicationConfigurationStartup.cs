﻿using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.Alamania;
using R5T.Dacia;
using R5T.Ives.Configuration;
using R5T.Leeds;
using R5T.Lombardy.Standard;
using R5T.Macommania.Default;
using R5T.Richmond;
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
            //services
            //    .AddDefaultStringlyTypedPathOperator()
            //    .AddDefaultExecutableFileDirectoryPathProvider(
            //        services.AddDefaultExecutableFilePathProviderAction(),
            //        ServiceAction<IStringlyTypedPathOperator>.AlreadyAdded)
            //    .AddSingleton<IAppSettingsDirectoryPathProvider, ExecutableFileDirectoryAppSettingsDirectoryPathProvider>()
            //    .AddSingleton<IDefaultAppSettingsJsonFileNameProvider, DefaultAppSettingsJsonFileNameProvider>()
            //    .AddSingleton<IDefaultAppSettingsJsonFilePathProvider, DefaultAppSettingsJsonFilePathProvider>()
            //    .AddDirectConfigurationBasedConfigurationNameProvider()
            //    .AddSingleton<IConfigurationNameSpecificAppSettingsJsonFileNameProvider, ConfigurationNameSpecificAppSettingsJsonFileNameProvider>()
            //    .AddSingleton<IConfigurationNameSpecificAppSettingsJsonFilePathProvider, ConfigurationNameSpecificAppSettingsJsonFilePathProvider>()
            //    .AddMachineLocationAwareSecretsDirectoryPathProvider()
            //    ;

            // -1.
            var configurationAction = ServiceAction<IConfiguration>.AlreadyAdded;
#pragma warning disable IDE0042 // Deconstruct variable declaration
            var stringlyTypedPathRelatedOperatorsAction = services.AddStringlyTypedPathRelatedOperatorsAction();
            var machineLocationAwareSecretsDirectoryPathProviderAction = services.AddMachineLocationAwareSecretsDirectoryPathProviderAction(
                stringlyTypedPathRelatedOperatorsAction.stringlyTypedPathOperatorAction);
#pragma warning restore IDE0042 // Deconstruct variable declaration

            // 0.
            var configurationNameToAppSettingsFileNameTokenConverterAction = services.AddDefaultConfigurationNameToAppSettingsFileNameTokenConverterAction();
            var defaultAppSettingsJsonFileNameProviderAction = services.AddDefaultAppSettingsJsonFileNameProviderAction();
            var executableFilePathProviderAction = services.AddDefaultExecutableFilePathProviderAction();

            // 1.
            var configurationNameProviderAction = services.AddDirectConfigurationBasedConfigurationNameProviderAction(
                configurationAction);
            var configurationNameSpecificAppSettingsJsonFileNameProviderAction = services.AddConfigurationNameSpecificAppSettingsJsonFileNameProviderAction(
                configurationNameToAppSettingsFileNameTokenConverterAction);
            var executableFileDirectoryPathProviderAction = services.AddDefaultExecutableFileDirectoryPathProviderAction(
                executableFilePathProviderAction,
                stringlyTypedPathRelatedOperatorsAction.stringlyTypedPathOperatorAction);

            // 2.
            var appSettingsDirectoryPathProviderAction = services.AddExecutableFileDirectoryAppSettingsDirectoryPathProvideAction(
                executableFileDirectoryPathProviderAction);

            // 3.
            var configurationNameSpecificAppSettingsJsonFilePathProviderAction = services.AddConfigurationNameSpecificAppSettingsJsonFilePathProviderAction(
                appSettingsDirectoryPathProviderAction,
                configurationNameProviderAction,
                configurationNameSpecificAppSettingsJsonFileNameProviderAction,
                stringlyTypedPathRelatedOperatorsAction.stringlyTypedPathOperatorAction);
            var defaultAppSettingsJsonFilePathProviderAction = services.AddDefaultAppSettingsJsonFilePathProviderAction(
                appSettingsDirectoryPathProviderAction,
                defaultAppSettingsJsonFileNameProviderAction,
                stringlyTypedPathRelatedOperatorsAction.stringlyTypedPathOperatorAction);

            services
                .Run(configurationNameSpecificAppSettingsJsonFilePathProviderAction)
                .Run(defaultAppSettingsJsonFilePathProviderAction)
                .Run(machineLocationAwareSecretsDirectoryPathProviderAction.SecretsDirectoryPathProviderAction)
                .AddSingleton<IRivetOrganizationDirectoryPathProvider, SharedRivetOrganizationDirectoryPathProvider>() // Override.
                ;
        }
    }
}
