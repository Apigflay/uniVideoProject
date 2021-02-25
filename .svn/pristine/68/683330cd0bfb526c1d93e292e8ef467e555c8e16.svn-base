using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Model
{
    [Serializable]
    public class UserInfo
    {
        /// <summary>
        /// 真实idx
        /// </summary>
        public int realuidx { get; set; }
        /// <summary>
        /// 如果用户是短号就要下发短号
        /// </summary>
        public int useridx { get; set; }

        public string userId { get; set; }

        public int gender { get; set; }

        public string myname { get; set; }

        public string signatures { get; set; }

        public string smallpic { get; set; }

        public string bigpic { get; set; }

        /// <summary>
        /// 用户VIP等级
        /// </summary>
        public int level { get; set; }

        public int grade { get; set; }

        public int curexp { get; set; }
        /// <summary>
        /// 参考Enum枚举(客户端没用)
        /// </summary>
        [JsonIgnore]
        public int logintype { get; set; }
    }


    /// <summary>
    /// 我的页面信息
    /// </summary>
    public class MyUserInfo : UserInfo
    {
        public int starlevel { get; set; }
        /// <summary>
        /// 喵币
        /// </summary>
        public Int64 owncash { get; set; }

        /// <summary>
        /// 关注数
        /// </summary>
        public int friendnum { get; set; }

        /// <summary>
        /// 粉丝数
        /// </summary>
        public int fansnum { get; set; }

        /// <summary>
        /// 是否操作员所关注的
        /// </summary>
        public int hufan { get; set; }
        /// <summary>
        /// 送出礼物总喵币
        /// </summary>
        public Int64 sendgift { get; set; }
        /// <summary>
        /// 收到喵粮
        /// </summary>
        public Int64 catfood { get; set; }

    }
    /// <summary>
    /// 用户其他信息
    /// </summary>
    public class OtherInfo
    {
        public OtherInfo()
        {
            nation = "";
            nationFlag = "";
        }
        /// <summary>
        /// 喵币
        /// </summary>
        public Int64 owncash { get; set; }

        /// <summary>
        /// 关注数
        /// </summary>
        public int friendnum { get; set; }

        /// <summary>
        /// 粉丝数
        /// </summary>
        public int fansnum { get; set; }

        /// <summary>
        /// 是否操作员所关注的
        /// </summary>
        public int hufan { get; set; }

        /// <summary>
        /// 送出礼物总喵币
        /// </summary>
        public Int64 sendgift { get; set; }

        /// <summary>
        /// 收到喵粮
        /// </summary>
        public Int64 catfood { get; set; }

        /// <summary>
        /// 是否关注
        /// </summary>
        public int isFollow { get; set; }

        /// <summary>
        /// vip还剩多少天到期 addtime 2016-9-13
        /// </summary>
        public int vipExpirationDate { get; set; }
        /// <summary>
        /// 是否签约主播,国籍，国旗 add 2016-10-28
        /// </summary>
        public string nation { get; set; }

        public string nationFlag { get; set; }

        public int isSign { get; set; }
        public int starlevel { get; set; }

        /// <summary>
        /// 主播等级 add 2017年6月28日17:15:02
        /// </summary>
        public int anchorLevel { get; set; }
        /// <summary>
        /// 测试字段是否认证2017-09-12 09:57:16
        /// </summary>
        [JsonIgnore]
        public int isV { get; set; }
        /// <summary>
        /// 用户所在区域ID
        /// </summary>
        public int areaid { get; set; }
    }
}
