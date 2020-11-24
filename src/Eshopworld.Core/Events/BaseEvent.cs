using System;
using Newtonsoft.Json;

namespace Eshopworld.Core.Events
{
    /// <summary>
    /// Standard interface that every event that will be published & consumed from the event streaming bus will expose
    /// </summary>
    public abstract class BaseEvent
    {
        [JsonConstructor]
        protected BaseEvent(
            string identifier,
            string originService,
            string createdBy,
            DateTimeOffset createdOnUtc)
        {
            Identifier = identifier;
            OriginService = originService;
            CreatedBy = createdBy;
            CreatedOnUtc = createdOnUtc;
        }
        /// <summary>
        /// Identifier of the event (aggregate's identifier), used as the message key on the bus
        /// </summary>
        public string Identifier { get; private set; }
        /// <summary>
        /// The service that is responsible for generating the event
        /// </summary>
        public string OriginService { get; private set; }
        /// <summary>
        /// User reference that is responsibile for generating the event
        /// </summary>
        public string CreatedBy { get; private set; }
        /// <summary>
        /// Date & time when the event was emitted
        /// </summary>
        public DateTimeOffset CreatedOnUtc { get; private set; }
        /// <summary>
        /// Code of the brand for which this event was emitted
        /// </summary>
        public string TenantCode { get; set; }
        /// <summary>
        /// Trace identifier that can be used to correlate the request, the event created and the response in the logging
        /// </summary>
        public string CorrelationId { get; set; }
    }
}
