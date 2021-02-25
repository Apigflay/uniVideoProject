using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace OAuth.SinaWeibo
{
    public class SinaWeiboLogin
    {
        private static BaseOAuthConfig config = new SinaWeiboAuthConfig();
        private string appKey = config.AppKey;
        private string appSecret = config.AppSecret;

        public SinaWeiboLogin(int platForm)
        {
            //switch (platForm)
            //{
            //    case 2:
            //        appKey = "2237426525";
            //        break;
            //}
        }
        /// <summary>
        /// step1：获取新浪微博code
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        //public string GetAuthCodeURL(string state)
        //{
        //    string api = "/oauth2/authorize";
        //    string url = string.Format("{0}?client_id={1}&redirect_uri={2}&response_type=code&state={3}"
        //        , config.BaseURL + api, config.AppKey, HttpUtility.UrlEncode(config.RedirectURL), state);

        //    return url;
        //}
        /// <summary>
        /// step2：获取新浪微博access_token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        //public string GetAccessToken(string code)
        //{
        //    string api = "/oauth2/access_token";
        //    string url = string.Format("{0}client_id={1}&client_secret={2}&grant_type=authorization_code&code={3}&redirect_uri={4}"
        //        , config.BaseURL + api, config.AppKey, config.AppSecret, code, HttpUtility.UrlEncode(config.RedirectURL));
        //    return HttpHelper.HttpGet(url);
        //}

        /// <summary>
        /// step3：获取新浪微博userinfo
        /// </summary>
        /// <param name="scstoken"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public string GetUserInfo(string token, string uid)
        {
            string api = "/2/users/show.json";
            string url = String.Format("{0}?access_token={1}&uid={2}&source={3}",
                config.BaseURL + api, token, uid, config.AppKey);
            return HttpHelper.HttpGet(url);
        }
    }
}
