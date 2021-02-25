using System.Collections.Specialized;
using System.Configuration;

namespace OAuth.Weixin
{
    internal class WeixinAuthConfig : BaseOAuthConfig
    {
        private NameValueCollection WeixinSection = 
            (NameValueCollection)ConfigurationManager.GetSection("WeixinSectionGroup/WeixinSection");

        public WeixinAuthConfig()
        {
            _section = WeixinSection;
        }
    }
}
