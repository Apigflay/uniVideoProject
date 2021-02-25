/******************************************
* 文 件 名：TimeHelper
* Copyright(c) live.9158.com
* 创 建 人：zhaorui
* 创建日期：2016年7月
* 文件描述：
******************************************
* 修 改 人：
* 修改日期：
* 备注描述：
*****************************************/
using System;
using System.Web;

namespace Common
{
    public class UtilHelper
    {
        public UtilHelper() { }

        #region URL有关
        /// <summary>
        /// eg: http://live.9158.com/about/test?param=test
        /// </summary>
        /// <returns></returns>
        public static string GetFullURL()
        {
            var context = HttpContext.Current;
            if (null != context)
            {
                return context.Request.Url.ToString().ToLower();
            }
            return "";
        }

        /// <summary>
        /// eg: live.9158.com/about/test
        /// </summary>
        /// <returns></returns>
        public static string GetHostAndPath()
        {
            return GetHost().ToLower() + GetPath();
        }

        /// <summary>
        /// eg： live.9158.com
        /// </summary>
        /// <returns></returns>
        public static string GetHost()
        {
            var context = HttpContext.Current;
            if (null != context)
            {
                return context.Request.Url.Host;
            }
            return "";
        }

        /// <summary>
        /// eg: /Home/Login
        /// </summary>
        /// <returns></returns>
        public static string GetPath()
        {
            var context = HttpContext.Current;
            if (null != context)
            {
                return context.Request.Path.ToLower();
            }
            return "";
        }
        /// <summary>
        ///eg: /about/test?param=test
        /// </summary>
        /// <returns></returns>
        //public static string GetRawURL()
        //{
        //    var context = HttpContext.Current;
        //    if (null != context)
        //    {
        //        return context.Request.RawUrl;
        //    }
        //    return "";
        //}

        /// <summary>
        /// eg: ?id=1
        /// </summary>
        /// <returns></returns>
        public static string GetQuery()
        {
            var context = HttpContext.Current;
            if (null != context)
            {
                return context.Request.Url.Query;
            }
            return "";
        }

        /// <summary>
        /// 获取用户代理
        /// </summary>
        /// <returns></returns>
        public static string GetUserAgent()
        {
            var context = HttpContext.Current;
            if (context == null)
            {
                return "未知";
            }
            return context.Request.UserAgent;
        }

        public static string GetReferer()
        {
            var url = "";
            var context = HttpContext.Current;
            if (context == null)
            {
                return "";
            }
            if (context.Request.UrlReferrer != null)
            {
                url = context.Request.UrlReferrer.AbsoluteUri;
            }
            return url;
        }

        #endregion

        #region 服务器相关
        /// <summary>
        /// //获取服务器IP和端口
        /// </summary>
        /// <returns></returns>
        public static string GetServerIP()
        {
            var context = HttpContext.Current;
            if (context == null)
            {
                return "";
            }
            var serverIP = context.Request.ServerVariables.Get("Local_Addr").ToString();
            var port = context.Request.ServerVariables.Get("Server_Port").ToString();

            return serverIP;
        }

        /// <summary>
        /// 获取AspNet 所占内存
        /// </summary>
        /// <returns></returns>
        public static string GetAspNetMemory()
        {
            string temp;
            try
            {
                temp = ((Double)System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / 1048576).ToString("N2") + "M";
            }
            catch
            {
                temp = "未知";
            }
            return temp;
        }

        /// <summary>
        /// 当前程序占用内存
        /// </summary>
        /// <returns></returns>
        public static string GetCurAPPMemory()
        {
            string temp;
            try
            {
                temp = ((Double)GC.GetTotalMemory(false) / 1048576).ToString("N2") + "M";
            }
            catch
            {
                temp = "未知";
            }
            return temp;
        }

        /// <summary>  
        /// 进程开始时间   
        /// </summary>  
        /// <returns></returns>  
        private string GetPrStart()
        {
            string temp;
            try
            {
                temp = System.Diagnostics.Process.GetCurrentProcess().StartTime.ToString();
            }
            catch
            {
                temp = "未知";
            }
            return temp;
        }

        /// <summary>  
        /// AspNet CPU时间  
        /// </summary>  
        /// <returns></returns>  
        private string GetAspNetCpu()
        {
            string temp;
            try
            {
                temp = ((TimeSpan)System.Diagnostics.Process.GetCurrentProcess().TotalProcessorTime).TotalSeconds.ToString("N0");
            }
            catch
            {
                temp = "未知";
            }
            return temp;
        }
        #endregion

    }
}
