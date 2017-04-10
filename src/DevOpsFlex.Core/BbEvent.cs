namespace DevOpsFlex.Core
{
    using System.Collections.Generic;
    using JetBrains.Annotations;
    using Newtonsoft.Json;

    /// <summary>
    /// Base class for all events pushed by the telemetry platform. Can't be inherited directly but is base
    /// to all the classes that you can inherit from.
    /// </summary>
    public class BbEvent
    {
        /// <summary>
        /// Stores the internal correlation vector.
        /// </summary>
        [CanBeNull]
        public string CorrelationVector { get; set; }

        /// <summary>
        /// Initializes an instance of <see cref="BbEvent"/>.
        /// </summary>
        internal BbEvent()
        {
        }

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
