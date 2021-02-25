using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class RandomHelper
    {
        private static char[] _chars;
        private static Random _rad = new Random();
        static RandomHelper()
        {
            _chars = new char[36];
            for (int i = 65; i <= 90; i++)//(int)'A'一样
            {
                _chars[i - 65] = (char)i;
            }
            for (int i = 48; i < 58; i++)
            {
                _chars[i - 22] = (char)(i);
            }
        }
        /// <summary>
        /// 获取随即数据(a-z,0-9)
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static string GetRadString(int length)
        {
            string str = string.Empty;
            for (int i = 0; i < length; i++)
            {
                str += _chars[_rad.Next(0, 35)];
            }
            return str;
        }
        ///<summary>
        ///生成随机字符串 
        ///</summary>
        ///<param name="length">目标字符串的长度</param>
        ///<param name="useNum">是否包含数字，1=包含，默认为包含</param>
        ///<param name="useLow">是否包含小写字母，1=包含，默认为包含</param>
        ///<param name="useUpp">是否包含大写字母，1=包含，默认为包含</param>
        ///<param name="useSpe">是否包含特殊字符，1=包含，默认为不包含</param>
        ///<param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
        ///<returns>指定长度的随机字符串</returns>
        public static string GetRandomString(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;
            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }
            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        } 

        /// <summary>
        /// 生成n位字母
        /// </summary>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static string GenerateLetter(int Length)
        {
            char[] constant = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            StringBuilder newRandom = new StringBuilder(constant.Length);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(constant.Length - 1)]);
            }
            return newRandom.ToString();
        }

        /// <summary>
        /// 生成n为验证码
        /// </summary>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static string GenerateNum(int Length)
        {
            char[] constant = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            StringBuilder newRandom = new StringBuilder(constant.Length);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(constant.Length - 1)]);
            }
            return newRandom.ToString();
        }
        public static string Generate(int length)
        {
            char[] constant = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            StringBuilder newRandom = new StringBuilder(constant.Length);
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                newRandom.Append(constant[rd.Next(constant.Length - 1)]);
            }
            return newRandom.ToString().ToLower();
        }

    }
}
