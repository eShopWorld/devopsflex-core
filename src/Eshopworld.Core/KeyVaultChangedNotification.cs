namespace Eshopworld.Core
{
    /// <summary>
    /// this notification captures that the content of the Key Vault has changed
    ///
    /// the key vault is designated by name <see cref="KeyVaultName"/>
    /// </summary>
    public class KeyVaultChangedNotification : BaseNotification
    {
        public string KeyVaultName { get; set; }
    }
}
