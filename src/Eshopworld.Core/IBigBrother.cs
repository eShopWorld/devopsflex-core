namespace Eshopworld.Core
{
    using System.Runtime.CompilerServices;
    using JetBrains.Annotations;

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
        void Publish(
            [NotNull] TelemetryEvent @event,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = -1);

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
        /// <param name="kustoNameLocationUri">The pair 'KUSTONAME.LOCATION' used in all Kusto endpoint URIs.</param>
        /// <param name="kustoDb">The name of the Kusto database to stream to.</param>
        /// <param name="tenantId">The AAD tenant ID where the Kusto engine is located.</param>
        /// <param name="appId">The AAD application ID used to authenticate in Kusto.</param>
        /// <param name="appKey">The AAD application Key used to authenticate in Kusto.</param>
        /// <returns></returns>
        IBigBrother UseKusto(string kustoNameLocationUri, string kustoDb, string tenantId, string appId, string appKey);
    }
}
