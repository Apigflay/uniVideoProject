using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.View
{
    /// <summary>
    /// 我的页面兑换ViewModel
    /// </summary>
    public class VMIncome
    {
        public int apiversion { get; set; }
        public int useridx { get; set; }
        public string chkCode { get; set; }
        public string token { get; set; }
        public GiftExchange MyWawa { get; set; }
        public List<GiftExchange> BabyRecordList { get; set; }
        public WithdrawRecord MyWithdrawInfo { get; set; }//我的提现信息
        public WalletModel MyWallet { get; set; }
    }
}
