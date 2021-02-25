using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DAL
{
    public class RFHelper<T> where T : new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IList<T> ConvertToModel(DataTable dt)
        {
            IList<T> ts = new List<T>();// 定义集合
            if (dt == null)
                return ts;

            Type type = typeof(T); // 获得此模型的类型
            string tempName = "";
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                PropertyInfo[] propertys = t.GetType().GetProperties();// 获得此模型的公共属性
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;
                    if (dt.Columns.Contains(tempName))
                    {
                        if (!pi.CanWrite) continue;
                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                ts.Add(t);
            }
            return ts;
        }

        /// <summary>
        /// 将datatable转换成List集合
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ConvertToList(DataTable dt)
        {
            List<T> ts = new List<T>();// 定义集合

            if (dt == null)
                return ts;

            Type type = typeof(T); // 获得此模型的类型
            string tempName = "";
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                PropertyInfo[] propertys = t.GetType().GetProperties();// 获得此模型的公共属性
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;
                    if (dt.Columns.Contains(tempName))
                    {
                        if (!pi.CanWrite) continue;
                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                ts.Add(t);
            }
            return ts;
        }

        /// <summary>
        /// 将dt,ds转换为单个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        //public static T GetEntity<T>(DataTable dt) where T : new()
        public static T GetEntity(DataTable dt)
        {
            T entity = new T();
            if ((dt != null) && (dt.Rows.Count > 0))
            {
                //返回单个实体，默认是取第一行的数据
                DataRow dr = dt.Rows[0];
                foreach (PropertyInfo item in typeof(T).GetProperties())
                {
                    if (dt.Columns.Contains(item.Name))
                    {
                        if (dr[item.Name] != DBNull.Value)
                        {
                            //item.SetValue(entity,dr[item.Name].ToString(), null);
                            item.SetValue(entity, Convert.ChangeType(dr[item.Name].ToString(), item.PropertyType), null);
                        }
                    }
                }
            }
            return entity;
        }
    }
}
