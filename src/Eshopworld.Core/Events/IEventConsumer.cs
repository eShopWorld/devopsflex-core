using System;
using System.Threading;
using System.Threading.Tasks;

namespace Eshopworld.Core.Events
{
    /// <summary>
    /// Contract that provides a way to send events to the event streaming bus
    /// </summary>
    public interface IEventConsumer: IDisposable
    {
        /// <summary>
        /// Start consuming events from the configured topics
        /// </summary>
        /// <param name="startFromBeginningWhenNoPointer">If no consumer's group offset is stored on Kafka and this is set to true the consumer will consume all existing events in the topic</param>
        /// <param name="cancelToken">Token that can be passed to cancel the operation.</param>
        Task StartConsumingAsync(bool startFromBeginningWhenNoPointer = false, CancellationToken cancelToken = default);

        /// <summary>
        /// Start consuming events from the configured topics from a certain date & time regardless of the stored consume's group offset
        /// </summary>
        /// <param name="start">The date & time to start consuming from.</param>
        /// <param name="cancelToken">Token that can be passed to cancel the operation.</param>
        Task StartConsumingFromDateTimeAsync(DateTimeOffset start, CancellationToken cancelToken = default);

        /// <summary>
        /// Start consuming events from the configured topics from its beginning regardless of the stored consume's group offset
        /// </summary>
        /// <param name="cancelToken">Token that can be passed to cancel the operation.</param>
        Task StartConsumingFromBeginningAsync(CancellationToken cancelToken = default);
    }
}
