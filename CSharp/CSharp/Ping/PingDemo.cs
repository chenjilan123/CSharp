using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Linq;

namespace CSharp.Ping
{
    public class PingDemo
    {
        private const int SendTime = 5000;
        public void Start()
        {
            var hostInfo = GetHostInfo();
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp);
            socket.ReceiveTimeout = 1000;

            var ipAddress = hostInfo.AddressList.Where(ipAddr => ipAddr.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault();
            if (ipAddress == null)
            {
                Console.WriteLine("There is not any ipv4 address in the host!");
                return;
            }

            EndPoint hostEp = new IPEndPoint(ipAddress, 0);
            var clientInfo = hostInfo;
            EndPoint clientEp = new IPEndPoint(ipAddress, 0);

            var dataSize = 4;
            var packetSize = dataSize + 8;
            const int Icmp_echo = 8;
            var packet = new IcmPacket(Icmp_echo, 0, 0, 45, 0, dataSize);
            var buffer = new byte[packetSize];
            int index = packet.CountByte(buffer);
            if (index != packetSize)
            {
                Console.WriteLine("Reporter data error!");
                return;
            }
            var cksumBufferLength = (int)Math.Ceiling(((double)index) / 2);
            var cksumBuffer = new ushort[cksumBufferLength];
            var icmpHeaderBufferIndex = 0;
            for (int i = 0; i < cksumBufferLength; i++)
            {
                cksumBuffer[i] = BitConverter.ToUInt16(buffer, icmpHeaderBufferIndex);
                icmpHeaderBufferIndex += 2;
            }
            packet.CheckSum = IcmPacket.SumOfCheck(cksumBuffer);
            var sendData = new byte[packetSize];
            index = packet.CountByte(sendData);
            if (index != packetSize)
            {
                Console.WriteLine("Reporter data error!");
                return;
            }
            for (int i = 0; i < SendTime; i++)
            {
                var nBytes = 0;
                var startTime = Environment.TickCount;
                try
                {
                    nBytes = socket.SendTo(sendData, packetSize, SocketFlags.None, clientEp);
                }
                catch (Exception)
                {
                    Console.WriteLine("Unable to send reporter!");
                    return;
                }
                var receiveData = new byte[256];
                nBytes = 0;
                var timeConsume = 0;
                while(true)
                {
                    try
                    {
                        nBytes = socket.ReceiveFrom(receiveData, 256, SocketFlags.None, ref clientEp);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Timeout without response!");
                        return;
                    }
                    if (nBytes > 0)
                    {
                        timeConsume = Environment.TickCount - startTime;
                        if (timeConsume < 1)
                        {
                            Console.WriteLine($"Reply from: {ipAddress.ToString()} Send: {packetSize + 20}time<1ms; bytes received {nBytes}");
                        }
                        else
                        {
                            Console.WriteLine($"Reply from: {ipAddress.ToString()} Send: {packetSize + 20} in {timeConsume}ms; bytes received: {nBytes}");
                        }
                        break;
                    }
                }
            }
        }

        private IPHostEntry GetHostInfo()
        {
            IPHostEntry hostInfo = null;
            bool isHostRight = false;
            while (!isHostRight)
            {
                //Console.WriteLine("Please input the host address: ");
                var hostName = "127.0.0.1"; //Console.ReadLine();

                try
                {
                    hostInfo = Dns.GetHostEntry(hostName);
                    isHostRight = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Unrecognizable host name");
                }
            }

            return hostInfo;
        }
    }
}
