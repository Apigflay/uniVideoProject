using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ADspotsModel
    {
        // public int id { get; set; }
        public string title { get; set; }
        public string imageUrl { get; set; }
        public string link { get; set; }
        public int roomid { get; set; }
        public DateTime addTime { get; set; }
        public int state { get; set; }
        public int orderid { get; set; }
    }

    public class ADspotsRoom : ADspotsModel
    {
        public int lrCurrent { get; set; }
        public int serverid { get; set; }
        public string myname { get; set; }
        public string signatures { get; set; }

        public string smallpic { get; set; }
        public string bigpic { get; set; }
        public string gps { get; set; }
        public int useridx { get; set; }
        public string flv { get; set; }
        public int hiddenVer { get; set; }
        public int cutTime { get; set; }
        public string adsmallpic { get; set; }
        public string contents { get; set; }
        public int type { get; set; }
    }
    public class ADsportFull
    {
        public string title { get; set; }
        public string imageUrl { get; set; }
        public string link { get; set; }
        public int cutTime { get; set; }
        public int serverid { get; set; }
        public string contents { get; set; }
        public int roomid { get; set; }
        public int showmodel { get; set; }
        public int type { get; set; }
    }
    /// <summary>
    /// 广告表
    /// </summary>
    //public class ADspotsBase
    //{
    //    public int id { get; set; }
    //    public string title { get; set; }
    //    public string imageUrl { get; set; }
    //    public string link { get; set; }
    //    public int roomid { get; set; }
    //    public DateTime addTime { get; set; }
    //    public int state { get; set; }
    //    public int orderid { get; set; }
    //}
    /// <summary>
    /// 热门列表广告位
    /// Add 2016-10-12
    /// ver 2.0.0
    /// </summary>
    public class HotAds
    {
        public HotAds()
        {
            adType = "推广";
            adTitle = "";
            adContent = "";
            adLink = "";
        }
        public int id { get; set; }
        //public int putDevice { get; set; }//投放设备0：全部 ，1：android，2：ios
        public int position { get; set; }
        public string adType { get; set; }
        public string adTitle { get; set; }
        public string adContent { get; set; }
        public string adLink { get; set; }
        public int useridx { get; set; }
        public int roomid { get; set; }
        public int gameid { get; set; }
        public int serverid { get; set; }
        public int loopStatus { get; set; }//1:循环 0：不循环（default）
        public string smallPic { get; set; }
        public string bigPic { get; set; }
    }
}
