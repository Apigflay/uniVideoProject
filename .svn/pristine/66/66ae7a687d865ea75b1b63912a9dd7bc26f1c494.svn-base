using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 充值显示界面使用
    /// </summary>
    public class PayView
    {
        public int id { get; set; }
        public int platform { get; set; }
        public string channelStr { get; set; }
        public Int64 cash { get; set; }
        public string cashView { get; set; }
        public Int64 virtualCash { get; set; }
        public string virtualCashView { get; set; }
        public string contents { get; set; }

    }
    /// <summary>
    /// IOS支付
    /// </summary>
    [Serializable]
    public class IOSPay
    {
        public int id { get; set; }
        public int userIdx { get; set; }
        public string productId { get; set; }
        public string pKey { get; set; }
        public int price { get; set; }
        public int amount { get; set; }
        public string content { get; set; }
        public int step{ get; set; }
        public DateTime idate { get; set; }

    }
    public class ZFBpay
    {
        public int id { get; set; }
        public int useridx { get; set; }
        public int amount { get; set; }
        public string content { get; set; }
        public int step { get; set; }
    }

    public class PayBase
    {
        public int useridx { get; set; }
        public int amount { get; set; }
        public DateTime idate { get; set; }
        public string paytype { get; set; }
        public string stardate { get; set; }
        public string enddate { get; set; }
         
    }

    public class PayRoomScore
    {
        public int roomid { get; set; }
        public int useridx { get; set; }
        public int lead { get; set; }
        public int status { get; set; }
        public int roomcash { get; set; }
        public int roomscore { get; set; }
        public int oldroomscore { get; set; }
        public int oldcash { get; set; }
        public int lastcash { get; set; }
        public int addcash { get; set; }
    }

    public class PayMenu
    {
        public long moneys { get; set; }
        public long coin { get; set; }
        public string contents { get; set; } 
    }
}
