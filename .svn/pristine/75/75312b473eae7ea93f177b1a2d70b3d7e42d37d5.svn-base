using Common;
using System;
using System.Data;
using System.Text;
using System.Web;

namespace DAL
{
    public class NativeMethod
    {
        #region 单键
        private static NativeMethod _instance;

        static NativeMethod()
        {
            if (_instance == null)
                _instance = new NativeMethod();
        }

        public static NativeMethod Instance
        {
            get { return _instance; }
        }
        #endregion

        private static string path = HttpContext.Current.Server.MapPath("~/bin/LiveWebSelect.dll");
        private static byte[] ipArray = Encoding.ASCII.GetBytes("127.0.0.1");
        private static NativeInvoke ni = new NativeInvoke(path);
        /// <summary>
        /// 北京IP判断
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public int Get_IPInfo(string ip)
        {
            int flag = 0;
            try
            {
                byte[] bytes = Encoding.Default.GetBytes(ip);
                object[] obj = { ipArray, ipArray.Length, 2008, bytes, bytes.Length };

                flag = (int)ni.Invoke("GetBeijingIPInfo", obj, typeof(int));


                //DllInvoke di = new DllInvoke(path, "GetBeijingIPInfo");
                //flag = (int)di.Invoke(new object[] { ipArray, ipArray.Length, 2004, bytes, bytes.Length }, typeof(int));
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogFile.Error, "【IP判断调用数据库出错】flag:" + flag + ",IP:" + ip + ",Error:" + ex.Message);
                return -4;
            }
            return flag;
        }

        /// <summary>
        /// 获取我的关注（在线）
        /// </summary>
        /// <param name="fromUserIdx"></param>
        /// <param name="nType"></param>
        /// <returns></returns>
        //public static string GetMyFollow_Online(int fromUserIdx, int nType)
        //{
        //    int result = 0, memorySize = 0;
        //    string str = "-1";
        //    try
        //    {
        //        //DllInvoke di = new DllInvoke(HttpContext.Current.Server.MapPath("~/bin/LiveWebSelect.dll"), "WebToLiveSelectFriend");
        //        //ret = (int)ni.Invoke("WebToLiveSelectFriend", new object[] { ipArray, ipArray.Length, 2004, nType, fromUserIdx}, typeof(int));
        //        memorySize = (int)ni.Invoke("WebToLiveSelectFriend", new object[] { ipArray, ipArray.Length, 2004, nType, fromUserIdx }, typeof(int));
        //        if (memorySize > 0)
        //        {
        //            byte[] newByte = new byte[memorySize];
        //            result = (int)ni.Invoke("GetLiveSelectFriendInfo", new object[] { newByte, memorySize }, typeof(int));
        //            str = Encoding.Default.GetString(newByte);
        //        }
        //    }
        //    catch
        //    {
        //        return "[]";
        //    }
        //    return str;
        //}
    }
}
