using System;

namespace Model
{
    /// <summary>
    /// 我的钱包
    /// </summary>
    public class WalletModel
    {
        public int useridx { get; set; }
        public decimal myWallet { get; set; }
        public string aliPay { get; set; }
        public string aliPayName { get; set; }
        public int isBindAlipay { get; set; }
        /// <summary>
        /// 审核状态 1：success ,0：审核中
        /// </summary>
        public int auditStatus { get; set; }
    }

    /// <summary>
    /// 红包奖励
    /// </summary>
    public class RedPacketReward
    {
        public Int64 rowNo { get; set; }
        public int useridx { get; set; }
        public string nickName { get; set; }
        /// <summary>
        /// 红包类型 1：邀新红包 2：时长红包 3：任务红包
        /// </summary>
        public int redType { get; set; }
        public int rewardType { get; set; }
        public decimal rewardCash { get; set; }
        public DateTime receiveTime { get; set; }
        public string remark { get; set; }
        public int totalCount { get; set; }
    }

    public class WithdrawMoney
    {
        public int useridx { get; set; }
        public int totalCount { get; set; }
        public Int64 rowNo { get; set; }
        public decimal amount { get; set; }
        public DateTime succTime { get; set; }
    }

    /// <summary>
    /// 提现记录
    /// </summary>
    public class WithdrawRecord
    {
        public int useridx { get; set; }
        public decimal amount { get; set; }
        public string alipayID { get; set; }
        public string mobilePhone { get; set; }
        public string realName { get; set; }
    }
}
