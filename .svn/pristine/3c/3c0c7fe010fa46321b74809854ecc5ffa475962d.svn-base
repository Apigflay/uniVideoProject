using System.Collections.Specialized;

namespace OAuth
{
    internal abstract class BaseOAuthConfig
    {
        protected NameValueCollection _section;

        //public string AppId
        //{
        //    get { return _section["AppId"]; }
        //}

        /// <summary>
        /// 新浪微博使用AppKey而不是AppId
        /// </summary>
        public string AppKey
        {
            get { return _section["AppKey"]; }
        }

        public string AppSecret
        {
            get { return _section["AppSecret"]; }
        }

        /// <summary>
        /// 授权地址
        /// </summary>
        public string BaseURL
        {
            get { return _section["BaseURL"]; }
        }
        /// <summary>
        /// 成功登陆后的跳转地址
        /// </summary>
        public string RedirectURL
        {
            get { return _section["RedirectURL"]; }
        }
    }
}
