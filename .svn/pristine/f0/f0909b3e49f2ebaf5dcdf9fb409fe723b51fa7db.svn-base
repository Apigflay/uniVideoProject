using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace SocketCommon
{

    public class ServerHelper
    {
        #region 辅助方法
        /// 将结构转换为字节数组
        /// 结构对象
        /// 字节数组
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

        public static List<byte> createtgProxyHead(long nLen, long nRoomid, long dwIndex, long dwRemoveIndex, long dwUseridx)
        {
            var listbuff = new List<byte>();
            listbuff.AddRange(BitConverter.GetBytes((long)nLen));
            listbuff.AddRange(BitConverter.GetBytes((long)nRoomid));
            listbuff.AddRange(BitConverter.GetBytes((long)dwIndex));
            listbuff.AddRange(BitConverter.GetBytes((long)dwRemoveIndex));
            listbuff.AddRange(BitConverter.GetBytes((long)dwUseridx));



            return listbuff;
        }

        #endregion



        /// <summary>
        /// 用户币更新
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public static string NotifyGoldUpdate(int useridx, long Coin)
        {
            string ret = "fail";
            try
            {
                //SocketService bag = new SocketService("172.24.109.1", 2004);
                SocketService bag = new SocketService("173.248.227.122", 2004);
                //SocketService bag = new SocketService("122.227.23.109", 2004);
                var listbuff = new List<byte>();

                //包头tgProxyHead
                int nLength = 48;
                int nRoomid = 0;
                int dwIndex = 0;
                int dwRemoveIndex = 0;
                int dwUseridx = 0;
                listbuff.AddRange(BitConverter.GetBytes((int)nLength));
                listbuff.AddRange(BitConverter.GetBytes((int)nRoomid));
                listbuff.AddRange(BitConverter.GetBytes((int)dwIndex));
                listbuff.AddRange(BitConverter.GetBytes((int)dwRemoveIndex));
                listbuff.AddRange(BitConverter.GetBytes((int)dwUseridx));

                //包内容tgHead
                var nLen = 12 + 4 + 12;
                var tgHead = createhead(nLen, 1011, 0);
                listbuff.AddRange(tgHead);

                //包内容2
                var thHTTPhead = createtgHTTPHEAD(135);
                listbuff.AddRange(thHTTPhead);

                //包头tgHttpViceOwner
                listbuff.AddRange(BitConverter.GetBytes((int)useridx));
                listbuff.AddRange(BitConverter.GetBytes((Int64)Coin));


                //打包字符数组
                var messdata = new byte[listbuff.Count];
                listbuff.CopyTo(messdata);
                //发送
                ret = bag.Send(messdata);

            }
            catch
            {
                ret = "fail";
            }
            return ret;
        }

    }










}