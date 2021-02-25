using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Drawing;
using System.Data;
using System.Collections.Generic;

namespace Common
{
    public class Tools
    {
        /// <summary>
        /// 手机号验证
        /// </summary>
        public static string TelRegex = @"^0?(13\d|14[5,7,9]|15[0-3,5-9]|17[0,1,3,5-8]|18\d)\d{8}$";

        public static Regex numRegex = new Regex(@"^\d+$");
        
        /// <summary>
        /// 验证输入字符串为数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumber(string str)
        {
            return Regex.IsMatch(str, "^([0]|([1-9]+\\d{0,}?))(.[\\d]+)?$");
        }

        /// <summary>
        /// 是否为汉字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsChinese(string str)
        {
            return Regex.IsMatch(str, @"[\u4e00-\u9fa5]+$");
        }

        public static bool IsCompanyIP
        {
            get
            {
                if (GetRealIP().Equals("115.231.93.68") || GetRealIP().Equals("127.0.0.1"))
                {
                    return true;
                }
                else
                    return false;
            }
        }

        public static bool IsDebug
        {
            get
            {
                return GetRealIP().Equals("127.0.0.1") ? true : false;
            }
        }

        /// <summary>
        /// 是否为字母加数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsLetterAndNumber(string str)
        {
            return Regex.IsMatch(str, @"^[a-zA-Z0-9]*$");
        }

        /// <summary>
        /// 是否为英文
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEnglish(string str)
        {
            return Regex.IsMatch(str, @"^[a-zA-Z]");
        }

        /// <summary>
        /// 验证输入字符串为电话号码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsPhone(string str)
        {
            return Regex.IsMatch(str, @"^0?(13\d|14[5,7,9]|15[0-3,5-9]|17[0,1,3,5-8]|18\d)\d{8}$");
        }

        /// <summary>
        /// 返回字符串真实长度, 1个汉字长度为2
        /// </summary>
        /// <returns>字符长度</returns>
        public static int GetStringLength(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }

        /// <summary>
        /// 分页返回总页数
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static int GetPageCount(int totalCount, int pageSize)
        {
            int pageCount = 0;
            if (totalCount % pageSize == 0)
            {
                pageCount = totalCount / pageSize;
            }
            else
            {
                pageCount = totalCount / pageSize + 1;
            }
            return pageCount;
        }

        #region IP 相关

        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetRealIP()
        {
            string result = System.Web.HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.ServerVariables["HTTP_VIA"];
            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(result) || !Regex.IsMatch(result, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$"))
                result = "127.0.0.1";

            return result;
        }

        /// <summary>
        /// 将IP地址格式化为整数型
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static long IpToInt(string ip)
        {
            char[] dot = new char[] { '.' };
            string[] ipArr = ip.Split(dot);
            if (ipArr.Length == 3) ip = ip + ".0";
            ipArr = ip.Split(dot);

            long ip_Int = 0;

            long p1 = long.Parse(ipArr[0]) * 256 * 256 * 256;
            long p2 = long.Parse(ipArr[1]) * 256 * 256;
            long p3 = long.Parse(ipArr[2]) * 256;
            long p4 = long.Parse(ipArr[3]);

            ip_Int = p1 + p2 + p3 + p4;

            return ip_Int;
        }

        /// <summary>
        /// 随机IP
        /// </summary>
        /// <returns></returns>
        public static string RandomIP()
        {
            Random r = new Random();

            int ipNum1 = r.Next(1, 255);
            int ipNum2 = r.Next(1, 255);
            int ipNum3 = r.Next(1, 255);
            int ipNum4 = r.Next(1, 255);

            string ip = ipNum1 + "." + ipNum2 + "." + ipNum3 + "." + ipNum4;

            return ip;
        }

        #endregion

        /// <summary>
        /// 是否是手机终端
        /// </summary>
        /// <returns></returns>
        public static bool IsMobile()
        {
            string agent = (HttpContext.Current.Request.UserAgent + "").ToLower().Trim();
            if (agent == "" ||
                agent.IndexOf("mobile", StringComparison.Ordinal) != -1 ||
                agent.IndexOf("mobi", StringComparison.Ordinal) != -1 ||
                agent.IndexOf("nokia", StringComparison.Ordinal) != -1 ||
                agent.IndexOf("samsung", StringComparison.Ordinal) != -1 ||
                agent.IndexOf("sonyericsson", StringComparison.Ordinal) != -1 ||
                agent.IndexOf("mot", StringComparison.Ordinal) != -1 ||
                agent.IndexOf("blackberry", StringComparison.Ordinal) != -1 ||
                agent.IndexOf("lg", StringComparison.Ordinal) != -1 ||
                agent.IndexOf("htc", StringComparison.Ordinal) != -1 ||
                agent.IndexOf("j2me", StringComparison.Ordinal) != -1 ||
                agent.IndexOf("ucweb", StringComparison.Ordinal) != -1 ||
                agent.IndexOf("opera mini", StringComparison.Ordinal) != -1 ||
                agent.IndexOf("mobi", StringComparison.Ordinal) != -1 ||
                agent.IndexOf("android", StringComparison.Ordinal) != -1 ||
                agent.IndexOf("iphone", StringComparison.Ordinal) != -1 ||
                agent.IndexOf("ipad", StringComparison.Ordinal) != -1 ||
                agent.IndexOf("ipod", StringComparison.Ordinal) != -1 ||
                agent.IndexOf("okhttp", StringComparison.Ordinal) != -1 ||
                agent.IndexOf("miao bo", StringComparison.Ordinal) != -1 ||
                agent.IndexOf("91", StringComparison.Ordinal) != -1
                )
            {
                //终端可能是手机
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否是喵播APP
        /// </summary>
        /// <returns></returns>
        public static bool IsMiaoboApp()
        {
            bool flag = false;

            string agent = HttpContext.Current.Request.UserAgent.ToLower().Trim();
            if (agent.IndexOf("miao bo", StringComparison.Ordinal) != -1)
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// 获取代理名称
        /// </summary>
        /// <returns></returns>
        public static string GetdeviceName()
        {
            string agent = HttpContext.Current.Request.UserAgent ?? string.Empty;

            if (null == agent) return "unknown";

            List<string> list = new List<string>() { "Android", "iPhone", "iPod", "iPad", "Ruby", "okhttp", "Windows" };

            string deviceName = list.Find(f => agent.ToLower().Contains(f.ToLower())) ?? string.Empty;

            if (string.IsNullOrEmpty(deviceName)) { return "unknow"; }

            if (deviceName.Equals("Ruby") || deviceName.Equals("okhttp"))
                deviceName = "Android";

            if (agent.Contains("91"))
                deviceName = "miaopai";
            else if (agent.ToLower().Contains("micromessenger"))
                deviceName = "WeiXin";
            else if (agent.ToLower().Contains("qq"))
                deviceName = "QQ";

            return deviceName;
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        public static string[] SplitString(string strContent, string strSplit)
        {
            if (!string.IsNullOrEmpty(strContent))
            {
                if (strContent.IndexOf(strSplit) < 0)
                    return new string[] { strContent };

                return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
            }
            else
                return new string[0] { };
        }

        /// <summary>
        /// DataTable分页
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="PageIndex">页索引,注意：从1开始</param>
        /// <param name="PageSize">每页大小</param>
        /// <returns>分好页的DataTable数据</returns>  
        public static DataTable GetPagedTable(DataTable dt, int PageIndex, int PageSize)
        {
            if (PageIndex == 0) { return dt; }
            DataTable newdt = dt.Copy();
            newdt.Clear();
            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;

            if (rowbegin >= dt.Rows.Count)
            { return newdt; }

            if (rowend > dt.Rows.Count)
            { rowend = dt.Rows.Count; }

            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow newdr = newdt.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }
            return newdt;
        }

        #region  根据用户IP 获取城市 拼音 缩写
        /// 汉字转拼音缩写
        /// 2015-5-29
        /// 要转换的汉字字符串/// 拼音缩写
        public static string GetPYString(string str)
        {
            string tempStr = "";
            foreach (char c in str)
            {
                if ((int)c >= 33 && (int)c <= 126)
                {
                    //字母和符号原样保留
                    tempStr += c.ToString();
                }
                else
                {
                    //累加拼音声母
                    tempStr += GetPYChar(c.ToString());
                }
            }
            return tempStr;
        }

        /// Code By MuseStudio@hotmail.com
        /// 2015-5-30
        /// 要转换的单个汉字/// 拼音声母
        public static string GetPYChar(string c)
        {
            byte[] array = new byte[2];
            array = Encoding.Default.GetBytes(c);
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
        #endregion

        /// <summary>
        /// 得到用户星座
        /// </summary>
        /// <param name="birthDay"></param>
        /// <returns></returns>
        public static string GetAtomFromBirthday(DateTime birthDay)
        {
            float fBirthDay = 0.00F;// Convert.ToSingle(dtBirthDay.ToString("M.dd"));
            if (birthDay.Month == 1 && birthDay.Day < 20)
            {
                fBirthDay = float.Parse(string.Format("13.{0}", birthDay.Day));
            }
            else
            {
                fBirthDay = float.Parse(string.Format("{0}.{1}", birthDay.Month, birthDay.Day));
            }
            float[] atomBound = { 1.20F, 2.20F, 3.21F, 4.21F, 5.21F, 6.22F, 7.23F, 8.23F, 9.23F, 10.23F, 11.21F, 12.22F, 13.20F };
            string[] atoms = { "水瓶座", "双魚座", "白羊座", "金牛座", "双子座", "巨蟹座", "獅子座", "处女座", "天秤座", "天蠍座", "射手座", "魔羯座" };
            string ret = "外星人";
            for (int i = 0; i < atomBound.Length - 1; i++)
            {
                if (atomBound[i] <= fBirthDay && atomBound[i + 1] > fBirthDay)
                {
                    ret = atoms[i];
                    break;
                }
            }
            return ret;
        }

        /// <summary>
        /// 后台图片上传
        /// </summary>
        /// <param name="files"></param>
        /// <param name="DirectoryName"></param>
        /// <param name="imageUrl1"></param>
        /// <param name="ImageUrl2"></param>
        /// <returns></returns>
        public static string SaveImgae(HttpFileCollection files, string DirectoryName, ref string imageUrl1, ref string ImageUrl2)
        {
            #region  保存图片
            string ImgUrl1 = "";
            string ImgUrl2 = "";
            for (int i = 0; i < files.AllKeys.Length; i++)
            {
                HttpPostedFile file = files[i];

                string imgname = Path.GetFileName(file.FileName).ToLower();
                if (imgname == null || imgname.Length == 0)
                {
                    //Response.Write("<script>alert(\"请上传宣传2M以内的图片\");history.go(-1);</script>");
                    return "请上传宣传2M以内的图片";
                }
                if (file.ContentLength > 2 * 1024 * 1024)
                {
                    //Response.Write("<script>alert(\"请上传2M内的图片\");history.go(-1);</script>");
                    return "请上传宣传2M以内的图片";
                }
                imgname = imgname.Substring(0, imgname.Length - 4);
                string PicType = Path.GetExtension(file.FileName).ToLower();    //获得图片的扩展名
                if (PicType != ".jpg" && PicType != ".jpeg" && PicType != ".png" && PicType != ".webp")
                {
                    //Response.Write("<script>alert(\"只能上传格式为（'.jpg','.jpeg','.png'）\");history.go(-1);</script>");
                    return "格式错误";
                }
                string PathUrl = DirectoryName + "/";

                int ss = new Random().Next(1, 9999);
                string phsavename = DateTime.Now.ToString("yyyyMMddHHmmss") + ss + PicType;
                string phserver = PathUrl + System.DateTime.Now.ToString("yyyyMMdd") + "/" + phsavename;
                if (i == 0)
                {
                    ImgUrl1 = phserver;
                }
                else if (i == 1)
                {
                    ImgUrl2 = phserver;
                }
                string saveserver = "D:/img.laowo.com/houtaiadmin/" + PathUrl + System.DateTime.Now.ToString("yyyyMMdd") + "/";

                try
                {
                    if (true)
                    {
                        if (!Directory.Exists(saveserver))  //如果不存在就创建file文件夹
                            Directory.CreateDirectory(saveserver);

                        using (System.Drawing.Image originalImage = System.Drawing.Image.FromStream(file.InputStream))
                        {
                            file.SaveAs(@"D:\img.laowo.com\houtaiadmin\" + DirectoryName + "\\" + System.DateTime.Now.ToString("yyyyMMdd") + "\\" + phsavename);//正式
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Loger.WriteLog(LogFile.Error, "上传文件报错:" + ex.Message.ToString());
                    return "上传出错";
                }
            }
            imageUrl1 = "http://img.imeyoo.com/houtaiadmin/" + ImgUrl1;//正式
            ImgUrl2 = "http://img.imeyoo.com/houtaiadmin/" + ImgUrl2;

            return "0";
            #endregion
        }
    }
}