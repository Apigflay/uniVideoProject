using System.Collections.Generic;
using System.Linq;
using Model;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class GiftDAL
    {
        private DBContext db = new DBContext();

        #region 礼物相关

        /// <summary>
        ///  获取礼物分类
        /// </summary>
        /// <param name="param1">预留参数</param>
        /// <returns></returns>
        public List<GiftTab> GetGiftTabList_Data(int param1)
        {
            const string sql = "[Live_Get_GiftTab]";
            SqlParameter[] p ={
                              SqlHelper.MakeInParam("@param1",SqlDbType.Int,10,param1),
                              SqlHelper.MakeInParam("@platform",SqlDbType.Int,10,0)
                          };
            DataTable dt = DbHelper.GetDataTable(sql, p);

            return RFHelper<GiftTab>.ConvertToList(dt);
        }

        /// <summary>
        ///  获取礼物列表
        /// </summary>
        /// <param name="type">0:查询出8个 1:查询出所有的</param>
        /// <param name="platform">平台区分：0：喵播，1：新浪秀</param>
        /// <returns></returns>
        public List<GiftModel> GetGiftList_Data(int type, int isNewApp)
        {
            const string sql = "[live_Gift_List]";
            SqlParameter[] p ={
                              SqlHelper.MakeInParam("@type",SqlDbType.Int,10,type),
                              SqlHelper.MakeInParam("@platform",SqlDbType.Int,10,0),
                              SqlHelper.MakeInParam("@isNewApp",SqlDbType.Int,10,isNewApp)
                          };
            DataTable dt = DbHelper.GetDataTable(sql, p);

            return RFHelper<GiftModel>.ConvertToList(dt);
        }

        #endregion

        /// <summary>
        /// 宝宝兑换
        /// </summary>
        /// <param name="type">1：签约主播，2：散户主播</param>
        /// <param name="useridx"></param>
        /// <param name="nums"></param>
        /// <returns>-1：非签约/散户，-2：宝宝不足</returns>
        public int BabyExchange_Data(int type, int useridx, int nums)
        {
            string sql = type == 1 ? "Live_ExChangeGiftWaWa" : "Live_ExChangeGiftWaWa_SH";
            int ret = 0;
            SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx),
                                SqlHelper.MakeInParam("@nums",SqlDbType.Int,10,nums),
                                SqlHelper.MakeOutParam("@ret",SqlDbType.Int,4,0)
                                };
            object obj = DbHelper.ExecuteScalar(sql, sp);

            ret = (int)obj;
            return ret;
        }

        /// <summary>
        /// 获取宝宝兑换记录
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<GiftExchange> Get_BabyExchange_Record_Data(int useridx)
        {
            SqlParameter[] sp = {
                             SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx)
                             };
            DataTable dt = DbHelper.GetTable("Live_Get_WithdrawBaby_Record", sp);
            return RFHelper<GiftExchange>.ConvertToList(dt);
        }

        /// <summary>
        /// 获取娃娃数
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="stardate"></param>
        /// <param name="endadate"></param>
        /// <returns></returns>
        public GiftExchange getWaWaNums(int useridx, string stardate, string endadate)
        {
            var p = new DynamicParameters();
            p.Add("@useridx", useridx);
            p.Add("@stardate", stardate);
            p.Add("@enddate", endadate);

            return db.Write(c => c.Query<GiftExchange>(
                "Live_Select_GiftWawaByUseridx", p, commandType: CommandType.StoredProcedure).FirstOrDefault());
        }

        /// <summary>
        /// 获取签约主播宝宝未兑换数
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="dataAction">1：只输出剩余宝宝数，2：返回datatable</param>
        /// <param name="babyNum"></param>
        /// <returns></returns>
        public BabyGiftInfo Get_BabyNumsByuseridx_Data(int useridx, int dataAction, ref int babyNum)
        {
            SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,4,useridx),
                                SqlHelper.MakeInParam("@dataAction",SqlDbType.Int,4,dataAction),
                                SqlHelper.MakeOutParam("@babyNum",SqlDbType.Int,4,0),
                                };
            DataTable dt = DbHelper.GetDataTable("Live_Get_BabyByuseridx", sp);
            babyNum = (int)sp[2].Value;
            return RFHelper<BabyGiftInfo>.GetEntity(dt);
        }

        /// <summary>
        /// 获取散户主播未兑换宝宝数
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="dataAction">1：只输出剩余宝宝数，2：返回datatable</param>
        /// <param name="babyNum"></param>
        /// <returns></returns>
        public BabyGiftInfo Get_BabyNumsByuseridx_Sanhu_Data(int useridx, int dataAction, ref int babyNum)
        {
            SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,4,useridx),
                                SqlHelper.MakeInParam("@dataAction",SqlDbType.Int,4,dataAction),
                                SqlHelper.MakeOutParam("@babyNum",SqlDbType.Int,4,0),
                                };
            DataTable dt = DbHelper.GetTable("Live_Get_BabyByuseridx_SH", sp);
            babyNum = (int)sp[2].Value;
            return RFHelper<BabyGiftInfo>.GetEntity(dt);
        }

        /// <summary>
        /// 是否散户主播
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public int isRetail_Anchor_Data(int useridx)
        {
            int result = 0;

            SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,4,useridx),
                                SqlHelper.MakeOutParam("@result",SqlDbType.Int,4,0)
                                };
            DbHelper.ExecuteNonQuery("Live_isExists_Retail", sp);

            result = (int)sp[1].Value;
            return result;
        }

        /// <summary>
        /// 是否签约主播
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public int isSign_Anchor_Data(int useridx)
        {
            int result = 0;

            SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,4,useridx),
                                SqlHelper.MakeOutParam("@result",SqlDbType.Int,4,0)
                                };
            DbHelper.ExecuteNonQuery("Live_isSign_Anchor", sp);

            result = (int)sp[1].Value;
            return result;
        }
    }
}
