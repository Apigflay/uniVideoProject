using Common;
using Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class AccountDAL
    {
        /// <summary>
        /// 获取第三方登陆token
        /// </summary>
        /// <param name="type">0:登陆 1:注册</param>
        /// <param name="useridx"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GetThirdTokenByIdx(int type, int useridx, string token)
        {
            string ret = "";
            SqlParameter[] p =
                {
                   SqlHelper.MakeInParam("@useridx",SqlDbType.Int,20,useridx),
                   SqlHelper.MakeInParam("@token",SqlDbType.VarChar,50,token),
                   SqlHelper.MakeOutParam("@ret",SqlDbType.VarChar,50,""),
                   SqlHelper.MakeInParam("@type",SqlDbType.Int,4,type),
                   SqlHelper.MakeInParam("@platform",SqlDbType.VarChar,50,"mianjuApp"),
                };
            DbHelper.ExecuteNonQuery("Live_GetThirdToken", p);
            ret = Convert.ToString(p[2].Value.ToString());
            return ret;
        }

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="account">用户账号支持手机号/用户id/用户idx登录</param>
        /// <param name="pwd">用户密码</param>
        /// <param name="type">0:9158账号登陆 1:第三方账号登陆 </param>
        /// <param name="ip"></param>
        /// <param name="needpass">0：不需要密码 1：需要密码 第三方登录时传1</param>
        /// <returns></returns>
        public Login_UserInfo Login(string account, string pwd, int type, string ip, int needpass)
        {
            SqlParameter[] p =
                {
                   SqlHelper.MakeInParam("@id",SqlDbType.VarChar,20,account),
                   SqlHelper.MakeInParam("@pass",SqlDbType.VarChar,50,pwd),
                   SqlHelper.MakeInParam("@type",SqlDbType.Int,4,type),
                   SqlHelper.MakeInParam("@ip",SqlDbType.VarChar,50,ip),
                   SqlHelper.MakeInParam("@needPass",SqlDbType.Int,4,needpass)
                };
            DataTable dt = DbHelper.GetTable("[Live_UserLogin]", p);

            return RFHelper<Login_UserInfo>.GetEntity(dt);
        }

        /// <summary>
        /// 登陆日志记录
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="userid"></param>
        /// <param name="userip"></param>
        /// <returns></returns>
        public int Insert_LoginLog_Data(int useridx, string userid, string userip)
        {
            SqlParameter[] p = { 
                               SqlHelper.MakeInParam("@useridx",SqlDbType.Int,4,useridx),
                               SqlHelper.MakeInParam("@userid",SqlDbType.VarChar,30,userid),
                               SqlHelper.MakeInParam("@userip",SqlDbType.VarChar,30,userip),
                               };
            return DbHelper.ExecuteNonQuery("Live_Insert_LoginLog", p);
        }

        /// <summary>
        ///  普通注册，手机号注册
        /// </summary>
        /// <param name="m"></param>
        /// <returns>成功返回useridx，失败返回错误代码</returns>
        public int Register(MemberInfo m)
        {
            int ret = 0;
            SqlParameter[] p =
            {
                SqlHelper.MakeInParam("@UserId",SqlDbType.VarChar,20,m.UId),         //@UserId Varchar(20),
                SqlHelper.MakeInParam("@MyName",SqlDbType.VarChar,50,GetMyName(m.NickName)),   //@MyName Varchar(50),
                SqlHelper.MakeInParam("@Password",SqlDbType.VarChar,50,m.Pwd),      
                SqlHelper.MakeInParam("@MySex",SqlDbType.Int,4,m.Sex),              
                SqlHelper.MakeInParam("@ProvinceId",SqlDbType.Int,4,m.PId),         
                SqlHelper.MakeInParam("@CityId",SqlDbType.Int,4,m.CId),             
                SqlHelper.MakeInParam("@MyAge",SqlDbType.Int,4,m.Age),              
                SqlHelper.MakeInParam("@Birthday",SqlDbType.VarChar,50,m.Birthday), 
                SqlHelper.MakeInParam("@Email",SqlDbType.VarChar,50,""),            
                SqlHelper.MakeInParam("@RegSource",SqlDbType.SmallInt,4,40),        //@RegSource SmallInt,   40喵播注册来源
                SqlHelper.MakeInParam("@Introducer",SqlDbType.VarChar,50,"miaobo"), //@Introducer Varchar(50),
                SqlHelper.MakeInParam("@RegisterIp",SqlDbType.VarChar,50,m.ip),     //@RegisterIp Varchar(50),
                SqlHelper.MakeOutParam("@Return",SqlDbType.Int,4,ret)               //@Return Int Output
            };
            try
            {
                SqlHelper.ExecuteNonQuery(DbHelper.conn70, CommandType.StoredProcedure, "Register_Insert_Member", p);
                ret = int.Parse(p[12].Value.ToString());
            }
            catch (Exception e)
            {
                ret = -1;
                LogHelper.WriteLog(LogFile.SQL, "【注册失败70库】uId:" + m.UId + "|myName:" + m.NickName + "|ip:" + m.ip + "|" + e.Message);
            }
            return ret;
        }

        /// <summary>
        /// 第三方帐号注册登录，不存在则插入记录 存在则返回用户信息
        /// </summary>
        /// <param name="member">用户实体信息</param>
        /// <param name="iRet">返回值：1</param>
        /// <returns>-1：注册失败。0：已注册用户，1：新注册用户:绑定了，但注册信息没有。也是新注册用户。-3：游客账号已经绑定过了，-4：第三方账号已经绑定过了</returns>
        public MemberInfo VerifyThirdMember_V2(MemberInfo member, ref int iRet)
        {
            iRet = -1;
            SqlParameter[] p ={
                   SqlHelper.MakeInParam("@Account",SqlDbType.VarChar,50,member.Account),
                   SqlHelper.MakeInParam("@PassWord",SqlDbType.VarChar,32,member.Pwd),              
                   SqlHelper.MakeInParam("@Introduce",SqlDbType.VarChar,20,member.Introduce),
                   SqlHelper.MakeInParam("@IntroduceName",SqlDbType.VarChar,20,""),    
                   SqlHelper.MakeInParam("@ThirdName",SqlDbType.VarChar,10,GetMyName(member.NickName)),
                   SqlHelper.MakeInParam("@regIP",SqlDbType.VarChar,20,member.ip),
                   SqlHelper.MakeInParam("@MySex",SqlDbType.Int,4,member.Sex),
                   SqlHelper.MakeInParam("@MyAge",SqlDbType.Int,4,member.Age),
                   SqlHelper.MakeInParam("@Birthday",SqlDbType.VarChar,50,member.Birthday),
                   SqlHelper.MakeInParam("@ProvinceId",SqlDbType.Int,4,member.PId),
                   SqlHelper.MakeInParam("@CityId",SqlDbType.Int,4,member.CId),
                   SqlHelper.MakeInParam("@Email",SqlDbType.VarChar,50,""),

                   SqlHelper.MakeOutParam("@nRet",SqlDbType.Int,4,0),
                   SqlHelper.MakeOutParam("@Idx",SqlDbType.Int,4,0),
                   SqlHelper.MakeOutParam("@Id",SqlDbType.VarChar,20,""),
                   
                   SqlHelper.MakeInParam("@visitoridx",SqlDbType.Int,4,member.visitoridx)
                };
            try
            {
                DbHelper.ExecuteNonQuery("f_AccountThirdBind_mobile_V2", p);

                iRet = Convert.ToInt32(p[12].Value);
                member.UIdx = string.IsNullOrEmpty(p[13].Value.ToString()) ? 0 : Convert.ToInt32(p[13].Value);
                member.UId = Convert.ToString(p[14].Value);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogFile.SQL, "第三方注册失败 => Account:{0},msg:{1}", member.Account, ex.Message);
                iRet = -99;
            }
            return member;
        }
        /// <summary>
        /// 第三方登录 （暂时微信）
        /// </summary>
        /// <param name="member"></param>
        /// <param name="channelid"></param>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public int VerifyThirdLogin(MemberInfo member,int channelid,string deviceId)
        {
            SqlParameter[] p ={
                   SqlHelper.MakeInParam("@unionid",SqlDbType.VarChar,50,member.Account),
                   SqlHelper.MakeInParam("@pwd",SqlDbType.VarChar,255,member.Pwd),
                   SqlHelper.MakeInParam("@devType",SqlDbType.VarChar,20,member.devType),
                   SqlHelper.MakeInParam("@ip",SqlDbType.VarChar,20,member.ip),
                   SqlHelper.MakeInParam("@channelid",SqlDbType.Int,4,channelid),
                   SqlHelper.MakeInParam("@deviceId",SqlDbType.VarChar,50,deviceId),
                   SqlHelper.MakeOutParam("@iRet",SqlDbType.Int,4,0),
                   SqlHelper.MakeInParam("@smallpic",SqlDbType.NVarChar,256,member.Photo),
                   SqlHelper.MakeInParam("@bigllpic",SqlDbType.NVarChar,256,member.BigPic),
                   SqlHelper.MakeInParam("@myname",SqlDbType.NVarChar,50,member.NickName)

                };
            try
            {
                DbHelper.ExecuteNonQuery("[I_userRegisterWeChat]", p);
                int iRet = Convert.ToInt32(p[6].Value);
                return iRet;      
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogFile.SQL, "第三方注册失败 => Account:"+ member.Account + ",msg:"+ ex.Message + "");
                return 0;
            }
        }

        /// <summary>
        /// QQ登陆 Unionid记录
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="openid"></param>
        /// <param name="unionid"></param>
        /// <returns></returns>
        public int Live_QQLogin_Insert_Data(int useridx, string openid, string unionid)
        {
            string sql = "Live_AccountBind_Ex_Insert";
            SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@idx",SqlDbType.Int,10,useridx),
                                SqlHelper.MakeInParam("@account",SqlDbType.VarChar,50,openid),
                                SqlHelper.MakeInParam("@unionid",SqlDbType.VarChar,50,unionid),
                                };
            return DbHelper.ExecuteNonQuery(sql, sp);
        }

        /// <summary>
        /// 注册同步到喵播Live_Userinfo表中
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public int Live_Register(int regRet, MemberInfo member)
        {
            int ret = -1;
            SqlParameter[] p = {
                               SqlHelper.MakeInParam("@UId",SqlDbType.VarChar,20,member.UId),
                               SqlHelper.MakeInParam("@UIdx",SqlDbType.Int,20,member.UIdx),
                               SqlHelper.MakeInParam("@MyName",SqlDbType.NVarChar,20,member.NickName),
                               SqlHelper.MakeInParam("@Gender",SqlDbType.Int,4,member.Sex),
                               SqlHelper.MakeInParam("@Signature",SqlDbType.NVarChar,30,member.Signature),
                               SqlHelper.MakeInParam("@BigPic",SqlDbType.VarChar,200,member.BigPic),
                               SqlHelper.MakeInParam("@Smallpic",SqlDbType.VarChar,200,member.Photo),
                               SqlHelper.MakeInParam("@devType",SqlDbType.VarChar,20,member.devType),
                               SqlHelper.MakeInParam("@province",SqlDbType.VarChar,50,member.Province),
                               SqlHelper.MakeInParam("@city",SqlDbType.VarChar,50,member.City),
                               SqlHelper.MakeOutParam("@Ret",SqlDbType.Int,4,0),
                               SqlHelper.MakeInParam("@registerIP",SqlDbType.VarChar,30,member.ip),
                               SqlHelper.MakeInParam("@param1",SqlDbType.VarChar,10,member.PlatForm),
                               //SqlHelper.MakeInParam("@regResult",SqlDbType.Int,10,regRet),
                               };
            try
            {
                DbHelper.ExecuteNonQuery("Live_Register_V2", p);
                //如果是公司IP则向喵拍测试库以及喵播测试库同步一份数据 add 2017-04-12 09:53:50
                if (Tools.IsCompanyIP)
                {
                    DbHelper.ExecuteNonQuery(DbHelper.conn_MpTest, "Live_Register_V2", p);
                    DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, "Live_Register_V2", p);
                }
                ret = (int)p[10].Value;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogFile.SQL, "【喵播注册同步出错】uidx:" + member.UIdx + ",error:" + ex.Message);
            }
            return ret;
        }
        #region 老挝版手机注册
        /// <summary>
        ///  验证用户userid是否存在
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int useridCheck(string userid)
        {
            try
            {
                string sql = "I_useridCheck";
                SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@userid",SqlDbType.VarChar,20,userid),
                                 SqlHelper.MakeOutParam("@iRet",SqlDbType.Int,4,0)
                                };
                DbHelper.ExecuteNonQuery(sql, sp);
                int iRet = (int)sp[1].Value;
                return iRet;
            }
            catch (Exception)
            {

                return 0;
            }
          
        }
        /// <summary>
        ///  验证用户userid是否存在
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int phoneCheck(string phone)
        {
            try
            {
                string sql = "I_phoneCheck";
                SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@phone",SqlDbType.VarChar,20,phone),
                                 SqlHelper.MakeOutParam("@iRet",SqlDbType.Int,4,0)
                                };
                DbHelper.ExecuteNonQuery(sql, sp);
                int iRet = (int)sp[1].Value;
                return iRet;
            }
            catch (Exception)
            {

                return 0;
            }

        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="OriginalPwd">原密码md5密文</param>
        /// <param name="NewPwd">新密码md5密文</param>
        /// <returns></returns>
        public int userRegisterUpPwd(int  useridx,string  NewPwd )
        {

            try
            {
                string sql = "I_userRegisterUpPwd";
                SqlParameter[] sp = {
                                           SqlHelper.MakeInParam("@useridx",SqlDbType.Int,4,useridx),
                               // SqlHelper.MakeInParam("@OriginalPwd",SqlDbType.VarChar,255,OriginalPwd),
                                   SqlHelper.MakeInParam("@NewPwd",SqlDbType.VarChar,255,NewPwd),
                                      SqlHelper.MakeOutParam("@iRet",SqlDbType.Int,4,0),
                                };
                DbHelper.ExecuteNonQuery(sql, sp);
                int iRet = (int)sp[2].Value;
                return iRet;
            }
            catch (Exception)
            {

                return 0;
            }

        }
        /// <summary>
        /// 找回密码修改密码 type值为0 用手机号找回密码 ,值为1 用原密码修改
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="oldPwd"></param>
        /// <param name="newPwd"></param>
        /// <param name="type"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int userUpPwd(string phoneNumber, string oldPwd, string newPwd, int type, int uid)
        {
            try
            {
                string sql = "I_userUpPwd_W";
                SqlParameter[] sp = {
                                   SqlHelper.MakeInParam("@phoneNumber",SqlDbType.NVarChar,20,phoneNumber),
                                   SqlHelper.MakeInParam("@oldPwd",SqlDbType.VarChar,255,oldPwd),
                                   SqlHelper.MakeInParam("@newPwd",SqlDbType.VarChar,255,newPwd),
                                   SqlHelper.MakeInParam("@type",SqlDbType.Int,2,type),
                                   SqlHelper.MakeInParam("@uid",SqlDbType.Int,10,uid),
                                   SqlHelper.MakeOutParam("@iRet",SqlDbType.Int,4,0),
                                };
                DbHelper.ExecuteNonQuery(sql, sp);
                int iRet = (int)sp[5].Value;
                return iRet;
            }
            catch (Exception)
            {

                return 0;
            }
        }
        /// <summary>
        /// 绑定手机号
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="useridx"></param>
        /// <param name="type"> 1普通用户绑定 0 游客绑定</param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public int userBindTel(string phoneNumber, int useridx, int type, string pwd)
        {
            try
            {
                string sql = "I_userBindTel";
                SqlParameter[] sp = {
                                   SqlHelper.MakeInParam("@phoneNumber",SqlDbType.NVarChar,20,phoneNumber),
                                   SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx),
                                   SqlHelper.MakeOutParam("@iRet",SqlDbType.Int,4,0),
                                   SqlHelper.MakeInParam("@type",SqlDbType.Int,4,type),
                                   SqlHelper.MakeInParam("@pwd",SqlDbType.NVarChar,256,pwd)
                                };
                DbHelper.ExecuteNonQuery(sql, sp);
                int iRet = (int)sp[2].Value;
                return iRet;
            }
            catch (Exception)
            {

                return 0;
            }
        }
        /// <summary>
        /// 存在返回0 不存在 1
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public int userFountTel(int useridx)
        {
            try
            {
                string sql = "I_userFountTel";
                SqlParameter[] sp = {
                                   SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx),
                                   SqlHelper.MakeOutParam("@iRet",SqlDbType.Int,4,0),

                                };
                DbHelper.ExecuteNonQuery(sql, sp);
                int iRet = (int)sp[1].Value;
                return iRet;
            }
            catch (Exception)
            {

                return 1;
            }
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
        /// iRet -2 --手机号已被注册  -4 --获取用户idx失败 >0成功
        /// <returns></returns>
        public int userRegisterPhonenumber(string Pwd,string Phonenumber,string InvitationCode,string devType,string ip,string deviceId,int channelid)
        {
            try
            {


                string sql = "I_userRegisterPhone";
                SqlParameter[] sp = {
                                 SqlHelper.MakeInParam("@phonenumber",SqlDbType.VarChar,20,Phonenumber),
                                 SqlHelper.MakeInParam("@pwd",SqlDbType.VarChar,255,Pwd),
                                 SqlHelper.MakeInParam("@InvitationCode",SqlDbType.VarChar,20,InvitationCode),
                                 SqlHelper.MakeInParam("@devType",SqlDbType.VarChar,20,devType),
                                 SqlHelper.MakeInParam("@ip",SqlDbType.VarChar,20,ip),
                                 SqlHelper.MakeInParam("@deviceId",SqlDbType.VarChar,50,deviceId),
                                 SqlHelper.MakeInParam("@channelid",SqlDbType.Int,4,channelid),
                                 SqlHelper.MakeOutParam("@iRet",SqlDbType.Int,4,0)
                                };
                DbHelper.ExecuteNonQuery(sql, sp);
                int iRet = (int)sp[7].Value;
                return iRet;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogFile.SQL, "注册 " + ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// 手机注册
        /// </summary>
        /// <param name="m"> 用户信息</param>
        /// <param name="agentCode">代理邀请码</param>
        /// <param name="InvitationCode">用户邀请码</param>
        /// <returns></returns>
        public int I_userRegister(MemberInfo m, string agentCode, string InvitationCode,string channelid,string deviceId)
        {
            try
            {


                string sql = "I_userRegister";
                SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@userid",SqlDbType.VarChar,20,m.UId),
                                 SqlHelper.MakeInParam("@pwd",SqlDbType.VarChar,255,m.Pwd),
                                 SqlHelper.MakeInParam("@phoneNumber",SqlDbType.VarChar,20,m.phoneNum),
                                 SqlHelper.MakeInParam("@InvitationCode",SqlDbType.VarChar,16,InvitationCode),
                                 SqlHelper.MakeInParam("@agentCode",SqlDbType.VarChar,16,agentCode),
                                 SqlHelper.MakeInParam("@devType",SqlDbType.VarChar,20,m.devType),
                                 SqlHelper.MakeInParam("@province",SqlDbType.VarChar,50,m.Province),
                                 SqlHelper.MakeInParam("@city",SqlDbType.VarChar,50,m.City),
                                 SqlHelper.MakeInParam("@ip",SqlDbType.VarChar,20,m.ip),
                                 SqlHelper.MakeOutParam("@iRet",SqlDbType.Int,4,0),
                                 SqlHelper.MakeInParam("@channelid",SqlDbType.Int,4,channelid),
                                 SqlHelper.MakeInParam("@deviceId",SqlDbType.VarChar,50,deviceId)
                                };
                DbHelper.ExecuteNonQuery(sql, sp);
                int iRet = (int)sp[9].Value;
                return iRet;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogFile.SQL, "注册 " + ex.Message);
                return 0;
            }
        }

        #endregion
        // 通过Idx查询用户信息(118)multi_getuserinfo_byidx

        #region 获取用户昵称 118数据库用
        /// <summary>
        /// 验证用户昵称(118数据库5个长度)
        /// </summary>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public static string GetMyName(string nickname)
        {
            Encoding ascii = Encoding.Default;
            byte[] asciiBytes = ascii.GetBytes(nickname.Trim());
            if (asciiBytes.Length > 10)
            {
                nickname = nickname.Substring(0, 5);
            }
            if (string.IsNullOrEmpty(nickname))
            {
                nickname = GetMyName();
            }
            return nickname;
        }

        /// <summary>
        /// 获取第三方登陆昵称（118库用）
        /// </summary>
        /// <returns></returns>
        public static string GetMyName()
        {
            var random = new Random((int)DateTime.Now.Ticks);
            string[] move =
            {
                "th","xx", "xd", "xw", "xb","xm","xh","mb"
            };

            return move[random.Next(0, move.Length)] + RandomHelper.GenerateNum(5);
        }
        #endregion

        #region [ 实名认证 ]

        /// <summary>
        /// 芝麻实名认证记录
        /// EXEC [Live_Cert_Save_V2] 2,64214714,'330225198102240357',1,'卢双','system','127.0.0.1',''
        /// </summary>
        public int Certification_Save_Data(Certification cert)
        {
            const string sql = "Live_Cert_Save_V2";
            SqlParameter[] p ={
                SqlHelper.MakeInParam("@dataAction",SqlDbType.Int,4,1),
                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,20,cert.useridx),
                SqlHelper.MakeInParam("@certno",SqlDbType.VarChar,50,cert.certNo),
                SqlHelper.MakeInParam("@idtype",SqlDbType.Int,4,1),
                SqlHelper.MakeInParam("@realname",SqlDbType.VarChar,20,cert.realName),
                SqlHelper.MakeInParam("@openid",SqlDbType.VarChar,50,cert.openid),
                SqlHelper.MakeInParam("@userip",SqlDbType.VarChar,30,cert.userip),
                SqlHelper.MakeInParam("@phoneNo",SqlDbType.NVarChar,11,"")
            };
            return DbHelper.ExecuteNonQuery(sql, p);
        }

        /// <summary>
        /// 实名认证，人工审核
        /// </summary>
        /// <param name="cert"></param>
        /// <returns></returns>
        public int HumanCert_Save_Data(HumanCertAuth cert)
        {
            const string sql = "Live_HumanCert_Save";
            int ret = 0;
            SqlParameter[] p ={
                   SqlHelper.MakeInParam("@useridx",SqlDbType.Int,20,cert.useridx),
                   SqlHelper.MakeInParam("@certNo",SqlDbType.VarChar,50,cert.certNo),              
                   SqlHelper.MakeInParam("@realName",SqlDbType.VarChar,30,cert.realName),
                   SqlHelper.MakeInParam("@nation",SqlDbType.VarChar,20,cert.nation),
                   SqlHelper.MakeInParam("@phoneNo",SqlDbType.BigInt,20,cert.phoneNo),
                   SqlHelper.MakeInParam("@userip",SqlDbType.VarChar,30,cert.userip),
                   SqlHelper.MakeOutParam("@ret",SqlDbType.Int,10,0),
                   SqlHelper.MakeInParam("@idType",SqlDbType.Int,4,cert.idType)
            };
            DbHelper.ExecuteNonQuery(sql, p);
            //DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, sql, p);
            ret = Convert.ToInt32(p[6].Value);
            return ret;
        }

        /// <summary>
        /// 实名认证，自动审核
        /// </summary>
        /// <param name="cert"></param>
        /// <returns></returns>
        public int AutoCert_Save_Data(HumanCertAuth cert)
        {
            const string sql = "Live_AutoCert_Save";
            int ret = 0;
            SqlParameter[] p ={
                   SqlHelper.MakeInParam("@useridx",SqlDbType.Int,20,cert.useridx),
                   SqlHelper.MakeInParam("@certNo",SqlDbType.VarChar,50,cert.certNo),              
                   SqlHelper.MakeInParam("@realName",SqlDbType.VarChar,30,cert.realName),
                   SqlHelper.MakeInParam("@nation",SqlDbType.VarChar,20,cert.nation),
                   SqlHelper.MakeInParam("@phoneNo",SqlDbType.BigInt,20,cert.phoneNo),
                   SqlHelper.MakeInParam("@userip",SqlDbType.VarChar,30,cert.userip),
                   SqlHelper.MakeOutParam("@ret",SqlDbType.Int,10,0),
                   SqlHelper.MakeInParam("@idType",SqlDbType.Int,4,cert.idType)
            };
            DbHelper.ExecuteNonQuery(sql, p);

            ret = Convert.ToInt32(p[6].Value);
            return ret;
        }

        /// <summary>
        /// 修改实名认证证件图片
        /// </summary>
        /// <param name="userIdx"></param>
        /// <param name="front"></param>
        /// <param name="back"></param>
        /// <param name="handFront"></param>
        /// <returns></returns>
        public int UpdateCertPhoto_Data(int userIdx, string front, string back, string handFront, int certid)
        {
            int ret = 0;
            SqlParameter[] p = {
                               SqlHelper.MakeInParam("@useridx",SqlDbType.Int,20,userIdx),
                               SqlHelper.MakeInParam("@front",SqlDbType.VarChar,200,front),
                               SqlHelper.MakeInParam("@back",SqlDbType.VarChar,200,back),
                               SqlHelper.MakeInParam("@handFront",SqlDbType.VarChar,200,handFront),
                               SqlHelper.MakeInParam("@certid",SqlDbType.Int,5,certid)
                               };
            ret = DbHelper.ExecuteNonQuery("[Live_UpdateCertPhoto]", p);
            //ret = DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, "[Live_UpdateCertPhoto]", p);
            return ret;
        }

        public string IsExistsAccount_Data(string unionid)
        {
            SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@unionid",unionid)
                                };
            object result = DbHelper.ExecuteScalar("Live_IsExistsAccount", sp);
            if (result == DBNull.Value)
                return string.Empty;
            else
                return (string)result;
        }
        #endregion

        /// <summary>
        /// 首次登陸/注册记录用户所在地区
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="ip_info"></param>
        /// <returns></returns>
        public IPModel Live_PositionInfo_Insert_Data(int useridx, string ip, int isNewReg, IPModel ip_info)
        {
            try
            {
                SqlParameter[] sp = { 
                                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx),
                                SqlHelper.MakeInParam("@IP",SqlDbType.VarChar,20,ip),
                                SqlHelper.MakeInParam("@isNewReg",SqlDbType.Int,10,isNewReg),
                                };

                DataTable dt = DbHelper.GetDataTable("Live_PositionInfo_Insert", sp);
                return RFHelper<IPModel>.GetEntity(dt);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
