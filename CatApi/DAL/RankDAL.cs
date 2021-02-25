using Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System;

namespace DAL
{
    public class RankDAL
    {
        #region 粉丝奉献榜

        /// <summary>
        /// 我的守护位
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<RankInfo> Get_MyWard_SendGift_Data(int useridx)
        {
            const string sql = "[Live_MyWard_SendGift]";
            var list = new List<RankInfo>();
            SqlParameter[] p ={
                                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx)
                        };
            DataTable dt = SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, p);
            return RFHelper<RankInfo>.ConvertToList(dt);
        }

        /// <summary>
        /// 粉丝奉献榜 
        /// </summary>
        /// <param name="timetype">1：周，2：月，3：总 ,6：日</param>
        /// <param name="curuseridx"></param>
        /// <param name="useridx"></param>
        /// <param name="page"></param>
        /// <param name="rank"></param>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public List<RankInfo> Get_Rank_FansWard_Data(int timetype, int curuseridx, int useridx, Paging page, ref MyRankInfo rank, DateTime dt1, DateTime dt2)
        {
            //粉丝奉献棒月榜和总榜调用过程Rank_FansWard_Total
            var procName = (timetype == 3 || timetype == 2) ? "Rank_FansWard_Total" : "Rank_FansWard";
            var list = new List<RankInfo>();
            rank = new MyRankInfo();
            try
            {
                SqlParameter[] p ={
                                SqlHelper.MakeInParam("@curUseridx",SqlDbType.Int,10,curuseridx),
                                SqlHelper.MakeInParam("@toUseridx",SqlDbType.Int,10,useridx),
                                SqlHelper.MakeInParam("@page",SqlDbType.Int,10,page.pageIndex),
                                SqlHelper.MakeInParam("@pagesize",SqlDbType.Int,10,page.pageSize),
                                SqlHelper.MakeOutParam("@totalCount",SqlDbType.Int,10,0),
                                SqlHelper.MakeOutParam("@myRank",SqlDbType.Int,10,0),
                                SqlHelper.MakeOutParam("@myTotal",SqlDbType.Int,10,0),
                                SqlHelper.MakeOutParam("@myPhoto",SqlDbType.VarChar,200,""),
                                SqlHelper.MakeInParam("@startdate",SqlDbType.DateTime,30,dt1),
                                SqlHelper.MakeInParam("@enddate",SqlDbType.DateTime,30,dt2),
                          };
                DataTable dt = SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, procName, p);


                rank.count = (int)p[4].Value;
                rank.myRank = (int)p[5].Value;
                rank.myTotal = (int)p[6].Value;
                rank.myPhoto = p[7].Value.ToString();

                return RFHelper<RankInfo>.ConvertToList(dt);
            }
            catch(Exception ex)
            {
                return list;
            }
            
        }
        //public List<RankInfo> Get_SendGift_WeekRank_Data(int curuseridx, int useridx, int page, ref MyRankInfo rank)
        //{
        //    var list = new List<RankInfo>();
        //    rank = new MyRankInfo();
        //    SqlParameter[] p ={
        //                        SqlHelper.MakeInParam("@curUseridx",SqlDbType.Int,10,curuseridx),
        //                        SqlHelper.MakeInParam("@toUseridx",SqlDbType.Int,10,useridx),
        //                        SqlHelper.MakeInParam("@page",SqlDbType.Int,10,page),
        //                        SqlHelper.MakeInParam("@pagesize",SqlDbType.Int,10,15),
        //                        SqlHelper.MakeOutParam("@totalCount",SqlDbType.Int,10,0),
        //                        SqlHelper.MakeOutParam("@myRank",SqlDbType.Int,10,0),
        //                        SqlHelper.MakeOutParam("@myTotal",SqlDbType.Int,10,0),
        //                        SqlHelper.MakeOutParam("@myPhoto",SqlDbType.VarChar,200,""),
        //                  };
        //    DataTable dt = SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, "[User_SendGift_WeekRank]", p);
        //    rank.count = (int)p[4].Value;
        //    rank.myRank = (int)p[5].Value;
        //    rank.myTotal = (int)p[6].Value;
        //    rank.myPhoto = p[7].Value.ToString();
        //    return RFHelper<RankInfo>.ConvertToList(dt);
        //}
        /// <summary>
        /// 分享周榜
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<RankInfo> Get_Share_WeekRank_Data(int _curIdx, int _useridx, ref MyRankInfo rank)
        {
            List<RankInfo> list = new List<RankInfo>();
            rank = new MyRankInfo();
            SqlParameter[] p ={
                                SqlHelper.MakeInParam("@curUseridx",SqlDbType.Int,10,_curIdx),
                                SqlHelper.MakeInParam("@toUseridx",SqlDbType.Int,10,_useridx),
                                SqlHelper.MakeInParam("@page",SqlDbType.Int,10,1),
                                SqlHelper.MakeInParam("@pagesize",SqlDbType.Int,10,50),
                                SqlHelper.MakeOutParam("@totalCount",SqlDbType.Int,10,0),
                                SqlHelper.MakeOutParam("@myRank",SqlDbType.Int,10,0),
                                SqlHelper.MakeOutParam("@myTotal",SqlDbType.Int,10,0),
                                SqlHelper.MakeOutParam("@myPhoto",SqlDbType.VarChar,200,""),
                          };
            DataTable dt = DbHelper.GetTable("[User_Share_WeekRank]", p);
            rank.count = (int)p[4].Value;
            rank.myRank = (int)p[5].Value;
            rank.myTotal = (int)p[6].Value;
            rank.myPhoto = p[7].Value.ToString();
            return RFHelper<RankInfo>.ConvertToList(dt);
        }

        /// <summary>
        /// 观看周榜
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<RankInfo> Get_Watch_WeekRank_Data(int _curIdx, int _useridx, ref MyRankInfo rank)
        {
            var list = new List<RankInfo>();
            rank = new MyRankInfo();
            SqlParameter[] p ={
                                SqlHelper.MakeInParam("@curUseridx",SqlDbType.Int,10,_curIdx),
                                SqlHelper.MakeInParam("@toUseridx",SqlDbType.Int,10,_useridx),
                                SqlHelper.MakeOutParam("@totalCount",SqlDbType.Int,10,0),
                                SqlHelper.MakeOutParam("@myRank",SqlDbType.Int,10,0),
                                SqlHelper.MakeOutParam("@myTotal",SqlDbType.Int,10,0),
                                SqlHelper.MakeOutParam("@myPhoto",SqlDbType.VarChar,200,""),
                          };
            DataTable dt = DbHelper.GetTable("[User_Watch_WeekRank]", p);
            rank.count = (int)p[2].Value;
            rank.myRank = (int)p[3].Value;
            rank.myTotal = (int)p[4].Value;
            rank.myPhoto = p[5].Value.ToString();
            return RFHelper<RankInfo>.ConvertToList(dt);
        }

        /// <summary>
        /// 房间内礼物赠送排行榜
        /// </summary>
        /// <param name="roomid"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public List<SaleRankInfo> getInRoomSaleRank_Data(int roomid, int page, int pagesize, ref int total)
        {
            total = 0;
            SqlParameter[] p ={
                                SqlHelper.MakeInParam("@roomid",SqlDbType.Int,10,roomid),
                                SqlHelper.MakeInParam("@page",SqlDbType.Int,10,page),
                                SqlHelper.MakeInParam("@pagesize",SqlDbType.Int,10,pagesize),
                                SqlHelper.MakeOutParam("@totalCount",SqlDbType.Int,10,0)
                          };
            DataTable dt = DbHelper.GetTable("Live_RoomInSaleRank", p);
            total = (int)p[3].Value;
            return RFHelper<SaleRankInfo>.ConvertToList(dt);
        }

        #endregion

        #region 总榜排行榜

        /// <summary>
        /// 消费榜
        /// </summary>
        /// <param name="timetype">1:日，2：周，3：月，4：总</param>
        /// <param name="top"></param>
        /// <param name="stardate"></param>
        /// <param name="enddate"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="counts"></param>
        /// <returns></returns>
        public List<SaleRankInfo> GetUserSaleRank_Data(int timetype, int top, string stardate, string enddate, int page, int pagesize, ref int counts)
        {
            var proc = timetype == 4 ? "[Live_Get_TotalSaleRank_User]" : "[Live_Select_WeekRank_User]";

            var list = new List<RankInfo>();
            SqlParameter[] p = {
                                    SqlHelper.MakeInParam("@stardate",SqlDbType.VarChar,20,stardate),
                                    SqlHelper.MakeInParam("@enddate",SqlDbType.VarChar,20,enddate),
                                    SqlHelper.MakeInParam("@page",SqlDbType.Int,5,page),
                                    SqlHelper.MakeInParam("@pagesize",SqlDbType.Int,5,pagesize),
                                    SqlHelper.MakeOutParam("@counts",SqlDbType.Int,5,0),
                                    SqlHelper.MakeInParam("@top",SqlDbType.Int,5,top),
                                    SqlHelper.MakeInParam("@timetype",SqlDbType.Int,4,timetype)
                                };
            DataTable dt = DbHelper.GetTable(proc, p);
            counts = (int)p[4].Value;

            return RFHelper<SaleRankInfo>.ConvertToList(dt);
        }
        /// <summary>
        /// 主播排行榜 2：周榜，3：月榜，4：总榜
        /// </summary>
        /// <param name="stardate"></param>
        /// <param name="enddate"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="counts"></param>
        /// <returns></returns>
        public List<SaleRankInfo> GetAnchorRank_Data(int timetype, string stardate, string enddate, int page, int pagesize, ref int counts)
        {
            var list = new List<SaleRankInfo>();
            SqlParameter[] p = { 
                                SqlHelper.MakeInParam("@stardate",SqlDbType.VarChar,20,stardate),
                                SqlHelper.MakeInParam("@enddate",SqlDbType.VarChar,20,enddate),
                                SqlHelper.MakeInParam("@page",SqlDbType.Int,5,page),
                                SqlHelper.MakeInParam("@pagesize",SqlDbType.Int,5,pagesize),
                                SqlHelper.MakeOutParam("@counts",SqlDbType.Int,5,0),
                                SqlHelper.MakeInParam("@top",SqlDbType.Int,4,200),
                                SqlHelper.MakeInParam("@timetype",SqlDbType.Int,4,timetype)
                                };
            DataTable dt = DbHelper.GetTable("[Live_Select_WeekRank_Songer]", p);
            counts = (int)p[4].Value;

            return RFHelper<SaleRankInfo>.ConvertToList(dt);
        }

        /// <summary>
        /// 家族排行榜
        /// </summary>
        /// <param name="timetype">2：周榜，3：月榜，4：总榜</param>
        /// <param name="top"></param>
        /// <param name="stardate"></param>
        /// <param name="enddate"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="counts"></param>
        /// <returns></returns>
        public List<SaleRankInfo> GetFamilyRank_Data(int timetype, int top, string stardate, string enddate, int page, int pagesize, ref int counts)
        {
            var list = new List<SaleRankInfo>();
            SqlParameter[] p = { 
                                SqlHelper.MakeInParam("@stardate",SqlDbType.VarChar,20,stardate),
                                SqlHelper.MakeInParam("@enddate",SqlDbType.VarChar,20,enddate),
                                SqlHelper.MakeInParam("@page",SqlDbType.Int,5,page),
                                SqlHelper.MakeInParam("@pagesize",SqlDbType.Int,5,pagesize),
                                SqlHelper.MakeOutParam("@counts",SqlDbType.Int,5,0),
                                SqlHelper.MakeInParam("@top",SqlDbType.Int,5,top),
                                SqlHelper.MakeInParam("@timetype",SqlDbType.Int,4,timetype)
                                };
            DataTable dt = DbHelper.GetTable("[Live_Select_WeekRank_Family]", p);
            counts = (int)p[4].Value;

            return RFHelper<SaleRankInfo>.ConvertToList(dt);
        }

        #endregion

        #region 周星排行榜
        /// <summary>
        /// 获取上周周星礼物
        /// 作业过程3433：[job_Gift_WeekStar]
        /// </summary>
        /// <returns></returns>
        public List<WeekStarGift> GetlastWeekStar_Data()
        {
            const string sql = "Live_Get_GiftWeekStar";
            //SqlParameter[] p ={
            //                  SqlHelper.MakeInParam("@type",SqlDbType.Int,10,type)
            //              };
            DataTable dt = SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, null);
            return RFHelper<WeekStarGift>.ConvertToList(dt);
        }

        /// <summary>
        /// 获取当前周星主播
        /// </summary>
        /// <param name="giftid"></param>
        /// <param name="page"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public List<WeekStarGift> GettsWkStar_Data(int top, int giftid, Paging page, DateTime startdate, DateTime enddate)
        {
            const string sql = "Live_Get_CurWeekStar";
            SqlParameter[] p ={
                              SqlHelper.MakeInParam("@giftid",SqlDbType.Int,10,giftid),
                              SqlHelper.MakeInParam("@top",SqlDbType.Int,10,top),
                              SqlHelper.MakeInParam("@page",SqlDbType.Int,10,page.pageIndex),
                              SqlHelper.MakeInParam("@pageSize",SqlDbType.Int,10,page.pageSize),
                              SqlHelper.MakeOutParam("@totalCount",SqlDbType.Int,10,0),
                              SqlHelper.MakeInParam("@startdate",SqlDbType.DateTime,20,startdate),
                              SqlHelper.MakeInParam("@enddate",SqlDbType.DateTime,20,enddate),
                          };
            DataTable dt = SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, p);
            return RFHelper<WeekStarGift>.ConvertToList(dt);
        }

        #endregion

        #region 主播守护

        /// <summary>
        /// 获取守护列表购买商品
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<GuardGoods> Get_GoodsPriceList_Data()
        {
            const string sql = "[Live_Get_GuardGoods]";
            var list = new List<GuardGoods>();

            DataTable dt = DbHelper.GetTable(DbHelper.conn112_Mobile, sql, null);
            return RFHelper<GuardGoods>.ConvertToList(dt);
        }

        /// <summary>
        /// 我的守护位(金币购买)
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<RankInfo> Get_MyGuard_Data(int useridx, int top, ref int totalCount)
        {
            const string sql = "[Live_Get_Guardian_Byuidx]";
            var list = new List<RankInfo>();
            SqlParameter[] p ={
                                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx),
                                SqlHelper.MakeInParam("@top",SqlDbType.Int,10,top),
                                SqlHelper.MakeOutParam("@totalCount",SqlDbType.Int,10,0)
                        };
            DataTable dt = DbHelper.GetTable(DbHelper.conn112_Mobile, sql, p);
            totalCount = (int)p[2].Value;

            return RFHelper<RankInfo>.ConvertToList(dt);
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
        public int Purchase_Guard_Data(int useridx, int toUseridx, int roomid, int goodsId, string userip)
        {
            const string sql = "Live_Purchase_Guard";
            SqlParameter[] sp = { 
                                SqlHelper.MakeInParam("useridx",SqlDbType.Int,10,useridx),
                                SqlHelper.MakeInParam("toUseridx",SqlDbType.Int,10,toUseridx),
                                SqlHelper.MakeInParam("roomid",SqlDbType.Int,10,roomid),
                                SqlHelper.MakeInParam("goodsId",SqlDbType.Int,10,goodsId),
                                SqlHelper.MakeInParam("userip",SqlDbType.VarChar,20,userip),

                                SqlHelper.MakeOutParam("ret",SqlDbType.Int,10,0)
                                };

            DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, sql, sp);
            int ret = (int)sp[5].Value;

            return ret;
        }


        /// <summary>
        /// 大神榜
        /// </summary>
        /// <param name="rankType">0 最高派彩 1 连红 2 胜率</param>
        /// <param name="type">0 本期 1往期</param>
        /// <returns></returns>
        public List<Rank_v2> getUserGameRank01(int rankType, int timeType , int type)
        {

            const string sql = "lw_getUserGameRank01";
            var list = new List<RankInfo>();
            SqlParameter[] p ={
                                SqlHelper.MakeInParam("@rankType",SqlDbType.Int,10,rankType),
                                SqlHelper.MakeInParam("@type",SqlDbType.Int,10,type)
                        };
            DataTable dt = DbHelper.GetTable( sql, p);
            return RFHelper<Rank_v2>.ConvertToList(dt);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="rankType">,-- 1 富豪榜 2 主播榜</param>
        /// <param name="timeType"> -- 0 日榜 1周榜 2 月榜 3 总榜	</param>
        /// <param name="type"> -- 0 本期 1 往期	</param>
        /// <returns></returns>
        public List<Rank_v2> getUserGameRank02(int rankType, int timeType ,int type )
        {

            const string sql = "lw_getUserGameRank02";
            var list = new List<RankInfo>();
            SqlParameter[] p ={
                                SqlHelper.MakeInParam("@type",SqlDbType.Int,10,type),
                                SqlHelper.MakeInParam("@rankType",SqlDbType.Int,10,rankType),
                                SqlHelper.MakeInParam("@timeType",SqlDbType.Int,10,timeType)
                        };
            DataTable dt = DbHelper.GetTable( sql, p);
            return RFHelper<Rank_v2>.ConvertToList(dt);
        }

        #endregion
    }
}
