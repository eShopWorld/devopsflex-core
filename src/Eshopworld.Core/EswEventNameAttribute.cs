using System;

namespace Eshopworld.Core
{
    /// <summary>
    /// Describes an Esw Event Name Attribute
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class EswEventNameAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the name of the event.
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EswEventNameAttribute"/> class.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        public EswEventNameAttribute(string eventName)
        {
            EventName = eventName;
        }
    }
}
