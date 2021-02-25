using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;

namespace OAuth.Weixin
{
    /**
     * 微信登录API
     * 
     * 登录流程
     * 1 前端跳转微信授权页面
     * 2 获取AccessToken
     * 3 授权后接口调用，通过code获取access_token
     * 4 根据AccessToken和openid获取到用户信息（UnionID机制）
     * 
     * 参考文档
     * https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&t=resource/res_list&verify=1&id=open1419316518&token=&lang=zh_CN
     * 
     * @author zhaorui
     * @createDate 2016年8月
     * 
     * */
    public class WeixinLogin
    {
        private static BaseOAuthConfig config = new WeixinAuthConfig();
        private string appKey = config.AppKey;
        private string appSecret = config.AppSecret;

        //public WeixinLogin()
        //{
        //}
        //public WeixinLogin(int appid)
        //{
        //    if (appid == 1)//喵播
        //    {
        //        appKey = "wx10ada28ba95092ce";
        //        appSecret = "61bb6a6402bbf48ab851a8416a352e38";
        //    }
        //    else if (appid == 2)//喵拍
        //    {
        //        appKey = "wxaee383b67d66b862";
        //        appSecret = "4146c1c15c8887a3d9916ef8fbcedcd7";
        //    }
        //    else if (appid == 3)
        //    {
        //        appKey = "wx410814d7967b7957";
        //        appSecret = "600148f3377be52bc9a71fd63315af52";
        //    }
        //    else if (appid == 4)
        //    {
        //        appKey = "wxb47c65811768a4f5";
        //        appSecret = "eae775daf40ad8f3c366901b7ea8b1e8";
        //    }
        //}

        /// <summary>
        /// Step1：获取授权验证URL
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public string GetAuthCodeURL(string state)
        {
            string api = "https://open.weixin.qq.com/connect/oauth2/authorize";

            string url = String.Format("{0}?appid={1}&redirect_uri={2}&response_type=code&scope=snsapi_userinfo&state={3}#wechat_redirect",
                api, config.AppKey, HttpUtility.UrlEncode(config.RedirectURL), state);
            return url;
        }

        /// <summary>
        /// 授权后接口调用
        /// Step2：通过code获取access_token
        /// </summary>
        /// <param name="code"></param>
        /// <returns>
        /// 正确返回
        /// {"access_token":"i6AAduMTy9rIn07IZJQ3b2lsqwwrvcycTG21AZZxFC2EG4HfpddEpNLCg-l5tXx3Xk54oR9myW-a-phNWP3Br-0dZ9epSLmQ6xvBu3OiN0Y",
        /// "expires_in":7200,
        /// "refresh_token":"JckgEiZxbXnEGQCvfl8_bir8Ey3b4SSWmQ1ESxiI2yhfVTXyUmLuRoZa8cV-62Pgxu9hpCY3ygJimByqSJKEdTYulSAXOfcBJYtXJg3tC1A",
        /// "openid":"olPUPwLuwwVAfkvTrjqrMfhI5Mg8",
        /// "scope":"snsapi_userinfo",
        /// "unionid":"og7NiuJciP3MVY2loKHptAFkEEnI"}
        /// 错误返回
        /// 
        /// </returns>
        //public WeixinToken GetAccessToken(string code)
        //{
        //    string api = "/sns/oauth2/access_token";
        //    string url = String.Format("{0}?appid={1}&secret={2}&code={3}&grant_type=authorization_code",
        //        config.BaseURL + api, config.AppKey, config.AppSecret, code);

        //    string result = HttpHelper.HttpGet(url);

        //    return JsonConvert.DeserializeObject<WeixinToken>(result);
        //}
        public Dictionary<string, string> GetDictAccessToken(string code, int platForm)
        {
            string api = "/sns/oauth2/access_token";
            string url = String.Format("{0}?appid={1}&secret={2}&code={3}&grant_type=authorization_code",
                config.BaseURL + api, appKey, appSecret, code);

            if (platForm > 1)
            {
                url = GetAccessTokenURL(code, platForm, api, url);
            }
            string result = HttpHelper.HttpGet(url);
            //if (result.Contains("errcode"))
            //{
            //    LogHelper.WriteLog(LogFile.Error, "【AccessToken出错】" + result + ",url:" + url);
            //}
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
        }

        /// <summary>
        /// Step3：获取用户信息
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="openid"></param>
        /// <returns>
        /// 正确返回：用户信息
        /// 错误返回：{"errorcode":40003,"errmsg":"invalid openid"}
        /// </returns>
        public string GetUserInfo(string access_token, string openid)
        {
            string api = "/sns/userinfo";
            string url = String.Format("{0}?access_token={1}&openid={2}&lang=zh_CN",
                config.BaseURL + api, access_token, openid);
            return HttpHelper.HttpGet(url);
        }
        public WeiXinUser Get_WxUserInfo(string access_token, string openid)
        {
            string api = "/sns/userinfo";
            string url = String.Format("{0}?access_token={1}&openid={2}&lang=zh_CN",
                config.BaseURL + api, access_token, openid);

            string jsonData = HttpHelper.HttpGet(url);

            return JsonConvert.DeserializeObject<WeiXinUser>(jsonData);
        }

        private static string GetAccessTokenURL(string code, int platForm, string api, string url)
        {
            if (platForm == 2)//喵拍应用
            {
                url = String.Format("{0}?appid={1}&secret={2}&code={3}&grant_type=authorization_code",
                config.BaseURL + api, "wxaee383b67d66b862", "4146c1c15c8887a3d9916ef8fbcedcd7", code);
            }
            else if (platForm == 3)//马甲包1 add 2017/3/10
            {
                url = String.Format("{0}?appid={1}&secret={2}&code={3}&grant_type=authorization_code",
                config.BaseURL + api, "wx410814d7967b7957", "600148f3377be52bc9a71fd63315af52", code);
            }
            else if (platForm == 4)//马甲包2 add 2017/3/10
            {
                url = String.Format("{0}?appid={1}&secret={2}&code={3}&grant_type=authorization_code",
                config.BaseURL + api, "wxb47c65811768a4f5", "eae775daf40ad8f3c366901b7ea8b1e8", code);
            }
            else if (platForm == 5)//马甲包3 add 2017-03-14
            {
                url = String.Format("{0}?appid={1}&secret={2}&code={3}&grant_type=authorization_code",
                config.BaseURL + api, "wx2ea15bb0da08b735", "19a65cade37c4886db5c3881b189cae6", code);
            }
            else if (platForm == 6)//马甲包4 add 2017-03-14
            {
                url = String.Format("{0}?appid={1}&secret={2}&code={3}&grant_type=authorization_code",
                config.BaseURL + api, "wx988bfb97f1c8c4ca", "be68dbc73e3d1cbf6b41c3d0ff70f5ed", code);
            }
            else if (platForm == 7)//马甲包5 add 2017-03-14
            {
                url = String.Format("{0}?appid={1}&secret={2}&code={3}&grant_type=authorization_code",
                config.BaseURL + api, "wx1f048c2715ca7a76", "2175eac07ebc54f4893419c14e68b361", code);
            }
            //else if (platForm == 8)//马甲包6 add 2017-09-07
            //{
            //    url = String.Format("{0}?appid={1}&secret={2}&code={3}&grant_type=authorization_code",
            //    config.BaseURL + api, "wx2ea15bb0da08b735", "2175eac07ebc54f4893419c14e68b361", code);
            //}
            else if (platForm == 9)
            {
                url = String.Format("{0}?appid={1}&secret={2}&code={3}&grant_type=authorization_code",
               config.BaseURL + api, "wx236edeff3562212b", "3d4b3c898b05333f69d95153175dd386", code);
            }
            return url;
        }
    }
}
