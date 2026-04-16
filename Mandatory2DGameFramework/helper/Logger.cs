using System.Diagnostics;

namespace Mandatory2DGameFramework.helper.logger
{
    /// <summary>
    /// Provides a thread-safe, singleton logger for writing 
    /// trace information, warnings, and errors to registered
    /// listeners.
    /// </summary>
    /// <remarks>The Logger class does not add any default listeners. 
    /// User must add one or more TraceListener instances using the 
    /// AddListener method to receive log output. This design allows 
    /// the logging framework to remain neutral and adaptable to 
    /// different logging targets. The singleton instance can be 
    /// accessed via the Log property.</remarks>
    public sealed class Logger
    {
        #region Instances
        private static readonly Lazy<Logger> _log = new Lazy<Logger>(() => new Logger());
        #endregion

        #region Properties
        /// <summary>
        /// Gets the default logger instance for the application.
        /// </summary>
        /// <remarks>Use this property to write log messages 
        /// throughout the application. The returned logger is a 
        /// singleton and is thread-safe for concurrent use.</remarks>
        public static Logger Log => _log.Value;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Logger class.
        /// </summary>
        /// <remarks>This constructor is private to prevent direct 
        /// instantiation. Use the provided factory methods or 
        /// properties to obtain a Logger instance.</remarks>
        private Logger()
        {
            // no default listeners – frameworket must be neutral
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds the specified trace listener to the collection of 
        /// listeners if it is not already present.
        /// </summary>
        /// <remarks>If the specified listener is already present in 
        /// the collection, this method has no effect.</remarks>
        /// <param name="listener">The trace listener to add to the 
        /// collection. Cannot be null.</param>
        public void AddListener(TraceListener listener)
        {
            if (!Trace.Listeners.Contains(listener))
            {
                Trace.Listeners.Add(listener);
            }
        }

        /// <summary>
        /// Removes the specified trace listener from the collection of 
        /// listeners for the trace output.
        /// </summary>
        /// <remarks>If the specified listener is not present in the 
        /// collection, no action is taken.</remarks>
        /// <param name="listener">The trace listener to remove from the 
        /// trace listeners collection. Cannot be null.</param>
        public void RemoveListener(TraceListener listener)
        {
            if (Trace.Listeners.Contains(listener))
            {
                Trace.Listeners.Remove(listener);
            }
        }

        /// <summary>
        /// Writes an informational message to the application's trace 
        /// listeners.
        /// </summary>
        /// <remarks>This method uses the application's configured trace 
        /// listeners to record informational messages. Use this method to 
        /// log general information about application execution, such as 
        /// status updates or non-critical events.</remarks>
        /// <param name="message">The informational message to log. This value
        /// can  be null or empty, in which case no message is written.</param>
        public void LogInfo(string message)
        {
            Trace.TraceInformation(message);
        }

        /// <summary>
        /// Writes a warning message to the trace listeners configured for
        /// the application.
        /// </summary>
        /// <remarks>Use this method to record warning information that may 
        /// indicate a potential problem or require attention. The message is 
        /// sent to all trace listeners; ensure that listeners are properly
        /// configured to capture or display warning output.</remarks>
        /// <param name="message">The warning message to log. This value 
        /// cannot be null.</param>
        public void LogWarning(string message)
        {
            Trace.TraceWarning(message);
        }

        /// <summary>
        /// Writes an error message to the trace listeners configured for 
        /// the application.
        /// </summary>
        /// <remarks>This method uses the application's trace infrastructure to
        /// record error messages. Ensure that trace listeners are properly 
        /// configured to capture or display error output as needed.</remarks>
        /// <param name="message">The error message to log. This value can be 
        /// null or empty, but such messages may not provide useful diagnostic 
        /// information.</param>
        public void LogError(string message)
        {
            Trace.TraceError(message);
        }
        #endregion
    }
}