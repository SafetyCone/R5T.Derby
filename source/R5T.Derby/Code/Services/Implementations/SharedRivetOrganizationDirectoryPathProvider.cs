using System;

using R5T.Alamania;
using R5T.Bulgaria;
using R5T.Costobocia;
using R5T.Lombardy;
using R5T.Ostrogothia.Rivet;

using R5T.T0064;


namespace R5T.Derby
{
    /// <summary>
    /// Provides the Rivet organization directory path from the Dropbox directory path (provided by the codenamed Bulgaria project).
    /// </summary>
    [ServiceImplementationMarker]
    public class SharedRivetOrganizationDirectoryPathProvider : IRivetOrganizationDirectoryPathProvider, IServiceImplementation
    {
        public const string SharedDirectoryName = "Shared"; // Should be in an ISharedDirectoryNameConventions service library.


        private IDropboxDirectoryPathProvider DropboxDirectoryPathProvider { get; }
        private IOrganizationStringlyTypedPathOperator OrganizationStringlyTypedPathOperator { get; }
        private IStringlyTypedPathOperator StringlyTypedPathOperator { get; }


        public SharedRivetOrganizationDirectoryPathProvider(
            IDropboxDirectoryPathProvider dropboxDirectoryPathProvider,
            IOrganizationStringlyTypedPathOperator organizationStringlyTypedPathOperator,
            IStringlyTypedPathOperator stringlyTypedPathOperator)
        {
            this.DropboxDirectoryPathProvider = dropboxDirectoryPathProvider;
            this.OrganizationStringlyTypedPathOperator = organizationStringlyTypedPathOperator;
            this.StringlyTypedPathOperator = stringlyTypedPathOperator;
        }

        public string GetRivetOrganizationDirectoryPath()
        {
            var dropboxDirectoryPath = this.DropboxDirectoryPathProvider.GetDropboxDirectoryPath();

            var rivetOrganizationDropboxDirectoryPath = this.OrganizationStringlyTypedPathOperator.GetOrganizationDirectoryPath(dropboxDirectoryPath, RivetOrganization.Instance);

            var sharedRivetOrganizationDropboxDirectoryPath = this.StringlyTypedPathOperator.GetDirectoryPath(rivetOrganizationDropboxDirectoryPath, SharedRivetOrganizationDirectoryPathProvider.SharedDirectoryName);

            return sharedRivetOrganizationDropboxDirectoryPath;
        }
    }
}
