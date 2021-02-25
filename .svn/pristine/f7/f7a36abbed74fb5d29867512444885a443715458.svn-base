using System;
using Common;
using System.Web;
using Newtonsoft.Json;
using Model;
using Model.Common;
using System.Web.Security;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BLL
{
    public class AppDataBLL
    {
        /// <summary>
        /// 喵播默认图
        /// </summary>
        public static string DefaultPhoto { get { return "http://liveimg.9158.com/default.png"; } }

        /// <summary>
        /// 获取设备版本号
        /// </summary>
        public static string AppVersion
        {
            get { return HttpContext.Current.Request.Headers["version"] ?? "0"; }
        }

        /// <summary>
        /// app设备类型
        /// </summary>
        public static string AppDeviceType
        {
            get { return HttpContext.Current.Request.Headers["deviceType"] ?? "android"; }
        }

        /// <summary>
        /// 渠道类型
        /// </summary>
        public static string AppChannelId
        {
            get { return HttpContext.Current.Request.Headers["channelid"] ?? ""; }
        }

        public static string GetUseridx
        {
            get { return HttpContext.Current.Request.Headers["useridx"] ?? "0"; }
        }
        /// <summary>
        /// 设备号
        /// </summary>
        public static string AppDeviceId
        {
            get { return HttpContext.Current.Request.Headers["deviceid"] ?? ""; }
        }

        /// <summary>
        /// 1：正常（默认），0：审核
        /// </summary>
        public static string AuditStatus
        {
            get { return HttpContext.Current.Request.Headers["auditStatus"] ?? "1"; }
        }

        public static string GetAreaid
        {
            get { return HttpContext.Current.Request.Headers["areaid"] ?? "0"; }
        }

        /// <summary>
        /// 黑名单地区
        /// </summary>
        public static string blackAreaStatus
        {
            get { return HttpContext.Current.Request.Headers["blackAreaStatus"] ?? "1"; }
        }

        public static string bigGiftConfig
        {
            get { return "http://img.imeyoo.com/Gift/AnimationConfig.json"; }
        }

        /// <summary>
        /// 弹幕价格
        /// </summary>
        public static string barrage
        {
            get { return LiveBLL.Get_LiveConfigById(1).data; }
        }
        /// <summary>
        /// 全站弹幕价格
        /// </summary>
        public static string totalBarrage
        {
            get { return LiveBLL.Get_LiveConfigById(4).data; }
        }
        /// <summary>
        /// 红包最低价格
        /// </summary>
        public static string hongbaoPrice
        {
            get { return LiveBLL.Get_LiveConfigById(5).data; }
        }
        /// <summary>
        /// 传送价格
        /// </summary>
        public static string transferPrice
        {
            get { return LiveBLL.Get_LiveConfigById(6).data; }
        }
        /// <summary>
        /// 苹果支付开关 2：显示国内支付 1：显示苹果充值（包含黑名单地区） 0：都显示（不包含黑名单地区）
        /// </summary>
        public static string paySwitch
        {
            get { return LiveBLL.Get_LiveConfigById(11).data; }
        }

        /// <summary>
        /// 是否显示我的收益（兑换娃娃）
        /// </summary>
        public static string myIncome
        {
            get { return LiveBLL.Get_LiveConfigById(13).data; }
        }

        /// <summary>
        /// 是否显示微钱进
        /// </summary>
        public static string exchange
        {
            get { return LiveBLL.Get_LiveConfigById(14).data; }
        }

        /// <summary>
        /// 送礼物数量
        /// </summary>
        /// <returns></returns>
        public static string[] GiftNum()
        {
            return Tools.SplitString(LiveBLL.Get_LiveConfigById(2).data, ",");
        }

        /// <summary>
        /// 大礼物资源文件
        /// </summary>
        /// <returns></returns>
        public static string[] BigGiftRange()
        {
            var list = LiveBLL.Get_AllFaceStickers().FindAll(f => f.faceType == 3);

            System.Text.StringBuilder str = new System.Text.StringBuilder();
            foreach (var item in list)
            {
                str.Append(item.resName + ",");
            }
            return Tools.SplitString(str.ToString().TrimEnd(','), ",");
        }

        public static string[] GetBigGiftArrary(string path)
        {
            //string path = @"G:\logFiles\LiveLog\2017-11-22";
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            System.IO.DirectoryInfo folder = new System.IO.DirectoryInfo(path);

            foreach (FileInfo file in folder.GetFiles("*.zip"))
            {
                str.Append(file.Name + ",");
            }

            return Tools.SplitString(str.ToString().TrimEnd(','), ",");
        }

        /// <summary>
        /// iOS充值检测是否存在这个bundleid
        /// </summary>
        /// <param name="bundleId"></param>
        /// <returns></returns>
        public static bool CheckBundle_id(string bundleId)
        {
            string[] key = { "com.tiange.miaolive", "com.songchao.maobo", "com.tiange.miaomiao" };
            foreach (string str in key)
            {
                if (bundleId.IndexOf(str) >= 0)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 苹果支付黑名单
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static bool PayblackAddress(string position)
        {
            string[] key = { "北京", "香港", "美国", "新加坡", "香港特别行政区" };
            foreach (string str in key)
            {
                if (position.IndexOf(str) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 游戏黑名单
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static bool GameblackAddress(string position)
        {
            string[] key = Tools.SplitString(LiveBLL.Get_LiveConfigById(102).data, ",");// { "北京", "江苏", "连云港", "苏州" };
            foreach (string str in key)
            {
                if (position.IndexOf(str) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 随机分享文案
        /// </summary>
        /// <param name="nickName"></param>
        /// <returns></returns>
        public static string GetShareLettes(int useridx, string nickName)
        {
            var _content = "「" + nickName + "」";
            var _random = new Random((int)DateTime.Now.Ticks);
            string[] str = {
                                "{0}说：瞧我这绝活，也是没谁了~",
                                "{0}说：我今天有点特别哦，你发现了吗~",
                                "{0}正在直播：坐好，要起飞了...",
                                "{0}向你发出邀请，点进来就告诉你！",
                                "你关注的{0}@了你，想和你聊会天！",
                                "{0}想要和你互动：喵播第一浪，约吗？",
                                "爱你的小姐姐{0}想要给你跳个舞，要看呦！",
                                "{0}开始直播就想你了，花式求抱抱；"
                            };
            string _letters = str[_random.Next(0, str.Length)];
            return string.Format(_letters, _content);
        }

        #region 等级Level&Grade

        /// <summary>
        /// 获取等级阶级
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public static int Get_Grade_Rank(int grade)
        {
            int mylevel = 1;
            if (grade > 0 && grade <= 40)
            {
                mylevel = 1;
            }
            else if (grade > 40 && grade <= 80)
            {
                mylevel = 2;
            }
            else if (grade > 80 && grade <= 120)
            {
                mylevel = 3;
            }
            else if (grade > 120 && grade <= 160)
            {
                mylevel = 4;
            }
            else if (grade > 160 && grade <= 190)
            {
                mylevel = 5;
            }
            else if (grade > 190)
            {
                mylevel = 6;
            }
            return mylevel;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public static int Get_AnchorGrade_Rank(int grade)
        {
            int mylevel = 0;
            if (grade > 0 && grade <= 30)
            {
                mylevel = 1;
            }
            else if (grade > 30 && grade <= 45)
            {
                mylevel = 2;
            }
            else if (grade > 45 && grade <= 60)
            {
                mylevel = 3;
            }
            else if (grade > 60 && grade <= 75)
            {
                mylevel = 4;
            }
            else if (grade > 75)
            {
                mylevel = 5;
            }
            return mylevel;
        }

        //Vip等级对应名称
        public static string GetVipName(int level)
        {
            string name = "";
            switch (level)
            {
                case 1:
                    name = "普通土豪";
                    break;
                case 11:
                    name = "红色VIP";
                    break;
                case 15:
                    name = "紫色VIP";
                    break;
                case 30:
                    name = "蓝色皇冠";
                    break;
                case 31:
                    name = "粉色皇冠";
                    break;
                case 32:
                    name = "超级皇冠";
                    break;
                case 34:
                    name = "钻石皇冠";
                    break;
                case 35:
                    name = "王者皇冠";
                    break;
                case 36:
                    name = "代理";
                    break;
                case 39:
                    name = "至尊皇冠";
                    break;
                case 130:
                    name = "超管";
                    break;
                default:
                    break;
            }
            return name;
        }

        /// <summary>
        /// 用户等级对应标签
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public static string GetUserLabelName(int grade)
        {
            string name = "";

            if (grade > 0 && grade <= 5)
            {
                name = "一无所有";
            }
            else if (grade > 5 && grade <= 15)
            {
                name = "温饱户";
            }
            else if (grade > 15 && grade <= 30)
            {
                name = "小康家庭";
            }
            else if (grade > 30 && grade <= 50)
            {
                name = "暴发户";
            }
            else if (grade > 50 && grade <= 80)
            {
                name = "普通土豪";
            }
            else if (grade > 80 && grade <= 120)
            {
                name = "百万富翁";
            }
            else if (grade > 120 && grade <= 180)
            {
                name = "千万富翁";
            }
            else if (grade > 180 && grade <= 220)
            {
                name = "亿万富翁";
            }
            else if (grade > 220 && grade <= 280)
            {
                name = "亚洲首富";
            }
            else if (grade == 36)
            {
                name = "喵星客服";
            }
            else if (grade == 130)
            {
                name = "超级代理";
            }
            else if (grade > 280)
            {
                name = "世界首富";
            }
            return name;
        }

        /// <summary>
        /// 用户等级对应标签
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public static string GetAnchorLabelName(int grade, int gender = 0)
        {
            string name = "";

            if (grade > 0 && grade <= 10)
            {
                name = "群众演员";
            }
            else if (grade > 10 && grade <= 30)
            {
                name = "跑龙套";
            }
            else if (grade > 30 && grade <= 50)
            {
                name = "演艺新秀";
            }
            else if (grade > 50 && grade <= 70)
            {
                name = "正式演员";
            }
            else if (grade > 70 && grade <= 80)
            {
                name = "二线明星";
            }
            else if (grade > 80 && grade <= 90)
            {
                name = "担当主角";
            }
            else if (grade > 90 && grade <= 100)
            {
                name = "最佳演员";
            }
            else if (grade > 100 && grade <= 120)
            {
                name = "大牌明星";
            }
            else if (grade > 120 && grade <= 150)
            {
                name = "超级影星";
            }
            else if (grade > 150)
            {
                name = gender == 0 ? "奥斯卡影后" : "奥斯卡影帝";
            }
            return name;
        }

        #endregion

        public static int GetLoginType(string userid)
        {
            if (string.IsNullOrEmpty(userid)) return 0;

            userid = TextHelper.GetLetterString(userid);
            int thirdtype = 0;

            if (userid == MobileEnum.ThirdType.QQ.ToString())
            {
                thirdtype = 1;
            }
            else if (userid == MobileEnum.ThirdType.WeiXin.ToString())
            {
                thirdtype = 2;
            }
            else if (userid == MobileEnum.ThirdType.SinaWeibo.ToString())
            {
                thirdtype = 3;
            }
            else if (userid == MobileEnum.ThirdType.ChenLong.ToString())
            {
                thirdtype = 4;
            }
            return thirdtype;
        }

        /// <summary>
        /// 过滤非法文本（广告）
        /// </summary>
        /// <param name="fuidx"></param>
        /// <param name="touidx"></param>
        /// <param name="fromType">平台类型 502：喵播，503：喵拍</param>
        /// <param name="content"></param>
        /// <returns>true:内容违规</returns>
        public static bool VertifyContent(int fuidx, int touidx, int fromType, string content)
        {
            bool flag = false;
            string userip = Tools.GetRealIP();
            string url = string.Format("http://ggxt.9158.com:8844/?msg={0}||{1}||{2}||5||{3}||{4}||0||{5}||Name"
                , fuidx, touidx, DateTime.Now, content, userip, fromType);

            string result = HttpHelper.HttpGet(url);

            if (result.Equals("1.0"))
            {
                flag = true;

                //LogHelper.WriteLog(LogFile.Data, "【违规文本】" + fuidx + "," + fromType + "," + result + "," + content);
            }
            return flag;
        }

        /// <summary>
        /// 提现
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public static MobileResult withdrawAlipay(int useridx)
        {
            string timestamp = TimeHelper.GetTimeStamp();
            string md5KEY = "FYP5Ldx8";
            string payChkCode = FormsAuthentication.HashPasswordForStoringInConfigFile(useridx + timestamp + md5KEY, "MD5").ToLower(); ;
            string withdrawURL = string.Format("http://pay.miaobolive.com/pay/AlipayDrawMoney.aspx?useridx={0}&timestamp={1}&chk={2}", useridx, timestamp, payChkCode);
            string data = HttpHelper.HttpGet(withdrawURL);
            try
            {
                return JsonConvert.DeserializeObject<MobileResult>(data);
            }
            catch (Exception)
            {
                return new MobileResult(104, "出错啦！");
            }
        }

    }
}
