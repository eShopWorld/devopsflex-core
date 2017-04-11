namespace DevOpsFlex.Core
{
    using System.Collections.Generic;
    using JetBrains.Annotations;
    using Newtonsoft.Json;

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

        /// <summary>
        /// Converts this POCO to a <see cref="IDictionary{String, String}"/> by using JSonConvert twice (both directions).
        /// </summary>
        /// <returns>The converted <see cref="IDictionary{String, String}"/>.</returns>
        [NotNull]
        public IDictionary<string, string> ToStringDictionary()
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonConvert.SerializeObject(this));
        }
    }
}
