namespace Eshopworld.Core
{
    /// <summary>
    /// The base class from all BigBrother telemetry based events that are going to be
    /// tracked by AI as Telemetry Events.
    /// </summary>
    public class TelemetryEvent : BaseEvent
    {
        /// <summary>
        /// Gets and sets the method or property name of the caller to the method.
        /// </summary>
        public string? CallerMemberName { get; internal set; }

        /// <summary>
        /// Gets and sets the full path of the source file that contains the caller. This is the file path at the time of compile.
        /// </summary>
        public string? CallerFilePath { get; internal set; }

        /// <summary>
        /// Gets and sets the line number in the source file at which the method is called.
        /// </summary>
        public int CallerLineNumber { get; internal set; }
    }
}
