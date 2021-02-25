using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace Common
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

        public static List<byte> createtgProxyHead(int nLen, int nRoomid, int dwIndex, int dwRemoveIndex, int dwUseridx)
        {
            var listbuff = new List<byte>();
            listbuff.AddRange(BitConverter.GetBytes((int)nLen));
            listbuff.AddRange(BitConverter.GetBytes((int)nRoomid));
            listbuff.AddRange(BitConverter.GetBytes((int)dwIndex));
            listbuff.AddRange(BitConverter.GetBytes((int)dwRemoveIndex));
            listbuff.AddRange(BitConverter.GetBytes((int)dwUseridx));

            return listbuff;
        }

        #endregion

        /// <summary>
        /// 封号通知
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public static string BlackUser(int useridx)
        {
            string ret = "fail";
            //Common.SocketService bag = null;
            try
            {  //173.248.226.26
                Common.SocketService bag = new Common.SocketService("173.248.226.26", 20004);
                //bag = new Common.SocketService("122.227.58.246", 2004);
                var listbuff = new List<byte>();

                //包头tgProxyHead
                int nLength = 40;
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
                var nLen = 12 + 4 + 4;
                var tgHead = createhead(nLen, 1011, 0);
                listbuff.AddRange(tgHead);

                //包内容2
                var thHTTPhead = createtgHTTPHEAD(103);
                listbuff.AddRange(thHTTPhead);

                //参数
                listbuff.AddRange(BitConverter.GetBytes((int)useridx));
                //打包字符数组
                var messdata = new byte[listbuff.Count];
                listbuff.CopyTo(messdata);
                //发送
                ret = bag.Send(messdata);

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogFile.Error, "【封号系统出错】" + ex.Message);
                ret = "fail";
            }
            //bag = null;
            return ret;
        }

        /// <summary>
        /// 红包奖励通知
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="cash"></param>
        /// <param name="Reason"></param>
        /// <param name="PhtotoUrl"></param>
        public static string InviteRewardNotice(int inviteid)
        {
            string ret = "fail";
            try
            {
                //正式122.227.58.246：2004，测试122.227.23.109：2004
                Common.SocketService bag = new Common.SocketService("122.227.58.246", 2004);
                var listbuff = new List<byte>();

                //包头tgProxyHead
                int nLength = 20 + 12 + 4 + 4;
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
                var nLen = 12 + 4 + 4;
                var tgHead = createhead(nLen, 1011, 0);
                listbuff.AddRange(tgHead);

                //包内容2
                var thHTTPhead = createtgHTTPHEAD(128);
                listbuff.AddRange(thHTTPhead);

                //inviteid
                listbuff.AddRange(BitConverter.GetBytes((int)inviteid));

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

            //LogHelper.WriteLog(LogFile.Test, string.Format("【邀请红包奖励结果】{0},{1}", inviteid, ret));
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">0:评论，1：点赞</param>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public static string SendMiaopaiNotice(int type, int useridx)
        {
            string ret = "fail";
            try
            {
                //正式122.227.58.246：2004，测试122.227.23.109：2004
                Common.SocketService bag = new Common.SocketService("122.227.58.246", 2004);
                var listbuff = new List<byte>();

                //包头tgProxyHead
                int nLength = 20 + 12 + 4 + 8;
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
                var nLen = 12 + 4 + 8;
                var tgHead = createhead(nLen, 1011, 0);
                listbuff.AddRange(tgHead);

                //包内容2
                var thHTTPhead = createtgHTTPHEAD(127);
                listbuff.AddRange(thHTTPhead);

                //useridx
                listbuff.AddRange(BitConverter.GetBytes((int)useridx));
                listbuff.AddRange(BitConverter.GetBytes((int)type));

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

            //LogHelper.WriteLog(LogFile.Test, string.Format("【邀请红包奖励结果】{0},{1}", inviteid, ret));
            return ret;
        }


        /// <summary>
        /// 绑定手机号成功后发送消息到服务端
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public static string BindPhoneSuccessNotice(int useridx)
        {
            string ret = "fail";
            //Common.SocketService bag = null;
            try
            {
                Common.SocketService bag = new Common.SocketService(SocketService.laoWo_host, SocketService.laoWo_port);
                // Common.SocketService bag = new Common.SocketService("122.227.58.246", 2004);
                //bag = new Common.SocketService("122.227.58.246", 2004);
                var listbuff = new List<byte>();

                //包头tgProxyHead
                int nLength = 40;
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
                var nLen = 12 + 4 + 4;
                var tgHead = createhead(nLen, 1011, 0);
                listbuff.AddRange(tgHead);

                //包内容2
                var thHTTPhead = createtgHTTPHEAD(132);
                listbuff.AddRange(thHTTPhead);

                //参数
                listbuff.AddRange(BitConverter.GetBytes((int)useridx));
                //打包字符数组
                var messdata = new byte[listbuff.Count];
                listbuff.CopyTo(messdata);
                //发送
                ret = bag.Send(messdata);

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogFile.Error, "【绑定手机号发送消息失败】" + ex.Message);
                ret = "fail";
            }
            //bag = null;
            return ret;
        }

        #region 老挝游戏开奖
        public static string HTTP_LOTTERY_REMIND(int roomId, int nGameType, int nGameID, string nStage, string szGameName, int timeNum)
        {
            string ret = "fail";
            int nCMD = 1011;
            try
            {
                Common.SocketService bag = new Common.SocketService(SocketService.laoWo_host, SocketService.laoWo_port);
                var listbuff = new List<byte>();
                //包头tgProxyHead
                int nProxyheadLength = 36 + 8 + 64 + 24 + 4;
                int nRoomid = roomId;
                int dwIndex = 0;
                int dwRemoveIndex = 0;
                int dwUseridx = 0;
                var tgProxyhead = createtgProxyHead(nProxyheadLength, nRoomid, dwIndex, dwRemoveIndex, dwUseridx);
                listbuff.AddRange(tgProxyhead);
                //包内容tgHead
                var nLen = 16 + 8 + 64 + 24 + 4;
                var tgHead = createhead(nLen, nCMD, 0);
                listbuff.AddRange(tgHead);

                //包内容2
                var thHTTPhead = createtgHTTPHEAD(133);
                listbuff.AddRange(thHTTPhead);





                //content：
                listbuff.AddRange(BitConverter.GetBytes((int)nGameType));
                listbuff.AddRange(BitConverter.GetBytes((int)nGameID));
                //listbuff.AddRange(BitConverter.GetBytes((int)nStage));



                //64长度内容
                byte[] pcont = new byte[64];
                byte[] pcontdata = UTF8Encoding.UTF8.GetBytes(szGameName);
                List<byte> lTemp = new List<byte>();
                lTemp.AddRange(pcontdata);
                lTemp.CopyTo(pcont);
                listbuff.AddRange(pcont);


                byte[] pcont2 = new byte[24];
                byte[] pcontdata2 = UTF8Encoding.UTF8.GetBytes(nStage);
                List<byte> lTemp2 = new List<byte>();
                lTemp2.AddRange(pcontdata2);
                lTemp2.CopyTo(pcont2);
                listbuff.AddRange(pcont2);

                listbuff.AddRange(BitConverter.GetBytes((int)timeNum));
                //打包字符数组
                var messdata = new byte[listbuff.Count];
                listbuff.CopyTo(messdata);
                //发送
                ret = bag.Send(messdata);

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogFile.Error, "【充值出错】" + ex.Message);
                ret = "fail";
            }
            //bag = null;
            return ret;
        }


        public static string HTTP_LOTTERY_INFO(int roomId, int nGameType, int nGameID, string nStage, string nOpenNum, string szGameName, int timeNum)
        {
            string ret = "fail";
            int nCMD = 1011;
            try
            {
                Common.SocketService bag = new Common.SocketService(SocketService.laoWo_host, SocketService.laoWo_port);
                var listbuff = new List<byte>();
                //包头tgProxyHead
                int nProxyheadLength = 36 + 8 + 64 + 24 + 24;
                int nRoomid = roomId;
                int dwIndex = 0;
                int dwRemoveIndex = 0;
                int dwUseridx = 0;
                var tgProxyhead = createtgProxyHead(nProxyheadLength, nRoomid, dwIndex, dwRemoveIndex, dwUseridx);
                listbuff.AddRange(tgProxyhead);
                //包内容tgHead
                var nLen = 16 + 8 + 64 + 24 + 24;
                var tgHead = createhead(nLen, nCMD, 0);
                listbuff.AddRange(tgHead);

                //包内容2
                var thHTTPhead = createtgHTTPHEAD(134);
                listbuff.AddRange(thHTTPhead);

                //content：
                listbuff.AddRange(BitConverter.GetBytes((int)nGameType));
                listbuff.AddRange(BitConverter.GetBytes((int)nGameID));

                //64长度内容
                byte[] pcont = new byte[64];
                byte[] pcontdata = UTF8Encoding.UTF8.GetBytes(szGameName);
                List<byte> lTemp = new List<byte>();
                lTemp.AddRange(pcontdata);
                lTemp.CopyTo(pcont);
                listbuff.AddRange(pcont);


                //64长度内容
                byte[] pcont2 = new byte[24];
                byte[] pcontdata2 = UTF8Encoding.UTF8.GetBytes(nStage);
                List<byte> lTemp2 = new List<byte>();
                lTemp2.AddRange(pcontdata2);
                lTemp2.CopyTo(pcont2);
                listbuff.AddRange(pcont2);

                byte[] pcont3 = new byte[24];
                byte[] pcontdata3 = UTF8Encoding.UTF8.GetBytes(nOpenNum);
                List<byte> lTemp3 = new List<byte>();
                lTemp3.AddRange(pcontdata3);
                lTemp3.CopyTo(pcont3);
                listbuff.AddRange(pcont3);

                // listbuff.AddRange(BitConverter.GetBytes((int)timeNum));
                ////128长度内容
                //byte[] pconts = new byte[128];
                //byte[] pcontdatas = UTF8Encoding.UTF8.GetBytes(szUrl);
                //List<byte> lTemp3 = new List<byte>();
                //lTemp2.AddRange(pcontdatas);
                //lTemp2.CopyTo(pconts);
                //listbuff.AddRange(pconts);



                //打包字符数组
                var messdata = new byte[listbuff.Count];
                listbuff.CopyTo(messdata);
                //发送
                ret = bag.Send(messdata);

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogFile.Error, "【充值出错】" + ex.Message);
                ret = "fail";
            }
            //bag = null;
            return ret;
        }

        /// <summary>
        ///投注信息转发
        /// </summary>
        /// <returns></returns>
        public static string HTTP_GAMEBETTING(string json)
        {
            string ret = "fail";
            int nCMD = 136;
            try
            {
                Common.SocketService bag = new Common.SocketService(SocketService.laoWo_host, SocketService.laoWo_port);
                var listbuff = new List<byte>();
                //将json 字符串转换为字节数组
                byte[] jsondata = UTF8Encoding.UTF8.GetBytes(json);

                //包头tgProxyHead 
                int nProxyheadLength = 32 + jsondata.Length; //数据长度 头20 +内容长度
                int nRoomid = 0;
                int dwIndex = 0;
                int dwRemoveIndex = 0;
                int dwUseridx = 0;
                var tgProxyhead = createtgProxyHead(nProxyheadLength, nRoomid, dwIndex, dwRemoveIndex, dwUseridx);
                listbuff.AddRange(tgProxyhead);


                var nLen = jsondata.Length;
                var tgHead = createhead(nLen, nCMD, 0);
                listbuff.AddRange(tgHead);

                //填充
                byte[] jsonByte = new byte[jsondata.Length];
                //byte[] jsondata = UTF8Encoding.UTF8.GetBytes(json);
                List<byte> lTemp3 = new List<byte>();
                lTemp3.AddRange(jsondata);
                lTemp3.CopyTo(jsonByte);
                listbuff.AddRange(jsonByte);

                //打包字符数组
                var messdata = new byte[listbuff.Count];
                listbuff.CopyTo(messdata);
                //发送
                ret = bag.Send(messdata);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogFile.Error, "【投注转发】" + ex.Message);
                ret = "fail";
            }
            //bag = null;
            return ret;

        }

        #endregion
        #region

        /// <summary>
        /// 头像审核通知
        /// </summary>
        /// <param name="succ">0:失败；1：成功</param>
        /// <param name="useridx"></param>
        /// <returns></returns>
        //public static string HeadExamineNotice(int useridx, int succ, string imgurl, string title)
        //{
        //    string ret = "fail";
        //    try
        //    {
        //        //Common.SocketService bag = new Common.SocketService("172.24.109.1", 2004);
        //        Common.SocketService bag = new Common.SocketService("122.227.58.246", 2004);
        //        var listbuff = new List<byte>();

        //        //包头tgProxyHead
        //        int nLength = 556;
        //        int nRoomid = 0;
        //        int dwIndex = 0;
        //        int dwRemoveIndex = 0;
        //        int dwUseridx = 0;
        //        listbuff.AddRange(BitConverter.GetBytes((int)nLength));
        //        listbuff.AddRange(BitConverter.GetBytes((int)nRoomid));
        //        listbuff.AddRange(BitConverter.GetBytes((int)dwIndex));
        //        listbuff.AddRange(BitConverter.GetBytes((int)dwRemoveIndex));
        //        listbuff.AddRange(BitConverter.GetBytes((int)dwUseridx));

        //        //包内容tgHead
        //        var nLen = 12 + 4 + 520;
        //        var tgHead = createhead(nLen, 1011, 0);
        //        listbuff.AddRange(tgHead);

        //        //包内容2
        //        var thHTTPhead = createtgHTTPHEAD(121);
        //        listbuff.AddRange(thHTTPhead);

        //        //包头tgHttpViceOwner
        //        listbuff.AddRange(BitConverter.GetBytes((int)useridx));
        //        listbuff.AddRange(BitConverter.GetBytes((int)succ));

        //        byte[] pHttp = new byte[256];
        //        byte[] pHttpdata = UTF8Encoding.UTF8.GetBytes(imgurl);
        //        List<byte> lTemp = new List<byte>();
        //        lTemp.AddRange(pHttpdata);
        //        lTemp.CopyTo(pHttp);

        //        listbuff.AddRange(pHttp);

        //        byte[] pcont = new byte[256];
        //        byte[] pcontdata = UTF8Encoding.UTF8.GetBytes(title);
        //        List<byte> lTemp2 = new List<byte>();
        //        lTemp2.AddRange(pcontdata);
        //        lTemp2.CopyTo(pcont);

        //        listbuff.AddRange(pcont);
        //        //打包字符数组
        //        var messdata = new byte[listbuff.Count];
        //        listbuff.CopyTo(messdata);
        //        //发送
        //        ret = bag.Send(messdata);

        //    }
        //    catch
        //    {
        //        ret = "fail";
        //    }
        //    return ret;
        //}


        /// <summary>
        /// 推送消息
        /// </summary>
        /// <param name="contents"></param>
        /// <param name="httpurl"></param>
        /// <param name="nMsgType"></param>
        /// <param name="nUseridx"></param>
        /// <param name="nRoomid"></param>
        /// <param name="nServerID"></param>
        /// <returns></returns>
        //public string PushMsg(string contents, string httpurl, int nPlatform = 0, int nMsgType = 0, int nUseridx = 0, int nRoomid = 0, int nServerID = 0)
        //{
        //    string ret = "fail";
        //    try
        //    {
        //        Common.SocketService bag = new Common.SocketService("122.227.23.109", 11000);
        //        var listbuff = new List<byte>();

        //        Encoding myEncoding = Encoding.GetEncoding("gb2312");
        //        string ss = contents;// "[你有1个待领取的大礼包] 恭喜你获得“购喵播靓号，免费得大礼包”的机会；靓号在手，搭讪不愁。猛戳领取>>";
        //        byte[] pContent = new byte[myEncoding.GetBytes(ss).Length + 1];
        //        pContent = myEncoding.GetBytes(ss);

        //        //包头


        //        var nLen = 12 + 276 + pContent.Length;
        //        var tgHead = createhead(nLen, 5001, 0);
        //        listbuff.AddRange(tgHead);



        //        //int nMsgType = 2;//0-启动app	1-跳转房间	2-打开h5

        //        //int nUseridx = 0;
        //        //int nRoomid = 0;
        //        //int nServerID = 0;
        //        //int nPlatform = 0;//0-ios 1-android 2-全推


        //        //包内容
        //        listbuff.AddRange(BitConverter.GetBytes((int)nMsgType));
        //        listbuff.AddRange(BitConverter.GetBytes((int)nUseridx));
        //        listbuff.AddRange(BitConverter.GetBytes((int)nRoomid));
        //        listbuff.AddRange(BitConverter.GetBytes((int)nServerID));
        //        listbuff.AddRange(BitConverter.GetBytes((int)nPlatform));

        //        string szHttp = httpurl;

        //        byte[] pHttp = new byte[256];

        //        byte[] pHttpdata = UTF8Encoding.UTF8.GetBytes(szHttp);

        //        List<byte> lTemp = new List<byte>();
        //        lTemp.AddRange(pHttpdata);
        //        lTemp.CopyTo(pHttp);

        //        // byte[] pContent = new byte[contents.Length + 1];
        //        //  pContent = UTF8Encoding.UTF8.GetBytes(contents);


        //        listbuff.AddRange(pHttp);
        //        listbuff.AddRange(pContent);


        //        //打包字符数组
        //        var messdata = new byte[listbuff.Count];
        //        listbuff.CopyTo(messdata);
        //        //发送
        //        ret = bag.Send(messdata);
        //        //  return ret;
        //    }
        //    catch
        //    {
        //        ret = "fail";
        //    }
        //    return ret;

        //}
        /// <summary>
        /// 购买短号成功通知
        /// </summary>
        /// <param name="longnum"></param>
        /// <param name="shortnum"></param>
        /// <returns></returns>
        //public static string ChangeLongNumToShortNum(int longnum, int shortnum)
        //{
        //    string ret = "fail";
        //    try
        //    {
        //        Common.SocketService bag = new Common.SocketService("122.227.58.246", 2004);
        //        var listbuff = new List<byte>();

        //        //包头tgProxyHead
        //        int nLength = 44;
        //        int nRoomid = 0;
        //        int dwIndex = 0;
        //        int dwRemoveIndex = 0;

        //        listbuff.AddRange(BitConverter.GetBytes((int)nLength));
        //        listbuff.AddRange(BitConverter.GetBytes((int)nRoomid));
        //        listbuff.AddRange(BitConverter.GetBytes((int)dwIndex));
        //        listbuff.AddRange(BitConverter.GetBytes((int)dwRemoveIndex));
        //        listbuff.AddRange(BitConverter.GetBytes((int)longnum));

        //        //包内容tgHead
        //        var nLen = 12 + 4 + 8;
        //        var tgHead = createhead(nLen, 1011, 0);
        //        listbuff.AddRange(tgHead);

        //        //包内容2
        //        var thHTTPhead = createtgHTTPHEAD(120);
        //        listbuff.AddRange(thHTTPhead);

        //        //包内容3
        //        listbuff.AddRange(BitConverter.GetBytes((int)longnum));
        //        listbuff.AddRange(BitConverter.GetBytes((int)shortnum));


        //        //打包字符数组
        //        var messdata = new byte[listbuff.Count];
        //        listbuff.CopyTo(messdata);
        //        //发送
        //        ret = bag.Send(messdata);

        //    }
        //    catch
        //    {
        //        ret = "fail";
        //    }
        //    return ret;
        //}
        /// <summary>
        /// 撤销场控通知
        /// </summary>
        /// <param name="roomid"></param>
        /// <param name="useridx"></param>
        /// <returns></returns>
        //public static string ViceOwnerDel(int roomid, int useridx)
        //{
        //    string ret = "fail";
        //    try
        //    {
        //        Common.SocketService bag = new Common.SocketService("122.227.58.246", 2004);
        //        //Common.SocketService bag = new Common.SocketService("192.168.81.210", 2004);
        //        var listbuff = new List<byte>();

        //        //包头tgProxyHead
        //        int nLength = 52;
        //        int nRoomid = 0;
        //        int dwIndex = 0;
        //        int dwRemoveIndex = 0;
        //        int dwUseridx = 0;
        //        listbuff.AddRange(BitConverter.GetBytes((int)nLength));
        //        listbuff.AddRange(BitConverter.GetBytes((int)nRoomid));
        //        listbuff.AddRange(BitConverter.GetBytes((int)dwIndex));
        //        listbuff.AddRange(BitConverter.GetBytes((int)dwRemoveIndex));
        //        listbuff.AddRange(BitConverter.GetBytes((int)dwUseridx));

        //        //包内容tgHead
        //        var nLen = 12 + 4 + 8 + 8;
        //        var tgHead = createhead(nLen, 1011, 0);
        //        listbuff.AddRange(tgHead);

        //        //包内容2
        //        var thHTTPhead = createtgHTTPHEAD(120);
        //        listbuff.AddRange(thHTTPhead);

        //        //包头tgHttpViceOwner
        //        listbuff.AddRange(BitConverter.GetBytes((int)roomid));
        //        listbuff.AddRange(BitConverter.GetBytes((int)roomid));
        //        listbuff.AddRange(BitConverter.GetBytes((int)useridx));
        //        listbuff.AddRange(BitConverter.GetBytes(0));
        //        //打包字符数组
        //        var messdata = new byte[listbuff.Count];
        //        listbuff.CopyTo(messdata);
        //        //发送
        //        ret = bag.Send(messdata);
        //    }
        //    catch
        //    {
        //        ret = "fail";
        //    }
        //    return ret;
        //}

        #endregion
    }
}