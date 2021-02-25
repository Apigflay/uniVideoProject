using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ActiveModel
    {
        public int id { get; set; }
        public string name { get; set; }
        /// <summary>
        /// 0:结束，1：未开始，2：进行中
        /// </summary>
        public int state { get; set; }
        public string activeTime { get; set; }
        public string picture { get; set; }
        public string activeURL { get; set; }

        private string _stateName = "";

        public string stateName
        {
            get
            {
                return GetStateName();
            }
            set { _stateName = value; }
        }

        private string GetStateName()
        {
            switch (state)
            {
                case 0:
                    _stateName = "已结束";
                    break;
                case 1:
                    _stateName = "未开始";
                    break;
                case 2:
                    _stateName = "进行中";
                    break;
            }
            return _stateName;
        }
    }

    public class ActiveRoom
    {
        public int id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public string icon { get; set; }
        public string html { get; set; }
    }
}
