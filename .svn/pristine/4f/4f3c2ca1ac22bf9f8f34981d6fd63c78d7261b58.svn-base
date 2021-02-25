using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Dynamic;
using Newtonsoft.Json.Converters;

namespace Common
{
    /// <summary>
    /// Json操作类

    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 构造

        /// </summary>
        public JsonHelper() { }
        public static IsoDateTimeConverter dtConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss" };

        /// <summary>
        /// 获取JSON支持的字符串(默认清除低序位ASCII字符)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string JsonString(string str)
        {
            if (str.Length > 0)
            {
                //str = str.Replace("\\", "\\\\");
                //str = str.Replace("/", "\\/");
                //str = str.Replace("'", "\\'");
                //str = str.Replace("\"", "\\\"");
                //str = str.Replace("\r\n", "\\n");
                //str = str.Replace("<", "&lt;");
                //str = str.Replace(">", "&gt;");
                str = Regex.Replace(str, "[\\|\\\\|'|\"|\r|\n|\t|\f]", delegate(Match ma)
                {
                    switch (ma.Value)
                    {
                        case "\\":
                            return "\\\\";
                        case "'":
                            return "\\'";
                        case "\"":
                            return "\\\"";
                        case "\r":
                            return "\\r";
                        case "\n":
                            return "\\n";
                        case "\t":
                            return "\\t";
                        case "\f":
                            return "\\f";
                        default:
                            return ma.Value;
                    }
                }, RegexOptions.Compiled | RegexOptions.Singleline);
                str = Regex.Replace(str, @"[\x00-\x08\x0b-\x0c\x0e-\x1f\x7f]", "", RegexOptions.IgnoreCase);
                str = Regex.Replace(str, "[\u2E80-\u9FFF]|[\uFE30-\uFFA0]", delegate(Match ma)
                {
                    return HttpUtility.UrlEncodeUnicode(ma.Value).Replace("%u", "\\u").Replace("%", "\\x");
                }, RegexOptions.Compiled | RegexOptions.Singleline);
                //str = str.Replace(Convert.ToChar(10).ToString(), "\\n");
            }
            return str;
        }

        /// <summary>
        /// 获取JSON支持的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isUniconde">中文字符是否Uniconde</param>
        /// <returns></returns>
        public static string JsonString(string str, bool isUniconde)
        {
            if (str.Length > 0)
            {
                str = Regex.Replace(str, "[\\|\\\\|'|\"|\r|\n|\t|\f]", delegate(Match ma)
                {
                    switch (ma.Value)
                    {
                        case "\\":
                            return "\\\\";
                        case "'":
                            return "\\'";
                        case "\"":
                            return "\\\"";
                        case "\r":
                            return "\\r";
                        case "\n":
                            return "\\n";
                        case "\t":
                            return "\\t";
                        case "\f":
                            return "\\f";
                        //case "<":
                        //    return "&lt;";
                        //case ">":
                        //    return "&gt;";
                        default:
                            return ma.Value;
                    }
                }, RegexOptions.Compiled | RegexOptions.Singleline);
                str = Regex.Replace(str, @"[\x00-\x08\x0b-\x0c\x0e-\x1f\x7f]", "", RegexOptions.IgnoreCase);
                if (isUniconde)
                {
                    str = Regex.Replace(str, "[\u2E80-\u9FFF]|[\uFE30-\uFFA0]", delegate(Match ma)
                    {
                        return HttpUtility.UrlEncodeUnicode(ma.Value).Replace("%u", "\\u").Replace("%", "\\x");
                    }, RegexOptions.Compiled | RegexOptions.Singleline);
                }
            }
            return str;
        }

        #region JSON序列化

        /// <summary>
        /// JSON序列化

        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static string Serialize(DataRow dr)
        {
            if (dr == null || dr.Table.Columns.Count <= 0)
            {
                return "{}";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{");
                for (int i = 0; i < dr.Table.Columns.Count; i++)
                {
                    DataColumn dc = dr.Table.Columns[i];
                    sb.Append("\"" + dc.ColumnName.ToLower() + "\":");
                    if ((dc.DataType == typeof(Int32) || dc.DataType == typeof(Int64) || dc.DataType == typeof(Int16)) &&
                        !dr[i].Equals(DBNull.Value))
                    {
                        sb.AppendFormat("{0}", dr[i]);
                    }
                    else
                    {
                        sb.AppendFormat("\"{0}\"", JsonString(dr[i].ToString()));
                    }

                    if (i < dr.Table.Columns.Count - 1)
                    {
                        sb.Append(",");
                    }
                }
                sb.Append("}");
                return sb.ToString();
            }
        }

        /// <summary>
        /// JSON序列化

        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string Serialize(DataRow[] drs)
        {
            if (drs == null || drs.Length <= 0)
            {
                return "[]";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                for (int i = 0; i < drs.Length; i++)
                {
                    sb.Append(Serialize(drs[i]));
                    if (i < drs.Length - 1)
                    {
                        sb.Append(",");
                    }
                }
                sb.Append("]");
                return sb.ToString();
            }
        }

        /// <summary>
        /// JSON序列化

        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string Serialize(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
            {
                return "[]";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append(Serialize(dt.Rows[i]));
                    if (i < dt.Rows.Count - 1)
                    {
                        sb.Append(",");
                    }
                }
                sb.Append("]");
                return sb.ToString();
            }
        }


        /// <summary>
        /// JSON序列化

        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static string Serialize(DataSet ds)
        {
            if (ds == null || ds.Tables.Count <= 0)
            {
                return "[]";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    //sb.AppendFormat("\"table{0}\":", i);
                    sb.Append(Serialize(ds.Tables[i]));
                    if (i < ds.Tables.Count - 1)
                    {
                        sb.Append(",");
                    }
                }
                sb.Append("]");
                return sb.ToString();
            }
        }

        /// <summary>
        /// JSON序列化

        /// </summary>
        /// <param name="t">类</param>
        /// <returns></returns>
        public static string Serialize<T>(T t)
        {
            if (t == null)
            {
                return "{}";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                System.Reflection.PropertyInfo[] properties = typeof(T).GetProperties();

                sb.Append("{");
                for (int i = 0; i < properties.Length; i++)
                {
                    object value = properties[i].GetValue(t, null);
                    sb.AppendFormat("\"{0}\":\"{1}\"", properties[i].Name.ToLower(),
                                    JsonString(value == null ? string.Empty : value.ToString()));
                    if (i < properties.Length - 1)
                    {
                        sb.Append(",");
                    }
                }
                sb.Append("}");

                return sb.ToString();
            }
        }

        /// <summary>
        /// JSON序列化

        /// </summary>
        /// <param name="li">类集合</param>
        /// <returns></returns>
        public static string Serialize<T>(List<T> li)
        {
            if (li == null || li.Count <= 0)
            {
                return "[]";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                for (int i = 0; i < li.Count; i++)
                {
                    sb.Append(Serialize<T>(li[i]));
                    if (i < li.Count - 1)
                    {
                        sb.Append(",");
                    }
                }
                sb.Append("]");

                return sb.ToString();
            }
        }

        /// <summary>
        /// DataTable转换为json,管理后台专用格式。有总数。
        /// 返回json字符串格式[{"name":"jack","name":"rose"},{"count":"100"}]
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="count">总数</param>
        /// <returns>返回json字符串格式:[{"name":"jack","name":"rose"},{"count":"100"}]</returns>
        public static string DataTableToJson(DataTable dt, int count)
        {
            StringBuilder jsonString = new StringBuilder();
            if (dt == null || dt.Rows.Count <= 0)
            {
                return null;
            }
            jsonString.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j < dt.Columns.Count - 1)
                    {
                        jsonString.AppendFormat("\"{0}\":" + "\"{1}\",", dt.Columns[j].ColumnName, dt.Rows[i][j]);
                    }
                    else if (j == dt.Columns.Count - 1)
                    {
                        jsonString.AppendFormat("\"{0}\":" + "\"{1}\"", dt.Columns[j].ColumnName, dt.Rows[i][j]);
                    }
                }
                jsonString.Append(i == dt.Rows.Count - 1 ? "} " : "}, ");
            }
            jsonString.AppendFormat(",{{\"count\":\"{0}\"}}]", count);
            return jsonString.ToString();
        }
        /// <summary>
        /// DataTable转换为json,管理后台专用格式。无总数。
        /// 返回json字符串格式[{"name":"jack","name":"rose"},{"count":"100"}]
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToJson(DataTable dt)
        {
            StringBuilder jsonString = new StringBuilder();
            if (dt == null || dt.Rows.Count <= 0)
            {
                return null;
            }
            jsonString.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j < dt.Columns.Count - 1)
                    {
                        jsonString.AppendFormat("\"{0}\":" + "\"{1}\",", dt.Columns[j].ColumnName, dt.Rows[i][j]);
                    }
                    else if (j == dt.Columns.Count - 1)
                    {
                        jsonString.AppendFormat("\"{0}\":" + "\"{1}\"", dt.Columns[j].ColumnName, dt.Rows[i][j]);
                    }
                }
                jsonString.Append(i == dt.Rows.Count - 1 ? "} " : "}, ");
            }
            jsonString.Append("]");
            return jsonString.ToString();
        }
        #endregion

        #region JSON反序列化动态对象
        public static dynamic DynamicConvertJson(string json)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.RegisterConverters(new JavaScriptConverter[] { new DynamicJsonConverter() });
            dynamic dy = jss.Deserialize(json, typeof(object)) as dynamic;
            return dy;
        }  
        /// <summary>
        /// JSON反序列化动态对象
        /// </summary>
        private class DynamicJsonConverter : JavaScriptConverter
        {
            public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
            {
                if (dictionary == null)
                    throw new ArgumentNullException("dictionary");

                if (type == typeof(object))
                {
                    return new DynamicJsonObject(dictionary);
                }

                return null;
            }

            public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override IEnumerable<Type> SupportedTypes
            {
                get { return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(object) })); }
            }
        }
        private class DynamicJsonObject : System.Dynamic.DynamicObject
        {
            private IDictionary<string, object> Dictionary { get; set; }

            public DynamicJsonObject(IDictionary<string, object> dictionary)
            {
                this.Dictionary = dictionary;
            }

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                result = this.Dictionary[binder.Name];

                if (result is IDictionary<string, object>)
                {
                    result = new DynamicJsonObject(result as IDictionary<string, object>);
                }
                else if (result is ArrayList && (result as ArrayList) is IDictionary<string, object>)
                {
                    result = new List<DynamicJsonObject>((result as ArrayList).ToArray().Select(x => new DynamicJsonObject(x as IDictionary<string, object>)));
                }
                else if (result is ArrayList)
                {
                    result = new List<object>((result as ArrayList).ToArray());
                }

                return this.Dictionary.ContainsKey(binder.Name);
            }
        }
        /*
        /// <summary>
        /// JSON反序列化
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            System.IO.MemoryStream ms = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }
        */
        #endregion
    }
}