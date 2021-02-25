using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BLL;
using Common;
using Model;
using BLL.Mongo;
using Newtonsoft.Json;

namespace WebAPI.Controllers
{
    [H5Error]
    public class RankController : BaseController
    {
        RankBLL rank = new RankBLL();
        UserInfoBLL user = new UserInfoBLL();

        #region [ 我的等级页面 ]
        /// <summary>
        /// 我的等级界面
        /// live.9158.com/rank/MyLevel?useridx=60068188
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="chkcode"></param>
        /// <returns></returns>
        [OutputCache(Duration = 60 * 10, VaryByParam = "useridx")]
        public ActionResult MyLevel(int useridx = 0, string chkcode = "", string token = "")
        {
            if (useridx <= 0) return View("Error");

            UserInfo u = user.GetLiveUserInfoByIdx(useridx);
            int grade = u.grade;

            int mylevel = AppDataBLL.Get_Grade_Rank(grade);

            double curExp = u.curexp;
            double nextExp = (grade + 1) * (grade) * (grade) * 30 * 2.5 - 1;
            double progress = Math.Floor(curExp / nextExp * 100);
            if (progress > 0 && progress < 3) progress = progress + 4;

            //ViewBag.useridx = useridx;
            //ViewBag.token = token;
            //ViewBag.apiversion = 1;

            ViewBag.curexp = u.curexp;
            ViewBag.nextExp = nextExp;

            ViewBag.levelValue = "value" + mylevel;
            ViewBag.color = "color" + mylevel;
            ViewBag.progress = progress;
            ViewBag.grade = grade;
            ViewBag.curGrade = "LV" + grade;
            ViewBag.nextGrade = "LV" + (grade + 1);

            //MongoService.Inseret_H5WebAccess(useridx, "MyLevel", "我的等级");

            return View();
        }
        #endregion

        #region [ 房间内守护排行榜 ]

        /// <summary>
        /// 粉丝奉献榜（视图）
        /// live.9158.com/Rank/UserWeekRank?useridx=60068188&curuseridx=60068188&photo=
        /// </summary>
        /// <returns></returns>
        public ActionResult UserWeekRank(int useridx = 0, int curuseridx = 0, string photo = "")
        {
            if (useridx <= 0) { return Redirect("http://live.9158.com"); }

            //MongoService.Inseret_H5WebAccess(curuseridx, "UserWeekRank", "粉丝奉献榜");
            return View();
        }

        /// <summary>
        /// 粉丝守护（视图）
        /// live.9158.com/Rank/GuardRank?useridx=60068188&toUseridx=24881999
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="curuseridx"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        //public ActionResult GuardRank(int useridx = 0, int toUseridx = 0, string token = "")
        //{
        //    return View();
        //}

        #endregion

        #region [ 总排行榜/周星 ]

        /// <summary>
        /// 消费，主播，家族排行榜页面
        /// live.9158.com/Rank/totalRank?ranktype=1&useridx=60068188&photo=
        /// </summary>
        /// <param name="ranktype"></param>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public ActionResult totalRank(string Random, int ranktype = 1, int useridx = 0, string photo = "", string showtype = "")
        {
            if (string.IsNullOrEmpty(photo)) photo = CookieHelper.GetCookieValue("photo");
            if (string.IsNullOrEmpty(photo)) photo = AppDataBLL.DefaultPhoto;

            if (useridx > 0 && Tools.numRegex.IsMatch(useridx.ToString()))
            {
                CookieHelper.SetCookie("useridx", useridx.ToString());
            }

            if (!string.IsNullOrEmpty(Random) && photo.IndexOf("http://") > -1)
            {
                CookieHelper.SetCookie("photo", photo);
            }

            ViewBag.ranktype = ranktype;
            ViewBag.useridx = useridx;
            ViewBag.photo = photo;
            ViewBag.showtype = showtype.Contains("hall") ? "hall" : "return";

            //MongoService.Inseret_H5WebAccess(useridx, "totalRank", "总榜");

            if (!string.IsNullOrEmpty(Random))
            {
                return View("giftStarRank", new { ranktype = 4 });
            }
            return View();
        }

        /// <summary>
        /// 礼物周星榜页面
        /// live.9158.com/Rank/giftStarRank?showtype=card&useridx={0}&photo={1}
        /// </summary>
        /// <returns></returns>
        public ActionResult giftStarRank(string showtype = "", int useridx = 0, string photo = "")
        {
            if (string.IsNullOrEmpty(photo)) photo = CookieHelper.GetCookieValue("photo");
            if (string.IsNullOrEmpty(photo)) photo = AppDataBLL.DefaultPhoto;

            if (useridx > 0 && Tools.numRegex.IsMatch(useridx.ToString()))
            {
                CookieHelper.SetCookie("useridx", useridx.ToString());
            }
            if (photo.IndexOf("http://") > -1)
            {
                CookieHelper.SetCookie("photo", photo);
            }

            ViewBag.useridx = useridx;
            ViewBag.photo = photo;
            ViewBag.showtype = showtype.Contains("hall") ? "hall" : "return";

            //MongoService.Inseret_H5WebAccess(useridx, "giftStarRank", "周星榜");

            return View();
        }

        #endregion

        #region [ 排行榜数据 ]

        /// <summary>
        /// 粉丝奉献榜数据
        /// live.9158.com/Rank/UserRankData?ranktype=3&useridx=60068188&curuseridx=60068188&page=1
        /// </summary>
        /// <param name="ranktype">1：周榜 2：月榜 3：总榜 4：分享 5：观看 6：日榜</param>
        /// <param name="useridx">当前主播idx</param>
        /// <param name="curuseridx">当前登陆用户idx</param>
        /// <returns></returns>
        public ActionResult UserRankData(int ranktype = 6, int useridx = 0, int curuseridx = 0, int page = 1)
        {
            int count = 0, _totalPage = 0;
            IEnumerable<RankInfo> ranklist = new List<RankInfo>();
            MyRankInfo _myRank = new MyRankInfo();
            RankInfo myRank = new RankInfo();

            Paging pi = new Paging();
            pi.pageIndex = page;
            pi.pageSize = 50;

            if (ranktype == 4)
            {
                ranklist = rank.Get_Share_WeekRank(curuseridx, useridx, ref _myRank)
                    .Skip((page - 1) * pi.pageSize).Take(pi.pageSize);
                count = _myRank.count;   
            }
            else if (ranktype == 5)
            {
                ranklist = rank.Get_Watch_WeekRank(curuseridx, useridx, ref _myRank)
                    .Skip((page - 1) * pi.pageSize).Take(pi.pageSize);
                count = _myRank.count;
            }
            else
            {
                ranklist = rank.Get_Rank_FansWardList(ranktype, curuseridx, useridx, pi, ref myRank);

                count = ranklist.Count();
            }
            _totalPage = Tools.GetPageCount(count, pi.pageSize);

            MobileResult ret = new MobileResult();

            ret.code = "100";
            ret.msg = "success";
            ret.data = new { rankList = ranklist, myRankInfo = _myRank, myRankData = myRank, totalCount = count, totalPage = _totalPage };

            return Json(ret, "text/html; charset=UTF-8", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 总榜获取数据
        /// live.9158.com/Rank/totalRankData?ranktype=2&timetype=2
        /// </summary>
        /// <param name="ranktype">排行榜类型   1：消费，2：主播，3：家族</param>
        /// <param name="timetype">排行时间类型 1：日榜，2：周榜，3：月榜，4：总榜</param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult totalRankData(int ranktype = 1, int timetype = 1, int page = 1, int pagesize = 25)
        {
            DateTime dtNowTime = DateTime.Now;//  Convert.ToDateTime("2017-05-9 02:12:31");//  

            //初始化日时间段
            string stardate = dtNowTime.ToShortDateString();
            string enddate = stardate + " 23:59:59";

            int _isShowlswkData = 0;//当前时间是否展示上周数据
            int _totalCount = 0, _totalPage = 0;

            //消费日榜一次性全展示
            if (ranktype == 1 && timetype == 1)
            {
                page = 1; pagesize = 200;
            }

            //时间标签
            if (timetype == 2)//周
            {
                stardate = TimeHelper.GetWeekFirstDayMon(dtNowTime).ToString();
                enddate = TimeHelper.GetWeekLastDaySun(dtNowTime).ToShortDateString() + " 23:59:59";
            }
            else if (timetype == 3)//月
            {
                TimeHelper.GetTheMonthFirst_Last(ref stardate, ref enddate);
            }
            else if (timetype == 4)//总(年度/消费总)
            {
                stardate = dtNowTime.AddMonths(-dtNowTime.Month + 1).AddDays(-dtNowTime.Day + 1).ToShortDateString();
                enddate = dtNowTime.ToString();
            }

            //每周一整天，周二0~4点 特效展示上一周(从周一到下周一凌晨4点的数据)前三名（因为数据统计在每周一4点开始统计）
            if (timetype == 2 && dtNowTime.DayOfWeek == DayOfWeek.Monday)
            {
                string tswkFirstDay = TimeHelper.GetWeekFirstDayMon(dtNowTime).ToString();
                string tswkLastDay = TimeHelper.GetWeekLastDaySun(dtNowTime).ToShortDateString() + " 23:59:59";

                stardate = DateTime.Parse(tswkFirstDay).AddDays(-7).ToString("yyyy-MM-dd");
                enddate = DateTime.Parse(tswkLastDay).AddDays(-6).ToString("yyyy-MM-dd") + " 04:00:00";

                _isShowlswkData = 1;
                _totalCount = 3;//每周一只展示上周周榜前三名用户
                pagesize = 3;   //每周一只展示上周周榜前三的数据
            }

            List<SaleRankInfo> _rankList = rank.GetTotalRankList(ranktype, timetype, stardate, enddate, page, pagesize, ref _totalCount);

            _totalPage = Tools.GetPageCount(_totalCount, pagesize);

            MobileResult mr = new MobileResult();

            mr.code = "100";
            mr.msg = "success";
            mr.data = new { rankList = _rankList, totalCount = _totalCount, totalPage = _totalPage, isShow = _isShowlswkData };

            return Json(mr, "text/html; charset=UTF-8", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 周星榜数据
        /// live.9158.com/Rank/starRankData?giftid=325&useridx=60068188
        /// </summary>
        /// <param name="datatype">1：上周周星用户，2：本周周星用户</param>
        /// <param name="giftid"></param>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public ActionResult starRankData(int datatype = 1, int giftid = 0, int useridx = 0)
        {
            MobileResult mr = new MobileResult();

            //查询上周周星
            if (datatype == 1)
            {
                List<WeekStarGift> lastWeekStarList = rank.GetLastWeekStarList().OrderBy(f => f.giftOrder).ToList();

                mr.code = "100";
                mr.msg = "success";
                mr.data = new { rankList = lastWeekStarList, totalCount = lastWeekStarList.Count };

                return Content(JsonConvert.SerializeObject(mr));
            }
            Paging page = new Paging();
            page.pageIndex = 1;
            page.pageSize = 50;

            var _tswkList = rank.GetthisWeekStarList(giftid, page);
            var _tswkListTop3 = _tswkList.Take(3);

            if (useridx.ToString().Length == 4) { useridx = user.GetUseridxByshortidx(useridx); }
            var _myRank = _tswkList.Find(f => useridx == f.useridx);

            mr.code = "100";
            mr.msg = "success";
            mr.data = new { curWeek = _tswkListTop3, myRank = _myRank, totalCount = _tswkListTop3.Count(), totalPage = 1 };

            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 守护排行榜数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GuardRankData(int useridx = 0, int toUseridx = 0)
        {
            int _totalCount = 0;
            var _rankList = rank.Get_MyWard(toUseridx, 50, ref _totalCount);

            MobileResult mr = new MobileResult();

            mr.code = "100";
            mr.msg = "success";
            mr.data = new { rankList = _rankList, totalCount = _totalCount };

            return Content(JsonConvert.SerializeObject(mr));
        }

        #endregion

        #region 总榜 ,房间内排行榜（已停用）

        /// <summary>
        /// 总榜页面(APP 2.7.0版本停用)
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 60)]
        public ActionResult WeekRank()
        {
            return View();
        }

        /// <summary>
        /// 总榜(数据接口APP 2.7.0版本停用)
        /// live.9158.com/Rank/Ranklist?ranktype=0&page=1
        /// </summary>
        /// <param name="ranktype">排行榜类型 0：周消费榜 1:主播排行榜 2:家族消费排行榜</param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [OutputCache(Duration = 60, VaryByParam = "ranktype;page")]
        public ActionResult RankList(int ranktype = 0, int pagesize = 15, int page = 1)
        {
            MobileResult mr = new MobileResult();
            string stardate = TimeHelper.GetWeekFirstDayMon(DateTime.Now).ToShortDateString();
            string enddate = TimeHelper.GetWeekLastDaySun(DateTime.Now).ToShortDateString() + " 23:59:59";
            DateTime dt1 = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 04:00:00");//当前时间是否是4点以前
            DateTime dt2 = DateTime.Now;//当前时间

            int _totalCount = 0;
            int _totalPage = 0;
            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday || (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday && dt2 < dt1))
            {
                stardate = DateTime.Parse(stardate).AddDays(-7).ToString("yyyy-MM-dd");
                enddate = DateTime.Parse(enddate).AddDays(-6).ToString("yyyy-MM-dd") + " 04:00:00";
                pagesize = 3;
            }
            List<SaleRankInfo> list = new List<SaleRankInfo>();
            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                _totalCount = 3;
            }
            _totalPage = Tools.GetPageCount(_totalCount, pagesize);

            mr.code = "100";
            mr.msg = "success";
            mr.data = new { rankList = list, total = _totalCount, totalPage = _totalPage };

            return Json(mr, "text/html; charset=UTF-8", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 房间内送礼物排行(IOS:,Android:)已停用
        /// live.9158.com/Rank/RoomRank?roomid=60068188&useridx=60068188
        /// </summary>
        /// <returns></returns>
        //[OutputCache(Duration = 60, VaryByParam = "roomid")]
        public ActionResult RoomRank(int roomid = 0, int useridx = 0, int pagesize = 15, int page = 1)
        {
            if (roomid <= 0 || useridx <= 0) return View("Error");

            if (!Request.IsAjaxRequest()) return View();

            MobileResult mr = new MobileResult();

            var _totalCount = 0;
            var _list = rank.GetRoomInSaleRank(roomid, page, pagesize, ref _totalCount);
            var _totalPage = Tools.GetPageCount(_totalCount, pagesize);

            mr.code = "100";
            mr.msg = "success";
            mr.data = new { rankList = _list, totalCount = _totalCount, totalPage = _totalPage };

            return Json(mr, "text/html; charset=UTF-8", JsonRequestBehavior.AllowGet);
        }

        #endregion

        /// <summary>
        /// 获取数据库时间
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTime()
        {
            DateTime time = LiveBLL.Get_ServerTime();
            TimeModel tm = TimeHelper.GetCurTime(time);

            return Content(JsonConvert.SerializeObject(tm));
        }

        public ActionResult test()
        {
            DateTime time = LiveBLL.Get_ServerTime();
            TimeModel tm = TimeHelper.GetCurTime(time);

            return View();
        }


        public ActionResult rankView()
        {

            return View();
        }
        
        /// <summary>
        ///  排行榜页面
        /// live.9158.com/Rank/totalRank?ranktype=1&useridx=60068188&photo=
        /// </summary>
        /// <param name="ranktype"></param>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public ActionResult meuRank(string Random, int ranktype = 1, int useridx = 0, string photo = "", string showtype = "")
        {
            if (string.IsNullOrEmpty(photo)) photo = CookieHelper.GetCookieValue("photo");
            if (string.IsNullOrEmpty(photo)) photo = AppDataBLL.DefaultPhoto;

            if (useridx > 0 && Tools.numRegex.IsMatch(useridx.ToString()))
            {
                CookieHelper.SetCookie("useridx", useridx.ToString());
            }

            if (!string.IsNullOrEmpty(Random) && photo.IndexOf("http://") > -1)
            {
                CookieHelper.SetCookie("photo", photo);
            }

            ViewBag.ranktype = ranktype;
            ViewBag.useridx = useridx;
            ViewBag.photo = photo;
            ViewBag.showtype = showtype.Contains("hall") ? "hall" : "return";

            //MongoService.Inseret_H5WebAccess(useridx, "totalRank", "总榜");

            if (!string.IsNullOrEmpty(Random))
            {
                return View("giftStarRank", new { ranktype = 4 });
            }
            return View();
        }
        /// <summary>
        /// 总榜获取数据
        /// live.imeyyo.com/Rank/meuRankData?ranktype=2&timetype=2
        /// </summary>
        /// <param name="ranktype">排行榜类型   1：消费，2：主播，3：家族</param>
        /// <param name="timetype">排行时间类型 0 日榜 1周榜 2 月榜 3 总榜	</param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult meuRankData(int ranktype = 1, int timetype = 1, int page = 1, int pagesize = 25)
        {

            List<Rank_v2> rankList = rank.getRankList(ranktype, timetype, 0);
            MobileResult mr = new MobileResult();

            mr.code = "100";
            mr.msg = "success";
            mr.data = new { rankList = rankList, totalCount = 10, totalPage = 1, isShow = 1 };

            return Json(mr, "text/html; charset=UTF-8", JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 趣播获取排行榜
        /// </summary>
        /// <param name="rankType">0 大神榜 1 富豪 2 主播</param>
        /// <param name="timeType"> 0（ 0 最高派彩  1 连红 2 胜率） >0 (0 日榜 1周榜 2 月榜 3 总榜	)</param>
        /// <returns></returns>
        public ActionResult rankDatabyType(int rankType, int timeType, int page = 1, int pagesize = 25) {
            MobileResult mr = new MobileResult();

            List<Rank_v2> rankList = rank.getRankList(rankType, timeType,0);
            mr.code = "100";
            mr.msg = "success";
            mr.data = new { rankList = rankList };
            return Content(JsonConvert.SerializeObject(mr));
        }

    }
}
