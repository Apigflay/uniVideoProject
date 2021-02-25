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
    /// File: ���ݰ�ȫ��
    /// </summary>
    /// <remarks>
    /// <para>Author: "zhaorui"</para>
    /// <para>Date: "2016��3��16��"</para>
    /// <para>ProjectName: "����"</para>
    /// <para>Version: "V1.0"</para>
    /// </remarks>
    public static class CryptoHelper
    {
        public const string Register_KEY = "hangzhoutiangekejiwillcrashsoon.";//ע����Կ

        public static string Live_KEY
        {
            get { return "hangzhoutiangekeji9158miaobolive"; }////������32���ַ� 256λ}
        }

        public static string AES_KEY
        {
            get { return "hangzhoutiangeke"; }
        }

        public static string LivePc_key = "miaoboclientpc";

        /// <summary>
        /// ��ȡbase64���json�����ַ���ת��Ϊ�ֵ�
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

        #region ��ȡ�����Ʋ���

        /// <summary>
        /// ��ȡ��������json��ʽ����
        /// </summary>
        /// <returns>���ؽ����������ֵ����</returns>
        public static Dictionary<string, string> GetBinaryParam()
        {
            Stream stream = HttpContext.Current.Request.InputStream;
            byte[] bytes = StreamToBytes(stream);
            string param = Encoding.UTF8.GetString(bytes);

            return JsonConvert.DeserializeObject<Dictionary<string, string>>(param);
        }

        /// <summary>
        ///  ��ȡPOST����
        /// </summary>
        /// <returns>���ؽ����������ַ�������</returns>
        public static string GetStringParam()
        {
            Stream stream = HttpContext.Current.Request.InputStream;
            byte[] bytes = StreamToBytes(stream);

            string param = Encoding.UTF8.GetString(bytes);

            return param;
        }

        /// <summary>
        /// ��ȡPOST�����������ݣ�AES���ܹ��Ĳ�����
        /// </summary>
        /// <param name="key">������Կ(��Ҫ��Կ)</param>
        /// <returns>�����ֵ����Ͳ���</returns>
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
           // LogHelper.WriteLog(LogFile.Error, "[POST�����������]{0}", param);

            return null;
        }


        /// <summary>
        /// ��ȡPOST�����������ݣ�AES���ܹ��Ĳ�����
        /// </summary>
        /// <param name="key">������Կ(��Ҫ��Կ)</param>
        /// <returns>�����ֵ����Ͳ���</returns>
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
            //LogHelper.WriteLog(LogFile.Error, "[POST�����������]{0}", param);

            return null;
        }

        /// <summary>
        /// ��jsonת����ָ����������
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
        /// ��ȡPOST�����������ݣ�AES���ܹ��Ĳ�����
        /// </summary>
        /// <param name="key">������Կ(��Ҫ��Կ)</param>
        /// <returns>���ؽ��ܺ��json��ʽ�ַ�������</returns>
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
                //LogHelper.WriteLog(LogFile.Error, "GetBinaryParam��������:" + ex.Message);
            }
            return null;
        }
        #endregion

        #region DES �ӽ���
        /// <summary>
        /// DES ����(Tiangeͨ�ü��ܷ���)
        /// </summary>
        /// <param name="strText">�ַ���</param>
        /// <param name="encryptKey">��Կ</param>
        /// <returns></returns>
        public static string DESEncrypt(string strText, string encryptKey)
        {
            StringBuilder ret = new StringBuilder();
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                //���ַ����ŵ�byte������

                //ԭ��ʹ�õ�UTF8���룬�Ҹĳ�Unicode�����ˣ�����
                byte[] inputByteArray = Encoding.Default.GetBytes(strText);

                //�������ܶ������Կ��ƫ����

                //ʹ�����������������Ӣ���ı�
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
                //LogHelper.WriteLog(LogFile.Trace, "DES����ʧ�ܣ�" + ex.Message);
            }
            return ret.ToString();
        }
        /// <summary>
        /// �����ַ���(Tiangeͨ�ý��ܷ���)
        /// </summary>
        /// <param name="this.inputString">�����ܵ��ַ���</param>
        /// <param name="decryptKey">��Կ</param>
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

                //�������ܶ������Կ��ƫ��������ֵ��Ҫ�������޸�
                des.Key = ASCIIEncoding.ASCII.GetBytes(decryptKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(decryptKey);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                //����StringBuild����CreateDecryptʹ�õ��������󣬱���ѽ��ܺ���ı����������
                StringBuilder ret = new StringBuilder();
                return System.Text.Encoding.Default.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        #endregion

        #region Base64 ����
        /// <summary>
        /// ���� Base64 ����
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToBase64(string text)
        {
            return Convert.ToBase64String(Encoding.Default.GetBytes(text));
        }

        /// <summary>
        /// ���� Base64 ����
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string FromBase64(string text)
        {
            return Encoding.Default.GetString(Convert.FromBase64String(text));
        }
        #endregion

        #region MD5 ����(����DiscuzNT)
        /// <summary>
        /// MD5����(����DiscuzNT)
        /// </summary>
        /// <param name="str">ԭʼ�ַ���</param>
        /// <returns>MD5���</returns>
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

        #region DES �ӽ���(����DiscuzNT)
        //Ĭ����Կ����
        private static byte[] _Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// DES�����ַ���(����DiscuzNT)
        /// </summary>
        /// <param name="encryptString">�����ܵ��ַ���</param>
        /// <param name="encryptKey">������Կ,Ҫ��Ϊ8λ</param>
        /// <returns>���ܳɹ����ؼ��ܺ���ַ���,ʧ�ܷ���Դ��</returns>
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
        /// DES�����ַ���(����DiscuzNT)
        /// </summary>
        /// <param name="decryptString">�����ܵ��ַ���</param>
        /// <param name="decryptKey">������Կ,Ҫ��Ϊ8λ,�ͼ�����Կ��ͬ</param>
        /// <returns>���ܳɹ����ؽ��ܺ���ַ���,ʧ�ܷ�Դ��</returns>
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

        #region AES �ӽ���(256λ)
        /// <summary>
        /// 256λAES����  
        /// </summary>
        /// <param name="toEncrypt">Ҫ���ܵ�����</param>
        /// <param name="EncryptKey">������Կ</param>
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
        /// �ַ���AES����
        /// </summary>
        /// <param name="decryptStr">����</param>
        /// <param name="key">��Կ</param>
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
                //LogHelper.WriteLog(LogFile.Trace, "AES����:" + ex.Message + "|" + decrypted + "|" + toEncryptArray.Length);
                return decrypted;
            }
            return decrypted;
        }

        /// <summary>
        /// byte[] AES����
        /// </summary>
        /// <param name="decryptStr"></param>
        /// <param name="key">������Կ</param>
        /// <returns></returns>
        public static string AESDecryptFromBytes(byte[] decryptByte, string key)
        {
           
            string decrypted = "";
            //�����Ч���޷����Ƴ���bytesΪ��
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
                LogHelper.WriteLog(LogFile.Error, "AES����Crypto:{0},{1},{2}", ex.Message, decrypted, decryptByte.Length);
            }
            finally
            {
                rj.Clear();
            }

            return decrypted;
        }
        #endregion

        #region ����������ת

        /// <summary>
        /// �� Stream ת�� byte[]
        /// </summary>
        /// <param name="stream">��</param>
        /// <returns></returns>
        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // ���õ�ǰ����λ��Ϊ���Ŀ�ʼ   
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// <summary>
        /// ��������ת��string
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>����string��������</returns>
        public static string StreamToString(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // ���õ�ǰ����λ��Ϊ���Ŀ�ʼ   
            stream.Seek(0, SeekOrigin.Begin);

            //byte[]ת��string��
            string str = Encoding.Default.GetString(bytes);
            return str;
        }
        #endregion
    }
}