namespace Eshopworld.Core
{
    /// <summary>
    /// The base class from all BigBrother metric based events that are going to be
    /// tracked by AI as Telemetry Events.
    /// </summary>
    public class MetricTelemetryEvent : TelemetryEvent
    {
        /// <summary>
        /// Gets and sets the name of the metric being pushed.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets and sets the value for the metric being pushed.
        /// </summary>
        public double Value { get; set; }
    }
}
