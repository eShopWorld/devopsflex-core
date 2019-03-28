using System;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Eshopworld.Core
{
    /// <summary>
    /// The base class from all BigBrother <see cref="Exception"/> based events that are going to be
    /// tracked by AI as Exception Telemetry events.
    /// </summary>
    public class ExceptionEvent : TelemetryEvent
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ExceptionEvent"/>.
        /// </summary>
        /// <param name="exception">The source exception for this event.</param>
        public ExceptionEvent([NotNull]Exception exception)
        {
            Exception = exception;
        }

        /// <summary>
        /// Gets and sets the raw <see cref="Exception"/> that is associated with this event.
        /// </summary>
        [JsonIgnore]
        public Exception Exception { get; set; }

        /// <summary>
        /// Specifies whether stack trace of exception should be simplified, e.g. by removing
        /// references to compiler gerated methods.
        /// </summary>
        [JsonIgnore]
        public virtual bool SimplifyStackTrace => true;

        [JsonIgnore]
        public virtual bool UnwrapAggregate => true;
    }

    /// <summary>
    /// Constraint conversion extensions to <see cref="Exception"/> to produce events of, or based of <see cref="ExceptionEvent"/>.
    /// </summary>
    public static class ExceptionEventExtensions
    {
        /// <summary>
        /// Converts a generic <see cref="Exception"/> into a <see cref="ExceptionEvent"/>.
        /// </summary>
        /// <param name="exception">The original <see cref="Exception"/>.</param>
        /// <returns>The converted <see cref="ExceptionEvent"/>.</returns>
        public static ExceptionEvent ToExceptionEvent(this Exception exception)
        {
            return ToExceptionEvent<ExceptionEvent>(exception);
        }

        /// <summary>
        /// Converts a generic <see cref="Exception"/> into any class that inherits from <see cref="ExceptionEvent"/>.
        /// </summary>
        /// <typeparam name="T">The type of the specific <see cref="ExceptionEvent"/> super class.</typeparam>
        /// <param name="exception">The original <see cref="Exception"/>.</param>
        /// <returns>The converted super class of <see cref="ExceptionEvent"/>.</returns>
        public static T ToExceptionEvent<T>(this Exception exception)
            where T : ExceptionEvent
        {
            return (T)Activator.CreateInstance(typeof(T), exception);
        }
    }
}