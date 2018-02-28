namespace Eshopworld.Core
{
    using JetBrains.Annotations;

    /// <summary>
    /// The base class from all BigBrother telemetry based events that are going to be
    /// tracked by AI as Telemetry Events.
    /// </summary>
    public class BbTelemetryEvent : BbEvent
    {
        /// <summary>
        /// Stores the internal correlation vector.
        /// </summary>
        [CanBeNull]
        public string CorrelationVector { get; set; }
    }
}
