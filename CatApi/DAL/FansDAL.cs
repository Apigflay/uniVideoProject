using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;
using System.Web;
using Common;
using System.Data.SqlClient;

namespace DAL
{
    public class FansDAL
    {
        //private DBContext db = new DBContext();

        /// <summary>
        /// 3：关注（在线）
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<RoomOnline_V1> GetMyOnlineFollowList_Data(int useridx)
        {
            SqlParameter[] p ={
                              SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx)
                          };
            DataTable dt = DbHelper.GetTable("[Live_Select_Friend_v2]", p);
            return RFHelper<RoomOnline_V1>.ConvertToList(dt);
        }

        /// <summary>
        /// 我的所有关注
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="operid"></param>
        /// <returns></returns>
        public List<MyUserInfo> GetMyFriendList(int useridx, int operid, int page, int pagesize, ref int counts)
        {
            const string sql = "[Live_Select_MyFriendList_v2]";
            SqlParameter[] p ={
                              SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx),
                              SqlHelper.MakeInParam("@operid",SqlDbType.Int,10,operid),
                              SqlHelper.MakeInParam("@page",SqlDbType.Int,10,page),
                              SqlHelper.MakeInParam("@pagesize",SqlDbType.Int,10,pagesize),
                              SqlHelper.MakeOutParam("@counts",SqlDbType.Int,10,0)
                          };
            DataTable dt = SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, p);
            counts = (int)p[4].Value;
            return RFHelper<MyUserInfo>.ConvertToList(dt);
        }
        /// <summary>
        /// 我的所有粉丝 (2016-5-9 zhaorui)
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="operid"></param>
        /// <returns></returns>
        public List<MyFansInfo> GetMyFansList_New(int useridx, int operid, int page, int pagesize, ref int count)
        {
            const string sql = "[live_GetMyFansList_v2]";
            SqlParameter[] p ={
                              SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx),
                              SqlHelper.MakeInParam("@page",SqlDbType.Int,10,page),
                              SqlHelper.MakeInParam("@pagesize",SqlDbType.Int,10,pagesize),
                              SqlHelper.MakeOutParam("@count",SqlDbType.Int,10,0)
                          };
            DataTable dt = SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, p);
            count = (int)p[3].Value;
            return RFHelper<MyFansInfo>.ConvertToList(dt);
        }

        /// <summary>
        /// 关注/取消关注用户
        /// </summary>
        /// <param name="type">1:关注 2：取消关注</param>
        /// <param name="useridx"></param>
        /// <param name="fuseridx"></param>
        /// <returns></returns>
        public int SetFollowing(int type, int useridx, int fuseridx, string userip, string deviceid)
        {
            const string sql = "Live_Set_Following";
            int ret = 0;
            try
            {
                SqlParameter[] p ={
                              SqlHelper.MakeInParam("@type",SqlDbType.Int,10,type),
                              SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx),
                              SqlHelper.MakeInParam("@fuseridx",SqlDbType.Int,10,fuseridx),
                              SqlHelper.MakeOutParam("@ret",SqlDbType.Int,10,0),
                              SqlHelper.MakeInParam("@userip",SqlDbType.VarChar,50,userip),
                              SqlHelper.MakeInParam("@deviceid",SqlDbType.VarChar,100,deviceid)
                          };
                SqlHelper.ExecuteNonQuery(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, p);
                ret = (int)p[3].Value;
            }
            catch (Exception ex)
            {
                ret = -1;
                LogHelper.WriteLog(LogFile.Error, "【关注出错】type:" + type + ",fuseridx:" + fuseridx + ",useridx:" + useridx + ",msg:" + ex.Message);
            }
            return ret;
        }

        /// <summary>
        /// 是否关注
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="fuseridx"></param>
        /// <returns></returns>
        public int IsFollow_Data(int useridx, int fuseridx)
        {
            int ret = 0;
            SqlParameter[] sp = { 
                                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx),
                                SqlHelper.MakeInParam("@fuseridx",SqlDbType.Int,10,fuseridx),
                                SqlHelper.MakeOutParam("@ret",SqlDbType.Int,4,0)
                                };
            try
            {
                SqlHelper.ExecuteNonQuery(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, "Live_isFollow", sp);
                ret = (int)sp[2].Value;
            }
            catch
            {
                ret = -99;
            }
            return ret;
        }

        /// <summary>
        /// 获取关注数，粉丝数
        /// </summary>
        /// <param name="opertype">1:直接输出 2:返回表格</param>
        /// <param name="useridx"></param>
        /// <param name="followNum"></param>
        /// <param name="fansNum"></param>
        public void Get_FansInfo_Data(int opertype, int useridx, ref int followNum, ref int fansNum)
        {
            SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@operType",SqlDbType.Int,10,opertype),
                                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx),
                                SqlHelper.MakeOutParam("@followNum",SqlDbType.Int,10,0),
                                SqlHelper.MakeOutParam("@fansNum",SqlDbType.Int,4,0)
                                };
            DbHelper.ExecuteNonQuery("[Live_Get_FansInfo]", sp);
            
            followNum = (int)sp[2].Value;
            fansNum = (int)sp[3].Value;
        }
    }
}
