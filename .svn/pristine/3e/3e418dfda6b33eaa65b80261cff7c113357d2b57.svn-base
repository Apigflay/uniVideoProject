using System.Collections.Generic;
using Common;
using DAL;
using Model;

namespace BLL
{
    public class LogBLL
    {
        /// <summary>
        /// 添加日志到错误日志表WebAPI调用
        /// </summary>
        /// <param name="remark"></param>
        /// <param name="message"></param>
        public static int AddErrorLog(ErrorLog log)
        {
            int totalCount = 0;

            log.ServerName = System.Net.Dns.GetHostName();
            log.UserIP = Tools.GetRealIP();
            log.DevType = Tools.GetdeviceName();
            log.ServerIp = UtilHelper.GetServerIP();
            log.FullPath = UtilHelper.GetFullURL();
            log.AgentName = UtilHelper.GetUserAgent();

            string CK = "Live_AddErrorlog";

            if (!log.FullPath.ToLower().Equals("live.9158.com/userinfo/getuserinfo"))
            {
                LogHelper.WriteLog(LogFile.Trace, log.Message);

                if (CacheHelper.Get(CK) == null)
                {
                    LogDAL.AddErrorLog(log, ref totalCount);
                    CacheHelper.SetCache(CK, 1, 1);
                }
            }
            return 1;
        }

        /// <summary>
        /// 日志列表 Admin用
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<ErrorLog> GetLogList(string date, int pageIndex, int pageSize, ref int count)
        {
            return LogDAL.getLogList(date, pageIndex, pageSize, ref count);
        }
    }
}
