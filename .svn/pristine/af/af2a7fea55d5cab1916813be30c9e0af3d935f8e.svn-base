using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketCommon
{
    public class SocketService
    {  //测试50004 正式20004
        private static int port = 20004;
        private static string host = "173.248.226.26";
        private static byte[] recvBytes = new byte[1024];//接受从服务端返回的消息

        private static IPAddress ip = IPAddress.Parse(host);
        private static Socket _clientSocket;
        public SocketService()
        {
            try
            {
                //创建socket连接服务器
                _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp) { SendTimeout = 1000 * 3 };

                IPEndPoint iep = new IPEndPoint(ip, port);
                _clientSocket.Connect(iep);
            }
            catch (Exception ex)
            {
                _clientSocket.Dispose();
            }
        }
        public SocketService(string _ip, int _port)
        {
            try
            {
                //创建socket连接服务器
                _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp) { SendTimeout = 1000 * 3 };
                IPAddress ipa = IPAddress.Parse(_ip);
                IPEndPoint iep = new IPEndPoint(ipa, _port);
                _clientSocket.Connect(iep);
            }
            catch (Exception ex)
            {
                _clientSocket.Dispose();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command">发送消息类型</param>
        /// <param name="bytess"></param>
        /// <returns></returns>
        public string Send(byte[] bytess)
        {
            int len = bytess.Length;
            var listbuff = new List<byte>();
           // listbuff.AddRange(BitConverter.GetBytes((int)command));//发送的命令
           // listbuff.AddRange(BitConverter.GetBytes((int)len));//cmd len
            listbuff.AddRange(bytess);

            var messdata = new byte[listbuff.Count];
            listbuff.CopyTo(messdata);


            Console.WriteLine("发送消息中...");
           int ret =  _clientSocket.Send(messdata, messdata.Length, SocketFlags.None);//发送消息
            Console.WriteLine("消息发送成功");


            //int ret = _clientSocket.Receive(recvBytes, recvBytes.Length, 0);//服务器端返回的结果
            string recvStr = ret.ToString();//Encoding.Default.GetString(recvBytes);//接受从服务器端返回的buffer转换成字符串类型

            //关闭连接
            CloseSocket();
            return recvStr;
        }

        /// <summary>
        /// 关闭socket连接
        /// </summary>
        public void CloseSocket()
        {
            _clientSocket.Close();
            _clientSocket.Dispose();
        }


        
    }
}
