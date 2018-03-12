namespace Eshopworld.Core
{
    /// <summary>
    /// The base class from all BigBrother telemetry based events that are going to be
    /// tracked by AI as Telemetry Events.
    /// </summary>
    public class BbTelemetryEvent : BbEvent
    {
        /// <summary>
        /// Gets and sets the method or property name of the caller to the method.
        /// </summary>
        internal string CallerMemberName { get; set; }

        /// <summary>
        /// Gets and sets the full path of the source file that contains the caller. This is the file path at the time of compile.
        /// </summary>
        internal string CallerFilePath { get; set; }

        /// <summary>
        /// Gets and sets the line number in the source file at which the method is called.
        /// </summary>
        internal int CallerLineNumber { get; set; }
    }
}
