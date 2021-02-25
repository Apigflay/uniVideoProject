using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace ThirdAPI
{
    public class TDRCHelper
    {
        #region 测试环境
        //private const string partner_code = "mmzb";
        //private const string partner_key = "3f9bd1affb6943089c8a2163a13a1480";
        //private const string app_name = "mmzb_web";
        //private const string secret_key = "3d9b90b3b74346a2818e3246c3797058";
        //private const string event_id = "Register_web_20170627";
        //private const string api_url = "https://api.tongdun.cn/riskService/v1.1";//正式uRL
        #endregion
        
        #region 正式环境
        
        /// <summary>
        /// 合作方标识
        /// </summary>
        private const string partner_code = "mmzb";
        
        /// <summary>
        /// 合作方密钥
        /// </summary>
        private const string partner_key = "fcbef3ef980642e28bc38ff944b3df5f";

        /// <summary>
        /// 应用名称
        /// </summary>
        private const string app_name = "mmzb_web";

        /// <summary>
        /// 应用密钥
        /// </summary>
        private const string secret_key = "86173e7318ee4222a28907af44dac2e3";

        /// <summary>
        /// 事件标识
        /// </summary>
        private const string event_id = "Register_web_20170628";

        /// <summary>
        /// api 请求地址
        /// </summary>
        private const string api_url = "https://api.tongdun.cn/riskService/v1.1";

        #endregion

        /// <summary>
        /// 返回结果
        /// </summary>
        /// <param name="inviterName"></param>
        /// <param name="channel"></param>
        /// <param name="userip"></param>
        /// <param name="inviteridx"></param>
        /// <param name="deviceid"></param>
        /// <param name="invitecode"></param>
        /// <returns></returns>
        public static string Get_Register_RiskResult(string inviterName, string channel, string userip, int inviteridx, string deviceid, string invitecode)
        {
            string param = string.Format("partner_code={0}&secret_key={1}&event_id={2}&account_name={3}&ext_account_source={4}&account_login={5}&rem_code={6}&ip_address={7}"
                , partner_code, secret_key, event_id, inviterName, channel, inviteridx, invitecode, userip);

            string result = HttpHelper.HttpPost(api_url, param);

            return result;
        }

    }

    public class TDRCResultModel
    {
        public Boolean success { get; set; }
        public string reason_code { get; set; }
        public string seq_id { get; set; }
        public int spend_time { get; set; }
        /// <summary>
        /// 风险决策结果 有3种值
        /// Accept、Review、Reject
        /// </summary>
        public string final_decision { get; set; }
        /// <summary>
        /// 风险决策分数，范围在0~65536
        /// </summary>
        public int final_score { get; set; }
        public string risk_type { get; set; }
        public string policy_set_name { get; set; }
        public string policy_name { get; set; }

        /// <summary>
        /// 自己业务参数
        /// </summary>
        public int useridx { get; set; }
        public string userip { get; set; }
        public string channel { get; set; }
        public string strResult { get; set; }
        public int version { get; set; }
    }
}
