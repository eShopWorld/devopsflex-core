using System;
using System.Threading;
using System.Threading.Tasks;

namespace Eshopworld.Core.Events
{
    /// <summary>
    /// Contract that provides a way to send events to the event streaming bus
    /// </summary>
    public interface IEventProducer: IDisposable
    {
        /// <summary>
        /// Publish an event to a topic
        /// </summary>
        /// <typeparam name="TEvent">The type of the event that we are sending.</typeparam>
        /// <param name="event">The event that we are sending.</param>
        /// <param name="topicName">The name of the topic to send the event to.</param>
        /// <param name="partition">If partitioning is supported defines the topic partition the event will be published to.</param>
        /// <param name="cancelToken">Token that can be passed to cancel the operation.</param>
        Task ProduceAsync<TEvent>(TEvent @event, string topicName, int? partition = null, CancellationToken cancelToken = default) where TEvent : IEvent, new();
    }
}
