using BLL;
using Common;
using Common.Core;
using Newtonsoft.Json;
using Submail.AppConfig;
using Submail.Lib;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
/// <summary>
/// SendCode 的摘要说明
/// </summary>
public class SendCodeBLL
{
    //public static string pType = "0";  // 1是注册验证码 2是绑定手机验证码 3绑定成功提示 0是普通短信验证码
    public static string userIdx = "91589158";

    public static string sms_tell = ""; //手机号
    public static string sms_hmd = "";
    public static string sms_key1 = "6NB5H7X9";
    public static string sms_key2 = "OUFG-78CB-XE31-F81M";
    public static string accountId = "2575";
    public static string account = "SDK-A2575-2575";
    public static string password = "215741";
    //public static string mobile = "OUFG-78CB-XE31-F81M";

    //public static string sms_key1 = "MHNR57IQ"; //text
    //public static string sms_key2 = "CVDE-AZ6H-SDC5-MH89";
    public static string sms_con = "";//发送的消息

    /// <summary>
    /// 给手机发送验证码
    /// </summary>
    /// <param name="uidx">用户idx 可空值</param>
    /// <param name="uid">用户名 可空值</param>
    /// <param name="phonenum">手机号码</param>
    /// <param name="msg">发送的文本文字 可空值</param>
    /// <param name="sendType">验证码类型 1是注册  2是绑定 3绑定成功 可空值</param>
    /// <returns>1成功 0发送验证码失败 -1参数错误 -2验证码1分钟内只能获取一次 -3发送验证码次数已受限(同手机号5天只能发送5次,同IP一天10次) -4同IP已超限 -5：发送验证码太频繁 </returns>
    public static int sendSmsInfo(string uidx, string uid, string phonenum, string devId, int sendType, string Areacode)
    {
        var ip = Tools.GetRealIP();
        var isCanSendMsg = LiveBLL.SendCode_Save(1, phonenum, ip, devId, "", "", "", int.Parse(uidx));

        if (isCanSendMsg != 1) { return -2; }//IP，手机号1分钟内是否能发送验证码判断
        if (!VerifySendCode(phonenum)) { return -3; }

        int result = 0;
        string smsCode = RandomHelper.GenerateNum(6);
        string smsContent = MsgInfo(uidx, smsCode, sendType.ToString());
        smsContent = smsContent + "|" + DateTime.Now.ToString("yyyyMMddhhmm");
        //转成 Base64 形式的 System.String  
 
        string Sign = Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(CryptoHelper.ToMD5("7276" + "5b9063cbe1a36319fa6db8bf65d848fa")));

        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("account", "7276");
        dic.Add("extend", "");
        dic.Add("mobile", phonenum);
        dic.Add("sign", Sign);
        dic.Add("text", MsgInfo("0", smsCode, "4"));
        string str = HttpHelper.HttpPost("http://116.62.244.37/yqx/v1/sms/single_send", JsonConvert.SerializeObject(dic));

        Dictionary<string, object> dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(str);
        if (dict != null && dict["code"].ToString() == "0")
        {
            result = 1;

            //设置缓存
            setSendSmsMemCache(phonenum, smsCode);
         
            //发送成功记录一条数据到数据库
            LiveBLL.SendCode_Save(2, phonenum, ip, devId, smsCode, sendType.ToString(), smsContent, int.Parse(uidx));
        }
        else
        {
            LogHelper.WriteLog(LogFile.Log, "【发送短信验证码失败】{0},{1},{2}", phonenum, uidx, str);
        }
        return result;
    }

    /// <summary>
    /// 消息文字
    /// </summary>
    /// <param name="uid"></param>
    /// <param name="ptype"></param>
    /// <param name="smsCode"></param>
    /// <returns></returns>
    private static string MsgInfo(string uidx, string smsCode, string ptype)
    {
        string msg = "";
        switch (ptype)
        {
            case "1":
                msg = "【面具公园】验证码：" + smsCode + "。您可以在5分钟内用该验证码来完成操作注册";
                break;
            case "2":
                msg = "【面具公园】验证码：" + smsCode + "。您可以在5分钟内用该验证码来完成操作绑定手机号";
                break;
            case "3":
                msg = "【面具公园】验证码：" + smsCode + "。您可以在5分钟内用该验证码来完成操作找回密码";
                break;
            case "4":
                msg = "【面具公园】验证码：" + smsCode + "。您可以在5分钟内用该验证码来完成操作";
                break;
            case "5":
                msg = "【面具公园】验证码：" + smsCode + "。您可以在5分钟内用该验证码来完成操作";
                break;
            default:
                msg = "【面具公园】验证码：" + smsCode + "。您可以在5分钟内用该验证码来完成操作";
                break;
        }
        return msg;
    }
    /// <summary>
    /// MySummail短信发送
    /// </summary>
    /// <param name="type">1=国外 需要例子 +8860958730216，0 国内</param>
    /// <param name="MsgInfo"></param>
    /// <returns></returns>
    public static string MySummail(int type, string MsgInfo, string phonenum, string Areacode)
    {
        LogHelper.WriteLog(LogFile.Log, MsgInfo);
        string returnMessage = string.Empty;
        if (type == 1)
        {   //appid appkey 加密方式
            IAppConfig appConfig = new InternationalSmsConfig("60582", "fb32abb048bff1b6bfd2b628d686ad30", SignType.md5);
            InternationalSmsSend MessageSend = new InternationalSmsSend(appConfig);
            MessageSend.AddTo(Areacode + phonenum);
            MessageSend.AddContent(MsgInfo);
            MessageSend.Send(out returnMessage);
        }
        else if (type == 0) //国内
        {
            IAppConfig appConfig = new InternationalSmsConfig("33006", "7174b2acea042aab84498563e92d3976", SignType.md5);
            MessageSend MessageSend = new MessageSend(appConfig);
            MessageSend.AddTo(Areacode + phonenum);
            MessageSend.AddContent(MsgInfo);
            MessageSend.Send(out returnMessage);
        }
        LogHelper.WriteLog(LogFile.Log, MsgInfo+"213");
        return returnMessage;
    }

    /// <summary>
    /// 返回加密消息
    /// </summary>
    /// <param name="str_vnum"></param>
    /// <returns></returns>
    private static string sms_Content(string str_vnum)
    {
        //加密内容
        string sn = "ABCDEF";
        string MK1 = "";
        string MK2 = "";
        Random random1 = new Random();
        Random random2 = new Random();
        int i1 = random1.Next(6);

        int i2 = random2.Next(6);
        if (i1 == 0)
        {
            i1 = 1;
        }
        if (i2 == 0)
        {
            i2 = 1;
        }
        MK1 = sn.Substring(i1 - 1, 1);
        MK2 = sn.Substring(i2 - 1, 1);

        string sms_con_temp1 = str_vnum.Substring(0, i1 - 1) + MK1 + str_vnum.Substring(i1 - 1, (str_vnum.Length - i1 + 1));
        sms_con = sms_con_temp1.Substring(0, 8) + MK2 + sms_con_temp1.Substring(8, (sms_con_temp1.Length - 8));
        return sms_con;
    }

    /// <summary>
    /// 加密字符串
    /// </summary>
    /// <param name="input"></param>
    /// <param name="sKey"></param>
    /// <returns></returns>
    public static string EncryptString_N(string input, string sKey)
    {
        byte[] data = Encoding.UTF8.GetBytes(input);
        using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
        {
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            ICryptoTransform desencrypt = des.CreateEncryptor();
            byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
            return BitConverter.ToString(result);
        }
    }

    #region 发送验证码缓存

    /// <summary>
    /// 发送短信验证码缓存(5分钟)，手机号发送验证码次数(5天)，IP发送次数(1天)
    /// </summary>
    /// <param name="phoneNum">手机号码</param>
    /// <param name="data">发送的验证码</param>
    public static void setSendSmsMemCache(string phoneNum, string data)
    {
        //发送成功之后验证码缓存5分钟(用于验证验证码是否正确)
        string CK_Code = CacheKeys.LIVE_PHONE_VALI_CODE + phoneNum;

        //手机号发送验证码次数缓存5天
        string CK_Num = CacheKeys.LIVE_PHONE_SEND_CODE_NUM + phoneNum;

        //IP缓存1天
        string IPkey = CacheKeys.LIVE_PHONE_SEND_NUM_SAMEIP + Tools.GetRealIP();

        string SenCacheKey = CacheKeys.LIVE_PHONE_VALI_CODE + phoneNum;


        int sendNums = Convert.ToInt32(CacheHelper.GetCache(CK_Num) ?? "0");
        sendNums = sendNums + 1;

        int sendIPNum = Convert.ToInt32(CacheHelper.GetCache(IPkey) ?? "0"); 
        sendIPNum = sendIPNum + 1;
        CacheHelper.SetCache(CK_Code, data, 5);
        CacheHelper.SetCache(CK_Num, sendNums, 300);
        CacheHelper.SetCache(IPkey, sendIPNum, 1000);
        //MemcachedHelper.Store(CK_Code, data, new TimeSpan(0, 5, 0));
        //MemcachedHelper.Store(CK_Num, sendNums, new TimeSpan(5, 0, 0, 0));
        //MemcachedHelper.Store(IPkey, sendIPNum, new TimeSpan(1, 0, 0, 0));
        //setMemCacheIP();
    }

    public  static void setUpPwdToken(string token ,string useridx ){
        string CK_Token = CacheKeys.LIVE_UpPwdToken + useridx;
        CacheHelper.SetCache(CK_Token, token, 5);
        //MemcachedHelper.Store(CK_Token, token, new TimeSpan(0, 5, 0));
    }

    public static string getUpPwdToken(string useridx)
    {
        string CK_Token = CacheKeys.LIVE_UpPwdToken + useridx;
        if (CacheHelper.GetCache(CK_Token) == null)
        {
            return "";
        }
        else {
            return CacheHelper.GetCache(CK_Token)as string;
        }
    }
    /// <summary>
    /// 发送短信验证码IP缓存一天
    /// </summary>
    /// <param name="data"></param>
    //public static void setMemCacheIP()
    //{
    //    //IP缓存1天
    //    string IPkey = CacheKeys.LIVE_PHONE_SEND_NUM_SAMEIP + Tools.GetRealIP();

    //    int sendIPNum = MemcachedHelper.Get<int>(IPkey);
    //    sendIPNum = sendIPNum + 1;

    //    MemcachedHelper.Store(IPkey, sendIPNum, new TimeSpan(1, 0, 0, 0));
    //}

    /// <summary>
    /// 验证手机 是否可以继续发送验证码
    /// </summary>
    /// <param name="phoneNum"></param>
    /// <returns></returns>
    public static bool VerifySendCode(string phoneNum, int type = 1)
    {
        //手机号限制
        string CK_PhoneNum = CacheKeys.LIVE_PHONE_SEND_CODE_NUM + phoneNum;
        //ip限制
        string CK_IP = CacheKeys.LIVE_PHONE_SEND_NUM_SAMEIP + Tools.GetRealIP();

        int phoneNums = Convert.ToInt32(CacheHelper.GetCache(CK_PhoneNum)??"0");
        int ipSendNums = Convert.ToInt32(CacheHelper.GetCache(CK_IP) ?? "0"); 

        //公司IP除外
        if (Tools.IsCompanyIP)
        {
            return true;
        }

        if (phoneNums >= 5 || ipSendNums >= 10)
        {
            LogHelper.WriteLog(LogFile.Log, "【手机号或IP发送已受限】" + phoneNum + "|IP次数:" + ipSendNums + "|手机号次数：" + phoneNums);
            return false;
        }

        return true;
    }

    /// <summary>
    /// 验证 手机验证码
    /// </summary>
    /// <param name="phonenum"></param>
    /// <param name="code"></param>
    /// <returns>1:成功 -2:验证码失效 -3:验证码不匹配 </returns>
    public static int valiPhoneCode(string phonenum, string code)
    {
        int ret = 0;
        string cacheKey = CacheKeys.LIVE_PHONE_VALI_CODE + phonenum;
        string sCode = CacheHelper.GetCache(cacheKey) as string;
       // string sCode = (string)MemcachedHelper.Get(cacheKey);
        if (string.IsNullOrEmpty(sCode))
        {
            return -2;
        }

        if (code.ToLower().Equals(sCode.ToLower()))
        {
            ret = 1;
        }
        else
        {
            ret = -3;
        }
        return ret;
    }

    #endregion
}
