using System.Threading;
using System.Threading.Tasks;

namespace Eshopworld.Core.Events
{
    /// <summary>
    /// Contract for event handlers, only for internal use (use the generic one instead)
    /// </summary>
    public interface IEventHandler
    {
    }

    /// <summary>
    /// Generic contract for event handlers
    /// </summary>
    public interface IEventHandler<in TEvent> : IEventHandler where TEvent : IEvent
    {
        /// <summary>
        /// Handle event
        /// </summary>
        /// <param name="event">Event that needs to be handled</param>
        /// <param name="cancelToken">Token that can be passed to cancel the operation.</param>
        Task HandleAsync(TEvent @event, CancellationToken cancelToken = default);
    }
}
