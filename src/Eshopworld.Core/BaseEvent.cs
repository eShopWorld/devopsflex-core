using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Eshopworld.Core
{
    /// <summary>
    /// Base class for all events pushed by the telemetry platform. Can't be inherited directly but is base
    /// to all the classes that you can inherit from.
    /// </summary>
    public class BaseEvent
    {
        /// <summary>
        /// Holds a static reference to the <see cref="JsonSerializerSettings"/> used when serializing to Application Insights.
        /// </summary>
        internal static readonly JsonSerializerSettings EventFilterJsonSettings =
            new JsonSerializerSettings
            {
                ContractResolver = new EventContractResolver(EventFilterTargets.ApplicationInsights)
            };

        /// <summary>
        /// Holds a static reference to the <see cref="JsonSerializerSettings"/> used when serializing to Application Insights
        ///     and ignoring all reference properties besides <see cref="string"/>.
        /// </summary>
        internal static readonly JsonSerializerSettings EventFilterNoReferencesJsonSettings =
            new JsonSerializerSettings
            {
                ContractResolver = new EventContractResolver(EventFilterTargets.ApplicationInsights, true),
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Error
            };

        /// <summary>
        /// Initializes an instance of <see cref="BaseEvent"/>.
        /// </summary>
        /// <remarks>
        /// Intentionally hidden to prevent folks from instantiating this class directly.
        /// </remarks>
        internal BaseEvent()
        {
        }

        /// <summary>
        /// Converts this POCO to a <see cref="IDictionary{TKey,TValue}"/> by using JSonConvert twice (both directions).
        /// </summary>
        /// <returns>The converted <see cref="IDictionary{String, String}"/>.</returns>
        [NotNull]
        internal virtual IDictionary<string, string> ToStringDictionary()
        {
            try
            {
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonConvert.SerializeObject(this, EventFilterJsonSettings));
            }
            catch (Exception)
            {
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonConvert.SerializeObject(this, EventFilterNoReferencesJsonSettings));
            }
        }

        /// <summary>
        /// Copies all properties on this <see cref="BaseEvent"/> event to a specific target <see cref="IDictionary{String, String}"/>.
        /// </summary>
        /// <param name="target">The target <see cref="IDictionary{String, String}"/>.</param>
        /// <param name="replace">true if we want to replace previously existing keys, false otherwise. If false and the key already exists, this method will throw.</param>
        internal void CopyPropertiesInto(IDictionary<string, string> target, bool replace = true)
        {
            var properties = ToStringDictionary();

            foreach (var key in properties.Keys)
            {
                if (replace)
                    target[key] = properties[key];
                else if (!target.ContainsKey(key))
                    target.Add(key, properties[key]);
            }
        }
    }
}
