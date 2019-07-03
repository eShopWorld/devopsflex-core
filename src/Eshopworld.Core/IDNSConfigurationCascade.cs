namespace Eshopworld.Core
{
    /// <summary>
    /// this interface defines evolution platform (V2) service (e.g. API) deployment cascade and possible routes to its instances
    ///
    /// Proxy (Service Fabric Reverse Proxy) -> Cluster (regional external load balancer) -> Global (FrontDoor)
    /// </summary>
    public interface IDnsConfigurationCascade
    {
        string Proxy { get; set; }
        string Cluster { get; set; }
        string Global { get; set; }
    }
}
