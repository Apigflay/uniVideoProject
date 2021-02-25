using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace BLL
{
    public class PasswordBLL
    {
        PasswordDAL dal = new PasswordDAL();
        UserInfoBLL _user = new UserInfoBLL();

        /// <summary>
        /// 检测手机号是否可以绑定
        /// </summary>
        /// <param name="phoneNum"></param>
        /// <param name="uidx"></param>
        /// <returns></returns>
        public int CheckPhoneBind(string phoneNum, int uidx, ref int bindCount)
        {
            uidx = _user.GetUseridxByshortidx(uidx);

            return dal.CheckPhoneBind(phoneNum, uidx, ref bindCount);
        }

        /// <summary>
        /// 检验用户名是否绑定手机号
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string CheckUserid(string userid)
        {
            return dal.ChenkUserid_IsBindPhone_Data(userid);
        }

        /// <summary>
        /// 检测用户是否绑定手机号
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public int IsCheckPhonebyUseridx(int useridx)
        {
            return dal.IsCheckPhonebyUseridx_Data(useridx);
        }

        /// <summary>
        /// 密保手机绑定
        /// </summary>
        /// <param name="uidx"></param>
        /// <param name="mphone"></param>
        /// <param name="userid"></param>
        /// <param name="level"></param>
        public int AddPhoneBind(int uidx, string mphone, string userid, int level)
        {
            int result = dal.AddPhoneBind(uidx, mphone, userid, level);//118库

            dal.AddUserTellPassword(userid, mphone, Tools.GetRealIP());//70库 正式开启添加密码保护信息 

            return result;
        }

        /// <summary>
        /// 添加密码保护信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="tell"></param>
        /// <param name="ClientIP"></param>
        /// <returns></returns>
        //public int AddUserTellPassword(string UserId, string tell, string ClientIP)
        //{
        //    return dal.AddUserTellPassword(UserId, tell, ClientIP);
        //}

        /*验证密保情况*/
        //@ret=0  --没有2级密码和密码保护,没有电话密保
        //@ret=1  --没有2级密码和密码保护,有电话密保
        //@ret=2  --没有2级密码,有密码保护,没有电话密保
        //@ret=3  --有2级密码,有密码保护,没有电话密保
        //@ret=4  --有2级密码,有电话密保(优先),密码保护
        //@ret=5  --有2级密码,没有密码保护,没有电话密保 
        //@ret=6  --有2级密码,有电话密保,没有密码保护
        public int GetProtectState(string userid)
        {
            return dal.GetProtectState(userid);
        }

        /// <summary>
        /// 获取密保手机(70库)
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string GetPasswordTell(string userid)
        {
            return dal.GetPasswordTell_Data(userid);
        }

        /// <summary>
        /// 获取用户密保问题
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataTable GetPasswordQuestion(string userid)
        {
            return dal.GetPasswordQuestion(userid);
        }

        /// <summary>
        /// 验证密保问题
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="qones"></param>
        /// <param name="aones"></param>
        /// <returns></returns>
        public int GetPasswordQuestionCheck(string userid, string qones, string aones, string qtwo, string atwo, string qthree, string athree)
        {
            return dal.GetPasswordQuestionCheck(userid, qones, aones, qtwo, atwo, qthree, athree);
        }
    }
}
