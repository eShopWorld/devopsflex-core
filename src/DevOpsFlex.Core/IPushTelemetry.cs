namespace DevOpsFlex.Core
{
    using System;

    /// <summary>
    /// Used by connectors that wish to push telemetry onto the telemetry platform.
    /// </summary>
    public interface IPushTelemetry
    {
        /// <summary>
        /// Connects external packages that were loaded before big brother was loaded.
        ///     This is used by packages loaded before big brother, so that big brother can discover them and connect
        ///     them to start receiving their internal events.
        /// </summary>
        /// <returns>The <see cref="IObserver{BbEvent}"/> stream of events that the internal package will push to.</returns>
        IObserver<BbEvent> Connect();
    }
}
