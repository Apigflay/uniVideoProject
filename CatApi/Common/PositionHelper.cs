/******************************************
* 文 件 名：PositionHelper
* Copyright(c) live.9158.com
* 创 建 人：zhaorui
* 创建日期：2015年4月
* 文件描述：地理位置帮助类
******************************************
* 修 改 人：
* 修改日期：
* 备注描述：
*****************************************/

using Model;
using Newtonsoft.Json;
using System;

namespace Common
{
    internal class PositionConfig
    {
        public string ak = "7ffc961fd4a8217ffdc0b561a4b7e5ad";
        public string geoapi = "http://api.map.baidu.com/geocoder/v2/";
        public string locapi = "http://api.map.baidu.com/location/ip";
        public string sinaApi = "http://int.dpool.sina.com.cn/iplookup/iplookup.php";
    }

    /// <summary>
    /// Authorby: zhaorui
    /// time: 2016-7-6
    /// title:地理位置帮助类
    /// </summary>
    public class PositionHelper
    {
        internal static PositionConfig config = new PositionConfig();

        /// <summary>
        /// 根据经纬度获取地理位置信息
        /// 地理编码请求参数
        /// 参数       是否必须   举例
        /// location     是     根据经纬度坐标获取地址。支持批量，多组坐标间用|分隔，单次请求最多解析20组坐标。超过20组取前20组解析。批量解析需使用batch参数。批量解析仅召回行政区划数据。
        /// batch        否     请求为批量时必须，batch=true；若batch=false或为空，请求只解析第一组坐标。  
        /// pois         否     是否显示指定位置周边的poi，0为不显示，1为显示。当值为1时，默认显示周边1000米内的poi。
        /// radius       否     poi召回半径，允许设置区间为0-1000米，超过1000米按1000米召回。
        /// </summary>
        /// <param name="_longitude"></param>
        /// <param name="_latitude"></param>
        /// <returns></returns>
        public static GeoPosition Get_GeoPosition(double _longitude, double _latitude)
        {
            string apiurl = string.Format("{0}?ak={1}&location={2},{3}&output=json&pois=0", config.geoapi, config.ak, _latitude, _longitude);

            string data = HttpHelper.HttpGet(apiurl);

            return JsonConvert.DeserializeAnonymousType(data, new GeoPosition());
        }

        /// <summary>
        /// 获取用户地理位置信息如果获取不到返回用户输入的地理位置信息
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <param name="country"></param>
        /// <param name="pro"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        public static GeoPosition Get_GeoPosition(double lat, double lon, string pro, string city)
        {
            string apiurl = string.Format("{0}?ak={1}&location={2},{3}&output=json&pois=0", config.geoapi, config.ak, lat, lon);

            string data = HttpHelper.HttpGet(apiurl);

            GeoPosition p = (GeoPosition)JsonConvert.DeserializeObject(data, typeof(GeoPosition));

            if (p.status == 0)
            {
                if (p.result.addressComponent.country != "中国")
                {
                    p.result.addressComponent.province = "海外";
                }
            }
            else
            {
                p.result.addressComponent.province = pro;
                p.result.addressComponent.city = city;
            }
            return p;
        }
        /// <summary>
        /// 根据IP获取地理位置信息包括经纬度
        /// </summary>
        /// <returns>
        ///成功 status 为1， 失败返回：{"status":1,"message":"Internal Service Error:ip[127.0.0.1] loc failed"}
        /// </returns>
        public static BaiduPosition GetBaiduLocation(string ip)
        {
            string url = string.Format("{0}?ak={1}&ip={2}&coor=bd09ll", config.locapi, config.ak, ip);

            string data = HttpHelper.HttpGet(url);

            return JsonConvert.DeserializeObject<BaiduPosition>(data);
        }

        /// <summary>
        /// 获取当前访问位置信息
        /// </summary>
        /// <returns></returns>
        public static Location GetLocationInfo(string ip)
        {
            string apiurl = string.Format("{0}?format=json&ip={1}", config.sinaApi, ip);
            string CK = "Live_Location_Info_Ip_" + ip;
            var CK_Value = CacheHelper.GetCache(CK);

            Location loc = new Location(); ;
            if (string.IsNullOrEmpty(ip) || ip == "127.0.0.1")
            {
                return loc;
            }
            string data = "";
            try
            {
                if (CK_Value == null)
                {
                    data = HttpHelper.HttpGet(apiurl);
                    CacheHelper.SetCache(CK, data, 60 * 6);
                }
                else
                {
                    data = (string)CK_Value;
                }

                loc = JsonConvert.DeserializeObject<Location>(data);
            }
            catch
            {
                LogHelper.WriteLog(LogFile.Error, "【获取位置出错】url:" + apiurl + "data:" + data);
            }

            return loc;
        }
        public class PositionModel
        {
            public PositionModel()
            {
                MinLat = 0;
                MaxLat = 0;
                MinLng = 0;
                MaxLng = 0;
            }
            /// <summary>
            /// 纬度最小
            /// </summary>
            public double MinLat { get; set; }
            /// <summary>
            /// 纬度最大
            /// </summary>
            public double MaxLat { get; set; }
            /// <summary>
            /// 经度 最小值
            /// </summary>
            public double MinLng { get; set; }
            /// <summary>
            /// 经度 最大值
            /// </summary>
            public double MaxLng { get; set; }
        }
        /// <summary>
        /// 根据一个给定经纬度的点和距离，进行附近地点查询
        /// </summary>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        /// <param name="distance">距离（单位：公里或千米）</param>
        /// <returns>返回一个范围的4个点，最小纬度和纬度，最大经度和纬度</returns>
        public static PositionModel FindNeighPosition(double longitude, double latitude, double distance)
        {
            //先计算查询点的经纬度范围  
            double r = 6378.137;//地球半径千米  
            double dis = distance;//千米距离    
            double dlng = 2 * Math.Asin(Math.Sin(dis / (2 * r)) / Math.Cos(latitude * Math.PI / 180));
            dlng = dlng * 180 / Math.PI;//角度转为弧度  
            double dlat = dis / r;
            dlat = dlat * 180 / Math.PI;
            double minlat = latitude - dlat;
            double maxlat = latitude + dlat;
            double minlng = longitude - dlng;
            double maxlng = longitude + dlng;
            return new PositionModel
            {
                MinLat = minlat,
                MaxLat = maxlat,
                MinLng = minlng,
                MaxLng = maxlng
            };
        }
        /// <summary>
        /// 计算两点位置的距离，返回两点的距离，单位：公里或千米
        /// 该公式为GOOGLE提供，误差小于0.2米
        /// </summary>
        /// <param name="lat1">第一点纬度</param>
        /// <param name="lng1">第一点经度</param>
        /// <param name="lat2">第二点纬度</param>
        /// <param name="lng2">第二点经度</param>
        /// <returns>返回两点的距离，单位：公里或千米</returns>
        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            //地球半径，单位米
            double EARTH_RADIUS = 6378137;
            double radLat1 = Rad(lat1);
            double radLng1 = Rad(lng1);
            double radLat2 = Rad(lat2);
            double radLng2 = Rad(lng2);
            double a = radLat1 - radLat2;
            double b = radLng1 - radLng2;
            double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) * EARTH_RADIUS;
            return result / 1000;
        }

        /// <summary>
        /// 经纬度转化成弧度
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static double Rad(double d)
        {
            return (double)d * Math.PI / 180d;
        }
    }
}
