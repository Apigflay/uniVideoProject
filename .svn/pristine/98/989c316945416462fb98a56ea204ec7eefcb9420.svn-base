using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Passport
    {

    }
    /// <summary>
    /// 实名认证2016-8-29
    /// </summary>
    public class Certification
    {
        public int useridx { get; set; }
        public string certNo { get; set; }
        public int idType { get; set; }//证件类型    1：身份证
        public string realName { get; set; }
        public string openid { get; set; }
        public string userip { get; set; }
        public string certTime { get; set; }
    }
    /// <summary>
    /// 人工认证2016-9-20
    /// </summary>
    public class HumanCertAuth
    {
        public int useridx { get; set; }
        public string certNo { get; set; }
        public string realName { get; set; }
        public string nation { get; set; }//国籍    中国，海外
        public int idType { get; set; }//证件类型    1:身份证 2:港澳通行证 3:香港,澳门身份证 4:台胞证 5:护照 见表 Live_IdType
        public string phoneNo { get; set; }
        public string cert_Front { get; set; }
        public string cert_Back { get; set; }
        public string cert_HandFront { get; set; }
        public string userip { get; set; }

        public string usertoken { get; set; }//用户登录token
        public string signature { get; set; }//提交数据时签名认证
        public int apiVersion { get; set; }
    }
}
