using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Net;



namespace WebAPI.AliPay
{
    /// <summary>
    /// 类名：Config
    /// 功能：基础配置类
    /// 详细：设置帐户有关信息及返回路径
    /// 版本：3.3
    /// 日期：2012-07-05
    /// 说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    /// 
    /// 如何获取安全校验码和合作身份者ID
    /// 1.用您的签约支付宝账号登录支付宝网站(www.alipay.com)
    /// 2.点击“商家服务”(https://b.alipay.com/order/myOrder.htm)
    /// 3.点击“查询合作者身份(PID)”、“查询安全校验码(Key)”
    /// </summary>
    public class Config
    {
        #region 字段
        private static string partner = "";
        private static string private_key = "";
        private static string public_key = "";
        private static string input_charset = "";
        private static string sign_type = "";
        #endregion

        static Config()
        {
            //↓↓↓↓↓↓↓↓↓↓请在这里配置您的基本信息↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

            //合作身份者ID，以2088开头由16位纯数字组成的字符串
            partner = "2088801677358289";

            //商户的私钥
            private_key = @"
MIICWwIBAAKBgQDD0uZmLdydB+U0WGJ+DQW2V3ssik1e6PJJ9BL3ZY/atKKK+VKI
s7iJoaeG0rWX/6T0F7bdtY7OpyGqsZCxwdmRf4MFg7JUIeXyMHdH6RfdqB+89Gnh
wBbB5AzyZekgie13xTQzfQhQd1y0KT9reP1wiA4htpJwO/J35T76+5BywwIDAQAB
AoGAZ1GVAoBcD/YkdpPTk5InW2eYs41c+SCLBX3jq+hIGCqKQCz/4OSCDwvdqgLu
kP3u+GKytxOd/2arGraJE2Cl+3JufwPXi1RdoajBeD4pBsfwOWn2P00Jig+O1f+p
fNss/fkyKdGNURuS7k7IgHecoheh+RoiV3etdORE97KDx/ECQQD9OmwZBMwB3qRv
M0V6ysEFhzFmLRxPEAaQOf6jGMa95HWJFMhKWLMOFLomvH7TIthVZCSpiBeSPRDH
GQCjrLH3AkEAxfefku8ptzd8YTBWmOCGPz9hrgQUj6fttTfwnkJJn2wp0+E04zzL
tNs4JHzb6j1f+S6vLfm8WB1YLe8LNmKSlQJABJIsZ1s1z4bUUwmK5IeKam4hTyXb
T/YLGEljtk/5Lm5UTNAqOWVWfheTsKVQaMFFRG1VWBYTztj5V460+z9fywJAfxe8
MH6uT1ul1FdOIRNz3EL6mNcxxBBRQVhKd6+iyGCOceJRK9mqBc//OR2XqcgBR147
RM7hpcghBkxC7IVYCQJAfsp7FTJxOrwKTv8FojHp5KlpxHte/q4GnK6tciApqbyP
FPWsLKVHnOeF1736dfTayqPh4Kj7liB2uZKZmBzE6A==
";

            //支付宝的公钥，无需修改该值
            public_key = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCnxj/9qwVfgoUh/y2W89L6BkRAFljhNhgPdyPuBV64bfQNN1PjbCzkIM6qRdKBoLPXmKKMiFYnkd6rAoprih3/PrQEB/VsW8OoM8fxn67UDYuyBTqA23MML9q1+ilIZwBC2AQ2UBVOrFXfFl75p6/B5KsiNG9zpgmLCUYuLkxpLQIDAQAB";

            //↑↑↑↑↑↑↑↑↑↑请在这里配置您的基本信息↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑



            //字符编码格式 目前支持 gbk 或 utf-8
            input_charset = "utf-8";

            //签名方式，选择项：RSA、DSA、MD5
            sign_type = "RSA";
        }

        #region 属性
        /// <summary>
        /// 获取或设置合作者身份ID
        /// </summary>
        public static string Partner
        {
            get { return partner; }
            set { partner = value; }
        }

        /// <summary>
        /// 获取或设置商户的私钥
        /// </summary>
        public static string Private_key
        {
            get { return private_key; }
            set { private_key = value; }
        }

        /// <summary>
        /// 获取或设置支付宝的公钥
        /// </summary>
        public static string Public_key
        {
            get { return public_key; }
            set { public_key = value; }
        }

        /// <summary>
        /// 获取字符编码格式
        /// </summary>
        public static string Input_charset
        {
            get { return input_charset; }
        }

        /// <summary>
        /// 获取签名方式
        /// </summary>
        public static string Sign_type
        {
            get { return sign_type; }
        }
        #endregion
    }
}