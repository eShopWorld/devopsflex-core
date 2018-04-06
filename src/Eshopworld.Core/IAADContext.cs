namespace Eshopworld.Core
{
    /// <summary>
    /// this interface represents context for AAD Authentication
    /// 
    /// we currently gather information to allow for 
    ///  - file based authentication (file path to RBAC based service principal details as per https://github.com/Azure/azure-libraries-for-net/blob/master/AUTH.md#using-an-authentication-file
    ///  - app id/secret
    /// 
    /// these modes are either/or - if file is resolved, direct properties will not be resolved
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public interface IAADContext
    {
        /// <summary>
        /// file path to token file
        /// </summary>
        string AuthFilePath { get; }

        /// <summary>
        /// id of the azure tenant
        /// </summary>
        string TenantId { get; }

        /// <summary>
        /// azure subscription id
        /// </summary>
        string SubscriptionId { get; }

        /// <summary>
        /// client id (app id)
        /// </summary>
        string ClientId { get;  }

        /// <summary>
        /// shared client/app secret 
        /// </summary>
        string ClientSecret { get; }
    }
}
