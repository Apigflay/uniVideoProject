
namespace OAuth.QQ
{
    /*{
    "ret": 0,
    "msg": "",
    "is_lost":0,
    "nickname": "蛋炒饭",
    "gender": "女",
    "province": "浙江",
    "city": "杭州",
    "year": "2001",
    "figureurl": "http:\/\/qzapp.qlogo.cn\/qzapp\/204515\/4F445BCFFFB2F9D19B90616E70EEA7F6\/30",
    "figureurl_1": "http:\/\/qzapp.qlogo.cn\/qzapp\/204515\/4F445BCFFFB2F9D19B90616E70EEA7F6\/50",
    "figureurl_2": "http:\/\/qzapp.qlogo.cn\/qzapp\/204515\/4F445BCFFFB2F9D19B90616E70EEA7F6\/100",
    "figureurl_qq_1": "http:\/\/q.qlogo.cn\/qqapp\/204515\/4F445BCFFFB2F9D19B90616E70EEA7F6\/40",
    "figureurl_qq_2": "http:\/\/q.qlogo.cn\/qqapp\/204515\/4F445BCFFFB2F9D19B90616E70EEA7F6\/100",
    "is_yellow_vip": "0",
    "vip": "0",
    "yellow_vip_level": "0",
    "level": "0",
    "is_yellow_year_vip": "0"
    }*/
    public class QQUser : BaseUser
    {
        /// <summary>
        /// 0：正确返回用户信息
        /// </summary>
        public int ret { get; set; }
        /// <summary>
        /// 如果 ret < 0 ，会有相应的错误信息提示
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 用户在qq空间的昵称
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 性别  如果获取不到则默认返回"男"// 男,女
        /// </summary>
        public string gender { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        /// <summary>
        /// 标识用户是否为黄钻用户（0：不是；1：是）
        /// </summary>
        public string vip { get; set; }
        /// <summary>
        /// 黄钻等级
        /// </summary>
        public string yellow_vip_level { get; set; }
        /// <summary>
        /// 黄钻等级
        /// </summary>
        public string level { get; set; }
        /// <summary>
        /// 大小为30 * 30 像素的qq空间头像URL
        /// </summary>
        public string figureurl { get; set; }
        /// <summary>
        /// 大小为50 * 50 像素的qq空间头像URL
        /// </summary>
        public string figureurl_1 { get; set; }
        /// <summary>
        /// 大小为100×100像素的QQ空间头像URL
        /// </summary>
        public string figureurl_2 { get; set; }
        /// <summary>
        /// 大小为40×40像素的QQ头像URL
        /// </summary>
        public string figureurl_qq_1 { get; set; }
        /// <summary>
        /// 大小为100×100像素的QQ头像URL。需要注意，不是所有的用户都拥有QQ的100x100的头像，但40x40像素则是一定会有。
        /// </summary>
        public string figureurl_qq_2 { get; set; }
    }
    public class QQShortUser
    {
        public string client_id { get; set; }
        public string Openid { get; set; }
        public string unionid { get; set; }
    }
}
