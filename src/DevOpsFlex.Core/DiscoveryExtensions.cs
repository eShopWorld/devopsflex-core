namespace DevOpsFlex.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;

    /// <summary>
    /// Constains extensions to reflection constructs to help with discovery across the <see cref="AppDomain"/>.
    /// </summary>
    public static class DiscoveryExtensions
    {
        /// <summary>
        /// RACING DISCOVERY CONDITIONS !!!
        /// </summary>
        internal static readonly object Gate = new object();

        [NotNull]
        public static IEnumerable<IPushTelemetry> GetPushConnectors([NotNull]this AppDomain domain)
        {
            return domain.GetConnectors<IPushTelemetry>();
        }

        [NotNull]
        public static IEnumerable<IPullTelemetry> GetPullConnectors([NotNull]this AppDomain domain)
        {
            return domain.GetConnectors<IPullTelemetry>();
        }

        [NotNull]
        internal static IEnumerable<T> GetConnectors<T>([NotNull]this AppDomain domain)
        {
            return domain.GetAssemblies()
                         .SelectMany(a => a.GetTypes())
                         .Where(typeof(T).IsAssignableFrom)
                         .Select(t => Activator.CreateInstance<T>());
        }
    }
}
