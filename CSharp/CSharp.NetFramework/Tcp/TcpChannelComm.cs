using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using System.Security.Permissions;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using CSharp.Model;

namespace CSharp.NetFramework.Tcp
{
    public class TcpChannelComm
    {
        [SecurityPermission(SecurityAction.Demand)]
        public void StartServer()
        {
            var serverChannel = new TcpServerChannel(8080);
            ChannelServices.RegisterChannel(serverChannel);

            RemotingConfiguration.RegisterWellKnownServiceType(typeof(RemoteObject), "RemoteObject.rem", WellKnownObjectMode.Singleton);

            Console.WriteLine("------------------------------------------------------------------------------");
            Print("Channel Name", serverChannel.ChannelName);
            Print("Channel Priority", serverChannel.ChannelPriority.ToString());

            var data = (ChannelDataStore)serverChannel.ChannelData;
            foreach (var uri in data.ChannelUris)
            {
                Print("Channel Uri", uri);
            }
            var urls = serverChannel.GetUrlsForUri("Remotable.rem");
            var objectUrl = urls[0];
            string objectUri;
            var channelUri = serverChannel.Parse(objectUrl, out objectUri);
            Print("The object URL", objectUrl);
            Print("The object URI", objectUri);
            Print("The channel URI", channelUri);
            Console.WriteLine("------------------------------------------------------------------------------");
            
            Console.WriteLine("Server Listening...");
            Console.ReadLine();
        }

        [SecurityPermission(SecurityAction.Demand)]
        public void StartClient()
        {
            var clientChannel = new TcpChannel();
            ChannelServices.RegisterChannel(clientChannel);

            var remoteType = new WellKnownClientTypeEntry(typeof(RemoteObject), "tcp://localhost:8080/RemoteObject.rem");
            RemotingConfiguration.RegisterWellKnownClientType(remoteType);

            Console.WriteLine("-------------------------------------------------------------------------------------");
            string objectUri;
            IMessageSink messageSink = clientChannel.CreateMessageSink("tcp://localhost:8080/RemoteObject.rem", null, out objectUri);
            Print("The Uri of the message sink", objectUri);
            if (messageSink != null)
            {
                Print("The type of the message sink", messageSink.GetType().ToString());
            }
            Console.WriteLine("-------------------------------------------------------------------------------------");

            var service = new RemoteObject();
            Console.WriteLine("The Client is invoking the remote object");

            //调用该方法会阻塞, 等待远程调用的结果(不是客户端本地调用)。

            while (true)
            {
                Console.WriteLine($"The remote object has been called {service.GetCount()} times.");
                Thread.Sleep(500);
            }
        }

        private void Print(string key, string value)
        {
            var totalLength = (key.Length / 10 + 1) * 10;
            Console.WriteLine($"--{key.PadLeft(totalLength, ' ')}: {value}");
        }
    }
}
