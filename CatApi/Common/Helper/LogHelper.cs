/******************************************
* 文 件 名：LogHelper
* Copyright(c) live.9158.com
* 创 建 人：zhaorui
* 创建日期：2015年4月
* 文件描述：日志帮助类(独立)
******************************************
* 修 改 人：zhaorui
* 修改日期：2017年7月19日12:43:14
* 备注描述：优化逻辑，调整代码逻辑
*****************************************/

using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;
using Model;

namespace Common
{
    /// <summary>  
    /// 日志类型  
    /// </summary>  
    public enum LogFile
    {
        Debug = 1,
        Temp = 2,
        Log = 3,
        Test = 4,
        Data = 5,
        Game = 6,
        Error = 7,
        Warning = 8,
        Trace = 9,
        Info = 10,
        //Crash = 10,
        SQL = 11,
        IOSPay = 12,
        AliPay = 13,
        WXPay = 14
    }
    public static class LogHelper
    {
        private static object lockHelper = new object();
        private static string path = ConfigurationManager.AppSettings["LogPath"];
   //     private static MongoDBHelper mh = new MongoDBHelper();

        /// <summary>
        /// write log to file
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="msg">日志消息</param>
        /// <param name="isWriteToMongo">是否写入到mongodb</param>
        public static void WriteLog(LogFile logType, string msg)
        {
            //日志写入文件
            LogInfo(logType, msg);

            //日志写入到Mongodb数据库中
          //  WriteMongoLog(logType, msg);
        }

        /// <summary>
        /// 参数化写日志
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="msg"></param>
        /// <param name="arg"></param>
        public static void WriteLog(LogFile logType, string msg, params object[] arg)
        {
            StringBuilder sb = new StringBuilder(1024);
            sb.AppendFormat(msg, arg);

            //日志写入文件
            LogInfo(logType, sb.ToString());

            //日志写入到Mongodb数据库中
         //   WriteMongoLog(logType, sb.ToString());
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logFile">日志类型</param>
        /// <param name="deviceName"></param>
        /// <param name="userIP"></param>
        /// <param name="requestURL">请求的路径</param>
        /// <param name="msg">日志信息</param>
        public static void LogInfo(LogFile logFile, string msg)
        {
            if (path == null || path.Length == 0) { path = "C:\\LogFiles\\"; }

            string requestURL = RequestURL;
            string logPath = path + DateTime.Now.ToString("yyyy-MM-dd");
            string fileName = @"\\" + logFile.ToString() + ".txt";
            string filePath = logPath + fileName;

            StringBuilder sb = new StringBuilder(8192);
            //写入一些必要的错误信息。
            sb.AppendFormat("{0} | {1} | {2}\r\n", DateTime.Now.ToString("HH:mm:ss"), ClientIP, requestURL);
            sb.AppendFormat("{0}\r\n", msg);
            sb.Append("-------------------------------------------------------------\r\n");

            FileInfo fi = new FileInfo(filePath);
            try
            {
                if (!fi.Directory.Exists)
                {
                    fi.Directory.Create();
                }
                lock (lockHelper)
                {
                    using (StreamWriter writer = fi.AppendText())
                    {
                        writer.Write(sb);
                    }
                }
            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        /// 写Mongodb Log
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="msg"></param>
        /// <param name="userIP"></param>
        /// <param name="requestURL"></param>
        /// <param name="deviceName"></param>
        /// <param name="agentName"></param>
        private static void WriteMongoLog(LogFile logType, string msg)
        {
            try
            {
                string deviceName = Tools.GetdeviceName();

                WriteLog wlog = new WriteLog();
                wlog._id = Guid.NewGuid().ToString();
                wlog.DeviceType = deviceName;
                wlog.AgentName = AgentName;
                wlog.Message = msg;
                wlog.MsgType = logType.ToString();
                wlog.CreateTime = DateTime.Now;
                wlog.UserIP = ClientIP;
                wlog.HostPath = RequestURL;
                wlog.ServerName = System.Net.Dns.GetHostName();

                //写日志到Mongodb
                if (wlog != null)
                {
                  //  mh.Insert("live_log", wlog);
                }
            }
            catch (Exception ex)
            {
                LogInfo(LogFile.Warning, string.Format("MongoDb写日志失败{0},{1}", ex.Message, ex.StackTrace));
            }
        }

        #region  方法属性

        private static string RequestURL { get { return HttpContext.Current.Request.Path ?? string.Empty; } }
        private static string AgentName { get { return HttpContext.Current.Request.UserAgent ?? string.Empty; } }
        private static string ClientIP { get { return HttpContext.Current.Request.UserHostAddress ?? string.Empty; } }

        #endregion

    }
}
