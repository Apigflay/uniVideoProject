using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 版本有关
    /// </summary>
    public class LiveVersion
    {
        public int type { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string version { get; set; }
        public string updateInfo { get; set; }
        public string updateURL { get; set; }
        public DateTime updateTime { get; set; }
        public int isUpdate { get; set; }
        //2017-02-06 15:50:14 add
        public int auditVersion { get; set; }
        public string Bundleid { get; set; }
    }
    /// <summary>
    /// 喵播配置有关
    /// </summary>
    public class LiveConfig
    {
        public int id { get; set; }
        public string name { get; set; }
        public string data { get; set; }
        public string content { get; set; }
        public DateTime time { get; set; }
    }
    /// <summary>
    /// 游戏配置文件
    /// </summary>
    public class GameConfig
    {
        public GameConfig()
        {
            token = DateTime.Now.ToString();
            activeBigPic = "";
            activity = "";
            content = "";
        }
        public int gameid { get; set; }
        public string gameName { get; set; }
        public int isshow { get; set; }
        public int gameVersion { get; set; }
        public string androidName { get; set; }
        public string iosName { get; set; }
        public string activeName { get; set; }
        public string activePic { get; set; }
        public string androidurl { get; set; }
        public string iosurl { get; set; }
        public string token { get; set; }
        public string content { get; set; }
        public string activeBigPic { get; set; }
        public string activity { get; set; }//Android使用启动app时
        public int orientation { get; set; }//1:横屏，0：竖屏 默认 AddTime 2016-9-14
        public int openMode { get; set; }//打开方式1：1：喵播中打开，0：系统浏览器 默认 AddTime 2016-9-19
        public int advflag { get; set; }//是否广告轮播
    }

    public class GameRoomConfig
    {
        public int gameid { get; set; }
        public string imgURL { get; set; }
    }

    public class HotConfig
    {
        public string starttime { get; set; }
        public string endtime { get; set; }
    }

    /// <summary>
    /// Name:脸部贴纸
    /// AppVersion：2.0.0
    /// Time:2016-10-13
    /// Author:zhaorui
    /// </summary>
    public class FaceStickers
    {
        public int giftid { get; set; }
        //1:礼物动画，2:脸部贴纸动画资源
        public int faceType { get; set; }
        public string picture { get; set; }
        public string resName { get; set; }
        public int playTime { get; set; }//礼物动画播放时间以秒为单位
    }
}
