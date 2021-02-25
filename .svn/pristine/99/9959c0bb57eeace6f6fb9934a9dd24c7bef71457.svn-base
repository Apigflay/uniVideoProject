using System.Collections.Generic;
using DAL;
using Model;
using Common;
using BLL.Logic;
using BLL.Mongo;
using System.Diagnostics;

namespace BLL
{
    public class SearchBLL
    {
        SearchDAL sService = new SearchDAL();

        /// <summary>
        /// Live搜索功能根据useridx,nickname搜索
        /// </summary>
        /// <param name="where">搜索关键字</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns>返回集合</returns>
        //public List<SearchUserInfo> Live_Search(string keyword, int pageIndex, int pageSize, ref int count)
        //{
        //    List<SearchUserInfo> dataList = null;

        //    int searchType = 0;//1：idx Search ，2：NickName Search
        //    int haveResult = 0;//1:Have Result，0：No Result

        //    According To useridx To Search
        //    if (Tools.numRegex.IsMatch(keyword) && keyword.Length >= 4 && keyword.Length <= 10)
        //    {
        //        searchType = 1;
        //        dataList = sService.Live_SearchByIdx_Data(int.Parse(keyword));

        //    }
        //    else//According To nickName To Search
        //    {
        //        searchType = 2;
        //        From lucene.net in Search

        //        List<int> idxList = LuceneHelper.searchBykeyword(keyword, count, ref count);
        //        string idxs = string.Join(",", idxList);
        //        From db in Search
        //        dataList = sService.Live_Search_Data(keyword, idxs, pageIndex, pageSize, ref count);
        //    }
        //    Keyword Statis to DataBase
        //    if (pageIndex == 1 && searchType == 2)
        //    {
        //        if (dataList != null && dataList.Count > 0)
        //        {
        //            haveResult = 1;
        //        }

        //        StatisticsBLL.Search_Statis(keyword, haveResult);
        //    }

        //    return dataList;
        //}
        public List<UserSearchInfo> Live_SearchNew(string keyword, int pageIndex, int pageSize,int Sex, ref int count)
        {
            List<UserSearchInfo> dataList = null;
            //根据useridx精确查询
            if (Tools.numRegex.IsMatch(keyword) && keyword.Length ==8)
            {
                dataList = sService.Live_SearchByIdx_Data(int.Parse(keyword));
            }
            else//myname模糊查询
            {
                dataList = sService.Live_Search_Data(keyword, pageIndex, pageSize, Sex, ref count);
            }
            return dataList;
        }

        /// <summary>
        /// Search based on useridx
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public UserSearchInfo Live_SearchByIdx(int useridx)
        {
            var dataList = sService.Live_SearchByIdx_Data(useridx);

            return (dataList != null && dataList.Count > 0) ? dataList[0] : null;
        }

        /// <summary>
        /// Index Resource
        /// </summary>
        /// <returns></returns>
        public List<SearchUserInfo> Live_IndexSearch()
        {
            return sService.Live_IndexSearch_Data();
        }

        //public List<SearchUserInfo> Live_IndexSearchInfo(string searchdIdxs, string idxs)
        //{
        //    return sService.Live_IndexSearchInfo_Data(searchdIdxs, idxs);
        //}
    }
}
