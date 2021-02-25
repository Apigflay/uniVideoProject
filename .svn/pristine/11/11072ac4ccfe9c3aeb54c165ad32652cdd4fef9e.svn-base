using System;
using System.Security.Cryptography;
using System.Text;

/******************************************
* 文 件 名：AESHelper
* Copyright(c) live.9158.com
* 创 建 人：zhaorui
* 创建日期：2017年8月
* 文件描述：AES加密解密帮助类(Android,IOS,.Net 通用)
******************************************
* 修 改 人：
* 修改日期：
* 备注描述：
*****************************************/
namespace Common
{
    public class AESHelper
    {
        public static string AES_Key = ConfigHelper.GetAppSettings("AESkey");// "hangzhoutiangeke";//key，可自行修改
        public static string iv = "0392039203920300"; //偏移量,可自行修改

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt, string key, string iv)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] ivArray = UTF8Encoding.UTF8.GetBytes(iv);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.BlockSize = 128;
            rDel.KeySize = 256;
            rDel.FeedbackSize = 128;
            rDel.Padding = PaddingMode.PKCS7;
            rDel.Key = keyArray;
            rDel.IV = ivArray;
            rDel.Mode = CipherMode.CBC;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt, string key, string iv)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] ivArray = UTF8Encoding.UTF8.GetBytes(iv);
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.IV = ivArray;
            rDel.Mode = CipherMode.CBC;
            rDel.Padding = PaddingMode.Zeros;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        //public static string Encrypt(string toEncrypt, string key, string iv)
        //{
        //    byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
        //    byte[] ivArray = UTF8Encoding.UTF8.GetBytes(iv);
        //    byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

        //    RijndaelManaged rDel = new RijndaelManaged();
        //    rDel.Key = keyArray;
        //    rDel.IV = ivArray;
        //    rDel.Mode = CipherMode.CBC;
        //    rDel.Padding = PaddingMode.Zeros;

        //    ICryptoTransform cTransform = rDel.CreateEncryptor();
        //    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        //    return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        //}

        //public static string Decrypt(string toDecrypt, string key, string iv)
        //{
        //    byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
        //    byte[] ivArray = UTF8Encoding.UTF8.GetBytes(iv);
        //    byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

        //    RijndaelManaged rDel = new RijndaelManaged();
        //    rDel.Key = keyArray;
        //    rDel.IV = ivArray;
        //    rDel.Mode = CipherMode.CBC;
        //    rDel.Padding = PaddingMode.Zeros;

        //    ICryptoTransform cTransform = rDel.CreateDecryptor();
        //    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        //    return UTF8Encoding.UTF8.GetString(resultArray);
        //}
    }
}
