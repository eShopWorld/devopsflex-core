using System;

namespace Eshopworld.Core
{
    /// <summary>
    /// Offers control of how properties are serialized depending on the target destination of the event.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EventFilterAttribute : Attribute
    {
        public EventFilterTargets Targets { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="EventFilterAttribute"/>.
        /// </summary>
        /// <param name="targets"></param>
        public EventFilterAttribute(EventFilterTargets targets)
        {
            Targets = targets;
        }
    }

    /// <summary>
    /// Represents bit flag filters that reference different event destinations.
    /// </summary>
    [Flags]
    public enum EventFilterTargets
    {
        None                = 0,
        ApplicationInsights = 1 << 0,
        Kusto               = 1 << 1,
        Messaging           = 1 << 2,
        AllExceptMessaging  = ApplicationInsights | Kusto,
        All                 = ApplicationInsights | Kusto | Messaging
    }
}
