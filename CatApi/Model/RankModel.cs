using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class RankModel
    {
    }

    /// <summary>
    /// 房间内，周榜总榜排行榜使用
    /// </summary>
    public class SaleRankInfo
    {
        public SaleRankInfo()
        {
            myname = "匿名用户";
            smallpic = "http://liveimg.9158.com/default.png";
        }

        public long pos { get; set; }
        public Int64 sumprice { get; set; }
        public int rankChange { get; set; }
        
        public int isFollow { get; set; }
        public int useridx { get; set; }
        public string myname { get; set; }
        public string smallpic { get; set; }
        public int grade { get; set; }
        public int level { get; set; }
        public int gender { get; set; }
        public int isOnline { get; set; }
    }
    public class BetRankInfo
    {
        public BetRankInfo()
        {
            myname = "匿名用户";
            smallpic = "http://liveimg.9158.com/default.png";
        }

        public long row { get; set; }
        public Int64 sumprice { get; set; }
        public int rankChange { get; set; }

        //public int isFollow { get; set; }
        public int useridx { get; set; }
        public string myname { get; set; }
        public string smallpic { get; set; }
        //public int grade { get; set; }
        //public int level { get; set; }
        //public int gender { get; set; }
        //public int isOnline { get; set; }
    }
    /// <summary>
    /// 粉丝奉献榜，我的守护排行榜使用
    /// </summary>
    public class RankInfo : UserInfo
    {
        public RankInfo()
        {
            myname = "匿名用户";
            smallpic = "http://liveimg.9158.com/default.png";
            level = 1;
        }

        public Int64 rowNo { get; set; }
        public Int64 total { get; set; }//分享总数，观看时长，奉献
        public int rankChange { get; set; }//1：上升，0：下降
    }

    /// <summary>
    /// 我当前的等级在当前主播里面所排名
    /// </summary>
    public class MyRankInfo
    {
        public int count { get; set; }//总数
        public int myRank { get; set; }//我的排名
        public int myTotal { get; set; }//我奉献，分享，观看值
        public string myPhoto { get; set; }
    }

    /// <summary>
    /// 守护购买商品列表
    /// </summary>
    public class GuardGoods
    {
        public int goodsId { get; set; }
        public int dayNumber { get; set; }
        public Int64 goodsPrice { get; set; }
        public string goodsPriceView { get; set; }
    }

   
    public class Rank_v2 {
        /// <summary>
        /// 位序
        /// </summary>
        public int row { get; set; }
        /// <summary>
        /// 用户idx
        /// </summary>
        public int useridx { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public decimal rankNum { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string myName { get; set;  }
        /// <summary>
        /// 头像
        /// </summary>
        public string smallpic { get; set; }
        /// <summary>
        /// 升降序（0 无变化 2上升 1下降）
        /// </summary>
        public int sort { get; set; }
    }

    
}
