using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.View
{
    public class VMAnchorInfo
    {
        public UserInfo User { get; set; }

        public AnchorInfo Anchor { get; set; }

        public AnchorTask Task { get; set; }

        /// <summary>
        /// 阶段等级
        /// </summary>
        public int StageLevel { get; set; }
        public double Progress { get; set; }
    }
}
