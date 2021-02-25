/******************************************
* 文 件 名：HttpHelper
* Copyright(c) live.9158.com
* 创 建 人：zhaorui
* 创建日期：2015年4月
* 文件描述：网络请求帮助类
******************************************
* 修 改 人：
* 修改日期：
* 备注描述：
*****************************************/
using System;
using System.Text;
using System.Net;
using System.IO;
using System.Web;

namespace Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class HttpHelper
    {
        public static string HttpGet(string url)
        {
            return HttpGet(url, "utf-8");
        }
        public static bool GetClientType()
        {
            bool result = false;
            string userAgent = HttpContext.Current.Request.UserAgent;
            if (userAgent != null && userAgent.ToLower().Contains("livelaowo"))
            {
                result = true;
            }
            return result;
        }

        public static string HttpGet(string url, string encoding)
        {
            if (url.IndexOf('?') > -1)
            {
                url += "&_=" + DateTime.Now.Ticks.ToString();
            }
            else
            {
                url += "?_=" + DateTime.Now.Ticks.ToString();
            }
            HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
            httpWebRequest.Method = "Get";
            httpWebRequest.ServicePoint.Expect100Continue = false;
            httpWebRequest.Timeout = 7000;
            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36";
            return GetHttpWebResponse(httpWebRequest, encoding);
        }
        public static string HttpPost(string url, string data)
        {
            return HttpPost(url, "utf-8", data);
        }
        public static string HttpPost(string url, string encoding, string data)
        {
            if (url.IndexOf('?') > -1)
            {
                url += "&_=" + DateTime.Now.Ticks.ToString();
            }
            else
            {
                url += "?_=" + DateTime.Now.Ticks.ToString();
            }
            HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
            httpWebRequest.Method = "POST";
            httpWebRequest.ServicePoint.Expect100Continue = false;
            httpWebRequest.Timeout = 3000;
            httpWebRequest.ReadWriteTimeout = 2000;
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";

            StreamWriter requestWriter = new StreamWriter(httpWebRequest.GetRequestStream());
            try
            {
                requestWriter.Write(data);
            }
            finally
            {
                requestWriter.Close();
            }
            return GetHttpWebResponse(httpWebRequest, encoding);

        }

        private static string GetHttpWebResponse(WebRequest webRequest, string encoding)
        {
            WebResponse httpResponse = null;
            StreamReader responseReader = null;
            string responseData = String.Empty;
            try
            {
                httpResponse = webRequest.GetResponse();
                responseReader = new StreamReader(httpResponse.GetResponseStream(), Encoding.GetEncoding(encoding));
                responseData = responseReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogFile.Error, string.Format("【Http请求出错】Method:{3},URL:{0},data:{1},Msg:{2}", webRequest.RequestUri, responseData, ex.Message, webRequest.Method));
            }
            finally
            {
                if (httpResponse != null)
                {
                    httpResponse.Close();
                    responseReader.Close();
                }

            }
            return responseData;
        }
        /// <summary>
        /// 模拟请求 SendCode.cs使用
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string httpResponse(string url)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            Stream stream = webResponse.GetResponseStream();
            StreamReader sr = new StreamReader(stream, Encoding.Default);
            string str = sr.ReadToEnd().Replace(" ", "").Trim();

            webResponse.Close();
            sr.Close();
            sr.Dispose();
            stream.Close();
            stream.Dispose();
            webRequest.Abort();//连接清除
            return str;
        }

    }
}
