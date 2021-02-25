using Common;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Model;

namespace DAL
{
    public class CommonDAL
    {
        #region 单键
        private static CommonDAL _instance;

        static CommonDAL()
        {
            if (_instance == null)
                _instance = new CommonDAL();
        }

        public static CommonDAL Instance
        {
            get { return _instance; }
        }
        #endregion

        /// <summary>
        /// 获取北京IP
        /// </summary>
        /// <returns></returns>
        public DataTable GetBeiJinIP_Data()
        {
            string sql = "[Live_Get_BeijingIPInt]";
            return DbHelper.GetTable(sql, null);
        }

        /// <summary>
        /// 获取屏蔽黑名单地区IP段
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<IPModel> GetBlackIP_Data(int? type, string province)
        {
            string sql = "[Live_Get_BlackIPInt]";
            SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@type",SqlDbType.Int,4,type),
                                //SqlHelper.MakeInParam("@province",SqlDbType.VarChar,20,province)
                                };
            DataTable dt = DbHelper.GetTable(DbHelper.conn63_IP, sql, sp);
            return RFHelper<IPModel>.ConvertToList(dt);
        }

        /// <summary>
        /// 根据IP获取详细信息
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public IPModel GetAddressByIP_Data(string ip)
        {
            string sql = "[GetAddressInfoByIP]";
            SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@IP",SqlDbType.VarChar,20,ip)
                                };
            DataTable dt = DbHelper.GetTable(DbHelper.conn63_IP, sql, sp);
            return RFHelper<IPModel>.GetEntity(dt);
        }

        /// <summary>
        /// 获取所有省份
        /// </summary>
        /// <returns></returns>
        //public DataTable Get_Province_Data()
        //{
        //    const string sql = "Live_Province_List";

        //    return SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql);
        //}

        /// <summary>
        /// 获取所有城市
        /// </summary>
        /// <returns></returns>
        //public DataTable Get_City_Data()
        //{
        //    const string sql = "Live_City_List";

        //    return SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql);
        //}
    }
}
