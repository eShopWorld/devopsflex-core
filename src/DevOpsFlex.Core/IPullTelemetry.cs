namespace DevOpsFlex.Core
{
    using System;

    /// <summary>
    /// Used by connectors part of the telemetry platform that wish to receive telemetry from external packages.
    /// </summary>
    public interface IPullTelemetry
    {
        /// <summary>
        /// Connects external packages to the already loaded telemetry platform.
        ///     This is used by packages loaded after big brother, that need to start using it to push
        ///     internal telemetry.
        /// </summary>
        /// <param name="stream">The <see cref="IObservable{BbEvent}"/> stream that the internal package should push to.</param>
        void Connect(IObservable<BbEvent> stream);
    }
}
