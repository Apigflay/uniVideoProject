using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class BaiduModel
    {
        #region 公有属性
        public string apikey { get; set; }	        //string 	是 	应用的api key,用于标识应用。
        public int timestamp { get; set; }	        //int 	    是 	用户发起请求时的unix时间戳。本次请求签名的有效时间为该时间戳向后10分钟。
        public string sign { get; set; } 	        //string 	是 	调用参数签名值，与apikey成对出现。用于防止请求内容被篡改, 生成方法请参考。
        public uint? expires { get; set; }          //uint 	    否 	用户指定本次请求签名的失效时间。格式为unix时间戳形式，用于防止 replay 型攻击。为保证防止 replay攻击算法的正确有效，请保证客户端系统时间正确。
        public uint? device_type { get; set; }	    //uint  	否 	当一个应用同时支持多个设备平台类型（比如：Android和iOS），请务必设置该参数。其余情况可不设置。具体请参见：device_type参数使用说明，android:3,ios:4
        #endregion
    }

    public class Custom_content
    {
        public string roomid { get; set; }
        public string serverid { get; set; }
        public string useridx { get; set; }
        public Custom_content(string roomid, string serverid, string useridx)
        {
            this.roomid = roomid;
            this.serverid = serverid;
            this.useridx = useridx;
        }
    }

    //百度消息实体类
    public class BaiduMsgModel
    {
        public string title { get; set; }           //选填；通知标题，可以为空；如果为空则设为appid对应的应用名; 
        public string description { get; set; }     //必填；同志文本内容，不能为空；
        public string notification_builder_id { get; set; }    //选填；android客户端自定义通知样式，如果没有设置默认为0;
        public string notification_basic_style { get; set; }   //选填；只有notification_builder_id为0时有效，可以设置通知的基本样式包括(响铃：0x04;振动：0x02;可清除：0x01;),这是一个flag整形，每一位代表一种样式;
        public string open_type { get; set; }                  //选填： 点击通知后的行为(1：打开Url; 2：自定义行为；3：默认打开应用;);
        public string url { get; set; }             //选填；需要打开的Url地址，open_type为1时才有效; 
        public string pkg_content { get; set; }     //选填；open_type为2时才有效，Android端SDK会把pkg_content字符串转换成Android Intent,通过该Intent打开对应app组件，所以pkg_content字符串格式必须遵循Intent uri格式，最简单的方法可以通过Intent方法toURI()获取
        //public string custom_content { get; set; }  //选填：自定义内容，键值对，Json对象形式(可选)；在android客户端，这些键值对将以Intent中的extra进行传递。
        public Custom_content custom_content { get; set; }

        public BaiduMsgModel(string description)
        {
            this.description = description;
        }

        public BaiduMsgModel(string title, string description)
        {
            this.title = title;
            this.description = description;
            //this.notification_builder_id = "0";
            //this.notification_basic_style = "7";
            //this.open_type = "0";
            //this.url = "";
            //this.pkg_content = "";
            //this.custom_content = "";
        }

        public BaiduMsgModel(string title, string description, string opentype)
        {
            this.title = title;
            this.description = description;
            this.open_type = open_type;
        }
    }

    public class BaiduNotice : BaiduModel
    {
        #region 属性
        public string channel_id { get; set; }  //string 	是 	必须为端上初始化channel成功之后返回的channel_id 	唯一对应一台设备
        public uint msg_type { get; set; }      //number 	否 	取值如下：0：消息；1：通知。默认为0 	消息类型
        public string msg { get; set; }         //string 	是 	详情见消息/通知数据格式 	消息内容，json格式
        public uint msg_expires { get; set; }   //number 	否 	0~604800(86400*7)，默认为5小时(18000秒) 	相对于当前时间的消息过期时间，单位为秒
        public uint deploy_status { get; set; } //number 	否 	取值为：1：开发状态；2：生产状态； 若不指定，则默认设置为生产状态。 	设置iOS应用的部署状态，仅iOS应用推送时使用
        #endregion

        #region 构造函数
        public BaiduNotice(string apikey, string channel_id, string msg, int timestamp)
        {
            this.apikey = apikey;
            this.channel_id = channel_id;
            this.msg = msg;
            this.timestamp = timestamp;     //默认使用当前时间戳
            this.msg_type = 0;              //消息
            this.msg_expires = 60;          //7天过期,604800改了60s
            this.device_type = 3;           //安卓
            this.deploy_status = 2;         //生产状态
        }

        public BaiduNotice(string apikey, string channel_id, string msg, int timestamp, uint msg_type)
        {
            this.apikey = apikey;
            this.channel_id = channel_id;
            this.msg = msg;
            this.timestamp = timestamp;     //默认使用当前时间戳
            this.msg_type = msg_type;       //消息类型
            this.msg_expires = 60;      //7天过期,604800改了60s
            this.device_type = 3;           //安卓
            this.deploy_status = 2;         //生产状态
        }
        #endregion

        /*
        public BaiduNotice(string description)
        {
            this.description = description;
        }

        public BaiduNotice(string title, string description)
        {
            this.description = description;
            this.title = title;
        }
         * */
    }

    public class LiveInfo
    {
        public int U;
        public string N = "";
        public string P = "";
        public string Area = "";
    } 
}
