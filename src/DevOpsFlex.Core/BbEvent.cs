namespace DevOpsFlex.Core
{
    /// <summary>
    /// Base class for all events pushed by the telemetry platform. Can't be inherited directly but is base
    /// to all the classes that you can inherit from.
    /// </summary>
    public class BbEvent
    {
        /// <summary>
        /// Initializes an instance of <see cref="BbEvent"/>.
        /// </summary>
        /// <remarks>
        /// Intentionally hidden to prevent folks from instantiating this class directly.
        /// </remarks>
        internal BbEvent()
        {
        }
    }
}
