using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Eshopworld.Core
{
    /// <inheritdoc />
    /// <remarks>
    /// This resolver honors the usage of <see cref="EventFilterAttribute"/>.
    ///     It can also be used to ignore all reference types in the class.
    /// </remarks>
    public class EventContractResolver : DefaultContractResolver
    {
        private readonly EventFilterTargets _targets;
        private readonly bool _ignoreReferences;

        /// <summary>
        /// Initializes a new instance of <see cref="EventContractResolver"/>.
        /// </summary>
        /// <param name="targets">The <see cref="EventFilterAttribute"/> flags that we want to target with this resolver.</param>
        /// <param name="ignoreReferences">True if we want the resolver to ignore all reference properties, false otherwise.</param>
        public EventContractResolver(EventFilterTargets targets, bool ignoreReferences = false)
        {
            _targets = targets;
            _ignoreReferences = ignoreReferences;
        }

        /// <inheritdoc />
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (_ignoreReferences && (property.PropertyType.IsClass && property.PropertyType != typeof(string) || property.PropertyType.IsInterface))
            {
                property.ShouldSerialize = _ => false;
            }
            else if (property.AttributeProvider.GetAttributes(true).OfType<EventFilterAttribute>().FirstOrDefault() is EventFilterAttribute att)
            {
                if ((att.Targets & _targets) != _targets)
                {
                    property.ShouldSerialize = _ => false;
                }
            }

            return property;
        }
    }
}