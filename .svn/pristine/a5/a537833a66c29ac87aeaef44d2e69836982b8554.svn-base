using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 礼物分类
    /// </summary>
    public class GiftTab
    {
        public int tabid { get; set; }
        public string tabName { get; set; }
        public string tabIcon { get; set; }
        //public List<GiftModel> giftlist { get; set; }
    }
    /// <summary>
    /// 礼物列表模型类
    /// </summary>
    public class GiftModel
    {
        public GiftModel()
        {
            icon = " ";
            unit = "个";
            hoticon = " ";
            iconCartoon = "";
        }
        public int tabid { get; set; }
        public int giftId { get; set; }
        /// <summary>
        /// 礼物名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 礼物类型
        /// </summary>
        public int giftType { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public int price { get; set; }

        /// <summary>
        /// 礼物顺序
        /// </summary>
        public int giftOrder { get; set; }

        /// <summary>
        /// 海外版礼物排序
        /// </summary>
        [JsonIgnore]
        public int sortid { get; set; }

        /// <summary>
        /// 礼物说明
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string unit { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 角标地址
        /// </summary>
        public string hoticon { get; set; }
        /// <summary>
        /// 礼物webp动画
        /// </summary>
        public string iconCartoon { get; set; }

        [JsonIgnore]
        public string iconTemp { get; set; }
        /// <summary>
        /// 是否游戏打赏礼物
        /// </summary>
        public int isReward { get; set; }
    }

    public class GiftExchange
    {
        public int nums { get; set; }
        public int useridx { get; set; }
        //public string paytype { get; set; }
        //public string payuid { get; set; }
        //public string payname { get; set; }
        //public int phone { get; set; }
        public int wawaNum { get; set; }
        //public string title { get; set; }
        public int lastweeknum { get; set; }
        public int nowweeknum { get; set; }
        public int isSignAnchor { get; set; }
        public int isSanhu { get; set; }
        public DateTime createTime { get; set; }
    }
    public class BabyGiftInfo
    {
        public int useridx { get; set; }
        /// <summary>
        /// 从2017-5-22日起baby总数
        /// </summary>
        public int totalNum { get; set; }
        /// <summary>
        /// 已提现baby数
        /// </summary>
        public int alreadyNum { get; set; }
        /// <summary>
        /// 剩余可提现baby数
        /// </summary>
        public int babyNum { get; set; }
    }
    public class WeekStarGift : GiftModel
    {
        public Int64 rowNo { get; set; }
        public int useridx { get; set; }
        public string smallpic { get; set; }
        public Int64 totalNum { get; set; }
        public Int64 totalPrice { get; set; }
        public int gender { get; set; }
        public string myname { get; set; }
        public int level { get; set; }
        public int grade { get; set; }
        public int isOnline { get; set; }
    }
}
