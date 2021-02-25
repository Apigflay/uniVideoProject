using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Common;
using Model;

namespace DAL
{
    public class IncomeDAL
    {
        /// <summary>
        /// 我的钱包
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public decimal Get_MyWallet_Byidx_Data(int useridx)
        {
            SqlParameter[] param = { 
                                   SqlHelper.MakeInParam("@useridx",useridx),
                                   SqlHelper.MakeOutParam("@myWallet",SqlDbType.Money,0)
                                   };
            DbHelper.ExecuteNonQuery("Live_Get_MyWalletByidx", param);
            object obj = param[1].Value;

            return (decimal)obj;
        }

        /// <summary>
        /// 获取我的支付宝信息和钱包
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public WalletModel Get_AlipayInfo_MyWallet_Data(int useridx)
        {
            SqlParameter[] param = { 
                                   SqlHelper.MakeInParam("@useridx",useridx)
                                   };
            DataTable dt = DbHelper.GetTable("Live_Get_AlipayBindbyidx", param);

            return RFHelper<WalletModel>.GetEntity(dt);
        }

        /// <summary>
        /// 绑定支付宝信息
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        public int BindAliPayInfo_Data(WalletModel w)
        {
            SqlParameter[] param = { 
                                   SqlHelper.MakeInParam("@useridx",w.useridx),
                                   SqlHelper.MakeInParam("@account",SqlDbType.VarChar,40,w.aliPay),
                                   SqlHelper.MakeInParam("@realName",SqlDbType.VarChar,20,w.aliPayName),
                                   SqlHelper.MakeOutParam("@result",SqlDbType.Int,0)
                                   };
            DbHelper.ExecuteNonQuery("Live_Insert_AlipayBindInfo", param);

            int result = (int)param[3].Value;
            return result;
        }

        #region 红包奖励

        /// <summary>
        /// step1生成邀请码
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="inviteCode"></param>
        /// <param name="userip"></param>
        /// <returns></returns>
        public int GetInviteCodeByIdx_Data(int useridx, string ternimalName, string inviteCode, string userip)
        {
            SqlParameter[] param =
                {
                   SqlHelper.MakeInParam("@inviteridx",SqlDbType.Int,20,useridx),
                   SqlHelper.MakeInParam("@inviteCode",SqlDbType.VarChar,20,inviteCode),
                   SqlHelper.MakeInParam("@userip",SqlDbType.VarChar,30,userip),
                   SqlHelper.MakeOutParam("@result",SqlDbType.VarChar,50,""),
                   SqlHelper.MakeInParam("@ternimalName",SqlDbType.VarChar,30,ternimalName),
                };
            //object obj = DbHelper.ExecuteScalar(DbHelper.conn112_Mobile, "Live_Get_InviteCode", p);
            object obj = DbHelper.ExecuteScalar("Live_Get_InviteCode", param);

            return (int)obj;
        }

        /// <summary>
        /// step2 邀请新用户注册奖励红包
        /// </summary>
        /// <param name="uidx">被邀请人idx</param>
        /// <param name="inviteCode">邀请人邀请码</param>
        /// <param name="inviteid"></param>
        /// <returns></returns>
        public int Reward_Invite_Data(int uidx, string inviteCode, string userip, int osType, string deviceid, string thirdName, ref int inviteid)
        {
            string sql = "Live_InviteReward_Record_v2";
            //if (userip == "115.231.93.68") { sql = "Live_InviteReward_Record_test"; }

            SqlParameter[] paramter = { 
                                      SqlHelper.MakeInParam("@inviteCode",SqlDbType.VarChar,10,inviteCode),
                                      SqlHelper.MakeInParam("@inviteeidx",uidx),
                                      SqlHelper.MakeInParam("@userip",userip),
                                      SqlHelper.MakeInParam("@deviceid",deviceid),
                                      SqlHelper.MakeOutParam("@inviteid",SqlDbType.Int,0),
                                      SqlHelper.MakeInParam("@osType",osType),
                                      SqlHelper.MakeInParam("@platform",1),
                                      };
            var obj = DbHelper.ExecuteScalar(sql, paramter);

            inviteid = (int)paramter[4].Value;
            return (int)obj;
        }

        /// <summary>
        /// 我的红包明细
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<RedPacketReward> Get_RedPacketDetails_Byidx_Data(int useridx, int page, int pagesize)
        {
            SqlParameter[] param = { 
                                   SqlHelper.MakeInParam("@useridx",useridx),
                                   SqlHelper.MakeInParam("@page",page),
                                   SqlHelper.MakeInParam("@pageSize",pagesize)
                                   };
            DataTable dt = DbHelper.GetDataTable("Live_Get_RedPacket_DetailsByidx", param);

            return RFHelper<RedPacketReward>.ConvertToList(dt);
        }
        /// <summary>
        /// 提现明细
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<WithdrawMoney> Get_WithdrawDetails_Byidx_Data(int useridx, int page, int pagesize)
        {
            SqlParameter[] param = { 
                                   SqlHelper.MakeInParam("@useridx",useridx),
                                   SqlHelper.MakeInParam("@page",page),
                                   SqlHelper.MakeInParam("@pageSize",pagesize)
                                   };
            DataTable dt = DbHelper.GetTable("Live_Get_WithdrawMoney_Record", param);

            return RFHelper<WithdrawMoney>.ConvertToList(dt);
        }

        #endregion

        /// <summary>
        /// 获取邀请人信息
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public InviteInfo Get_Invite_DetailsInfoByidx_Data(int useridx, DateTime startDate, DateTime endDate)
        {
            SqlParameter[] param = { 
                                   SqlHelper.MakeInParam("@useridx",useridx),
                                   SqlHelper.MakeInParam("@redType",1),
                                   SqlHelper.MakeInParam("@beginDate",startDate),
                                   SqlHelper.MakeInParam("@endDate",endDate),
                                   };
            DataTable dt = DbHelper.GetDataTable("Live_Get_InviteRedPacket_info", param);

            return RFHelper<InviteInfo>.GetEntity(dt);
        }
    }
}
