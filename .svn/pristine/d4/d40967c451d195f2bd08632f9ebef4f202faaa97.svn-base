using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Model;
using BLL;
using Common;
using WebAPI.Controllers.Attribute;
using Model.Param;
using Newtonsoft.Json;

namespace WebAPI.Controllers
{
    public class FansController : BaseController
    {
        FansBLL fans = new FansBLL();
        UserInfoBLL user = new UserInfoBLL();

        /// <summary>
        /// 获取我的页面信息
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public ActionResult GetMyUserInfo()
        {
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            MobileResult mr = new MobileResult();
            if (dic == null) return new BaseController().ParamError("");

            var userIdx = Convert.ToInt32(dic["userIdx"]);
            var mod = user.GetMyUserInfo(userIdx);

            if (mod != null)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = mod;
            }
            return Json(mr, "text/html; charset=UTF-8", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///1：获取热门直播列表
        /// </summary>
        /// <returns></returns>
        //[OutputCache(Duration = 60, VaryByParam = "page")]
        public ActionResult GetHotLive(int ntype = 0, int apptype = 20, int page = 1)
        {
            int pagesize = 20;
            int counts = 0;

            var bl = new RoomBLL();
            var list = bl.getHotRank(ntype, apptype, page, pagesize, ref counts);
            var pagecounts = Tools.GetPageCount(counts, pagesize);

            MobileResult mr = new MobileResult();

            if (pagecounts != 0)
            {
                mr.code = "100";
                mr.msg = "操作成功";
                mr.data = new { counts = pagecounts, list = list };
            }

            return Json(mr, "text/html; charset=UTF-8", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 3：获取总贡献排行榜(2016-8-22 停用)
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 10 * 60, VaryByParam = "page")]
        public ActionResult GetSaleRank(int page = 1)
        {
            var list = "";

            MobileResult mr = new MobileResult();
            mr.code = "105";
            mr.msg = "操作成功";
            mr.data = list;
            return Json(mr, "text/html; charset=UTF-8", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 2：获取关注列表(在线的)
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMyOnlineFriendsList()
        {
            //CommonParam dicParam = CryptoHelper.GetAESBinaryModelParam<CommonParam>(CryptoHelper.Live_KEY);
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);

            MobileResult mr = new MobileResult();

            if (dic == null)
            {
                mr.code = "101";
                mr.msg = "Param Error";
                return Json(mr, "text/html; charset=UTF-8", JsonRequestBehavior.AllowGet);
            }

            int useridx = Convert.ToInt32(dic["userIdx"]);//dic.useridx;
            var list = fans.getMyOnlineFollowList(useridx);

            if (list != null && list.Count > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = list;
            }
            return Json(mr, "text/html; charset=UTF-8", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 我的关注列表(所有的)
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="operid"></param>
        /// <returns></returns>
        public ActionResult GetMyAllFriendsList()
        {
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            MobileResult mr = new MobileResult();

            if (dic != null && dic.ContainsKey("userIdx") && dic.ContainsKey("operid") && dic.ContainsKey("page"))
            {
                int pagesize = 1000;//客户端写死
                int _totalCount = 0;
                int _totalPage = 0;
                var userIdx = Convert.ToInt32(dic["userIdx"]);
                var operid = Convert.ToInt32(dic["operid"]);
                var page = Convert.ToInt32(dic["page"]);

                var list = fans.getMyFriendList(userIdx, operid, page, pagesize, ref _totalCount);

                _totalPage = Tools.GetPageCount(_totalCount, pagesize);

                if (list != null && list.Count > 0)
                {
                    mr.code = "100";
                    mr.msg = "success";
                    mr.data = new { counts = _totalCount, pageCount = _totalPage, list = list };
                }
            }
            return Json(mr, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 粉丝列表(2016-5-10 zhaorui)
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMyFansList()
        {
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            MobileResult mr = new MobileResult();

            if (dic == null) return new BaseController().ParamError();

            int totalCount = 0;
            int pageCount = 0;
            int pagesize = 20;//客户端1.0.3版本以下写死 20 
            var useridx = Convert.ToInt32(dic["userIdx"]);
            var page = Convert.ToInt32(dic["page"]);

            var list = fans.GetMyFansList_New(useridx, useridx, page, pagesize, ref totalCount);
            pageCount = Tools.GetPageCount(totalCount, pagesize);

            if (list.Count > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = new { counts = pageCount, totalNum = totalCount, list = list };
            }
            return Json(mr, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 关注/取消关注用户
        /// live.9158.com/Fans/SetFollowing
        /// </summary>
        /// <param name="type"></param>
        /// <param name="useridx"></param>
        /// <param name="fuseridx"></param>
        /// <returns></returns>
        //[Auth]
        public ActionResult SetFollowing()
        {
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null) return new BaseController().ParamError();

            var fuserIdx = Convert.ToInt32(dic["fuserIdx"]);//
            var userIdx = Convert.ToInt32(dic["userIdx"]);
            var type = Convert.ToInt32(dic["type"]);//1：关注，2：取消关注
            var deviceid = dic["deviceId"];
            var platform = dic.ContainsKey("platform") ? dic["platform"] : "miaobo";//miaopai

            //批量关注
            //if (type == 3)
            //{
            //    string[] strIdx = Tools.SplitString(dic["useridxx"].ToString(), ",");
            //    int len = strIdx.Length;
            //}

            int _followNum = 0, _fansNum = 0, _shortidx = 0, _realuidx = 0;
            var mr = new MobileResult();

            if (userIdx <= 0 || fuserIdx <= 0)
            {
                mr.code = "-2";
                mr.msg = "参数错误";
                return Json(mr);
            }
            int ret = fans.SetFollowing(type, userIdx, fuserIdx, deviceid);
            if (ret > 0)
            {
                mr.code = "100";
                mr.msg = "success";

                fans.Get_FansInfo(1, fuserIdx, ref _followNum, ref _fansNum);
            }
            else
            {
                mr.code = ret.ToString();//-2：已关注
                mr.msg = "操作失败";
            }
            _shortidx = userIdx.ToString().Length == 4 ? userIdx : user.Get_shortidxByUseridx(userIdx);

            mr.data = new { followNum = _followNum, _fansNum, shortidx = _shortidx };

            return Json(mr, "text/html; charset=UTF-8", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 是否关注
        /// </summary>
        /// <returns></returns>
        //[Auth]
        public ActionResult IsFollow()
        {
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            MobileResult mr = new MobileResult();
            if (dic != null)
            {
                var useridx = Convert.ToInt32(dic["userIdx"]);
                var fuserIdx = Convert.ToInt32(dic["fuserIdx"]);

                int ret = fans.IsFollow(useridx, fuserIdx);

                mr.code = "100";
                mr.msg = "操作成功";
                mr.data = ret;
            }

            return Json(mr, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 房间内送礼物排行  接口要改成GET请求
        /// </summary>
        /// <returns></returns>
        public ActionResult getRoomSaleList()
        {
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null) { return ParamError(); }

            MobileResult mr = new MobileResult();

            var roomid = Convert.ToInt32(dic["roomid"]);
            //var useridx = Convert.ToInt32(dic["userIdx"]);
            int page = 1;
            if (dic["page"] != null)
            {
                page = Convert.ToInt32(dic["page"]);
            }
            int pagesize = 15, totalCount = 0;
            var list = new RankBLL().GetRoomInSaleRank(roomid, page, pagesize, ref totalCount);

            mr.code = "100";
            mr.msg = "success";
            mr.data = list;

            return Json(mr, JsonRequestBehavior.AllowGet);
        }


    }
}
