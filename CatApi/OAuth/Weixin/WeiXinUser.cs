
namespace OAuth.Weixin
{
    /*{
        "openid": "olPUPwLuwwVAfkvTrjqrMfhI5Mg8",
        "nickname": "爱你哟💋么么哒",
        "sex": 1,
        "language": "zh_CN",
        "city": "杭州",
        "province": "浙江",
        "country": "中国",
        "headimgurl": "http:\/\/wx.qlogo.cn\/mmopen\/ajNVdqHZLLDnAxyemlojiciaE3hAttPibicVj8kHZlwicEvtJzhgZiarfJQGP1DqEAIAez7UxsWpvSlsjVoxaJSKEFUASNsJqYKzzMicCKqXvh2CiaA\/0",
        "privilege": [],
        "unionid": "og7NiuJciP3MVY2loKHptAFkEEnI"
     }*/
    public class WeiXinUser : BaseUser
    {
        /// <summary>
        /// 普通用户的标识，对当前开发者账号唯一
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 用户统一标识。针对一个微信开放平台账号下的应用，同一用户unionid是唯一的。
        /// </summary>
        public string unionid { get; set; }
        public string nickname { get; set; }
        /// <summary>
        /// 普通用户性别，1：男性，2：女性
        /// </summary>
        public int sex { get; set; }
        /// <summary>
        /// 国家，如中国为CN
        /// </summary>
        public string country { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        /// <summary>
        /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空
        /// </summary>
        public string headimgurl { get; set; }
        public string[] privilege { get; set; }
    }

    /// <summary>
    /// 通过code获取access_token
    /// </summary>
    public class WeixinToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string openid { get; set; }
        public string scope { get; set; }
    }
}
