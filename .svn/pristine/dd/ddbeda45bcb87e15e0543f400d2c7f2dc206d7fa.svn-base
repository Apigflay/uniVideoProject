using System;

namespace Model
{
    #region 百度根据经纬度获取地理位置Model类

    /// <summary>
    /// 百度根据经纬度获取地理位置信息模型类
    /// API地址：http://lbsyun.baidu.com/index.php?title=webapi/guide/webservice-geocoding
    /// </summary>
    public class GeoPosition
    {
        /// <summary>
        /// 状态吗说明
        /// 0   ：正常
        /// 1   ：服务器内部错误
        /// 2   ：请求参数非法
        /// 3   ：权限校验失败
        /// 4   ：配额校验失败
        /// 5   ：ak不存在或者非法
        /// 101 ：服务禁用
        /// 102 ：不通过白名单或者安全码不对
        /// 2xx ：无权限
        /// 3xx ：配额错误
        /// </summary>
        public int status { get; set; }
        public string message { get; set; }//出错时有值
        public GeoPositionResult result { get; set; }
    }
    public class GeoPositionResult
    {
        public GeoLocation location { get; set; }//经纬度
        public string formatted_address { get; set; }//北京市海淀区中关村南一街7号平房-4号
        public string business { get; set; }//所在商圈信息，如 "人民大学,中关村,苏州街"/ 中关村、北京大学、五道口
        public int cityCode { get; set; }//城市Id
        //public string[] pois { get; set; }
        //public string[] roads { get; set; }
        //public string[] poiRegions { get; set; }
        public AddressPart addressComponent { get; set; }//结构化地址信息
    }

    public class GeoLocation
    {
        public float lng { get; set; }
        public float lat { get; set; }
    }

    //地址部分
    public class AddressPart
    {
        public int country_code { get; set; }
        public string country { get; set; }//中国
        public string province { get; set; }//北京市
        public string city { get; set; }//北京市
        public string direction { get; set; }//西南
        public string district { get; set; }//海淀区,县级
        public string street { get; set; }//中关村南一街
        public string street_number { get; set; }//7号平房-4号
        public string distance { get; set; }
    }

    #endregion


    #region 百度根据IP获取地理位置类

    //百度地图API返回实体类
    public class BaiduPosition
    {
        /// <summary>
        /// 0：成功：1：失败
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// status为 1时下发错误信息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// //CN|北京|北京|None|CERNET|0|0
        /// </summary>
        public string address { get; set; }

        public BaiduPositionContent content { get; set; }
    }
    public class BaiduPositionContent
    {
        public string address { get; set; }//北京市
        public BaiduPositionAddress_detail address_detail { get; set; }
    }
    public class BaiduPositionAddress_detail
    {
        public int city_code { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string street_number { get; set; }
    }

    #endregion

    /// <summary>
    /// 新浪API接口，根据IP获取地址位置信息实体
    /// </summary>
    [Serializable]
    public class Location
    {
        public Location()
        {
            Country = "中国";
            Province = "浙江";
            City = "杭州";
        }
        public int Ret { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Isp { get; set; }
        public string Desc { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string District { get; set; }
    }

}