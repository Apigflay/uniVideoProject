using System.Collections.Generic;
using DAL;
using Model;
using System.Data;
using Common;

namespace BLL
{
    public class LiveBLL
    {
        private LiveDAL live = new LiveDAL();

        #region 版本号操作
        public static string GetDownUrl(int id)
        {
            return LiveDAL.GetDownUrl(id);
        }

        /// <summary>
        /// 后台 版本操作Live_Version
        /// </summary>
        /// <param name="dataAction">操作类型</param>
        /// <param name="lv">版本号实体类</param>
        /// <returns></returns>
        public static int Version_Info_Save(int dataAction, LiveVersion lv)
        {
            return LiveDAL.Version_Info_Save_Data(dataAction, lv);
        }

        /// <summary>
        ///  获取版本号列表
        /// </summary>
        /// <param name="id">0：所有的</param>
        /// <returns></returns>
        public static List<LiveVersion> GetAppVer_List()
        {
            string key = "Live_Get_AllAppVersion";

            if (CacheHelper.GetCache(key) == null)
            {
                List<LiveVersion> list = LiveDAL.Get_AppVersion_Data(0);
                CacheHelper.SetCache(key, list, 10);
                return list;
            }
            return CacheHelper.GetCache(key) as List<LiveVersion>;
        }

        /// <summary>
        /// 获取单个版本号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static LiveVersion GetAppVer_ById(int id)
        {
            return GetAppVer_List().Find(f => f.id == id);
        }

        /// <summary>
        /// 当前包版本号是否是审核状态
        /// </summary>
        /// <param name="bundleid"></param>
        /// <param name="currentVersion"></param>
        /// <returns></returns>
        public static int AuditStatus(int bundleid, int appVersion)
        {
            LiveVersion lv = LiveBLL.GetAppVer_ById(bundleid);

            var user_ip = Tools.GetRealIP();
            //var channel = AppDataBLL.AppChannelId;

            int auditVersion = lv == null ? 100 : lv.auditVersion;//客户端的审核版本号
            int auditStatus = 1;//审核状态：1：正常，0：审核

            #region Android

            //if ((channel == "app store" || appVersion >= auditVersion))
            //{
            //    auditStatus = 0;
            //    return;
            //}

            #endregion

            #region IOS审核模式判断

            bool isAmericanArea = CommonBLL.Instance.BlackAreaIPState(3, user_ip);

            //data:0审核期间 data:1审核通过
            //if ((bundleid == 10 || bundleid == 11 || bundleid == 450) && appVersion >= auditVersion)
            if (appVersion >= auditVersion)
            {
                auditStatus = 0;
            }

            if (isAmericanArea || (appVersion == 330 && bundleid == 20))
            {
                auditStatus = 0;
            }

            #endregion

            return auditStatus;
        }

        #endregion

        #region App配置文件

        /// <summary>
        /// 获取所有配置文件(缓存5分钟)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<LiveConfig> Get_LiveConfigList(int id)
        {
            string key = "Live_GetConfigList_ById_" + id;
            List<LiveConfig> list = CacheHelper.GetCache(key) as List<LiveConfig>;
            if (list == null)
            {
                list = LiveDAL.Get_LiveConfig_Data(id);

                CacheHelper.SetCache(key, list, 5);
            }
            return list;
        }

        /// <summary>
        /// 根据Id查询具体的配置文件返回data
        /// </summary>
        /// <param name="id">(1~10，100以上数据类  11~99开关类)</param>
        /// <returns></returns>
        public static LiveConfig Get_LiveConfigById(int id)
        {
            LiveConfig lc = Get_LiveConfigList(id).Find(f => f.id == id);
            if (lc == null)
            {
                lc = new LiveConfig();
                lc.data = "100";
            }
            return lc;
        }

        /// <summary>
        /// 配置文件操作Lie_Config
        /// </summary>
        /// <param name="dataAction">1:添加 3:修改</param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static int Config_Info_Save(int dataAction, int id, string name, string data, string content)
        {
            return LiveDAL.AppConfig_Save_Data(dataAction, id, name, data, content);
        }

        #endregion

        #region 游戏
        /// <summary>
        /// 获取所有的游戏数据
        /// </summary>
        /// <param name="gameid">1:游戏中心 </param>
        /// <returns></returns>
        public List<GameConfig> GetAllGameInfo(int gameid)
        {
            string key = "Live_GameConfig_" + gameid;
            List<GameConfig> list = CacheHelper.GetCache(key) as List<GameConfig>;

            if (list == null)
            {
                list = live.GetAllGameConfig_Data(gameid);
                CacheHelper.SetCache(key, list, 10);
            }
            return list;
        }

        /// <summary>
        /// 游戏登陆保存token
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <param name="gameid"></param>
        //public static void gamelogin_save(int useridx, int gameid, string token)
        //{
        //    string userip = Tools.GetRealIP();
        //    string pwd = CryptoHelper.ToBase64(useridx + "&mbch*kof*lin79&" + token);

        //    LiveDAL.gamelogin_save(useridx, token, gameid, userip);
        //}
        public static void gamelogin_save(int useridx, string token)
        {
            string userip = Tools.GetRealIP();
            //string pwd = CryptoHelper.ToBase64(useridx + "&mbch*kof*lin79&" + token);

            LiveDAL.gamelogin_save(useridx, token);
        }

        /// <summary>
        /// 获取游戏房间配置
        /// </summary>
        /// <returns></returns>
        public List<GameRoomConfig> Get_GameRoom_List()
        {
            return live.GetAllGameRoom_Config_Data();
        }

        #endregion

        #region APP广告

        /// <summary>
        /// 广告位 缓存10分钟
        /// </summary>
        /// <returns></returns>
        public List<ADspotsRoom> GetList(int type)
        {
            string key = "live_ad" + type;
            List<ADspotsRoom> list = null;
            if (CacheHelper.GetCache(key) == null)
            {
                list = live.GetList(type);
                CacheHelper.SetCache(key, list, 10);
            }
            else
            {
                list = (List<ADspotsRoom>)CacheHelper.GetCache(key);
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="areaid"></param>
        /// <returns></returns>
        public List<ADsportFull> GetListByFull(int type, string areaid)
        {
            string CK = "live_ad" + type + areaid;
            List<ADsportFull> list = null;
            if (CacheHelper.GetCache(CK) == null)
            {
                list = live.GetListByFull(type, areaid);
                CacheHelper.SetCache(CK, list, 10);
            }
            else
            {
                list = (List<ADsportFull>)CacheHelper.GetCache(CK);
            }
            return list;
        }

        /// <summary>
        /// 热门列表广告位
        /// </summary>
        /// <returns></returns>
        public List<HotAds> Get_HotAdsList(int devtype)
        {
            return live.Get_HotAdsList_Data(devtype);
        }

        #endregion

        /// <summary>
        /// 查询百度推广热门主播
        /// </summary>
        /// <returns></returns>
        public static DataTable GetBaiduHotLive()
        {
            return LiveDAL.GetBaiduHotLive();
        }

        /// <summary>
        /// 热门时间列表
        /// </summary>
        /// <returns></returns>
        public static List<HotConfig> GetHotTimeConfigList()
        {
            return LiveDAL.GetHotTimeConfigList_Data();
        }

        /// <summary>
        /// 获取脸部贴纸文件，礼物动画文件，大礼物文件(缓存10分钟)
        /// </summary>
        /// <returns></returns>
        public static List<FaceStickers> Get_AllFaceStickers()
        {
            string key = "Live_Get_AllFaceStickers";
            List<FaceStickers> list = CacheHelper.GetCache(key) as List<FaceStickers>;

            if (list == null)
            {
                list = LiveDAL.Get_AllFaceStickers_Data();
                CacheHelper.SetCache(key, list, 10);
            }
            return list;
        }

        /// <summary>
        /// 获取客服信息
        /// </summary>
        /// <returns></returns>
        public static DataTable Get_Custom_Contact()
        {
            return LiveDAL.Get_Custom_Contact_Data();
        }

        /// <summary>
        /// 视频存储
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="flv"></param>
        /// <param name="storedURL"></param>
        /// <returns></returns>
        public static int StoredVideo_Save(int useridx, string flv, string storedURL)
        {
            string _hostName = Tools.GetRealIP();

            return LiveDAL.StoredVideo_Save_Data(useridx, flv, storedURL, _hostName);
        }

        public static int AbroadCity_Save(int cityCode, string cityEn, string param)
        {
            return LiveDAL.AbroadCity_Save_Data(cityCode, cityEn, param);
        }

        /// <summary>
        /// 验证码发送成功记录
        /// </summary>
        /// <param name="mr"></param>
        /// <returns></returns>
        public static int SendCode_Save(int action, string number, string ip, string devId, string code, string sendType, string sendMsg, int useridx)
        {
            return LiveDAL.GetCodeRecord_Data(action, number, ip, devId, code, sendType, sendMsg, useridx);
        }

        /// <summary>
        /// 测试账号查询
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public static int Get_TestAccount(int dataType, int useridx, string thirdAccount)
        {
            DataTable dt = LiveDAL.Get_TestAccount_Data(dataType, useridx, thirdAccount);
            if (dt != null && dt.Rows.Count > 0)
            {
                return 1;
            }
            else
                return 0;
        }

        /// <summary>
        /// 获取活动列表
        /// </summary>
        /// <returns></returns>
        public static List<ActiveModel> Get_ActiveList()
        {
            var CK = "CK_Get_Active_History";
            var historyActiveList = CacheHelper.GetCache(CK) as List<ActiveModel>;

            if (historyActiveList == null)
            {
                historyActiveList = LiveDAL.Get_ActiveList_Data();
                CacheHelper.SetCache(CK, historyActiveList, 10);
            }

            //审核模式下不下发数据
            if (AppDataBLL.AuditStatus == "0")
            {
                return null;
            }

            return historyActiveList;
        }

        /// <summary>
        /// 获取房间活动
        /// </summary>
        /// <param name="areaid"></param>
        /// <returns></returns>
        public static List<ActiveRoom> Get_ActiveRoomList(int roomid, int areaid)
        {
            //审核模式下不下发数据
            if (AppDataBLL.AuditStatus == "0")
            {
                return null;
            }
            return LiveDAL.Get_RoomActiveList_Data(roomid, areaid);
        }

        /// <summary>
        /// 反馈记录
        /// </summary>
        /// <param name="uidx"></param>
        /// <param name="content"></param>
        /// <param name="contact"></param>
        /// <returns></returns>
        public static int Insert_Feedback(int uidx, string content, string contact, string osVersion, string appversion, string deviceType)
        {
            return LiveDAL.Insert_Feedback_Data(uidx, content, contact, osVersion, appversion, deviceType);
        }

        /// <summary>
        /// 获取数据库时间
        /// </summary>
        /// <returns></returns>
        public static System.DateTime Get_ServerTime()
        {
            return LiveDAL.Get_ServerTime_Data();
        }

    }
}
