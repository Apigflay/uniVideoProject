using Enyim.Caching;
using Enyim.Caching.Memcached;
using System;

namespace Common.Memcache
{
    public class MemcachedHelper
    {
        private static MemcachedClient MemClient;
        static readonly object padlock = new object();

        //线程安全的单例模式
        public static MemcachedClient getInstance()
        {
            if (MemClient == null)
            {
                lock (padlock)
                {
                    if (MemClient == null)
                    {
                        MemClientInit();
                    }
                }
            }
            return MemClient;
        }
        //初始化缓存（注意：enyim.com/memcached在web.config里面的位置）
        static void MemClientInit()
        {
            try
            {
                MemClient = new MemcachedClient("enyim.com/memcached");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //构造函数
        static MemcachedHelper()
        {
            getInstance();
        }

        #region Set

        public static void Store(string Key, object Value, DateTime ExpiredAt)
        {
            MemClient.Store(StoreMode.Set, Key, Value, ExpiredAt);
        }
        public static void Store(string Key, object Value, TimeSpan ExpiredAt)
        {
            MemClient.Store(StoreMode.Set, Key, Value, ExpiredAt);
        }
        public static void Set(string Key, object Value, DateTime ExpiredAt)
        {
            MemClient.Store(StoreMode.Set, Key, Value, ExpiredAt);
        }
        public static void Set(string Key, object Value, int ExpiredAt)
        {
            TimeSpan span = new TimeSpan(0, ExpiredAt, ExpiredAt);
            int i = 0;
            while (!MemClient.Store(StoreMode.Set, Key, Value, span) && i < 3)
            {
                i++;
            }
        }

        #endregion

        #region Get

        public static T Get<T>(string Key)
        {
            return MemClient.Get<T>(Key);
        }
        public static object Get(string Key)
        {
            return MemClient.Get(Key);
        }
        /// <summary>
        /// 调用C++ 写入的缓存
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string MemGet(string Key)
        {
            string val = string.Empty;
            try
            {
                val = MemClient.Get(Key).ToString().Replace("\u0006", string.Empty);

                if (val.Length > 29)
                {
                    val = val.Substring(0, 29);
                }
            }
            catch (Exception)
            {
                val = string.Empty;
            }
            return val;
        }
        #endregion

        public static void Remove(string Key)
        {
            MemClient.Remove(Key);
        }

        public static void Delete(string Key)
        {
            MemClient.Remove(Key);
        }

        public static bool Exists(string Key)
        {
            return MemClient.Get(Key) == null ? false : true;
        }
    }
}
