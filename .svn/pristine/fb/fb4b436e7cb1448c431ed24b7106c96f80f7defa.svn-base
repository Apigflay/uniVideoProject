using Common;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Security;
using DAL;

namespace BLL
{
    public class ThirdLoginParam
    {
        /// <summary>
        /// 第三方唯一值
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 游客账号参数
        /// </summary>
        public int visitoridx { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string devType { get; set; }
        /// <summary>
        /// 平台 miaobo / miaopai
        /// </summary>
        public string platForm { get; set; }
        /// <summary>
        /// QQ 登录参数
        /// </summary>
        public string Unionid { get; set; }
        /// <summary>
        /// 渠道号
        /// </summary>
        public int Channelid { get; set; }
        /// <summary>
        /// 设备号
        /// </summary>
        public string DeviceId { get; set; }

    }
    public class ThirdLoginBLL
    {
        private static AccountDAL _account = new AccountDAL();

        /// <summary>
        /// 第三方帐号类型
        /// </summary>
        public enum ThirdType { QQ, WeiXin, SinaWeibo, ChenLong, Google, Twitter, Facebook, m, Huawei }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="third"></param>
        /// <param name="thirdinfo"></param>
        /// <param name="iRet"></param>
        /// <param name="param">公共参数，业务参数</param>
        /// <returns></returns>
        public static MemberInfo VerifyThirdMember(ThirdType third, string thirdinfo, ref int iRet, ThirdLoginParam param)
        {
            MemberInfo member = new MemberInfo();

            if (third == ThirdType.QQ)
            {
                OAuth.QQ.QQUser qq_info = JsonConvert.DeserializeObject<OAuth.QQ.QQUser>(thirdinfo);

                member.Account = param.Account;//Openid
                //member.Unionid = param.Unionid.ToString();

                member.NickName = qq_info.nickname;
                member.Signature = "";
                member.Sex = qq_info.gender.Equals("男") ? 1 : 0;
                member.Photo = qq_info.figureurl_qq_1.Replace("\\", "");
                member.BigPic = qq_info.figureurl_qq_2.ToString().Replace("\\", "");
            }
            else if (third == ThirdType.WeiXin)
            {
                OAuth.Weixin.WeiXinUser wx_info = JsonConvert.DeserializeObject<OAuth.Weixin.WeiXinUser>(thirdinfo);

                member.Account = wx_info.unionid;
                member.Pwd = CryptoHelper.ToMD5("mianjuweixin888").ToLower();

                //member.NickName = wx_info.nickname;
                //member.Signature = "";
                //member.Sex = wx_info.sex == 1 ? 1 : 0;//微信的性别1：男 2：女
                //member.Photo = wx_info.headimgurl.Replace("/0", "/132"); ;
                //member.BigPic = wx_info.headimgurl;
            }
            else if (third == ThirdType.SinaWeibo)
            {
                OAuth.SinaWeibo.SinaWeiboUser wb_info = JsonConvert.DeserializeObject<OAuth.SinaWeibo.SinaWeiboUser>(thirdinfo);

                member.Account = wb_info.idstr;

                member.NickName = wb_info.screen_name;
                member.Signature = wb_info.description;
                member.Sex = wb_info.gender.Equals("m") ? 1 : 0;
                member.Photo = wb_info.avatar_large;
                member.BigPic = wb_info.avatar_hd;
            }
            else if (third == ThirdType.ChenLong)
            {
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(thirdinfo);

                member.Account = CryptoHelper.ToMD5(dic["uid"].ToString());

                member.NickName = dic["nikename"].ToString();
                member.Signature = "";
                member.Sex = dic["gender"].Equals("1") ? 1 : 0;
                member.Photo = dic["photo"].ToString();
                member.BigPic = dic["photo"].ToString();
            }
            else if (third == ThirdType.Huawei)
            {
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(thirdinfo);

                member.Account = CryptoHelper.ToMD5(param.Account);

                member.NickName = GetNickName();// dic["nickname"].ToString();
                member.Signature = "";
                member.Sex = 1;// dic["gender"].Equals("1") ? 1 : 0;
                member.Photo = "http://liveimg.9158.com/head_default250.jpg";// dic["photo"].ToString();
                member.BigPic = "http://liveimg.9158.com/head_default250.jpg";// dic["photo"].ToString();
            }
            else if (third == ThirdType.Facebook)
            {
                OAuth.Facebook.FaceBookUser fb_info = JsonConvert.DeserializeObject<OAuth.Facebook.FaceBookUser>(thirdinfo);

                member.Account = fb_info.id;

                member.NickName = fb_info.name; 
                member.Signature = "";
                member.Sex = 1;// dic["gender"].Equals("1") ? 1 : 0;
                member.Photo = "http://img.imeyoo.com/default_250.png";// dic["photo"].ToString();
                member.BigPic = "http://img.imeyoo.com/default_640.png";// dic["photo"].ToString();
            }
            #region [ Common Param ]

            iRet = -1;
            //int provinceid = 11, cityid = 69;
            var userip = Tools.GetRealIP();
            //取出省和市ID/给118库用
            //CommonBLL.Instance.GetAddressByIP(ref provinceid, ref cityid);
            //IPModel ip_info = CommonBLL.Instance.GetAddressByIP();
            member.ip = userip;
            member.devType = param.devType.Trim();


            #endregion

            #region [ Check NickName Is Illegal ]

            //Add 2017-03-24 检测昵称是否违规
            //if (!Check_illegalnickName(member.NickName))
            //{
            //    return null;
            //}

            //if (AppDataBLL.VertifyContent(0, 0, 502, member.NickName))
            //{
            //    member.NickName = GetNickName();
            //}

            #endregion

            //if (Tools.IsDebug)
            //{
            //    member.Account = "og7NiuO7ukXC5LN7xJpPyXs64cDA";
            //}
            //Register Or Login Or Visitor Bind
            member = _account.VerifyThirdMember_V2(member, ref iRet);

            //First Resigter Success
            if (iRet == 1)
            {
                Location loc = PositionHelper.GetLocationInfo(userip);
                member.Province = loc.Country != "中国" ? "海外" : loc.Province;
                member.City = loc.City;

                //注册成功后数据同步到喵播库 Live_userinfo表
                _account.Live_Register(iRet, member);

                //记录qq登陆用户的unionid
                if (third == ThirdType.QQ || !string.IsNullOrEmpty(param.Unionid))
                {
                    _account.Live_QQLogin_Insert_Data(member.UIdx, member.Account, param.Unionid);
                }
            }
            else if (iRet < 0)
            {
                //string msg = JsonConvert.SerializeObject(member);
                LogHelper.WriteLog(LogFile.Log, "【登录/注册/游客绑定失败】{0}|{1}|{2}", iRet, member.Account, member.NickName);
            }

            //获取用户所在地区信息
            IPModel db_IP_Info = _account.Live_PositionInfo_Insert_Data(member.UIdx, userip, iRet, null);

            if (db_IP_Info != null)
            {
                member.areaid = db_IP_Info.areaid;
            }

            return member;
        }

        public static int VerifyThirdLogin(ThirdType third, string thirdinfo, ThirdLoginParam param, int channelid,string deviceId)
        {
            MemberInfo member = new MemberInfo();

            if (third == ThirdType.QQ)
            {
                OAuth.QQ.QQUser qq_info = JsonConvert.DeserializeObject<OAuth.QQ.QQUser>(thirdinfo);

                member.Account = param.Account;//Openid
                //member.Unionid = param.Unionid.ToString();

                member.NickName = qq_info.nickname;
                member.Signature = "";
                member.Sex = qq_info.gender.Equals("男") ? 1 : 0;
                member.Photo = qq_info.figureurl_qq_1.Replace("\\", "");
                member.BigPic = qq_info.figureurl_qq_2.ToString().Replace("\\", "");
            }
            else if (third == ThirdType.WeiXin)
            {
                OAuth.Weixin.WeiXinUser wx_info = JsonConvert.DeserializeObject<OAuth.Weixin.WeiXinUser>(thirdinfo);

                member.Account = wx_info.unionid;
                member.NickName = wx_info.nickname;
                member.Photo = wx_info.headimgurl.Replace("/0", "/132"); ;
                member.BigPic = wx_info.headimgurl;
                member.Pwd = CryptoHelper.ToMD5("mianjuweixin888").ToLower();

                //member.NickName = wx_info.nickname;
                //member.Signature = "";
                //member.Sex = wx_info.sex == 1 ? 1 : 0;//微信的性别1：男 2：女
                
            }
            else if (third == ThirdType.SinaWeibo)
            {
                OAuth.SinaWeibo.SinaWeiboUser wb_info = JsonConvert.DeserializeObject<OAuth.SinaWeibo.SinaWeiboUser>(thirdinfo);

                member.Account = wb_info.idstr;

                member.NickName = wb_info.screen_name;
                member.Signature = wb_info.description;
                member.Sex = wb_info.gender.Equals("m") ? 1 : 0;
                member.Photo = wb_info.avatar_large;
                member.BigPic = wb_info.avatar_hd;
            }
            else if (third == ThirdType.ChenLong)
            {
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(thirdinfo);

                member.Account = CryptoHelper.ToMD5(dic["uid"].ToString());

                member.NickName = dic["nikename"].ToString();
                member.Signature = "";
                member.Sex = dic["gender"].Equals("1") ? 1 : 0;
                member.Photo = dic["photo"].ToString();
                member.BigPic = dic["photo"].ToString();
            }
            else if (third == ThirdType.Huawei)
            {
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(thirdinfo);

                member.Account = CryptoHelper.ToMD5(param.Account);

                member.NickName = GetNickName();// dic["nickname"].ToString();
                member.Signature = "";
                member.Sex = 1;// dic["gender"].Equals("1") ? 1 : 0;
                member.Photo = "http://liveimg.9158.com/head_default250.jpg";// dic["photo"].ToString();
                member.BigPic = "http://liveimg.9158.com/head_default250.jpg";// dic["photo"].ToString();
            }
            else if (third == ThirdType.Facebook)
            {
                OAuth.Facebook.FaceBookUser fb_info = JsonConvert.DeserializeObject<OAuth.Facebook.FaceBookUser>(thirdinfo);

                member.Account = fb_info.id;

                member.NickName = fb_info.name;
                member.Signature = "";
                member.Sex = 1;// dic["gender"].Equals("1") ? 1 : 0;
                member.Photo = "http://img.imeyoo.com/default_250.png";// dic["photo"].ToString();
                member.BigPic = "http://img.imeyoo.com/default_640.png";// dic["photo"].ToString();
            }
            #region [ Common Param ]

            //int provinceid = 11, cityid = 69;
            var userip = Tools.GetRealIP();
            //取出省和市ID/给118库用
            //CommonBLL.Instance.GetAddressByIP(ref provinceid, ref cityid);
            //IPModel ip_info = CommonBLL.Instance.GetAddressByIP();
            member.ip = userip;
            member.devType = param.devType.Trim();


            #endregion

            int Ret = _account.VerifyThirdLogin(member, channelid, deviceId);

            //First Resigter Success
            if (Ret == 0 )
            {
                LogHelper.WriteLog(LogFile.Log, "【登录/注册】{0}|{1}|{2}", Ret, member.Account);
            }
            return Ret;
        }

        /// <summary>
        /// 验证第三方昵称如果非法替换为空
        /// </summary>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public static string VerifyNickName(string nickname, int length = 15)
        {
            if (string.IsNullOrEmpty(nickname.Trim()) || !CheckNickName(nickname))
            {
                nickname = GetNickName();
            }
            nickname = TextHelper.Substring(nickname, length, "").Trim();

            return nickname;
        }
        /// <summary>
        /// 第三方登陆随机用户密码
        /// </summary>
        /// <param name="pwdSrc"></param>
        /// <returns></returns>
        public static string RandomMd5Pwd()
        {
            string rdmPwd = Membership.GeneratePassword(16, 1).Replace("&", "@").ToLower();
            return CryptoHelper.ToMD5(rdmPwd).ToLower();
        }
        /// <summary>
        /// 随机昵称
        /// </summary>
        /// <returns></returns>
        public static string GetNickName()
        {
            var random = new Random((int)DateTime.Now.Ticks);
            string[] move = { "土豪", "小雪", "小朵", "小武", "小白", "小米", "小新", "喵播用户", "手机用户" };

            return move[random.Next(0, move.Length)] + RandomHelper.GenerateNum(5);
        }
        /// <summary>
        /// 检查非法昵称 
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public static bool CheckNickName(string UserName)
        {
            string[] key1 = { "mm", "jj", "bb", "jb", "sb", "av", "www.", "com.", "?", "//", "@", "http://", "baobei", "admin", "&", "%", "9158", "裸", "脱", "拖", "托", "操", "日", "你妈", "干你", "几吧", "煞笔", "你好难看呀", "就约我吧", "我草", "叼", "卧槽", "色情", "情色", "a片", "毛片", "女优", "妓女", "鸭子", "咪咪", "鸡吧", "激情", "大奶", "夫妻", "少妇", "粗大", "授精", "手淫", "爽吗", "飞机", "大无霸" };
            foreach (string str1 in key1)
            {
                if (UserName.IndexOf(str1) >= 0)
                    return false;
            }

            string[] key2 = { "👮🏻", "系统", "客服", "公告", "中奖", "官方", "消息", "运营", "巡管", "巡警", "做爱", "看片", "看b", "看互", "看女", "找女", "浪女", "骚女", "等女", "熟女", "视频女", "露咪", "露j", "操你", "操b", "滚男", "视频男", "系统消息", "习近平", "代充", "销售", "喵播", "毒" };
            foreach (string str2 in key2)
            {
                for (int i = 0; i < str2.Length; i++)
                {
                    if (UserName.IndexOf(str2[i]) >= 0)
                    {
                        if (i == str2.Length - 1)
                            return false;
                        else
                            continue;
                    }
                    else
                        break;
                }
            }

            return true;
        }
        /// <summary>
        /// 检测非法昵称
        /// </summary>
        /// <param name="_content"></param>
        /// <returns></returns>
        public static bool Check_illegalnickName(string _content)
        {
            _content = TextHelper.ToDBC(_content).ToLower();
            string[] key = { "看B", "看妹妹", "福利", "私播", "喷水", "脱光", "真人", "性用", "表演", "luo聊", "luo", "倮", "裸", "mm124234", "威信", "+Q", "50元", "100元", "刺激", "先货", "一对一", "+v信", "看图片", "扣逼" };

            foreach (string str in key)
            {
                if (_content.IndexOf(str) >= 0)
                    return false;
            }
            return true;
        }
    }
}
