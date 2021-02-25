using Model;
using Common;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// 
    /// </summary>
    public class LogDAL
    {
        /// <summary>
        /// 错误日志记录
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public static int AddErrorLog(ErrorLog log, ref int totalCount)
        {
            string sql = "[Live_AddErrorLog]";
            SqlParameter[] ps = { 
                                SqlHelper.MakeInParam("@serverIp",SqlDbType.VarChar,50,log.ServerIp),
                                SqlHelper.MakeInParam("@userIP",SqlDbType.VarChar,50,log.UserIP),
                                SqlHelper.MakeInParam("@fullPath",SqlDbType.VarChar,200,log.FullPath),
                                SqlHelper.MakeInParam("@remark",SqlDbType.VarChar,500,log.Remark),
                                SqlHelper.MakeInParam("@message",SqlDbType.VarChar,250,log.Message),
                                SqlHelper.MakeInParam("@devType",SqlDbType.VarChar,20,log.DevType),
                                SqlHelper.MakeInParam("@agentName",SqlDbType.VarChar,250,log.AgentName),
                                SqlHelper.MakeInParam("@version",SqlDbType.VarChar,250,log.Version),
                                SqlHelper.MakeInParam("@serverName",SqlDbType.VarChar,250,log.ServerName),
                                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,20,log.useridx),
                                SqlHelper.MakeOutParam("@totalCount",SqlDbType.Int,4,0),
                                SqlHelper.MakeInParam("@stackTrace",SqlDbType.VarChar,100000,log.StackTrace),
                                };
            int ret = DbHelper.ExecuteNonQuery(sql, ps);
            //totalCount = (int)ps[10].Value;

            return ret;
        }
        /// <summary>
        /// 添加日志到错误日志表(DAL使用)
        /// </summary>
        /// <param name="serverIP"></param>
        /// <param name="fullPath"></param>
        /// <param name="remark"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        //public static int AddErrorLog(string remark, string message)
        //{
        //    var serverIP = UtilHelper.GetServerIP();
        //    var fullPath = UtilHelper.GetFullURL();
        //    var devType = Tools.GetAgentName();
        //    var agentName =UtilHelper.GetUserAgent();
        //    var userIP = Tools.GetRealIP();
        //    var serverName = System.Net.Dns.GetHostName();
        //    const string sql = "[Live_AddErrorLog]";

        //    return new DBContext().Write(c => c.Execute(sql, new
        //    {
        //        @serverIp = serverIP,
        //        @fullPath = fullPath,
        //        @remark = remark,
        //        @message = message,
        //        @userIP = userIP,
        //        @agentName = agentName,
        //        @devType = devType,
        //        @serverName = serverName
        //    }));
        //}

        /// <summary>
        /// 日志列表 Admin用
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<ErrorLog> getLogList(string date, int pageIndex, int pageSize, ref int count)
        {
            count = 0;
            List<ErrorLog> list = new List<ErrorLog>();
            var p = new DynamicParameters();
            p.Add("@date", date);
            p.Add("@pageIndex", pageIndex);
            p.Add("@pageSize", pageSize);
            p.Add("@pageCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

            list = new DBContext().Write(c => c.Query<ErrorLog>("[Live_LogList]", p, commandType: CommandType.StoredProcedure)
                    .ToList());
            count = p.Get<int>("@pageCount");
            return list;
        }
    }
}
