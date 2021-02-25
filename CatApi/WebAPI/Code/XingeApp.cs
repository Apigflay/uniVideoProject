using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net.Security;

//using XinGePushSDK.NET.Res;
//using XinGePushSDK.NET.Utility;
using Common;
using Model;

namespace WebAPI
{
    public class XinGeConfig
    {
        public const String RESTAPI_PUSHSINGLEDEVICE = "http://openapi.xg.qq.com/v2/push/single_device";
        public const String RESTAPI_PUSHSINGLEACCOUNT = "http://openapi.xg.qq.com/v2/push/single_account";
        public const String RESTAPI_PUSHACCOUNTLIST = "http://openapi.xg.qq.com/v2/push/account_list";
        public const String RESTAPI_PUSHALLDEVICE = "http://openapi.xg.qq.com/v2/push/all_device";
        public const String RESTAPI_PUSHTAGS = "http://openapi.xg.qq.com/v2/push/tags_device";
        public const String RESTAPI_QUERYPUSHSTATUS = "http://openapi.xg.qq.com/v2/push/get_msg_status";
        public const String RESTAPI_QUERYDEVICECOUNT = "http://openapi.xg.qq.com/v2/application/get_app_device_num";
        public const String RESTAPI_QUERYTAGS = "http://openapi.xg.qq.com/v2/tags/query_app_tags";
        public const String RESTAPI_CANCELTIMINGPUSH = "http://openapi.xg.qq.com/v2/push/cancel_timing_task";
        public const String RESTAPI_BATCHSETTAG = "http://openapi.xg.qq.com/v2/tags/batch_set";
        public const String RESTAPI_BATCHDELTAG = "http://openapi.xg.qq.com/v2/tags/batch_del";
        public const String RESTAPI_QUERYTOKENTAGS = "http://openapi.xg.qq.com/v2/tags/query_token_tags";
        public const String RESTAPI_QUERYTAGTOKENNUM = "http://openapi.xg.qq.com/v2/tags/query_tag_token_num";

        public const String HTTP_POST = "POST";
        public const String HTTP_GET = "GET";

        public const int DEVICE_ALL = 0;
        public const int DEVICE_BROWSER = 1;
        public const int DEVICE_PC = 2;
        public const int DEVICE_ANDROID = 3;
        public const int DEVICE_IOS = 4;
        public const int DEVICE_WINPHONE = 5;

        /// <summary>
        /// IOS生产环境
        /// </summary>
        public const uint IOSENV_PROD = 1;
        /// <summary>
        /// IOS开发环境
        /// </summary>
        public const uint IOSENV_DEV = 2;

        /// <summary>
        /// android 通知消息
        /// </summary>
        public const uint message_type_info = 1;

        /// <summary>
        /// android 透传消息
        /// </summary>
        public const uint message_type_touchuan = 2;
    }

    public class SignUtility
    {
        /// <summary>
        /// 计算参数签名
        /// </summary>
        /// <param name="params">请求参数集，所有参数必须已转换为字符串类型</param>
        /// <param name="secret">签名密钥</param>
        /// <returns>签名</returns>
        public static string GetSignature(IDictionary<string, string> parameters, string secret, string url)
        {
            // 先将参数以其参数名的字典序升序进行排序
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters);
            IEnumerator<KeyValuePair<string, string>> iterator = sortedParams.GetEnumerator();

            // 遍历排序后的字典，将所有参数按"key=value"格式拼接在一起
            StringBuilder basestring = new StringBuilder();
            basestring.Append("POST").Append(url.Replace("http://",""));
            while (iterator.MoveNext())
            {
                string key = iterator.Current.Key;
                string value = iterator.Current.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
                    basestring.Append(key).Append("=").Append(value);
                }
            }
            basestring.Append(secret);
            
            //LogHelper.WriteLog(LogFile.Test, basestring.ToString());

            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(basestring.ToString(), "md5").ToLower();
            /* 使用MD5对待签名串求签
            MD5 md5 = MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(basestring.ToString()));

            // 将MD5输出的二进制结果转换为小写的十六进制
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                string hex = bytes[i].ToString("x2");
                result.Append(hex);
            }
            return result.ToString();
            */
        }
    }

    public class XingeApp
    {
        private string accessId;
        private string secretKey;
        public uint valid_time;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="accessId">accessId</param>
        /// <param name="secretKey">secretKey</param>
        /// <param name="valid_time">配合timestamp确定请求的有效期，单位为秒，
        /// 最大值为600。若不设置此参数或参数值非法，则按默认值600秒计算有效期</param>
        public XingeApp(string accessId, string secretKey, uint valid_time = 600)
        {
            if (string.IsNullOrEmpty(accessId))
            {
                throw new ArgumentNullException("accessId");
            }
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentNullException("secretKey");
            }
            this.valid_time = valid_time;
            this.accessId = accessId;
            this.secretKey = secretKey;
        }

        /// <summary>
        /// 发起推送请求到信鸽并获得相应
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="parameters">字段</param>
        /// <returns>返回值json反序列化后的类</returns>
        private Result CallRestful(String url, IDictionary<string, string> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            try
            {
                parameters.Add("access_id", accessId);
                parameters.Add("timestamp", ((int)(DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalSeconds).ToString());
                parameters.Add("valid_time", valid_time.ToString());
                string md5sing = SignUtility.GetSignature(parameters, this.secretKey, url);
                parameters.Add("sign", md5sing);
                /*
                var res = HttpWebResponseUtility.CreatePostHttpResponse(url, parameters, null, null, Encoding.UTF8, null);
                var resstr = res.GetResponseStream();
                System.IO.StreamReader sr = new System.IO.StreamReader(resstr);
                var resstring = sr.ReadToEnd();
                */
                StringBuilder datastr = new StringBuilder();
                foreach (KeyValuePair<string, string> kv in parameters)
                { 
                    if(datastr.Length>0)
                        datastr.Append("&");
                    datastr.Append(kv.Key + "=" +kv.Value);
                }
                //LogHelper.WriteLog(LogFile.Test, datastr.ToString());
                string retstring = HttpHelper.HttpPost(url, "utf-8", datastr.ToString());
                //LogHelper.WriteLog(LogFile.Test, retstring);
                dynamic dc = JsonHelper.DynamicConvertJson(retstring);
                return new Result(dc.ret_code.ToString(), dc.ret_code.ToString() == "0" ? "ok" : dc.err_msg.ToString()); 
                //return JsonConvert.DeserializeObject<Result>(retstring);
            }
            catch (Exception e)
            {
                return new Result ("-1", e.Message);
            }
        }

        /// <summary>
        /// 推送到 单个设备 安卓
        /// </summary>
        /// <param name="DeviceToken"></param>
        /// <param name="msg"></param>
        /// <returns>返回值json反序列化后的类</returns>
        public Result PushToSingleDevice(string DeviceToken, Msg_Android msg)
        {
            if (string.IsNullOrEmpty(DeviceToken))
            {
                throw new ArgumentNullException("DeviceToken");
            }
            if (msg == null)
            {
                throw new ArgumentNullException("msg");
            }
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("device_token", DeviceToken);
            parameters.Add("message_type", "1");
            parameters.Add("message", JsonConvert.SerializeObject(msg)); //JsonHelper.Serialize(msg)
            /*
            if (msg.expire_time.HasValue)
            {
                parameters.Add("expire_time", msg.expire_time.Value.ToString());
            }
            parameters.Add("send_time", msg.send_time);
            */
            parameters.Add("multi_pkg", "1");
            //parameters.Add("environment", "0");

            //parameters.Add("access_id", accessId);
            //parameters.Add("timestamp", ((int)(DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalSeconds).ToString());
            //parameters.Add("valid_time", valid_time.ToString());
            //string md5sing = SignUtility.GetSignature(parameters, this.secretKey, XinGeConfig.RESTAPI_PUSHSINGLEDEVICE);
            //parameters.Add("sign", md5sing);

            return CallRestful(XinGeConfig.RESTAPI_PUSHSINGLEDEVICE, parameters);
        }



        /// <summary>
        /// 推送批量消息 安卓
        /// </summary>
        /// <param name="msg"></param>
        /// <returns>返回值json反序列化后的类</returns>
        public Result PushToMultiDevice(Msg_Android msg)
        {
            if (msg == null)
            {
                throw new ArgumentNullException("msg");
            }
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("message_type", "1");
            parameters.Add("message", JsonConvert.SerializeObject(msg)); //JsonHelper.Serialize(msg)
            /*
            if (msg.expire_time.HasValue)
            {
                parameters.Add("expire_time", msg.expire_time.Value.ToString());
            }
            parameters.Add("send_time", msg.send_time);
            */
            parameters.Add("multi_pkg", "1");

            return CallRestful(XinGeConfig.RESTAPI_PUSHALLDEVICE, parameters);
        }
    }
}