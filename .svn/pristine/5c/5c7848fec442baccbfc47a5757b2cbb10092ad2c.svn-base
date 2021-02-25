using BLL;
using Common;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.Mvc;
using WebAPI.WeixinPay;
using Model.View;
using Common.Core;
using ThirdAPI;

namespace WebAPI.Controllers
{
    public class PayController : BaseController
    {
        UserInfoBLL user = new UserInfoBLL();
        PayBLL pay = new PayBLL();
        GiftBLL gift = new GiftBLL();
        IncomeBLL income = new IncomeBLL();

        #region 红包收益，提现，邀请信息

        /// <summary>
        /// 红包收益
        /// live.9158.com/Pay/ExChange?useridx=60068188&chkcode=7480713e3ec4e68647f3fa6201ae93de&token=Ze3RDHVbSTSc5ESZLnhEEI46x2sL2&apiversion=3&Random=9
        /// </summary>
        /// <param name="chkcode"></param>
        /// <param name="token"></param>
        /// <param name="useridx"></param>
        /// <param name="apiversion">1：显示宝宝兑换（用babayview页面），2：红包版本（显示宝宝兑换和现金提现），3：显示现金提现</param>
        /// <returns></returns>
        public ActionResult ExChange(string chkcode, int useridx = 0, int apiversion = 1)
        {
            if (useridx <= 0 || string.IsNullOrEmpty(chkcode)) return View("Error");

            UserInfo u = user.GetLiveUserInfoByIdx(useridx);

            string code = CryptoHelper.ToMD5("&&**miaomiao" + u.userId);
            string myToken = MemcachedHelper.MemGet(CacheKeys.LIVE_USER_TOKEN + useridx);

            if (chkcode.ToLower() != code.ToLower()) return View("Error");

            VMIncome vmMyIncome = new VMIncome();
            vmMyIncome.apiversion = apiversion;
            vmMyIncome.useridx = u.useridx;
            vmMyIncome.chkCode = chkcode;
            vmMyIncome.MyWawa = gift.Get_BabyInfo(useridx);

            //防止后面还没访问此页面进行提交申请
            MemcachedHelper.Set("Cache_WithdrawView_" + useridx, chkcode, 5);

            if (apiversion == 1)
            {
                return View("babyView", vmMyIncome);
            }
            else
            {
                //红包收益
                vmMyIncome.MyWallet = income.Get_AlipayInfo_MyWallet(useridx);

                return View(vmMyIncome);
            }
        }

        /// <summary>
        /// 提现页面
        /// live.9158.com/pay/withdrawView?useridx=60068188&token=cebe0c15f0a6eef3581a570febb090e8
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public ActionResult withdrawView(int useridx = 0, string token = "")
        {
            //UserInfo u = user.GetLiveUserInfoByIdx(useridx);

            string code = CryptoHelper.ToMD5("&&**miaomiao" + useridx);
            //string myToken = MemcachedHelper.MemGet(CacheKeys.LIVE_USER_TOKEN + useridx);

            if (token.ToLower() != code.ToLower()) return View("Error");

            VMIncome vmMyIncome = new VMIncome();
            vmMyIncome.useridx = useridx;
            vmMyIncome.chkCode = token;

            //防止后面还没访问此页面进行提交申请
            MemcachedHelper.Set("Cache_WithdrawView_" + useridx, token, 5);

            vmMyIncome.MyWallet = income.Get_AlipayInfo_MyWallet(useridx);

            return View("ExChange", vmMyIncome);
        }

        /// <summary>
        /// 现金提现
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="chkcode"></param>
        /// <returns></returns>
        public ActionResult withdrawWalletData(int useridx = 0, string chkcode = "")
        {
            var memValue = MemcachedHelper.Get("Cache_WithdrawView_" + useridx);
            MobileResult mr = new MobileResult();

            if (useridx <= 0)
            {
                mr.code = "101";
                mr.msg = "ParamError";
                return Content(JsonConvert.SerializeObject(mr));
            }
            else if (memValue == null || !memValue.Equals(chkcode))
            {
                mr.code = "102";
                mr.msg = "请退出重新进入";
                return Content(JsonConvert.SerializeObject(mr));
            }
            WalletModel result = income.Get_AlipayInfo_MyWallet(useridx);
            if (result.auditStatus == 0)
            {
                mr.code = "105";
                mr.msg = "账号正在审核中，请等待审核结果";
            }
            else if (result.isBindAlipay == 0)
            {
                mr.code = "103";
                mr.msg = "请先绑定支付宝";
            }
            else if (MemcachedHelper.Exists("Live_WithdrawMoney_" + useridx))
            {
                mr.code = "104";
                mr.msg = "你已经提现过，请于5分钟后再来尝试。";
            }
            if (int.Parse(mr.code) <= 105)
            {
                LogHelper.WriteLog(LogFile.Log, "【现金提现】{0}|{1}|{2}", useridx, result.myWallet, mr.msg);
                return Content(JsonConvert.SerializeObject(mr));
            }

            //提现逻辑,调用王树星那边接口
            mr = AppDataBLL.withdrawAlipay(useridx);

            #region 日志消息记录

            if (mr.code == "-1")
            {
                mr.msg = "等级未达到5级";
            }
            else if (mr.code == "-3")
            {
                mr.msg = "金额未达到要求";
            }
            else if (mr.code == "-8")
            {
                mr.msg = "首次仅能提现1元";
            }
            else if (mr.code == "100")
            {
                mr.msg = "success";
                MemcachedHelper.Store("Live_WithdrawMoney_" + useridx, chkcode, new TimeSpan(0, 5, 0));
            }

            LogHelper.WriteLog(LogFile.Log, "【现金提现】{0}|{1}|{2}|{3}", useridx, result.myWallet, mr.code, mr.msg);
            #endregion

            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 获取邀请信息
        /// live.9158.com/Pay/GetInviteInfo?useridx=60068188&token=
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public ActionResult GetInviteInfo(int useridx = 0, string token = "")
        {
            if (useridx <= 0) { return new BaseController().ParamError(); }
            string userTokenKey = CacheKeys.LIVE_USER_TOKEN + useridx;
            string userTokenValue = (string)MemcachedHelper.MemGet(userTokenKey);

            if (string.IsNullOrEmpty(userTokenValue) || !token.Equals(userTokenValue))
            {
                return LoginFail();
            }
            InviteInfo iinfo = income.Get_Invite_DetailsInfoByidx(useridx);

            MobileResult mr = new MobileResult();

            if (iinfo != null)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = new { inviteInfo = iinfo };
            }
            else
            {
                mr.data = new { inviteInfo = new InviteInfo() };
            }

            return Content(JsonConvert.SerializeObject(mr));
        }

        #endregion

        #region 绑定支付宝相关

        /// <summary>
        /// 绑定支付宝页面
        /// live.9158.com/Pay/bindAlipay?useridx=&token=
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public ActionResult bindAlipay()
        {
            return View();
        }

        /// <summary>
        /// 绑定支付宝操作
        /// </summary>
        /// <param name="form"></param>
        /// <param name="useridx"></param>
        /// <param name="chkcode"></param>
        /// <returns></returns>
        [HttpPost]
        //string geetest_challenge, string geetest_validate, string geetest_seccode
        public ActionResult bindAlipay(FormCollection form, int useridx = 0, string chkcode = "")
        {
            //var memValue = MemcachedHelper.Get("Cache_WithdrawView_" + useridx);

            MobileResult mr = new MobileResult();
            WalletModel wallet = new WalletModel();

            wallet.useridx = useridx;
            wallet.aliPay = form["alipayID"].Trim();
            wallet.aliPayName = form["realname"].Trim();

            //string geetest_challenge = form["geetest_challenge"];
            //string geetest_validate = form["geetest_validate"];
            //string geetest_seccode = form["geetest_seccode"];

            //GeetestLib_V2 geetest = new GeetestLib_V2("d9762b1339678f455dba3628083ea3c9", "54c37b479fb86bca2fafd59d0adbd6a9");

            //if (1 != geetest.enhencedValidateRequest(geetest_challenge, geetest_validate, geetest_seccode))
            //{
            //    mr.code = "12";
            //    mr.msg = "验证码错误";
            //    return Content(JsonConvert.SerializeObject(mr));
            //}

            if (string.IsNullOrEmpty(wallet.aliPay) || string.IsNullOrEmpty(wallet.aliPayName))
            {
                mr.code = "101";
                mr.msg = "支付宝账号或真实名称不能为空";
                return Content(JsonConvert.SerializeObject(mr));
            }

            int result = income.BindAliPayInfo(wallet);
            if (result > 0)
            {
                mr.code = "100";
                mr.msg = "绑定支付宝账号成功";
            }
            else if (result == -1)
            {
                mr.code = "101";
                mr.msg = "支付宝账号或真实姓名不能为空";
            }
            else if (result == -2)
            {
                mr.code = "102";
                mr.msg = "该用户已绑定支付宝账号";
            }
            else if (result == -3 || result == -4)
            {
                mr.code = "103";
                mr.msg = "已存在支付宝账号";
            }
            else if (result == 0)
            {
                mr.code = "103";
                mr.msg = "绑定失败";
            }
            if (result <= 0)
            {
                LogHelper.WriteLog(LogFile.Log, "【绑定支付宝错误信息】{0},{1},{2},{3}", useridx, wallet.aliPay, wallet.aliPayName, mr.msg);
            }
            return Content(JsonConvert.SerializeObject(mr));
        }

        #endregion

        #region 宝宝相关

        /// <summary>
        /// 宝宝提现页面
        /// live.9158.com/Pay/babyView?useridx={}&apiversion=1&chkcode={}
        /// </summary>
        /// <param name="chkcode"></param>
        /// <param name="useridx"></param>
        /// <param name="apiversion"></param>
        /// <returns></returns>
        public ActionResult babyView(string chkcode, int useridx = 0, int apiversion = 1)
        {
            if (useridx <= 0 || string.IsNullOrEmpty(chkcode)) return View("Error");

            UserInfo u = user.GetLiveUserInfoByIdx(useridx);

            string code = CryptoHelper.ToMD5("&&**miaomiao" + u.userId);
            string myToken = MemcachedHelper.MemGet(CacheKeys.LIVE_USER_TOKEN + useridx);

            if (chkcode.ToLower() != code.ToLower()) return View("Error");

            VMIncome vmMyIncome = new VMIncome();
            vmMyIncome.apiversion = apiversion;
            vmMyIncome.useridx = u.useridx;
            vmMyIncome.chkCode = chkcode;
            vmMyIncome.MyWawa = gift.Get_BabyInfo(useridx);

            MemcachedHelper.Set("Cache_WithdrawView_" + useridx, chkcode, 5);

            return View(vmMyIncome);
        }

        /// <summary>
        /// 宝宝提现
        /// </summary>
        /// <param name="type">1：签约，2：散户</param>
        /// <param name="chkcode"></param>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public ActionResult babySubmitData(int type = 0, string chkcode = "", int useridx = 0)
        {
            int result = 0;
            var memValue = MemcachedHelper.Get("Cache_WithdrawView_" + useridx);

            MobileResult mr = new MobileResult();
            if (useridx <= 0 || string.IsNullOrEmpty(chkcode))
            {
                return ParamError();
            }
            if (!memValue.Equals(chkcode))
            {
                mr.code = "102";
                mr.msg = "签名验证失败";
                return Content(JsonConvert.SerializeObject(mr));
            }

            result = gift.BabyExchange_Submit(type, useridx);

            if (result > 0)
            {
                mr.code = "100";
                mr.msg = "success";
            }
            else if (result == -1)
            {
                mr.code = "103";
                mr.msg = "非签约或散户主播不可提现";
            }
            else if (result == -2)
            {
                mr.code = "105";
                mr.msg = "宝宝数不足";
            }
            else
            {
                mr.code = "106";
                mr.msg = "提现失败";
            }
            LogHelper.WriteLog(LogFile.Log, "【宝宝兑换申请】{0},type:{1},ret:{2},msg:{3}", useridx, type, result, mr.msg);

            return Content(JsonConvert.SerializeObject(mr));
        }

        #endregion

        #region 充值列表

        /// <summary>
        /// 充值商品列表
        /// </summary>
        /// <param name="platform">1:Android 2:iphone</param>
        /// <param name="channelId">渠道 1:微信 2:支付宝 3:AppStore</param>
        /// <returns></returns>
        //[OutputCache(Duration = 60 * 5, VaryByParam = "platform,typeId")]
        public ActionResult Index(int platform = 1, int typeId = 1)
        {
            PayBLL pay = new PayBLL();
            Result r = new Result();

            var dt = pay.GetRechargeView(platform, typeId);
            if (dt != null && dt.Count > 0)
            {
                r.code = "100";
                r.msg = "success";
                r.data = dt;
            }
            return Json(r, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region IOS充值 已停用
        /// <summary>
        /// 创建IOS订单
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateOrderByios()
        {
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null)
            {
                LogHelper.WriteLog(LogFile.IOSPay, "IOS支付创建订单参数错误");
                return Json(new Result { code = "101", msg = "参数错误" });
            }
            string goodsId = dic["goodsId"];//商品Id
            string price = dic["price"];//价格
            //string amount = dic["amount"].ToString().Replace("喵币", "");//获得多少猫币
            string content = dic["content"] ?? "充值";   //充值类型
            string userIdx = dic["userIdx"] ?? "0";
            //string userId = dic["userId"] ?? "";

            var mr = new Result();
            var pay = new PayBLL();

            //是否有充值权限
            int canbuy = 1;// paylimit.ReviewUser_new(userId, int.Parse(userIdx), ip, devId, curAmount, DateTime.Now.ToString());//验证充值权限
            if (canbuy != 1)
            {
                mr.code = "110";
                mr.msg = "没有权限充值,请联系工作人员";
                return Json(mr);
            }
            //当天充值总币限制
            int sumCoinByDate = 100000;// pay.IOSPaySumCoin(0);
            if (sumCoinByDate >= 100000)
            {
                mr.code = "111";
                mr.msg = "充值金额超限";
                return Json(mr);
            }

            ////该账号 加币 大于20万  不再进行加币操作
            //int sumcoin = pay.IOSPaySumCoin(int.Parse(userIdx));
            //if (sumcoin > 200000)
            //{
            //    mr.code = "111";
            //    mr.msg = "该账号当前充值金额超限";
            //    LogHelper.WriteLog(LogFile.IOSPay, "该账号充值已限额:uidx:" + userIdx + "|sumcoin:" + sumcoin);
            //    return Json(mr);
            //}

            ////生成喵播充值订单
            //int orderId = pay.CreateIOSorder(int.Parse(userIdx), int.Parse(price), int.Parse(price) * 700, goodsId, content, Tools.GetRealIP());
            //if (orderId <= 0)
            //{
            //    mr.code = "112";
            //    mr.msg = "生成订单失败";
            //    LogHelper.WriteLog(LogFile.IOSPay, "【本苹果充值系统】喵播生成订单失败，或许该订单已存在");
            //    return Json(mr);
            //}
            //if (orderId > 0)
            //{
            //    mr.code = "100";
            //    mr.msg = "success";
            //    mr.data = new { orderId = orderId };
            //    return Json(mr);
            //}
            return Json(mr);
        }
        /// <summary>
        /// 客户端收到票据后提交服务端验证
        /// </summary>
        /// <param name="biid">票据的订单号</param>
        /// <param name="key">票据信息</param>
        /// <returns></returns>
        public JsonResult VerifyReceipt()
        {
            Result mr = new Result();
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null)
            {
                LogHelper.WriteLog(LogFile.IOSPay, "IOS支付提交票据参数错误");

                return Json(new Result { code = "101", msg = "参数错误" });
            }
            string userIdx = dic["userIdx"];
            string key = dic["mykey"];
            string receiptData = dic["receiptData"];
            string time = dic["timeout"];
            string amount = dic["amount"];
            string ntype = dic["ntype"];   //1-秘币 2 vip
            //string userIdx = "10000205";
            //string key = "0c8b09fd138d9a8a3b41ca615a191eb3";
            //string receiptData = "MII1tQYJKoZIhvcNAQcCoII1pjCCNaICAQExCzAJBgUrDgMCGgUAMIIlVgYJKoZIhvcNAQcBoIIlRwSCJUMxgiU/MAoCAQgCAQEEAhYAMAoCARQCAQEEAgwAMAsCAQECAQEEAwIBADALAgEDAgEBBAMMATEwCwIBCwIBAQQDAgEAMAsCAQ8CAQEEAwIBADALAgEQAgEBBAMCAQAwCwIBGQIBAQQDAgEDMAwCAQoCAQEEBBYCNCswDAIBDgIBAQQEAgIAiTANAgENAgEBBAUCAwHWUDANAgETAgEBBAUMAzEuMDAOAgEJAgEBBAYCBFAyNTIwGAIBBAIBAgQQ5vRLz8P1DbNL78veVFvjLDAZAgECAgEBBBEMD2NvbS50Zy5mYWNldGFsazAbAgEAAgEBBBMMEVByb2R1Y3Rpb25TYW5kYm94MBwCAQUCAQEEFJqIvr8X8owEzwfUrp75XyesewHGMB4CAQwCAQEEFhYUMjAxOS0wNi0xOVQwOTowNToyNlowHgIBEgIBAQQWFhQyMDEzLTA4LTAxVDA3OjAwOjAwWjBKAgEHAgEBBEIImUWmEMCxScL6Z4XuP35CO2H6YPoGJL3PB0RVKbWu1JKFEJi6Fkp9uU5819UgtbEMVLAS3Gcgid/ZFjXnnwexh/UwUwIBBgIBAQRLmtrpLRKjldcXJI3eRsxRsXrwOuiBHfMgt1zxhZk0moR9PFDxhkHwjH6tWyzxDCnDGV1Qg2NC+neQrtgZa8XqDgnDEq/+onN1togZMIIBVwIBEQIBAQSCAU0xggFJMAsCAgasAgEBBAIWADALAgIGrQIBAQQCDAAwCwICBrACAQEEAhYAMAsCAgayAgEBBAIMADALAgIGswIBAQQCDAAwCwICBrQCAQEEAgwAMAsCAga1AgEBBAIMADALAgIGtgIBAQQCDAAwDAICBqUCAQEEAwIBATAMAgIGqwIBAQQDAgECMAwCAgauAgEBBAMCAQAwDAICBq8CAQEEAwIBADAMAgIGsQIBAQQDAgEAMBsCAganAgEBBBIMEDEwMDAwMDA1Mzc2MjEyODQwGwICBqkCAQEEEgwQMTAwMDAwMDUzNzYyMTI4NDAdAgIGpgIBAQQUDBJjb20uZmFjZXRhbGsudmlwMzAwHwICBqgCAQEEFhYUMjAxOS0wNi0xN1QwMjoyMDo0MVowHwICBqoCAQEEFhYUMjAxOS0wNi0xN1QwMjoyMDo0MVowggFXAgERAgEBBIIBTTGCAUkwCwICBqwCAQEEAhYAMAsCAgatAgEBBAIMADALAgIGsAIBAQQCFgAwCwICBrICAQEEAgwAMAsCAgazAgEBBAIMADALAgIGtAIBAQQCDAAwCwICBrUCAQEEAgwAMAsCAga2AgEBBAIMADAMAgIGpQIBAQQDAgEBMAwCAgarAgEBBAMCAQIwDAICBq4CAQEEAwIBADAMAgIGrwIBAQQDAgEAMAwCAgaxAgEBBAMCAQAwGwICBqcCAQEEEgwQMTAwMDAwMDUzNzc1MzcyMTAbAgIGqQIBAQQSDBAxMDAwMDAwNTM3NzUzNzIxMB0CAgamAgEBBBQMEmNvbS5mYWNldGFsay52aXAzMDAfAgIGqAIBAQQWFhQyMDE5LTA2LTE3VDA5OjAyOjA5WjAfAgIGqgIBAQQWFhQyMDE5LTA2LTE3VDA5OjAyOjA5WjCCAVcCARECAQEEggFNMYIBSTALAgIGrAIBAQQCFgAwCwICBq0CAQEEAgwAMAsCAgawAgEBBAIWADALAgIGsgIBAQQCDAAwCwICBrMCAQEEAgwAMAsCAga0AgEBBAIMADALAgIGtQIBAQQCDAAwCwICBrYCAQEEAgwAMAwCAgalAgEBBAMCAQEwDAICBqsCAQEEAwIBAjAMAgIGrgIBAQQDAgEAMAwCAgavAgEBBAMCAQAwDAICBrECAQEEAwIBADAbAgIGpwIBAQQSDBAxMDAwMDAwNTM3NzY1OTkzMBsCAgapAgEBBBIMEDEwMDAwMDA1Mzc3NjU5OTMwHQICBqYCAQEEFAwSY29tLmZhY2V0YWxrLnZpcDMwMB8CAgaoAgEBBBYWFDIwMTktMDYtMTdUMDk6MjE6MzVaMB8CAgaqAgEBBBYWFDIwMTktMDYtMTdUMDk6MjE6MzVaMIIBVwIBEQIBAQSCAU0xggFJMAsCAgasAgEBBAIWADALAgIGrQIBAQQCDAAwCwICBrACAQEEAhYAMAsCAgayAgEBBAIMADALAgIGswIBAQQCDAAwCwICBrQCAQEEAgwAMAsCAga1AgEBBAIMADALAgIGtgIBAQQCDAAwDAICBqUCAQEEAwIBATAMAgIGqwIBAQQDAgECMAwCAgauAgEBBAMCAQAwDAICBq8CAQEEAwIBADAMAgIGsQIBAQQDAgEAMBsCAganAgEBBBIMEDEwMDAwMDA1MzgwOTQ3NTQwGwICBqkCAQEEEgwQMTAwMDAwMDUzODA5NDc1NDAdAgIGpgIBAQQUDBJjb20uZmFjZXRhbGsudmlwMzAwHwICBqgCAQEEFhYUMjAxOS0wNi0xOFQwNzoyMToyMVowHwICBqoCAQEEFhYUMjAxOS0wNi0xOFQwNzoyMToyMVowggFXAgERAgEBBIIBTTGCAUkwCwICBqwCAQEEAhYAMAsCAgatAgEBBAIMADALAgIGsAIBAQQCFgAwCwICBrICAQEEAgwAMAsCAgazAgEBBAIMADALAgIGtAIBAQQCDAAwCwICBrUCAQEEAgwAMAsCAga2AgEBBAIMADAMAgIGpQIBAQQDAgEBMAwCAgarAgEBBAMCAQIwDAICBq4CAQEEAwIBADAMAgIGrwIBAQQDAgEAMAwCAgaxAgEBBAMCAQAwGwICBqcCAQEEEgwQMTAwMDAwMDUzODExMjAxMTAbAgIGqQIBAQQSDBAxMDAwMDAwNTM4MTEyMDExMB0CAgamAgEBBBQMEmNvbS5mYWNldGFsay52aXAzMDAfAgIGqAIBAQQWFhQyMDE5LTA2LTE4VDA3OjUwOjAyWjAfAgIGqgIBAQQWFhQyMDE5LTA2LTE4VDA3OjUwOjAyWjCCAVcCARECAQEEggFNMYIBSTALAgIGrAIBAQQCFgAwCwICBq0CAQEEAgwAMAsCAgawAgEBBAIWADALAgIGsgIBAQQCDAAwCwICBrMCAQEEAgwAMAsCAga0AgEBBAIMADALAgIGtQIBAQQCDAAwCwICBrYCAQEEAgwAMAwCAgalAgEBBAMCAQEwDAICBqsCAQEEAwIBAjAMAgIGrgIBAQQDAgEAMAwCAgavAgEBBAMCAQAwDAICBrECAQEEAwIBADAbAgIGpwIBAQQSDBAxMDAwMDAwNTM4MTI2MzE5MBsCAgapAgEBBBIMEDEwMDAwMDA1MzgxMjYzMTkwHQICBqYCAQEEFAwSY29tLmZhY2V0YWxrLnZpcDMwMB8CAgaoAgEBBBYWFDIwMTktMDYtMThUMDg6MTU6MTlaMB8CAgaqAgEBBBYWFDIwMTktMDYtMThUMDg6MTU6MTlaMIIBVwIBEQIBAQSCAU0xggFJMAsCAgasAgEBBAIWADALAgIGrQIBAQQCDAAwCwICBrACAQEEAhYAMAsCAgayAgEBBAIMADALAgIGswIBAQQCDAAwCwICBrQCAQEEAgwAMAsCAga1AgEBBAIMADALAgIGtgIBAQQCDAAwDAICBqUCAQEEAwIBATAMAgIGqwIBAQQDAgECMAwCAgauAgEBBAMCAQAwDAICBq8CAQEEAwIBADAMAgIGsQIBAQQDAgEAMBsCAganAgEBBBIMEDEwMDAwMDA1MzgxMzI2MjMwGwICBqkCAQEEEgwQMTAwMDAwMDUzODEzMjYyMzAdAgIGpgIBAQQUDBJjb20uZmFjZXRhbGsudmlwMzAwHwICBqgCAQEEFhYUMjAxOS0wNi0xOFQwODoyOTo1NlowHwICBqoCAQEEFhYUMjAxOS0wNi0xOFQwODoyOTo1NlowggFXAgERAgEBBIIBTTGCAUkwCwICBqwCAQEEAhYAMAsCAgatAgEBBAIMADALAgIGsAIBAQQCFgAwCwICBrICAQEEAgwAMAsCAgazAgEBBAIMADALAgIGtAIBAQQCDAAwCwICBrUCAQEEAgwAMAsCAga2AgEBBAIMADAMAgIGpQIBAQQDAgEBMAwCAgarAgEBBAMCAQIwDAICBq4CAQEEAwIBADAMAgIGrwIBAQQDAgEAMAwCAgaxAgEBBAMCAQAwGwICBqcCAQEEEgwQMTAwMDAwMDUzODEzMzQ2OTAbAgIGqQIBAQQSDBAxMDAwMDAwNTM4MTMzNDY5MB0CAgamAgEBBBQMEmNvbS5mYWNldGFsay52aXAzMDAfAgIGqAIBAQQWFhQyMDE5LTA2LTE4VDA4OjMxOjQ2WjAfAgIGqgIBAQQWFhQyMDE5LTA2LTE4VDA4OjMxOjQ2WjCCAVcCARECAQEEggFNMYIBSTALAgIGrAIBAQQCFgAwCwICBq0CAQEEAgwAMAsCAgawAgEBBAIWADALAgIGsgIBAQQCDAAwCwICBrMCAQEEAgwAMAsCAga0AgEBBAIMADALAgIGtQIBAQQCDAAwCwICBrYCAQEEAgwAMAwCAgalAgEBBAMCAQEwDAICBqsCAQEEAwIBAjAMAgIGrgIBAQQDAgEAMAwCAgavAgEBBAMCAQAwDAICBrECAQEEAwIBADAbAgIGpwIBAQQSDBAxMDAwMDAwNTM4MTM2MTc3MBsCAgapAgEBBBIMEDEwMDAwMDA1MzgxMzYxNzcwHQICBqYCAQEEFAwSY29tLmZhY2V0YWxrLnZpcDMwMB8CAgaoAgEBBBYWFDIwMTktMDYtMThUMDg6Mzc6MDRaMB8CAgaqAgEBBBYWFDIwMTktMDYtMThUMDg6Mzc6MDRaMIIBVwIBEQIBAQSCAU0xggFJMAsCAgasAgEBBAIWADALAgIGrQIBAQQCDAAwCwICBrACAQEEAhYAMAsCAgayAgEBBAIMADALAgIGswIBAQQCDAAwCwICBrQCAQEEAgwAMAsCAga1AgEBBAIMADALAgIGtgIBAQQCDAAwDAICBqUCAQEEAwIBATAMAgIGqwIBAQQDAgECMAwCAgauAgEBBAMCAQAwDAICBq8CAQEEAwIBADAMAgIGsQIBAQQDAgEAMBsCAganAgEBBBIMEDEwMDAwMDA1MzgxNDQwOTAwGwICBqkCAQEEEgwQMTAwMDAwMDUzODE0NDA5MDAdAgIGpgIBAQQUDBJjb20uZmFjZXRhbGsudmlwMzAwHwICBqgCAQEEFhYUMjAxOS0wNi0xOFQwODo1NDoyMVowHwICBqoCAQEEFhYUMjAxOS0wNi0xOFQwODo1NDoyMVowggFXAgERAgEBBIIBTTGCAUkwCwICBqwCAQEEAhYAMAsCAgatAgEBBAIMADALAgIGsAIBAQQCFgAwCwICBrICAQEEAgwAMAsCAgazAgEBBAIMADALAgIGtAIBAQQCDAAwCwICBrUCAQEEAgwAMAsCAga2AgEBBAIMADAMAgIGpQIBAQQDAgEBMAwCAgarAgEBBAMCAQIwDAICBq4CAQEEAwIBADAMAgIGrwIBAQQDAgEAMAwCAgaxAgEBBAMCAQAwGwICBqcCAQEEEgwQMTAwMDAwMDUzODQ1NTIwMjAbAgIGqQIBAQQSDBAxMDAwMDAwNTM4NDU1MjAyMB0CAgamAgEBBBQMEmNvbS5mYWNldGFsay52aXAzMDAfAgIGqAIBAQQWFhQyMDE5LTA2LTE5VDA2OjI0OjQxWjAfAgIGqgIBAQQWFhQyMDE5LTA2LTE5VDA2OjI0OjQxWjCCAVcCARECAQEEggFNMYIBSTALAgIGrAIBAQQCFgAwCwICBq0CAQEEAgwAMAsCAgawAgEBBAIWADALAgIGsgIBAQQCDAAwCwICBrMCAQEEAgwAMAsCAga0AgEBBAIMADALAgIGtQIBAQQCDAAwCwICBrYCAQEEAgwAMAwCAgalAgEBBAMCAQEwDAICBqsCAQEEAwIBAjAMAgIGrgIBAQQDAgEAMAwCAgavAgEBBAMCAQAwDAICBrECAQEEAwIBADAbAgIGpwIBAQQSDBAxMDAwMDAwNTM4NDk0MTc3MBsCAgapAgEBBBIMEDEwMDAwMDA1Mzg0OTQxNzcwHQICBqYCAQEEFAwSY29tLmZhY2V0YWxrLnZpcDMwMB8CAgaoAgEBBBYWFDIwMTktMDYtMTlUMDc6NTc6MDdaMB8CAgaqAgEBBBYWFDIwMTktMDYtMTlUMDc6NTc6MDdaMIIBVwIBEQIBAQSCAU0xggFJMAsCAgasAgEBBAIWADALAgIGrQIBAQQCDAAwCwICBrACAQEEAhYAMAsCAgayAgEBBAIMADALAgIGswIBAQQCDAAwCwICBrQCAQEEAgwAMAsCAga1AgEBBAIMADALAgIGtgIBAQQCDAAwDAICBqUCAQEEAwIBATAMAgIGqwIBAQQDAgECMAwCAgauAgEBBAMCAQAwDAICBq8CAQEEAwIBADAMAgIGsQIBAQQDAgEAMBsCAganAgEBBBIMEDEwMDAwMDA1Mzg0OTY4NjcwGwICBqkCAQEEEgwQMTAwMDAwMDUzODQ5Njg2NzAdAgIGpgIBAQQUDBJjb20uZmFjZXRhbGsudmlwMzAwHwICBqgCAQEEFhYUMjAxOS0wNi0xOVQwODowNTo0N1owHwICBqoCAQEEFhYUMjAxOS0wNi0xOVQwODowNTo0N1owggFXAgERAgEBBIIBTTGCAUkwCwICBqwCAQEEAhYAMAsCAgatAgEBBAIMADALAgIGsAIBAQQCFgAwCwICBrICAQEEAgwAMAsCAgazAgEBBAIMADALAgIGtAIBAQQCDAAwCwICBrUCAQEEAgwAMAsCAga2AgEBBAIMADAMAgIGpQIBAQQDAgEBMAwCAgarAgEBBAMCAQIwDAICBq4CAQEEAwIBADAMAgIGrwIBAQQDAgEAMAwCAgaxAgEBBAMCAQAwGwICBqcCAQEEEgwQMTAwMDAwMDUzODQ5OTAzODAbAgIGqQIBAQQSDBAxMDAwMDAwNTM4NDk5MDM4MB0CAgamAgEBBBQMEmNvbS5mYWNldGFsay52aXAzMDAfAgIGqAIBAQQWFhQyMDE5LTA2LTE5VDA4OjEwOjQ4WjAfAgIGqgIBAQQWFhQyMDE5LTA2LTE5VDA4OjEwOjQ4WjCCAVcCARECAQEEggFNMYIBSTALAgIGrAIBAQQCFgAwCwICBq0CAQEEAgwAMAsCAgawAgEBBAIWADALAgIGsgIBAQQCDAAwCwICBrMCAQEEAgwAMAsCAga0AgEBBAIMADALAgIGtQIBAQQCDAAwCwICBrYCAQEEAgwAMAwCAgalAgEBBAMCAQEwDAICBqsCAQEEAwIBAjAMAgIGrgIBAQQDAgEAMAwCAgavAgEBBAMCAQAwDAICBrECAQEEAwIBADAbAgIGpwIBAQQSDBAxMDAwMDAwNTM4NDk5MjU4MBsCAgapAgEBBBIMEDEwMDAwMDA1Mzg0OTkyNTgwHQICBqYCAQEEFAwSY29tLmZhY2V0YWxrLnZpcDMwMB8CAgaoAgEBBBYWFDIwMTktMDYtMTlUMDg6MTE6MjhaMB8CAgaqAgEBBBYWFDIwMTktMDYtMTlUMDg6MTE6MjhaMIIBVwIBEQIBAQSCAU0xggFJMAsCAgasAgEBBAIWADALAgIGrQIBAQQCDAAwCwICBrACAQEEAhYAMAsCAgayAgEBBAIMADALAgIGswIBAQQCDAAwCwICBrQCAQEEAgwAMAsCAga1AgEBBAIMADALAgIGtgIBAQQCDAAwDAICBqUCAQEEAwIBATAMAgIGqwIBAQQDAgECMAwCAgauAgEBBAMCAQAwDAICBq8CAQEEAwIBADAMAgIGsQIBAQQDAgEAMBsCAganAgEBBBIMEDEwMDAwMDA1Mzg1MDA5NjMwGwICBqkCAQEEEgwQMTAwMDAwMDUzODUwMDk2MzAdAgIGpgIBAQQUDBJjb20uZmFjZXRhbGsudmlwMzAwHwICBqgCAQEEFhYUMjAxOS0wNi0xOVQwODoxNDoyN1owHwICBqoCAQEEFhYUMjAxOS0wNi0xOVQwODoxNDoyN1owggFXAgERAgEBBIIBTTGCAUkwCwICBqwCAQEEAhYAMAsCAgatAgEBBAIMADALAgIGsAIBAQQCFgAwCwICBrICAQEEAgwAMAsCAgazAgEBBAIMADALAgIGtAIBAQQCDAAwCwICBrUCAQEEAgwAMAsCAga2AgEBBAIMADAMAgIGpQIBAQQDAgEBMAwCAgarAgEBBAMCAQIwDAICBq4CAQEEAwIBADAMAgIGrwIBAQQDAgEAMAwCAgaxAgEBBAMCAQAwGwICBqcCAQEEEgwQMTAwMDAwMDUzODUwMTE5MTAbAgIGqQIBAQQSDBAxMDAwMDAwNTM4NTAxMTkxMB0CAgamAgEBBBQMEmNvbS5mYWNldGFsay52aXAzMDAfAgIGqAIBAQQWFhQyMDE5LTA2LTE5VDA4OjE1OjA5WjAfAgIGqgIBAQQWFhQyMDE5LTA2LTE5VDA4OjE1OjA5WjCCAVcCARECAQEEggFNMYIBSTALAgIGrAIBAQQCFgAwCwICBq0CAQEEAgwAMAsCAgawAgEBBAIWADALAgIGsgIBAQQCDAAwCwICBrMCAQEEAgwAMAsCAga0AgEBBAIMADALAgIGtQIBAQQCDAAwCwICBrYCAQEEAgwAMAwCAgalAgEBBAMCAQEwDAICBqsCAQEEAwIBAjAMAgIGrgIBAQQDAgEAMAwCAgavAgEBBAMCAQAwDAICBrECAQEEAwIBADAbAgIGpwIBAQQSDBAxMDAwMDAwNTM4NTAxNDM3MBsCAgapAgEBBBIMEDEwMDAwMDA1Mzg1MDE0MzcwHQICBqYCAQEEFAwSY29tLmZhY2V0YWxrLnZpcDMwMB8CAgaoAgEBBBYWFDIwMTktMDYtMTlUMDg6MTY6MDlaMB8CAgaqAgEBBBYWFDIwMTktMDYtMTlUMDg6MTY6MDlaMIIBVwIBEQIBAQSCAU0xggFJMAsCAgasAgEBBAIWADALAgIGrQIBAQQCDAAwCwICBrACAQEEAhYAMAsCAgayAgEBBAIMADALAgIGswIBAQQCDAAwCwICBrQCAQEEAgwAMAsCAga1AgEBBAIMADALAgIGtgIBAQQCDAAwDAICBqUCAQEEAwIBATAMAgIGqwIBAQQDAgECMAwCAgauAgEBBAMCAQAwDAICBq8CAQEEAwIBADAMAgIGsQIBAQQDAgEAMBsCAganAgEBBBIMEDEwMDAwMDA1Mzg1MDg2OTcwGwICBqkCAQEEEgwQMTAwMDAwMDUzODUwODY5NzAdAgIGpgIBAQQUDBJjb20uZmFjZXRhbGsudmlwMzAwHwICBqgCAQEEFhYUMjAxOS0wNi0xOVQwODozMTo1NlowHwICBqoCAQEEFhYUMjAxOS0wNi0xOVQwODozMTo1NlowggFXAgERAgEBBIIBTTGCAUkwCwICBqwCAQEEAhYAMAsCAgatAgEBBAIMADALAgIGsAIBAQQCFgAwCwICBrICAQEEAgwAMAsCAgazAgEBBAIMADALAgIGtAIBAQQCDAAwCwICBrUCAQEEAgwAMAsCAga2AgEBBAIMADAMAgIGpQIBAQQDAgEBMAwCAgarAgEBBAMCAQIwDAICBq4CAQEEAwIBADAMAgIGrwIBAQQDAgEAMAwCAgaxAgEBBAMCAQAwGwICBqcCAQEEEgwQMTAwMDAwMDUzODUxMTQ0MzAbAgIGqQIBAQQSDBAxMDAwMDAwNTM4NTExNDQzMB0CAgamAgEBBBQMEmNvbS5mYWNldGFsay52aXAzMDAfAgIGqAIBAQQWFhQyMDE5LTA2LTE5VDA4OjM2OjIzWjAfAgIGqgIBAQQWFhQyMDE5LTA2LTE5VDA4OjM2OjIzWjCCAVcCARECAQEEggFNMYIBSTALAgIGrAIBAQQCFgAwCwICBq0CAQEEAgwAMAsCAgawAgEBBAIWADALAgIGsgIBAQQCDAAwCwICBrMCAQEEAgwAMAsCAga0AgEBBAIMADALAgIGtQIBAQQCDAAwCwICBrYCAQEEAgwAMAwCAgalAgEBBAMCAQEwDAICBqsCAQEEAwIBAjAMAgIGrgIBAQQDAgEAMAwCAgavAgEBBAMCAQAwDAICBrECAQEEAwIBADAbAgIGpwIBAQQSDBAxMDAwMDAwNTM4NTIwMTM3MBsCAgapAgEBBBIMEDEwMDAwMDA1Mzg1MjAxMzcwHQICBqYCAQEEFAwSY29tLmZhY2V0YWxrLnZpcDMwMB8CAgaoAgEBBBYWFDIwMTktMDYtMTlUMDg6NTA6MjFaMB8CAgaqAgEBBBYWFDIwMTktMDYtMTlUMDg6NTA6MjFaMIIBVwIBEQIBAQSCAU0xggFJMAsCAgasAgEBBAIWADALAgIGrQIBAQQCDAAwCwICBrACAQEEAhYAMAsCAgayAgEBBAIMADALAgIGswIBAQQCDAAwCwICBrQCAQEEAgwAMAsCAga1AgEBBAIMADALAgIGtgIBAQQCDAAwDAICBqUCAQEEAwIBATAMAgIGqwIBAQQDAgECMAwCAgauAgEBBAMCAQAwDAICBq8CAQEEAwIBADAMAgIGsQIBAQQDAgEAMBsCAganAgEBBBIMEDEwMDAwMDA1Mzg1MjMzODAwGwICBqkCAQEEEgwQMTAwMDAwMDUzODUyMzM4MDAdAgIGpgIBAQQUDBJjb20uZmFjZXRhbGsudmlwMzAwHwICBqgCAQEEFhYUMjAxOS0wNi0xOVQwODo1NTozNVowHwICBqoCAQEEFhYUMjAxOS0wNi0xOVQwODo1NTozNVowggFXAgERAgEBBIIBTTGCAUkwCwICBqwCAQEEAhYAMAsCAgatAgEBBAIMADALAgIGsAIBAQQCFgAwCwICBrICAQEEAgwAMAsCAgazAgEBBAIMADALAgIGtAIBAQQCDAAwCwICBrUCAQEEAgwAMAsCAga2AgEBBAIMADAMAgIGpQIBAQQDAgEBMAwCAgarAgEBBAMCAQIwDAICBq4CAQEEAwIBADAMAgIGrwIBAQQDAgEAMAwCAgaxAgEBBAMCAQAwGwICBqcCAQEEEgwQMTAwMDAwMDUzODUyMzg3MjAbAgIGqQIBAQQSDBAxMDAwMDAwNTM4NTIzODcyMB0CAgamAgEBBBQMEmNvbS5mYWNldGFsay52aXAzMDAfAgIGqAIBAQQWFhQyMDE5LTA2LTE5VDA4OjU2OjEzWjAfAgIGqgIBAQQWFhQyMDE5LTA2LTE5VDA4OjU2OjEzWjCCAVcCARECAQEEggFNMYIBSTALAgIGrAIBAQQCFgAwCwICBq0CAQEEAgwAMAsCAgawAgEBBAIWADALAgIGsgIBAQQCDAAwCwICBrMCAQEEAgwAMAsCAga0AgEBBAIMADALAgIGtQIBAQQCDAAwCwICBrYCAQEEAgwAMAwCAgalAgEBBAMCAQEwDAICBqsCAQEEAwIBAjAMAgIGrgIBAQQDAgEAMAwCAgavAgEBBAMCAQAwDAICBrECAQEEAwIBADAbAgIGpwIBAQQSDBAxMDAwMDAwNTM4NTI2MTAwMBsCAgapAgEBBBIMEDEwMDAwMDA1Mzg1MjYxMDAwHQICBqYCAQEEFAwSY29tLmZhY2V0YWxrLnZpcDMwMB8CAgaoAgEBBBYWFDIwMTktMDYtMTlUMDk6MDA6NTJaMB8CAgaqAgEBBBYWFDIwMTktMDYtMTlUMDk6MDA6NTJaMIIBVwIBEQIBAQSCAU0xggFJMAsCAgasAgEBBAIWADALAgIGrQIBAQQCDAAwCwICBrACAQEEAhYAMAsCAgayAgEBBAIMADALAgIGswIBAQQCDAAwCwICBrQCAQEEAgwAMAsCAga1AgEBBAIMADALAgIGtgIBAQQCDAAwDAICBqUCAQEEAwIBATAMAgIGqwIBAQQDAgECMAwCAgauAgEBBAMCAQAwDAICBq8CAQEEAwIBADAMAgIGsQIBAQQDAgEAMBsCAganAgEBBBIMEDEwMDAwMDA1Mzg1MjcyODkwGwICBqkCAQEEEgwQMTAwMDAwMDUzODUyNzI4OTAdAgIGpgIBAQQUDBJjb20uZmFjZXRhbGsudmlwMzAwHwICBqgCAQEEFhYUMjAxOS0wNi0xOVQwOTowMjowMVowHwICBqoCAQEEFhYUMjAxOS0wNi0xOVQwOTowMjowMVowggFXAgERAgEBBIIBTTGCAUkwCwICBqwCAQEEAhYAMAsCAgatAgEBBAIMADALAgIGsAIBAQQCFgAwCwICBrICAQEEAgwAMAsCAgazAgEBBAIMADALAgIGtAIBAQQCDAAwCwICBrUCAQEEAgwAMAsCAga2AgEBBAIMADAMAgIGpQIBAQQDAgEBMAwCAgarAgEBBAMCAQIwDAICBq4CAQEEAwIBADAMAgIGrwIBAQQDAgEAMAwCAgaxAgEBBAMCAQAwGwICBqcCAQEEEgwQMTAwMDAwMDUzODUyOTA2OTAbAgIGqQIBAQQSDBAxMDAwMDAwNTM4NTI5MDY5MB0CAgamAgEBBBQMEmNvbS5mYWNldGFsay52aXAzMDAfAgIGqAIBAQQWFhQyMDE5LTA2LTE5VDA5OjA1OjI2WjAfAgIGqgIBAQQWFhQyMDE5LTA2LTE5VDA5OjA1OjI2WqCCDmUwggV8MIIEZKADAgECAggO61eH554JjTANBgkqhkiG9w0BAQUFADCBljELMAkGA1UEBhMCVVMxEzARBgNVBAoMCkFwcGxlIEluYy4xLDAqBgNVBAsMI0FwcGxlIFdvcmxkd2lkZSBEZXZlbG9wZXIgUmVsYXRpb25zMUQwQgYDVQQDDDtBcHBsZSBXb3JsZHdpZGUgRGV2ZWxvcGVyIFJlbGF0aW9ucyBDZXJ0aWZpY2F0aW9uIEF1dGhvcml0eTAeFw0xNTExMTMwMjE1MDlaFw0yMzAyMDcyMTQ4NDdaMIGJMTcwNQYDVQQDDC5NYWMgQXBwIFN0b3JlIGFuZCBpVHVuZXMgU3RvcmUgUmVjZWlwdCBTaWduaW5nMSwwKgYDVQQLDCNBcHBsZSBXb3JsZHdpZGUgRGV2ZWxvcGVyIFJlbGF0aW9uczETMBEGA1UECgwKQXBwbGUgSW5jLjELMAkGA1UEBhMCVVMwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQClz4H9JaKBW9aH7SPaMxyO4iPApcQmyz3Gn+xKDVWG/6QC15fKOVRtfX+yVBidxCxScY5ke4LOibpJ1gjltIhxzz9bRi7GxB24A6lYogQ+IXjV27fQjhKNg0xbKmg3k8LyvR7E0qEMSlhSqxLj7d0fmBWQNS3CzBLKjUiB91h4VGvojDE2H0oGDEdU8zeQuLKSiX1fpIVK4cCc4Lqku4KXY/Qrk8H9Pm/KwfU8qY9SGsAlCnYO3v6Z/v/Ca/VbXqxzUUkIVonMQ5DMjoEC0KCXtlyxoWlph5AQaCYmObgdEHOwCl3Fc9DfdjvYLdmIHuPsB8/ijtDT+iZVge/iA0kjAgMBAAGjggHXMIIB0zA/BggrBgEFBQcBAQQzMDEwLwYIKwYBBQUHMAGGI2h0dHA6Ly9vY3NwLmFwcGxlLmNvbS9vY3NwMDMtd3dkcjA0MB0GA1UdDgQWBBSRpJz8xHa3n6CK9E31jzZd7SsEhTAMBgNVHRMBAf8EAjAAMB8GA1UdIwQYMBaAFIgnFwmpthhgi+zruvZHWcVSVKO3MIIBHgYDVR0gBIIBFTCCAREwggENBgoqhkiG92NkBQYBMIH+MIHDBggrBgEFBQcCAjCBtgyBs1JlbGlhbmNlIG9uIHRoaXMgY2VydGlmaWNhdGUgYnkgYW55IHBhcnR5IGFzc3VtZXMgYWNjZXB0YW5jZSBvZiB0aGUgdGhlbiBhcHBsaWNhYmxlIHN0YW5kYXJkIHRlcm1zIGFuZCBjb25kaXRpb25zIG9mIHVzZSwgY2VydGlmaWNhdGUgcG9saWN5IGFuZCBjZXJ0aWZpY2F0aW9uIHByYWN0aWNlIHN0YXRlbWVudHMuMDYGCCsGAQUFBwIBFipodHRwOi8vd3d3LmFwcGxlLmNvbS9jZXJ0aWZpY2F0ZWF1dGhvcml0eS8wDgYDVR0PAQH/BAQDAgeAMBAGCiqGSIb3Y2QGCwEEAgUAMA0GCSqGSIb3DQEBBQUAA4IBAQANphvTLj3jWysHbkKWbNPojEMwgl/gXNGNvr0PvRr8JZLbjIXDgFnf4+LXLgUUrA3btrj+/DUufMutF2uOfx/kd7mxZ5W0E16mGYZ2+FogledjjA9z/Ojtxh+umfhlSFyg4Cg6wBA3LbmgBDkfc7nIBf3y3n8aKipuKwH8oCBc2et9J6Yz+PWY4L5E27FMZ/xuCk/J4gao0pfzp45rUaJahHVl0RYEYuPBX/UIqc9o2ZIAycGMs/iNAGS6WGDAfK+PdcppuVsq1h1obphC9UynNxmbzDscehlD86Ntv0hgBgw2kivs3hi1EdotI9CO/KBpnBcbnoB7OUdFMGEvxxOoMIIEIjCCAwqgAwIBAgIIAd68xDltoBAwDQYJKoZIhvcNAQEFBQAwYjELMAkGA1UEBhMCVVMxEzARBgNVBAoTCkFwcGxlIEluYy4xJjAkBgNVBAsTHUFwcGxlIENlcnRpZmljYXRpb24gQXV0aG9yaXR5MRYwFAYDVQQDEw1BcHBsZSBSb290IENBMB4XDTEzMDIwNzIxNDg0N1oXDTIzMDIwNzIxNDg0N1owgZYxCzAJBgNVBAYTAlVTMRMwEQYDVQQKDApBcHBsZSBJbmMuMSwwKgYDVQQLDCNBcHBsZSBXb3JsZHdpZGUgRGV2ZWxvcGVyIFJlbGF0aW9uczFEMEIGA1UEAww7QXBwbGUgV29ybGR3aWRlIERldmVsb3BlciBSZWxhdGlvbnMgQ2VydGlmaWNhdGlvbiBBdXRob3JpdHkwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQDKOFSmy1aqyCQ5SOmM7uxfuH8mkbw0U3rOfGOAYXdkXqUHI7Y5/lAtFVZYcC1+xG7BSoU+L/DehBqhV8mvexj/avoVEkkVCBmsqtsqMu2WY2hSFT2Miuy/axiV4AOsAX2XBWfODoWVN2rtCbauZ81RZJ/GXNG8V25nNYB2NqSHgW44j9grFU57Jdhav06DwY3Sk9UacbVgnJ0zTlX5ElgMhrgWDcHld0WNUEi6Ky3klIXh6MSdxmilsKP8Z35wugJZS3dCkTm59c3hTO/AO0iMpuUhXf1qarunFjVg0uat80YpyejDi+l5wGphZxWy8P3laLxiX27Pmd3vG2P+kmWrAgMBAAGjgaYwgaMwHQYDVR0OBBYEFIgnFwmpthhgi+zruvZHWcVSVKO3MA8GA1UdEwEB/wQFMAMBAf8wHwYDVR0jBBgwFoAUK9BpR5R2Cf70a40uQKb3R01/CF4wLgYDVR0fBCcwJTAjoCGgH4YdaHR0cDovL2NybC5hcHBsZS5jb20vcm9vdC5jcmwwDgYDVR0PAQH/BAQDAgGGMBAGCiqGSIb3Y2QGAgEEAgUAMA0GCSqGSIb3DQEBBQUAA4IBAQBPz+9Zviz1smwvj+4ThzLoBTWobot9yWkMudkXvHcs1Gfi/ZptOllc34MBvbKuKmFysa/Nw0Uwj6ODDc4dR7Txk4qjdJukw5hyhzs+r0ULklS5MruQGFNrCk4QttkdUGwhgAqJTleMa1s8Pab93vcNIx0LSiaHP7qRkkykGRIZbVf1eliHe2iK5IaMSuviSRSqpd1VAKmuu0swruGgsbwpgOYJd+W+NKIByn/c4grmO7i77LpilfMFY0GCzQ87HUyVpNur+cmV6U/kTecmmYHpvPm0KdIBembhLoz2IYrF+Hjhga6/05Cdqa3zr/04GpZnMBxRpVzscYqCtGwPDBUfMIIEuzCCA6OgAwIBAgIBAjANBgkqhkiG9w0BAQUFADBiMQswCQYDVQQGEwJVUzETMBEGA1UEChMKQXBwbGUgSW5jLjEmMCQGA1UECxMdQXBwbGUgQ2VydGlmaWNhdGlvbiBBdXRob3JpdHkxFjAUBgNVBAMTDUFwcGxlIFJvb3QgQ0EwHhcNMDYwNDI1MjE0MDM2WhcNMzUwMjA5MjE0MDM2WjBiMQswCQYDVQQGEwJVUzETMBEGA1UEChMKQXBwbGUgSW5jLjEmMCQGA1UECxMdQXBwbGUgQ2VydGlmaWNhdGlvbiBBdXRob3JpdHkxFjAUBgNVBAMTDUFwcGxlIFJvb3QgQ0EwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQDkkakJH5HbHkdQ6wXtXnmELes2oldMVeyLGYne+Uts9QerIjAC6Bg++FAJ039BqJj50cpmnCRrEdCju+QbKsMflZ56DKRHi1vUFjczy8QPTc4UadHJGXL1XQ7Vf1+b8iUDulWPTV0N8WQ1IxVLFVkds5T39pyez1C6wVhQZ48ItCD3y6wsIG9wtj8BMIy3Q88PnT3zK0koGsj+zrW5DtleHNbLPbU6rfQPDgCSC7EhFi501TwN22IWq6NxkkdTVcGvL0Gz+PvjcM3mo0xFfh9Ma1CWQYnEdGILEINBhzOKgbEwWOxaBDKMaLOPHd5lc/9nXmW8Sdh2nzMUZaF3lMktAgMBAAGjggF6MIIBdjAOBgNVHQ8BAf8EBAMCAQYwDwYDVR0TAQH/BAUwAwEB/zAdBgNVHQ4EFgQUK9BpR5R2Cf70a40uQKb3R01/CF4wHwYDVR0jBBgwFoAUK9BpR5R2Cf70a40uQKb3R01/CF4wggERBgNVHSAEggEIMIIBBDCCAQAGCSqGSIb3Y2QFATCB8jAqBggrBgEFBQcCARYeaHR0cHM6Ly93d3cuYXBwbGUuY29tL2FwcGxlY2EvMIHDBggrBgEFBQcCAjCBthqBs1JlbGlhbmNlIG9uIHRoaXMgY2VydGlmaWNhdGUgYnkgYW55IHBhcnR5IGFzc3VtZXMgYWNjZXB0YW5jZSBvZiB0aGUgdGhlbiBhcHBsaWNhYmxlIHN0YW5kYXJkIHRlcm1zIGFuZCBjb25kaXRpb25zIG9mIHVzZSwgY2VydGlmaWNhdGUgcG9saWN5IGFuZCBjZXJ0aWZpY2F0aW9uIHByYWN0aWNlIHN0YXRlbWVudHMuMA0GCSqGSIb3DQEBBQUAA4IBAQBcNplMLXi37Yyb3PN3m/J20ncwT8EfhYOFG5k9RzfyqZtAjizUsZAS2L70c5vu0mQPy3lPNNiiPvl4/2vIB+x9OYOLUyDTOMSxv5pPCmv/K/xZpwUJfBdAVhEedNO3iyM7R6PVbyTi69G3cN8PReEnyvFteO3ntRcXqNx+IjXKJdXZD9Zr1KIkIxH3oayPc4FgxhtbCS+SsvhESPBgOJ4V9T0mZyCKM2r3DYLP3uujL/lTaltkwGMzd/c6ByxW69oPIQ7aunMZT7XZNn/Bh1XZp5m5MkL72NVxnn6hUrcbvZNCJBIqxw8dtk2cXmPIS4AXUKqK1drk/NAJBzewdXUhMYIByzCCAccCAQEwgaMwgZYxCzAJBgNVBAYTAlVTMRMwEQYDVQQKDApBcHBsZSBJbmMuMSwwKgYDVQQLDCNBcHBsZSBXb3JsZHdpZGUgRGV2ZWxvcGVyIFJlbGF0aW9uczFEMEIGA1UEAww7QXBwbGUgV29ybGR3aWRlIERldmVsb3BlciBSZWxhdGlvbnMgQ2VydGlmaWNhdGlvbiBBdXRob3JpdHkCCA7rV4fnngmNMAkGBSsOAwIaBQAwDQYJKoZIhvcNAQEBBQAEggEAP/X9uhVRppRAJprBiZ/54nGYEeSc4+YnOT7HUULot6ZUi2Dzx7xdJdvy9vG4QsC4Tb0luEjNyQrpvB+9wePhije1moDdIaPhwhMrQUWeAO5s18EYG23Gl+HY3gHUlQOaZ88hYLNrYkNMk0GoB2s/hXBrWq6+5nqkZqoQBsYwjYb/hEat00KWHWCNETLmnahv7aWYutjZquh1n5eksjw6prbvKs4DPkIi9b4CfhSBu02/GiP5guo5GWkvVdRTx/Ln2+a6mIECmaVnOSpQ7ZBIqy1qKi2CEi49ZFZIYJwr2j1didsa9f3/sZQGomDIImya3Cy3PqCwSKhOcbq8fqrm0A==";
            //string time = "1560935127";
            //string amount = "198";
            //string ntype = "2";   //1-秘币 2 vip

            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(userIdx) || string.IsNullOrEmpty(receiptData))
            {
                return Json(new Result { code = "101", msg = "参数错误" });
            }
            string k = userIdx + time + "&mianju.tiange.com" + amount;
            string myKey = CryptoHelper.ToMD5(userIdx + time + "&mianju.tiange.com"+ amount).ToLower();

            
            if (!myKey.Equals(key))
            {
                return Json(new Result { code = "105", msg = "key错误" });
            }
            var timeot = GetDateTime(time);
            if (timeot.AddMinutes(1) < DateTime.Now)
            {
                mr.code = "102";
                mr.msg = "请求超时";
                return Json(mr, JsonRequestBehavior.AllowGet);
            }

            //验证票据
            var verify = VerifyData( receiptData) ?? new VerifyReceipt();
            if (verify == null)
            {
                mr.code = "110";
                mr.msg = "票据有问题！";
                return Json(mr, JsonRequestBehavior.AllowGet);
            }
            if (verify.Status != 0)//票据验证不通过
            {
                mr.code = "112";
                mr.msg = "票据验证不通过";
                LogHelper.WriteLog(LogFile.IOSPay, "票据验证不通过by_" + userIdx + ",v：" );
                return Json(mr, JsonRequestBehavior.AllowGet);
            }
            //成功
            if (verify.Status == 0)
            {
                PayBLL bll = new PayBLL();
                #region  订单生成
                int Order = 0;
                bll.UNpayOrderInsert(userIdx, amount, "1", "IOSPay", ntype, "3", "mianju", out Order);
                if (Order == 0)
                {
                    mr.code = "103";
                    mr.msg = "验证成功，订单生成失败";
                    LogHelper.WriteLog(LogFile.IOSPay, "订单生成失败");
                    return Json(mr, JsonRequestBehavior.AllowGet);
                }

                #endregion

                #region 处理订单
                string Useridx = "0";
                //更新订单表
                int result = bll.UNpayOrderUpdate(Order.ToString(), verify.Receipt.In_app[0].transaction_id, (decimal.Parse(amount) * 100).ToString(), ref Useridx);
                LogHelper.WriteLog(LogFile.IOSPay, "异步回调~更新订单result：" + result);
                if (result > 0)
                {
                    //加币 处理 //=0加币成功 =1 失败
                    if (bll.UNpayUpdate(Order.ToString()) == 1)
                    {
                        LogHelper.WriteLog(LogFile.IOSPay, "逻辑处理成功：useridx" + Useridx+"订单号"+ Order.ToString());
                        mr.code = "100";
                        mr.msg = "充值成功";
                        return Json(mr, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        LogHelper.WriteLog(LogFile.IOSPay, "异步回调~加币失败" + Order);
                        mr.code = "104";
                        mr.msg = "充值失败，请联系客服";
                        return Json(mr, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    //充值订单和实际充值不对等
                    LogHelper.WriteLog(LogFile.IOSPay, "订单更新失败" + Order.ToString() + "金额:" + amount);
                    mr.code = "104";
                    mr.msg = "充值失败，请联系客服";
                    return Json(mr, JsonRequestBehavior.AllowGet);
                }
                #endregion

            }
            return Json(mr, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 提交数据到apple 验证订单数据
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static VerifyReceipt VerifyData( string data)
        {
            var json = "{\"receipt-data\":\"" + data + "\"}";
            string sendresponsetext = string.Empty;
            try
            {
                byte[] postBytes = Encoding.UTF8.GetBytes(json);
                var url = "";
                //当前审核版本时用沙盒地址
                if (1==1)
                {
                    url = "https://sandbox.itunes.apple.com/verifyReceipt";
                    //LogHelper.WriteLog(LogFile.IOSPay, "苹果充值沙盒充值：curVersion:" );
                }
                else
                {
                    url = "https://buy.itunes.apple.com/verifyReceipt";
                }
                var request = System.Net.WebRequest.Create(url);

                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = postBytes.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(postBytes, 0, postBytes.Length);
                    stream.Flush();
                }
                var sendresponse = request.GetResponse();
                using (var streamReader = new StreamReader(sendresponse.GetResponseStream()))
                {
                    sendresponsetext = streamReader.ReadToEnd().Trim();
                }
                return JsonConvert.DeserializeObject<VerifyReceipt>(sendresponsetext);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogFile.IOSPay, "app store 票据验证异常" + ex.ToString() + json);
            }
            return null;
        }

        private DateTime GetDateTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime targetDt = dtStart.Add(toNow);
            return dtStart.Add(toNow);
        }  
        #endregion

        #region 支付宝充值 已停用
        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        //public SortedDictionary<string, string> GetRequestPost()
        //{
        //    int i = 0;
        //    SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
        //    NameValueCollection coll;
        //    //Load Form variables into NameValueCollection variable.
        //    coll = Request.Form;

        //    // Get names of all forms into a string array.
        //    String[] requestItem = coll.AllKeys;

        //    for (i = 0; i < requestItem.Length; i++)
        //    {
        //        sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
        //    }

        //    return sArray;
        //}
        /// <summary>
        /// 支付宝支付异步通知
        /// </summary>
        /// <returns></returns>
        //public ActionResult Alipaynotify()
        //{
        //    SortedDictionary<string, string> sPara = GetRequestPost();

        //    if (sPara.Count > 0)//判断是否有带返回参数
        //    {
        //        WebAPI.AliPay.Notify aliNotify = new AliPay.Notify();
        //        bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);

        //        if (verifyResult)//验证成功
        //        {
        //            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //            //请在这里加上商户的业务逻辑程序代码
        //            string trade_no = Request.Form["trade_no"];         //支付宝交易号
        //            string order_no = Request.Form["out_trade_no"];     //获取订单号
        //            string total_fee = Request.Form["total_fee"];       //获取总金额
        //            string subject = Request.Form["subject"];           //商品名称、订单名称
        //            //string body = Request.Form["body"];                 //商品描述、订单备注、描述
        //            string buyer_email = Request.Form["buyer_email"];   //买家支付宝账号
        //            string trade_status = Request.Form["trade_status"]; //交易状态

        //            //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
        //            //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表

        //            //商户订单号string out_trade_no = Request.Form["out_trade_no"];
        //            //支付宝交易号string trade_no = Request.Form["trade_no"];
        //            //交易状态string trade_status = Request.Form["trade_status"];

        //            // Loger.WriteLog(LogFile.AliPay, "订单号 =" + order_no.ToString() +"返回参数："+ Request.Form.ToString());

        //            if (Request.Form["trade_status"] == "TRADE_FINISHED")
        //            {
        //                //判断该笔订单是否在商户网站中已经做过处理
        //                //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
        //                //如果有做过处理，不执行商户的业务程序

        //                //注意：
        //                //退款日期超过可退款期限后（如三个月可退款），支付宝系统发送该交易状态通知
        //                //请务必判断请求时的total_fee、seller_id与通知时获取的total_fee、seller_id为一致的
        //                return Content("success");
        //            }
        //            else if (Request.Form["trade_status"] == "TRADE_SUCCESS")
        //            {
        //                //判断该笔订单是否在商户网站中已经做过处理
        //                //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
        //                //如果有做过处理，不执行商户的业务程序
        //                //Loger.WriteLog(LogFile.AliPay, "订单号 =" + order_no.ToString() + " 支付宝交易号=" + trade_no + "交易金额 =" + total_fee + "|" + subject + "|" + buyer_email);
        //                PayBLL bll = new PayBLL();
        //                ZFBpay p = bll.GetZFBPayInfo(Convert.ToInt32(order_no));
        //                int temp = Convert.ToInt32(total_fee.Substring(0, total_fee.IndexOf('.')));
        //                if (temp == p.amount)
        //                {
        //                    //验证金额
        //                    int ret = bll.SuccessOrderByZFB(Convert.ToInt32(order_no));//验证成功 执行加币
        //                    if (ret == 1)
        //                    {
        //                        //Loger.WriteLog(LogFile.AliPay, "订单号 :" + order_no.ToString() + " 加币成功");
        //                        LogHelper.WriteLog(LogFile.AliPay, "【支付宝充值】账号:" + buyer_email + ",交易金额：" + total_fee + ",订单编号：" + order_no + ",交易号：" + trade_no + ",--加币成功--");

        //                        try
        //                        {
        //                            int useridx = new PayBLL().getUseridxFromOrderID(Convert.ToInt64(order_no), 1);
        //                            long newcoin = new PayBLL().getUsetCashByUseridx(useridx);
        //                            DllInvokeMethod.NotifyCashUpdate(useridx, Convert.ToInt32(newcoin));
        //                        }
        //                        catch (Exception)
        //                        {
        //                            // Loger.WriteLog(LogFile.AliPay, "发送加币消息出错:" + e.Message.ToString());
        //                        }
        //                        return Content("success");
        //                    }
        //                    else
        //                    {
        //                        //Loger.WriteLog(LogFile.AliPay, "订单号 :" + order_no.ToString() + " 执行加币过程失败");
        //                        LogHelper.WriteLog(LogFile.AliPay, "【支付宝充值】账号:" + buyer_email + ",交易金额：" + total_fee + ",订单编号：" + order_no + ",交易号：" + trade_no + ",--加币失败--");

        //                        return Content("FAIL");
        //                    }
        //                }
        //                else
        //                {
        //                    LogHelper.WriteLog(LogFile.AliPay, " 订单号:" + order_no.ToString() + "|金额不正确 :" + total_fee + "|" + p.amount);
        //                    return Content("fail");
        //                }

        //                //注意：
        //                //付款完成后，支付宝系统发送该交易状态通知
        //                //请务必判断请求时的total_fee、seller_id与通知时获取的total_fee、seller_id为一致的
        //            }

        //            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //            //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——
        //            return Content("success");  //请不要修改或删除
        //        }
        //        else//验证失败
        //        {
        //            return Content("fail");
        //        }
        //    }
        //    else
        //    {
        //        return Content("无通知参数");
        //    }
        //    // return Content("");
        //}

        //public ActionResult AlipaynotifyByH5()
        //{
        //    LogHelper.WriteLog(LogFile.AliPay, "公众号支付宝开始调用" + Request.Form.ToString());


        //    SortedDictionary<string, string> sPara = GetRequestPost();
        //    //SortedDictionary<string, string> sPara = new SortedDictionary<string, string>();
        //    //sPara.Add("payment_type", "1");
        //    //sPara.Add("subject", "%u5145%u503c%u55b5%u5e01%uff0c%u55b5%u64adID%uff1a60107995");
        //    //sPara.Add("trade_no", "2016050421001004320286104464");
        //    //sPara.Add("buyer_email", "tjox_tian%40163.com");
        //    //sPara.Add("gmt_create", "2016-05-04+16%3a51%3a40");
        //    //sPara.Add("notify_type", "trade_status_sync");
        //    //sPara.Add("quantity", "1");
        //    //sPara.Add("out_trade_no", "2276");
        //    //sPara.Add("seller_id", "2088801677358289");
        //    //sPara.Add("notify_time", "2016-05-04+16%3a55%3a01");
        //    //sPara.Add("body", "%u55b5%u64ad");
        //    //sPara.Add("trade_status", "TRADE_SUCCESS");
        //    //sPara.Add("is_total_fee_adjust", "N");
        //    //sPara.Add("total_fee", "1.00");
        //    //sPara.Add("gmt_payment", "2016-05-04+16%3a51%3a41");
        //    //sPara.Add("seller_email", "jhzhifubao2%409158.com");
        //    //sPara.Add("price", "1.00");
        //    //sPara.Add("buyer_id", "2088502652200327");
        //    //sPara.Add("notify_id", "3724c242e0102398850996c19be38c7igy");
        //    //sPara.Add("use_coupon", "N");
        //    //sPara.Add("sign_type", "MD5");
        //    //sPara.Add("sign", "32bdd227b9846b0353f86e7f05b37a2f");
        //    if (sPara.Count > 0)//判断是否有带返回参数
        //    {
        //        string sign_type = "MD5";

        //        WebAPI.AliPay.Notify aliNotify = new AliPay.Notify();
        //        bool verifyResult = aliNotify.VerifyByH5(sPara, Request.Form["notify_id"], Request.Form["sign"], sign_type);
        //        // bool verifyResult = aliNotify.VerifyByH5(sPara, "3724c242e0102398850996c19be38c7igy", "32bdd227b9846b0353f86e7f05b37a2f", "MD5");
        //        // return View();
        //        if (verifyResult)//验证成功
        //        {
        //            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //            //请在这里加上商户的业务逻辑程序代码
        //            string trade_no = Request.Form["trade_no"];         //支付宝交易号
        //            string order_no = Request.Form["out_trade_no"];     //获取订单号
        //            string total_fee = Request.Form["total_fee"];       //获取总金额
        //            string subject = Request.Form["subject"];           //商品名称、订单名称
        //            //    string body = Request.Form["body"];           //商品描述、订单备注、描述
        //            string buyer_email = Request.Form["buyer_email"];   //买家支付宝账号
        //            string trade_status = Request.Form["trade_status"]; //交易状态

        //            //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
        //            //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表

        //            //商户订单号                string out_trade_no = Request.Form["out_trade_no"];

        //            //支付宝交易号                string trade_no = Request.Form["trade_no"];

        //            //交易状态    string trade_status = Request.Form["trade_status"];
        //            //
        //            // Loger.WriteLog(LogFile.AliPay, "订单号 =" + order_no.ToString() +"返回参数："+ Request.Form.ToString());

        //            if (Request.Form["trade_status"] == "TRADE_FINISHED")
        //            {
        //                //判断该笔订单是否在商户网站中已经做过处理
        //                //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
        //                //如果有做过处理，不执行商户的业务程序

        //                //注意：
        //                //退款日期超过可退款期限后（如三个月可退款），支付宝系统发送该交易状态通知
        //                //请务必判断请求时的total_fee、seller_id与通知时获取的total_fee、seller_id为一致的
        //                return Content("success");
        //            }
        //            else if (Request.Form["trade_status"] == "TRADE_SUCCESS")
        //            {
        //                //判断该笔订单是否在商户网站中已经做过处理
        //                //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
        //                //如果有做过处理，不执行商户的业务程序
        //                LogHelper.WriteLog(LogFile.AliPay, "订单号 =" + order_no.ToString() + " 支付宝交易号=" + trade_no + "交易金额 =" + total_fee + "|" + subject + "|" + buyer_email);
        //                PayBLL bll = new PayBLL();
        //                ZFBpay p = bll.GetZFBPayInfo(Convert.ToInt32(order_no));
        //                int temp = Convert.ToInt32(total_fee.Substring(0, total_fee.IndexOf('.')));
        //                if (temp == p.amount)
        //                {
        //                    //验证金额
        //                    int ret = bll.SuccessOrderByZFB(Convert.ToInt32(order_no));//验证成功 执行加币
        //                    if (ret == 1)
        //                    {
        //                        LogHelper.WriteLog(LogFile.AliPay, "订单号 :" + order_no.ToString() + " 加币成功");
        //                        try
        //                        {
        //                            int useridx = new PayBLL().getUseridxFromOrderID(Convert.ToInt64(order_no), 1);
        //                            long newcoin = new PayBLL().getUsetCashByUseridx(useridx);
        //                            DllInvokeMethod.NotifyCashUpdate(useridx, Convert.ToInt32(newcoin));
        //                        }
        //                        catch (Exception)
        //                        {
        //                            // Loger.WriteLog(LogFile.AliPay, "发送加币消息出错:" + e.Message.ToString());
        //                        }
        //                        return Content("success");
        //                    }
        //                    else
        //                    {
        //                        LogHelper.WriteLog(LogFile.AliPay, "订单号 :" + order_no.ToString() + " 执行加币过程失败");
        //                        return Content("FAIL");
        //                    }
        //                }
        //                else
        //                {
        //                    LogHelper.WriteLog(LogFile.AliPay, " 订单号:" + order_no.ToString() + "|金额不正确 :" + total_fee + "|" + p.amount);
        //                    return Content("fail");
        //                }

        //                //注意：
        //                //付款完成后，支付宝系统发送该交易状态通知
        //                //请务必判断请求时的total_fee、seller_id与通知时获取的total_fee、seller_id为一致的
        //            }
        //            else
        //            {
        //            }

        //            //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

        //            return Content("success");  //请不要修改或删除

        //            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //        }
        //        else//验证失败
        //        {
        //            return Content("fail");
        //        }
        //    }
        //    else
        //    {
        //        return Content("无通知参数");
        //    }

        //    // return Content("");
        //}

        //[Auth]
        //public JsonResult CreateOrderByZFB()
        //{
        //    MobileResult mr = new MobileResult();

        //    Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
        //    if (dic != null)
        //    {
        //        int useridx = Convert.ToInt32(dic["userIdx"]); //用户IDX
        //        string moneys = dic["moneys"].ToString();  //支付金额
        //        string contents = dic["type"].ToString();//支付类型 1充值 2升级vip
        //        //if (string.IsNullOrWhiteSpace(useridx))
        //        //{
        //        //    mr.code = "-1";
        //        //    mr.msg = "参数错误";
        //        //    return Json(mr);
        //        //}
        //        if (string.IsNullOrWhiteSpace(moneys))
        //        {
        //            mr.code = "-2";
        //            mr.msg = "参数错误";
        //            return Json(mr);
        //        }
        //        if (string.IsNullOrWhiteSpace(contents))
        //        {
        //            mr.code = "-3";
        //            mr.msg = "参数错误";
        //            return Json(mr);
        //        }
        //        if (contents == "1")
        //        {
        //            contents = "充值";
        //        }
        //        else
        //        {
        //            contents = "升级vip";
        //        }
        //        int ntype = 1; //苹果1安卓0微信公众号2
        //        PayBLL bll = new PayBLL();
        //        int orderid = bll.CreateOrderByZFB(Convert.ToInt32(useridx), Convert.ToInt32(moneys), contents, ntype, "");



        //        mr.code = "100";
        //        mr.msg = "操作成功";
        //        mr.data = orderid;
        //    }


        //    return Json(mr, JsonRequestBehavior.AllowGet);
        //}
        //[HttpPost]
        //public JsonResult CreateZFBOrderByWXPub()
        //{
        //    MobileResult mr = new MobileResult();
        //    LogHelper.WriteLog(LogFile.AliPay, "222开始调用");
        //    Stream stream = HttpContext.Request.InputStream;
        //    byte[] bytes = new byte[stream.Length];
        //    stream.Read(bytes, 0, bytes.Length);
        //    stream.Seek(0, SeekOrigin.Begin);// 设置当前流的位置为流的开始   
        //    string param = Encoding.UTF8.GetString(bytes);

        //    // Loger.WriteLog(LogFile.AliPay, "222开始调用" + param);
        //    string param2 = CryptoHelper.AESDecrypt(param, "zanghouhtiji91ke5mli8angeiaobove");
        //    // Loger.WriteLog(LogFile.AliPay, "222开始调用" + param2);
        //    Dictionary<string, string> dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(param2);
        //    if (dic != null)
        //    {
        //        string useridx = dic["idx"].ToString(); //用户IDX
        //        string useridx2 = dic["idx2"].ToString(); //推荐人IDX2
        //        string moneys = dic["moneys"].ToString();  //支付金额

        //        // Loger.WriteLog(LogFile.AliPay, "微信公众号调用支付宝" + useridx + "|" + useridx2 + "|" + moneys);
        //        if (string.IsNullOrWhiteSpace(moneys) || string.IsNullOrWhiteSpace(useridx) || string.IsNullOrWhiteSpace(useridx2))
        //        {
        //            mr.code = "-2";
        //            mr.msg = "参数错误";
        //            return Json(mr);
        //        }
        //        if (useridx2 == "0")
        //        {
        //            useridx2 = "";
        //        }

        //        string contents = "充值";
        //        int ntype = 2; //苹果1安卓0微信公众号2
        //        PayBLL bll = new PayBLL();
        //        int orderid = bll.CreateOrderByZFB(Convert.ToInt32(useridx), Convert.ToInt32(moneys), contents, ntype, useridx2);


        //        mr.code = "100";
        //        mr.msg = "操作成功";
        //        mr.data = orderid;
        //    }
        //    return Json(mr, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region 微信支付 已停用
        // [Auth]
        public ActionResult CreateOrderbyWx()
        {
            // Loger.WriteLog(LogFile.WXPay, "生成订单开始:" + System.DateTime.Now);
            MobileResult mr = new MobileResult();
            string orderid = "0";// WxPayApi.GenerateOutTradeNo();//"10000";
            string moneys = "0";
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic != null)
            {
                int useridx = Convert.ToInt32(dic["userIdx"]); //用户IDX
                int moneyss = Convert.ToInt32(dic["moneys"].ToString());  //支付金额
                moneys = moneyss.ToString();
                string contents = "";// dic["type"].ToString();//支付类型 1充值 2升级vip
                if (dic["type"].ToString() == "2")
                {
                    contents = "vip升级";
                }
                else
                {
                    contents = "充值";
                }
                PayBLL bll = new PayBLL();
                long ret = bll.CreateOrderByWX(useridx, moneyss, contents);
                orderid = ret.ToString();  //生成微信订单号 
            }

            string fee = Convert.ToString(Convert.ToInt32(moneys) * 100);  //  将元转分
            int coin = Convert.ToInt32(moneys) * 1000;
            //string fee = moneys;  //测试
            //统一下单
            WxPayData data = new WxPayData();
            data.SetValue("body", coin.ToString() + "个喵币");
            data.SetValue("attach", "喵币");
            data.SetValue("out_trade_no", orderid);//WxPayApi.GenerateOutTradeNo()
            data.SetValue("total_fee", fee);
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            data.SetValue("time_expire", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));
            data.SetValue("goods_tag", "喵播充值");
            data.SetValue("trade_type", "APP");
            data.SetValue("openid", "");


            WxPayData result = WxPayApi.UnifiedOrder(data);

            if (!result.IsSet("appid") || !result.IsSet("prepay_id") || result.GetValue("prepay_id").ToString() == "")
            {
                // Log.Error(this.GetType().ToString(), "UnifiedOrder response error!");
                // throw new WxPayException("UnifiedOrder response error!");
                mr.code = "-1";
                mr.msg = "参数错误";
                //LogHelper.WriteLog(LogFile.WXPay, "参数错误:" + result.ToXml());
                return Json(mr, JsonRequestBehavior.AllowGet);
            }
            if (result.GetValue("return_code").ToString() != "SUCCESS")
            {
                mr.code = "-2";
                mr.msg = "提交订单失败";
                return Json(mr, JsonRequestBehavior.AllowGet);
            }
            mr.code = "100";
            mr.msg = "操作成功";
            mr.data = new { orderid = orderid, prepay_id = result.GetValue("prepay_id").ToString(), nonce_str = result.GetValue("nonce_str").ToString(), appid = result.GetValue("appid").ToString(), mch_id = result.GetValue("mch_id").ToString() };
            //  Loger.WriteLog(LogFile.WXPay, "生成订单号:" + orderid);
            return Json(mr, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 微信支付回调
        /// </summary>
        /// <returns></returns>
        public ActionResult Wxpaynotify()
        {
            //接收从微信后台POST过来的数据

            // Loger.WriteLog(LogFile.Error, "调用时间:" + System.DateTime.Now);

            System.IO.Stream s = Request.InputStream;//page.Request.InputStream;
            int count = 0;
            byte[] buffer = new byte[1024];
            StringBuilder builder = new StringBuilder();
            while ((count = s.Read(buffer, 0, 1024)) > 0)
            {
                builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
            }
            s.Flush();
            s.Close();
            s.Dispose();

            // Loger.WriteLog(LogFile.Error, "Receive data from WeChat :" + builder.ToString());

            //转换数据格式并验证签名
            WxPayData data = new WxPayData();
            try
            {
                data.FromXml(builder.ToString());
            }
            catch (WxPayException ex)
            {
                //若签名错误，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", ex.Message);
                // Log.Error(this.GetType().ToString(), "Sign check error : " + res.ToXml());
                //LogHelper.WriteLog(LogFile.WXPay, "Sign check error  :" + res.ToXml());
                return Content("FAIL");
            }


            //  Loger.WriteLog(LogFile.Error, "Check sign success");


            WxPayData notifyData = data;// WeixinPay.Notify.GetNotifyData();

            //检查支付结果中transaction_id是否存在
            if (!notifyData.IsSet("transaction_id"))
            {
                //若transaction_id不存在，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "支付结果中微信订单号不存在");
                return Content("FAIL");
            }

            string transaction_id = notifyData.GetValue("transaction_id").ToString();

            //查询订单，判断订单真实性
            if (!QueryOrder(transaction_id))
            {
                //若订单查询失败，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "订单查询失败");
                // Log.Error(this.GetType().ToString(), "Order query failure : " + res.ToXml());
                // page.Response.Write(res.ToXml());
                // page.Response.End();
                //LogHelper.WriteLog(LogFile.WXPay, "Order query failure   :" + res.ToXml());
                return Content("FAIL");
            }
            //查询订单成功
            else
            {
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "SUCCESS");
                res.SetValue("return_msg", "OK");
                // Loger.WriteLog(LogFile.WXPay, "order query success:" + res.ToXml());
                //业务加币
                string orderid = notifyData.GetValue("out_trade_no").ToString();
                PayBLL bll = new PayBLL();
                int ret = bll.SuccessOrderByWX(Convert.ToInt64(orderid));
                //if (ret != 1)
                //{
                //    Loger.WriteLog(LogFile.WXPay, "订单号" + orderid + "加币数据库过程报错 :" + notifyData.ToXml());
                //    return Content("FAIL");
                //}
                try
                {
                    int useridx = new PayBLL().getUseridxFromOrderID(Convert.ToInt64(orderid), 1);
                    long newcoin = new PayBLL().getUsetCashByUseridx(useridx);
                    DllInvokeMethod.NotifyCashUpdate(useridx, Convert.ToInt32(newcoin));
                }
                catch (Exception e)
                {
                    LogHelper.WriteLog(LogFile.Error, "发送加币消息出错:" + e.Message.ToString());
                }
                //LogHelper.WriteLog(LogFile.WXPay, "订单号" + orderid + "加币成功 ret=" + ret);

            }

            return Content("SUCCESS");
        }
        //查询订单
        private bool QueryOrder(string transaction_id)
        {
            WxPayData req = new WxPayData();
            req.SetValue("transaction_id", transaction_id);
            WxPayData res = WxPayApi.OrderQuery(req);
            if (res.GetValue("return_code").ToString() == "SUCCESS" &&
                res.GetValue("result_code").ToString() == "SUCCESS")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 弃用提现

        /// <summary>
        /// 提现申请页面
        /// live.9158.com/Pay/WithdrawView?useridx=68101444&sign=ccd6c9afe235be13b93d22af44ef4168&Random=5
        /// </summary>
        /// <returns></returns>
        //public ActionResult WithdrawView(string sign, int useridx = 0, string usertoken = "")
        //{
        //    if (useridx <= 0 || string.IsNullOrEmpty(sign)) return View("Error");
        //    if (MemcachedHelper.Get("Cache_WithdrawView_" + useridx) == null) return View("Error");
        //    if (Tools.GetRealIP().Equals("115.231.93.68"))
        //    {
        //        string tokenKey = CacheKeys.LIVE_USER_TOKEN + useridx;
        //        string tokenValue = (string)MemcachedHelper.MemGet(tokenKey);

        //        LogHelper.WriteLog(LogFile.Debug, "【提现申请】" + useridx + "," + usertoken + "," + tokenValue);
        //    }

        //    string mySign = CryptoHelper.ToMD5("&&**miaomiao" + user.GetLiveUserInfoByIdx(useridx).userId);

        //    if (sign.ToLower() != mySign.ToLower()) return View("Error");

        //    WalletModel wm = pay.Get_MyIncome(0, useridx);
        //    LiveConfig lc = LiveBLL.Get_LiveConfigById(100);
        //    WithdrawRecord wr = new WithdrawRecord();

        //    //将数组组装到viewModel
        //    VMExchange vmEc = new VMExchange();
        //    vmEc.wallet = wm;
        //    vmEc.WithdrawMinRequire = Convert.ToDecimal(lc.data);
        //    vmEc.MyWithdrawInfo = wr;

        //    return View(vmEc);
        //}

        /// <summary>
        /// 申请提现操作
        /// </summary>
        /// <param name="form">表单数据</param>
        /// <param name="useridx"></param>
        /// <param name="sign">加密串</param>
        /// <param name="money">要提现金额</param>
        /// <returns></returns>
        //[HttpPost]
        //public ActionResult WithdrawApply(FormCollection form, int useridx = 0, string sign = "", decimal money = 0)
        //{
        //    if (useridx <= 0 || money <= 0 || string.IsNullOrEmpty(sign)) return Content("-1");
        //    if (MemcachedHelper.Get("Cache_WithdrawView_" + useridx) == null) return Content("-13");

        //    UserInfo u = user.GetLiveUserInfoByIdx(useridx);
        //    WithdrawRecord wr = new WithdrawRecord();
        //    string code = CryptoHelper.ToMD5("&&**miaomiao" + u.userId);

        //    wr.amount = money;
        //    wr.useridx = u.useridx;
        //    wr.alipayID = TextHelper.FilterSpecial(form["alipayID"]);
        //    wr.realName = TextHelper.FilterSpecial(form["realname"]);
        //    wr.mobilePhone = TextHelper.FilterSpecial(form["phoneno"]);
        //    bool isPhone = Tools.IsPhone(wr.mobilePhone);

        //    if ((u == null && u.useridx <= 0) || sign.ToLower() != code.ToLower()) return Content("-11");

        //    if (string.IsNullOrEmpty(wr.alipayID) || string.IsNullOrEmpty(wr.realName) ||
        //        string.IsNullOrEmpty(wr.mobilePhone) || money <= 0)
        //        return Content("-11");

        //    if (!isPhone)
        //        return Content("-12");

        //    WalletModel wm = pay.Get_MyIncome(0, useridx);
        //    LiveConfig lc = LiveBLL.Get_LiveConfigById(100);

        //    if (wm.myMoney < Convert.ToDecimal(lc.data))
        //        return Content("-14");

        //    if (wm.myMoney < money)
        //        return Content("-15");

        //    //业务逻辑
        //    int result = pay.Withdraw_Insert(wr);
        //    if (result > 0)
        //    {
        //        return Content("1");
        //    }
        //    return Content(result.ToString());
        //}

        #endregion
        #region 老挝版金币提现
        public ActionResult userPutForward(int useridx)
        {

            // string useridx = "60424678";
            //if (!HttpHelper.GetClientType())
            //{
            //    return new BaseController().ParamError("请在APP中打开");
            //}
            if (useridx <= 0)
            {
                return new BaseController().ParamError("CheckMobile");
            }
            //if (rawurl != "" && (rawurl.IndexOf("ok.sina.com.cn") > -1 || rawurl.IndexOf("www.9see.com") > -1))
            // LW_GoldLimitSet(int useridx, ref decimal sale, ref decimal money)
            Decimal sale = 0;
            Decimal money = 0;
            //取得用户金币余额及可提现余额
            pay.LW_GoldLimitSet(useridx, ref sale, ref money);
            ViewBag.useridx = useridx;
            ViewBag.sale = sale;
            ViewBag.money = money;
            ViewBag.hk = sale / 100;
            return View();
        }


        public ActionResult submitExtract(int userIdx, string pwd, Decimal money, string bankId)
        {
            //string rawurl = (Request.ServerVariables["HTTP_REFERER"] == null ? "" : Request.ServerVariables["HTTP_REFERER"].Trim());
            //if (rawurl == "" || rawurl.IndexOf("live.quboshow.com") < 0)
            //{
            //    return new BaseController().ParamError("CheckMobile");
            //}
            //if (userIdx <= 0 || pwd == "")
            //{
            //    return new BaseController().ParamError("CheckMobile");
            //}
            var mr = new Result();
            int iRet = pay.userPutForward(userIdx, money, CryptoHelper.ToMD5(pwd), bankId);
            Decimal sale = 0;
            //取得用户金币余额及可提现余额
            pay.LW_GoldLimitSet(userIdx, ref sale, ref money);
            // int iRet = pa.pwdCheck(userIdx, CryptoHelper.ToMD5(pwd));
            if (iRet == 1)
            {
                mr.code = "100";
                mr.msg = "申请成功，等待审核";
            }
            else
            {
                string[] msgArry = { "数据异常", "余额不足", "提现金额大于可提现金额", "密码有误" };
                mr.code = "101";
                mr.msg = msgArry[(-iRet)];
            }
            mr.data = new
            {
                sale = sale,
                money = money
            };
            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 主播收益提现申请
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UpIncomeLogState(int id = 0)
        {
            var mr = new Result();
            if (id <= 0)
            {
                return new BaseController().ParamError("CheckMobile");
            }
            int iRet = pay.UpIncomeLogState(id);
            if (iRet == 1)
            {
                mr.code = "100";
                mr.msg = "申请成功，等待审核";
            }
            else
            {
                if (iRet == -1)
                {
                    mr.msg = "暂无该待提现记录";
                }
                if (iRet == 0)
                {
                    mr.msg = "系统异常";
                }
                mr.code = "101";
            }
            mr.data = new
            {
                iRet = iRet
            };
            return Content(JsonConvert.SerializeObject(mr));
        }



        #endregion
    }
}
