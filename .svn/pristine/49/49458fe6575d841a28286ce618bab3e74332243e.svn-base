using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Web;

namespace OAuth.Share
{
    public class ShareInfo
    {
        public static string AppID = "wxc75938df28a5b317";
        public static string AppSecret = "8bfa20276f836a1304e92a433664010e";

        public static string TicketUrl = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi";
        public static string AstokenUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";

        /// <summary>
        /// 获取access_token
        /// {"access_token":"Hnj_RIjco0y-cfOODCXH_bvGIH8A_OQTpuKovLKObTy3SC06x2Z4PuVVxzB8GRX9Q8VWmxzWFfh7nm0XMqU8Pr5LrsYFgnvjfB9ax4vYDLqGX6oaZD2-Tm_s_Vl54rsxCDFgAHAPQS","expires_in":7200}
        /// </summary>
        /// <returns></returns>
        public ShareModel Get_AccessToken()
        {
            string url = string.Format(AstokenUrl, AppID, AppSecret);
            string data = HttpHelper.HttpGet(url);

            return JsonConvert.DeserializeObject<ShareModel>(data);
        }

        /// <summary>
        /// 获取ticket
        /// {"errcode":0,"errmsg":"ok","ticket":"bxLdikRXVbTPdHSM05e5u-ta-XnpGSHFrHNsty1PbbYlmSM0Gys07vvOf0Ijq4KLbmm0uMAtmfrwft1PQ7cczQ","expires_in":7200}
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public ShareModel Get_Ticket(string access_token)
        {
            string url = string.Format(TicketUrl, access_token);
            string data = HttpHelper.HttpGet(url);

            return JsonConvert.DeserializeObject<ShareModel>(data);
        }

        /// <summary>
        /// 获取分享签名
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="nonceStr"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public string Get_Sign(string ticket, string nonceStr, string timestamp)
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            string sign = string.Format("jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}", ticket, nonceStr, timestamp, url);

            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sign, "SHA1").ToLower();
        }
    }

    public class ShareModel
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }

        //
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public string ticket { get; set; }
    }
}
