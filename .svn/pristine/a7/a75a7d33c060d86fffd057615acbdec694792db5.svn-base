using System;
using System.Web.Mvc;
using Common;
using Common.Core;
using ThirdAPI;
using Model;
using System.Collections.Specialized;
using System.Web;
using BLL;
using Newtonsoft.Json;
using OAuth.QQ;
using Model.Param;
using BLL.Common;

namespace WebAPI.Controllers
{
    public class PassportController : BaseController
    {
        private QQLogin qq = new QQLogin();
        private OAuth.Weixin.WeixinLogin wx = new OAuth.Weixin.WeixinLogin();
        AccountBLL account = new AccountBLL();

        #region 业务无关

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logintype">1：qq,2:wechat,3:weibo,4</param>
        /// <param name="from"></param>
        /// <returns></returns>
        public ActionResult ThirdLogin(int logintype = 1)
        {
            var from = Request["from"] ?? "http://live.9158.com";
            var authURL = "http://live.9158.com/";
            var state = DateTime.Now.Ticks.ToString();
            Session["state"] = state;

            if (logintype == 1)
            {
                authURL = qq.GetAuthCodeURL(state);
            }
            else if (logintype == 2)
            {
                authURL = wx.GetAuthCodeURL(state);
            }
            else if (logintype == 3)
            {
                //authURL = wb.GetAuthCodeURL(state);
            }
            CookieHelper.SetCookie("mbLoginGoto_URL", from);

            return Redirect(authURL);
        }

        /// <summary>
        /// QQ登陆成功授权后回调地址
        /// </summary>
        /// <returns></returns>
        public ActionResult QQCallback()
        {
            //Step1:获取code,state
            string code = Request.QueryString["code"];
            string state = Request.QueryString["state"];
            string sessionState = Session["state"].ToString();
            string defaultURL = "http://live.9158.com";
            string gotoURL = CookieHelper.GetCookieValue("mbLoginGoto_URL") ?? defaultURL;

            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(state) || sessionState != state)
            {
                return Redirect(gotoURL);
            }

            //Step2:通过Authorization Code获取Access_token
            var access_token = qq.GetAccessToken(code);

            //Step3:使用Access_token获取OpenID
            var openid = qq.GetOpenID(access_token);

            //Step4:使用Acces_token,OpenID，appid获取用户信息
            //var thirdInfo = qq.GetUserInfo(access_token, openid, "miaobo");
            QQUser qq_info = qq.Get_QQUserInfo(access_token, openid, "miaobo");

            if (qq_info.ret != 0)
            {
                return Redirect(defaultURL);
            }

            //验证成功用户信息写入cookie
            if (LiveBLL.Get_TestAccount(2, 0, openid) > 0)
            {
                MemberInfo m_info = new MemberInfo();
                m_info.Account = openid;
                m_info.NickName = qq_info.nickname;
                m_info.Photo = qq_info.figureurl_qq_2;
                m_info.ip = Tools.GetRealIP();

                //string m_jsonInfo = JsonConvert.SerializeObject(m_info);

                string MemKey = "Live_M" + openid;
                string cookieValue = CryptoHelper.ToBase64(openid);

                CookieHelper.SetCookie("Live_M", cookieValue, DateTime.Now.AddMinutes(20));
                MemcachedHelper.Store(MemKey, openid, new TimeSpan(0, 20, 0));

                LogHelper.WriteLog(LogFile.Log, "1【QQ网页登陆成功】c_v:" + cookieValue + ",memV:" + openid);
            }

            return Redirect(gotoURL);
        }

        public ActionResult WeiXinCallback()
        {
            //Step1:获取code,state
            string code = Request.QueryString["code"];
            string state = Request.QueryString["state"];

            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(state))
            {
                return Redirect("http://live.9158.com/login.html");
            }

            //if (!string.IsNullOrEmpty(code))
            //{
            //Step2:通过Authorization Code获取Access_token和openid
            //OAuth.Weixin.WeixinToken token = wx.GetAccessToken(code);

            //Step3:使用Acces_token,OpenID，appid获取用户信息
            //string thirdInfo =wx.GetUserInfo(access_token, qq.GetUserInfo(access_token, openid);
            //}

            return Content("111");
        }

        #endregion

        #region 实名认证
        /// <summary>
        /// Step1 芝麻认证客户端初始化
        /// </summary>
        /// <returns></returns>
        public ActionResult ZhimaAuth()
        {
            ZhimaAuthParam dic = CryptoHelper.GetAESBinaryModelParam<ZhimaAuthParam>(CryptoHelper.Live_KEY);
            if (dic == null || dic.useridx <= 0) { return new BaseController().ParamError("芝麻认证"); }

            ZhimaAuthLib.ZhiMaInfo zm = new ZhimaAuthLib.ZhiMaInfo();
            int useridx = dic.useridx;
            zm.name = dic.name;
            zm.certNo = dic.certno;
            //zm.certType = "IDENTITY_CARD";

            MobileResult mr = new MobileResult();
            mr.code = "100";
            mr.msg = "success";

            if (dic.apiversion == 2)
            {
                zm.certScene = ZhimaAuthLib.CertScene.FACE_SDK;

                mr.data = new
                {
                    bizNo = ZhimaAuthLib.ZhimaCustomerCertificationInitialize(zm),
                    merchantID = "268820000178891747194"
                };
            }
            else if (dic.apiversion == 3)//add 2017-11-22
            {
                zm.certScene = ZhimaAuthLib.CertScene.SMART_FACE;

                string bizNo = ZhimaAuthLib.ZhimaCustomerCertificationInitialize(zm);
                //string returnURL = string.Empty;

                mr.data = new
                {
                    bizNo = bizNo,
                    link = ZhimaAuthLib.ZhimaCustomerCertificationCertify(dic.returnURL, bizNo)
                };

                MemcachedHelper.Set("Live_Mem_CertBizno" + useridx, bizNo, 5);
            }
            else
            {
                string token = ZhimaAuthLib.ZhimaCustomerCertifyInitial(useridx, zm);
                string zhimaApply = ZhimaAuthLib.ZhimaCustomerCertifyApply(token);

                mr.data = zhimaApply;
            }
            return Json(mr, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Step2 芝麻验签
        /// </summary>
        /// <returns></returns>
        public ActionResult ZhimaVerifySign()
        {
            ZhimaAuthParam dic = CryptoHelper.GetAESBinaryModelParam<ZhimaAuthParam>(CryptoHelper.Live_KEY);
            if (dic == null || dic.useridx <= 0) { return new BaseController().ParamError(); }

            MobileResult mr = new MobileResult();
            Certification cert = new Certification();
            //ZhimaAuthLib.ZhiMaInfo zm = new ZhimaAuthLib.ZhiMaInfo();
            //zm.certNo = dic.certno;
            //zm.name = dic.name;
            bool flag = false;
            string bizNo = dic.bizNo;
            string result = string.Empty;

            if (dic.apiversion == 2 | dic.apiversion == 3)
            {
                if (string.IsNullOrEmpty(bizNo))
                {
                    bizNo = MemcachedHelper.Get<string>("Live_Mem_CertBizno" + dic.useridx);
                    result = ZhimaAuthLib.ZhimaCustomerCertificationQuery(bizNo);
                }
                else
                {
                    result = ZhimaAuthLib.ZhimaCustomerCertificationQuery(dic.bizNo);
                }

                if (result != null && result.Equals("true"))
                {
                    flag = true;
                    cert.openid = bizNo;

                    mr.code = "100";
                    mr.msg = "success";
                }
            }
            else
            {
                #region V1 Logic

                string param = dic.param;
                string sign = dic.sign;
                result = ZhimaAuthLib.decryptAndVerifySign(param, sign);

                NameValueCollection nvc = TextHelper.GetQueryList(result);
                if (nvc["certify_result"] == "true" && nvc["real_name_flag"] == "true")
                {
                    flag = true;
                    cert.openid = nvc["open_id"];

                    mr.code = "100";
                    mr.msg = "success";
                    mr.data = result;
                }

                #endregion
            }

            //验证成功入库操作
            if (flag)
            {
                cert.useridx = dic.useridx;
                cert.certNo = dic.certno;
                cert.realName = dic.name;
                cert.userip = Tools.GetRealIP();

                account.Certification_Save(cert);
            }

            LogHelper.WriteLog(LogFile.Info, "芝麻认证结果|{0}|{1}|{2}|{3}|{4}", dic.useridx, result, dic.apiversion, dic.bizNo, dic.name);

            return Json(mr, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 人工认证
        /// </summary>
        /// <returns></returns>
        //[RequireHttps]
        public ActionResult HumanAuth()
        {
            MobileResult mr = new MobileResult();
            HumanCertAuth cert = new HumanCertAuth();
            int useridx = cert.useridx = Convert.ToInt32(Request["useridx"]);
            cert.idType = Convert.ToInt32(Request["idtype"]);
            cert.realName = Request["realname"].Trim();
            cert.certNo = Request["certno"].Trim();
            cert.phoneNo = Request["phoneno"].Trim();
            cert.nation = Request["nation"];
            cert.usertoken = Request["usertoken"];//金汉下发的32位字符
            cert.signature = Request["signature"];//Md5("a="+useridx+"b="+certno+"mbAuth)$.")
            cert.userip = Tools.GetRealIP();
            string mySign = CryptoHelper.ToMD5("a=" + useridx + "b=" + cert.certNo + "mbAuth)$.").ToLower();

            if (cert == null || cert.idType <= 0 || cert.useridx <= 0 ||
                string.IsNullOrEmpty(cert.phoneNo) ||
                string.IsNullOrEmpty(cert.realName) ||
                string.IsNullOrEmpty(cert.usertoken) ||
                string.IsNullOrEmpty(cert.signature))
            {
                mr.code = "101";
                mr.msg = "Param Error";
                return Json(mr, JsonRequestBehavior.AllowGet);
            }
            if (mySign != cert.signature)
            {
                mr.code = "102";
                mr.msg = "signature Error";
                LogHelper.WriteLog(LogFile.Debug, "【人工认证验签错误】sign:" + cert.signature + ",mySign:" + mySign);
                return Json(mr, JsonRequestBehavior.AllowGet);
            }

            HttpFileCollection files = HttpContext.ApplicationInstance.Request.Files;

            //新版本不用上传用户图片
            if (cert.apiVersion == 2)
            {
                if (files == null && files.AllKeys.Length < 2)
                {
                    mr.code = "103";
                    mr.msg = "Please Select A Picture";
                    return Json(mr, JsonRequestBehavior.AllowGet);
                }
            }

            int isAutoAudit = 0;
            int ret = account.HumanCert_Save(cert, ref isAutoAudit);
            if (ret == -2)
            {
                mr.code = "110";
                mr.msg = "已提交，请等待审核结果";
            }
            else if (ret == -3)
            {
                mr.code = "111";
                mr.msg = "已经实名过,请大退APP查看";
            }
            else if (ret == -4)
            {
                mr.code = "113";
                mr.msg = "已达上限,明日再来提交";
            }
            else
            {
                mr.code = "114";
                mr.msg = "提交资料失败，请稍后再试";
            }

            if (ret < 1)
            {
                string msg = JsonConvert.SerializeObject(cert);
                LogHelper.WriteLog(LogFile.Log, "人工实名认证结果：{0},idtype:{1},certno:{2},name:{3},nation:{4},phoneNo:{5},{6},{7},{8}"
                    , useridx, cert.idType, cert.certNo, cert.realName, cert.nation, cert.phoneNo, ret, mr.msg, isAutoAudit);
                return Content(JsonConvert.SerializeObject(mr));
            }

            string webPic_1 = "", webPic_2 = "", webPic_3 = "";
            if (cert.apiVersion == 0)
            {
                PicSaveBLL.SaveCertPhoto(useridx, files, out webPic_1, out webPic_2, out webPic_3);
            }

            //修改实名认证图片地址
            int result = account.UpdateCertPhoto(useridx, webPic_1, webPic_2, webPic_3, ret);
            if (result > 0)
            {
                mr.code = "100";
                mr.msg = "success";
            }
            else
            {
                mr.code = "114";
                mr.msg = "修改图片地址失败";
            }
            LogHelper.WriteLog(LogFile.Log, "实名认证结果：{0},提交资料结果:{1},修改图片地址结果：{2},{3},{4}", useridx, ret, result, mr.msg, mr.code);

            return Json(mr, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
