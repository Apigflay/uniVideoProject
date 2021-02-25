using System;
using System.Configuration;
using System.Web;

namespace Common
{
    /// <summary>
    /// 配置文件读取类
    /// </summary>
    public sealed class ConfigHelper
    {
        public const string DEFAULT_KEY = "q0m3sd8l";//数据库连接字符串加密秘钥

        /// <summary>
        ///根据Key获取AppSetting里面的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppSettings(string key)
        {
            string cacheKey = "AppSettings-" + key;
            object cache = GetCache(cacheKey);
            if (cache == null)
            {
                cache = ConfigurationManager.AppSettings[key];

                if (cache != null)
                    SetCache(cacheKey, cache, 30);
                else
                    cache = string.Empty;
            }
            return cache.ToString();
        }

        /// <summary>
        /// 获取数据库连接字符串 读取connectionStrings里面的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetDbString(string key)
        {
            string cacheKey = "DbString-" + key;
            string isEncrypt = ConfigHelper.GetAppSettings("EncryptConns");

            object cache = GetCache(cacheKey);

            if (cache == null)
            {
                cache = ConfigurationManager.ConnectionStrings[key];
                if (cache != null)
                {
                    if (!string.IsNullOrEmpty(isEncrypt) && isEncrypt.Equals("true"))
                    {
                        cache = CryptoHelper.DESDecrypt(cache.ToString(), DEFAULT_KEY);
                    }
                    SetCache(cacheKey, cache, 180);
                }
                else
                    cache = string.Empty;
            }
            return cache.ToString();
        }
        public static string GetDbString(string key, bool isEncrypt)
        {
            string cacheKey = "DbString-" + key;

            object cache = GetCache(cacheKey);

            if (cache == null)
            {
                cache = ConfigurationManager.ConnectionStrings[key];
                if (cache != null)
                {
                    if (isEncrypt)
                    {
                        cache = CryptoHelper.DESDecrypt(cache.ToString(), DEFAULT_KEY);
                    }
                    SetCache(cacheKey, cache, 180);
                }
                else
                    cache = string.Empty;
            }
            return cache.ToString();
        }
        #region 获取配置文件 无用
        /// <summary>
        /// 获取布尔值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetConfigBool(string key)
        {
            bool flag = false;
            string configString = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrEmpty(configString))
            {
                flag = false;
            }
            else if (configString.Equals("true") || configString.Equals("false"))
            {
                flag = bool.Parse(configString);
            }
            return flag;
        }

        /// <summary>
        /// 获取数值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        //public static decimal GetConfigDecimal(string key)
        //{
        //    decimal num = 0M;
        //    string configString = GetConfigString(key);
        //    if ((configString != null) && (string.Empty != configString))
        //    {
        //        try
        //        {
        //            num = decimal.Parse(configString);
        //        }
        //        catch (FormatException)
        //        {
        //        }
        //    }
        //    return num;
        //}

        /// <summary>
        /// 获取整数值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        //public static int GetConfigInt(string key)
        //{
        //    int num = 0;
        //    string configString = GetConfigString(key);
        //    if ((configString != null) && (string.Empty != configString))
        //    {
        //        try
        //        {
        //            num = int.Parse(configString);
        //        }
        //        catch (FormatException)
        //        {
        //        }
        //    }
        //    return num;
        //}

        /// <summary>
        /// 获取数据库连接字符串  读取AppSettings里面的值
        /// </summary>
        /// <param name="key">key值</param>
        /// <returns></returns>
        //public static string GetDbStringByKey(string key)
        //{
        //    string cacheKey = "DbString-" + key;
        //    object cache = CacheHelper.GetCache(cacheKey);
        //    if (cache == null)
        //    {
        //        cache = ConfigurationManager.AppSettings[key];
        //        if (cache != null)
        //        {
        //            if (ConfigHelper.GetConfigString("EncryptConns") == "true")
        //            {
        //                cache = CryptoHelper.DESDecrypt(cache.ToString(), CryptoHelper.DEFAULT_KEY);
        //            }
        //            CacheHelper.SetCache(cacheKey, cache, DateTime.Now.AddMinutes(180.0), TimeSpan.Zero);
        //        }
        //        else
        //        {
        //            cache = string.Empty;
        //        }

        //    }
        //    return cache.ToString();
        //}

        #endregion

        #region 缓存
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="CacheKey">key</param>
        /// <param name="objObject">值</param>
        /// <param name="Timeout">过期时间（分钟）</param>
        public static void SetCache(string CacheKey, object objObject, int Timeout)
        {
            HttpRuntime.Cache.Insert(CacheKey, objObject, null, DateTime.Now.AddMinutes(Timeout), System.Web.Caching.Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static object GetCache(string CacheKey)
        {
            return HttpRuntime.Cache[CacheKey];
        }
        #endregion

    }
}

