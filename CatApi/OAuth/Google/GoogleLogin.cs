using Common;
using System.Web;

namespace OAuth.Google
{
    public class GoogleLogin
    {
        private GoogleConfig config = new GoogleConfig();

        /// <summary>
        /// Step1:获取授权URL
        /// </summary>
        /// <returns></returns>
        public string GetAuthCodeURL()
        {
            string api = "";

            return "";
        }

        /// <summary>
        /// Step2 获取AccessToken
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetAccessToken(string code)
        {
            string url = string.Format("{0}?code={1}&client_id={2}&client_secret={3}&redirect_uri={4}&grant_type=authorization_code"
                , config.Access_TokenURL
                , code
                , HttpUtility.HtmlEncode(config.AppKey)
                , config.AppSecrect
                , HttpUtility.HtmlEncode(config.RedirectURL));

            string data = HttpHelper.HttpGet(url);
            return data;
        }

        /// <summary>
        /// Step3:获取用户信息(android)
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public string GetUserInfo2(string access_token)
        {
            string api = "/oauth2/v3/tokeninfo";
            string url = string.Format("{0}?id_token={1}", config.UserInfoURL + api, access_token);

            return HttpHelper.HttpGet(url);
        }

        /// <summary>
        /// Step3:获取用户信息(ios)
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public string GetUserInfo(string access_token)
        {
            string api = "/oauth2/v3/userinfo";
            string url = string.Format("{0}?access_token={1}", config.UserInfoURL + api, access_token);

            return HttpHelper.HttpGet(url);
        }
    }
}
