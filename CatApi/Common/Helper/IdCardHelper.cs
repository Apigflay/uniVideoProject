/******************************************
* 文 件 名：IdCardHelper
* Copyright(c) live.9158.com
* 创 建 人：zhaorui
* 创建日期：2017年3月
* 文件描述：身份证帮助类
* 参考地址：http://phgzs.com/post/shenfenzheng.html
******************************************
* 修 改 人：
* 修改日期：
* 备注描述：
*****************************************/

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Common
{
    public class IdCardHelper
    {
        #region 私有变量（属性）
        /// <summary>
        /// 中国公民身份证号码最小长度。
        /// </summary>
        private static int CHINA_ID_MIN_LENGTH = 15;

        /// <summary>
        /// 中国公民身份证号码最大长度。
        /// </summary>
        private static int CHINA_ID_MAX_LENGTH = 18;

        /// <summary>
        /// 每位加权因子
        /// </summary>
        private static int[] power = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };

        /// <summary>
        /// 第18位校检码
        /// </summary>
        private static string[] verifyCode = new string[] { "1", "0", "X", "9", "8", "7", "6", "5", "4", "3", "2" };

        /// <summary>
        /// 最低年限
        /// </summary>
        private static int MIN = 1930;

        /// <summary>
        /// 国内身份证校验
        /// </summary>
        private static Dictionary<string, string> cityCodes = null;

        /// <summary>
        /// 台湾身份证校验
        /// </summary>
        private static Dictionary<string, int> twFirstCode = null;

        /// <summary>
        /// 香港身份证校验
        /// </summary>
        private static Dictionary<string, int> hkFirstCode = null;

        #endregion

        #region 静态构造函数
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static IdCardHelper()
        {
            #region 初始化cityCodes
            cityCodes = new Dictionary<string, string>();
            cityCodes.Add("11", "北京");
            cityCodes.Add("12", "天津");
            cityCodes.Add("13", "河北");
            cityCodes.Add("14", "山西");
            cityCodes.Add("15", "内蒙古");
            cityCodes.Add("21", "辽宁");
            cityCodes.Add("22", "吉林");
            cityCodes.Add("23", "黑龙江");
            cityCodes.Add("31", "上海");
            cityCodes.Add("32", "江苏");
            cityCodes.Add("33", "浙江");
            cityCodes.Add("34", "安徽");
            cityCodes.Add("35", "福建");
            cityCodes.Add("36", "江西");
            cityCodes.Add("37", "山东");
            cityCodes.Add("41", "河南");
            cityCodes.Add("42", "湖北");
            cityCodes.Add("43", "湖南");
            cityCodes.Add("44", "广东");
            cityCodes.Add("45", "广西");
            cityCodes.Add("46", "海南");
            cityCodes.Add("50", "重庆");
            cityCodes.Add("51", "四川");
            cityCodes.Add("52", "贵州");
            cityCodes.Add("53", "云南");
            cityCodes.Add("54", "西藏");
            cityCodes.Add("61", "陕西");
            cityCodes.Add("62", "甘肃");
            cityCodes.Add("63", "青海");
            cityCodes.Add("64", "宁夏");
            cityCodes.Add("65", "新疆");
            cityCodes.Add("71", "台湾");
            cityCodes.Add("81", "香港");
            cityCodes.Add("82", "澳门");
            cityCodes.Add("91", "国外");
            #endregion

            #region 初始化twFirstCode
            twFirstCode = new Dictionary<string, int>();
            twFirstCode.Add("A", 10);
            twFirstCode.Add("B", 11);
            twFirstCode.Add("C", 12);
            twFirstCode.Add("D", 13);
            twFirstCode.Add("E", 14);
            twFirstCode.Add("F", 15);
            twFirstCode.Add("G", 16);
            twFirstCode.Add("H", 17);
            twFirstCode.Add("J", 18);
            twFirstCode.Add("K", 19);
            twFirstCode.Add("L", 20);
            twFirstCode.Add("M", 21);
            twFirstCode.Add("N", 22);
            twFirstCode.Add("P", 23);
            twFirstCode.Add("Q", 24);
            twFirstCode.Add("R", 25);
            twFirstCode.Add("S", 26);
            twFirstCode.Add("T", 27);
            twFirstCode.Add("U", 28);
            twFirstCode.Add("V", 29);
            twFirstCode.Add("X", 30);
            twFirstCode.Add("Y", 31);
            twFirstCode.Add("W", 32);
            twFirstCode.Add("Z", 33);
            twFirstCode.Add("I", 34);
            twFirstCode.Add("O", 35);
            #endregion

            #region 初始化hkFirstCode
            hkFirstCode = new Dictionary<string, int>();
            hkFirstCode.Add("A", 1);
            hkFirstCode.Add("B", 2);
            hkFirstCode.Add("C", 3);
            hkFirstCode.Add("R", 18);
            hkFirstCode.Add("U", 21);
            hkFirstCode.Add("Z", 26);
            hkFirstCode.Add("X", 24);
            hkFirstCode.Add("W", 23);
            hkFirstCode.Add("O", 15);
            hkFirstCode.Add("N", 14);
            #endregion

        }
        #endregion

        #region 私有工具方法
        /// <summary>
        /// 数字验证
        /// </summary>
        /// <param name="val">要验证的字符串</param>
        /// <returns></returns>
        private static bool isNum(string val)
        {
            return string.IsNullOrEmpty(val) ? false : Regex.IsMatch(val, @"^[0-9]*$");
        }

        /// <summary>
        /// 将power和值与11取模获得余数进行校验码判断
        /// </summary>
        /// <param name="iSum">int iSum</param>
        /// <returns>校验位</returns>
        private static string getCheckCode18(int iSum)
        {
            string sCode = "";
            switch (iSum % 11)
            {
                case 10:
                    sCode = "2";
                    break;
                case 9:
                    sCode = "3";
                    break;
                case 8:
                    sCode = "4";
                    break;
                case 7:
                    sCode = "5";
                    break;
                case 6:
                    sCode = "6";
                    break;
                case 5:
                    sCode = "7";
                    break;
                case 4:
                    sCode = "8";
                    break;
                case 3:
                    sCode = "9";
                    break;
                case 2:
                    sCode = "x";
                    break;
                case 1:
                    sCode = "0";
                    break;
                case 0:
                    sCode = "1";
                    break;
            }
            return sCode;
        }

        /// <summary>
        /// 将身份证的每位和对应位的加权因子相乘之后，再得到和值
        /// </summary>
        /// <param name="sArr">sArr</param>
        /// <returns>身份证编码</returns>
        private static int getPowerSum(string sArr)
        {
            int iSum = 0;
            int power_len = power.Length;
            int iarr_len = sArr.Length;
            if (power_len == iarr_len)
            {
                for (int i = 0; i < iarr_len; i++)
                {
                    for (int j = 0; j < power_len; j++)
                    {
                        if (i == j)
                        {
                            iSum = iSum + Convert.ToInt32(sArr.Substring(i, 1)) * power[j];
                            break;
                        }
                    }
                }
            }
            return iSum;
        }

        /// <summary>
        /// 验证小于当前日期 是否有效
        /// </summary>
        /// <param name="iYear">待验证日期(年)</param>
        /// <param name="iMonth">待验证日期(月 1-12)</param>
        /// <param name="iDate">待验证日期(日)</param>
        /// <returns>是否有效</returns>
        private static bool valiDate(int iYear, int iMonth, int iDate)
        {
            int year = System.DateTime.Now.Year;
            if (iYear < MIN || iYear >= year)
            {
                return false;
            }
            if (iMonth < 1 || iMonth > 12)
            {
                return false;
            }
            int datePerMonth = 31;
            switch (iMonth)
            {
                case 4:
                    datePerMonth = 30;
                    break;
                case 6:
                    datePerMonth = 30;
                    break;
                case 9:
                    datePerMonth = 30;
                    break;
                case 11:
                    datePerMonth = 30;
                    break;
                case 2:
                    bool dm = ((iYear % 4 == 0 && iYear % 100 != 0) || (iYear % 400 == 0)) && (iYear > MIN && iYear < year);
                    datePerMonth = dm ? 29 : 28;
                    break;
                default:
                    datePerMonth = 31;
                    break;
            }
            return (iDate >= 1) && (iDate <= datePerMonth);
        }

        /// <summary>
        /// 根据身份编号获取户籍省份
        /// </summary>
        /// <param name="idCard">身份编号</param>
        /// <returns>户籍省份</returns>
        private static string getProvinceByIdCard(string idCard)
        {
            int len = idCard.Length;
            string sProvince = null;
            string sProvinNum = "";
            if (len == CHINA_ID_MIN_LENGTH || len == CHINA_ID_MAX_LENGTH)
            {
                sProvinNum = idCard.Substring(0, 2);
            }
            sProvince = cityCodes[sProvinNum];
            return sProvince;
        }

        /// <summary>
        /// 根据身份编号获取性别
        /// </summary>
        /// <param name="idCard">身份编号</param>
        /// <returns>性别(M-男，F-女，N-未知)</returns>
        private static string getGenderByIdCard(string idCard)
        {
            string sGender = "N";
            if (idCard.Length == CHINA_ID_MIN_LENGTH)
            {
                idCard = Conver15CardTo18(idCard);
            }
            string sCardNum = idCard.Substring(16, 1);
            if ((Convert.ToInt32(sCardNum) % 2 != 0))
            {
                sGender = "M";
            }
            else
            {
                sGender = "F";
            }
            return sGender;
        }

        /// <summary>
        /// 根据身份编号获取生日天
        /// </summary>
        /// <param name="idCard">身份编号</param>
        /// <returns>NULL string</returns>
        private static string getDateByIdCard(string idCard)
        {
            int len = idCard.Length;
            if (len < CHINA_ID_MIN_LENGTH)
            {
                return null;
            }
            else if (len == CHINA_ID_MIN_LENGTH)
            {
                idCard = Conver15CardTo18(idCard);
            }
            return idCard.Substring(12, 2);
        }

        /// <summary>
        /// 根据身份编号获取年龄
        /// </summary>
        /// <param name="idCard">idCard 身份编号</param>
        /// <returns>年龄</returns>
        private static int getAgeByIdCard(string idCard)
        {
            int iAge = 0;
            if (idCard.Length == CHINA_ID_MIN_LENGTH)
            {
                idCard = Conver15CardTo18(idCard);
            }
            string sYear = idCard.Substring(6, 4);
            int year = 0;
            if (int.TryParse(sYear, out year))
            {
                int iCurrYear = System.DateTime.Now.Year;
                iAge = iCurrYear - year;
            }

            return iAge;
        }
        #endregion

        #region 将15位身份证号码转换为18位
        /// <summary>
        /// 将15位身份证号码转换为18位
        /// </summary>
        /// <param name="idCard">15位身份编码</param>
        /// <returns>18位身份编码</returns>
        public static string Conver15CardTo18(string idCard)
        {
            string idCard18 = "";
            if (idCard.Length != CHINA_ID_MIN_LENGTH)
            {
                return null;
            }

            if (!isNum(idCard))
            {
                return null;
            }

            // 获取出生年月日
            string sYear = "19" + idCard.Substring(6, 2);
            idCard18 = idCard.Substring(0, 6) + sYear + idCard.Substring(8);
            // 转换字符数组
            string sArr = idCard18;
            if (sArr.Length != 0)
            {
                int iSum17 = getPowerSum(sArr);
                // 获取校验位
                string sVal = getCheckCode18(iSum17);
                if (sVal.Length > 0)
                {
                    idCard18 += sVal;
                }
                else
                {
                    return null;
                }
            }

            return idCard18;
        }
        #endregion

        #region 验证18位身份编码是否合法
        /// <summary>
        /// 验证18位身份编码是否合法
        /// </summary>
        /// <param name="idCard">身份编码</param>
        /// <returns>是否合法</returns>
        public static bool ValidateIdCard18(string idCard)
        {
            bool bTrue = false;
            if (idCard.Length == CHINA_ID_MAX_LENGTH)
            {
                // 前17位
                string code17 = idCard.Substring(0, 17);
                // 第18位
                string code18 = idCard.Substring(17, 1);
                if (isNum(code17))
                {
                    string sArr = code17;
                    if (sArr.Length != 0)
                    {
                        int iSum17 = getPowerSum(sArr);
                        // 获取校验位
                        string val = getCheckCode18(iSum17);
                        if (!string.IsNullOrEmpty(val) && val == code18)
                        {
                            bTrue = true;
                        }
                    }
                }
            }
            return bTrue;
        }
        #endregion

        #region 验证15位身份编码是否合法
        /// <summary>
        /// 验证15位身份编码是否合法
        /// </summary>
        /// <param name="idCard">身份编码</param>
        /// <returns>是否合法</returns>
        public static bool ValidateIdCard15(string idCard)
        {
            if (idCard.Length != CHINA_ID_MIN_LENGTH)
            {
                return false;
            }
            if (isNum(idCard))
            {
                string proCode = idCard.Substring(0, 2);
                if (!cityCodes.ContainsKey(proCode))
                {
                    return false;
                }
                // 升到18位
                idCard = Conver15CardTo18(idCard);
                return ValidateIdCard18(idCard);
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 验证身份证是否合法
        /// <summary>
        /// 验证身份证是否合法
        /// </summary>
        /// <param name="idCard">身份编码</param>
        /// <returns>是否合法</returns>
        public static bool ValidateCard(string idCard)
        {
            string card = idCard.Trim();
            if (ValidateIdCard18(card))
            {
                return true;
            }
            if (ValidateIdCard15(card))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 验证台湾身份证号码
        /// <summary>
        /// 验证台湾身份证号码
        /// 正则表达式/^[a-zA-Z][0-9]{9}$/
        /// </summary>
        /// <param name="idCard">身份证号码</param>
        /// <returns>是否符合</returns>
        public static bool ValidateTWCard(string idCard)
        {
            string start = idCard.Substring(0, 1);
            string mid = idCard.Substring(1, 8);
            string end = idCard.Substring(9, 1);
            int iStart = twFirstCode[start];
            int sum = iStart / 10 + (iStart % 10) * 9;
            int iflag = 8;
            for (int i = 0; i < mid.Length; i++)
            {
                sum = sum + Convert.ToInt32(mid.Substring(i, 1)) * iflag;
                iflag--;
            }
            return (sum % 10 == 0 ? 0 : (10 - sum % 10)).ToString() == end ? true : false;
        }
        #endregion

        #region [X]验证澳门身份证号码
        /// <summary>
        /// 验证澳门身份证号码(暂未找到实际的验证算法，先用正则代替)
        /// 正则表达式/^[1|5|7][0-9]{6}\(?[0-9A-Z]\)?$/
        /// </summary>
        /// <param name="idCard">身份证号码</param>
        /// <returns>是否符合</returns>
        public static bool ValidateAMCard(string idCard)
        {
            return Regex.IsMatch(idCard, @"^[1|5|7][0-9]{6}\(?[0-9A-Z]\)?$");
        }
        #endregion

        #region [X]验证香港身份证号码
        /// <summary>
        /// 验证香港身份证号码(暂未找到实际的验证算法，先用正则代替)
        /// 正则表达式/^[A-Z]{1,2}[0-9]{6}\(?[0-9A]\)?$/
        /// * 
        /// * 身份证前2位为英文字符，如果只出现一个英文字符则表示第一位是空格，对应数字58 前2位英文字符A-Z分别对应数字10-35
        /// * 最后一位校验码为0-9的数字加上字符"A"，"A"代表10
        /// * 
        /// * 
        /// * 将身份证号码全部转换为数字，分别对应乘9-1相加的总和，整除11则证件号码有效
        /// * 
        /// </summary>
        /// <param name="idCard">身份证号码</param>
        /// <returns>是否符合</returns>
        public static bool ValidateHKCard(string idCard)
        {
            return Regex.IsMatch(idCard, @"^[A-Z]{1,2}[0-9]{6}\(?[0-9A]\)$");
        }
        #endregion
    }
}
