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
        /// Initializes an instance of <see cref="BbEvent"/>.
        /// </summary>
        /// <remarks>
        /// Intentionally hidden to prevent folks from instantiating this class directly.
        /// </remarks>
        internal BbEvent()
        {
        }

        /// <summary>
        /// Converts this POCO to a <see cref="IDictionary{TKey,TValue}"/> by using JSonConvert twice (both directions).
        /// </summary>
        /// <returns>The converted <see cref="IDictionary{String, String}"/>.</returns>
        [NotNull]
        public IDictionary<string, string> ToStringDictionary()
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonConvert.SerializeObject(this));
        }

        /// <summary>
        /// Copies all properties on this <see cref="BbEvent"/> event to a specific target <see cref="IDictionary{String, String}"/>.
        /// </summary>
        /// <param name="target">The target <see cref="IDictionary{String, String}"/>.</param>
        /// <param name="replace">true if we want to replace previously existing keys, false otherwise. If false and the key already exists, this method will throw.</param>
        public void CopyPropertiesInto(IDictionary<string, string> target, bool replace = true)
        {
            var properties = ToStringDictionary();

            foreach (var key in properties.Keys)
            {
                if (replace)
                    target[key] = properties[key];
                else
                    target.Add(key, properties[key]);
            }
        }
    }
}
