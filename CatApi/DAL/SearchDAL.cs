using Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class SearchDAL
    {
        private DBContext db = new DBContext();

        #region 搜索

        /// <summary>
        /// 根据昵称搜索用户(主播)
        /// </summary>
        /// <param name="where">用户昵称</param>
        /// <returns></returns>
        public List<UserSearchInfo> Live_Search_Data(string keyword, int pageIndex, int pageSize,int Sex, ref int count)
        {
            SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@mayname",SqlDbType.VarChar,40,keyword),
                                SqlHelper.MakeInParam("@sex",SqlDbType.Int,4,Sex),
                                SqlHelper.MakeInParam("@pageIndex",SqlDbType.Int,4,pageIndex),
                                SqlHelper.MakeInParam("@pageSize",SqlDbType.Int,4,pageSize),
                                SqlHelper.MakeOutParam("@totalCount",SqlDbType.Int,4,0)
                                };
            //DataTable dt = DbHelper.GetTable("Live_Search", sp); //以家族名称进行模糊搜索
            DataTable dt = DbHelper.GetTable("Live_Search_New_MJ", sp); //高消耗
            //DataTable dt = DbHelper.GetDataTable("Live_Search_New_V2", sp);
            count = (int)sp[4].Value;

            return RFHelper<UserSearchInfo>.ConvertToList(dt);
        }

        /// <summary>
        /// 根据useridx精确搜索用户
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<UserSearchInfo> Live_SearchByIdx_Data(int useridx)
        {
            const string sql = "[Live_SearchByIdx]";

            SqlParameter[] sp = { 
                                SqlHelper.MakeInParam("@userIdx",SqlDbType.Int,4,useridx)
                                };
            DataTable dt = DbHelper.GetDataTable(sql, sp);

            return RFHelper<UserSearchInfo>.ConvertToList(dt);
        }

        /// <summary>
        /// 索引资源
        /// </summary>
        /// <returns></returns>
        public List<SearchUserInfo> Live_IndexSearch_Data()
        {
            const string sql = "[Live_IndexSearch]";

            DataTable dt = DbHelper.GetTable(sql, null);

            return RFHelper<SearchUserInfo>.ConvertToList(dt);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="searchdIdxs"></param>
        ///// <param name="idxs"></param>
        ///// <returns></returns>
        //public List<SearchUserInfo> Live_IndexSearchInfo_Data(string searchdIdxs, string idxs)
        //{
        //    const string sql = "[Live_IndexSearch_Byidxs]";

        //    SqlParameter[] sp = {
        //                        SqlHelper.MakeInParam("@searchIdxs",SqlDbType.VarChar,100000,searchdIdxs),
        //                        SqlHelper.MakeInParam("@idxs",SqlDbType.VarChar,100000,idxs)
        //                        };
        //    DataTable dt = DbHelper.GetTable(sql, sp);

        //    return RFHelper<SearchUserInfo>.ConvertToList(dt);
        //}

        /// <summary>
        /// 根据昵称搜索用户
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        //public List<SearchUserInfo> Live_Search(string where, int pageIndex, int pageSize, ref int count)
        //{
        //    count = 0;
        //    var p = new DynamicParameters();
        //    p.Add("@nickName", where);
        //    p.Add("@PageIndex", pageIndex);
        //    p.Add("@PageSize", pageSize);
        //    p.Add("@pageCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
        //    var list = new List<SearchUserInfo>();

        //    list = db.Write(
        //        q => q.Query<SearchUserInfo>("[Live_Search]", p, commandType: CommandType.StoredProcedure).ToList()
        //        );
        //    count = p.Get<int>("@pageCount");

        //    return list;
        //}
        #endregion
    }
}
