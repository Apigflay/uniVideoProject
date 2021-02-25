using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.View
{
    public class VMAnswer
    {
        public Login_UserInfo Userinfo { get; set; }
        public WalletModel MyWallet { get; set; }
        /// <summary>
        /// 剩余答题次数
        /// </summary>
        public int remainNum { get; set; }
        /// <summary>
        /// 成功邀请人数
        /// </summary>
        public int invitedNum { get; set; }
        public int isAnchor { get; set; }

    }
}
