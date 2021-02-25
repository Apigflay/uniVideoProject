using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.View
{
    public class VMAppConfig
    {
        //public List<SelectListItem> item1 { get; set; }

        public string barrage { get; set; }
        public string paySwitch { get; set; }
        public string totalBarrage { get; set; }
        public string hbPrice { get; set; }

        public Model.LiveConfig config { get; set; }
        public List<Model.LiveConfig> configs { get; set; }

    }
}
