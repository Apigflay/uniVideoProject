using Common;
using DAL;
using System;
using System.Data;
using System.Collections.Generic;
using Model;

namespace BLL
{
    public class CommonBLL
    {

        #region 单键
        private static CommonBLL _instance;

        static CommonBLL()
        {
            if (_instance == null)
                _instance = new CommonBLL();
        }

        public static CommonBLL Instance
        {
            get { return _instance; }
        }
        #endregion

        #region 数据访问

        /// <summary>
        /// 获取黑名单地区IP
        /// </summary>
        /// <param name="type">1：热门黑名单，2：游戏，3：美国</param>
        /// <returns></returns>
        private List<IPModel> GetBlackIP_List(int type)
        {
            string CK = "Cache_GetBlackIP_List_" + type;
            List<IPModel> iplist = (List<IPModel>)CacheHelper.GetCache(CK);

            if (iplist == null)
            {
                //Stopwatch sw = new Stopwatch();
                //sw.Start();
                iplist = CommonDAL.Instance.GetBlackIP_Data(type, "");
                //sw.Stop();

                CacheHelper.SetCache(CK, iplist, DateTime.Now.AddDays(10), TimeSpan.Zero);
                //LogHelper.WriteLog(LogFile.Debug, "【获取黑名单地区IP】{0},用时：{1}", iplist.Count, sw.Elapsed);
            }
            return iplist;
        }

        /// <summary>
        /// 获取所有北京IP
        /// </summary>
        /// <returns></returns>
        private DataTable GetBeiJinIP()
        {
            string cachekey = "Live_Get_IPInfo_Beijin";
            DataTable dt = (DataTable)CacheHelper.GetCache(cachekey);
            if (dt == null)
            {
                dt = CommonDAL.Instance.GetBeiJinIP_Data();
                CacheHelper.SetCache(cachekey, dt, DateTime.Now.AddDays(10), TimeSpan.Zero);
            }
            return dt;
        }

        /// <summary>
        /// 根据IP获取pid，cid
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public void GetAddressByIP(ref int pid, ref int cid)
        {
            string ip = Tools.GetRealIP();

            IPModel ipInfo = CommonDAL.Instance.GetAddressByIP_Data(ip);
            pid = ipInfo.pid;
            cid = ipInfo.cid;
        }

        /// <summary>
        /// 根据IP获取详细信息
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public IPModel GetAddressByIP()
        {
            string ip = Tools.GetRealIP();

            return CommonDAL.Instance.GetAddressByIP_Data(ip);
        }
        #endregion

        #region Logic

        /// <summary>
        /// 判断当前IP是否是黑名单地区
        /// </summary>
        /// <param name="type">1：热门(北京，美国)，2：游戏</param>
        /// <param name="userip"></param>
        /// <returns>true：是，false：不是</returns>
        public bool BlackAreaIPState(int type, string userip)
        {
            var list = GetBlackIP_List(type);

            long N_Ip = Tools.IpToInt(userip);

            var isExists = list.Exists(f => f.IPFromINT <= N_Ip && N_Ip <= f.IPToINT);

            return isExists;
        }

        /// <summary>
        /// 判断当前IP是否是北京
        /// </summary>
        /// <param name="Ip"></param>
        /// <returns>1:北京IP 0：非北京</returns>
        public int BeiJinIPState(string Ip)
        {
            DataRow[] drs = null;
            long N_Ip = Tools.IpToInt(Ip);
            DataTable dt = GetBeiJinIP();
            if (N_Ip > 0 && dt != null && dt.Rows.Count > 0)
            {
                drs = dt.Select(" IPFromINT<" + N_Ip + " and " + N_Ip + "<IPToINT");
            }
            if (drs != null && drs.Length > 0)
            {
                return 1;
            }
            return 0;
        }

        #endregion

        /// <summary>
        /// 地区是否是美国审核状态
        /// </summary>
        /// <returns></returns>
        public bool isAudit()
        {
            string userip = Tools.GetRealIP();
            bool isAmerican = BlackAreaIPState(3, userip);

            //if (Tools.IsDebug)
            //{
            //    return true;
            //}
            //if ("zhaorui.tiao58.com" == UtilHelper.GetHost())
            if ("zhaorui.tiao58.com" == UtilHelper.GetHost() && isAmerican)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isAudit(int id)
        {
            string appVersion = AppDataBLL.AppVersion.Replace(".", "");
            string appType = AppDataBLL.AppDeviceType;
            string userip = Tools.GetRealIP();
            bool isAmerican = BlackAreaIPState(3, userip);

            if (appType.ToLower() == "ios")
            {
                LiveVersion lv = LiveBLL.GetAppVer_ById(id);

                if (lv != null && int.Parse(appVersion) >= lv.auditVersion)
                {
                    return true;
                }
            }

            //if (Tools.IsDebug)
            //{
            //    return true;
            //}
            //if ("zhaorui.tiao58.com" == UtilHelper.GetHost())
            if ("zhaorui.tiao58.com" == UtilHelper.GetHost() && isAmerican)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
