namespace DevOpsFlex.Core
{
    using System;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;

    /// <summary>
    /// Used by <see cref="DevOpsFlex"/> packages to push internal telemetry.
    /// </summary>
    public class LittleBrother
    {
        /// <summary>
        /// The internal stream as a dual <see cref="Subject{BbEvent}"/>.
        /// </summary>
        internal static readonly Subject<BbEvent> Stream = new Subject<BbEvent>();

        /// <summary>
        /// Performance check to avoid streaming events if no connectors are found.
        /// </summary>
        internal bool Connected;

        /// <summary>
        /// Initializes a new instance of <see cref="LittleBrother"/>.
        /// </summary>
        public LittleBrother()
        {
            foreach (var connector in AppDomain.CurrentDomain.GetPullConnectors())
            {
                connector.Connect(Stream.AsObservable());
                Connected = true;
            }
        }

        /// <summary>
        /// Publishes an internal <see cref="BbEvent"/> by streaming it to any available BigBrothers in the <see cref="AppDomain"/>.
        /// </summary>
        /// <param name="event"></param>
        public void Publish(BbEvent @event)
        {
            if (Connected) Stream.OnNext(@event);
        }
    }
}
