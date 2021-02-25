using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Param
{
    public class AlbumParam : CommonParam
    {
        public int opertype { get; set; }//1：删除，2：举报，3：分享
        public int albumid { get; set; }
    }
}
