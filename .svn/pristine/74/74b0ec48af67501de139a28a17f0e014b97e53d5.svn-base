
#define release

using BLL;
using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Data;
using System.Configuration;
using OAuth.Weixin;
using OAuth.QQ;
using OAuth.SinaWeibo;
using Newtonsoft.Json;
using Common.Core;
using ThirdAPI;
using Model.Param;
using BLL.Mongo;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using OAuth.Facebook;
using System.Linq;

namespace WebAPI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserInfoBLL user = new UserInfoBLL();
        private readonly AccountBLL bll = new AccountBLL();
        private readonly IncomeBLL income = new IncomeBLL();
        private readonly PasswordBLL _password = new PasswordBLL();

        #region 极验验证

        /// <summary>
        /// 极验验证
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MobileGeetest()
        {
            GeetestLib geetest = new GeetestLib(GeetestConfig.publicKey, GeetestConfig.privateKey);
            ViewBag.geetestStatus = geetest.preProcess();
            Session["geetestStatus"] = geetest.preProcess();
            //string data = "{\"success\":0,\"gt\":\"b46d1900d0a894591916ea94ea91bd2c\",\"challenge\":\"9028bb7e0df896b16da8ab4e899a5e42\"}";

            return Content(geetest.getResponseStr());
        }

        /// <summary>
        /// 极验验证获取3.0获取
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCaptcha()
        {
            GeetestLib_V2 geetest = new GeetestLib_V2(GeetestConfig_v3.publicKey, GeetestConfig_v3.privateKey);

            string userip = Tools.GetRealIP();
            String userID = "test";
            Byte gtServerStatus = geetest.preProcess(userID, "web", userip);
            //Session[GeetestLib.gtServerStatusSessionKey] = gtServerStatus;
            //Session["userID"] = userID;

            return Content(geetest.getResponseStr());
        }

        #endregion

        #region 注册和绑定手机号
        /// <summary>
        /// Step 1
        /// 1：先验证手机号是否可以注册或绑定
        /// 2：发送验证码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckMobile()
        {
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Register_KEY);
            if (dic == null)
            {
                return new BaseController().ParamError("CheckMobile");
            }
            var mr = new Result();
            var reg = new Regex(Tools.TelRegex);

            var sendType = dic["sendType"].ToString() ?? "1";//发送验证码类型 1:注册 2:绑定手机号
            var phoneNum = dic["tel"].ToString().Trim();
            //var devType = dic["deviceType"].ToString();//android ios
            var deviceid = dic["deviceId"].ToString();//设备ID
            var gt_challenge = dic["geetest_challenge"] ?? "";
            var gt_validate = dic["geetest_validate"] ?? "";
            var gt_seccode = dic["geetest_seccode"] ?? "";
            var tg_Validcode = dic["tg_validate"].ToLower();//普通验证码
            var userid = dic["userId"] != "" ? dic["userId"] : "91589158";
            var useridx = dic["userIdx"] != "" ? dic["userIdx"] : "0";

            int bindCount = 0;//手机号绑定次数

            if (string.IsNullOrWhiteSpace(phoneNum) || !reg.IsMatch(phoneNum))
            {
                mr.code = "110";
                mr.msg = "手机号码不正确";
                return Content(JsonConvert.SerializeObject(mr));
            }

            #region  验证码验证操作

            GeetestLib geetest = new GeetestLib(GeetestConfig.publicKey, GeetestConfig.privateKey);

            //判断及验证是否可用 1：极验证 0：普通验证
            string vcType = geetest.preProcess().ToString();// Session["geetestStatus"].ToString() ?? "0";// ViewBag.geetestStatus ?? "0";
            if (vcType == "1")
            {
                if (!geetest.ValidateRequest(gt_challenge, gt_validate, gt_seccode))
                {
                    mr.code = "112";
                    mr.msg = "验证码错误，请重试";
                    return Json(mr, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                string ajax_validate = Session["Ajax_ValidateNum"].ToString().ToLower();
                if (string.IsNullOrEmpty(tg_Validcode))
                {
                    mr.code = "111";
                    mr.msg = "请输入验证码";
                    return Json(mr, JsonRequestBehavior.AllowGet);
                }
                else if (ajax_validate != tg_Validcode)
                {
                    mr.code = "112";
                    mr.msg = "验证码错误，请重试！";
                    return Json(mr, JsonRequestBehavior.AllowGet);
                }
                //LogHelper.WriteLog(LogFile.Test, "【验证码】tg_validate:" + tg_Validcode + "," + ajax_validate);
            }

            #endregion

            if (sendType == "1")//注册
            {
                //检测手机号是否可注册或绑定
                _password.CheckPhoneBind(phoneNum, -1, ref bindCount);
                if (bindCount <= 0)
                {
                    int flag = SendCodeBLL.sendSmsInfo(useridx, userid, phoneNum, deviceid, 1, "+86");
                    SendCodeMsg(mr, flag);
                }
                else
                {
                    mr.code = "113";
                    mr.msg = "手机号已绑定其他操作，" + bindCount;
                }
                return Content(JsonConvert.SerializeObject(mr));
            }

            //绑定手机
            if (sendType == "2")
            {
                //2 判断用户是否绑定手机
                string userphone = _password.CheckUserid(userid);
                if (!string.IsNullOrWhiteSpace(userphone))
                {
                    mr.code = "114";
                    mr.msg = "该账号已绑定手机号" + userphone;
                    mr.data = userphone;
                    return Json(mr);
                }
                if (_password.CheckPhoneBind(phoneNum, int.Parse(useridx), ref bindCount) == 0)
                {
                    LogHelper.WriteLog(LogFile.Log, "【手机号绑定次数已受限】{0},{1},{2}", useridx, phoneNum, bindCount);

                    mr.code = "118";
                    mr.msg = "该手机绑定次数已受限";
                    return Content(JsonConvert.SerializeObject(mr));
                }
                int state = _password.GetProtectState(userid);
                if (state == 0)
                {
                    //绑定手机流程
                    int flag = SendCodeBLL.sendSmsInfo(useridx, userid, phoneNum, deviceid, 2, "+86");
                    SendCodeMsg(mr, flag);
                }
                if (state == 1 || state == 4 || state == 6)
                {
                    string tellPhone = _password.GetPasswordTell(userid);

                    mr.code = "116";
                    mr.msg = "该账号已经绑定手机请前往密保中心进行更换" + tellPhone;
                }
                if (state == 5)
                {
                    mr.code = "119";//二级密码
                    mr.code = "输入二级密码";
                }
                if (state == 2 || state == 3)
                {
                    DataTable dt = _password.GetPasswordQuestion(userid);
                    mr.code = "117";
                    mr.msg = "验证密保问题";
                    mr.data = new { one = dt.Rows[0]["Question"], two = dt.Rows[0]["questionone"], three = dt.Rows[0]["questiontwo"] };//JsonConvert.SerializeObject(dt);
                }

                LogHelper.WriteLog(LogFile.Log, "【手机号绑定操作】{0},{1},绑定次数:{2},{3}", useridx, phoneNum, bindCount, mr.msg);
            }
            return Json(mr, "text/html; charset=UTF-8");
        }

        /// <summary>
        /// 二、验证手机返送验证码是否正确(有点多余)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult VerifySmsCode()
        {
            var key = CryptoHelper.Register_KEY;
            //string useridx = AppDataBLL.GetUseridx;

            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(key);
            if (dic == null) { return ParamError("，请重新提交"); }

            var phoneNum = dic["tel"].Trim();
            var smsCode = dic["smsCode"];
            var mr = new MobileResult();

            if (string.IsNullOrWhiteSpace(phoneNum) || !Tools.IsPhone(phoneNum))
            {
                mr.code = "110";
                mr.msg = "手机号码不正确";
                return Json(mr);
            }
            if (string.IsNullOrEmpty(smsCode))
            {
                mr.code = "101";
                mr.msg = "请输入验证码";
                return Json(mr);
            }
            int ret = SendCodeBLL.valiPhoneCode(phoneNum, smsCode);

            MobileResultBLL.ValiCodeMessage(mr, ret);
            return Json(mr);
        }

        /// <summary>
        /// 三、手机号真正注册
        /// </summary>
        /// <returns></returns>
        /// 100:注册成功 0:注册失败 -1:参数错误 -2:验证码失效 -3:验证码不匹配 -5:注册太频繁 -6:密码和用户名不能一致
        [HttpPost]
        public ActionResult RegisterPhone()
        {
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Register_KEY);

            if (dic == null) { return new BaseController().ParamError("RegisterPhone"); }

            MemberInfo m = new MemberInfo();
            var phoneCode = dic["smsCode"].ToString();//短信验证码            
            //var devId = dic["deviceId"].ToString();

            m.phoneNum = dic["tel"].ToString();
            m.PwdSrc = dic["pwd"].ToString();
            m.Sex = dic["gender"].Equals("1") ? 1 : 0;//默认女
            m.devType = TextHelper.FilterSpecial(dic["deviceType"]);
            m.NickName = TextHelper.FilterSpecial(dic["nickName"].Trim());

            var ret = 0;
            var mr = new MobileResult();

            if (!Tools.IsPhone(m.phoneNum))
            {
                mr.code = "110";
                mr.msg = "手机号格式有误";
                return Json(mr);
            }

            if (!CacheBLL.VerifyRegisterIP())
            {
                mr.code = "111";
                mr.msg = "同IP注册太过频繁";
                return Json(mr);
            }
            ret = SendCodeBLL.valiPhoneCode(m.phoneNum, phoneCode);
            if (ret != 1)
            {
                mr.code = "112";
                mr.msg = "验证码错误";
                return Json(mr);
            }
            BaseMemberInfo member = new BaseMemberInfo();

            //3:注册
            ret = bll.Register_Mobile(m, ref member);

            mr.code = ret.ToString();
            mr.msg = MobileResultBLL.GetRegisterMessageByRet(ret.ToString());

            if (ret > 0)
            {
                mr.code = "100";
                mr.msg = "注册成功";
                mr.data = member;
            }
            if (ret != -5)
            {
                LogHelper.WriteLog(LogFile.Log, "【手机号注册结果】{0},{1},{2}", m.phoneNum, mr.code, mr.msg);
            }

            return Json(mr, JsonRequestBehavior.AllowGet);
        }

        #region 绑定手机号
        /// <summary>
        /// step1 验证密保问题
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BindMobileCheckQuestion()
        {
            PhoneBindParam param = CryptoHelper.GetAESBinaryModelParam<PhoneBindParam>(CryptoHelper.Live_KEY);

            if (param == null) { return new BaseController().ParamError(); }

            //1 获取用户IDX,ID,手机号
            var userid = param.userId;
            var useridx = param.useridx;
            var phoneNum = param.phone;
            var devId = param.deviceId;

            string question1 = param.one;
            string answer1 = param.answerone;//手机端查传入答案
            string question2 = param.two;
            string answer2 = param.answertwo;
            string question3 = param.three;
            string answer3 = param.answerthree;
            string ip = Tools.GetRealIP();

            //验证传入的密保问题
            MobileResult mr = new MobileResult();

            if (_password.GetPasswordQuestionCheck(userid, question1, answer1, question2, answer2, question3, answer3) < 0)
            {
                mr.code = "-1";
                mr.msg = "验证密保问题失败";
                return Content(JsonConvert.SerializeObject(mr));
            }

            //发送sms短信
            int flag = SendCodeBLL.sendSmsInfo(useridx.ToString(), userid, phoneNum, devId, 2, "+86");
            switch (flag)
            {
                case 1:
                    mr.code = "100";
                    mr.msg = "操作成功";
                    break;
                case 0:
                    mr.code = "-10";
                    mr.msg = "发送验证码失败";
                    break;
                case -1:
                    mr.code = "-11";
                    mr.msg = "参数错误";
                    break;
                case -2:
                    mr.code = "-12";
                    mr.msg = "手机号发送已受限";
                    break;
                default:
                    mr.msg = "受限";
                    break;
            }

            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// step3 绑定手机
        /// </summary>
        /// <returns></returns>
        public ActionResult BindMobile()
        {
            var mr = new MobileResult();

            Dictionary<string, string> T = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (T == null) { return ParamError(); }

            //1 获取用户IDX,ID,手机号
            var useridx = Convert.ToInt32(T["userIdx"].ToString());
            var smsCode = T["verify"];//验证码
            var userid = T["userId"] == null ? "" : T["userId"].ToString();
            var phoneNum = T["phone"];
            var devId = T["deviceId"];
            var ulevel = Convert.ToInt32(T["level"]);

            //验证手机短信
            int ret = SendCodeBLL.valiPhoneCode(phoneNum, smsCode);
            if (ret == -2)
            {
                mr.code = "-2";
                mr.msg = "验证码失效";
                return Json(mr);
            }
            else if (ret == -3)
            {
                mr.code = "-3";
                mr.msg = "验证码不匹配";
                return Json(mr);
            }

            //绑定手机操作 118库
            int result = _password.AddPhoneBind(useridx, phoneNum, userid, ulevel);
            if (result > 0)
            {
                mr.code = "100";
                mr.msg = "操作成功";

                ServerHelper.BindPhoneSuccessNotice(useridx);
                SendCodeBLL.sendSmsInfo(useridx.ToString(), userid, phoneNum, devId, 3, "+86");
            }
            else
            {
                mr.code = "-4";
                mr.msg = "绑定手机失败";
            }

            return Json(mr, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region ThirdLogin QQ,WeiXin,SinaWeibo
        /// <summary>
        /// qq Login
        /// </summary>
        /// <param name="ThirdName">区分 喵播，还是喵拍，param:miaobo/miaopai</param>
        /// <param name="visitoridx"></param>
        /// <returns></returns>
        public ActionResult QQLogin(string ThirdName = "miaobo", int visitoridx = 0)
        {
            var key = Request["key"];
            var openid = Request["openid"];
            var access_token = Request["access_code"];
            var deviceType = Request["deviceType"] ?? "unknow";
            var myKey = CryptoHelper.ToMD5(openid + "&miabo.tiange.com").ToLower();
            var mr = new Result();

            if (string.IsNullOrEmpty(access_token) || string.IsNullOrEmpty(openid) || !myKey.Equals(key))
            {
                mr.code = "101";
                mr.msg = "参数错误";
                return Content(JsonConvert.SerializeObject(mr));
            }

            QQLogin qq = new QQLogin();

            var unionid = qq.GetUnionID(access_token);                      //获取唯一标识
            var thirdInfo = qq.GetUserInfo(access_token, openid, ThirdName);//获取第三方用户信息

            if (string.IsNullOrEmpty(thirdInfo) || !thirdInfo.Contains("nickname"))
            {
                LogHelper.WriteLog(LogFile.Debug, "【qq登陆获取信息失败】{0},{1}", thirdInfo);

                mr.code = "102";
                mr.msg = "获取第三方用户信息失败";
                return Content(JsonConvert.SerializeObject(mr));
            }

            int iRet = -1;
            ThirdLoginParam param = new ThirdLoginParam();
            param.Account = openid;
            param.devType = deviceType;
            param.Unionid = unionid;
            param.visitoridx = visitoridx;
            param.platForm = ThirdName;

            MemberInfo member = ThirdLoginBLL.VerifyThirdMember(ThirdLoginBLL.ThirdType.QQ, thirdInfo, ref iRet, param);

            if (member.UIdx > 0 && iRet >= 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = new
                {
                    ret = iRet,
                    uid = member.UId,
                    uidx = member.UIdx,
                    areaid = member.areaid,
                    token = AccountBLL.getThirdLoginToken(member.UIdx)
                };
            }
            else if (iRet == -4 || iRet == -5)
            {
                mr.code = "103";
                mr.msg = "账号绑定失败Code:" + iRet;
            }
            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        ///微信登陆二部曲：
        ///1:通过移动端传过来的code来换取access_token 和openid
        ///2:通过换取过来的access_token和openid来获取用户的信息
        /// </summary>
        /// <param name="deviceType">设备类型：iPhone,iPad,Android,iPod</param>
        /// <param name="ThirdName">区分 喵播，还是喵拍，param:miaobo/miaopai</param>
        /// <param name="deciveId">设备号</param>
        /// <param name="channelid">渠道号</param>
        /// <returns></returns>
        public ActionResult WeiXinLogin(string deviceType = "unknow",string code= "", string key= "", string deciveId="" ,int channelid=0)
        {
            string myKey = CryptoHelper.ToMD5(code + "&mianju.tiange.com").ToLower();

            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(key.ToLower()) || myKey != key.ToLower())
            {
                return new BaseController().ParamError();
            }
            MobileResult mr = new MobileResult();
            WeixinLogin wx = new WeixinLogin();

            Dictionary<string, string> dic = wx.GetDictAccessToken(code, 0);

            if (dic == null || dic.ContainsKey("errcode"))
            {
                mr.code = "101";
                mr.msg = "param error";
                return Json(mr, JsonRequestBehavior.AllowGet);
            }

            string thirdUserInfo = wx.GetUserInfo(dic["access_token"], dic["openid"]);

            if (string.IsNullOrEmpty(thirdUserInfo))
            {
                mr.code = "102";
                mr.msg = "获取第三方用户信息失败";
                return Json(mr, JsonRequestBehavior.AllowGet);
            }

            ThirdLoginParam param = new ThirdLoginParam();
            param.devType = deviceType;
            param.DeviceId = deciveId;
            param.Channelid = channelid;
            int ret = ThirdLoginBLL.VerifyThirdLogin(ThirdLoginBLL.ThirdType.WeiXin, thirdUserInfo, param, channelid, deciveId);
            //测试代码 永久新用户
            //if (Tools.IsCompanyIP && iRet == 0) iRet = 1;
            if (ret > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = new
                {
                    ret = ret,
                    token = AccountBLL.getThirdLoginToken(ret),
                    state=bll.userFountTel(ret)
                };
            }
            else 
            {
                mr.code = "103";
                mr.msg = "账号绑定失败Code:" + ret;
            }

            return Json(mr, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新浪微博登陆
        /// </summary>
        /// <param name="deviceType"></param>
        /// <param name="ThirdName">用来区分喵播登录/注册还是喵拍 miaobo/miaopai</param>
        /// <param name="visitoridx">区分游客注册/登录 0：正常，大于0：游客</param>
        /// <returns></returns>
        public ActionResult SinaWeiboLogin(string deviceType = "unknow", string ThirdName = "miaobo", int visitoridx = 0)
        {
            string uid = Request["uid"];
            string key = Request["key"];
            string access_token = Request["access_token"];
            string myKey = CryptoHelper.ToMD5(uid + "&miabo.tiange.com").ToLower();

            MobileResult mr = new MobileResult();

            if (string.IsNullOrEmpty(access_token) || string.IsNullOrEmpty(uid) || !myKey.Equals(key))
            {
                mr.code = "101";
                mr.msg = "参数错误！";
                return Content(JsonConvert.SerializeObject(mr));
            }

            SinaWeiboLogin sina = new SinaWeiboLogin(1);

            string thirdInfo = sina.GetUserInfo(access_token, uid);
            if (string.IsNullOrEmpty(thirdInfo))
            {
                LogHelper.WriteLog(LogFile.Error, "【新浪微博登陆获取用户信息失败】" + uid + ",thirdName:" + ThirdName);
                mr.code = "102";
                mr.msg = "Get Third UserInfo Fail";
                return Content(JsonConvert.SerializeObject(mr));
            }
            int iRet = -1;
            ThirdLoginParam param = new ThirdLoginParam();
            param.devType = deviceType;
            param.visitoridx = visitoridx;
            param.platForm = ThirdName;

            MemberInfo member = ThirdLoginBLL.VerifyThirdMember(ThirdLoginBLL.ThirdType.SinaWeibo, thirdInfo, ref iRet, param);

            if (iRet > -1 && member.UIdx > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = new { ret = iRet, uid = member.UId, uidx = member.UIdx, token = AccountBLL.getThirdLoginToken(member.UIdx), areaid = member.areaid };
            }
            else if (iRet == -4 || iRet == -5)
            {
                mr.code = "103";
                mr.msg = "账号绑定失败Code:" + iRet;
            }
            return Content(JsonConvert.SerializeObject(mr));
        }

        #endregion

        #region 辰龙Login
        /// <summary>
        /// 辰龙登陆
        /// live.9158.com/Account/ChenLongLogin?uidx=appletest001&pwd=123456&secretkey=1&deviceType=未知
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <param name="secretkey"></param>
        /// <param name="deviceType"></param>
        /// <returns></returns>
        public ActionResult ChenLongLogin(string uid, string pwd, string secretkey, string deviceType = "unknow")
        {
            Result r = new Result();

            string myKey = CryptoHelper.ToMD5(uid + "&miabo.tiange.com").ToLower();
            string thirdKey = ConfigurationManager.AppSettings["ChenLongLoginkey"];

            if (string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(pwd) || secretkey != myKey)
            {
                r.code = "101";
                r.msg = "参数错误！";
                return Json(r, JsonRequestBehavior.AllowGet);
            }

            string m = CryptoHelper.ToMD5(uid + pwd + thirdKey);
            string url = "http://mobile.cl0579.com/api/login_mb.aspx";
            string sendurl = string.Format("{0}?account={1}&pwd={2}&m={3}&time={4}", url, uid, pwd, m, TimeHelper.GetTimeStamp());
            string thirdInfo = "";
            if (uid == "appletest001")
            {
                thirdInfo = "{\"uid\":\"1661058\",\"nikename\":\"apple001\",\"gender\":\"1\",\"photo\":\"http://qp.cl0579.com/Images/Faces/1.gif\"}";
            }
            else
            {
                thirdInfo = HttpHelper.HttpGet(sendurl);
            }
            if (string.IsNullOrEmpty(thirdInfo) && !thirdInfo.Contains("uid"))
            {
                r.code = "112";
                r.msg = "获取第三方用户信息失败";
                return Json(r, JsonRequestBehavior.AllowGet);
            }

            if (!string.IsNullOrEmpty(thirdInfo) && thirdInfo.Contains("uid"))
            {
                int iRet = -1;
                ThirdLoginParam param = new ThirdLoginParam();
                param.devType = deviceType;

                MemberInfo member = ThirdLoginBLL.VerifyThirdMember(ThirdLoginBLL.ThirdType.ChenLong, thirdInfo, ref iRet, param);

                if (iRet > -1 && member.UIdx > 0)
                {
                    r.code = "100";
                    r.msg = "操作成功";
                    r.data = new { ret = iRet, uid = member.UId, uidx = member.UIdx, token = AccountBLL.getThirdLoginToken(member.UIdx) };

                    //Loger.WriteLog(LogFile.Test, "辰龙登录:uidx:" + member.UIdx + "|uid:" + member.UId + "|nickName:" + member.NickName + "|" + thirdInfo);
                }
            }
            else
            {
                r.code = "110";
                r.msg = "用户名或密码错误";
                //Loger.WriteLog(LogFile.Test, "辰龙登陆失败:" + thirdInfo + "," + sendurl);
            }
            return Json(r, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 华为Login

        /// <summary>
        /// 华为登录
        /// </summary>
        /// <param name="deviceType"></param>
        /// <param name="ThirdName"></param>
        /// <param name="visitoridx"></param>
        /// <returns></returns>
        public ActionResult HuaweiLogin(string deviceType = "unknow", string ThirdName = "miaobo", int visitoridx = 0)
        {
            MobileResult mr = new MobileResult();

            string key = Request["key"];
            string access_token = Request["access_code"];
            string openid = Request["openid"];
            string myKey = CryptoHelper.ToMD5(openid + "&miabo.tiange.com").ToLower();

            string nickName = Request["nickname"];
            string photo = Request["photo"];
            string gender = Request["gender"];

            if (string.IsNullOrEmpty(access_token) || string.IsNullOrEmpty(openid) || !myKey.Equals(key))
            {
                mr.code = "101";
                mr.msg = "参数错误！";
                return Content(JsonConvert.SerializeObject(mr));
            }

            OAuth.Huawei.HuaweiLogin hw = new OAuth.Huawei.HuaweiLogin();
            //获取华为账号唯一ID
            string myopenid = hw.GetOpenId(access_token);

            if (openid != myopenid)
            {
                mr.code = "102";
                mr.msg = "invalid openid";
                return Content(JsonConvert.SerializeObject(mr));
            }

            int iRet = -1;
            ThirdLoginParam param = new ThirdLoginParam();
            param.devType = deviceType;
            param.visitoridx = visitoridx;
            param.platForm = ThirdName;
            param.Account = myopenid;

            var thirdObj = new { nickname = nickName, gender = gender, photo = photo };
            var thirdInfo = JsonConvert.SerializeObject(thirdObj);// "{\"nickname\":\"apple001\",\"gender\":\"1\",\"photo\":\"http://qp.cl0579.com/Images/Faces/1.gif\"}";

            MemberInfo member = ThirdLoginBLL.VerifyThirdMember(ThirdLoginBLL.ThirdType.Huawei, thirdInfo, ref iRet, param);

            if (iRet > -1 && member.UIdx > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = new { ret = iRet, uid = member.UId, uidx = member.UIdx, token = AccountBLL.getThirdLoginToken(member.UIdx), areaid = member.PId };
            }
            else if (iRet == -4 || iRet == -5)
            {
                mr.code = "103";
                mr.msg = "账号绑定失败Code:" + iRet;
            }
            return Content(JsonConvert.SerializeObject(mr));
        }

        #endregion

        #region Google,Twitter,Fackbook

        public ActionResult Facebook(string access_code, string key, string deviceType = "未知", string appcode = "", string appversion = "")
        {
            LogHelper.WriteLog(LogFile.Test, "登陆日志：" + Request["access_code"]);

            var access_token = access_code;//; Request["access_code"];
            //var key = Request["key"];
            var myKey = CryptoHelper.ToMD5(access_token + "&miabo.tiange.com").ToLower();

            var mr = new Result();

            if (string.IsNullOrEmpty(access_token) || !myKey.Equals(key))
            {
                LogHelper.WriteLog(LogFile.Test, "mykey：" + myKey + ",key:" + key);

                mr.code = "101";
                mr.msg = "参数错误";
                return Json(mr, JsonRequestBehavior.AllowGet);
            }

            FaceBookLogin fb = new FaceBookLogin();
            string thirdInfo = fb.GetUserInfo(access_token);
            //string thirdInfo = "";

            //try
            //{
            //    FacebookAuthClient client = new FacebookAuthClient();
            //    thirdInfo = client.DoWork(access_token);
            //    client.Close();

            //}
            //catch (Exception ex)
            //{
            //    LogHelper.WriteLog(LogFile.Error, ex.ToString());
            //}

            if (string.IsNullOrEmpty(thirdInfo) || thirdInfo.Contains("Error"))
            {
                mr.code = "102";
                mr.msg = "获取第三方用户信息失败";
                return Json(mr, JsonRequestBehavior.AllowGet);
            }

            int iRet = -1;
            ThirdLoginParam param = new ThirdLoginParam();
            param.devType = deviceType;
            MemberInfo thirdMember = ThirdLoginBLL.VerifyThirdMember(ThirdLoginBLL.ThirdType.Facebook, thirdInfo, ref iRet, param);

            if (iRet > -1)
            {
                mr.code = "100";
                mr.msg = "操作成功";
                mr.data = new { ret = iRet, uidx = thirdMember.UIdx, uid = thirdMember.UId, token = AccountBLL.getThirdLoginToken(thirdMember.UIdx) };
                if (appcode == "com.homepagetw.ios") //ios台湾企业包
                {
                    //new AccountBLL().AS_insert_userinfo(thirdMember.UIdx, "localPackage", 1, appversion, 0, 0, "com.homepagetw.ios", "Facebook", "MAOBO-IOS-0000-0000");
                }
                else if (appcode == "com.homepagetw.android")//安卓台湾企业包
                {
                    //new AccountBLL().AS_insert_userinfo(thirdMember.UIdx, "localPackage", 1, appversion, 0, 0, "com.homepagetw.android", "Facebook", "MAOBO-Android-0000-0000");
                }
            }
            return Json(mr, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult Google(string appcode = "", string appversion = "")
        //{
        //    var access_code = Request["access_code"];
        //    var key = Request["key"];
        //    var deviceType = Request["deviceType"] ?? "ios";
        //    var myKey = CryptoHelper.ToMD5(access_code + "&miabo.tiange.com").ToLower();

        //    var mr = new Result();

        //    if (string.IsNullOrEmpty(access_code) || !myKey.Equals(key))
        //    {
        //        mr.code = "101";
        //        mr.msg = "参数错误";
        //        return Json(mr, JsonRequestBehavior.AllowGet);
        //    }

        //    //GoogleLogin g = new GoogleLogin();
        //    //string thirdInfo = "";
        //    //if (deviceType == "android")
        //    //{
        //    //    thirdInfo = g.GetUserInfo2(access_code);
        //    //}
        //    //else
        //    //{
        //    //    thirdInfo = g.GetUserInfo(access_code);
        //    //}

        //    string thirdInfo = "";
        //    try
        //    {
        //        ServiceGoogleAuth.GoogleAuthClient client = new GoogleAuthClient();
        //        thirdInfo = client.DoWork(access_code, deviceType);
        //        client.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.WriteLog(LogFile.Error, ex.ToString());
        //    }

        //    if (string.IsNullOrEmpty(thirdInfo) || !thirdInfo.Contains("sub") || thirdInfo.Contains("Error"))
        //    {
        //        LogHelper.WriteLog(LogFile.Test, "【Google获取信息失败】" + thirdInfo + ",token:" + access_code);
        //        mr.code = "102";
        //        mr.msg = "获取第三方用户信息失败";
        //        return Json(mr, JsonRequestBehavior.AllowGet);
        //    }
        //    LogHelper.WriteLog(LogFile.Test, "【Google获取信息】" + thirdInfo + ",token:" + access_code);

        //    int iRet = -1;
        //    ThirdLoginParam param = new ThirdLoginParam();
        //    MemberInfo thirdMember = ThirdLoginBLL.VerifyThirdMember(ThirdLoginBLL.ThirdType.Google, thirdInfo, ref iRet, param);

        //    if (iRet > -1)
        //    {
        //        mr.code = "100";
        //        mr.msg = "操作成功";
        //        mr.data = new { ret = iRet, uidx = thirdMember.UIdx, token = AccountBLL.getThirdLoginToken(thirdMember.UIdx) };
        //        if (appcode == "com.homepagetw.ios") //ios台湾企业包
        //        {
        //            new AccountBLL().AS_insert_userinfo(thirdMember.UIdx, "localPackage", 1, appversion, 0, 0, "com.homepagetw.ios", "Facebook", "MAOBO-IOS-0000-0000");
        //        }
        //        else if (appcode == "com.homepagetw.android")//安卓台湾企业包
        //        {
        //            new AccountBLL().AS_insert_userinfo(thirdMember.UIdx, "localPackage", 1, appversion, 0, 0, "com.homepagetw.android", "Facebook", "MAOBO-Android-0000-0000");
        //        }
        //    }
        //    LogHelper.WriteLog(LogFile.Test, "GoogleInfo：" + thirdInfo);
        //    return Json(mr, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult Twitter(string appcode = "", string appversion = "")
        //{
        //    // string resource_url = "https://api.twitter.com/1.1/users/show.json";

        //    // oauth application keys
        //    var oauth_token = Request.QueryString["token"];// "755667217883209728-JAfwbuIcemiJsFkBqF28DNsBbPwNUXm"; //"insert here...";
        //    var oauth_token_secret = Request.QueryString["token_secret"];// "jVlhKpQawnzPgdSYU2njNOwJs2LTl1am5xq3Y407ucKIa"; //"insert here...";
        //    var screen_name = Request.QueryString["screen_name"];//twitterDev
        //    var key = Request["key"];

        //    var myKey = CryptoHelper.ToMD5("twitter" + oauth_token + "|" + screen_name + "&miabo.tiange.com").ToLower();

        //    //var oauth_consumer_key = "EXD39lgP5xPdhkjeLGHs0X70V";// = "insert here...";
        //    //var oauth_consumer_secret = "S1C9oE3klPXmJoObGfIashtmzEdslROYtrwPgKsUnskZgbFBa0";// = "insert here...";

        //    if (string.IsNullOrEmpty(oauth_token) || string.IsNullOrEmpty(oauth_token_secret) || string.IsNullOrEmpty(screen_name) || !myKey.Equals(key))
        //    {
        //        return Json(new Result("101", "param error"), JsonRequestBehavior.AllowGet);
        //    }
        //    #region 注释不用的代码
        //    // oauth implementation details
        //    //var oauth_version = "1.0";
        //    //var oauth_signature_method = "HMAC-SHA1";

        //    //// unique request details
        //    //var oauth_nonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
        //    //var timeSpan = DateTime.UtcNow
        //    //    - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        //    //var oauth_timestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();


        //    ////create oauth signature
        //    //var baseFormat = "oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" +
        //    //                "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}&screen_name={6}"; 

        //    //var baseString = string.Format(baseFormat,
        //    //                            oauth_consumer_key,
        //    //                            oauth_nonce,
        //    //                            oauth_signature_method,
        //    //                            oauth_timestamp,
        //    //                            oauth_token,
        //    //                            oauth_version
        //    //                            , screen_name//, "twitterDev"
        //    //                            );

        //    //baseString = string.Concat("GET&", Uri.EscapeDataString(resource_url), "&", Uri.EscapeDataString(baseString));

        //    //var compositeKey = string.Concat(Uri.EscapeDataString(oauth_consumer_secret),
        //    //                        "&", Uri.EscapeDataString(oauth_token_secret));

        //    //string oauth_signature;
        //    //using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(compositeKey)))
        //    //{
        //    //    oauth_signature = Convert.ToBase64String(
        //    //        hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(baseString)));
        //    //}

        //    //// create the request header
        //    //var headerFormat = "OAuth oauth_nonce=\"{0}\", oauth_signature_method=\"{1}\", " +
        //    //                   "oauth_timestamp=\"{2}\", oauth_consumer_key=\"{3}\", " +
        //    //                   "oauth_token=\"{4}\", oauth_signature=\"{5}\", " +
        //    //                   "oauth_version=\"{6}\"";

        //    //var authHeader = string.Format(headerFormat,
        //    //                        Uri.EscapeDataString(oauth_nonce),
        //    //                        Uri.EscapeDataString(oauth_signature_method),
        //    //                        Uri.EscapeDataString(oauth_timestamp),
        //    //                        Uri.EscapeDataString(oauth_consumer_key),
        //    //                        Uri.EscapeDataString(oauth_token),
        //    //                        Uri.EscapeDataString(oauth_signature),
        //    //                        Uri.EscapeDataString(oauth_version)
        //    //                );

        //    //ServicePointManager.Expect100Continue = false;

        //    // make the request
        //    //var thirdInfo = TwitterHttpGet(ref resource_url, authHeader, screen_name);
        //    #endregion 注释不用的代码

        //    string thirdInfo = "";
        //    try
        //    {
        //        TwitterAuthClient client = new TwitterAuthClient();
        //        thirdInfo = client.DoWork(oauth_token, oauth_token_secret, screen_name);
        //        client.Close();

        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.WriteLog(LogFile.Error, ex.ToString());
        //    }

        //    Result mr = new Result();

        //    if (string.IsNullOrEmpty(thirdInfo) || thirdInfo.Contains("Error"))
        //    {
        //        LogHelper.WriteLog(LogFile.Test, "【Twitter获取信息失败】" + thirdInfo);
        //        mr.code = "102";
        //        mr.msg = "获取第三方用户信息失败";
        //        return Json(mr, JsonRequestBehavior.AllowGet);
        //    }

        //    MemberInfo thirdMember;
        //    int iRet = ThirdLoginBLL.VerifyThirdMember(BLL.ThirdLoginBLL.ThirdType.Twitter, out thirdMember, thirdInfo, "", "");
        //    if (iRet > -1)
        //    {
        //        mr.code = "100";
        //        mr.msg = "操作成功";
        //        mr.data = new { ret = iRet, uid = thirdMember.UId, uidx = thirdMember.UIdx, token = AccountBLL.getThirdLoginToken(thirdMember.UIdx) };
        //        if (appcode == "com.homepagetw.ios") //ios台湾企业包
        //        {
        //            //new AccountBLL().AS_insert_userinfo(thirdMember.UIdx, "localPackage", 1, appversion, 0, 0, "com.homepagetw.ios", "Facebook", "MAOBO-IOS-0000-0000");
        //        }
        //        else if (appcode == "com.homepagetw.android")//安卓台湾企业包
        //        {
        //            //new AccountBLL().AS_insert_userinfo(thirdMember.UIdx, "localPackage", 1, appversion, 0, 0, "com.homepagetw.android", "Facebook", "MAOBO-Android-0000-0000");
        //        }

        //    }
        //    LogHelper.WriteLog(LogFile.Test, "【Twitter登陆】ret:" + iRet + ",uid:" + thirdMember.UId + "thirdInfo:" + thirdInfo);
        //    return Json(mr, JsonRequestBehavior.AllowGet);
        //}

        //private static string TwitterHttpGet(ref string resource_url, string authHeader, string name)
        //{
        //    var postBody = "screen_name=" + name;
        //    resource_url += "?" + postBody;
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(resource_url);
        //    request.Headers.Add("Authorization", authHeader);
        //    request.Method = "GET";
        //    request.ContentType = "application/x-www-form-urlencoded";

        //    var thirdInfo = "";

        //    using (var response = request.GetResponse())
        //    {
        //        using (var sr = new StreamReader(response.GetResponseStream()))
        //        {
        //            //序列化用户信息
        //            thirdInfo = sr.ReadToEnd();
        //            //执行登陆
        //        }
        //    }
        //    return thirdInfo;
        //}

        #endregion

        #region 邀请人登陆奖励

        /// <summary>
        /// live.9158.com/Account/InviteReward
        /// </summary>
        /// <returns></returns>
        public ActionResult InviteReward()
        {
            InviteParam dicParam = CryptoHelper.GetAESBinaryModelParam<InviteParam>(CryptoHelper.Register_KEY);

            if (dicParam == null) { return ParamError("邀新奖励"); }

            int useridx = dicParam.useridx;
            int anchoridx = dicParam.anchoridx;
            string userid = dicParam.userid;
            string deviceid = dicParam.deviceId;
            string appVersion = dicParam.version;
            string inviteCode = dicParam.inviteCode;
            string deviceType = dicParam.deviceType;
            string userip = Tools.GetRealIP();
            int osType = deviceType.Equals("iPhone") ? 1 : 2;

            if (inviteCode.Length != 8) inviteCode = "";
            if (string.IsNullOrEmpty(inviteCode) || !Tools.IsLetterAndNumber(inviteCode))
            {
                string logMsg = "【新用户注册邀请码不符合要求】";
                //MongoService.InsertInviteAccess(4, useridx, inviteCode, deviceid, appVersion, userid, 0, logMsg);
                return new BaseController().ParamError();
            }

            MobileResult mr = new MobileResult();

            //邀请奖励调用
            //-1：邀请码不存在，-2：邀请码已使用，-3：当日平台总额已用完，-4：被邀请人非微信注册，-5：设备或账号已领取过，-6：自己不能邀请自己，1：success
            int inviteid = 0;
            int result = income.Reward_Invite(useridx, userid, inviteCode, osType, deviceid, appVersion, userip, ref inviteid);
            if (result > 0 && inviteid > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = result.ToString();
            }
            return Content(JsonConvert.SerializeObject(mr));
        }

        #endregion

        #region 登陆获取用户信息,退出

        /// <summary>
        /// 登陆获取用户信息
        /// </summary>
        /// <param name="account">useridx/userid/手机号</param>
        /// <param name="token">第三方账号登陆token/9158登陆md5用户名密码</param>
        /// <param name="loginType">9158:9158账号登陆 third:第三方账号登陆</param>
        /// <returns></returns>
        public ActionResult Login()
        {
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Register_KEY);
            if (dic == null || !dic.ContainsKey("account") || !dic.ContainsKey("token"))
            {
                return new BaseController().ParamError();
            }
            string param = CryptoHelper.GetBinaryStringParam(CryptoHelper.Register_KEY);
            string account = dic["account"];
            string token = dic["token"];
            string loginType = dic["logintype"] ?? "third";


            //0：不需要密码 1：需要密码 第三方登录时传1
            int needPass = 0, lType = 0;
            if (loginType == "third")
            {
                lType = 1;
                needPass = 1;
            }
            string userip = Tools.GetRealIP();

            Login_UserInfo lu = bll.Login(account, token, lType, userip, needPass);

            MobileResult mr = new MobileResult();

            if (lu != null && lu.useridx > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = lu;

                lu.mb = CryptoHelper.ToBase64(lu.cash.ToString());
                lu.cash = 0;
                lu.token = RandomHelper.GetRandomString(29, true, true, true, false, "");//token;

                string userTokenKey = CacheKeys.MiaoPai_USER_TOKEN + lu.useridx;
                MemcachedHelper.Store(userTokenKey, lu.token, new TimeSpan(7, 0, 0, 0));
            }

            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 用户退出登陆(喵拍)
        /// </summary>
        /// <param name="token"></param>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public ActionResult LoginOut(string token, int useridx = 0)
        {
            string userTokenKey = CacheKeys.MiaoPai_USER_TOKEN + useridx;
            string userTokenValue = MemcachedHelper.Get<string>(userTokenKey);

            if (useridx != 0)
            {
                if (token.Equals(userTokenValue))
                {
                    MemcachedHelper.Remove(userTokenKey);

                    //LogHelper.WriteLog(LogFile.Debug, string.Format("【喵拍退出登陆】{0},{1},{2}", useridx, token, userTokenValue));
                    return Content("success");
                }
            }
            return Content("fail");
        }

        #endregion

        #region 发送验证码返回值
        public static void SendCodeMsg(Result mr, int flag)
        {
            switch (flag)
            {
                case 1:
                    mr.code = "100";
                    mr.msg = "发送验证码成功";
                    break;
                case 0:
                    mr.code = "0";
                    mr.msg = "发送验证码失败";
                    break;
                case -2:
                    mr.code = "-2";
                    mr.msg = "验证码1分钟内只能发一次";
                    break;
                case -3:
                    mr.code = "-3";
                    mr.msg = "手机号发送已受限";
                    break;
                case -5:
                    mr.msg = "-5";
                    mr.msg = "发送验证码太过频繁";
                    break;
                default:
                    mr.code = "0";
                    mr.msg = "发送验证码失败";
                    break;
            }
        }
        #endregion

        #region 老挝账户注册
        //public ActionResult UserRegister() { 
        //[HttpPost]
        /// <summary>
        /// 验证userid 是否存在
        /// </summary>
        /// <returns></returns>
        public ActionResult useridCheck()
        {
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Register_KEY);
            if (dic == null)
            {
                return new BaseController().ParamError("CheckMobile");
            }
            var mr = new Result();
            //var reg = new Regex(Tools.TelRegex);
            string userid = dic["userId"].ToString();
            int iRet = 0;
            if (userid != "")
            {
                iRet = bll.useridCheck(userid);
                mr.code = "100";
                mr.msg = "success";
                mr.data = iRet;
            }
            return Content(JsonConvert.SerializeObject(mr));
        }
        //[HttpPost]
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public ActionResult userRegister()
        {
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Register_KEY);
            if (dic == null)
            {
                return new BaseController().ParamError("CheckMobile");
            }
            var mr = new Result();

            string userid = dic["userId"].ToString();
            string pwd = dic["pwd"].ToString();
            string phoneNumber = dic["phoneNumber"].ToString();
            string agentCode = dic["agentCode"].ToString();
            string devType = dic["devType"].ToString();
            //string channelid = dic["channelid"].ToString();
            string channelid = "0";
            if (dic.ContainsKey("channelid"))
            {
                channelid = dic["channelid"].ToString();
            }
            string deviceId = "";
            if (dic.ContainsKey("deviceId"))
            {
                deviceId = dic["deviceId"].ToString();
            }
            string code = "0"; //验证码
            if (dic.ContainsKey("code"))
            {
                code = dic["code"].ToString();
            }
            if (code.Equals("0") == false && code != "")
            {
                int ret = SendCodeBLL.valiPhoneCode(phoneNumber, code);
                if (ret == -2)
                {
                    mr.code = "-2";
                    mr.msg = "验证码失效";
                    Content(JsonConvert.SerializeObject(mr));
                }
                else if (ret == -3)
                {
                    mr.code = "-3";
                    mr.msg = "验证码不匹配";
                    return Content(JsonConvert.SerializeObject(mr));
                }
            }
            string InvitationCode = bll.GenerateStringID();
            string regIp = Tools.GetRealIP();
            //   Location loc = PositionHelper.GetLocationInfo(regIp);
            MemberInfo member = new MemberInfo();
            member.ip = regIp;
            member.UId = userid;
            member.phoneNum = phoneNumber;
            member.Pwd = CryptoHelper.ToMD5(pwd).ToLower();
            member.NickName = ThirdLoginBLL.VerifyNickName(userid);
            //member.Province = loc.Province;
            //member.City = loc.City;

            member.devType = devType;
            //if (!Tools.IsPhone(member.phoneNum))
            //{
            //    mr.code = "110";
            //    mr.msg = "手机号格式有误";
            //    return Json(mr);
            //}

            //if (!CacheBLL.VerifyRegisterIP())
            //{
            //    mr.code = "111";
            //    mr.msg = "同IP注册太过频繁";
            //    return Json(mr);
            //}
            if (agentCode.Length > 8)
            {
                mr.code = "101";
                mr.msg = "代理邀请码无效";
                mr.data = -3;
                return Content(JsonConvert.SerializeObject(mr));
            }
            int iRet = bll.I_userRegister(member, agentCode, InvitationCode, channelid, deviceId); ;
            if (iRet > 10)
            {

                mr.code = "100";
                mr.msg = "success";
                mr.data = iRet;
            }
            else
            {
                string[] msg = { "参数异常", "userid已存在", "手机号已被注册", "代理邀请码无效", "获取用户idx失败" };
                mr.code = "101";
                mr.msg = msg[-iRet];
                mr.data = iRet;
            }
            return Content(JsonConvert.SerializeObject(mr));
        }
        /// <summary>
        /// 修改密码 2019-4-2
        ///  type值为0 用手机号找回密码 ,值为1 用原密码修改
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult userRegisterUpPwd_W()
        {
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null)
            {
                return new BaseController().ParamError("CheckMobile");
            }
            var mr = new Result();
            string oldPwd = dic["oldPwd"].ToString();
            string Code = dic["Code"].ToString();
            string NewPwd = dic["newPwd"].ToString();
            string phoneNumber = dic["phoneNumber"].ToString();
            string uid = dic["userIdx"].ToString();

            if (uid == "")
            {
                uid = "0";
            }
            int iRet = 0;
            int type = 1;
            if (oldPwd == "" && Code != "")
            {
                type = 0;
                int ret = SendCodeBLL.valiPhoneCode(phoneNumber, Code);
                if (ret == -2)
                {
                    mr.code = "-2";
                    mr.msg = "验证码失效";
                    return Content(JsonConvert.SerializeObject(mr));
                }
                else if (ret == -3)
                {
                    mr.code = "-3";
                    mr.msg = "验证码不匹配";
                    return Content(JsonConvert.SerializeObject(mr));
                }
            }
            iRet = bll.userUpPwd(phoneNumber, CryptoHelper.ToMD5(oldPwd).ToLower(), CryptoHelper.ToMD5(NewPwd).ToLower(), type, int.Parse(uid));
            if (iRet == 1)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = iRet;
            }
            else
            {
                mr.code = "101";
                mr.msg = "原密码错误";
                mr.data = iRet;
            }


            return Content(JsonConvert.SerializeObject(mr));
        }

        public ActionResult userRegisterUpPwd()
        {
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Register_KEY);
            if (dic == null)
            {
                return new BaseController().ParamError("CheckMobile");
            }
            var mr = new Result();
            //var reg = new Regex(Tools.TelRegex);
            int userIdx = int.Parse(dic["userIdx"].ToString());
            string token = dic["upPwdToken"].ToString();
            string NewPwd = dic["newPwd"].ToString();
            int iRet = 0;
            string MCKToken = SendCodeBLL.getUpPwdToken(userIdx.ToString());

            if (token == MCKToken)
            {
                CacheHelper.SetCache(CacheKeys.LIVE_UpPwdToken + userIdx, "", 5);
                //MemcachedHelper.Remove(CacheKeys.LIVE_UpPwdToken + userIdx);
                iRet = bll.userRegisterUpPwd(userIdx, CryptoHelper.ToMD5(NewPwd).ToLower());
                if (iRet == 1)
                {
                    mr.code = "100";
                    mr.msg = "success";
                    mr.data = iRet;
                }
                else
                {
                    mr.code = "101";
                    mr.msg = "修改失败";
                    mr.data = iRet;
                }
            }
            else
            {
                if (MCKToken == "")
                {
                    mr.code = "103";
                    mr.msg = "token失效";
                    mr.data = 0;
                }
                else
                {
                    mr.code = "102";
                    mr.msg = "非法请求";
                    mr.data = 0;
                }

            }

            return Content(JsonConvert.SerializeObject(mr));
        }
        
        //string Code = dic["Code"].ToString();

        public ActionResult CheckMobileLaoWo()
        {
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Register_KEY);
            if (dic == null)
            {
                return new BaseController().ParamError("CheckMobile");
            }
            var mr = new Result();
            string useridx = dic["userIdx"].ToString();
            string phoneNum = dic["phoneNum"].ToString();
            string Code = dic["code"].ToString();
            int ret = SendCodeBLL.valiPhoneCode(phoneNum, Code);
            string token = bll.GenerateStringID();
            SendCodeBLL.setUpPwdToken(token, useridx);
            if (ret == 1)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = token;
                return Content(JsonConvert.SerializeObject(mr));
            }
            else if (ret == -3)
            {
                mr.code = "-3";
                mr.msg = "验证码不匹配";
                return Content(JsonConvert.SerializeObject(mr));
            }
            else if (ret == -2)
            {
                mr.code = "-2";
                mr.msg = "验证码失效";
                return Content(JsonConvert.SerializeObject(mr));

            }
            else
            {
                mr.code = "0";
                mr.msg = "异常";
                return Content(JsonConvert.SerializeObject(mr));
            }
        }

        #endregion
        #region  面具公园模块
        //[HttpPost]
        /// <summary>
        /// 验证手机号是否存在
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IphoneCheck()
        {
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null)
            {
                return new BaseController().ParamError("CheckMobile");
            }
            var mr = new Result();
            //var reg = new Regex(Tools.TelRegex);
            string phone = dic["phone"].ToString();
            int iRet = 0;
            if (phone != "")
            {
                iRet = bll.phoneCheck(phone);
                mr.code = "100";
                mr.msg = "success";
                mr.data = iRet;
            }
            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 修改密码 2019-4-2
        /// 用手机号找回密码  oldPwd="" && Code != ""
        ///  type值为0 用手机号找回密码 ,值为1 用原密码修改
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult userUpPwd()
        {
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null)
            {
                return new BaseController().ParamError("CheckMobile");
            }
            var mr = new Result();
            string oldPwd = dic["oldPwd"].ToString();
            string Code = dic["Code"].ToString();
            string NewPwd = dic["newPwd"].ToString();
            string phoneNumber = dic["phoneNumber"].ToString();
            string uid = dic["userIdx"].ToString();

            if (uid == "")
            {
                uid = "0";
            }
            int iRet = 0;
            int type = 1;
            //手机号验证
            if (oldPwd == "" && Code != "")
            {
                type = 0;
                int ret = SendCodeBLL.valiPhoneCode(phoneNumber, Code);
                if (ret == -2)
                {
                    mr.code = "-2";
                    mr.msg = "验证码失效";
                    return Content(JsonConvert.SerializeObject(mr));
                }
                else if (ret == -3)
                {
                    mr.code = "-3";
                    mr.msg = "验证码不匹配";
                    return Content(JsonConvert.SerializeObject(mr));
                }
            }
            iRet = bll.userUpPwd(phoneNumber, CryptoHelper.ToMD5(oldPwd).ToLower(), CryptoHelper.ToMD5(NewPwd).ToLower(), type, int.Parse(uid));
            if (iRet == 1)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = iRet;
            }
            else
            {
                mr.code = "101";
                mr.msg = "原密码错误";
                mr.data = iRet;
            }


            return Content(JsonConvert.SerializeObject(mr));
        }
        /// <summary>
        /// 绑定手机号
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult userBindTel()
        {
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null)
            {
                return new BaseController().ParamError("CheckMobile");
            }
            var mr = new Result();
            string Code = dic["Code"].ToString();
            string phoneNumber = dic["phoneNumber"].ToString();
            string uid = dic["userIdx"].ToString();
            //string type = dic["type"].ToString();  // 1普通用户绑定 0 游客绑定
            //string pwd = dic["pwd"].ToString();
            string type = "1";
            string pwd = "";

            int iRet = 0;
            int ret = SendCodeBLL.valiPhoneCode(phoneNumber, Code);
            if (ret == -2)
            {
                mr.code = "-2";
                mr.msg = "验证码失效";
                return Content(JsonConvert.SerializeObject(mr));
            }
            else if (ret == -3)
            {
                mr.code = "-3";
                mr.msg = "验证码不匹配";
                return Content(JsonConvert.SerializeObject(mr));
            }
            iRet = bll.userBindTel(phoneNumber, int.Parse(uid), int.Parse(type), CryptoHelper.ToMD5(pwd).ToLower());
            if (iRet == 1)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = iRet;
            }
            else if (iRet == 2)
            {
                mr.code = "102";
                mr.msg = "手机号已经被注册";
                mr.data = iRet;
            }
            else
            {
                mr.code = "101";
                mr.msg = "绑定失败";
                mr.data = iRet;
            }


            return Content(JsonConvert.SerializeObject(mr));
        }
        /// <summary>
        /// 手机号注册
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult userRegisterPhone()
        {
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null)
            {
                return new BaseController().ParamError("CheckMobile");
            }
            var mr = new Result();
            string Code = dic["Code"].ToString();
            string Pwd = dic["Pwd"].ToString();
            string phoneNumber = dic["phoneNumber"].ToString();
            string deviceType = dic["devType"].ToString();
            string deciveId = dic["deciveId"].ToString();
            string channelid = dic["channelid"].ToString();

            int ret = SendCodeBLL.valiPhoneCode(phoneNumber, Code);
            if (ret == -2)
            {
                mr.code = "-2";
                mr.msg = "验证码失效";
                return Content(JsonConvert.SerializeObject(mr));
            }
            else if (ret == -3)
            {
                mr.code = "-3";
                mr.msg = "验证码不匹配";
                return Content(JsonConvert.SerializeObject(mr));
            }
            string InvitationCode = bll.GenerateStringID();
            string regIp = Tools.GetRealIP();

            var iRet = bll.userRegisterPhonenumber(CryptoHelper.ToMD5(Pwd).ToLower(), phoneNumber, InvitationCode, deviceType,regIp,deciveId,int.Parse(channelid));
            if (iRet > 1)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = iRet;
            }
            else if(iRet==-2)
            {
                mr.code = "101";
                mr.msg = "手机号已被注册";
                mr.data = iRet;
            }
            else if (iRet == -4)
            {
                mr.code = "102";
                mr.msg = "获取用户idx失败";
                mr.data = iRet;
            }
            else
            {
                mr.code = "104";
                mr.msg = "注册失败";
                mr.data = iRet;
            }
            return Content(JsonConvert.SerializeObject(mr));
        }
        /// <summary>
        /// 手机验证码发送
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SendCode()
        {
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null)
            {
                return new BaseController().ParamError("CheckMobile");
            }
            var mr = new Result();
            string phoneNum = dic["phoneNum"].ToString();
            string type = dic["type"].ToString();
            string Areacode = "+86";
            if (dic.ContainsKey("Areacode"))
            {
                Areacode = dic["Areacode"].ToString();
            }
            if (type == "1")
            {
                int flag = SendCodeBLL.sendSmsInfo("0", "", phoneNum, "", 1, Areacode);
                SendCodeMsg(mr, flag);
                mr.data = phoneNum;
            }
            else
            {
                List<UserInvitationInfo> list = user.phoneNumGetUserRealNameInfoSet(phoneNum);
                if (list != null && list.Count > 0)
                {
                    int flag = SendCodeBLL.sendSmsInfo(list[0].userIdx.ToString(), "", phoneNum, "", 3, Areacode);
                    SendCodeMsg(mr, flag);
                    mr.data = list[0].userIdx.ToString();
                }
                else
                {
                    mr.code = "108";
                    mr.msg = "手机号未注册";
                    mr.data = 0;

                }
            }

            return Content(JsonConvert.SerializeObject(mr));
        }

        #endregion

        public ActionResult test()
        {
            var list = new List<MemberInfo>()
            { new MemberInfo { Account="123123|213"},
             new MemberInfo{Account="213|1231"}
            };
            return Content(CryptoHelper.ToMD5("12345").ToLower());

        }
    }
}