using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class InviteInfo
    {
        public int useridx { get; set; }
        /// <summary>
        /// 今日已经邀请多少人
        /// </summary>
        public int invitedNum { get; set; }
        /// <summary>
        /// 今日已经奖励多少
        /// </summary>
        public decimal rewardCash { get; set; }
        /// <summary>
        /// 我的余额
        /// </summary>
        public decimal myWallet { get; set; }
        /// <summary>
        /// 今日剩余多少次奖励
        /// </summary>
        public int remainNum { get; set; }
        /// <summary>
        /// 今日限额多少个
        /// </summary>
        public int limitNum { get; set; }
    }
}
