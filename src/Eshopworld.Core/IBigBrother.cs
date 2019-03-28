using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Eshopworld.Core
{
    /// <summary>
    /// Contract that provides a way to publish telemetry events for instrumentation.
    /// </summary>
    public interface IBigBrother
    {
        /// <summary>
        /// Publishes a <see cref="TelemetryEvent"/> through the pipeline.
        /// </summary>
        /// <param name="event">The event that we want to publish.</param>
        /// <param name="callerMemberName">
        /// This should be populated by <see cref="System.Runtime.CompilerServices"/>, do not populate this manually.
        /// The method or property name of the caller to the method.
        /// </param>
        /// <param name="callerFilePath">
        /// This should be populated by <see cref="System.Runtime.CompilerServices"/>, do not populate this manually.
        /// The full path of the source file that contains the caller. This is the file path at the time of compile.
        /// </param>
        /// <param name="callerLineNumber">
        /// This should be populated by <see cref="System.Runtime.CompilerServices"/>, do not populate this manually.
        /// The line number in the source file at which the method is called.
        /// </param>
        Task PublishAsync<T>(
            [NotNull] T @event,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = -1) where T : TelemetryEvent;

        /// <remarks>
        /// To be removed in 4.0
        /// </remarks>>
        [Obsolete("Switch to PublishAsync.")]
        void Publish<T>(
            [NotNull] T @event,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = -1) where T : TelemetryEvent;

        /// <summary>
        /// Publishes an anonymous class through the pipeline.
        /// </summary>
        /// <param name="event">The anonymous event that we want to publish.</param>
        /// <param name="callerMemberName">
        /// This should be populated by <see cref="System.Runtime.CompilerServices"/>, do not populate this manually.
        /// The method or property name of the caller to the method.
        /// </param>
        /// <param name="callerFilePath">
        /// This should be populated by <see cref="System.Runtime.CompilerServices"/>, do not populate this manually.
        /// The full path of the source file that contains the caller. This is the file path at the time of compile.
        /// </param>
        /// <param name="callerLineNumber">
        /// This should be populated by <see cref="System.Runtime.CompilerServices"/>, do not populate this manually.
        /// The line number in the source file at which the method is called.
        /// </param>
        void Publish(
            [NotNull] object @event,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = -1);

        /// <summary>
        /// Creates a tracked metric proxy used to track custom metrics in Application Insights.
        /// </summary>
        /// <typeparam name="T">The base type for the proxy we want to create.</typeparam>
        /// <returns>A proxy to the base type of the metric requested.</returns>
        T GetTrackedMetric<T>() where T : ITrackedMetric;

        /// <summary>
        /// Creates a tracked metric proxy used to track custom metrics in Application Insights.
        /// </summary>
        /// <typeparam name="T">The base type for the proxy we want to create.</typeparam>
        /// <param name="parameters">A flat list of the constructor parameters to invoke when instantiating the base type.</param>
        /// <returns>A proxy to the base type of the metric requested.</returns>
        T GetTrackedMetric<T>(params object[] parameters) where T : ITrackedMetric;

        /// <summary>
        /// Forces the telemetry channel to be in developer mode, where it will instantly push
        /// telemetry to the Application Insights account.
        /// </summary>
        [NotNull] IBigBrother DeveloperMode();

        /// <summary>
        /// Flush out all telemetry clients, both the external and the internal one.
        /// </summary>
        /// <remarks>
        /// There is internal telemetry associated with calling this method to prevent bad usage.
        /// </remarks>
        void Flush();

        /// <summary>
        /// Uses Kusto to stream <see cref="TelemetryEvent"/> besides the normal streaming targets defined in <see cref="IBigBrother"/>.
        /// </summary>
        /// <param name="kustoUri">The absolute URI of the Kusto Engine to use for the base engine.</param>
        /// <param name="kustoIngestUri">The absolute URI of the Kusto ingestion endpoint.</param>
        /// <param name="kustoDb">The name of the Kusto database to stream to.</param>
        /// <returns></returns>
        IBigBrother UseKusto(string kustoUri, string kustoIngestUri, string kustoDb);
    }
}
