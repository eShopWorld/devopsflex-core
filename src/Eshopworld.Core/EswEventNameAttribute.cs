using System;

namespace Eshopworld.Core
{
    /// <summary>
    /// Describes an Esw Event Name Attribute.
    /// Should be used to override the default event name that Captain Hook includes when 
    /// calling an endpoint in response to an event.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class EswEventNameAttribute : Attribute
    {
        /// <summary>
        /// Fully qualified event name
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EswEventNameAttribute"/> class.
        /// </summary>
        /// <param name="eventName">The Fully qualified event name that captain hook should include when calling an endpoint.</param>
        public EswEventNameAttribute(string eventName)
        {
            EventName = eventName;
        }
    }
}
