using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;
using Common;

namespace DAL
{
    public class PasswordDAL
    {
        #region 单键
        private static PasswordDAL _instance;

        static PasswordDAL()
        {
            if (_instance == null)
                _instance = new PasswordDAL();
        }

        public static PasswordDAL Instance
        {
            get { return _instance; }
        }
        #endregion

        /// <summary>
        /// 验证手机是否可以绑定密保
        /// </summary>
        /// <param name="phoneNum">手机</param>
        /// <param name="uidx"></param>
        /// <param name="bindCount">已绑定次数</param>
        /// 1 是可用 0 不能
        public int CheckPhoneBind(string phoneNum, int uidx, ref int bindCount)
        {
            int ret = 0;
            const string sql = "f_userphone_check_V2";
            try
            {
                SqlParameter[] p ={
			        SqlHelper.MakeInParam("@phonenum", SqlDbType.BigInt, 4, phoneNum),
                    SqlHelper.MakeInParam("@user",SqlDbType.Int,4,uidx),
			        SqlHelper.MakeOutParam("@checksta", SqlDbType.Int, 4),
                    SqlHelper.MakeOutParam("@bindCount",SqlDbType.Int,0)
		        };

                SqlHelper.ExecuteNonQuery(DbHelper.conn118, CommandType.StoredProcedure, sql, p);
                ret = Convert.ToInt32(p[2].Value.ToString().Trim());
                bindCount = Convert.ToInt32(p[3].Value);
            }
            catch (Exception ex)
            {
                ret = -1;
                //LogDAL.AddErrorLog(uidx.ToString(), ex.Message);
            }
            return ret;
        }

        /// <summary>
        /// 验证该用户名是否存在绑定手机号
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>返回绑定的手机号</returns>
        public string ChenkUserid_IsBindPhone_Data(string userid)
        {
            SqlParameter[] p =
		    {
			    SqlHelper.MakeInParam("@userid", SqlDbType.VarChar, 20, userid),
			    SqlHelper.MakeOutParam("@phonenum", SqlDbType.VarChar, 20)
		    };
            SqlHelper.ExecuteNonQuery(DbHelper.conn118, CommandType.StoredProcedure, "f_check_BindUserid", p);
            return p[1].Value.ToString();
        }

        /// <summary>
        /// 检测用户是否绑定手机号
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public int IsCheckPhonebyUseridx_Data(int useridx)
        {
            SqlParameter[] p =
			    {
				    SqlHelper.MakeInParam("@useridx", SqlDbType.Int, 4, useridx),
				    SqlHelper.MakeOutParam("@checksta", SqlDbType.Int,4,0),
			    };
            SqlHelper.ExecuteNonQuery(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, "[f_IsCheckPhonebyUseridx]", p);

            int result = (int)p[1].Value;

            return result;
        }

        /// <summary>
        /// 绑定手机号
        /// </summary>
        /// <param name="uidx"></param>
        /// <param name="mphone"></param>
        /// <param name="userid"></param>
        /// <param name="level"></param>
        public int AddPhoneBind(int uidx, string mphone, string userid, int level)
        {
            SqlParameter[] p =
            {
                SqlHelper.MakeInParam("@user",SqlDbType.Int,4,uidx),
                SqlHelper.MakeInParam("@userlevel",SqlDbType.Int,4,level),
                SqlHelper.MakeInParam("@userid",SqlDbType.VarChar,30,userid),
                SqlHelper.MakeInParam("@phonenum", SqlDbType.BigInt, 4, mphone)
            };
            return SqlHelper.ExecuteNonQuery(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, "[live_BindPhone_Insert]", p);
            //手机注册绑定密保
            //return SqlHelper.ExecuteNonQuery(DbHelper.conn118, CommandType.StoredProcedure, "f_userphone_insert", p);
        }

        #region [ 70库相关 ]

        /// <summary>
        /// 添加密码保护信息(70库)
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="tell"></param>
        /// <param name="ClientIP"></param>
        /// <returns></returns>
        public int AddUserTellPassword(string UserId, string tell, string ClientIP)
        {
            SqlParameter[] p =
			{
				SqlHelper.MakeInParam("@UserId", SqlDbType.VarChar, 20, UserId),
 				SqlHelper.MakeInParam("@tell", SqlDbType.VarChar, 20, tell),
				SqlHelper.MakeInParam("@ClientIP", SqlDbType.VarChar, 20, ClientIP)
			};
            return SqlHelper.ExecuteNonQuery(DbHelper.conn70, CommandType.StoredProcedure, "Index_Insert_UserTellPasswordNew", p);
        }

        /// <summary>
        /// 获取密保手机(70库)
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string GetPasswordTell_Data(string userid)
        {
            try
            {
                SqlParameter[] p ={
                    SqlHelper.MakeInParam("@UserId", SqlDbType.VarChar, 20, userid)				 
                };
                return SqlHelper.ExecuteScalar(DbHelper.conn70, CommandType.StoredProcedure, "Index_Select_UserTellPasswordNew", p).ToString();
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(LogFile.SQL, userid + "|" + "绑定手机" + e.Message);
                return "";
            }
        }

        /// <summary>
        /// 1查询用户密码保护信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int GetProtectState(string userid)
        {
            SqlParameter[] p =
			{
				SqlHelper.MakeInParam("@UserId", SqlDbType.VarChar, 20, userid),
				SqlHelper.MakeOutParam("@Ret", SqlDbType.Int, 4)
			};
            SqlHelper.ExecuteNonQuery(DbHelper.conn70, CommandType.StoredProcedure, "Common_User_CheckCert_new", p);
            return int.Parse(p[1].Value.ToString());
        }

        /// <summary>
        /// 2查询用户密保问题
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataTable GetPasswordQuestion(string userid)
        {
            SqlParameter[] p =
			{
				SqlHelper.MakeInParam("@UserId", SqlDbType.VarChar, 20, userid)
			};
            return SqlHelper.ExecuteDataTable(DbHelper.conn70, CommandType.StoredProcedure, "Index_Select_ProtectPassword", p);
        }

        /// <summary>
        /// 3验证用户密保问题
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int GetPasswordQuestionCheck(string userid, string qones, string ones, string qtwo, string atwo, string qthree, string athree)
        {
            try
            {
                SqlParameter[] p =
			    {
				    SqlHelper.MakeInParam("@UserId", SqlDbType.VarChar, 20, userid),
				    SqlHelper.MakeInParam("@QOnes", SqlDbType.VarChar,50,qones),
                    SqlHelper.MakeInParam("@Ones", SqlDbType.VarChar, 50, ones),
                    SqlHelper.MakeInParam("@QTwos", SqlDbType.VarChar, 50, qtwo),
                    SqlHelper.MakeInParam("@Twos", SqlDbType.VarChar, 50, atwo),
                    SqlHelper.MakeInParam("@QThrs", SqlDbType.VarChar, 50, qthree),
                    SqlHelper.MakeInParam("@Thrs", SqlDbType.VarChar, 50, athree),
				    SqlHelper.MakeOutParam("@Ret", SqlDbType.Int, 4)
			    };
                SqlHelper.ExecuteNonQuery(DbHelper.conn70, CommandType.StoredProcedure, "Common_Pro_authenticate", p);
                return int.Parse(p[7].Value.ToString());
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogFile.SQL, userid + "|验证密保失败：" + ex.Message);
                return -2;
            }
        }

        #endregion
    }
}
