using System;

namespace Eshopworld.Core.Events
{
    /// <summary>
    /// Standard interface that every event that will be published & consumed from the event streaming bus will expose
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// Identifier of the event (aggregate's identifier), used as the message key on the bus
        /// </summary>
        string Identifier { get; }

        /// <summary>
        /// User reference that is responsibile for generating the event
        /// </summary>
        string CreatedBy { get; }

        /// <summary>
        /// Trace identifier that can be used to correlate the request, the event created and the response in the logging
        /// </summary>
        string CorrelationId { get; }

        /// <summary>
        /// Code of the brand for which this event was emitted
        /// </summary>
        string TenantCode { get; }

        /// <summary>
        /// Date & time when the event was emitted
        /// </summary>
        DateTimeOffset CreatedOnUtc { get; }
    }
}
