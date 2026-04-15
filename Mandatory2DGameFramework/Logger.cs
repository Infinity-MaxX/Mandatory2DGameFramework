using System.Diagnostics;

namespace Mandatory2DGameFramework.logging
{
    public sealed class Logger
    {
        private static readonly Lazy<Logger> _log = new Lazy<Logger>(() => new Logger());
        
        public static Logger Log => _log.Value;

        private Logger()
        {
            // Ingen default listeners – frameworket skal være neutralt
        }

        public void AddListener(TraceListener listener)
        {
            if (!Trace.Listeners.Contains(listener))
            {
                Trace.Listeners.Add(listener);
            }
        }

        public void RemoveListener(TraceListener listener)
        {
            if (Trace.Listeners.Contains(listener))
            {
                Trace.Listeners.Remove(listener);
            }
        }

        public void LogInfo(string message)
        {
            Trace.TraceInformation(message);
        }

        public void LogWarning(string message)
        {
            Trace.TraceWarning(message);
        }

        public void LogError(string message)
        {
            Trace.TraceError(message);
        }
    }
}