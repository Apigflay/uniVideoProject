using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// 喵播 手机充值
    /// </summary>
    public class PayDAL
    {
        private DBContext db = new DBContext();
        /// <summary>
        /// 充值9158币列表  
        /// </summary>
        /// <param name="platform"> 1: Android 2:Iphone 3:Ipad 4:iPhone企业</param>
        /// <param name="channelId">1:微信 2:支付宝  3:AppStore</param>
        /// <returns></returns>
        public List<PayView> GetRechargeView_Data(int platform, int channelId)
        {
            var sql = "Live_Recharge_List";
            SqlParameter[] p ={
                              SqlHelper.MakeInParam("@PlatForm",SqlDbType.Int,10,platform),
                              SqlHelper.MakeInParam("@ChannelId",SqlDbType.Int,10,channelId)
                          };
            DataTable dt = SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, p);
            return RFHelper<PayView>.ConvertToList(dt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<PayMenu> getPayMenuList()
        {
            var list = new List<PayMenu>();
            return db.Write(c => c.Query<PayMenu>("Live_Select_PayMenuList", null, commandType: CommandType.StoredProcedure).ToList());
        }
        /// <summary>
        /// 获取用户总喵币
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public long GetUserCashByuseridx(int useridx)
        {
            long coin = 0;
            using (IDbConnection conn = DbHelper.OpenConnection())
            {
                var p = new DynamicParameters();
                p.Add("@useridx", useridx);
                p.Add("@cash", dbType: DbType.Int64, direction: ParameterDirection.Output);

                var obj = conn.Query<Int64>(
             "f_GetUserCashByUserIdx", p, commandType: CommandType.StoredProcedure).ToList();
                coin = p.Get<Int64>("@cash");
            }
            return coin;
        }


        /// <summary>
        /// 获取用户总喵币
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public long GetUserGameCashByuseridx(int useridx)
        {
            long coin = 0;
            using (IDbConnection conn = DbHelper.OpenConnection())
            {
                var p = new DynamicParameters();
                p.Add("@useridx", useridx);
                p.Add("@myWallet", dbType: DbType.Int64, direction: ParameterDirection.Output);

                var obj = conn.Query<Int64>(
             "Live_Get_GoldByidx", p, commandType: CommandType.StoredProcedure).ToList();
                coin = p.Get<Int64>("@myWallet");
            }
            return coin;
        }

        /// <summary>
        /// 根据订单号查询用户IDX
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="ptype"></param>
        /// <returns></returns>
        public int GetUserIdxFromOrderID(long orderid, int ptype)
        {
            int useridx = 0;
            using (IDbConnection conn = DbHelper.OpenConnection())
            {
                var p = new DynamicParameters();
                p.Add("@orderid", orderid);
                p.Add("@ptype", ptype);
                p.Add("@ret", dbType: DbType.Int64, direction: ParameterDirection.Output);

                var obj = conn.Query<Int64>(
             "AS_Select_OrderForUserIDX", p, commandType: CommandType.StoredProcedure).ToList();
                useridx = p.Get<int>("@ret");

            }
            return useridx;
        }

        #region 我的收益 没用
        public WalletModel Get_MyIncome_Data(int dataAction, int uidx)
        {
            string sql = "Live_Get_MyIncome";

            SqlParameter[] sp ={
                                   SqlHelper.MakeInParam("@dataAction",SqlDbType.Int,4,dataAction),
                                   SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,uidx),
                                   SqlHelper.MakeOutParam("@myWallet",SqlDbType.Float,20,0),
                                   SqlHelper.MakeOutParam("@myMoney",SqlDbType.Float,20,0)
                               };
            DataTable dt = DbHelper.GetTable(sql, sp);

            string result = sp[2].Value.ToString();
            //myWallet = Convert.ToDecimal(result);
            //myMoney = Convert.ToDecimal(sp[3]);
            return RFHelper<WalletModel>.GetEntity(dt);
        }
        /// <summary>
        /// 现金提现申请
        /// </summary>
        /// <param name="wr"></param>
        /// <returns></returns>
        public int Withdraw_Insert_Data(WithdrawRecord wr)
        {
            string sql = "[Live_ApplyWithdraw]";
            int ret = 0;
            SqlParameter[] sp ={
                                   SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,wr.useridx),
                                   SqlHelper.MakeInParam("@money",SqlDbType.Float,10,wr.amount),
                                   SqlHelper.MakeInParam("@alipayid",SqlDbType.VarChar,20,wr.alipayID),
                                   SqlHelper.MakeInParam("@realname",SqlDbType.VarChar,20,wr.realName),
                                   SqlHelper.MakeInParam("@mobilephone",SqlDbType.NVarChar,11,wr.mobilePhone),
                                   SqlHelper.MakeOutParam("@ret",SqlDbType.Int,20,0)
                               };
            DbHelper.ExecuteNonQuery(sql, sp);

            ret = (int)sp[5].Value;

            return ret;
        }

        #endregion

        #region 房间公积金 弃用

        /// <summary>
        /// 获取公积金相关信息记录
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<PayRoomScore> getRoomScoreList(int useridx)
        {
            var list = new List<PayRoomScore>();

            using (IDbConnection conn = DbHelper.OpenConnection())
            {
                var p = new DynamicParameters();
                p.Add("@useridx", useridx);

                list = conn.Query<PayRoomScore>(
             "Live_Select_RoomScore", p, commandType: CommandType.StoredProcedure).ToList();
            }

            return list;
        }
        /// <summary>
        /// 获取房间用户房间公积金及麦时
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="lead"></param>
        /// <returns></returns>
        public List<PayRoomScore> getRoomScoreList(int useridx, int lead)
        {
            var list = new List<PayRoomScore>();

            using (IDbConnection conn = DbHelper.OpenConnection())
            {
                var p = new DynamicParameters();
                p.Add("@useridx", useridx);
                p.Add("@level", lead);

                list = conn.Query<PayRoomScore>(
             "Live_Select_RoomScore", p, commandType: CommandType.StoredProcedure).ToList();
            }

            return list;
        }

        /// <summary>
        /// 获取用户可领取公积金
        /// </summary>
        /// <param name="roomid"></param>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public int getRoomCash(int roomid, int useridx)
        {
            int ret = 0;
            using (IDbConnection conn = DbHelper.OpenConnection())
            {
                var p = new DynamicParameters();
                p.Add("@roomid", roomid);
                p.Add("@useridx", useridx);
                p.Add("@ret", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var obj = conn.Execute(
             "Live_Select_RoomCoin", p, commandType: CommandType.StoredProcedure);
                ret = p.Get<int>("@ret");

            }
            return ret;
        }

        /// <summary>
        /// 领取房间公积金
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="roomid"></param>
        /// <returns></returns>
        public int AddRoomCash(int useridx, int roomid)
        {
            int ret = 0;
            using (IDbConnection conn = DbHelper.OpenConnection())
            {
                var p = new DynamicParameters();
                p.Add("@useridx", useridx);
                p.Add("@roomid", roomid);
                p.Add("@ret", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var obj = conn.Execute("Live_GetRoomCash", p, commandType: CommandType.StoredProcedure);
                ret = p.Get<int>("@ret");

            }
            return ret;
        }

        #endregion

        #region 苹果充值 弃用

        /// <summary>
        /// 创建订单 苹果充值 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="amount"></param>
        /// <param name="paytype"></param>
        /// <param name="ptype"></param>
        /// <returns></returns>
        public int CreateIOSorder(int userIdx, int price, int amount, string pId, string content, string ip)
        {
            int ret = 0;
            try
            {
                using (IDbConnection conn = DbHelper.OpenConnection())
                {
                    var p = new DynamicParameters();
                    p.Add("@userIdx", userIdx);
                    p.Add("@pId", pId);
                    p.Add("@price", price);
                    p.Add("@amount", amount);
                    p.Add("@step", 1);
                    p.Add("@content", content);
                    p.Add("@ip", ip);
                    var obj = conn.Query<decimal>("[Live_CreateIOSPayOrder]"
                      , p
                      , commandType: CommandType.StoredProcedure).First();
                    ret = Convert.ToInt32(obj);
                }
            }
            catch (Exception ex)
            {
                ret = -1;
            }
            return ret;
        }
        /// <summary>
        /// 根据订单Id获取商品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IOSPay Get(int id)
        {
            const string sqlStr = "[live_GetIOSPayInfoById]";
            var p = new DynamicParameters();
            p.Add("@Id", id, DbType.Int32);
            IOSPay pay = new IOSPay();

            return db.Write(c => c.Query<IOSPay>(sqlStr, p).FirstOrDefault());
        }

        public int UpdateIos_info(int type, int orderid, string iosorderNum, string deviceId, int step, string des)
        {
            int ret = 0;
            string sql = "live_UpdateIOSPay_Info";
            //using (IDbConnection conn = DbHelper.OpenConnection())
            //{
            ret = db.Write(c => c.Execute(sql
                , new { @action = type, @id = orderid, @iosOrderNum = iosorderNum, @pKey = deviceId, @step = step, @des = des }
                , commandType: CommandType.StoredProcedure));
            //}
            return ret;
        }
        /// <summary>
        /// 苹果充值正式 加币
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int iosPay_Vip(int id)
        {
            int ret = 0;
            var p = new DynamicParameters();
            p.Add("@Id", id, DbType.Int32);
            p.Add("@ret", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (IDbConnection conn = DbHelper.OpenConnection())
            {
                conn.Execute(
                    "[IosPay_upVip]"
                    , p
                    , commandType: CommandType.StoredProcedure);
                ret = p.Get<int>("@ret");
            }
            //catch (Exception ex)
            //{
            //    Loger.WriteLog(LogFile.IOSPay,"【苹果充值 数据库用户加币操作】"+ ex.Message);
            //}
            return ret;
        }

        /// <summary>
        /// 查询该用户 加币总数
        /// 查询该账号 已充值 币额
        /// </summary>
        /// <param name="uidx"></param>
        /// <returns></returns>
        public int IOSPaySumCoin(int uidx)
        {
            int ret = 0;
            const string sql = "live_IOSPayLimit";
            using (IDbConnection conn = DbHelper.OpenConnection())
            {
                var obj = conn.Query<Int64>(sql
                    , new { @useridx = uidx }
                    , commandType: CommandType.StoredProcedure).First();
                ret = Convert.ToInt32(obj);
            }
            return ret;
        }

        /// <summary>
        /// 苹果充值 走充值流程
        /// </summary>
        /// <param name="uidx"></param>
        /// <param name="userId"></param>
        /// <param name="orderId">订单id</param>
        /// <param name="sumCoin"></param>
        public int IOSPayAddCoin(int uidx, string userId, int price, int amount)
        {
            int ret = -1;
            try
            {
                var p = new DynamicParameters();
                p.Add("@userIdx", uidx);
                p.Add("@userId", userId);
                p.Add("@amount", amount);
                p.Add("@ret", dbType: DbType.Int32, direction: ParameterDirection.Output);
                using (IDbConnection conn = DbHelper.OpenConnection())
                {
                    const string sql = "[live_IOSPay_AddCoin]";
                    conn.Execute(sql, p, commandType: CommandType.StoredProcedure);
                    ret = p.Get<int>("@ret");
                }
            }
            catch (Exception ex)
            {
                ret = -1;
            }
            return ret;
        }

        #endregion

        #region 支付宝充值 弃用
        /// <summary>
        /// 创建支付宝订单
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="moneys"></param>
        /// <param name="contents"></param>
        /// <returns></returns>
        public int CreateOrderByZFB(int useridx, int moneys, string contents, int ntype, string memo)
        {
            int ret = 0;
            using (IDbConnection conn = DbHelper.OpenConnection())
            {
                var p = new DynamicParameters();
                p.Add("@useridx", useridx);
                p.Add("@amount", moneys);
                p.Add("@content", contents);
                p.Add("@orderid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@ntype", ntype);
                p.Add("@stepmemo", memo);
                conn.Execute("Live_Insert_PayCreateZFBOrder"
                  , p
                  , commandType: CommandType.StoredProcedure);
                ret = p.Get<int>("@orderid");
            }
            return ret;
        }
        /// <summary>
        /// 成功加币
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public int SuccessOrderByZFB(int orderid)
        {
            int ret = 0;
            using (IDbConnection conn = DbHelper.OpenConnection())
            {
                var p = new DynamicParameters();
                p.Add("@id", orderid);

                var obj = conn.Query<int>("zfbpay_up_Mobile_new"
                   , p
                   , commandType: CommandType.StoredProcedure);
                ret = Convert.ToInt32(obj.First());
            }
            return ret;
        }

        public ZFBpay GetZFBPayinfo(int id)
        {
            ZFBpay mod = new ZFBpay();
            using (IDbConnection conn = DbHelper.OpenConnection())
            {
                var p = new DynamicParameters();
                p.Add("@id", id);

                mod = conn.Query<ZFBpay>("Live_Select_Zfbpay_Info"
                   , p
                   , commandType: CommandType.StoredProcedure).FirstOrDefault<ZFBpay>();
            }
            return mod;
        }


        #endregion

        #region 微信充值 弃用

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="moneys"></param>
        /// <param name="contents"></param>
        /// <returns></returns>
        public long CreateOrderByWX(int useridx, int moneys, string contents)
        {
            long ret = 0;
            using (IDbConnection conn = DbHelper.OpenConnection())
            {
                var p = new DynamicParameters();
                p.Add("@useridx", useridx);
                p.Add("@amount", moneys);
                p.Add("@content", contents);
                p.Add("@orderid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute("Live_Insert_PayCreateWeixinOrder"
                  , p
                  , commandType: CommandType.StoredProcedure);
                ret = p.Get<int>("@orderid");
            }
            return ret;
        }
        /// <summary>
        /// 支付订单完成送币
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public int SuccessOrderByWx(long orderid)
        {
            int ret = 0;
            using (IDbConnection conn = DbHelper.OpenConnection())
            {
                var p = new DynamicParameters();
                p.Add("@id", orderid);

                ret = conn.Execute("Live_Wxpay_up_Mobile_new"
                   , p
                   , commandType: CommandType.StoredProcedure);
            }
            return ret;
        }
        public int UNpayOrderInsert(string useridx, string value, string deviceType, string content, string ntype, string ptype, string package, out int orderid)
        {
            SqlParameter[] p = {
                                SqlHelper.MakeOutParam("@orderid",SqlDbType.Int,4,0),
                                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,4,useridx),
                                SqlHelper.MakeInParam("@amount",SqlDbType.Decimal,8,value),
                                SqlHelper.MakeInParam("@content",SqlDbType.VarChar,20,"充值"),
                                SqlHelper.MakeInParam("@stepmemo",SqlDbType.VarChar,50,content),
                                SqlHelper.MakeInParam("@ntype",SqlDbType.Int,4,ntype),
                                SqlHelper.MakeInParam("@ptype",SqlDbType.Int,4,ptype),
                                SqlHelper.MakeInParam("@ip",SqlDbType.VarChar,50,Tools.GetRealIP()),
                                SqlHelper.MakeInParam("@deviceType",SqlDbType.Int,4,deviceType),
                                SqlHelper.MakeInParam("@introducerNo",SqlDbType.VarChar,50,package)
                               };
            try
            {
                int result = SqlHelper.ExecuteNonQuery(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, "Live_Insert_UNPayCreateOrder", p);
                orderid = p[0].Value == DBNull.Value ? 0 : int.Parse(p[0].Value.ToString());
                return result;
            }
            catch (Exception e)
            {
                orderid = 0;
                return 0;
            }
        }
        /// <summary>
        /// 更新订单流水记录
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="amount"></param>
        /// <param name="succTime"></param>
        /// <param name="ipsbillno"></param>
        /// <returns></returns>
        public int UNpayOrderUpdate(string orderid, string ipsbillno, string amount, ref string useridx)
        {
            //orderid = "10173";
            //ipsbillno = "213";
            //amount = "1000";
            //decimal t = Convert.ToDecimal(amount) / 100;
            SqlParameter[] p = {
                                SqlHelper.MakeInParam("@id",SqlDbType.Int,10,orderid),
                                SqlHelper.MakeInParam("@ipsbillno",SqlDbType.VarChar,50,ipsbillno),
                                SqlHelper.MakeInParam("@amount",SqlDbType.Decimal,8,Convert.ToDecimal(amount)/100),
                                SqlHelper.MakeOutParam("@useridx",SqlDbType.Int,15,0),
                               };
            try
            {
                int result = SqlHelper.ExecuteNonQuery(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, "Live_Update_UNPayUpdateOrder", p);
                useridx = p[3].Value.ToString();
                return result;

            }
            catch (Exception e)
            {
                return 0;
            }
        }
        /// <summary>
        /// 修改订单加币
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public int UNpayUpdate(string orderid)
        {
            SqlParameter[] p = {
            SqlHelper.MakeInParam("@id",SqlDbType.Int,4,orderid),
            SqlHelper.MakeOutParam("@ret",SqlDbType.Int,2,0)
        };
            try
            {
                int result = SqlHelper.ExecuteNonQuery(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, "Live_UNPay_Up", p);
                int ret = (int)p[1].Value;
                return ret;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        #endregion

        #region 弃用
        /// <summary>
        /// 苹果微信新用户赠送货币
        /// </summary>
        /// <param name="uidx"></param>
        /// <param name="userId"></param>
        /// <param name="orderId">订单id</param>
        /// <param name="sumCoin"></param>
        public static int AddCoinByNewWeixinUser(int uidx, string userId)
        {
            int ret = 0;
            var p = new DynamicParameters();
            p.Add("@useridx", uidx);
            p.Add("@userid", userId);
            p.Add("@cash", 200);
            ret = new DBContext().Write(c => c.Execute("live_iphone_logpay", p, commandType: CommandType.StoredProcedure));
            return ret;
        }
        /// <summary>
        /// 9158币转换成喵币
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="coin"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        public int CoinAddFromThird(int useridx, int coin)
        {
            int ret = 0;
            using (IDbConnection conn = DbHelper.OpenConnection())
            {
                var p = new DynamicParameters();
                p.Add("@useridx", useridx);
                p.Add("@coin", coin);
                p.Add("@Sources", "9158");

                ret = conn.Execute("Live_AddCoinFromThird"
                   , p
                   , commandType: CommandType.StoredProcedure);
            }
            return ret;
        }

        #endregion

        #region 老挝版金币提现
        /// <summary>
        /// 查询用户金币余额及可提现余额
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="sale">可提现余额</param>
        /// <param name="money">用户余额</param>
        public void LW_GoldLimitSet(int useridx, ref decimal sale, ref decimal money)
        {

            const string procName = "I_LW_GoldLimitSet";
            SqlParameter[] sp = {
              SqlHelper.MakeInParam("@useridx",SqlDbType.Int,4,useridx),
                 SqlHelper.MakeOutParam("@money",SqlDbType.Money,20,money),
                SqlHelper.MakeOutParam("@sale",SqlDbType.Money,20,sale)
            };
            DbHelper.ExecuteNonQuery(procName, sp);
            money = decimal.Round((decimal)sp[1].Value, 2);
            sale = decimal.Round((decimal)sp[2].Value, 2);
        }

        /// <summary>
        /// 金币提现
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="amout">提现金额</param>
        public int userPutForward(int useridx, decimal amout, string pwd, string bankId)
        {
            int iRet = 0;
            const string procName = "I_userCoinPutForward";
            SqlParameter[] sp = {
              SqlHelper.MakeInParam("@useridx",SqlDbType.Int,4,useridx),
               SqlHelper.MakeInParam("@bankId",SqlDbType.VarChar,100,bankId),
              SqlHelper.MakeInParam("@amout",SqlDbType.Decimal,10,amout),
              SqlHelper.MakeInParam("@pwd",SqlDbType.VarChar,255,pwd),
              SqlHelper.MakeOutParam("@iRet",SqlDbType.Int,4,iRet)
            };
            DbHelper.ExecuteNonQuery(procName, sp);
            iRet = (int)sp[4].Value;
            return iRet;
        }
        /// <summary>
        /// 金币提现
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="amout">提现金额</param>
        public int UpIncomeLogState(int id)
        {
            int iRet = 0;
            const string procName = "I_LW_IncomeLogUpiState";
            SqlParameter[] sp = {
              SqlHelper.MakeInParam("@id",SqlDbType.Int,4,id),
              SqlHelper.MakeOutParam("@iRet",SqlDbType.Int,4,iRet)
            };
            DbHelper.ExecuteNonQuery(procName, sp);
            iRet = (int)sp[1].Value;
            return iRet;
        }
           
        public List<AnchorTixianList> anchorTixianLists(int useridx, int pageIndex, int pageSize)
        {
            var sql = "AnchorTixianList";
            SqlParameter[] p ={
                              SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx),
                              SqlHelper.MakeInParam("@pageIndex",SqlDbType.Int,10,pageIndex),
                              SqlHelper.MakeInParam("@pageSize",SqlDbType.Int,10,pageSize)
                          };
            DataTable dt = SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, p);
            return RFHelper<AnchorTixianList>.ConvertToList(dt);
        }
        
        #endregion
    }
}
