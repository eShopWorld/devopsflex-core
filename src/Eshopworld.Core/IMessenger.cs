namespace Eshopworld.Core
{
    using System;
    using System.Threading.Tasks;
    using JetBrains.Annotations;

    /// <summary>
    /// Contract that provides a simple way to send messages to queues.
    /// </summary>
    public interface ISendMessages : IDisposable
    {
        /// <summary>
        /// Sends a message onto a queue.
        /// </summary>
        /// <typeparam name="T">The type of the message that we are sending.</typeparam>
        /// <param name="message">The message that we are sending.</param>
        Task Send<T>([NotNull] T message) where T : class;
    }

    /// <summary>
    /// Contract that provides a simple way to send events to topics.
    /// </summary>
    public interface IPublishEvents : IDisposable
    {
        /// <summary>
        /// Sends an event onto a topic.
        /// </summary>
        /// <typeparam name="T">The type of the event that we are sending.</typeparam>
        /// <param name="event">The event that we are sending.</param>
        Task Publish<T>(T @event) where T : class;
    }

    public interface ICancelReceive
    {
        /// <summary>
        /// Stops receiving a message or event type by disabling the read pooling on the a message queue or topic subscription.
        /// </summary>
        /// <typeparam name="T">The type of the message that we are cancelling the receive on.</typeparam>
        void CancelReceive<T>() where T : class;
    }

    /// <summary>
    /// Contract that provides a simple way to send and receive messages through callbacks.
    /// </summary>
    public interface IDoMessages : ISendMessages, ICancelReceive
    {
        /// <summary>
        /// Sets up a call back for receiving any message of type <typeparamref name="T"/>.
        /// If you try to setup more then one callback to the same message or event type <typeparamref name="T"/> you'll get an <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <typeparam name="T">The type of the message that we are subscribing to receiving.</typeparam>
        /// <param name="callback">The <see cref="Action{T}"/> delegate that will be called for each message received.</param>
        /// <param name="batchSize">The size of the batch when reading for a queue - used as the pre-fetch parameter of the message receiver.</param>
        /// <exception cref="InvalidOperationException">Thrown when you attempt to setup multiple callbacks against the same <typeparamref name="T"/> parameter.</exception>
        void Receive<T>(Action<T> callback, int batchSize = 10) where T : class;
    }

    /// <summary>
    /// Contract that provides a simple way to send and receive events through callbacks.
    /// </summary>
    public interface IDoPubSub : IPublishEvents, ICancelReceive
    {
        /// <summary>
        /// Sets up a call back for receiving any event of type <typeparamref name="T"/>.
        /// If you try to setup more then one callback to the same message or event type <typeparamref name="T"/> you'll get an <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <typeparam name="T">The type of the event that we are subscribing to receiving.</typeparam>
        /// <param name="callback">The <see cref="Action{T}"/> delegate that will be called for each event received.</param>
        /// <param name="subscriptionName">The name of the reliable subscription we're doing for this event type.</param>
        /// <param name="batchSize">The size of the batch when reading for a topic subscription - used as the pre-fetch parameter of the message receiver</param>
        /// <exception cref="InvalidOperationException">Thrown when you attempt to setup multiple callbacks against the same <typeparamref name="T"/> parameter.</exception>
        Task Subscribe<T>(Action<T> callback, string subscriptionName, int batchSize = 10) where T : class;
    }

    /// <summary>
    /// Contract that exposes a reactive way to receive and send messages.
    /// </summary>
    public interface IDoReactiveMessages : ISendMessages, ICancelReceive
    {
        /// <summary>
        /// Setups up the required receive pipeline for the given message type and returns a reactive
        /// <see cref="IObservable{T}"/> that you can plug into.
        /// </summary>
        /// <typeparam name="T">The type of the message we want the reactive pipeline for.</typeparam>
        /// <returns>The typed <see cref="IObservable{T}"/> that you can plug into.</returns>
        IObservable<T> GetMessageObservable<T>(int batchSize = 10) where T : class;
    }

    /// <summary>
    /// Contract that exposes a reactive way to receive and send messages.
    /// </summary>
    public interface IDoReactiveEvents : ISendMessages, ICancelReceive
    {
        /// <summary>
        /// Setups up the required receive pipeline for the given event type and returns a reactive
        /// <see cref="IObservable{T}"/> that you can plug into.
        /// </summary>
        /// <typeparam name="T">The type of the event we want the reactive pipeline for.</typeparam>
        /// <param name="subscriptionName">The name of the reliable subscription we're doing for this event type.</param>
        /// <param name="batchSize">The size of the batch when reading for a topic subscription - used as the pre-fetch parameter of the message receiver</param>
        /// <returns>The typed <see cref="IObservable{T}"/> that you can plug into.</returns>
        Task<IObservable<T>> GetEventObservable<T>(string subscriptionName, int batchSize = 10) where T : class;
    }

    /// <summary>
    /// Contract that exposes all messaging operations, both for queues and topics.
    /// </summary>
    public interface IDoFullMessaging : IDoMessages, IDoPubSub
    {
    }

    /// <summary>
    /// Contract that exposes all messaging operations, both for queues and topics, through reactive (Rx) receivers.
    /// </summary>
    public interface IDoFullReactiveMessaging : IDoReactiveMessages, IDoReactiveEvents
    {
    }
}
