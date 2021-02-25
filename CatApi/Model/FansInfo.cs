using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class FansInfo
    {

    }

    /// <summary>
    /// 粉丝列表数据
    /// </summary>
    public class MyFansInfo
    {
        public Int64 rowId { get; set; }
        public int useridx { get; set; }
        public int grade { get; set; }
        public int level { get; set; }
        public string myname { get; set; }
        public int gender { get; set; }
        public string signatures { get; set; }
        public string smallpic { get; set; }
        public int hufan { get; set; }
    }
}
