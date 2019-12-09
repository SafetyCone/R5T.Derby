using System;

using Microsoft.Extensions.Configuration;

using ShrewsburyFileNames = R5T.Shrewsbury.FileNames;


namespace R5T.Derby.Extensions
{
    public static class IConfigurationBuilderExtensions
    {
        /// <summary>
        /// Adds:
        /// * <see cref="ShrewsburyFileNames.DefaultAppSettingsJsonFileName"/>.
        /// </summary>
        public static IConfigurationBuilder AddDefaultAppSettingsJsonFile(this IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.AddJsonFile(ShrewsburyFileNames.DefaultAppSettingsJsonFileName);

            return configurationBuilder;
        }
    }
}
