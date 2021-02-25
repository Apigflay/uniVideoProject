using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Model;
namespace DAL
{
    /// <summary>
    /// 功能凌乱的文件放在这儿
    /// </summary>
    public class LiveDAL
    {
        private DBContext db = new DBContext();

        #region 版本号
        /// <summary>
        /// 获取下载地址
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public static string GetDownUrl(int id)
        {
            try
            {
                SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@id",SqlDbType.Int,10,id),
                                SqlHelper.MakeOutParam("@value",SqlDbType.NVarChar,100,0)
                                };
                SqlHelper.ExecuteNonQuery(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, "[AS_AppConfig_List]", sp);

                return sp[1].Value.ToString();
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 版本号查询
        /// </summary>
        /// <param name="id">0：查询所有的，</param>
        /// <returns></returns>
        public static List<LiveVersion> Get_AppVersion_Data(int id)
        {
            const string sql = "live_getVersion";
            SqlParameter[] p ={
                                SqlHelper.MakeInParam("@Id",SqlDbType.Int,10,id)
                        };
            DataTable dt = SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, p);
            return RFHelper<LiveVersion>.ConvertToList(dt);
        }

        /// <summary>
        /// 版本操作Live_Version
        /// </summary>
        /// <param name="dataAction">操作类型：1 add,2 delete 3 update 4 select</param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="version"></param>
        /// <param name="updateInfo"></param>
        /// <returns></returns>
        public static int Version_Info_Save_Data(int dataAction, LiveVersion lv)
        {
            const string sql = "CURD_LiveVersion";
            SqlParameter[] p = {
                                   SqlHelper.MakeInParam("@DataAction",SqlDbType.Int,10,dataAction),
                                   SqlHelper.MakeInParam("@type",SqlDbType.Int,10,lv.type),
                                   SqlHelper.MakeInParam("@id",SqlDbType.Int,10,lv.id),
                                   SqlHelper.MakeInParam("@isupdate",SqlDbType.Int,10,lv.isUpdate),
                                   SqlHelper.MakeInParam("@name",SqlDbType.VarChar,20,lv.name),
                                   SqlHelper.MakeInParam("@version",SqlDbType.VarChar,20,lv.version),
                                   SqlHelper.MakeInParam("@updateInfo",SqlDbType.NVarChar,200,lv.updateInfo),
                                   SqlHelper.MakeInParam("@updateURL",SqlDbType.VarChar,200,lv.updateURL),
                                   SqlHelper.MakeInParam("@auditVersion",SqlDbType.Int,4,lv.auditVersion),
                                   SqlHelper.MakeInParam("@bundleid",SqlDbType.VarChar,20,lv.Bundleid)
                               };

            return SqlHelper.ExecuteNonQuery(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, p);
        }

        #endregion

        #region 配置文件

        /// <summary>
        /// 获取配置文件列表
        /// </summary>
        /// <param name="id">0:返回所有的 >0：返回具体的</param>
        /// <returns></returns>
        public static List<LiveConfig> Get_LiveConfig_Data(int id)
        {
            const string sql = "AS_Config_List";
            SqlParameter[] p ={
                                SqlHelper.MakeInParam("@id",SqlDbType.Int,10,id)
                        };
            DataTable dt = SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, p);
            return RFHelper<LiveConfig>.ConvertToList(dt);
        }
        /// <summary>
        /// 根据Id查询具体的配置文件返回model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public static LiveConfig Get_LiveConfigById_Data(int id)
        //{
        //    const string sql = "[AS_Config_List]";
        //    SqlParameter[] ps = { 
        //                        SqlHelper.MakeInParam("@id",SqlDbType.Int,4,id)
        //                        };
        //    DataTable dt = SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, ps);
        //    return RFHelper<LiveConfig>.GetEntity(dt);
        //}
        /// <summary>
        /// 配置文件操作Lie_Config
        /// </summary>
        /// <param name="dataAction">1:添加 3:修改</param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static int AppConfig_Save_Data(int dataAction, int id, string name, string data, string content)
        {
            var p = new DynamicParameters();
            p.Add("@DataAction", dataAction);
            p.Add("@id", id);
            p.Add("@name", name);
            p.Add("@data", data);
            p.Add("@content", content);

            return new DBContext().Write(c => c.Execute("AS_LiveConfig_Save", p, commandType: CommandType.StoredProcedure));
        }
        #endregion

        #region 游戏
        /// <summary>
        /// 获取所有游戏配置文件
        /// </summary>
        /// <returns></returns>
        public List<GameConfig> GetAllGameConfig_Data(int gameid)
        {
            const string sql = "Live_Get_GameConfig";
            SqlParameter[] p = {
                                   SqlHelper.MakeInParam("id",SqlDbType.Int,10,gameid)
                               };

            DataTable dt = SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, p);
            return RFHelper<GameConfig>.ConvertToList(dt);
        }
        /// <summary>
        /// 获取游戏房间配置
        /// </summary>
        /// <returns></returns>
        public List<GameRoomConfig> GetAllGameRoom_Config_Data()
        {
            const string sql = "Live_Get_GameRoomConfig";

            DataTable dt = SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, null);
            return RFHelper<GameRoomConfig>.ConvertToList(dt);
        }
        /// <summary>
        /// 游戏登陆数据保存
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <param name="gameid"></param>
        public static void gamelogin_save(int user, string token)
        {
            SqlParameter[] p =
                {
                   SqlHelper.MakeInParam("@userIdx",SqlDbType.VarChar,20,user),
                   SqlHelper.MakeInParam("@token",SqlDbType.VarChar,100,token),
                //   SqlHelper.MakeInParam("@gameid",SqlDbType.Int,4,gameid),
                //   SqlHelper.MakeInParam("@userip",SqlDbType.VarChar,40,userip),
                };
            SqlHelper.ExecuteNonQuery(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, "[GameCenterTokenInsert]", p);
        }

        #endregion

        #region APP广告

        public List<ADspotsRoom> GetList(int state)
        {
            string sql = "Live_Select_ADspots";
            SqlParameter[] p ={
                                  SqlHelper.MakeInParam("@state",SqlDbType.Int,4,state)
                              };
            DataTable dt = SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, p);
            return RFHelper<ADspotsRoom>.ConvertToList(dt);
        }

        public List<ADsportFull> GetListByFull(int state, string areaid)
        {
            string sql = areaid == "1" ? "Live_Select_ADspots_Tw" : "Live_Select_ADspots";
            SqlParameter[] p ={
                                  SqlHelper.MakeInParam("@state",SqlDbType.Int,4,state)
                              };
            DataTable dt = DbHelper.GetDataTable(sql, p);
            return RFHelper<ADsportFull>.ConvertToList(dt);
        }

        /// <summary>
        /// 热门列表广告位
        /// </summary>
        /// <returns></returns>
        public List<HotAds> Get_HotAdsList_Data(int devtype)
        {
            string sql = "[Live_Get_HotAdsList]";
            SqlParameter[] p = {
                               SqlHelper.MakeInParam("@devtype",SqlDbType.Int,4,devtype)
                               };
            DataTable dt = DbHelper.GetDataTable(sql, p);

            //if (Common.Tools.IsCompanyIP)
            //{
            //    dt = DbHelper.GetTable(DbHelper.conn112_Mobile, sql, p);
            //}
            return RFHelper<HotAds>.ConvertToList(dt);
        }

        #endregion

        /// <summary>
        /// 查询百度推广热门主播
        /// </summary>
        /// <returns></returns>
        public static DataTable GetBaiduHotLive()
        {
            SqlParameter[] p =
                {
                   SqlHelper.MakeInParam("@page",SqlDbType.Int,4,1),
                   SqlHelper.MakeInParam("@pagesize",SqlDbType.Int,4,100),
                   SqlHelper.MakeOutParam("@counts",SqlDbType.Int,4)
                };
            return SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, "[Live_Select_HotRank_New]", p);
        }

        /// <summary>
        /// 热门时间列表配置
        /// </summary>
        /// <returns></returns>
        public static List<HotConfig> GetHotTimeConfigList_Data()
        {
            DataTable dt = DbHelper.GetTable("[Live_Get_HotLiveConfig]", null);
            return RFHelper<HotConfig>.ConvertToList(dt);
        }

        /// <summary>
        /// 获取脸部贴纸文件，礼物动画文件，大礼物文件
        /// </summary>
        /// <returns></returns>
        public static List<FaceStickers> Get_AllFaceStickers_Data()
        {
            DataTable dt = DbHelper.GetTable("Live_Get_FaceStickers", null);
            return RFHelper<FaceStickers>.ConvertToList(dt);
        }
        /// <summary>
        /// 获取客服信息
        /// </summary>
        /// <returns></returns>
        public static DataTable Get_Custom_Contact_Data()
        {
            return DbHelper.GetTable("Live_Get_Custom_Contact", null);
        }
        /// <summary>
        /// 视频存储地址保存
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="flv"></param>
        /// <param name="storedURL"></param>
        /// <returns></returns>
        public static int StoredVideo_Save_Data(int useridx, string flv, string storedURL, string machineName)
        {
            string sql = "live_StoredVideo_Save";
            SqlParameter[] sp ={
                              SqlHelper.MakeInParam("@useridx",SqlDbType.Int,20,useridx),
                              SqlHelper.MakeInParam("@flv",SqlDbType.VarChar,200,flv),
                              SqlHelper.MakeInParam("@storedURL",SqlDbType.VarChar,200,storedURL),
                              SqlHelper.MakeInParam("@machineName",SqlDbType.VarChar,30,machineName)
                              };
            return SqlHelper.ExecuteNonQuery(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, sp);
        }
        /// <summary>
        /// 海外地区统计
        /// </summary>
        /// <param name="cityCode"></param>
        /// <param name="cityEn"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int AbroadCity_Save_Data(int cityCode, string cityEn, string param)
        {
            string sql = "Live_AbroadCity_Save";
            SqlParameter[] sp ={
                              SqlHelper.MakeInParam("@cityCode",SqlDbType.Int,20,cityCode),
                              SqlHelper.MakeInParam("@cityEn",SqlDbType.VarChar,30,cityEn),
                              SqlHelper.MakeInParam("@param",SqlDbType.VarChar,30,param)
                              };
            return SqlHelper.ExecuteNonQuery(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, sp);
        }

        /// <summary>
        /// 发送验证码记录
        /// </summary>
        /// <param name="action"></param>
        /// <param name="number"></param>
        /// <param name="ip"></param>
        /// <param name="devId"></param>
        /// <param name="code"></param>
        /// <param name="sendType"></param>
        /// <param name="sendMsg"></param>
        /// <returns></returns>
        public static int GetCodeRecord_Data(int action, string number, string ip, string devId, string code, string sendType, string sendMsg, int useridx)
        {
            int ret = 0;
            const string sql = "[Live_PhoneCode_Save]";
            SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@Action",SqlDbType.Int,4,action),
                                SqlHelper.MakeInParam("@number",SqlDbType.VarChar,11,number),
                                SqlHelper.MakeInParam("@deviceid",SqlDbType.VarChar,100,devId),
                                SqlHelper.MakeInParam("@ip",SqlDbType.VarChar,20,ip),
                                SqlHelper.MakeInParam("@sendType",SqlDbType.VarChar,10,sendType),
                                SqlHelper.MakeInParam("@sendMsg",SqlDbType.VarChar,100,sendMsg),
                                SqlHelper.MakeInParam("@code",SqlDbType.VarChar,10,code),
                                SqlHelper.MakeOutParam("@ret",SqlDbType.Int,4,0),
                                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx),
                                };
            SqlHelper.ExecuteNonQuery(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, sp);
            ret = (int)sp[7].Value;
            return ret;
        }

        /// <summary>
        /// 测试账号查询
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public static DataTable Get_TestAccount_Data(int dataType, int useridx, string thirdAccount)
        {
            string sql = "Live_Get_TestAccount";
            SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@dataType",SqlDbType.Int,4,dataType),
                                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx),
                                SqlHelper.MakeInParam("@thirdAccount",SqlDbType.VarChar,50,thirdAccount)
                                };
            return SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, sp);
        }

        /// <summary>
        /// 获取活动列表
        /// </summary>
        /// <returns></returns>
        public static List<ActiveModel> Get_ActiveList_Data()
        {
            DataTable dt = DbHelper.GetTable("Live_Get_Active_History", null);

            return RFHelper<ActiveModel>.ConvertToList(dt);
        }

        /// <summary>
        /// 获取房间活动
        /// </summary>
        /// <param name="areaid"></param>
        /// <returns></returns>
        public static List<ActiveRoom> Get_RoomActiveList_Data(int roomid, int areaid)
        {
            SqlParameter[] sp = { 
                                    SqlHelper.MakeInParam("@roomid",SqlDbType.Int,4,roomid),
                                    SqlHelper.MakeInParam("@areaid",SqlDbType.Int,4,areaid)
                                };
            DataTable dt = DbHelper.GetTable("Live_Get_RoomActive", sp);

            return RFHelper<ActiveRoom>.ConvertToList(dt);
        }

        /// <summary>
        /// 反馈
        /// </summary>
        /// <param name="uidx"></param>
        /// <param name="content"></param>
        /// <param name="contact"></param>
        /// <returns></returns>
        public static int Insert_Feedback_Data(int uidx, string content, string contact, string osversion, string appversion, string device)
        {
            SqlParameter[] param = { 
                                   SqlHelper.MakeInParam("@uidx",uidx),
                                   SqlHelper.MakeInParam("@content",content),
                                   SqlHelper.MakeInParam("@contactWay",contact),
                                   SqlHelper.MakeInParam("@osVersion",osversion),
                                   SqlHelper.MakeInParam("@deviceType",device),
                                   SqlHelper.MakeInParam("@appVersion",appversion),
                                   };
            return DbHelper.ExecuteNonQuery("Live_Insert_feedback", param);
        }

        /// <summary>
        /// 获取数据库时间
        /// </summary>
        /// <returns></returns>
        public static System.DateTime Get_ServerTime_Data()
        {
            string sql = "GetDate";
            SqlParameter[] sp = { 
                                SqlHelper.MakeOutParam("@time",SqlDbType.DateTime,20,"")
                                };
            SqlHelper.ExecuteNonQuery(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, sp);
            return (System.DateTime)sp[0].Value;
        }
    }
}
