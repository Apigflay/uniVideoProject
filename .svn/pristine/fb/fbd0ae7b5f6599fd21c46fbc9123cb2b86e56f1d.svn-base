using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAL
{
    /// <summary>
    /// 统计有关（其他数据库访问类）
    /// </summary>
    public class StatisticsDAL
    {
        private static string promo = DbHelper.conn63_MobileMiaobo_Promo;

        /// <summary>
        /// 记录播放次数
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="anchoridx"></param>
        /// <returns></returns>
        public static int PlayShare_Record(int useridx, int anchoridx, int roomid, string ip, string shareType)
        {
            int ret = 0;
            try
            {
                SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@roomid",SqlDbType.Int,20,roomid),
                                SqlHelper.MakeInParam("@fuseridx",SqlDbType.Int,20,useridx),
                                SqlHelper.MakeInParam("@anchoridx",SqlDbType.Int,20,anchoridx),
                                SqlHelper.MakeInParam("@ip",SqlDbType.VarChar,20,ip),
                                SqlHelper.MakeInParam("@shareType",SqlDbType.VarChar,20,shareType)
                                };
                ret = SqlHelper.ExecuteNonQuery(promo, CommandType.StoredProcedure, "Live_Record_Play", sp);
            }
            catch (Exception ex)
            {
                ret = -1;
                LogHelper.WriteLog(LogFile.SQL, "【辅助库查询失败】" + ex.Message);
            }
            return ret;
        }

        /// <summary>
        /// 热搜城市统计
        /// </summary>
        /// <param name="cityname"></param>
        /// <returns></returns>
        public static int HotCity_Statis(string cityName)
        {
            string sql = "Live_Hot_SearchCity_Statis";
            SqlParameter[] ps = { 
                                SqlHelper.MakeInParam("@cityName",SqlDbType.VarChar,20,cityName)
                                };
            return DbHelper.ExecuteNonQuery(sql, ps);

        }

        /// <summary>
        /// 接口访问统计
        /// </summary>
        /// <param name="contr"></param>
        /// <param name="rawurl"></param>
        /// <returns></returns>
        //public static int LiveApi_Statis_data(int type, string path, string rawurl, string referer)
        //{
        //    string sql = "AS_LiveApi_Save";

        //    SqlParameter[] sp = {
        //                        SqlHelper.MakeInParam("@type",SqlDbType.Int,4,type),
        //                        SqlHelper.MakeInParam("@Path",SqlDbType.VarChar,200,path),
        //                        SqlHelper.MakeInParam("@rawurl",SqlDbType.VarChar,200,rawurl),
        //                        SqlHelper.MakeInParam("@Referer",SqlDbType.VarChar,200,referer)
        //                        };
        //    return SqlHelper.ExecuteNonQuery(promo, CommandType.StoredProcedure, sql, sp);
        //}

        /// <summary>
        /// 游戏点击量统计
        /// </summary>
        /// <param name="gameid"></param>
        /// <returns></returns>
        public static int AccessGame_Statis_Data(int accessType, int gameid)
        {
            const string sql = "Live_AccessGame_Statis";
            SqlParameter[] ps = {
                                    SqlHelper.MakeInParam("@accessType",SqlDbType.Int,4,accessType),
                                    SqlHelper.MakeInParam("@gameid",SqlDbType.Int,4,gameid)
                                };
            return DbHelper.ExecuteNonQuery(sql, ps);
        }

        /// <summary>
        /// 版本检测更新统计
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="deviceType"></param>
        /// <param name="version"></param>
        /// <param name="userip"></param>
        /// <returns></returns>
        //public static int CheckUpdate_Statis_Data(int useridx, string deviceType, string version, string userip)
        //{
        //    const string sql = "Live_CheckUpdate_Statis";
        //    SqlParameter[] ps = {
        //                            SqlHelper.MakeInParam("@useridx",SqlDbType.Int,12,useridx),
        //                            SqlHelper.MakeInParam("@deviceType",SqlDbType.VarChar,10,deviceType),
        //                            SqlHelper.MakeInParam("@version",SqlDbType.VarChar,10,version),
        //                            SqlHelper.MakeInParam("@userip",SqlDbType.VarChar,30,userip),
        //                        };
        //    return SqlHelper.ExecuteNonQuery(promo, CommandType.StoredProcedure, sql, ps);
        //}

        /// <summary>
        /// 搜索关键词统计
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static int SearchKeyword_Statis_Data(string keyword, int haveReesult)
        {
            return 0;
            //const string sql = "Live_Insert_sKey";
            //SqlParameter[] ps = {
            //                        SqlHelper.MakeInParam("@keyword",SqlDbType.NVarChar,20,keyword),
            //                        SqlHelper.MakeInParam("@haveResult",SqlDbType.Int,4,haveReesult),
            //                    };
            //return DbHelper.ExecuteNonQuery(sql, ps);
        }
    }
}
