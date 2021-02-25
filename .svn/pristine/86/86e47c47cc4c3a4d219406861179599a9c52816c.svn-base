using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class AcceptTime
    {
        public List<TimeInterval> start { get; set; }
        public List<TimeInterval> end { get; set; }
    }
    public class TimeInterval
    {
        public int hour { get; set; }
        public int min { get; set; }
    }

    public class XgMessage
    {
        //public uint? expire_time { get; set; }
        //public uint? loop_times { get; set; }
        //public uint? loop_interval { get; set; }
        //public string send_time { get; set; }
        //public List<AcceptTime> accept_time { get; set; }
        public int builder_id { get; set; }
        //public uint message_type { get; set; }
        //public IDictionary<string, string> custom_content { get; set; }
        public int icon_type { get; set; }
        public string icon_res { get; set; }
        public string small_icon { get; set; }
        public string custom_content { get; set; } //Custom_content
        
        protected XgMessage()//uint message_type
        {
            //this.message_type = message_type;
            
            //this.send_time = "2013-12-20 18:31:00";

            //accept_time = new List<AcceptTime>();

            //custom_content = new Dictionary<string, object>();
        }
    }

    public class Msg_Android : XgMessage
    {
        public string title { get; set; }
        public string content { get; set; }
        public string action { get; set; }

        public Msg_Android(string title)
            : base()//message_type
        {
            this.title = "喵播";
            this.content = title;
            this.builder_id = 0;
            this.icon_type = 0;
            this.icon_res = "icon";
            this.small_icon = "logo";
        }
    }
}
