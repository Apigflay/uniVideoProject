using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    public class RoomDAL
    {
        public DBContext db = new DBContext();

        /// <summary>
        /// 1:热门列表（陈春森那边调用 2016-5-12）
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static DataTable GetHotOnlineRoom(int page, int pagesize, ref int count)
        {
            count = 0;
            DataTable dt = null;
            SqlParameter[] p ={
                              SqlHelper.MakeInParam("@page",SqlDbType.Int,10,page),
                              SqlHelper.MakeInParam("@pagesize",SqlDbType.Int,10,pagesize),
                              SqlHelper.MakeOutParam("@count",SqlDbType.Int,10,count)
                          };
            dt = DbHelper.GetTable("[Live_Select_HotRank]", p);

            count = (int)p[2].Value;
            return dt;
        }

        /// <summary>
        /// 1:热门
        /// </summary>
        /// <returns></returns>
        public List<RoomOnline_V1> getHotRank_Data(int ntype, int bundleid, int page, int pagesize, ref int counts)
        {
            counts = 0;
            SqlParameter[] p ={
                                  SqlHelper.MakeInParam("@page",SqlDbType.Int,10,page),
                                  SqlHelper.MakeInParam("@pageSize",SqlDbType.Int,10,pagesize),
                                  SqlHelper.MakeOutParam("@counts",SqlDbType.Int,10,counts),
                                  SqlHelper.MakeInParam("@ntype",SqlDbType.Int,10,ntype),
                                  SqlHelper.MakeInParam("@bundleid",SqlDbType.Int,10,bundleid),
                          };
            DataTable dt = DbHelper.GetTable("[Live_Select_HotRank_New]", p);
            counts = (int)p[2].Value;
            return RFHelper<RoomOnline_V1>.ConvertToList(dt);
        }

        /// <summary>
        /// 1:热门V2
        /// </summary>
        /// <param name="ntype">1.all 2.all city 3.red songer</param>
        /// <param name="province"></param>
        /// <param name="city"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="counts"></param>
        /// <returns></returns>
        public List<RoomOnline_V1> GetHotRank_Data_v2(int areaid, int ntype, int bundleid, string province, string city, int page, int pagesize, ref int counts, ref int nbool)
        {
            counts = 0;
            nbool = 0;
            //string procName = areaid == 0 ? "[Live_Select_HotRank_V3]" : "Live_Select_HotRank_Tw_V2";
            string procName = "Live_Select_HotRank_V3";
            SqlParameter[] p ={
                              SqlHelper.MakeInParam("@ntype",SqlDbType.Int,10,ntype),
                              SqlHelper.MakeInParam("@param1",SqlDbType.VarChar,20,province),
                              SqlHelper.MakeInParam("@content",SqlDbType.VarChar,20,city),
                              SqlHelper.MakeInParam("@page",SqlDbType.Int,10,page),
                              SqlHelper.MakeInParam("@pageSize",SqlDbType.Int,10,pagesize),
                              SqlHelper.MakeOutParam("@counts",SqlDbType.Int,10,counts),
                              SqlHelper.MakeOutParam("@nBool",SqlDbType.Int,10,0),
                              SqlHelper.MakeInParam("@allroom",SqlDbType.Int,10,1),
                              //SqlHelper.MakeInParam("@bundleid",SqlDbType.Int,4,bundleid),
                              //SqlHelper.MakeInParam("@sex",SqlDbType.Int,4,sex),
                          };
            DataTable dt = DbHelper.GetTable(procName, p);
            counts = (int)p[5].Value;
            nbool = (int)p[6].Value;

            return RFHelper<RoomOnline_V1>.ConvertToList(dt);
        }
        /// <summary>
        /// 2:最新
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<RoomOnline> GetNewRoomOnline(int page, int pagesize, int areaid, ref int count)
        {
            string porcName = areaid == 0 ? "[Live_NewRoomOnline_v2]" : "[Live_NewRoomOnline_Tw]";
            count = 0;
            SqlParameter[] p ={
                              SqlHelper.MakeInParam("@PageIndex",SqlDbType.Int,10,page),
                              SqlHelper.MakeInParam("@pageSize",SqlDbType.Int,10,pagesize),
                              SqlHelper.MakeInParam("@sex",SqlDbType.Int,4,1),
                              SqlHelper.MakeOutParam("@count",SqlDbType.Int,10,count),
                          };
            DataTable dt = DbHelper.GetTable(porcName, p);
            count = (int)p[3].Value;
            return RFHelper<RoomOnline>.ConvertToList(dt);
        }

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
            DataTable dt = SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, "[Live_Select_Friend_v2]", p);
            return RFHelper<RoomOnline_V1>.ConvertToList(dt);
        }

        /// <summary>
        /// 我的直播间
        /// </summary>
        /// <param name="type">1：我的直播间，2：我担任管理的直播间</param>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public MyLiveRoom GetMyLiveRoom(int type, int useridx)
        {
            var sql = "f_MyLiveRoom_new";
            SqlParameter[] p ={
                              SqlHelper.MakeInParam("@Action",SqlDbType.Int,10,type),
                              SqlHelper.MakeInParam("@UserIdx",SqlDbType.Int,10,useridx)
                          };
            DataTable dt = SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, p);
            return RFHelper<MyLiveRoom>.GetEntity(dt);
        }

        /// <summary>
        /// 我的直播间的管理员
        /// </summary>
        /// <param name="type"></param>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<MyLiveRoom> GetMyRoomAdmin(int type, int useridx)
        {
            const string sql = "f_MyLiveRoom_new";
            var p = new DynamicParameters();
            p.Add("@Action", type);
            p.Add("@UserIdx", useridx);

            return db.Write(
                c => c.Query<MyLiveRoom>(sql, p, commandType: CommandType.StoredProcedure).ToList()
                );
        }

        /// <summary>
        /// 获取在线主播用户信息
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public Room GetOnlineUserInfoByIdx(int useridx)
        {
            var p = new DynamicParameters();
            p.Add("@userIdx", useridx);

            return db.Write(
                c => c.Query<Room>("live_GetOnlineUserInfoByIdx", p, commandType: CommandType.StoredProcedure).SingleOrDefault()
                );
        }

        /// <summary>
        /// 获取热搜城市
        /// </summary>
        /// <returns></returns>
        public DataTable Get_HotCity_Data()
        {
            const string sql = "[Live_Get_HotCity]";
            return SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, null);
        }

        /// <summary>
        /// 根据城市推荐主播(搜索页面) v:1.5.0
        /// </summary>
        /// <returns></returns>
        public List<RandomRoomOnline> Get_Anchor_ByRandom_Data(string province, string city)
        {
            const string sql = "[Live_GetAnchor_ByRandom_v2]";
            SqlParameter[] p =
            {
                SqlHelper.MakeInParam("@position",SqlDbType.VarChar,20,province)
            };
            DataTable dt = SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, p);

            return RFHelper<RandomRoomOnline>.ConvertToList(dt);
        }

        /// <summary>
        /// 搜索主播（根据城市）v:105
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<RoomOnline> Get_Anchor_ByPosition_Data(string condition, int page, int pageSize, ref int count)
        {
            count = 0;
            const string sql = "Live_Search_ByPosition";
            var p = new DynamicParameters();
            p.Add("@condition", condition);
            p.Add("@pageIndex", page);
            p.Add("@pageSize", pageSize);
            p.Add("@totalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

            List<RoomOnline> list = db.Write(c => c.Query<RoomOnline>(sql, p, commandType: CommandType.StoredProcedure).ToList());
            count = p.Get<int>("@totalCount");

            return list;
        }

        /// <summary>
        /// 大厅tab
        /// </summary>
        /// <returns></returns>
        public List<HotTab> Get_HotTab_Data(string areaid, int isBeijinIP)
        {
            string sql = areaid == "0" ? "Live_Get_HotTab" : "Live_Get_HotTab_TW";
            SqlParameter[] ps = {
                                SqlHelper.MakeInParam("@param1",SqlDbType.Int,4,isBeijinIP)
                                };
            DataTable dt = SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, ps);
            return RFHelper<HotTab>.ConvertToList(dt);
        }

        /// <summary>
        /// 附近列表
        /// </summary>
        /// <param name="_longitude"></param>
        /// <param name="_latitude"></param>
        /// <returns></returns>
        public List<RoomOnline_V1> Get_NearbyAnchor_Data(Paging page, double _longitude, double _latitude, ref int totalCount)
        {
            const string sql = "[Live_Get_Anchor_Nearby]";
            SqlParameter[] p =
            {
                SqlHelper.MakeInParam("@pageIndex",SqlDbType.Int,4,page.pageIndex),
                SqlHelper.MakeInParam("@pageSize",SqlDbType.Int,4,page.pageSize),
                SqlHelper.MakeInParam("@latitude",SqlDbType.Float,20,_latitude),
                SqlHelper.MakeInParam("@longitude",SqlDbType.Float,20,_longitude),
                SqlHelper.MakeOutParam("@totalCount",SqlDbType.Int,10,0)
            };
            DataTable dt = SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, p);

            totalCount = (int)p[4].Value;
            return RFHelper<RoomOnline_V1>.ConvertToList(dt);
        }

        /// <summary>
        /// 获取充值代理房间
        /// </summary>
        /// <returns></returns>
        public List<Room> Get_AgentRoom_Data()
        {
            string sql = "Live_Get_AgentRoom";
            SqlParameter[] ps = {
                                    SqlHelper.MakeInParam("@Action",SqlDbType.Int,4,1),
                                    SqlHelper.MakeInParam("@param1",SqlDbType.VarChar,20,"")
                                };
            DataTable dt = SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, ps);
            return RFHelper<Room>.ConvertToList(dt);
        }

        /// <summary>
        /// 连麦权限
        /// </summary>
        /// <param name="roomid"></param>
        /// <returns></returns>
        public int LianMaiStatus(int useridx)
        {
            int ret = 0;
            SqlParameter[] sp = { 
                                SqlHelper.MakeInParam("@roomid",SqlDbType.Int,10,useridx),
                                SqlHelper.MakeOutParam("@ret",SqlDbType.Int,4,0)
                                };
            SqlHelper.ExecuteNonQuery(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, "Live_LianMaiStatus", sp);
            ret = (int)sp[1].Value;

            return ret;
        }

        /// <summary>
        /// 获取推荐主播
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="sex"></param>
        /// <returns></returns>
        public List<RoomOnline> Get_RecAnchor_Data(int dataType, int sex, int top)
        {
            SqlParameter[] sp = { 
                                SqlHelper.MakeInParam("@dateAction",SqlDbType.Int,4,dataType),
                                SqlHelper.MakeInParam("@top",SqlDbType.Int,4,top),
                                SqlHelper.MakeInParam("@sex",SqlDbType.Int,4,sex)
                                };
            DataTable dt = DbHelper.GetTable("Live_Get_RecAnchor", sp);
            return RFHelper<RoomOnline>.ConvertToList(dt);
        }

        /// <summary>
        /// 获取用户直播总时长
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns>单位：分钟</returns>
        public double Get_UserLivingTime_Data(int useridx)
        {
            SqlParameter[] sp = { 
                                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,4,useridx),
                                SqlHelper.MakeOutParam("@alltime",SqlDbType.VarChar,20,""),
                                };
            DbHelper.ExecuteNonQuery("[f_GetStarPhoneOnline]", sp);

            string data = sp[1].Value.ToString();
            data = string.IsNullOrEmpty(data) ? "0" : data;

            return Convert.ToDouble(data);
        }

        #region 服务端

        /// <summary>
        /// 金汉使用
        /// </summary>
        /// <param name="rtmp"></param>
        /// <returns></returns>
        public RoomOnlineUser GetRoomOnlineUserBy_rtmp(string rtmp)
        {
            var sql = "live_GetRoomOnlieInfoBy_Rtmp";
            var p = new DynamicParameters();
            p.Add("@rtmp", rtmp);

            return db.Write(
                f => f.Query<RoomOnlineUser>(sql, p, commandType: CommandType.StoredProcedure).SingleOrDefault()
                );
        }

        /// <summary>
        /// 根据ServerType查询数据
        /// </summary>
        /// <param name="type">@Servertype 0 代理服务器  1登陆服务器 2房间服务器 3粉丝服务器</param>
        /// <returns></returns>
        public List<ServerInfo> GetServerInfoByType(int serverType)
        {
            const string sql = "f_GetLiveServerIpInfo";
            SqlParameter[] sqlParam = {
                SqlHelper.MakeInParam("@ServerType",serverType)
            };

            DataTable dt = DbHelper.GetDataTable(sql, sqlParam);
            return RFHelper<ServerInfo>.ConvertToList(dt);
            //return db.Write(c => c.Query<ServerInfo>(sql
            //        , new { @ServerType = serverType }
            //        , commandType: CommandType.StoredProcedure).ToList());
        }

        /// <summary>
        /// 获取房间服务Id
        /// </summary>
        /// <param name="uidx">用户Idx</param>
        /// <returns></returns>
        public Room GetServeridByUserIdx(int uidx, ref int isStar)
        {
            string sql = "Live_CheckRoomExist";
            isStar = 0;
            try
            {
                SqlParameter[] p ={
                              SqlHelper.MakeInParam("@UserIdx",SqlDbType.Int,10,uidx),
                              SqlHelper.MakeOutParam("@isStar",SqlDbType.Int,10,0)
                          };
                DataTable dt = SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, p);

                isStar = (int)p[1].Value;

                return RFHelper<Room>.GetEntity(dt);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取房间服务Id
        /// </summary>
        /// <param name="uidx">用户Idx</param>
        /// <returns></returns>
        public Room GetServeridByUserIdx_Data_PC(int uidx, ref int isStar)
        {
            string sql = "Live_CheckRoomExist_Pc";
            isStar = 0;
            try
            {
                SqlParameter[] p ={
                              SqlHelper.MakeInParam("@UserIdx",SqlDbType.Int,10,uidx),
                              SqlHelper.MakeOutParam("@isStar",SqlDbType.Int,10,0)
                          };
                DataTable dt = SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, p);

                isStar = (int)p[1].Value;

                return RFHelper<Room>.GetEntity(dt);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        /// <summary>
        /// 检测视频ID是否存在
        /// </summary>
        /// <param name="liveid"></param>
        /// <returns></returns>
        public int Check_LiveId_Data(string liveid)
        {
            SqlParameter[] sp = { 
                                SqlHelper.MakeInParam("@liveid",SqlDbType.VarChar,50,liveid),
                                };
            return (int)DbHelper.ExecuteScalar("Live_Check_Liveid", sp);
        }
    }
}
