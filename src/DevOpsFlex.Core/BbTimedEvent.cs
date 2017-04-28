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
        /// The START time for this event.
        /// </summary>
        [JsonIgnore]
        internal DateTime StartTime = DateTime.Now;

        /// <summary>
        /// The END time for thie event.
        /// </summary>
        [JsonIgnore]
        internal DateTime? EndTime;

        /// <summary>
        /// Gets the total elapsed processing time.
        ///     If End() hasn't been called it will use <see cref="DateTime.Now"/> as the current end time without setting an EndTime.
        /// </summary>
        public TimeSpan ProcessingTime => EndTime?.Subtract(StartTime) ?? DateTime.Now.Subtract(StartTime);

        /// <summary>
        /// Ends the event by marking that the process it's tracking has finished.
        /// </summary>
        public void End()
        {
            if (EndTime == null)
            {
                EndTime = DateTime.Now;
            }
        }
    }
}
