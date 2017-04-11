namespace DevOpsFlex.Core
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// The base class from all BigBrother <see cref="Exception"/> based events that are going to be
    /// tracked by AI as Exception Telemetry events.
    /// </summary>
    public class BbExceptionEvent : BbTelemetryEvent
    {
        /// <summary>
        /// Gets and sets the raw <see cref="Exception"/> that is associated with this event.
        /// </summary>
        [JsonIgnore]
        public Exception Exception { get; set; }
    }

    /// <summary>
    /// Containst conversion extensions to <see cref="Exception"/> to produce events of, or based of <see cref="BbExceptionEvent"/>.
    /// </summary>
    public static class BbExceptionEventExtensions
    {
        /// <summary>
        /// Converts a generic <see cref="Exception"/> into a <see cref="BbExceptionEvent"/>.
        /// </summary>
        /// <param name="exception">The original <see cref="Exception"/>.</param>
        /// <returns>The converted <see cref="BbExceptionEvent"/>.</returns>
        public static BbExceptionEvent ToBbEvent(this Exception exception)
        {
            return ToBbEvent<BbExceptionEvent>(exception);
        }

        /// <summary>
        /// Converts a generic <see cref="Exception"/> into any class that inherits from <see cref="BbExceptionEvent"/>.
        /// </summary>
        /// <typeparam name="T">The type of the specific <see cref="BbExceptionEvent"/> super class.</typeparam>
        /// <param name="exception">The original <see cref="Exception"/>.</param>
        /// <returns>The converted super class of <see cref="BbExceptionEvent"/>.</returns>
        public static T ToBbEvent<T>(this Exception exception)
            where T : BbExceptionEvent, new()
        {
            return new T
            {
                Exception = exception
            };
        }
    }
}