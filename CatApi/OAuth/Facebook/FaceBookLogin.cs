using Common;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Web;

namespace OAuth.Facebook
{
    public class FaceBookLogin
    {
        private FaceBookConfig config = new FaceBookConfig();

        /// <summary>
        /// Step:1 获取授权URL
        /// </summary>
        /// <returns></returns>
        public string GetAuthCodeURL()
        {
            string api = "https://www.facebook.com/dialog/oauth";
            string url = string.Format("{0}?client_id={1}&redirect_uri={2}&code=acgon",
                api, config.AppKey, config.RedirectURL);

            return url;
        }

        /// <summary>
        /// Step:2 获取acces_token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetAccessToken(string code)
        {
            string url = string.Format("{0}?client_id={1}&client_secret={3}&redirect_uri={2}&code={4}",
                config.AcessTokenURL, config.AppKey, config.AppSecrect, HttpUtility.UrlEncode(config.RedirectURL), code);

            string result = HttpHelper.HttpGet(url);

            NameValueCollection nvc = OAuth.QueryString.GetQueryList(result);

            return nvc["access_token"];
        }


        /// <summary>
        /// Step:3 获取用户信息
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public string GetUserInfo(string access_token)
        {
            string url = string.Format("{0}?access_token={1}&scope=email&fields=id,name,email,link,gender",
                config.UserInfoURL, access_token);

            string data = "";
            try
            {
                data = HttpHelper.HttpGet(url);
            }
            catch (System.Exception ex)
            {
                LogHelper.WriteLog(LogFile.Test, "UserInfo:" + data + ",ex:" + ex.Message + ",d:" + ex.Source);
            }
            return data;
        }
    }
}
