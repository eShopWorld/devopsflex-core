namespace DevOpsFlex.Core
{
    using System;
    using System.Collections.Generic;
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
        /// The internal map of connectors and their subscriptions.
        /// </summary>
        internal static readonly Dictionary<Type, IDisposable> ConnectedSubscriptions = new Dictionary<Type, IDisposable>();

        /// <summary>
        /// Gates connector subscriptions and the storage of those subscriptions in a static dictionary.
        /// </summary>
        internal static readonly object Gate = new object();

        /// <summary>
        /// Performance check to avoid streaming events if no connectors are found.
        /// </summary>
        internal volatile bool Connected;

        /// <summary>
        /// Initializes a new instance of <see cref="LittleBrother"/>.
        /// </summary>
        public LittleBrother()
        {
            foreach (var connector in AppDomain.CurrentDomain.GetPullConnectors())
            {
                lock (Gate)
                {
                    ConnectedSubscriptions[connector.GetType()] = Stream.Subscribe(connector.Connect());
                    Connected = true;
                }
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
