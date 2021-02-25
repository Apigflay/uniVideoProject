using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 直播分享信息
    /// </summary>
    public class ShareModel
    {
        /// <summary>
        /// 房间Id
        /// </summary>
        public int roomid { get; set; }
        /// <summary>
        /// 用户Idx
        /// </summary>
        public int useridx { get; set; }
        /// <summary>
        /// 主播昵称
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 主播头像
        /// </summary>
        public string photo { get; set; }
        /// <summary>
        /// 直播流flv地址
        /// </summary>
        public string flv { get; set; }
        /// <summary>
        /// 直播流m3u8地址
        /// </summary>
        public string m3u8 { get; set; }
    }
}
