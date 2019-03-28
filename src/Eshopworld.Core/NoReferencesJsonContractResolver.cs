using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Eshopworld.Core
{
    /// <inheritdoc />
    /// <remarks>
    /// This resolver ignores all reference types.
    /// </remarks>
    public class NoReferencesJsonContractResolver : DefaultContractResolver
    {
        /// <inheritdoc />
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);

            if (prop.PropertyType.IsClass && prop.PropertyType != typeof(string) || prop.PropertyType.IsInterface)
            {
                prop.ShouldSerialize = obj => false;
            }

            return prop;
        }
    }
}