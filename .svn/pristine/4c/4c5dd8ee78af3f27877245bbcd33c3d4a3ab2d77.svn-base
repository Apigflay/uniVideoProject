using System;
using System.Diagnostics;
using Common;

namespace WebAPI
{
    /// <summary>
    /// 喵播程序工具配置文件
    /// </summary>
    public class ToolsHelper
    {
        /// <summary>
        /// 计算密码强度
        /// </summary>
        /// <param name="password">密码字符串</param>
        /// <returns></returns>
        public static bool PasswordStrength(string password)
        {
            //空字符串强度值为0
            if (string.IsNullOrEmpty(password)) return false;

            //字符统计
            int iNum = 0, iLtt = 0, iSym = 0;
            foreach (char c in password)
            {
                if (c >= '0' && c <= '9') iNum++;
                else if (c >= 'a' && c <= 'z') iLtt++;
                else if (c >= 'A' && c <= 'Z') iLtt++;
                else iSym++;
            }

            if (iLtt == 0 && iSym == 0) return false; //纯数字密码
            if (iNum == 0 && iLtt == 0) return false; //纯符号密码
            if (iNum == 0 && iSym == 0) return false; //纯字母密码

            if (password.Length >= 6 && password.Length < 16) return true;//长度不大于6的密码

            if (iLtt == 0) return true; //数字和符号构成的密码
            if (iSym == 0) return true; //数字和字母构成的密码
            if (iNum == 0) return true; //字母和符号构成的密码

            return true; //由数字、字母、符号构成的密码
        }

        /// <summary>  
        /// 生成时间戳  
        /// </summary>  
        /// <param name="time"> DateTime时间格式</param>  
        /// <returns>Unix时间戳格式</returns>  
        //public static int GetDefauleTimestamp()
        //{
        //    DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        //    DateTime nowTime = DateTime.Now;
        //    return (int)(nowTime - startTime).TotalSeconds;
        //}

        #region 创建远程图片服务器连接

        public static bool Connect(string remoteHost, string userName, string passpord)
        {
            ///香港服务器直接返回true add by zhaorui 2018-1-15  	HK-87-41
            string serverIP = System.Net.Dns.GetHostName();
            if (serverIP.Equals("HK-87-41")) return true;

            bool Flag = true;
            Process proc = new Process();
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.CreateNoWindow = true;
            try
            {
                proc.Start();
                string command = @"net use \\" + remoteHost + "  " + passpord + "  " + "  /user:" + remoteHost + "\\" + userName + ">NUL";

                proc.StandardInput.WriteLine(command);
                command = "exit";
                proc.StandardInput.WriteLine(command);
                while (proc.HasExited == false)
                {
                    proc.WaitForExit(1000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                if (errormsg != "")
                    Flag = false;
                proc.StandardError.Close();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogFile.Warning, "上传图片远程连接:" + ex.Message);
                Flag = false;
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
            return Flag;
        }
        #endregion

    }
}