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
        /// <param name="stream">The <see cref="IObserver{BbEvent}"/> stream that the internal package should push to.</param>
        void Connect(IObserver<BbEvent> stream);
    }
}
