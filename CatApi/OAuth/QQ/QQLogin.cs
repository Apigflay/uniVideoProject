using Common;
using Common.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Web;

namespace OAuth.QQ
{
    /**
     * QQ互联API
     * 
     * 登录流程
     * 1 前端跳转qq授权页面
     * 2 获取AccessToken
     * 3 根据获取到的AccessToken获取openid
     * 4 根据AccessToken和openid获取到用户信息
     * 
     * 参考文档
     * http://www.tuicool.com/articles/EzeI3qb
     * 
     * @author zhaorui
     * @createDate 2016年8月
     * 
     * */
    public class QQLogin
    {
        private QQOAuthConfig config = new QQOAuthConfig();

        /// <summary>
        /// Step1：获取授权URL
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public string GetAuthCodeURL(string state)
        {
            string api = "/oauth2.0/authorize";

            string url = String.Format("{0}?response_type=code&client_id={1}&redirect_uri={2}&state={3}",
                config.BaseURL + api, config.AppKey, HttpUtility.UrlEncode(config.RedirectURL), state);
            return url;
        }

        /// <summary>
        /// step2： 获取AccessToken
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetAccessToken(string code)
        {
            string api = "/oauth2.0/token";

            string queryString = String.Format("?grant_type=authorization_code&client_id={0}&redirect_uri={1}&client_secret={2}&code={3}",
                config.AppKey, HttpUtility.UrlEncode(config.RedirectURL), config.AppSecret, code);

            string url = config.BaseURL + api;

            string result = HttpHelper.HttpGet(url + queryString);

            NameValueCollection nvc = OAuth.QueryString.GetQueryList(result);

            return nvc["access_token"];
        }

        /// <summary>
        /// Step3：获取用户永久标识openid
        /// </summary>
        /// <param name="astoken"></param>
        /// <returns></returns>
        public string GetOpenID(string access_token)
        {
            string api = "/oauth2.0/me";

            string url = String.Format("{0}?access_token={1}", config.BaseURL + api, access_token);
            //callback( {"client_id":"204515","openid":"E1DB195456CAE69CD85315B010B614B4"} );

            string result = HttpHelper.HttpGet(url);
            result = result.Trim().ExTrimPrefix("callback(").ExTrimSuffix(");");

            return JsonConvert.DeserializeAnonymousType(result, new QQShortUser()).Openid;
        }

        /// <summary>
        /// Step3：获取用户唯一标识unionid
        /// </summary>
        /// <param name="astoken"></param>
        /// <returns></returns>
        public string GetUnionID(string access_token)
        {
            string api = "/oauth2.0/me";

            string url = String.Format("{0}?access_token={1}&unionid=1", config.BaseURL + api, access_token);
            //callback( {"client_id":"204515","openid":"E1DB195456CAE69CD85315B010B614B4","unionid":"YOUR_UNIONID"} );

            string result = HttpHelper.HttpGet(url);
            result = result.Trim().ExTrimPrefix("callback(").ExTrimSuffix(");");

            return JsonConvert.DeserializeAnonymousType(result, new QQShortUser()).unionid;
        }

        /// <summary>
        /// Step4：获取用户信息
        /// API地址http://wiki.open.qq.com/wiki/website/get_user_info
        /// </summary>
        /// <param name="astoken"></param>
        /// <param name="openid"></param>
        /// <returns>{"ret": 0,"msg": "","is_lost":0,"nickname": "爱你哟","gender": "男","province": "浙江","city": "杭州","year": "2016"}</returns>
        public string GetUserInfo(string access_token, string openid, string appName)
        {
            string api = "/user/get_user_info";

            string url = String.Format("{0}?access_token={1}&oauth_consumer_key={2}&openid={3}",
                config.BaseURL + api, access_token, config.AppKey, openid);
            if (appName == "miaopai")
            {
                url = String.Format("{0}?access_token={1}&oauth_consumer_key={2}&openid={3}",
                config.BaseURL + api, access_token, "1105912221", openid);
            }

            return HttpHelper.HttpGet(url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="openid"></param>
        /// <param name="appName"></param>
        /// <returns>
        /// 0：正确返回，其他：失败
        /// 失败见http://wiki.open.qq.com/wiki/website/%E5%85%AC%E5%85%B1%E8%BF%94%E5%9B%9E%E7%A0%81%E8%AF%B4%E6%98%8E
        /// </returns>
        public QQUser Get_QQUserInfo(string access_token, string openid, string appName)
        {
            string api = "/user/get_user_info";

            string url = String.Format("{0}?access_token={1}&oauth_consumer_key={2}&openid={3}",
                config.BaseURL + api, access_token, config.AppKey, openid);
            if (appName == "miaopai")
            {
                url = String.Format("{0}?access_token={1}&oauth_consumer_key={2}&openid={3}",
                config.BaseURL + api, access_token, "1105912221", openid);
            }

            string jsonInfo = HttpHelper.HttpGet(url);

            return JsonConvert.DeserializeObject<QQUser>(jsonInfo);
        }
    }
}
