using System;
using Common;
using DAL;
using Model;

namespace BLL
{
    public class AccountBLL
    {
        AccountDAL dal = new AccountDAL();
        PasswordBLL pwdbll = new PasswordBLL();

        /// <summary>
        /// 生成第三方用户登陆token 当做密码给手机端
        /// </summary>
        /// <param name="userIdx"></param>
        /// <returns></returns>
        public static string getThirdLoginToken(int userIdx)
        {
            string thirdLoginToken = "";
            string s = "QWERTYUIOPLKJHGFDSAZXCVBNMqwertyuiopasdfghjklzxcvbnm0123456789!$";

            Random random = new Random();
            int nNum = random.Next(20, 30);
            for (int i = 0; i < nNum; i++)
            {
                thirdLoginToken += s[random.Next(s.Length)];
            }
            //string thirdLoginToken = RandomHelper.GetRadString(32);
            return AccountDAL.GetThirdTokenByIdx(1, userIdx, thirdLoginToken);
        }

        /// <summary>
        /// 用户普通登陆
        /// </summary>
        /// <param name="account">用户账号</param>
        /// <param name="pwd">用户密码/第三方账号登陆生成的token</param>
        /// <param name="type">0:9158登陆 1:第三方账号登陆</param>
        /// <param name="ip"></param>
        /// <param name="needpass">1</param>
        /// <returns></returns>
        public Login_UserInfo Login(string account, string pwd, int type, string ip, int needpass)
        {
            Login_UserInfo lu = dal.Login(account, pwd, type, ip, needpass);

            if (lu != null && lu.useridx > 0)
            {
                Insert_LoginLog(lu.useridx, lu.userid);
            }
            return lu;
        }

        /// <summary>
        /// 记录登陆日志
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int Insert_LoginLog(int useridx, string userid)
        {
            string userip = Tools.GetRealIP();

            return dal.Insert_LoginLog_Data(useridx, userid, userip);
        }

        /// <summary>
        /// 第三方登陆时使用
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        //public static string GetThirdToken(int useridx, string token)
        //{
        //    return AccountDAL.GetThirdTokenByIdx(0, useridx, token);
        //}

        /// <summary>
        /// 普通注册
        /// </summary>
        /// <param name="m"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public int Register(MemberInfo m, ref BaseMemberInfo member)
        {
            int uIdx = 0;
            string regIp = Tools.GetRealIP();
            Location loc = PositionHelper.GetLocationInfo(regIp);

            m.ip = regIp;
            m.Birthday = "19950101";
            m.Age = 20;
            m.Pwd = CryptoHelper.ToMD5(m.PwdSrc).ToLower();
            m.NickName = ThirdLoginBLL.VerifyNickName(m.NickName);
            m.Province = loc.Province;
            m.City = loc.City;

            m.UIdx = dal.Register(m);//注册成功 返回值是useridx
            uIdx = m.UIdx;

            if (uIdx > 0)
            {
                dal.Live_Register(uIdx, m);//新注册的用户信息同步到Live_UserInfo表中

                member = new BaseMemberInfo();
                member.uIdx = m.UIdx;
                member.uId = m.UId;
                member.pwd = m.Pwd;
                member.phone = m.phoneNum;
                member.nickName = m.NickName;
            }
            return uIdx;
        }

        /// <summary>
        /// 手机号注册
        /// </summary>
        /// <param name="info"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public int Register_Mobile(MemberInfo m, ref BaseMemberInfo member)
        {
            m.UId = "m" + m.phoneNum;
            m.UIdx = Register(m, ref member);//成功返回用户信息失败返回数据库返回消息

            if (m.UIdx > 0)
            {
                pwdbll.AddPhoneBind(m.UIdx, m.phoneNum, m.UId, 1);//118库 手机号注册绑定手机号
            }
            return m.UIdx;
        }
        #region 老挝版手机注册相关

        /// <summary>
        ///  验证用户userid是否存在
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int useridCheck(string userid) {
            return dal.useridCheck(userid);
        }
        /// <summary>
        ///  验证用户phone是否存在
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public int phoneCheck(string phone)
        {
            return dal.phoneCheck(phone);
        }

        /// <summary>
        /// 产生邀请码
        /// </summary>
        /// <returns></returns>
        public string GenerateStringID()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks).ToUpper();
        }
        /// <summary>
        /// 手机注册
        /// </summary>
        /// <param name="m"> 用户信息</param>
        /// <param name="agentCode">代理邀请码</param>
        /// <param name="InvitationCode">用户邀请码</param>
        /// <returns></returns>
        public int I_userRegister(MemberInfo m, string agentCode,string InvitationCode ,string channelid,string deviceId)
        {
            return dal.I_userRegister(m,agentCode,InvitationCode, channelid, deviceId);
        }

        public int userRegisterUpPwd(int useridx, string NewPwd)
        {
            return dal.userRegisterUpPwd(useridx, NewPwd);
        }
        /// <summary>
        /// 修改密码找回密码 type值为0 用手机号找回密码 ,值为1 用原密码修改
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="oldPwd"></param>
        /// <param name="newPwd"></param>
        /// <param name="type"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int userUpPwd(string phoneNumber, string oldPwd, string newPwd, int type, int uid)
        {
            return dal.userUpPwd(phoneNumber, oldPwd, newPwd, type, uid);
        }
        /// <summary>
        /// 绑定手机号
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="useridx"></param>
        /// <param name="type"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public int userBindTel(string phoneNumber, int useridx, int type, string pwd)
        {
            return dal.userBindTel(phoneNumber, useridx, type, pwd);
        }/// <summary>
        /// 查看是否绑定手机号
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="useridx"></param>
        /// <param name="type"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public int userFountTel( int useridx)
        {
            return dal.userFountTel( useridx);
        }
        /// <summary>
        /// 手机号注册
        /// </summary>
        /// <param name="Pwd"></param>
        /// <param name="Phonenumber"></param>
        /// <param name="InvitationCode"></param>
        /// <param name="devType">设备类型 android | ios</param>
        /// <param name="ip"></param>
        /// <param name="deviceId"> 设备号</param>
        /// <param name="channelid">渠道号</param>
        /// <returns></returns>
        public int userRegisterPhonenumber(string Pwd, string Phonenumber, string InvitationCode, string devType, string ip, string deviceId, int channelid)
        {
            return dal.userRegisterPhonenumber(Pwd, Phonenumber,InvitationCode,devType,ip,deviceId,channelid);
        }
        #endregion
        #region 实名认证

        /// <summary>
        /// 芝麻实名认证记录
        /// </summary>
        public int Certification_Save(Certification cert)
        {
            return dal.Certification_Save_Data(cert);
        }

        /// <summary>
        /// 人工审核
        /// </summary>
        /// <param name="cert"></param>
        /// <returns></returns>
        public int HumanCert_Save(HumanCertAuth cert, ref int isAutoAudit)
        {
            var isAuto = LiveBLL.Get_LiveConfigById(25).data;
            var result = 0;

            Random random = new Random();
            int nNum = random.Next(1, 10);

            ///自动审核加满足条件
            if (nNum > 0 && nNum < 7)
            {
                if (cert.idType == 1 && IdCardHelper.ValidateCard(cert.certNo) && Tools.IsChinese(cert.realName) && isAuto.Equals("1"))
                {
                    isAutoAudit = 1;
                    result = dal.AutoCert_Save_Data(cert);
                }
                if (result == -4)
                {
                    isAutoAudit = 0;
                    result = dal.HumanCert_Save_Data(cert);
                }
            }
            else
            {
                isAutoAudit = 0;
                result = dal.HumanCert_Save_Data(cert);
            }

            //add 2017-03-30 08:29
            //实名认证自动审核通道(规则：大陆身份证，姓名合法，身份证满足正则合法)
            //if (cert.idType == 1 && IdCardHelper.ValidateCard(cert.certNo) && Tools.IsChinese(cert.realName) && isAuto.Equals("1"))
            //{
            //    return dal.AutoCert_Save_Data(cert);
            //}
            //else
            //{
            //    return dal.HumanCert_Save_Data(cert);
            //}
            return result;
        }

        /// <summary>
        /// 实名认证图片修改
        /// </summary>
        /// <param name="userIdx"></param>
        /// <param name="nickName"></param>
        /// <param name="signatrues"></param>
        /// <returns></returns>
        public int UpdateCertPhoto(int userIdx, string front, string back, string handFront, int certid)
        {
            return dal.UpdateCertPhoto_Data(userIdx, front, back, handFront, certid);
        }

        #endregion

        public string IsExistsAccount(string unionid, ref int isEsist)
        {
            isEsist = 0;
            string str = dal.IsExistsAccount_Data(unionid);
            if (!string.IsNullOrEmpty(str))
            {
                isEsist = 1;
            }
            return str;
        }
    }
}
