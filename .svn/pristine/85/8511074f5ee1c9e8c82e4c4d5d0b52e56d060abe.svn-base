using System;

namespace Model
{
    /*下发给客户端json数据格式
     {
        "code": "value",         //响应状态码，必需。客户端应首先根据此项结果进行相应处理。
        "msg":"value",           //状态信息
        "data": {                //返回数据
            "result":[           //如果是列表/分页数据将数据存储在此结果中
                {...},
                ......
            ],
            "pageinfo":{...}    //分页相关数据(如果有分页数据时才有此对象) 
        },
        "timestamp": 1466152833  //服务器时间戳
    }
     */

    /******************Common Code Complain*******************
     *1~100 全局错误
     *> 101 业务代码
     *100：success
     *99： No relevant data //没有相关数据
     *98：
     *97：Unknown Error //未知错误
     * 1：参数错误，参数传递错误
     * 2：登录信息失效（token校验失败，超过有效期）
     * 3：非法请求(可以判断客户端请求头中是否包含某个值)
     * 4：程序错误（全局错误Error）
     * 5：版本升级
     * 6：图片服务器连接失败
     * 7：验证码错误
     * 8：验证码失效
     * 9：接口请求太过频繁
     * 10：绑定手机号
     * 11：请求超时
     * 12：验证码错误
     * 20~40 Operate DataBase Description
     * 20：Insert Failed //添加失败
     * 21：Delete Failed //删除失败
     * 22：Update Failed //修改失败
     * 23：Operate Failed //操作失败
     *************************************************/
    public class MobileResult
    {
        /// <summary>
        /// 常用Code状态码说明
        /// 100：操作成功
        /// 101：字符串格式不正确(参数错误)
        /// 102：登录信息失效（token校验失败，超过有效期）
        /// 103：请在APP内打开
        /// 104：
        /// 105：版本停止维护
        /// 106：查询操作时无数据
        /// </summary>
        public MobileResult()
        {
            code = "106";
            msg = "No data";
            data = "";
        }

        public MobileResult(int code, string msg)
        {
            this.code = code.ToString();
            this.msg = msg;
        }

        public MobileResult(int code, string msg, string data)
        {
            this.code = code.ToString();
            this.msg = msg;
            this.data = data;
        }
        public string code { get; set; }
        public string msg { get; set; }
        public object data { get; set; }
    }

    /// <summary>
    /// 分页实体类
    /// </summary>
    public class Paging
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int totalCount { get; set; }//记录总条数
        public int totalPage { get; set; }//总页数
    }

    public class ErrorLog
    {
        public Int64 row { get; set; }

        /// <summary>
        /// 发生异常的服务器
        /// </summary>
        public string ServerIp { get; set; }

        /// <summary>
        /// 服务器名称
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// 来源路径
        /// </summary>
        public string FullPath { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 代理名称
        /// </summary>
        public string AgentName { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string DevType { get; set; }
        /// <summary>
        /// 用户IP
        /// </summary>
        public string UserIP { get; set; }
        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 用户Idx
        /// </summary>
        public int useridx { get; set; }
        public string StackTrace { get; set; }
    }
    public class WriteLog
    {
        //[BsonId]
        //public ObjectId _Id { get; set; }
        public string _id { get; set; }
        public string MsgType { get; set; }
        //public string Title { get; set; }
        public string Message { get; set; }
        public string UserIP { get; set; }
        public string HostPath { get; set; }
        public string DeviceType { get; set; }
        public string AgentName { get; set; }
        public string ServerName { get; set; }
        //public string Stacktrace { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public class TimeModel
    {
        public string time { get; set; }
        public string shortDate { get; set; }
        public string shortTime { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public int hour { get; set; }
        public int minute { get; set; }
        public int second { get; set; }
        public DayOfWeek weekday { get; set; }
    }

    public class IPModel
    {
        public long IPFromINT { get; set; }
        public long IPToINT { get; set; }
        public int areaid { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public int pid { get; set; }
        public int cid { get; set; }
    }
}
