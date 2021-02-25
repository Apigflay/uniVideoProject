using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Model
{
    public class AnchorInfo
    {
        public int useridx { get; set; }
        public int isSign { get; set; }
        public int anchorLevel { get; set; }
        public int curExp { get; set; }
        public int beginExp { get; set; }
        public int nextExp { get; set; }
        public int upRequire { get; set; }
        /// <summary>
        /// 还差多少经验值 nextExp - (curExp - upRequire)
        /// </summary>
        public double shortExp { get; set; }
        /// <summary>
        /// 直播时长
        /// </summary>
        public double livingTime { get; set; }
    }

    public class AnchorTask
    {
        public int useridx { get; set; }
        public int onlineExp { get; set; }
        public int babyWeekExp { get; set; }
        public int catfoodExp { get; set; }
        public int weekStarExp { get; set; }

        public List<AnchorTaskLimit> taskLimit { get; set; }
    }
    /// <summary>
    /// 当前任务最大限制
    /// </summary>
    public class AnchorTaskLimit
    {
        public int id { get; set; }
        public int limit { get; set; }
        public string introduce { get; set; }
    }
    /// <summary>
    /// 家族信息
    /// </summary>
    public class Family
    {
        public Family()
        {
            qq = "";
            weixin = "";
            mobilePhone = "";
        }
        public int useridx { get; set; }
        public int roomid { get; set; }
        public string familyName { get; set; }
        public int gender { get; set; }
        public string nickName { get; set; }
        public int familyNum { get; set; }
        public string qq { get; set; }
        public string weixin { get; set; }
        public string mobilePhone { get; set; }
        public string notice { get; set; }
    }

    public class Album
    {
        public Int64 rowNo { get; set; }
        public int useridx { get; set; }
        public int albumid { get; set; }
        /// <summary>
        /// 0普通照片 1阅后及焚
        /// </summary>
        public int albumType { get; set; }
        public int buyNumber { get; set; }
        public int isBought { get; set; }
        public string imgURL { get; set; }

       // [JsonIgnore]
       /// <summary>
       /// 排序
       /// </summary>
        public int groupid { get; set; }

        /// <summary>
        /// 此列不需要序列化
        /// </summary>
        [JsonIgnore]
        public string phyPath { get; set; }
    }
    public class AlbumNew
    {

        public int albumid { get; set; }
        /// <summary>
        /// 0普通照片 1阅后及焚
        /// </summary>
        public int albumType { get; set; }

        public string imgURL { get; set; }


        public int groupid { get; set; }

        /// <summary>
        /// 是否被阅后  1表示 已经看过 0没有看过
        /// </summary>
        public string ret { get; set; }


    }

    public class UserInvitationInfo
    {


        public int userIdx { get; set; }
        public string userId { get; set; }
        public string phoneNumber { get; set; }
        public string userName { get; set; }
        public string myName { get; set; }
        public int gender { get; set; }
        public string dateBirth { get; set; }
        public string invitationCode { get; set; }
        public string reserveInfo { get; set; }
        public string address { get; set; }
        public string remarks { get; set; }

    }
    /// <summary>
    /// 面具 个人信息
    /// </summary>
    public class UserInfomation
    {
        /// <summary>
        /// 
        /// </summary>
        public int userIdx { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Myname { get; set; }
        /// <summary>
        /// 性别0男1女
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        ///  年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 小图
        /// </summary>
        public string Smallpic { get; set; }
        /// <summary>
        /// 大图
        /// </summary>
        public string Bigpic { get; set; }
        /// <summary>
        /// 职业
        /// </summary>
        public string Job { get; set; }
        /// <summary>
        /// 约会范围
        /// </summary>
        public string AppointmentRage { get; set; }
        /// <summary>
        /// 约会内容
        /// </summary>
        public string AppointmentProgram { get; set; }
        /// <summary>
        /// 约会期望
        /// </summary>
        public string AppointmentExpect { get; set; }
        /// <summary>
        /// 月收入
        /// </summary>
        public string MonthlyIncome { get; set; }
        /// <summary>
        /// 体型
        /// </summary>
        public string Shape { get; set; }
        /// <summary>
        /// 微信
        /// </summary>
        public string WeChat { get; set; }
        /// <summary>
        /// qq
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// 身高
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 体重
        /// </summary>
        public int Weight { get; set; }
        /// <summary>
        /// 胸围
        /// </summary>
        public string Bust { get; set; }
        /// <summary>
        /// 个人介绍
        /// </summary>
        public string Introduction { get; set; }
        /// <summary>
        /// 状态 1表示没有完善资料跳转完善资料  2社交隐藏 3社交不隐藏
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string phoneNumber { get; set; }
        /// <summary>
        /// 照片数量
        /// </summary>
        public int PhotoNumber { get; set; }
        /// <summary>
        /// 照片价格
        /// </summary>
        public decimal PhotoMoney { get; set; }
        /// <summary>
        /// 社交帐号  默认0 公开    1-隐身
        /// </summary>
        public int realState { get; set; }
    }
    public class UserMynameInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int userIdx { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Myname { get; set; }
        /// <summary>
        /// 性别0男1女
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 小图
        /// </summary>
        public string Smallpic { get; set; }
        /// <summary>
        /// 大图
        /// </summary>
        public string Bigpic { get; set; }
        /// <summary>
        /// 0否 1vip
        /// </summary>
        public string IsVip { get; set; }
        /// <summary>
        ///  女性认证
        /// </summary>
        public int Authentication { get; set; }
        /// <summary>
        /// 访客数量
        /// </summary>
        public int VisitorCount { get; set; }
        /// <summary>
        /// 关注数 喜欢数
        /// </summary>
        public long FollowNum { get; set; }
        /// <summary>
        /// 被喜欢数
        /// </summary>
        public long FansNum { get; set; }
        /// <summary>
        /// 阅后及焚数量
        /// </summary>
        public int lookNum { get; set; }


    }
    /// <summary>
    /// 列表
    /// </summary>
    public class UserListInfo
{
    /// <summary>
    /// 排序
    /// </summary>
    public long rowid { get; set; }
        /// <summary>
        /// 排idx
        /// </summary>
        public int userIdx { get; set; }
        /// <summary>
        ///  昵称
        /// </summary>
        public string Myname { get; set; }
        /// <summary>
        /// 性别 0男 1-女
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 体型
        /// </summary>
        public string  Shape { get; set; }
        /// <summary>
        ///  年龄
        /// </summary>
        public int Age { get; set; }
    /// <summary>
    /// 杭州市
    /// </summary>
    public string City { get; set; }
    /// <summary>
    /// 图片
    /// </summary>
    public string Smallpic { get; set; }
    /// <summary>
    /// 信息技术|it
    /// </summary>
    public string Job { get; set; }
    /// <summary>
    /// 胸围
    /// </summary>
    public string Bust { get; set; }
    /// <summary>
    /// 收入范围
    /// </summary>
    public string MonthlyIncome { get; set; }
    /// <summary>
    /// 身高
    /// </summary>
    public int Height { get; set; }
    /// <summary>
    /// 体重
    /// </summary>
    public int Weight { get; set; }
    /// <summary>
    /// 介绍
    /// </summary>
    public string Introduction { get; set; }
    /// <summary>
    /// 照片数量
    /// </summary>
    public int PhotoNumber { get; set; }
    /// <summary>
    /// 照片价格
    /// </summary>
    public decimal photoMoney { get; set; }
        /// <summary>
        /// 0 不是VIP 1是 vip 
        /// </summary>
        public int IsVip { get; set; }
        /// <summary>
        ///  0未认证 1-已经认证
        /// </summary>
        public int Authentication { get; set; }
    /// <summary>
    /// 是否在线
    /// </summary>
    public string IsLine { get; set; }
    /// <summary>
    /// 距离
    /// </summary>
    public string Distance { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Addtime { get; set; }
        /// <summary>
        /// 收益数量
        /// </summary>
        public Decimal Number { get; set; }
}
    /// <summary>
    /// 查询列表
    /// </summary>
    public class UserSearchInfo
    {
        /// <summary>
        /// 排序
        /// </summary>
        public long rowid { get; set; }
        /// <summary>
        /// 排idx
        /// </summary>
        public int userIdx { get; set; }
        /// <summary>
        ///  昵称
        /// </summary>
        public string Myname { get; set; }
        /// <summary>
        /// 性别 0男 1-女
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        ///  年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 杭州市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Smallpic { get; set; }
        /// <summary>
        /// 身高
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 体重
        /// </summary>
        public int Weight { get; set; }
        /// <summary>
        /// 0 不是VIP 1是 vip 
        /// </summary>
        public int IsVip { get; set; }
        /// <summary>
        ///  0未认证 1-已经认证
        /// </summary>
        public int Authentication { get; set; }

    }
    /// <summary>
    /// 用户评价
    /// </summary>
    public class Userevaluate
    {
        public int useridx { get; set; }
        /// <summary>
        /// 评价内容
        /// </summary>
        public string evaluate { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int number { get; set; }
    }
    /// <summary>
    /// 个人信息获取约会期望职业约会节目数据
    /// </summary>
    public class UserDataInfo
    {
        public int Id { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 类别编号 0-职业 1-约会期望 2-约会节目
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleTypeName { get; set; }
        /// <summary>
        /// 模块内容
        /// </summary>
        public string ModuleContent { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

    }
    public class Userevaluates
    {
        public long rowid { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Myname { get; set; }
        /// <summary>
        /// 评价内容
        /// </summary>
        public string evaluate { get; set; }
        public DateTime addtime { get; set; }
    }
    public class GameCenterUserInfo
    {


        /// <summary>
        /// 
        /// </summary>
        public int useridx { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string InvitationCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string phoneNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int gender { get; set; }
        /// <summary>
        /// 
        /// </summary>
       // public string dateBirth { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reserveInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string remarks { get; set; }
        /// <summary>
        /// 
        /// </summary>
      //  public string addtime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string myname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string signatures { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long owncash { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string smallpic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// 
    }

    public class myUserAgentInfo
    {
        public int useridx { get; set; }
        public int weekConsumeTwo { get; set; }

        public int weekConsumeThree { get; set; }
        public int weekConsumeOne { get; set; }
        public int weekConsume { get; set; }
        public Decimal weekRebateOne { get; set; }
        public Decimal weekRebateTwo { get; set; }
        public Decimal weekRebateThree { get; set; }
        public Decimal weekRebate { get; set; }
        public Decimal Rebate { get; set; }
        public int subordinateNumOne { get; set; }
        public int subordinateNumTwo { get; set; }
        public int subordinateNumThree { get; set; }
        public DateTime Settlement { get; set; }

    }

    public class BankCardImgConfigure
    {
        //        id  INT IDENTITY(1,1), --id
        //bankName VARCHAR(50), --银行名称
        //bankIcon VARCHAR(200),--银行卡图标
        //bankBackgIcon VARCHAR(200),--银行卡背景图标
        //bankBackgColour  VARCHAR(200), --银行卡背景颜色
        //reserve INT , --预留
        //reserveStr VARCHAR(100) --预留

        public int id { get; set; }
        public string bankName { get; set; }
        public string bankIcon { get; set; }
        public string bankBackgIcon { get; set; }
        public string bankBackgColour { get; set; }
        public int reserve { get; set; }
        public string reserveStr { get; set; }

    }


    public class LaoWo_UserBankInfo
    {

        //        id INT IDENTITY(1,1),
        //userIdx int ,				--用户idx
        //bankUserName VARCHAR(20),	--持卡人名称
        //bankId  VARCHAR(30) ,				--银行信息id
        //bankType INT ,				--银行类别
        //BankCardType INT ,			--银行卡类别
        //rProvince VARCHAR(50),		--开户省
        //rCity VARCHAR(50),			--开户市
        //Dot VARCHAR(200)			--开户网点
        public int id { get; set; }
        public int userIdx { get; set; }
        public string bankUserName { get; set; }
        public string bankId { get; set; }
        public int bankType { get; set; }
        public int bankCardType { get; set; }
        public string rProvince { get; set; }
        public string rCity { get; set; }
        public string dot { get; set; }
        public string bankName { get; set; }
        public string bankIcon { get; set; }
        public string bankBackgIcon { get; set; }
        public string bankBackgColour { get; set; }
        public LaoWo_UserBankInfo() { }
        public LaoWo_UserBankInfo(Dictionary<string, string> dic)
        {
            this.id = 0;
            this.userIdx = int.Parse(dic["userIdx"].ToString());
            this.bankUserName = dic["bankUserName"].ToString();
            this.bankType = int.Parse(dic["bankType"].ToString());
            this.bankCardType = int.Parse(dic["bankCardType"].ToString());
            this.bankId = dic["bankId"].ToString();
            this.rProvince = dic["rProvince"].ToString();
            this.rCity = dic["rCity"].ToString();
            this.dot = dic["dot"].ToString();

        }
    }

    public class LW_IncomeLog
    {
        public int id { get; set; }
        public int userIdx { get; set; }
        public int sType { get; set; }
        public string sName { get; set; }
        public string company { get; set; }
        public Decimal reward { get; set; }
        public int iState { get; set; }
        public DateTime addtime { get; set; }
    }

    public class LW_Accounts_MoneyTradeDetail
    {

        public int userIdx { get; set; }
        public int type { get; set; }
        public string descript { get; set; }
        public Decimal money { get; set; }
        public int transactionMode { get; set; }
        public DateTime addtime { get; set; }
    }
    /// <summary>
    /// 游戏中心用户数据
    /// </summary>
    public class GameUserInfo
    {
        public int userIdx { get; set; }
        public int type { get; set; }
        public string descript { get; set; }
    }
    /// <summary>
    /// 主播提现记录
    /// </summary>
    public class AnchorTixianList
    {
        public DateTime idate { get; set; }
        /// <summary>
        /// 提现港币   *100等于提现蜜币
        /// </summary>
        public decimal amount { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int step { get; set; }

    }

}
