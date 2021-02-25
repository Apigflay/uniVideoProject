using System.Collections.Specialized;
using System.Configuration;

namespace OAuth.QQ
{
    internal class QQOAuthConfig : BaseOAuthConfig
    {
        private NameValueCollection QQSection = (NameValueCollection)ConfigurationManager.GetSection("QQSectionGroup/QQSection");

        public QQOAuthConfig()
        {
            _section = QQSection;
        }

        //public const string AuthCodeURL = "https://graph.qq.com/oauth2.0/authorize";
        //public const string AccessTokenURL = "https://graph.qq.com/oauth2.0/token";
        //public const string OpenIDURL = "https://graph.qq.com/oauth2.0/me";
        //public const string UserInfoURL = "https://graph.qq.com/user/get_user_info";
    }
}
