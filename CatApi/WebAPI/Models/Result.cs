using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WebAPI
{
    public class Result
    {
        /// <summary>
        /// 常用Code状态码说明
        /// 100：操作成功
        /// 101：字符串格式不正确(参数错误)
        /// 102：获取信息失败
        /// 103：token失效
        /// 104：接口安全验证失败
        /// 105：版本停止维护
        /// 106：没有查询到数据
        /// </summary>
        public Result()
        {
            code = "106";
            msg = "nodata";
            data = "";
        }
        public Result(string code, string msg)
        {
            this.code = code;
            this.msg = msg;
        }
        public string code { get; set; }

        public string msg { get; set; }

        public object data { get; set; }
    }
}