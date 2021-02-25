using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class MobileResultBLL
    {
        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="mr"></param>
        /// <param name="flag"></param>
        public static void SendCodeMessage(MobileResult mr, int flag)
        {
            switch (flag)
            {
                case 1:
                    mr.code = "100";
                    mr.msg = "发送验证码成功";
                    break;
                case 0:
                    mr.code = "115";
                    mr.msg = "发送验证码失败";
                    break;
                case -4:
                    mr.code = "114";
                    mr.msg = "验证码1分钟内只能发一次";
                    break;
                case -2:
                    mr.code = "116";
                    mr.msg = "手机号发送已受限";
                    break;
            }
        }
        public static void ValiCodeMessage(MobileResult mr, int ret)
        {
            switch (ret)
            {
                case 1:
                    mr.code = "100";
                    mr.msg = "匹配成功";
                    break;
                case -2:
                    mr.code = "114";
                    mr.msg = "验证码失效";
                    break;
                case -3:
                    mr.code = "115";
                    mr.msg = "验证码错误";
                    break;
            }
        }

        /// <summary>
        /// 获取注册提示内容
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetRegisterMessageByRet(string code)
        {
            string msg = string.Empty;
            switch (code)
            {
                case "0":
                    //msg = "用户名已存在或不合法";
                    msg = "该手机号已注册过账号，请勿重复注册";
                    break;
                case "-1":
                    msg = "IP被封";
                    break;
                case "-2":
                    msg = "昵称不合法";
                    break;
                case "-3":
                    msg = "IP被封24小时";
                    break;
                case "-4":
                    msg = "IP被封10天";
                    break;
                case "-5":
                    msg = "注册过于频繁,稍后再试"; //五分钟内相同IP的注册用户
                    break;
                case "-6":
                    msg = "当天注册数量超过限制";//24小时内相同IP的注册用户已超过10个  
                    break;
                case "-10":
                    msg = "违法注册"; //未知的注册来源
                    break;
                case "-11":
                    msg = "密码不符合规则"; //不是有效注册的IP  
                    break;
                case "10":
                    msg = "注册成功";
                    break;
                default:
                    msg = "注册失败";
                    break;
            }
            return msg;
        }
    }
}
