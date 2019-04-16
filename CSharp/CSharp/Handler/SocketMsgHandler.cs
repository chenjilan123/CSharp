using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CSharp.Handler
{
    /// <summary>
    /// 管理消息的接收, 发送, 编码, 通知
    /// </summary>
    public class SocketMsgHandler
    {
        private Encoding _encoding = Encoding.Unicode;
        private const string Receive = "Recv";
        private const string Send = "Send";

        #region Recv
        public void RecvMsg(Socket client)
        {
            var thread = new Thread(new ParameterizedThreadStart(ReceiveMessage));
            thread.IsBackground = true;
            thread.Start(client);
        }

        private void ReceiveMessage(object obj)
        {
            var client = obj as Socket;

            try
            {
                while (true)
                {
                    //也可以使用socket.Receive来接收数据。
                    using (var ns = new NetworkStream(client))
                    {
                        var dataSize = new byte[4];
                        ns.Read(dataSize, 0, dataSize.Length);
                        var size = BitConverter.ToInt32(dataSize, 0);

                        var dataMessage = new byte[size];
                        var dataLeft = size;
                        var start = 0;
                        while (dataLeft > 0)
                        {
                            var recv = ns.Read(dataMessage, start, dataLeft);
                            start += recv;
                            dataLeft -= recv;
                        }
                        //PrintMessage
                        Output(dataMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        #endregion

        #region Send
        public void SendMsg(Socket client)
        {
            try
            {
                while (true)
                {
                    Console.WriteLine($"Send: ");
                    var message = Console.ReadLine();
                    if (string.IsNullOrEmpty(message)) continue;
                    var dataMessage = _encoding.GetBytes(message);
                    var dataSize = BitConverter.GetBytes(dataMessage.Length);
                    var data = new byte[dataMessage.Length + dataSize.Length];
                    Array.Copy(dataSize, 0, data, 0, dataSize.Length);
                    Array.Copy(dataMessage, 0, data, 4, dataMessage.Length);

                    client.Send(data);

                    //PrintLog
                    PrintLog(Send, message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        #endregion

        #region Inform(消息通知, 可抽象出去)
        private void Output(byte[] dataMessage)
        {
            var message = _encoding.GetString(dataMessage);
            PrintLog(Receive, message);
        }
        #endregion

        #region Log
        private void PrintLog(string operate, string message)
        {
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} - {operate}: {message}");
        }
        #endregion
    }
}
