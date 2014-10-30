using Serilog;
using XSockets.Logger;

namespace QuintusDemo.Server
{
    /// <summary>
    /// Custom logging module
    /// </summary>
    public class MyLogger : XLogger
    {
        public MyLogger()
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Verbose().WriteTo.ColoredConsole().CreateLogger();
        }
    }
}