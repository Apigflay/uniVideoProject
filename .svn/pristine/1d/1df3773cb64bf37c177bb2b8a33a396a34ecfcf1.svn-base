using System.Collections.Generic;
using Model;
using DAL;
using Common;
using BLL.Mongo;

namespace BLL
{
    public class FansBLL
    {
        FansDAL dal = new FansDAL();
        UserInfoBLL user = new UserInfoBLL();

        /// <summary>
        /// 3：我的关注（在线的）
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<RoomOnline_V1> getMyOnlineFollowList(int useridx)
        {
            return dal.GetMyOnlineFollowList_Data(useridx);
        }
        /// <summary>
        /// 我的关注
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="operid"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="counts"></param>
        /// <returns></returns>
        public List<MyUserInfo> getMyFriendList(int useridx, int operid, int page, int pagesize, ref int counts)
        {
            return dal.GetMyFriendList(useridx, operid, page, pagesize, ref counts);
        }

        /// <summary>
        /// 我的粉丝
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="operid"></param>
        /// <returns></returns>
        public List<MyFansInfo> GetMyFansList_New(int useridx, int operid, int page, int pagesize, ref int count)
        {
            return dal.GetMyFansList_New(useridx, operid, page, pagesize, ref count);
        }

        /// <summary>
        /// 关注/取消关注
        /// </summary>
        /// <param name="type">1：关注，2：取消关注</param>
        /// <param name="useridx"></param>
        /// <param name="fuseridx"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public int SetFollowing(int type, int useridx, int fuseridx, string deviceid)
        {
            //var _baseinfo = user.GetLiveUserInfoByIdx(fuseridx);
            var _userip = Tools.GetRealIP();

            int ret = 0;
            if (useridx == fuseridx) return -99;
            //if (type == 1 && (_baseinfo.level < 15 || _baseinfo.grade < 6))
            //{
            //    //add time 2017-04-05 13:08:26you
            //    if (!CacheBLL.Verifyfollow(fuseridx) || !CacheBLL.VerifyfollowByIP(fuseridx))
            //    {
            //        LogHelper.WriteLog(LogFile.Warning, "【关注太过于频繁】" + fuseridx + "," + deviceid);
            //        return -1;
            //    }
            //}

            ret = dal.SetFollowing(type, useridx, fuseridx, _userip, deviceid);

            if (type == 2 && ret == 0)
            {
                LogHelper.WriteLog(LogFile.Log, "【取消关注失败】{0},{1},{2}", fuseridx, useridx, deviceid);
            }

            if (ret == 100)
            {
                //TODO MongoService.InsertSetFollow(type, fuseridx, useridx, deviceid);
            }

            return ret;
        }

        /// <summary>
        /// 是否关注
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="fuseridx"></param>
        /// <returns></returns>
        public int IsFollow(int useridx, int fuseridx)
        {
            return dal.IsFollow_Data(useridx, fuseridx);
        }

        /// <summary>
        /// 获取关注数，粉丝数
        /// </summary>
        /// <param name="opertype">1:直接输出 2:返回表格</param>
        /// <param name="useridx"></param>
        /// <param name="followNum"></param>
        /// <param name="fansNum"></param>
        public void Get_FansInfo(int opertype, int useridx, ref int followNum, ref int fansNum)
        {
            dal.Get_FansInfo_Data(opertype, useridx, ref followNum, ref fansNum);
        }
    }
}
