namespace DevOpsFlex.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using JetBrains.Annotations;

    /// <summary>
    /// Constains extensions to reflection constructs to help with discovery across the <see cref="AppDomain"/>.
    /// </summary>
    public static class DiscoveryExtensions
    {
        /// <summary>
        /// Gets all the <see cref="IPushTelemetry"/> connectors present in a specific <see cref="AppDomain"/>.
        /// </summary>
        /// <param name="domain">The specific <see cref="AppDomain"/> that we want to scan.</param>
        /// <returns>A list of all <see cref="IPushTelemetry"/> instances found in the <see cref="AppDomain"/>.</returns>
        [NotNull]
        public static IEnumerable<IPushTelemetry> GetPushConnectors([NotNull]this AppDomain domain)
        {
            return domain.GetAssemblies()
                         .GetConnectors<IPushTelemetry>();
        }

        /// <summary>
        /// Gets all the <see cref="IPullTelemetry"/> connectors present in a specific <see cref="AppDomain"/>.
        /// </summary>
        /// <param name="domain">The specific <see cref="AppDomain"/> that we want to scan.</param>
        /// <returns>A list of all <see cref="IPullTelemetry"/> instances found in the <see cref="AppDomain"/>.</returns>
        [NotNull]
        public static IEnumerable<IPullTelemetry> GetPullConnectors([NotNull]this AppDomain domain)
        {
            return domain.GetAssemblies()
                         .GetConnectors<IPullTelemetry>();
        }

        /// <summary>
        /// Gets all the connectors of type <typeparamref name="T"/> present in a specific <see cref="AppDomain"/>.
        /// </summary>
        /// <typeparam name="T">The type of interface to look for when scanning the <see cref="AppDomain"/>.</typeparam>
        /// <param name="assemblies">The specific <see cref="AppDomain"/> that we want to scan.</param>
        /// <returns>A list of all <typeparamref name="T"/> instances found in the <see cref="AppDomain"/>.</returns>
        [NotNull]
        internal static IEnumerable<T> GetConnectors<T>([NotNull]this IEnumerable<Assembly> assemblies)
        {
            return assemblies.Where(a => a.FullName.StartsWith(nameof(DevOpsFlex)))
                             .SelectMany(a => a.GetTypes())
                             .Where(t => !t.IsInterface && typeof(T).IsAssignableFrom(t))
                             .Select(t =>
                             {
                                 // This can be more elegantely done through a Roslyn Analyzer package
                                 if (t.GetConstructor(new Type[] {}) == null)
                                 {
                                     throw new InvalidOperationException($"Type {t.FullName} needs an empty constructor to be considered a connector. But doesn't have one.");
                                 }

                                 return (T) Activator.CreateInstance(t);
                             });
        }
    }
}
