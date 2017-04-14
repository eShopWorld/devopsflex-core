namespace DevOpsFlex.Core
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// The base class for events that are timed. They START and END and while doing that they also
    ///     fire a Metric event with the time taken to execute throughout from the START to the END.
    /// </summary>
    public class BbTimedEvent : BbTelemetryEvent
    {
        /// <summary>
        /// Gets and sets the START time for this event.
        /// </summary>
        [JsonIgnore]
        internal DateTime StartTime = DateTime.Now;

        /// <summary>
        /// Gets and sets the END time for thie event.
        /// </summary>
        [JsonIgnore]
        internal DateTime EndTime;

        /// <summary>
        /// Ends the event by marking that the process it's tracking has finished.
        /// </summary>
        public void End()
        {
            EndTime = DateTime.Now;
        }
    }
}
