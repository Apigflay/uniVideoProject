using System;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections.Specialized;

namespace Common
{
    public static class TextHelper
    {
        /// <summary>
        /// 过滤特殊字符
        /// 如果字符串为空，直接返回。
        /// </summary>
        /// <param name="str">需要过滤的字符串</param>
        /// <returns>过滤好的字符串</returns>
        public static string FilterSpecial(string strHtml)
        {
            if (string.Empty == strHtml)
            {
                return strHtml;
            }
            string[] aryReg = { "select", "'", "delete", "?", "<", ">", "*", "%", "$", "#", "\"\"", ">=", "=<", ";", "||", "[", "]", "&", "|", " ", "''" };
            for (int i = 0; i < aryReg.Length; i++)
            {
                strHtml = strHtml.Replace(aryReg[i], string.Empty);
            }
            return strHtml;
        }

        public static string FilterSql(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            s = s.Trim().ToLower();
            //s = ClearScript(s);
            s = s.Replace("=", "");
            s = s.Replace("'", "");
            s = s.Replace(";", "");
            s = s.Replace(" or ", "");
            s = s.Replace("select", "");
            s = s.Replace("update", "");
            s = s.Replace("insert", "");
            s = s.Replace("delete", "");
            s = s.Replace("declare", "");
            s = s.Replace("exec", "");
            s = s.Replace("drop", "");
            s = s.Replace("create", "");
            s = s.Replace("%", "");
            s = s.Replace("--", "");
            return s;
        }

        /**/
        // /
        // / 转半角的函数(DBC case)
        // /
        // /任意字符串
        // /半角字符串
        // /
        // /全角空格为12288，半角空格为32
        // /其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        // /
        public static String ToDBC(String input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new String(c);
        }

        /// Code By MuseStudio@hotmail.com
        /// 2015-5-30
        /// 要转换的单个汉字/// 拼音声母
        public static string GetPYChar(string c)
        {
            byte[] array = new byte[2];
            array = System.Text.Encoding.Default.GetBytes(c);
            int i = (short)(array[0] - '\0') * 256 + ((short)(array[1] - '\0'));
            if (i < 0xB0A1) return "*";
            if (i < 0xB0C5) return "a";
            if (i < 0xB2C1) return "b";
            if (i < 0xB4EE) return "c";
            if (i < 0xB6EA) return "d";
            if (i < 0xB7A2) return "e";
            if (i < 0xB8C1) return "f";
            if (i < 0xB9FE) return "g";
            if (i < 0xBBF7) return "h";
            if (i < 0xBFA6) return "j";
            if (i < 0xC0AC) return "k";
            if (i < 0xC2E8) return "l";
            if (i < 0xC4C3) return "m";
            if (i < 0xC5B6) return "n";
            if (i < 0xC5BE) return "o";
            if (i < 0xC6DA) return "p";
            if (i < 0xC8BB) return "q";
            if (i < 0xC8F6) return "r";
            if (i < 0xCBFA) return "s";
            if (i < 0xCDDA) return "t";
            if (i < 0xCEF4) return "w";
            if (i < 0xD1B9) return "x";
            if (i < 0xD4D1) return "y";
            if (i < 0xD7FA) return "z";
            return "*";
        }

        #region HTML编解码
        /// <summary>
        /// HTML 编码(包括特殊字符,扩展 HtmlEncode)
        /// </summary>
        /// <param name="source">源内容</param>
        /// <param name="convertSpace">是否转换HTML</param>
        /// <param name="convertSpace">是否转换空格</param>
        /// <param name="convertLine">是否转换回车</param>
        public static string HtmlEncode(string source, bool convertHtml, bool convertSpace, bool convertLine)
        {
            if (convertHtml)
            {
                source = HttpUtility.HtmlEncode(source);
            }
            if (convertSpace)
            {
                source = Regex.Replace(source, @"(<[^<>]+>)([^<> ]*)([ ]+)([^<>]*)(<[^<>]+>)", "&nbsp;");
            }
            if (convertLine)
            {
                source = Regex.Replace(source, @"[\r\n|\r|\n]", "<br/>");
            }
            return source;
        }

        /// <summary>
        /// HTML 解码(包括特殊字符,扩展 HtmlDecode)
        /// </summary>
        /// <param name="source">源内容</param>
        /// <param name="convertBR">是否转换换行</param>
        public static string HtmlDecode(string source, bool convertBR)
        {
            source = HttpUtility.HtmlDecode(source);
            if (convertBR)
            {
                source = Regex.Replace(source, "<br>", Environment.NewLine, RegexOptions.IgnoreCase);
            }
            return source;
        }
        #endregion

        #region Substring
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="startString">开始字符串</param>
        /// <param name="endString">结束字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string Substring(string source, string startString, string endString)
        {
            string temp = source;
            if (startString != null && startString != string.Empty)
            {
                int start = temp.IndexOf(startString);
                if (start > -1)
                {
                    temp = temp.Substring(start, startString.Length);
                }
            }
            if (endString != null && endString != string.Empty)
            {
                int end = temp.IndexOf(endString);
                if (end > -1)
                {
                    temp = temp.Substring(0, end);
                }
            }
            return temp != source ? temp : string.Empty;
        }

        /// <summary>
        /// 截取字符串(字符串如果操过指定长度则将超出的部分用指定字符串代替)
        /// </summary>
        /// <param name="source">要检查的字符串</param>
        /// <param name="length">指定长度</param>
        /// <param name="tail">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string Substring(string source, int length, string tail)
        {
            return Substring(source, 0, length, tail);
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="source">要检查的字符串</param>
        /// <param name="startIndex">起始位置</param>
        /// <param name="length">指定长度</param>
        /// <param name="tail">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string Substring(string source, int startIndex, int length, string tail)
        {
            string myResult = source;
            //当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
            if (System.Text.RegularExpressions.Regex.IsMatch(source, "[\u0800-\u4e00]+") || System.Text.RegularExpressions.Regex.IsMatch(source, "[\xAC00-\xD7A3]+"))
            {
                //当截取的起始位置超出字段串长度时
                if (startIndex >= source.Length)
                {
                    return "";
                }
                else
                {
                    return source.Substring(startIndex, ((length + startIndex) > source.Length) ? (source.Length - startIndex) : length);
                }
            }
            if (length >= 0)
            {
                byte[] bsSrcString = Encoding.Default.GetBytes(source);
                //当字符串长度大于起始位置
                if (bsSrcString.Length > startIndex)
                {
                    int p_EndIndex = bsSrcString.Length;
                    //当要截取的长度在字符串的有效长度范围内
                    if (bsSrcString.Length > (startIndex + length))
                    {
                        p_EndIndex = length + startIndex;
                    }
                    else
                    {   //当不在有效范围内时,只取到字符串的结尾
                        length = bsSrcString.Length - startIndex;
                        tail = "";
                    }
                    int nRealLength = length;
                    int[] anResultFlag = new int[length];
                    byte[] bsResult = null;

                    int nFlag = 0;
                    for (int i = startIndex; i < p_EndIndex; i++)
                    {
                        if (bsSrcString[i] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3)
                            {
                                nFlag = 1;
                            }
                        }
                        else
                        {
                            nFlag = 0;
                        }
                        anResultFlag[i] = nFlag;
                    }
                    if ((bsSrcString[p_EndIndex - 1] > 127) && (anResultFlag[length - 1] == 1))
                    {
                        nRealLength = length + 1;
                    }
                    bsResult = new byte[nRealLength];
                    Array.Copy(bsSrcString, startIndex, bsResult, 0, nRealLength);
                    myResult = Encoding.Default.GetString(bsResult);
                    myResult = myResult + tail;
                }
            }

            return myResult;
        }

        /// <summary>
        /// 取指定长度的字符串
        /// </summary>
        /// <param name="source">要检查的字符串</param>
        /// <param name="startIndex">起始位置</param>
        /// <param name="length">指定长度</param>
        /// <param name="tail">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string Substring1(string source, int startIndex, int length, string tail)
        {
            string myResult = source;
            Byte[] bComments = Encoding.UTF8.GetBytes(source);
            foreach (char c in Encoding.UTF8.GetChars(bComments))
            {
                //当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
                //if (System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\u0800-\u4e00]+") || System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\xAC00-\xD7A3]+"))
                if ((c > '\u0800' && c < '\u4e00') || (c > '\xAC00' && c < '\xD7A3'))
                {
                    //当截取的起始位置超出字段串长度时
                    if (startIndex >= source.Length)
                    {
                        return "";
                    }
                    else
                    {
                        return source.Substring(startIndex, ((length + startIndex) > source.Length) ? (source.Length - startIndex) : length);
                    }
                }
            }
            if (length >= 0)
            {
                byte[] bsSrcString = Encoding.Default.GetBytes(source);
                //当字符串长度大于起始位置
                if (bsSrcString.Length > startIndex)
                {
                    int p_EndIndex = bsSrcString.Length;
                    //当要截取的长度在字符串的有效长度范围内
                    if (bsSrcString.Length > (startIndex + length))
                    {
                        p_EndIndex = length + startIndex;
                    }
                    else
                    {   //当不在有效范围内时,只取到字符串的结尾
                        length = bsSrcString.Length - startIndex;
                        tail = "";
                    }
                    int nRealLength = length;
                    int[] anResultFlag = new int[length];
                    byte[] bsResult = null;
                    int nFlag = 0;
                    for (int i = startIndex; i < p_EndIndex; i++)
                    {
                        if (bsSrcString[i] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3)
                            {
                                nFlag = 1;
                            }
                        }
                        else
                        {
                            nFlag = 0;
                        }
                        anResultFlag[i] = nFlag;
                    }
                    if ((bsSrcString[p_EndIndex - 1] > 127) && (anResultFlag[length - 1] == 1))
                    {
                        nRealLength = length + 1;
                    }
                    bsResult = new byte[nRealLength];
                    Array.Copy(bsSrcString, startIndex, bsResult, 0, nRealLength);
                    myResult = Encoding.Default.GetString(bsResult);
                    myResult = myResult + tail;
                }
            }

            return myResult;
        }
        #endregion

        #region UrlDecode, UrlEncode
        public static string UrlDecode(string code)
        {
            return System.Web.HttpUtility.UrlDecode(code);
        }
        public static string UrlEncode(string code)
        {
            return System.Web.HttpUtility.UrlEncode(code);
        }
        #endregion

        #region ConvertToUnicode
        /// <summary>   
        /// 把汉字字符转码为Unicode字符集   
        /// </summary>   
        /// <param name="strGB">要转码的字符</param>   
        /// <returns>转码后的字符</returns>   
        public static string ConvertToUnicode(string strGB)
        {
            //char[] chs = strGB.ToCharArray();
            //string result = string.Empty;
            //foreach (char c in chs)
            //{
            //    result += @"\\u" + char.ConvertToUtf32(c.ToString(), 0).ToString("x");
            //}
            //return result;

            string outStr = string.Empty;

            if (!string.IsNullOrEmpty(strGB))
            {
                for (int i = 0; i < strGB.Length; i++)
                {

                    outStr += "\\u" + ((int)strGB[i]).ToString("x");
                }
            }

            return outStr;

        }
        #endregion

        #region ConvertToGB2312
        /// <summary>
        /// 将Unicode编码转换为汉字字符串
        /// </summary>
        /// <param name="str">Unicode编码字符串</param>
        /// <returns>汉字字符串</returns>
        public static string ConvertToGB2312(string str)
        {
            string r = "";
            MatchCollection mc = Regex.Matches(str, @"\\u([\w]{2})([\w]{2})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            byte[] bts = new byte[2];
            foreach (Match m in mc)
            {
                bts[0] = (byte)int.Parse(m.Groups[2].Value, NumberStyles.HexNumber);
                bts[1] = (byte)int.Parse(m.Groups[1].Value, NumberStyles.HexNumber);
                r += Encoding.Unicode.GetString(bts);
            }
            return r;
        }
        #endregion

        public static string byteToString(byte[] byteArr)
        {
            return System.Text.Encoding.Default.GetString(byteArr);
        }

        public static byte[] StringToByte(string str)
        {
            return System.Text.Encoding.Default.GetBytes(str);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public static NameValueCollection GetQueryList(string queryString)
        {
            queryString = queryString.Replace("?", "");
            NameValueCollection result = new NameValueCollection(StringComparer.OrdinalIgnoreCase);
            if (!string.IsNullOrEmpty(queryString))
            {
                int count = queryString.Length;
                for (int i = 0; i < count; i++)
                {
                    int startIndex = i;
                    int index = -1;
                    while (i < count)
                    {
                        char item = queryString[i];
                        if (item == '=')
                        {
                            if (index < 0)
                            {
                                index = i;
                            }
                        }
                        else if (item == '&')
                        {
                            break;
                        }
                        i++;
                    }
                    string key = null;
                    string value = null;
                    if (index >= 0)
                    {
                        key = queryString.Substring(startIndex, index - startIndex);
                        value = queryString.Substring(index + 1, (i - index) - 1);
                    }
                    else
                    {
                        key = queryString.Substring(startIndex, i - startIndex);
                    }
                    result[key] = value;
                    if ((i == (count - 1)) && (queryString[i] == '&'))
                    {
                        result[key] = string.Empty;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 从字符串中提取所有字母
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetLetterString(string str)
        {
            return Regex.Replace(str, "[0-9]", string.Empty, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 从字符串中提取所有数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetNumberString(string str)
        {
            return Regex.Replace(str, "[A-Za-z]", string.Empty, RegexOptions.IgnoreCase);
        }
    }
}
