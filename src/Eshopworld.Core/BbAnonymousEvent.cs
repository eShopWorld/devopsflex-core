namespace Eshopworld.Core
{
    using JetBrains.Annotations;

    /// <summary>
    /// The base class from all BigBrother anonymous class based events that are going to be
    /// tracked by AI as Telemetry Events.
    /// </summary>
    internal class BbAnonymousEvent : BbTelemetryEvent
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BbAnonymousEvent"/>.
        /// </summary>
        /// <param name="payload"></param>
        internal BbAnonymousEvent([NotNull] object payload)
        {
            Payload = payload;
        }

        /// <summary>
        /// Gets and sets the name of the event being pushed.
        /// </summary>
        [NotNull] internal string Name => CallerMemberName;

        /// <summary>
        /// Gets and sets the anonynimous class as an <see cref="object"/> that originated this event.
        /// </summary>
        [NotNull] internal object Payload { get; }
    }
}
