using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace Common
{
    public class SocketService
    {

        //173.248.227.122  20000;
        private static int port = 4000;
        private static string host = "122.227.58.246";
        public static int laoWo_port = 20004;  //50004测试
        public static string laoWo_host = "173.248.226.26";
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ip"></param>
        /// <param name="_port"></param>
        public SocketService(string _ip, int _port)
        {
            try
            {
                //创建socket连接服务器
                _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                { SendTimeout = 1000 * 3 };
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


            //Console.WriteLine("发送消息中...");
            int ret = _clientSocket.Send(messdata, messdata.Length, SocketFlags.None);//发送消息
            //Console.WriteLine("消息发送成功");


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
            _clientSocket.Shutdown(SocketShutdown.Both);
            _clientSocket.Close();
            _clientSocket.Dispose();
        }
        public static int nServerPort = 8500;
        #region 辅助方法
        /// 将结构转换为字节数组
        /// 结构对象
        /// 字节数组；
        public byte[] StructToBytes(object obj)
        {
            //得到结构体的大小
            int size = Marshal.SizeOf(obj);
            //创建byte数组
            byte[] bytes = new byte[size];
            //分配结构体大小的内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将结构体拷到分配好的内存空间
            Marshal.StructureToPtr(obj, structPtr, false);
            //从内存空间拷到byte数组
            Marshal.Copy(structPtr, bytes, 0, size);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            //返回byte数组
            return bytes;
        }

        /// byte数组转结构
        /// 
        /// byte数组
        /// 结构类型
        /// 转换后的结构
        public object BytesToStruct(byte[] bytes, Type type)
        {
            //得到结构的大小
            int size = Marshal.SizeOf(type);
            // Log(size.ToString(), 1);
            //byte数组长度小于结构的大小
            if (size > bytes.Length)
            {
                //返回空
                return null;
            }
            //分配结构大小的内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将byte数组拷到分配好的内存空间
            Marshal.Copy(bytes, 0, structPtr, size);
            //将内存空间转换为目标结构
            object obj = Marshal.PtrToStructure(structPtr, type);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            //返回结构
            return obj;
        }

        public static List<byte> createhead(int nLen, int nCMD, int nZipLen)
        {
            var listbuff = new List<byte>();
            listbuff.AddRange(BitConverter.GetBytes((int)nLen));
            listbuff.AddRange(BitConverter.GetBytes((int)nCMD));
            listbuff.AddRange(BitConverter.GetBytes((int)nZipLen));

            return listbuff;

            // var messdata = new byte[listbuff.Count];
            //listbuff.CopyTo(messdata);
            // return messdata;
        }

        public static List<byte> createtgHTTPHEAD(int nCommad)
        {
            var listbuff = new List<byte>();
            listbuff.AddRange(BitConverter.GetBytes((int)nCommad));

            return listbuff;
        }

        public static List<byte> createtgProxyHead(int nLen, int dwIndex, int nparam, int nparam1, int nparam2)
        {
            var listbuff = new List<byte>();
            listbuff.AddRange(BitConverter.GetBytes((int)nLen));
            listbuff.AddRange(BitConverter.GetBytes((int)nparam));
            listbuff.AddRange(BitConverter.GetBytes((int)nparam1));
            listbuff.AddRange(BitConverter.GetBytes((int)dwIndex));
            listbuff.AddRange(BitConverter.GetBytes((int)nparam2));

            return listbuff;
        }

        #endregion

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="command">发送消息类型</param>
        ///// <param name="bytess"></param>
        ///// <returns></returns>
        //public int Send(int command, byte[] bytess, ref string recvStr)
        //{
        //    int len = bytess.Length;
        //    var listbuff = new List<byte>();
        //    listbuff.AddRange(BitConverter.GetBytes((int)command));//发送的命令
        //    listbuff.AddRange(BitConverter.GetBytes((int)len));//cmd len
        //    listbuff.AddRange(bytess);

        //    var messdata = new byte[listbuff.Count];
        //    listbuff.CopyTo(messdata);


        //    Console.WriteLine("发送消息中...");
        //    _clientSocket.Send(messdata, messdata.Length, SocketFlags.None);//发送消息
        //    Console.WriteLine("消息发送成功");


        //    int ret = _clientSocket.Receive(recvBytes, recvBytes.Length, 0);//服务器端返回的结果
        //    recvStr = Encoding.Default.GetString(recvBytes);//接受从服务器端返回的buffer转换成字符串类型

        //    //关闭连接
        //    CloseSocket();
        //    return ret;
        //}

      
        
    }
}
