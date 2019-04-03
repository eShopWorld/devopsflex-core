namespace Eshopworld.Core
{
    /// <summary>
    /// The base interface from all BigBrother metric based tracking that are going to be
    /// tracked by AI as Metric TimeSeries.
    /// </summary>
    public interface ITrackedMetric
    {
        /// <summary>
        /// Gets and sets the value for the metric being pushed.
        /// </summary>
        double Metric { get; set; }
    }
}
