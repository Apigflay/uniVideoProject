using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Param
{
    public class ZhimaAuthParam
    {
        public int apiversion { get; set; }
        public int useridx { get; set; }
        public string  name { get; set; }
        public string certno { get; set; }


        #region V1 Param

        public string param { get; set; }
        public string sign { get; set; }

        #endregion

        #region V2 Param

        public string bizNo { get; set; }

        #endregion

        #region V3 Param

        public string returnURL { get; set; }

        #endregion

        public string token { get; set; }
        public string deviceId { get; set; }
        public string deviceType { get; set; }
        public string version { get; set; }

    }
}
