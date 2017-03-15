namespace DevOpsFlex.Core
{
    using System;
    using System.Reactive.Linq;

    /// <summary>
    /// Base implementation for a push connector to facilitate package design and integration.
    /// </summary>
    public abstract class LittleBrotherPushConnector : IPushTelemetry
    {
        /// <summary>
        /// Connects an internal package telemetry pipeline to Big Brother.
        /// </summary>
        /// <returns>The internal package <see cref="IObservable{BbEvent}"/> stream.</returns>
        public virtual IObservable<BbEvent> Connect()
        {
            return LittleBrother.Stream.AsObservable();
        }
    }
}
