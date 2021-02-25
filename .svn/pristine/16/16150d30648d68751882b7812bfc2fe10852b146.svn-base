using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace BLL
{
    public class RankBLL
    {
        RankDAL rank = new RankDAL();
        UserInfoDAL user = new UserInfoDAL();
        RoomDAL room = new RoomDAL();

        #region 粉丝奉献榜

        /// <summary>
        /// 我的守护位 缓存10分钟
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<RankInfo> Get_MyWard_SendGift(int useridx)
        {
            string key = "Live_MyWard_SendGift_" + useridx;
            List<RankInfo> list = null;
            if (null == CacheHelper.GetCache(key))
            {
                list = rank.Get_MyWard_SendGift_Data(useridx);
                CacheHelper.SetCache(key, list, 10);
                return list;
            }
            return CacheHelper.GetCache(key) as List<RankInfo>;
        }

        /// <summary>
        /// 粉丝奉献 1：周，2：月，3：总，6：日
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<RankInfo> Get_Rank_FansWardList(int timetype, int curuseridx, int useridx, Paging page, ref RankInfo myRank)
        {
            DateTime now = DateTime.Now;
            DateTime dt1 = DateTime.Now, dt2 = DateTime.Now;

            if (timetype == 1)
            {
                TimeHelper.GetTheWeekFirst_Last(now, ref dt1, ref dt2);
            }
            else if (timetype == 2)
            {
                TimeHelper.GetTheMonthFirst_Last(ref dt1, ref dt2);
            }
            else if (timetype == 6)
            {
                dt1 = Convert.ToDateTime(now.ToShortDateString());
                dt2 = dt1.AddDays(1);
            }
            else
            {
                dt1 = Convert.ToDateTime("2017-2-20");
            }

            string CK = "Cache_FansWard_Rank_" + timetype + "_" + useridx + "_" + page.pageIndex;

            //从缓存获取数据
            List<RankInfo> ranklist = CacheHelper.GetCache(CK) as List<RankInfo>;
            MyRankInfo info = new MyRankInfo();

            //走数据库查询
            if (ranklist == null || ranklist.Count <= 0)
            {
                ranklist = rank.Get_Rank_FansWard_Data(timetype, curuseridx, useridx, page, ref info, dt1, dt2);

                int time = timetype == 3 ? 60 * 12 : 10;
                CacheHelper.SetCache(CK, ranklist, time);
            }

            if (curuseridx.ToString().Length == 4) { curuseridx = user.GetUseridxByshortidx_Data(curuseridx); }

            myRank = ranklist.FirstOrDefault(f => f.useridx == curuseridx);
            if (myRank == null)
            {
                myRank = new RankInfo();

                myRank.smallpic = user.GetLiveUserInfoByIdx(curuseridx).smallpic;
            }
            return ranklist;
        }

        /// <summary>
        /// 分享周榜 已停用 2017-03-1
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<RankInfo> Get_Share_WeekRank(int curIdx, int useridx, ref MyRankInfo myrank)
        {
            string key = "User_Share_WeekRank_" + curIdx + "_" + useridx;
            string key2 = "User_Share_WeekRank_Rank_" + curIdx + "_" + useridx;
            List<RankInfo> list = null;

            if (CacheHelper.GetCache(key) == null || CacheHelper.GetCache(key2) == null)
            {
                list = rank.Get_Share_WeekRank_Data(curIdx, useridx, ref myrank);
                CacheHelper.SetCache(key, list, 10);
                CacheHelper.SetCache(key2, myrank, 10);
            }
            else
            {
                list = CacheHelper.GetCache(key) as List<RankInfo>;
                myrank = CacheHelper.GetCache(key2) as MyRankInfo;
            }
            return list;
        }

        /// <summary>
        /// 观看周榜 已停用 2017-03-1
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<RankInfo> Get_Watch_WeekRank(int _curIdx, int _useridx, ref MyRankInfo myrank)
        {
            string key = "User_Watch_WeekRank_" + _curIdx + "_" + _useridx;
            string key2 = "User_Watch_WeekRank_" + _curIdx + "_" + _useridx;
            List<RankInfo> list = CacheHelper.GetCache(key) as List<RankInfo>;
            myrank = CacheHelper.GetCache(key2) as MyRankInfo;

            if (list == null || myrank == null)
            {
                list = rank.Get_Watch_WeekRank_Data(_curIdx, _useridx, ref myrank);
                CacheHelper.SetCache(key, list, 10);
                CacheHelper.SetCache(key2, myrank, 10);
            }
            return list;
        }

        #endregion

        #region 总榜内排行榜

        /// <summary>
        /// 消费，主播，家族
        /// </summary>
        /// <param name="ranktype"></param>
        /// <param name="timetype"></param>
        /// <param name="stardate"></param>
        /// <param name="enddate"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="_totalCount"></param>
        /// <returns></returns>
        public List<SaleRankInfo> GetTotalRankList(int ranktype, int timetype, string stardate, string enddate, int page, int pagesize, ref int _totalCount)
        {
            var _rankList = new List<SaleRankInfo>();
            if (ranktype == 1)//消费榜
            {
                _rankList = GetSaleRankList(timetype, stardate, enddate, page, pagesize, ref _totalCount);
            }
            else if (ranktype == 2)//主播榜
            {
                _rankList = GetAnchorRankList(timetype, stardate, enddate, page, pagesize, ref _totalCount);
                foreach (var item in _rankList)
                {
                    Room r = room.GetOnlineUserInfoByIdx(item.useridx);
                    if (r != null && r.serverid > 0)
                    {
                        item.isOnline = 1;
                    }
                }
            }
            else if (ranktype == 3)//家族榜
            {
                _rankList = GetFamilyRankList(timetype, stardate, enddate, page, pagesize, ref _totalCount);
            }
            return _rankList;
        }

        /// <summary>
        /// 消费排行榜
        /// </summary>
        /// <param name="timetype">1：日榜，2：周榜，3：月榜 , 4：总榜</param>
        /// <param name="stardate"></param>
        /// <param name="enddate"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="counts"></param>
        /// <returns></returns>
        public List<SaleRankInfo> GetSaleRankList(int timetype, string stardate, string enddate, int page, int pagesize, ref int counts)
        {
            string CK = "Cache_Get_SaleRank_" + timetype + "_" + page;
            string CK_Total = "Cache_Get_SaleRank_Total_" + timetype;
            var top = 200;
            var rankList = CacheHelper.GetCache(CK) as List<SaleRankInfo>;

            if (rankList == null || CacheHelper.GetCache(CK_Total) == null)
            {
                rankList = rank.GetUserSaleRank_Data(timetype, top, stardate, enddate, page, pagesize, ref counts);
                int time = timetype == 4 ? 60 * 12 : 10;

                CacheHelper.SetCache(CK, rankList, time);
                CacheHelper.SetCache(CK_Total, counts, time);
            }
            else
            {
                counts = (int)CacheHelper.GetCache(CK_Total);
            }
            //日榜和昨日排行榜排名进行比较
            if (timetype == 1)
            {
                RankChange(top, rankList);
            }
            return rankList;
        }

        /// <summary>
        /// 今日和昨日排名变化比较
        /// </summary>
        /// <param name="top"></param>
        /// <param name="rankList"></param>
        private void RankChange(int top, List<SaleRankInfo> rankList)
        {
            List<SaleRankInfo> yesterDaylist = GetYesterdaySaleRank(top);

            foreach (var item in rankList)
            {
                SaleRankInfo myRank = yesterDaylist.Find(f => item.useridx == f.useridx);
                //如果今天的排名大于等于昨天的排名则上升(排名大则对应序列号小)
                if (myRank == null)
                {
                    item.rankChange = 1;
                }
                else if (myRank != null && item.pos <= myRank.pos)
                {
                    item.rankChange = 1;
                }
            }
        }

        /// <summary>
        /// 获取昨日消费排名用于和今日排名进行比较
        /// </summary>
        /// <returns></returns>
        public List<SaleRankInfo> GetYesterdaySaleRank(int top)
        {
            string key = "Cache_Get_YesterDayRank";
            string stardate = DateTime.Now.AddDays(-1).ToShortDateString();
            string enddate = DateTime.Now.ToShortDateString();
            int counts = 0;
            if (DateTime.Now.Hour == 24)
            {
                CacheHelper.Delete(key);
            }
            var yesterdayList = (List<SaleRankInfo>)CacheHelper.GetCache(key);
            if (CacheHelper.GetCache(key) == null)
            {
                yesterdayList = rank.GetUserSaleRank_Data(1, top, stardate, enddate, 1, top, ref counts);
                CacheHelper.SetCache(key, yesterdayList, 60 * 1);
            }

            return yesterdayList;
        }

        /// <summary>
        /// 主播排行榜
        /// </summary>
        /// <param name="timetype">2：周榜，3：月榜，4：总榜</param>
        /// <param name="stardate"></param>
        /// <param name="enddate"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="counts"></param>
        /// <returns></returns>
        public List<SaleRankInfo> GetAnchorRankList(int timetype, string stardate, string enddate, int page, int pagesize, ref int counts)
        {
            var CK = "live_AnchorRank_type_" + timetype + "_page_" + page;
            var CKTotal = "live_AnchorRank_type_" + timetype + "_Total_" + counts;
            var list = new List<SaleRankInfo>();

            if (CacheHelper.GetCache(CK) == null || CacheHelper.GetCache(CKTotal) == null)
            {
                list = rank.GetAnchorRank_Data(timetype, stardate, enddate, page, pagesize, ref counts);
                CacheHelper.SetCache(CK, list, 10);
                CacheHelper.SetCache(CKTotal, counts, 10);
            }
            else
            {
                list = (List<SaleRankInfo>)CacheHelper.GetCache(CK);
                counts = (int)CacheHelper.GetCache(CKTotal);
            }
            //if (page == 1)
            //{
            //    foreach (var item in list)
            //    {
            //        item.isFollow = new FansBLL().IsFollow(item.useridx, 29371898);
            //    }
            //}
            return list;
        }

        /// <summary>
        /// 家族排行榜
        /// </summary>
        /// <param name="timetype">2：周榜，3：月榜，4：总榜</param>
        /// <param name="stardate"></param>
        /// <param name="enddate"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="counts"></param>
        /// <returns></returns>
        public List<SaleRankInfo> GetFamilyRankList(int timetype, string stardate, string enddate, int page, int pagesize, ref int counts)
        {
            var key = "live_FaimilyRank_" + timetype + "_" + page;
            var keyTotal = "live_FaimilyRank_Total_" + timetype + "_" + counts;
            var list = new List<SaleRankInfo>();

            if (CacheHelper.GetCache(key) == null || CacheHelper.GetCache(keyTotal) == null)
            {
                list = rank.GetFamilyRank_Data(timetype, 200, stardate, enddate, page, pagesize, ref counts);
                CacheHelper.SetCache(key, list, 10);
                CacheHelper.SetCache(keyTotal, counts, 10);
            }
            else
            {
                list = (List<SaleRankInfo>)CacheHelper.GetCache(key);
                counts = (int)CacheHelper.GetCache(keyTotal);
            }
            return list;
        }

        #endregion

        #region 房间总消费排行榜

        /// <summary>
        /// 房间内总消费排行榜
        /// </summary>
        /// <param name="roomid"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public List<SaleRankInfo> GetRoomInSaleRank(int roomid, int page, int pagesize, ref int total)
        {
            var key = "live_RoomInAnchorRank_" + roomid + "_" + page;
            var keyTotal = "live_RoomInAnchorRank_Total_" + roomid;
            var list = new List<SaleRankInfo>();

            if (CacheHelper.GetCache(key) == null || CacheHelper.GetCache(keyTotal) == null)
            {
                list = rank.getInRoomSaleRank_Data(roomid, page, pagesize, ref total);
                CacheHelper.SetCache(key, list, 10);
                CacheHelper.SetCache(keyTotal, total, 10);
            }
            else
            {
                list = (List<SaleRankInfo>)CacheHelper.GetCache(key);
                total = (int)CacheHelper.GetCache(keyTotal);
            }
            return list;
        }

        #endregion

        #region 周星榜

        /// <summary>
        /// 获取上周周星
        /// </summary>
        /// <returns></returns>
        public List<WeekStarGift> GetLastWeekStarList()
        {
            string key = "Cache_Get_LastWeekList";
            //如果当前时间是周一4点则把缓存清掉
            var ltwkList = (List<WeekStarGift>)CacheHelper.GetCache(key);

            if (ltwkList == null)
            {
                ltwkList = rank.GetlastWeekStar_Data();
                CacheHelper.SetCache(key, ltwkList, 60);
            }
            return ltwkList;
        }

        /// <summary>
        /// 获取本周周星
        /// </summary>
        /// <param name="giftid"></param>
        /// <param name="page"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public List<WeekStarGift> GetthisWeekStarList(int giftid, Paging page)
        {
            string key = "Cache_Get_CurWeekStar_" + giftid;
            int top = 20;
            DateTime dt1 = DateTime.Now, dt2 = DateTime.Now;

            TimeHelper.GetTheWeekFirst_Last(DateTime.Now, ref dt1, ref dt2);

            List<WeekStarGift> tsWkList = (List<WeekStarGift>)CacheHelper.GetCache(key);

            if (tsWkList == null || tsWkList.Count <= 0)
            {
                tsWkList = rank.GettsWkStar_Data(top, giftid, page, dt1, dt2);

                CacheHelper.SetCache(key, tsWkList, 10);
            }

            foreach (var item in tsWkList)
            {
                Room r = room.GetOnlineUserInfoByIdx(item.useridx);
                if (r != null && r.roomid > 0)
                {
                    item.isOnline = 1;
                }
            }
            return tsWkList;
        }

        #endregion

        #region 主播守护

        /// <summary>
        /// 获取守护列表购买商品
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<GuardGoods> Get_GoodsPriceList()
        {
            return rank.Get_GoodsPriceList_Data();
        }

        /// <summary>
        /// 我的守护排行榜(喵币购买的)
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<RankInfo> Get_MyWard(int useridx, int top, ref int totalCount)
        {
            return rank.Get_MyGuard_Data(useridx, top, ref totalCount);
        }

        /// <summary>
        /// 守护购买
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="toUseridx"></param>
        /// <param name="roomid"></param>
        /// <param name="goodsId"></param>
        /// <param name="userip"></param>
        /// <returns></returns>
        public int Purchase_Guard(int useridx, int toUseridx, int roomid, int goodsId, string userip)
        {

            return rank.Purchase_Guard_Data(useridx, toUseridx, roomid, goodsId, userip);
        }
            /// <summary>
        /// 大神榜
        /// </summary>
        /// <param name="timeType">0 最高派彩 1 连红 2 胜率</param>
        /// <param name="type">0 本期 1往期</param>
        /// <returns></returns>
        public List<Rank_v2> getUserGameRank01(int rankType, int timeType, int type)
        {
            if (type == 0)
            {
                return giftRankSort(rank.getUserGameRank01(timeType, 0, type), rankType, timeType);

            }
            else {
                return rank.getUserGameRank01(timeType, 0, type);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rankType">,-- 1 富豪榜 2 主播榜</param>
        /// <param name="timeType"> -- 0 日榜 1周榜 2 月榜 3 总榜	</param>
        /// <param name="type"> -- 0 本期 1 往期	</param>
        /// <returns></returns>
        public List<Rank_v2> getUserGameRank02(int rankType, int timeType, int type)
        {
            if (type == 0)
            {
                return giftRankSort(rank.getUserGameRank02(rankType, timeType, type), rankType, timeType);

            }
            else
            {
                return rank.getUserGameRank02(rankType, timeType, type);
            }
           
        }

        /// <summary>
        /// 获取排行升降序
        /// </summary>
        /// <param name="primary"></param>
        /// <param name="lastTime"></param>
        /// <returns></returns>
        public List<Rank_v2> giftRankSort(List<Rank_v2> primary,int rankType,int timeType)
        {
            string chacheKey = string.Format(CacheKeys.LIVE_RANK_V2, rankType, timeType);
            List<Rank_v2> lastTimeCache = CacheHelper.Get(chacheKey) as List<Rank_v2>;
            if (lastTimeCache == null || lastTimeCache.Count <= 0)
            {
                lastTimeCache =getRankList(rankType, timeType, 1);
                CacheHelper.SetCache(chacheKey, lastTimeCache, 1);
            }

            List<Rank_v2> lastTime = lastTimeCache.ToList();

            if (primary != null && lastTime != null)
            {
                foreach (var item in primary)
                {
                    var isbe = 0;
                    foreach (var items in lastTime)
                    {
                        if (item.useridx == items.useridx)
                        {

                            isbe = 1;
                            if (item.row < items.row)
                            {
                                item.sort = 2;//上升
                            }
                            else if (item.row > items.row)
                            {
                                item.sort = 1;//下降
                            }
                            else
                            {
                                item.sort = 0;//无变化
                            }
                            lastTime.Remove(items);
                            break;
                        }

                    }
                    if (isbe == 0)//上次未上榜
                    {
                        item.sort = 2;
                    }
                }
            }
            return primary;
        }

        /// <summary>
        /// 总排行榜
        /// </summary>
        /// <param name="ranktype">1 大神榜 2富豪榜 3 主播榜</param>
        /// <param name="timeType"></param>
        /// <returns></returns>
        public List<Rank_v2> getRankList( int ranktype ,int timeType,int type)
        {

            List<Rank_v2> giftList = new List<Rank_v2>();

            switch (ranktype)
            {
                case 0:
                    giftList = getUserGameRank01(0, timeType, type);
                    break;
                case 1:
                    giftList = getUserGameRank02(1, timeType, type);
                    break;
                case 2:
                    giftList = getUserGameRank01(2, timeType, type);
                    break;
                default:
                    break;
            }

            return giftList;
        }
          




        #endregion
    }
}
