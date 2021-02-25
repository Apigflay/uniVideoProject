using System.Collections.Generic;
using Common;
using DAL;
using Model;
using Newtonsoft.Json;
using ThirdAPI;
using System;
using BLL.Mongo;

namespace BLL
{
    public class IncomeBLL
    {
        IncomeDAL dal = new IncomeDAL();
        UserInfoBLL user = new UserInfoBLL();

        /// <summary>
        /// 获取我的支付宝信息和钱包
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public WalletModel Get_AlipayInfo_MyWallet(int useridx)
        {
            WalletModel wallet = dal.Get_AlipayInfo_MyWallet_Data(useridx);

            wallet.myWallet = dal.Get_MyWallet_Byidx_Data(useridx);
            wallet.isBindAlipay = !string.IsNullOrEmpty(wallet.aliPay) && wallet.auditStatus == 1 ? 1 : 0;

            return wallet;
        }

        /// <summary>
        /// 我的钱包
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public decimal Get_MyWallet_Byidx(int useridx)
        {
            return dal.Get_MyWallet_Byidx_Data(useridx);
        }

        /// <summary>
        /// 绑定支付宝信息
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        public int BindAliPayInfo(WalletModel w)
        {
            return dal.BindAliPayInfo_Data(w);
        }

        #region 邀新红包
        /// <summary>
        /// step1 生成邀请码
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="inviteCode"></param>
        /// <param name="userip"></param>
        /// <returns></returns>
        public int GetInviteCodeByIdx(int useridx, string ternimalName, ref string inviteCode)
        {
            string userip = Tools.GetRealIP();

            inviteCode = RandomHelper.GetRandomString(8, true, true, true, false, "");

            return dal.GetInviteCodeByIdx_Data(useridx, ternimalName, inviteCode, userip);
        }

        /// <summary>
        /// step2 邀新红包奖励
        /// </summary>
        /// <param name="uidx"></param>
        /// <param name="userid"></param>
        /// <param name="inviteCode"></param>
        /// <param name="osType">1:ios,2:android</param>
        /// <param name="deviceid"></param>
        /// <param name="appVersion"></param>
        /// <param name="inviteid"></param>
        /// <returns>-1：邀请码不存在，-2：邀请码已使用，-3：当日平台总额已用完，-4：被邀请人非微信注册，-5：设备或账号已领取过，-6：自己不能邀请自己，-7：风险范围，1：success</returns>
        public int Reward_Invite(int uidx, string userid, string inviteCode, int osType, string deviceid, string appVersion, string userip, ref int inviteid)
        {
            //return -3;//add by zhaorui 2018-1-2 下掉邀新紅包功能

            string thirdName = TextHelper.GetLetterString(userid);
            if (uidx <= 0) return -1;

            //目前被邀请者只支持微信用户
            //2017-07-18 15:00:53 去掉被邀请人只能是微信用户(潘跃龙要求)
            //if (!thirdName.Equals("WeiXin")) return -4;

            #region 风险评估 2017年6月28日13:15:31

            //string channel = AppDataBLL.AppChannelId;
            //string nickName = user.GetLiveUserInfoByIdx(uidx).myname;

            //string strRiskRet = TDRCHelper.Get_Register_RiskResult(nickName, channel, userip, uidx, deviceid, inviteCode);
            //TDRCResultModel tdrcResult = JsonConvert.DeserializeObject<TDRCResultModel>(strRiskRet);
            //tdrcResult.version = 2;
            //tdrcResult.useridx = uidx;
            //tdrcResult.userip = userip;
            //tdrcResult.channel = channel;
            //tdrcResult.strResult = strRiskRet;

            //记录风险评估日志
            //MongoService.Insert_RegisterRiskLog(tdrcResult);

            #endregion

            int result = dal.Reward_Invite_Data(uidx, inviteCode, userip, osType, deviceid, thirdName, ref inviteid);

            //红包消息通知到客户端领取红包操作
            //if (result > 0 && inviteid > 0)
            //{
            //    ServerHelper.InviteRewardNotice(inviteid);
            //}

            //日志记录
            string cookieCode = CookieHelper.GetCookieValue("Live_inviteCode");
            string logMsg = "【邀新奖励调用结束】奖励结果：" + GetInviteResultMsg(result);
            //MongoService.InsertInviteAccess(2, uidx, inviteCode, deviceid, appVersion, userid, result, logMsg);

            return result;
        }
        /// <summary>
        /// 我的红包明细
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<RedPacketReward> Get_RedPacketDetails_Byidx(int useridx, int page, int pagesize)
        {
            return dal.Get_RedPacketDetails_Byidx_Data(useridx, page, pagesize);
        }
        /// <summary>
        /// 红包提现明细
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<WithdrawMoney> Get_RedPacketWithDraw_Details_Byidx(int useridx, int page, int pagesize)
        {
            return dal.Get_WithdrawDetails_Byidx_Data(useridx, page, pagesize);
        }

        #endregion

        /// <summary>
        /// 获取邀请人信息
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public InviteInfo Get_Invite_DetailsInfoByidx(int useridx)
        {
            DateTime dt1 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            DateTime dt2 = dt1.AddDays(1);// Convert.ToDateTime(dt1 + " 23:59:59");

            InviteInfo ii = dal.Get_Invite_DetailsInfoByidx_Data(useridx, dt1, dt2);
            ii.myWallet = Get_MyWallet_Byidx(useridx);

            return ii;
        }

        /// <summary>
        /// 邀新奖励返回结果
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public string GetInviteResultMsg(int result)
        {
            string msg = "";
            switch (result)
            {
                case 1:
                    msg = "success";
                    break;
                case -1:
                    msg = "邀请码不存在";
                    break;
                case -2:
                    msg = "邀请码已使用";
                    break;
                case -3:
                    msg = "当日平台总额已用完";
                    break;
                case -4:
                    msg = "被邀请人非微信注册";
                    break;
                case -5:
                    msg = "设备或账号已领取过";
                    break;
                case -6:
                    msg = "自己不能邀请自己";
                    break;
            }
            return msg;
        }
    }
}
