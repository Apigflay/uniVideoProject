using BLL.Mongo;
using Common;
using Common.Core;
using DAL;
using Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BLL
{
    public class RoomBLL
    {
        RoomDAL room = new RoomDAL();

        /// <summary>
        /// 导航菜单列表查询
        /// </summary>
        /// <returns></returns>
        public List<HotTab> Get_HotTab(int isEnglish)
        {
            string ip = Tools.GetRealIP();
            string areaid = AppDataBLL.GetAreaid;
            bool isBlackArea = CommonBLL.Instance.BlackAreaIPState(1, ip);

            string CK = "Get_Hot_Tab_" + isBlackArea + "_eng_" + isEnglish + "_" + areaid;

            List<HotTab> _tabList = CacheHelper.GetCache(CK) as List<HotTab>;

            if (_tabList == null || _tabList.Count==0)
            {
                _tabList = room.Get_HotTab_Data(areaid, isBlackArea ? 1 : 0);

                #region English Translate

                if (isEnglish == 1)
                {
                    foreach (var item in _tabList)
                    {
                        item.tabName = item.tabEnglishName;
                    }
                }

                #endregion
                
                CacheHelper.SetCache(CK, _tabList, 10);
            }
            return _tabList;
        }

        #region 主播列表

        /// <summary>
        /// 热门列表（陈春森那边调用 2016-5-12）
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static DataTable GetHotOnlineRoom(int page, int pagesize, ref int count)
        {
            return RoomDAL.GetHotOnlineRoom(page, pagesize, ref count);
        }

        /// <summary>
        /// 1:热门列表V1
        /// </summary>
        /// <returns></returns>
        public List<RoomOnline_V1> getHotRank(int ntype, int bundleid, int page, int pagesize, ref int counts)
        {
            return room.getHotRank_Data(ntype, bundleid, page, pagesize, ref counts);
        }

        /// <summary>
        /// 1、热门列表
        /// </summary>
        /// <param name="ntype">0：新版热门，1：热门，2：同城(已下架)，3：才艺，4：白名单，5：娱乐/游戏，8：海外，9：签约，10：客服，11：新秀</param>
        /// <param name="useridx"></param>
        /// <param name="page"></param>
        /// <param name="isNewApp"></param>
        /// <param name="isAudit">Android 审核状态</param>
        /// <param name="counts"></param>
        /// <param name="nbool"></param>
        /// <returns></returns>
        public List<RoomOnline_V1> GetHotRank_v2(int ntype, int useridx, int page, int isNewApp, int isAudit, ref int counts, ref int nbool)
        {
            var pagesize = 20;
            var province = "来自喵星";
            var city = "来自喵星";
            var ip = Tools.GetRealIP();
            string channel = AppDataBLL.AppChannelId;
            string areaid = AppDataBLL.GetAreaid;
            bool isBlackArea = CommonBLL.Instance.BlackAreaIPState(1, ip);
            bool isShenzhen = CommonBLL.Instance.BlackAreaIPState(4, ip);
            List<RoomOnline_V1> roomList = null;

            //新版本喵播热门标签 add 2017-12-15
            if (isNewApp == 1 && ntype == 1)
            {
                ntype = 0;
            }

            #region 黑名单地区(北京,美国)/Android审核状态

            if (isBlackArea || isAudit == 1 || useridx == 64214717 || (channel == "M00156" && isShenzhen))
            {
                //热门标签传入ntype = 4 进行查询
                if (ntype == 1 || ntype == 0) ntype = 4;

                //黑名单地区province 为 北京市
                province = "北京市";
            }

            #endregion

            string CK = "Live_Get_HotRoomList_" + ntype + "_" + page + "_" + isBlackArea + "_" + areaid;
            string CK_Total = "Live_Get_HotRoomListTotal_" + ntype + "_" + areaid;

            object obj1 = CacheHelper.Get(CK);
            object obj2 = CacheHelper.Get(CK_Total);

            if (obj1 == null || obj2 == null)
            {
                roomList = room.GetHotRank_Data_v2(int.Parse(areaid), ntype, 20, province, city, page, pagesize, ref counts, ref nbool).FindAll(f => f.nType == 0);
                CacheHelper.SetCacheDateTime(CK, roomList, 10);
                CacheHelper.SetCacheDateTime(CK_Total, counts, 10);
            }
            else
            {
                counts = (int)CacheHelper.GetCache(CK_Total);
                roomList = (List<RoomOnline_V1>)CacheHelper.GetCache(CK);
            }

            //互动区 当主播数小于4个的时候不给与显示 2017年11月2日
            if (ntype == 5 && counts < 4)
            {
                roomList = null;
            }
            return roomList;
        }

        /// <summary>
        /// 热门推荐2个主播
        /// </summary>
        /// <param name="isAudit"></param>
        /// <param name="counts"></param>
        /// <param name="nbool"></param>
        /// <returns></returns>
        public List<RoomOnline_V1> GetHotRecRank(ref int counts, ref int nbool)
        {
            string ip = Tools.GetRealIP();
            string areaid = AppDataBLL.GetAreaid;
            bool isBlackArea = CommonBLL.Instance.BlackAreaIPState(1, ip);
            List<RoomOnline_V1> roomList = null;

            if (AppDataBLL.AuditStatus == "0" || isBlackArea || areaid != "0")
            {
                roomList = null;
            }
            else
            {
                roomList = room.GetHotRank_Data_v2(0, 0, 20, "来自喵星", "来自喵星", 1, 2, ref counts, ref nbool).FindAll(f => f.nType == 1);
            }
            return roomList;
        }

        /// <summary>
        /// 2:最新主播列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<RoomOnline> GetNewOnlineRoom(int page, int pagesize, ref int count)
        {
            //int ntype = 0;//区分北京和非北京用户，如果北京用户直接返回空集合
            int areaid = int.Parse(AppDataBLL.GetAreaid);
            string ip = Tools.GetRealIP();
            string key = "Live_Get_NewRoomOnline_" + page + "_" + areaid;
            string keyTotal = "Live_Get_NewRoomOnlineTotal" + "_" + areaid;
            List<RoomOnline> roomList = null;

            if (CacheHelper.GetCache(key) == null || CacheHelper.GetCache(keyTotal) == null)
            {
                roomList = room.GetNewRoomOnline(page, pagesize, areaid, ref count);

                CacheHelper.SetCache(key, roomList, 1);
                CacheHelper.SetCache(keyTotal, count, 1);
            }
            else
            {
                count = (int)CacheHelper.GetCache(keyTotal);
                return (List<RoomOnline>)CacheHelper.GetCache(key);
            }
            return roomList;
        }

        /// <summary>
        /// 3：我的关注（在线的）
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<RoomOnline_V1> getMyOnlineFollowList(int useridx)
        {
            return room.GetMyOnlineFollowList_Data(useridx);
        }

        /// <summary>
        /// 附近
        /// </summary>
        /// <param name="_longitude"></param>
        /// <param name="_latitude"></param>
        /// <returns></returns>
        public List<RoomOnline_V1> Get_Anchor_Nearby(Paging page, double _longitude, double _latitude, ref int totalCount)
        {
            return room.Get_NearbyAnchor_Data(page, _longitude, _latitude, ref totalCount);
        }

        /// <summary>
        /// 随机推荐附近主播
        /// </summary>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public List<RandomRoomOnline> Get_Anchor_ByRandom(double lon, double lat)
        {
            string _country = "中国", _pro = "来自喵星", _city = "来自喵星";

            if (lon == 0.0 || lat == 0.0)
            {
                BaiduPosition loc = PositionHelper.GetBaiduLocation(Tools.GetRealIP());
                if (loc != null && loc.status == 0)
                {
                    _pro = loc.content.address_detail.province;
                    _city = loc.content.address_detail.city;
                }
            }
            else
            {
                GeoPosition p = PositionHelper.Get_GeoPosition(lon, lat);
                if (p.status == 0)
                {
                    _country = p.result.addressComponent.country;
                    _pro = (_country == "中国") ? p.result.addressComponent.province : "海外";
                    _city = p.result.addressComponent.city;
                }
            }

            List<RandomRoomOnline> randomList = room.Get_Anchor_ByRandom_Data(_pro, _city);

            foreach (var item in randomList)
            {
                if (item.latitude == 0 || item.longitude == 0)
                    item.distance = 0;
                else
                    item.distance = GeoLocationHelper.Distance(lon, lat, item.longitude, item.latitude) / 2;

                item.longitude = 0;
                item.latitude = 0;
            }
            //对距离进行排序
            randomList.Sort((x, y) => x.distance.CompareTo(y.distance));//升序
            //randomList.Sort((x, y) => y.distance.CompareTo(x.distance));//倒序

            return randomList;
        }

        /// <summary>
        /// 搜索主播（根据城市）v:105
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<RoomOnline> Get_Anchor_ByPosition(string condition, int page, int pageSize, ref int count)
        {
            if (Tools.numRegex.IsMatch(condition) || condition.Length < 2)
            {
                return null;
            }
            List<RoomOnline> list = room.Get_Anchor_ByPosition_Data(condition, page, pageSize, ref count);

            ///统计
            if (list != null && list.Count > 0 && page == 1)
            {
                StatisticsBLL.HotCity_Statis(list.First().position);
            }

            return list;
        }

        #endregion

        /// <summary>
        /// 我的直播间
        /// </summary>
        /// <param name="type"></param>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public MyLiveRoom GetMyLiveRoom(int type, int useridx)
        {
            MyLiveRoom myroom = room.GetMyLiveRoom(type, useridx);
            if (myroom != null && myroom.useridx > 0)
            {
                return myroom;
            }
            else
                return null;
        }

        /// <summary>
        /// 我的直播间的所有管理员
        /// </summary>
        /// <param name="type"></param>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<MyLiveRoom> GetMyRoomAdmin(int type, int useridx)
        {
            return room.GetMyRoomAdmin(3, useridx);
        }

        /// <summary>
        /// 获取在线主播用户信息
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public Room GetOnlineUserInfoByIdx(int useridx)
        {
            return room.GetOnlineUserInfoByIdx(useridx);
        }

        /// <summary>
        /// 获取热搜城市名称(缓存60分钟)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_HotCity()
        {
            var key = "Live_Get_HotCity";
            DataTable dt = null;

            if (CacheHelper.GetCache(key) == null)
            {
                dt = room.Get_HotCity_Data();

                CacheHelper.SetCache(key, dt, 60);
                return dt;
            }
            return (DataTable)CacheHelper.GetCache(key);
        }

        /// <summary>
        /// 获取充值代理房间
        /// </summary>
        /// <returns></returns>
        public List<Room> Get_AgentRoom()
        {
            return room.Get_AgentRoom_Data();
        }

        /// <summary>
        /// 连麦权限
        /// </summary>
        /// <param name="roomid"></param>
        /// <returns></returns>
        public int LianMaiStatus(int useridx)
        {
            return room.LianMaiStatus(useridx);
        }

        /// <summary>
        /// 获取推荐主播（新用户弹框推荐关注）
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="sex"></param>
        /// <returns></returns>
        public List<RoomOnline> Get_RecAnchorList(int dataType, int useridx, int sex)
        {
            int top = 6;
            int hour = DateTime.Now.Hour;

            Location loc = PositionHelper.GetLocationInfo(Tools.GetRealIP());

            //只要黑名单地区的都不下发数据
            if (AppDataBLL.PayblackAddress(loc.Province)
                || AppDataBLL.PayblackAddress(loc.City)
                || AppDataBLL.PayblackAddress(loc.Country)
                || loc.Province.Contains("广东"))
            {
                return null;
            }

            var recAnchorList = room.Get_RecAnchor_Data(dataType, sex, top);
            if (dataType == 2 && (hour >= 19 && hour <= 24))
            {
                recAnchorList = recAnchorList.OrderBy(f => f.isOnline).ToList();
            }
            return recAnchorList;
        }

        /// <summary>
        /// 获取用户直播总时长
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public double Get_UserLivingTime(int useridx)
        {
            return room.Get_UserLivingTime_Data(useridx);
        }

        #region C++服务端使用

        /// <summary>
        /// 金汉使用
        /// </summary>
        /// <param name="rtmp"></param>
        /// <returns></returns>
        public RoomOnlineUser GetRoomOnlineUserBy_rtmp(string rtmp)
        {
            return room.GetRoomOnlineUserBy_rtmp(rtmp);
        }

        /// <summary>
        /// 根据ServerType查询数据
        /// </summary>
        /// <param name="type">@Servertype 0 代理服务器  1登陆服务器 2房间服务器</param>
        /// <returns></returns>
        public List<ServerInfo> GetServerInfoByType(int serverType)
        {
            return room.GetServerInfoByType(serverType);
        }

        /// <summary>
        /// 获取房间服务Id
        /// </summary>
        /// <param name="uidx">用户Idx</param>
        /// <returns></returns>
        public Room GetServeridByUserIdx(int uidx, ref int isStar)
        {
            return room.GetServeridByUserIdx(uidx, ref isStar);
        }

        /// <summary>
        /// 获取房间服务Id
        /// </summary>
        /// <param name="uidx">用户Idx</param>
        /// <returns></returns>
        public Room GetServeridByUserIdx_PC(int uidx, ref int isStar)
        {
            return room.GetServeridByUserIdx_Data_PC(uidx, ref isStar);
        }

        #endregion

        /// <summary>
        /// 检测视频ID是否存在
        /// </summary>
        /// <param name="liveid"></param>
        /// <returns></returns>
        public string Check_LiveId(string liveid)
        {
            string CK = "Live_CheckLiveID_" + liveid;

            string data = (string)CacheHelper.Get(CK);
            if (data == null)
            {
                data = room.Check_LiveId_Data(liveid).ToString();

                CacheHelper.SetCache(CK, data);
            }

            return data;
        }
    }
}
