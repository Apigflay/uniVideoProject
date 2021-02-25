//using Memcached.ClientLibrary;
using Enyim.Caching;
using Enyim.Caching.Memcached;
using System;

namespace Common.Core
{
    /******************************************
    * 文 件 名：MemcachedHelper
    * 创 建 人：zhaorui
    * 创建日期：2017年7月
    * 文件描述：Enyim.Caching 帮助类
    * 源码地址：https://github.com/enyim/EnyimMemcached
    ******************************************
    * 修 改 人：
    * 修改日期：
    * 备注描述：
    *****************************************/
    public class MemcachedHelper
    {
        private static MemcachedClient MemClient;
        private static readonly object _lock = new object();

        //构造函数
        static MemcachedHelper()
        {
            getInstance();
        }

        //线程安全的单例模式
        private static MemcachedClient getInstance()
        {
            if (MemClient == null)
            {
                lock (_lock)
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

        #region Set

        /// <summary>
        /// 此方法时间存储不行，用的是世界时间有待研究
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <param name="ExpiredAt"></param>
        public static void Store(string Key, object Value, DateTime ExpiredAt)
        {
            int i = 0;
            while (!MemClient.Store(StoreMode.Set, Key, Value, ExpiredAt) && i < 3)
            {
                i++;
            }
        }

        public static void Store(string Key, object Value, TimeSpan ExpiredAt)
        {
            int i = 0;
            while (!MemClient.Store(StoreMode.Set, Key, Value, ExpiredAt) && i < 3)
            {
                i++;
            }
        }

        /// <summary>
        /// 兼容老版本以分钟计算
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <param name="ExpiredAt"></param>
        public static void Set(string Key, object Value, int ExpiredAt)
        {
            TimeSpan span = new TimeSpan(0, ExpiredAt, 0);
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
            object obj = MemClient.Get(Key);

            return obj;
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

        #region 增量，减量

        /// <summary>
        /// 增量
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="defaultValue">存储的起始值</param>
        /// <param name="delta">以多少增长存储</param>
        /// <param name="expirAt">缓存时间</param>
        /// <returns>返回的值不可为负 ，最小为0</returns>
        public static ulong Increment(string Key, ulong delta, TimeSpan expirAt, ulong defaultValue = 1)
        {
            return MemClient.Increment(Key, defaultValue, delta, expirAt);
        }

        /// <summary>
        /// 减量
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="defaultValue"></param>
        /// <param name="delta"></param>
        /// <param name="expirAt"></param>
        /// <returns></returns>
        public static ulong Decrement(string Key, ulong delta, TimeSpan expirAt, ulong defaultValue = 1)
        {
            return MemClient.Decrement(Key, defaultValue, delta, expirAt);
        }

        #endregion

        #region Other

        public static void Remove(string Key)
        {
            MemClient.Remove(Key);
        }

        public static bool Exists(string Key)
        {
            return MemClient.Get(Key) != null ? true : false;
        }

        #endregion
    }

    /// <summary>
    /// MemcachedHelper操作类
    /// 用此类一定要把ICSharpCode.SharpZipLib.dll，log4net.dll放入到.bin文件夹中
    /// </summary>
    /// </summary>
    //public class MemcachedHelper
    //{
    //    private static MemcachedClient _mc = null;

    //    static MemcachedHelper()
    //    {
    //        _mc = new MemcachedClient();
    //        //默认压缩阈值 15KB
    //        _mc.CompressionThreshold = 30720;
    //        //是否启用压缩
    //        _mc.EnableCompression = true;
    //        //默认编码
    //        _mc.DefaultEncoding = "UTF-8";
    //    }

    //    /// <summary>
    //    /// MemcachedClient实例
    //    /// </summary>
    //    public static MemcachedClient mCache
    //    {
    //        get
    //        {
    //            if (SockIOPoolHelper.Status == SockIOPoolStatus.Stopped)
    //            {
    //                SockIOPoolHelper.Start();
    //            }
    //            return _mc;
    //        }
    //    }
    //    #region Set

    //    /// <summary>
    //    /// 设置缓存
    //    /// </summary>
    //    /// <param name="key">键</param>
    //    /// <param name="objObject">值</param>
    //    /// <param name="expiry">过期时间</param>
    //    public static void Set(string key, object objObject, DateTime expiry)
    //    {
    //        if (objObject == null) return;
    //        int i = 0;
    //        while ((!mCache.Set(key, objObject, expiry)) && i < 3)
    //        {
    //            i++;
    //        }
    //    }

    //    /// <summary>
    //    /// 设置缓存
    //    /// </summary>
    //    /// <param name="key">键</param>
    //    /// <param name="objObject">值</param>
    //    /// <param name="expiry">过期时间(分钟)</param>
    //    public static void Set(string key, object objObject, int expiry)
    //    {
    //        if (objObject == null) return;
    //        int i = 0;
    //        while ((!mCache.Set(key, objObject, DateTime.Now.AddMinutes(expiry))) && i < 3)
    //        {
    //            i++;
    //        }
    //    }

    //    #endregion

    //    #region Get

    //    /// <summary>
    //    /// 获取缓存
    //    /// </summary>
    //    /// <param name="key">键</param>
    //    /// <returns></returns>
    //    public static object Get(string key)
    //    {
    //        return mCache.Get(key);
    //    }

    //    public static string MemGet(string key)
    //    {
    //        string val = string.Empty;
    //        try
    //        {
    //            val = mCache.Get(key).ToString();
    //            if (val.Length > 29)
    //            {
    //                val = val.Substring(0, 29);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            val = null;
    //        }
    //        return val;
    //    }

    //    #endregion

    //    #region Delete
    //    /// <summary>
    //    /// 删除缓存
    //    /// </summary>
    //    /// <param name="key">键</param>
    //    public static void Delete(string key)
    //    {
    //        CacheHelper.Delete(key);
    //        int i = 0;
    //        while ((!mCache.Delete(key, DateTime.MaxValue)) && i < 3)
    //        {
    //            i++;
    //        }
    //    }

    //    /// <summary>
    //    /// 删除缓存
    //    /// </summary>
    //    /// <param name="expiry">过期时间</param>
    //    /// <param name="key">键</param>
    //    public static void Delete(string key, DateTime expiry)
    //    {
    //        CacheHelper.Delete(key);
    //        int i = 0;
    //        while ((!mCache.Delete(key, expiry)) && i < 3)
    //        {
    //            i++;
    //        }
    //    }
    //    #endregion

    //    /// <summary>
    //    /// 检查是否存在
    //    /// </summary>
    //    /// <param name="key">键</param>
    //    /// <returns></returns>
    //    public static bool Exists(string key)
    //    {
    //        return mCache.KeyExists(key);
    //    }
    //}

    /// <summary>
    /// SOCKET池帮助类
    /// </summary>
    //public static class SockIOPoolHelper
    //{
    //    /// <summary>
    //    /// 缓存服务器列表
    //    /// </summary>
    //    public static string[] Servers = { "122.226.86.72:11215" };
    //    /// <summary>
    //    /// Socket池状态
    //    /// </summary>
    //    public static SockIOPoolStatus Status = SockIOPoolStatus.Stopped;
    //    /// <summary>
    //    /// 开始SOCKET池
    //    /// </summary>
    //    public static void Start()
    //    {
    //        if (Status == SockIOPoolStatus.Stopped)
    //        {
    //            //初始化池
    //            SockIOPool pool = SockIOPool.GetInstance();
    //            pool.SetServers(Servers);
    //            pool.InitConnections = 3;//初始连接数
    //            pool.MinConnections = 3;//最小连接数
    //            pool.MaxConnections = 10;//最大连接数
    //            pool.SocketConnectTimeout = 1000;//socket连接超时时间
    //            pool.SocketTimeout = 3000;
    //            pool.MaintenanceSleep = 3000;//主线程睡眠时间
    //            pool.Failover = true;
    //            pool.Nagle = false;
    //            pool.Initialize();

    //            Status = SockIOPoolStatus.Started;
    //        }
    //    }

    //    /// <summary>
    //    /// 停止SOCKET池
    //    /// </summary>
    //    public static void Stop()
    //    {
    //        //清空所有缓存
    //        MemcachedClient mc = new MemcachedClient();
    //        mc.EnableCompression = false;
    //        mc.FlushAll();

    //        //关闭SOCKET池
    //        SockIOPool.GetInstance().Shutdown();

    //        Status = SockIOPoolStatus.Stopped;
    //    }

    //}

    /// <summary>
    /// SOCKET池状态
    /// </summary>
    public enum SockIOPoolStatus
    {
        /// <summary>
        /// 已开启
        /// </summary>
        Started,
        /// <summary>
        /// 已停止
        /// </summary>
        Stopped
    }
}
