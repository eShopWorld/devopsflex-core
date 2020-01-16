namespace Eshopworld.Core
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// The base class for events that are timed. They START and END and while doing that they also
    ///     fire a Metric event with the time taken to execute throughout from the START to the END.
    /// </summary>
    public class TimedTelemetryEvent : TelemetryEvent
    {
        /// <summary>
        /// The START time for this event.
        /// </summary>
        [JsonIgnore]
        internal DateTime StartTime = GetDateTimeUtcNow();

        /// <summary>
        /// The END time for thie event.
        /// </summary>
        [JsonIgnore]
        internal DateTime? EndTime;

        /// <summary>
        /// Gets the total elapsed processing time.
        ///     If End() hasn't been called it will use the current time as the end time without setting the <see cref="EndTime"/> property.
        /// </summary>
        [JsonIgnore]
        public TimeSpan ProcessingTime => (EndTime?? GetDateTimeUtcNow()).Subtract(StartTime);

        /// <summary>
        /// testability delegate so that we have full control over timing aspects
        /// </summary>
        internal static Func<DateTime> GetDateTimeUtcNow { get; set; } = () => DateTime.UtcNow;

        /// <summary>
        /// Ends the event by marking that the process it's tracking has finished.
        /// </summary>
        public void End()
        {
            if (EndTime == null)
            {
                EndTime = GetDateTimeUtcNow();
            }
        }
    }
}
