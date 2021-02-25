using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace OAuth.Huawei
{
    public class HuaweiLogin
    {
        /// <summary>
        /// 根据access_token获取华为唯一id(openid)
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public string GetOpenId(string access_token)
        {
            string accesstoken = HttpUtility.UrlEncode(access_token, Encoding.UTF8);

            string api = "https://api.cloud.huawei.com/rest.php";
            string timestamp = TimeHelper.GetTimeStamp();

            string url = String.Format("{0}?access_token={1}&nsp_ts={2}&nsp_svc=huawei.oauth2.user.getTokenInfo&open_id=OPENID"
                , api, accesstoken, timestamp);

            string result = HttpHelper.HttpGet(url);

            //LogHelper.WriteLog(LogFile.Debug, "token:{0},url:{1},result:{2}", accesstoken, url, result);

            return JsonConvert.DeserializeAnonymousType(result, new HuaweiOpenid()).open_id;
        }
    }

    public class HuaweiOpenid
    {
        public string scope { get; set; }
        public string open_id { get; set; }
        public string client_id { get; set; }
    }
}
