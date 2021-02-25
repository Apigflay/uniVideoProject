using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Common;
using DAL;
using Model;

namespace BLL
{
    public class PayBLL
    {
        PayDAL dal = new PayDAL();

        /// <summary>
        /// 充值9158币列表  
        /// </summary>
        /// <param name="platform"> 1: Android 2:Iphone 3:Ipad 4:iPhone企业</param>
        /// <param name="channelId">1:微信 2:支付宝  3:AppStore</param>
        /// <returns></returns>
        public List<PayView> GetRechargeView(int platform, int channelId)
        {
            return dal.GetRechargeView_Data(platform, channelId);
        }

        #region 充值 弃用
        public int CreateOrderByZFB(int useridx, int moneys, string contents, int ntype, string memo)
        {
            return dal.CreateOrderByZFB(useridx, moneys, contents, ntype, memo);
        }

        public int SuccessOrderByZFB(int orderid)
        {
            return dal.SuccessOrderByZFB(orderid);
        }

        public ZFBpay GetZFBPayInfo(int id)
        {
            return dal.GetZFBPayinfo(id);
        }
        public long CreateOrderByWX(int useridx, int moneys, string contents)
        {
            return dal.CreateOrderByWX(useridx, moneys, contents);
        }
        public int UNpayOrderInsert(string useridx, string value, string deviceType, string content, string ntype, string ptype, string package, out int orderid)
        {
            return dal.UNpayOrderInsert(useridx, value, deviceType, content, ntype, ptype, package, out orderid);
        }
        public int UNpayOrderUpdate(string orderid, string ipsbillno, string amount, ref string useridx)
        {
            return dal.UNpayOrderUpdate(orderid, ipsbillno, amount, ref useridx);
        }
        public int UNpayUpdate(string orderid)
        {
            return dal.UNpayUpdate(orderid);
        }
        public int SuccessOrderByWX(long orderid)
        {
            return dal.SuccessOrderByWx(orderid);
        }

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
            return dal.CreateIOSorder(userIdx, price, amount, pId, content, ip);
        }
        /// <summary>
        /// 根据订单Id获取商品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IOSPay Get(int id)
        {
            return dal.Get(id);
        }

        public int UpdateIos_info(int type, int orderid, string iosorderNum, string deviceId, int step, string des)
        {
            return dal.UpdateIos_info(type, orderid, iosorderNum, deviceId, step, des);
        }
        /// <summary>
        /// 查询该用户 加币总数
        /// 查询该账号 已充值 币额
        /// </summary>
        /// <param name="uidx"></param>
        /// <returns></returns>
        public int IOSPaySumCoin(int uidx)
        {
            return dal.IOSPaySumCoin(uidx);
        }
        /// <summary>
        /// 苹果充值正式 加币
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int iosPay_Vip(int id)
        {
            return dal.iosPay_Vip(id);
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
            return dal.IOSPayAddCoin(uidx, userId, price, amount);
        }

        //public List<PayBase> GetPayPagerList(string paytype, int useridx, string stardate, string enddate, int page, int pagesize, ref int counts)
        //{
        //    return dal.getPayPagerList(paytype, useridx, stardate, enddate, page, pagesize, ref counts);
        //}
        #endregion

        public List<PayMenu> getPayMenuList()
        {
            return dal.getPayMenuList();
        }
        public long getUsetCashByUseridx(int useridx)
        {
            return dal.GetUserCashByuseridx(useridx);
        }
        public long getUserGameCashByuseridx(int useridx)
        {
            return dal.GetUserGameCashByuseridx(useridx);
        }
        public int getUseridxFromOrderID(long orderid, int ptype)
        {
            return dal.GetUserIdxFromOrderID(orderid, ptype);
        }
        /// <summary>
        /// 我的钱包
        /// </summary>
        /// <param name="dataAction"></param>
        /// <param name="uidx"></param>
        /// <param name="myWallet"></param>
        /// <returns></returns>
        //public WalletModel Get_MyIncome(int dataAction, int uidx)
        //{
        //    return dal.Get_MyIncome_Data(dataAction, uidx);
        //}

        /// <summary>
        /// 短视频提现申请
        /// </summary>
        /// <param name="wr"></param>
        /// <returns></returns>
        //public int Withdraw_Insert(WithdrawRecord wr)
        //{
        //    return dal.Withdraw_Insert_Data(wr);
        //}

        #region 弃用

        public List<PayRoomScore> getRoomScoreList(int useridx)
        {
            return dal.getRoomScoreList(useridx);
        }
        public List<PayRoomScore> getRoomScoreList(int useridx, int lead)
        {
            return dal.getRoomScoreList(useridx, lead);
        }
        public int getRoomCash(int useridx, int roomid)
        {
            return dal.getRoomCash(roomid, useridx);
        }
        public int AddRoomCash(int useridx, int roomid)
        {
            return dal.AddRoomCash(useridx, roomid);
        }
        public int CoinAddFromThird(int useridx, int coin)
        {
            return dal.CoinAddFromThird(useridx, coin);
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
            dal.LW_GoldLimitSet( useridx, ref  sale, ref  money);
        }

        /// <summary>
        /// 金币提现
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="amout">提现金额</param>
        public int  userPutForward(int useridx,  decimal amout,string pwd,string bankId)
        {
            return dal.userPutForward(useridx, amout, pwd, bankId);
        }
        /// <summary>
        /// 主播交易记录申请提现
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="amout">提现金额</param>
        public int UpIncomeLogState(int id)
        {
            return dal.UpIncomeLogState(id);
        }
        public List<AnchorTixianList> anchorTixianLists(int useridx,int pageIndex,int pageSize)
        {
            return dal.anchorTixianLists(useridx, pageIndex, pageSize);
        }
        #endregion
    }
}
