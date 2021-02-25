using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;

namespace OAuth.SinaWeibo
{
    internal class SinaWeiboAuthConfig : BaseOAuthConfig
    {
        private NameValueCollection WeiboSection = 
            (NameValueCollection)ConfigurationManager.GetSection("SinaWeiboSectionGroup/SinaWeiboSection");

        public SinaWeiboAuthConfig()
        {
            _section = WeiboSection;
        }
    }
}
