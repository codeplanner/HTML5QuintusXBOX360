using XSockets.Core.Common.Interceptor;
using XSockets.Core.Common.Utility.Logging;
using XSockets.Plugin.Framework;

namespace QuintusDemo.Server
{
    /// <summary>
    /// Custom module to display the data in/out of the server
    /// </summary>
    public class MyMessageInterceptor : IMessageInterceptor
    {
        public void OnIncomingMessage(XSockets.Core.Common.Protocol.IXSocketProtocol protocol, XSockets.Core.Common.Socket.Event.Interface.IMessage message)
        {
            Composable.GetExport<IXLogger>().Information("In {@m}",message);
        }

        public void OnOutgoingMessage(XSockets.Core.Common.Protocol.IXSocketProtocol protocol, XSockets.Core.Common.Socket.Event.Interface.IMessage message)
        {
            Composable.GetExport<IXLogger>().Information("Out {@m}", message);
        }
    }
}