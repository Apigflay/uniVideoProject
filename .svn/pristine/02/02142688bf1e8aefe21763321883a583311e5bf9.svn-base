using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Web;
using BLL.Mongo;

namespace BLL
{
    public class StatisticsBLL
    {
        /// <summary>
        /// 新增用户点击统计
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="anchoridx"></param>
        /// <returns></returns>
        public static int PlayShare_Record(int fuseridx, int anchoridx, int roomid, string ip, string shareType)
        {
            return StatisticsDAL.PlayShare_Record(fuseridx, anchoridx, roomid, ip, shareType);
        }

        /// <summary>
        /// 热搜城市统计
        /// </summary>
        /// <param name="cityname"></param>
        /// <returns></returns>
        public static int HotCity_Statis(string cityname)
        {
            return StatisticsDAL.HotCity_Statis(cityname);
        }

        /// <summary>
        /// 接口访问统计
        /// </summary>
        /// <param name="type">1：下载，2：页面访问</param>
        /// <returns></returns>
        public static int LiveApi_Statis(int type)
        {
            string rawURL = UtilHelper.GetFullURL();
            string referer = UtilHelper.GetReferer();

            return MongoService.Insert_Download(type, "", rawURL, referer);

            //return StatisticsDAL.LiveApi_Statis_data(type, path, rawurl, referer);
        }

        /// <summary>
        /// 游戏点击量统计
        /// </summary>
        /// <param name="gameid"></param>
        /// <returns></returns>
        public static int AccessGame_Statis(int gameid)
        {
            int type = 1;
            //var deviceType = AppDataBLL.AppDeviceType;
            //var version = AppDataBLL.AppVersion;
            //if ((deviceType == "android" && version == "1.9.0") || (deviceType == "ios" && version == "2.0.0"))
            //{
            //    type = 2;
            //}
            return StatisticsDAL.AccessGame_Statis_Data(type, gameid);
        }

        //public static int CheckUpdate_Statis(int useridx, string deviceType, string version, string userip)
        //{
        //    return StatisticsDAL.CheckUpdate_Statis_Data(useridx, deviceType, version, userip);
        //}

        /// <summary>
        /// 搜索关键词统计
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static int Search_Statis(string keyword, int haveResult)
        {
            return StatisticsDAL.SearchKeyword_Statis_Data(keyword, haveResult);
        }
    }
}
