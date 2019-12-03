using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CSharp.Http
{
    public class HttpProxyServer
    {
        Socket clientSocket;
        Byte[] read = new byte[1024];
        //定义一个空间，存储来自客户端请求数据包  
        Byte[] Buffer = null;
        Encoding ASCII = Encoding.ASCII;
        //设定编码  
        Byte[] RecvBytes = new Byte[4096];
        //定义一个空间，存储Web服务器返回的数据 

        public HttpProxyServer(Socket socket)
        {
            this.clientSocket = socket;
        }

        public void Run()
        {

            string clientmessage = " ";
            //存放来自客户端的HTTP请求字符串  
            string URL = " ";
            //存放解析出地址请求信息  
            int bytes = ReadMessage(read, ref clientSocket, ref clientmessage);
            if (bytes == 0)
            {
                return;
            }
            int index1 = clientmessage.IndexOf(' ');
            int index2 = clientmessage.IndexOf(' ', index1 + 1);
            if ((index1 == -1) || (index2 == -1))
            {
                throw new IOException();
            }
            string fullUrl1 = clientmessage.Substring(index1 + 1, index2 - index1);
            int index3 = fullUrl1.StartsWith("http://") ? fullUrl1.IndexOf('/', index1 + 8) : 0;
            int index4 = fullUrl1.IndexOf(' ', index1 + 8);
            int index5 = index4 - index3;

            //if (!fullUrl1.Contains("baidu")) return;
            //Console.WriteLine(fullUrl1);
            Console.WriteLine(clientmessage);
            //return;
            //URL = part1.Substring()
            //URL = fullUrl1.Substring(index1 + 4, (fullUrl1.Length - index5) - 8);
            URL = fullUrl1.StartsWith("http://") ? fullUrl1 : "http://" + fullUrl1;



            Console.WriteLine(URL);
            try
            {
                var uri = new Uri(URL);
                IPHostEntry IPHost = Dns.Resolve(uri.Host);
                Console.WriteLine("远程主机名： " + IPHost.HostName);
                string[] aliases = IPHost.Aliases;
                IPAddress[] address = IPHost.AddressList;
                Console.WriteLine("Web服务器IP地址：" + address[0]);
                //解析出要访问的服务器地址  
                IPEndPoint ipEndpoint = new IPEndPoint(address[0], 80);
                Socket IPsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //创建连接Web服务器端的Socket对象  

                Console.WriteLine($"开始连接到Http服务器: {ipEndpoint.ToString()}");
                IPsocket.Connect(ipEndpoint);
                Console.WriteLine($"连接到Http服务器成功: {ipEndpoint.ToString()}");
                //Socket连Web接服务器  
                if (IPsocket.Connected)
                    Console.WriteLine("Socket 正确连接！");
                string GET = clientmessage;
                Byte[] ByteGet = ASCII.GetBytes(GET);
                Console.WriteLine($"发送消息: {GET}");
                //IPsocket.Send(ByteGet, ByteGet.Length, 0);
                //代理访问软件对服务器端传送HTTP请求命令  
                Int32 rBytes = IPsocket.Receive(RecvBytes, RecvBytes.Length, 0);
                //代理访问软件接收来自Web服务器端的反馈信息  
                String strRetPage = string.Empty;
                while (rBytes > 0)
                {
                    Console.WriteLine("接收字节数：" + rBytes.ToString());
                    strRetPage += ASCII.GetString(RecvBytes, 0, rBytes);
                    Console.WriteLine(strRetPage);
                    rBytes = IPsocket.Receive(RecvBytes, RecvBytes.Length, 0);
                }
                //Console.WriteLine(strRetPage);
                IPsocket.Shutdown(SocketShutdown.Both);
                IPsocket.Close();
                SendMessage(clientSocket, strRetPage);
                //代理服务软件往客户端传送接收到的信息  
            }
            catch (Exception exc2)
            {
                Console.WriteLine(exc2.Message);
            }
        }
        //string clientmessage = " ";
        ////存放来自客户端的HTTP请求字符串  
        //string URL = " ";
        ////存放解析出地址请求信息  
        //int bytes = ReadMessage(read, ref clientSocket, ref clientmessage);
        //if (bytes == 0)
        //{
        //    return;
        //}
        //int index1 = clientmessage.IndexOf(' ');
        //int index2 = clientmessage.IndexOf(' ', index1 + 1);
        //if ((index1 == -1) || (index2 == -1))
        //{
        //    throw new IOException();
        //}
        //string part1 = clientmessage.Substring(index1 + 1, index2 - index1);
        //int index3 = part1.IndexOf('/', index1 + 8);
        //int index4 = part1.IndexOf(' ', index1 + 8);
        //int index5 = index4 - index3;
        //URL = part1.Substring(index1 + 4, (part1.Length - index5) - 8);
        //try
        //{
        //    IPHostEntry IPHost = Dns.Resolve(URL);
        //    Console.WriteLine("远程主机名： " + IPHost.HostName);
        //    string[] aliases = IPHost.Aliases;
        //    IPAddress[] address = IPHost.AddressList;
        //    Console.WriteLine("Web服务器IP地址：" + address[0]);
        //    //解析出要访问的服务器地址  
        //    IPEndPoint ipEndpoint = new IPEndPoint(address[0], 80);
        //    Socket IPsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //    //创建连接Web服务器端的Socket对象  
        //    IPsocket.Connect(ipEndpoint);
        //    //Socket连Web接服务器  
        //    if (IPsocket.Connected)
        //        Console.WriteLine("Socket 正确连接！");
        //    string GET = clientmessage;
        //    Byte[] ByteGet = ASCII.GetBytes(GET);
        //    IPsocket.Send(ByteGet, ByteGet.Length, 0);
        //    //代理访问软件对服务器端传送HTTP请求命令  
        //    Int32 rBytes = IPsocket.Receive(RecvBytes, RecvBytes.Length, 0);
        //    //代理访问软件接收来自Web服务器端的反馈信息  
        //    Console.WriteLine("接收字节数：" + rBytes.ToString());
        //    String strRetPage = string.Empty;
        //    var strRetPagestrRetPage = strRetPage + ASCII.GetString(RecvBytes, 0, rBytes);
        //    while (rBytes > 0)
        //    {
        //        rBytes = IPsocket.Receive(RecvBytes, RecvBytes.Length, 0);
        //        strRetPagestrRetPage = strRetPage + ASCII.GetString(RecvBytes, 0, rBytes);
        //    }
        //    IPsocket.Shutdown(SocketShutdown.Both);
        //    IPsocket.Close();
        //    SendMessage(clientSocket, strRetPage);
        //    //代理服务软件往客户端传送接收到的信息  
        //}
        //catch (Exception exc2)
        //{

        //}
        //接收客户端的HTTP请求数据
        private int ReadMessage(byte[] ByteArray, ref Socket s, ref String clientmessage)
        {
            int bytes = s.Receive(ByteArray, 1024, 0);
            string messagefromclient = Encoding.ASCII.GetString(ByteArray);
            clientmessage = (String)messagefromclient;
            return bytes;
        }
        //传送从Web服务器反馈的数据到客户端
        private void SendMessage(Socket s, string message)
        {
            Buffer = new Byte[message.Length + 1];
            int length = ASCII.GetBytes(message, 0, message.Length, Buffer, 0);
            Console.WriteLine("传送字节数：" + length.ToString());
            s.Send(Buffer, length, 0);
        }
    }
}
