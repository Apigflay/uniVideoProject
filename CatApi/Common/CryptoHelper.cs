using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Web;
using Model;

namespace Common
{
    /// <summary>
    /// File: 数据安全类
    /// </summary>
    /// <remarks>
    /// <para>Author: "zhaorui"</para>
    /// <para>Date: "2016年3月16日"</para>
    /// <para>ProjectName: "喵播"</para>
    /// <para>Version: "V1.0"</para>
    /// </remarks>
    public static class CryptoHelper
    {
        public const string Register_KEY = "hangzhoutiangekejiwillcrashsoon.";//注册秘钥

        public static string Live_KEY
        {
            get { return "hangzhoutiangekeji9158miaobolive"; }////必须是32个字符 256位}
        }

        public static string AES_KEY
        {
            get { return "hangzhoutiangeke"; }
        }

        public static string LivePc_key = "miaoboclientpc";

        /// <summary>
        /// 获取base64后的json参数字符串转换为字典
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetParam(string p)
        {
            string param = FromBase64(p);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(param);
        }

        public static string GetdicParamByKey(Dictionary<string, string> dicParam, string keyName)
        {
            return dicParam.ContainsKey(keyName) ? dicParam[keyName] : "";
        }

        #region 获取二进制参数

        /// <summary>
        /// 获取二进制流json格式参数
        /// </summary>
        /// <returns>返回解析二进制字典参数</returns>
        public static Dictionary<string, string> GetBinaryParam()
        {
            Stream stream = HttpContext.Current.Request.InputStream;
            byte[] bytes = StreamToBytes(stream);
            string param = Encoding.UTF8.GetString(bytes);

            return JsonConvert.DeserializeObject<Dictionary<string, string>>(param);
        }

        /// <summary>
        ///  获取POST参数
        /// </summary>
        /// <returns>返回解析二进制字符串参数</returns>
        public static string GetStringParam()
        {
            Stream stream = HttpContext.Current.Request.InputStream;
            byte[] bytes = StreamToBytes(stream);

            string param = Encoding.UTF8.GetString(bytes);

            return param;
        }

        /// <summary>
        /// 获取POST二进制流数据（AES加密过的参数）
        /// </summary>
        /// <param name="key">解密秘钥(需要秘钥)</param>
        /// <returns>返回字典类型参数</returns>
        public static Dictionary<string, string> GetBinaryParam(string key)
        {
            Stream stream = HttpContext.Current.Request.InputStream;
            byte[] bytes = new byte[stream.Length];
            string param = string.Empty;

            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);

            try
            {
                param = CryptoHelper.AESDecryptFromBytes(bytes, key);
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(param);
            }
            catch (Exception ex)
            {
             // LogHelper.WriteLog(LogFile.Error, "GetBinaryParam:{0},{1}", param, ex.Message);
            }
           // LogHelper.WriteLog(LogFile.Error, "[POST请求参数错误]{0}", param);

            return null;
        }


        /// <summary>
        /// 获取POST二进制流数据（AES加密过的参数）
        /// </summary>
        /// <param name="key">解密秘钥(需要秘钥)</param>
        /// <returns>返回字典类型参数</returns>
        public static Dictionary<string, string> GetBinaryParamss(string key)
        {
            Stream stream = HttpContext.Current.Request.InputStream;
            byte[] bytes = new byte[stream.Length];
            string param = string.Empty;

            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);

            try
            {
                param = CryptoHelper.AESDecryptFromBytes(bytes, key);
                LogHelper.WriteLog(LogFile.Log, param + "///" + key);
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(param);
            }
            catch (Exception ex)
            {
                
            }
            //LogHelper.WriteLog(LogFile.Error, "[POST请求参数错误]{0}", param);

            return null;
        }

        /// <summary>
        /// 把json转换成指定类型数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetAESBinaryModelParam<T>(string key) where T : class, new()
        {
            Stream stream = HttpContext.Current.Request.InputStream;
            byte[] bytes = StreamToBytes(stream);
            string param = string.Empty;
            try
            {
                param = CryptoHelper.AESDecryptFromBytes(bytes, key);

                return JsonConvert.DeserializeObject<T>(param);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogFile.Error, "GetAESBinaryModelParam:{0},{1}", param, ex.Message);
            }
            return null;
        }
        /// <summary>
        /// 获取POST二进制流数据（AES加密过的参数）
        /// </summary>
        /// <param name="key">解密秘钥(需要秘钥)</param>
        /// <returns>返回解密后的json格式字符串参数</returns>
        public static string GetBinaryStringParam(string key)
        {
            Stream stream = HttpContext.Current.Request.InputStream;
            byte[] bytes = StreamToBytes(stream);

            try
            {
                return CryptoHelper.AESDecryptFromBytes(bytes, key);
            }
            catch (Exception ex)
            {
                //LogHelper.WriteLog(LogFile.Error, "GetBinaryParam方法出错:" + ex.Message);
            }
            return null;
        }
        #endregion

        #region DES 加解密
        /// <summary>
        /// DES 加密(Tiange通用加密方法)
        /// </summary>
        /// <param name="strText">字符串</param>
        /// <param name="encryptKey">密钥</param>
        /// <returns></returns>
        public static string DESEncrypt(string strText, string encryptKey)
        {
            StringBuilder ret = new StringBuilder();
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                //把字符串放到byte数组中

                //原来使用的UTF8编码，我改成Unicode编码了，不行
                byte[] inputByteArray = Encoding.Default.GetBytes(strText);

                //建立加密对象的密钥和偏移量

                //使得输入密码必须输入英文文本
                des.Key = ASCIIEncoding.ASCII.GetBytes(encryptKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(encryptKey);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                foreach (byte b in ms.ToArray())
                {
                    ret.AppendFormat("{0:X2}", b);
                }
            }
            catch (Exception ex)
            {
                //LogHelper.WriteLog(LogFile.Trace, "DES加密失败：" + ex.Message);
            }
            return ret.ToString();
        }
        /// <summary>
        /// 解密字符串(Tiange通用解密方法)
        /// </summary>
        /// <param name="this.inputString">加了密的字符串</param>
        /// <param name="decryptKey">密钥</param>
        public static string DESDecrypt(string inputString, string decryptKey)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = new byte[inputString.Length / 2];
                for (int x = 0; x < inputString.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(inputString.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }

                //建立加密对象的密钥和偏移量，此值重要，不能修改
                des.Key = ASCIIEncoding.ASCII.GetBytes(decryptKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(decryptKey);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象
                StringBuilder ret = new StringBuilder();
                return System.Text.Encoding.Default.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        #endregion

        #region Base64 编码
        /// <summary>
        /// 进行 Base64 编码
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToBase64(string text)
        {
            return Convert.ToBase64String(Encoding.Default.GetBytes(text));
        }

        /// <summary>
        /// 进行 Base64 解码
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string FromBase64(string text)
        {
            return Encoding.Default.GetString(Convert.FromBase64String(text));
        }
        #endregion

        #region MD5 加密(来自DiscuzNT)
        /// <summary>
        /// MD5加密(来自DiscuzNT)
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>MD5结果</returns>
        public static string ToMD5(string str)
        {
            byte[] b = Encoding.Default.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
            {
                ret += b[i].ToString("x").PadLeft(2, '0');
            }
            return ret;
        }
        #endregion

        #region DES 加解密(来自DiscuzNT)
        //默认密钥向量
        private static byte[] _Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// DES加密字符串(来自DiscuzNT)
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串,失败返回源串</returns>
        public static string DESEncode(string encryptString, string encryptKey)
        {
            //encryptKey = TextKit.Substring(encryptKey, 8, "");
            encryptKey = TextHelper.Substring1(encryptKey, 0, 8, "");
            encryptKey = encryptKey.PadRight(8, ' ');
            byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
            byte[] rgbIV = _Keys;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }

        /// <summary>
        /// DES解密字符串(来自DiscuzNT)
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串,失败返源串</returns>
        public static string DESDecode(string decryptString, string decryptKey)
        {
            try
            {
                //decryptKey = TextKit.Substring(decryptKey, 8, "");
                decryptKey = TextHelper.Substring1(decryptKey, 0, 8, "");
                decryptKey = decryptKey.PadRight(8, ' ');
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = _Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region AES 加解密(256位)
        /// <summary>
        /// 256位AES加密  
        /// </summary>
        /// <param name="toEncrypt">要加密的内容</param>
        /// <param name="EncryptKey">加密秘钥</param>
        /// <returns></returns> 
        public static string AESEncrypt(string encryptStr, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(encryptStr);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        /// 字符串AES解密
        /// </summary>
        /// <param name="decryptStr">密文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string AESDecryptFromString(string decryptStr, string key)
        {
            string decrypted = " ";
            byte[] keyArray = Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = Convert.FromBase64String(decryptStr);

            RijndaelManaged rDel = new RijndaelManaged();

            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            try
            {
                ICryptoTransform cTransform = rDel.CreateDecryptor();

                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                decrypted = Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                //LogHelper.WriteLog(LogFile.Trace, "AES解密:" + ex.Message + "|" + decrypted + "|" + toEncryptArray.Length);
                return decrypted;
            }
            return decrypted;
        }

        /// <summary>
        /// byte[] AES解密
        /// </summary>
        /// <param name="decryptStr"></param>
        /// <param name="key">解密秘钥</param>
        /// <returns></returns>
        public static string AESDecryptFromBytes(byte[] decryptByte, string key)
        {
           
            string decrypted = "";
            //填充无效，无法被移除。bytes为空
            byte[] keyArray = Encoding.UTF8.GetBytes(key);
            //byte[] toEncryptArray = decryptByte;

            RijndaelManaged rj = new RijndaelManaged();
            rj.Key = keyArray;
            rj.Mode = CipherMode.ECB;
            rj.Padding = PaddingMode.PKCS7;
            
            try
            {
                ICryptoTransform cTransform = rj.CreateDecryptor();

                byte[] resultArray = cTransform.TransformFinalBlock(decryptByte, 0, decryptByte.Length);
                decrypted = Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogFile.Error, "AES解密Crypto:{0},{1},{2}", ex.Message, decrypted, decryptByte.Length);
            }
            finally
            {
                rj.Clear();
            }

            return decrypted;
        }
        #endregion

        #region 二进制流互转

        /// <summary>
        /// 将 Stream 转成 byte[]
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns></returns>
        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始   
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// <summary>
        /// 二进制流转成string
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>返回string类型数据</returns>
        public static string StreamToString(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始   
            stream.Seek(0, SeekOrigin.Begin);

            //byte[]转成string：
            string str = Encoding.Default.GetString(bytes);
            return str;
        }
        #endregion
    }
}