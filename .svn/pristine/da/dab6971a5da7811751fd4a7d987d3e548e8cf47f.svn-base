using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAL
{
    public class DbHelper
    {

        /// <summary>
        /// 默认连接63数据库
        /// </summary>
        /// <returns></returns>
        public static SqlConnection OpenConnection()
        {
            SqlConnection conn = new SqlConnection(conn63_MobileMiaobo);
            //SqlConnection conn = new SqlConnection(conn112_Mobile);
            conn.Open();
            return conn;
        }
        /// <summary>
        /// 获取Sql Server的连接数据库对象。SqlConnection 重装方法
        /// </summary>
        /// <param name="connstr">数据库连接字符串</param>
        /// <returns></returns>
        public static SqlConnection OpenConnection(string connstr)
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            return conn;
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        /// <param name="conn"></param>
        public static void CloseConnection(IDbConnection conn)
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
                conn.Dispose();
            }
        }


        #region 数据库连接查询字符串

        /// <summary>
        /// 测试库MobileTiange库
        /// </summary>
        public static string conn112_Mobile
        {
            get { return ConfigHelper.GetDbString("Miaobo_112"); }
        }

        /// <summary>
        /// 正式库63 MobileTiange库 喵播
        /// </summary>
        public static string conn63_MobileMiaobo
        {
            get { return ConfigHelper.GetDbString("Miaobo_63"); }
        }
        //public static string conn63_MobileMiaobo
        //{
        //    get { return ConfigHelper.GetDbString("Miaobo_112"); }
        //}

        /// <summary>
        /// 游戏库
        /// </summary>
        public static string lawoGame_124
        {
            get { return ConfigHelper.GetDbString("lawoGame_124"); }
        }
        /// <summary>
        /// 
        /// </summary>
        public static string conn63_MobileMiaobo_Promo
        {
            get { return ConfigHelper.GetDbString("Miaobo_63_Promo"); }
        }
        /// <summary>
        /// 70数据 主库
        /// </summary>
        public static string conn70
        {
            get { return ConfigHelper.GetDbString("Tiange_70"); }
        }

        /// <summary>
        /// 118房间数据库
        /// </summary>
        public static string conn118
        {
            get { return ConfigHelper.GetDbString("TenTiange_118"); }
        }

        /// <summary>
        /// 63IP地址数据库
        /// </summary>
        public static string conn63_IP
        {
            get { return ConfigHelper.GetDbString("IP"); }
        }
        /// <summary>
        /// 喵拍测试数据库
        /// </summary>
        public static string conn_MpTest
        {
            get { return ConfigHelper.GetDbString("aipaiDB_Test", false); }
        }
        /// <summary>
        /// 118 Tiange数据库
        /// </summary>
        //public static string conn118_tiange
        //{
        //    get { return ConfigHelper.GetDbString("Tiange_118"); }
        //}

        #endregion

        #region SqlHelper再封装

        /// <summary>
        /// 返回datatable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public static DataTable GetTable(string procName, SqlParameter[] sp)
        {
            return SqlHelper.ExecuteDataTable(conn63_MobileMiaobo, CommandType.StoredProcedure, procName, sp);
        }
        public static DataTable GetDataTable(string procName, SqlParameter[] sp)
        {
            return SqlHelper.ExecuteDataTable(conn63_MobileMiaobo, CommandType.StoredProcedure, procName, sp);
        }

        public static DataTable GetTable(string dbString, string sql, SqlParameter[] ps)
        {
            return SqlHelper.ExecuteDataTable(dbString, CommandType.StoredProcedure, sql, ps);
        }

        public static int ExecuteNonQuery(string procName, SqlParameter[] ps)
        {
            try
            {
                return SqlHelper.ExecuteNonQuery(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, procName, ps);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogFile.SQL, "【SQL_执行出错】过程：" + procName + ",msg:" + ex.Message);
                return -1;
            }
        }

        public static int ExecuteNonQuery(string dbString, string procName, SqlParameter[] sp)
        {
            return SqlHelper.ExecuteNonQuery(dbString, CommandType.StoredProcedure, procName, sp);
        }

        public static object ExecuteScalar(string procName, SqlParameter[] sp)
        {
            return SqlHelper.ExecuteScalar(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, procName, sp);
        }

        public static object ExecuteScalar(string dbString, string procName, SqlParameter[] sp)
        {
            return SqlHelper.ExecuteScalar(dbString, CommandType.StoredProcedure, procName, sp);
        }

        #endregion
    }

    /// <summary>
    /// 数据库操作接口
    /// </summary>
    public interface IDBContext
    {
        //string GetConnectionString();
        /// <summary>
        /// 对数据库进行赠送改查操作
        /// </summary>
        /// <param name="action">委托，为用户传入已打开SQLConnection对象，方法使用完毕后自动关闭数据库连接</param>
        void Write(Action<SqlConnection> action);

        /// <summary>
        /// ，方法可以直接返回执行结果
        /// </summary>
        /// <typeparam name="T">要返回的数据类型</typeparam>
        /// <param name="func"></param>
        /// <returns>返回结果</returns>
        T Write<T>(Func<SqlConnection, T> func);
    }

    /// <summary>
    /// 数据库操作类
    /// </summary>
    public class DBContext : IDBContext
    {
        public string WriteConnName { get; set; }

        /// <summary>
        /// 构造函数（默认连接）
        /// </summary>
        public DBContext()
        {
            WriteConnName = DbHelper.conn63_MobileMiaobo;
        }
        /// <summary>
        /// 构造函数重载
        /// </summary>
        /// <param name="conn">数据库连接字符串</param>
        public DBContext(string conn)
        {
            WriteConnName = conn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public void Write(Action<SqlConnection> action)
        {
            using (var conn = new SqlConnection(WriteConnName))
            {
                conn.Open();
                action(conn);
            }
        }

        public T Write<T>(Func<SqlConnection, T> func)
        {
            using (var conn = new SqlConnection(WriteConnName))
            {
                conn.Open();
                T value = func(conn);
                conn.Dispose();
                conn.Close();//关闭数据连接
                return value;
            }
        }
    }
}
