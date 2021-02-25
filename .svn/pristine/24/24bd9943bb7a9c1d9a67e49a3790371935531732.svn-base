using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;

namespace Model
{
    /// <summary>
    /// 直播房间信息表
    /// </summary>
    public class Room
    {
        public int roomid { get; set; }

        public int useridx { get; set; }

        public string flv { get; set; }

        public int serverid { get; set; }

        public string position { get; set; }
        /// <summary>
        /// 连麦状态
        /// </summary>
        public int lianMaiStatus { get; set; }
        /// <summary>
        /// 2017-02-08 add  %10  1:android 0:ios
        /// </summary>
        public int phonetype { get; set; }
        /// <summary>
        /// 2017-02-21 add 是否在播
        /// </summary>
        public int isOnline { get; set; }
        /// <summary>
        /// 2017-08-11 add 喵播PC客户端使用
        /// </summary>
        public string familyName { get; set; }
    }

    /// <summary>
    /// 房间信息表
    /// </summary>
    public class RoomOnline : Room
    {
        public RoomOnline()
        {
            photo = "http://liveimg.9158.com/smldefault.png";
        }
        public string nickname { get; set; }
        public string photo { get; set; }
        public int sex { get; set; }
        public int starlevel { get; set; }
        public int allnum { get; set; }
        public int @new { get; set; }
        public int anchorLevel { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class RoomOnline_V1 //: UserInfo
    {
        public RoomOnline_V1()
        {
            nation = "";
            nationFlag = "";
            familyName = "";
        }
        public Int64 pos { get; set; }

        #region userinfo

        public int useridx { get; set; }
        public string userId { get; set; }
        public int gender { get; set; }
        public string myname { get; set; }
        public string smallpic { get; set; }
        public string bigpic { get; set; }

        #endregion

        public int allnum { get; set; }

        public int roomid { get; set; }

        public int serverid { get; set; }

        public string gps { get; set; }

        public string flv { get; set; }

        #region 主播信息

        public int anchorlevel { get; set; }

        public int starlevel { get; set; }

        public string familyName { get; set; }

        public int isSign { get; set; }

        #region 海外信息

        public string nation { get; set; }

        public string nationFlag { get; set; }

        #endregion



        #endregion

        /// <summary>
        /// add 2016-12-28 附近的人使用
        /// </summary>
        public double distance { get; set; }

        public int gameid { get; set; }
        public string gameName { get; set; }

        /// <summary>
        /// 1：人气推荐
        /// </summary>
        [JsonIgnore]
        public int nType { get; set; }
        /// <summary>
        /// 密码房0-普通 1-密码房
        /// </summary>
        public int islock { get; set; }
    }

    /// <summary>
    /// 在线房间信息表
    /// </summary>
    public class RoomOnlineUser
    {
        public int roomid { get; set; }
        public int useridx { get; set; }
        public string nickname { get; set; }
        public string rtmp { get; set; }
        public string flv { get; set; }
        public string m3u8 { get; set; }
    }

    /// <summary>
    /// 随机推荐主播
    /// </summary>
    public class RandomRoomOnline : RoomOnline
    {
        public double longitude { get; set; }

        public double latitude { get; set; }

        public double distance { get; set; }

    }

    /// <summary>
    /// 我的直播间使用
    /// </summary>
    public class MyLiveRoom
    {
        public MyLiveRoom()
        {
            serverId = -1;
            gender = "0";
        }
        public int roomid { get; set; }
        public int useridx { get; set; }
        public int lead { get; set; }
        public string nickname { get; set; }
        public int serverId { get; set; }
        public string photo { get; set; }
        public int level { get; set; }
        public string gender { get; set; }
    }

    public class HotTab
    {
        public HotTab()
        {
            //tabEnglishName = "";
        }
        public int parentid { get; set; }
        public int tabid { get; set; }
        public string tabName { get; set; }
        [JsonIgnore]
        public string tabEnglishName { get; set; }
        public string tabIcon { get; set; }
        public string tabImg { get; set; }
        /// <summary>
        /// 喵播新版本标签图片 2017-12-20
        /// </summary>
        public string tabImgNew { get; set; }
    }
}
