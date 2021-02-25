using Common;
using Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BLL
{
    public class CacheBLL
    {
        /// <summary>
        /// 搜索设备受限
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        //public static bool GetSearchMemCacheNum(string deviceId, string content)
        //{
        //    if (!Tools.IsMobile())
        //    {
        //        return false;
        //    }
        //    string ip = Tools.GetRealIP();
        //    string key = CacheKeys.LIVE_SEARCH_NUM_SAME_DeviceId + deviceId;
        //    string num = (string)MemcachedHelper.Get(key);
        //    string version = AppDataBLL.AppVersion;

        //    if (deviceId == "000000000000000" || deviceId == "00000000-0000-0000-0000-000000000000" || ip == "115.231.93.68" || ip == "115.231.93.69")
        //    {
        //        return true;
        //    }
        //    if (!string.IsNullOrEmpty(num) && int.Parse(num) == 50)
        //    {
        //        LogHelper.WriteLog(LogFile.Warning, "【搜索设备受限】version:" + version + "|" + deviceId + "|" + content + "|" + num);
        //        return false;
        //    }
        //    return true;
        //}
        
        /// <summary>
        /// 搜索设备缓存三小时
        /// </summary>
        /// <param name="data"></param>
        //public static void setMemCacheSearch(string deviceId)
        //{
        //    string key = CacheKeys.LIVE_SEARCH_NUM_SAME_DeviceId + deviceId;
        //    string num = (string)MemcachedHelper.Get(key);

        //    if (!string.IsNullOrEmpty(num))
        //        num = (int.Parse(num) + 1).ToString();
        //    else
        //        num = "1";

        //    MemcachedHelper.Set(key, num, DateTime.Now.AddHours(3));
        //}

        /// <summary>
        /// 用户注册IP判断 (同一IP下15分钟内只能注册5个用户)
        /// </summary>
        /// <returns></returns>
        public static bool VerifyRegisterIP()
        {
            bool flag = true;
            string cacheKey = CacheKeys.LIVE_PHONE_Regiseter_NUM_SAMEIP + Tools.GetRealIP();
            int num = MemcachedHelper.Get<int>(cacheKey);

            if (num > 10) { flag = false; }

            num = num + 1;

            MemcachedHelper.Store(cacheKey, num, new TimeSpan(0, 15, 0));
            return flag;
        }

        /// <summary>
        /// 修改图像次数限制
        /// </summary>
        /// <param name="type">1:add 2:get</param>
        /// <param name="useridx"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static bool VerifyUpdatePhoto(int type, int useridx, ref int count)
        {
            bool flag = true;
            string CK = CacheKeys.LIVE_UPLOAD_Photo + useridx;

            int number = MemcachedHelper.Get<int>(CK);

            if (type == 1)
            {
                number = number + 1;
                count = number;
                MemcachedHelper.Store(CK, number, new TimeSpan(1, 0, 0, 0));
            }
            else
            {
                count = number;
                if (number >= 20) { flag = false; }
            }
            return flag;
        }

        /// <summary>
        /// 一定时间内加关注验证
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static bool Verifyfollow(int useridx, int type = 1)
        {
            bool flag = true;
            string CK = CacheKeys.LIVE_Follow + useridx;
            int number = 0;
            if (type == 2)
            {
                CK = CacheKeys.LIVE_FollowByIP + Tools.GetRealIP();
            }
            number = MemcachedHelper.Get<int>(CK);

            if (number > 45)
            {
                flag = false;
            }
            number = number + 1;
            //count = number;

            MemcachedHelper.Store(CK, number, new TimeSpan(1, 0, 0));

            return flag;
        }

        /// <summary>
        /// 同一个IP12小时内只允许关注100个
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static bool VerifyfollowByIP(int useridx)
        {
            bool flag = true;

            if (Tools.IsCompanyIP) return true;

            string userip = Tools.GetRealIP();
            string cachekey = CacheKeys.LIVE_FollowByIP + userip;
            int number = MemcachedHelper.Get<int>(cachekey);

            if (number > 100)
            {
                flag = false;
            }
            number = number + 1;


            MemcachedHelper.Store(cachekey, number, new TimeSpan(12, 0, 0));
            return flag;
        }
    }
}
