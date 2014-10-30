using System;
using XSockets.Core.Common.Socket;

namespace QuintusDemo.Server
{
    /// <summary>
    /// Will only start a XSockets server
    /// 
    /// We do not need to write any server side code since the quintus demo uses the built-in "generic" controller of XSockets.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            using (var container = XSockets.Plugin.Framework.Composable.GetExport<IXSocketServerContainer>())
            {
                container.Start();
                Console.ReadLine();
            }
        }
    }
}
