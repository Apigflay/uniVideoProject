using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class BaseMemberInfo
    {
        public int uIdx { get; set; }
        public string uId { get; set; }
        public string pwd { get; set; }
        //public int gender { get; set; }
        //public string photo { get; set; }
        public string nickName { get; set; }
        public string phone { get; set; }
    }
    /// <summary>
    /// 登陆用户信息表（喵拍使用）
    /// CreateTime:2017-2-13
    /// Author:zhaorui
    /// </summary>
    [Serializable]
    public class Login_UserInfo
    {
        public int useridx { get; set; }
        public int shortidx { get; set; }
        public string userid { get; set; }

        public string myname { get; set; }

        public int mysex { get; set; }

        public string signatures { get; set; }

        public string smallpic { get; set; }

        public string bigpic { get; set; }
        /// <summary>
        /// 用户喵币
        /// </summary>
        public Int64 cash { get; set; }
        public string mb { get; set; }
        public int fansNum { get; set; }
        public int followNum { get; set; }
        public string token { get; set; }
    }

    /// <summary>
    /// 直播用户信息表 
    /// CreateTime:2016-3-25
    /// Author:zhaorui
    /// </summary>
    [Serializable]
    public class Live_UserInfo
    {
        public int userIdx { get; set; }

        public string nickName { get; set; }

        public int gender { get; set; }

        public int level { get; set; }

        public int grade { get; set; }

        public string signatures { get; set; }

        public string smallpic { get; set; }

        public string bigpic { get; set; }
    }
    /// <summary>
    /// 搜索用户信息表
    /// </summary>
    public class SearchUserInfo : Live_UserInfo
    {
        public Int64 rowNo { get; set; }

        /// <summary>
        /// 直播状态1：在直播2：未直播
        /// </summary>
        public int livetype { get; set; }

        //public int friendNum { get; set; }
        //public int fansNum { get; set; }
    }

    /// <summary>
    /// 源9158 Member表基本数据模型
    /// </summary>
    public class MemberInfo
    {
        /// <summary>
        /// 第三方唯一ID,QQ:openid,Weixin:Unionid,Sinaweibo:uid
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// QQ登录Unionid
        /// </summary>
        public string Unionid { get; set; }
        /// <summary>
        /// 游客账号，绑定第三方登录使用
        /// </summary>
        public int visitoridx { get; set; }

        /// <summary>
        /// 用户首次注册区域ID
        /// </summary>
        public int areaid { get; set; }
        public int PId { get; set; }
        public int CId { get; set; }

        private int _uIdx = 0;
        private string _uId = "";
        private string _nickName = "";
        private string _pwd = "";
        private string _pwdSrc = "";
        private int _sex = 1;
        private int _age = 18;
        private string _birthday = "19980101";
        private string _province = "";
        private string _city = "";
        private string _photo = "";
        private string _bigPic = "";
        private string _introduce = "";
        private string _phoneNum = "";
        private string _ip = "";
        private string _signature = "";
        private string _devType = "";
        private string _platForm = "";

        //add 2017-02-10 17:07:01 区分平台：喵播 ，喵拍
        public string PlatForm
        {
            get { return _platForm; }
            set { _platForm = value; }
        }
        public string devType
        {
            get { return _devType; }
            set { _devType = value; }
        }

        public string Signature
        {
            get { return _signature; }
            set { _signature = value; }
        }
        /// <summary>
        /// 用户IDX
        /// </summary>
        public int UIdx
        {
            get { return _uIdx; }
            set { _uIdx = value; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UId
        {
            get { return _uId; }
            set { _uId = value; }
        }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName
        {
            get { return _nickName; }
            set { _nickName = value; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }
        /// <summary>
        /// 源密码（未MD5）
        /// </summary>
        public string PwdSrc
        {
            get { return _pwdSrc; }
            set { _pwdSrc = value; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex
        {
            get
            {
                return _sex == 2 ? 0 : _sex;
            }
            set
            {
                _sex = value;
            }
        }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }
        /// <summary>
        /// 所在省
        /// </summary>
        public string Province
        {
            get { return _province; }
            set { _province = value; }
        }
        /// <summary>
        /// 所在城市
        /// </summary>
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string Photo
        {
            get
            {
                if (string.IsNullOrEmpty(_photo))
                {
                    _photo = "http://liveimg.9158.com/default.png";
                }
                return _photo;
            }
            set { _photo = value; }
        }
        public string BigPic
        {
            get
            {
                if (string.IsNullOrEmpty(_bigPic))
                {
                    _bigPic = "http://liveimg.9158.com/default.png";
                }
                return _bigPic;
            }
            set { _bigPic = value; }
        }

        /// <summary>
        /// 介绍
        /// </summary>
        public string Introduce
        {
            get { return _introduce; }
            set { _introduce = value; }
        }

        /// <summary>
        /// 用户绑定手机
        /// </summary>
        public string phoneNum
        {
            get { return _phoneNum; }
            set { _phoneNum = value; }
        }

        /// <summary>
        /// 用户IP
        /// </summary>
        public string ip
        {
            get { return _ip; }
            set { _ip = value; }
        }
    }

}
