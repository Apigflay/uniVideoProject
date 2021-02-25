using BLL;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPI.Areas.H5.Controllers
{
    public class RankingController : Controller
    {
        //
        // GET: /H5/Ranking/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 明星榜
        /// https://live.9158.com/H5/Ranking/StarRank?useridx=0&photo=
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        public ActionResult StarRank(int useridx = 0, string photo = "", string areaid = "0")
        {
            if (areaid != "0" || useridx == 63583358)
            {
                string gotoURL = string.Format("https://tw1.livemiao.com/NewRank/giftStarRank?useridx={0}&photo={1}", useridx, photo);
                return Redirect(gotoURL);
            }

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

            return View();
        }

        /// <summary>
        /// 排行榜：主播，消费，家族
        /// https://live.9158.com/H5/Ranking/RankIndex?ranktype=1&useridx=0&photo=&areaid=1
        /// </summary>
        /// <param name="ranktype">1：消费，2：主播，3：家族</param>
        /// <param name="useridx"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        public ActionResult RankIndex(string random, int ranktype = 0, int useridx = 0, string areaid = "0")
        {
            if (areaid != "0" || useridx == 63583358)
            {
                string gotoURL = string.Format("https://tw1.livemiao.com/NewRank/totalRank?useridx={0}&ranktype={1}", useridx, ranktype);
                return Redirect(gotoURL);
            }

            //if (string.IsNullOrEmpty(photo)) photo = CookieHelper.GetCookieValue("photo");
            //if (string.IsNullOrEmpty(photo)) photo = AppDataBLL.DefaultPhoto;

            if (useridx > 0 && Tools.numRegex.IsMatch(useridx.ToString()))
            {
                CookieHelper.SetCookie("useridx", useridx.ToString());
            }

            //if (!string.IsNullOrEmpty(random) && photo.IndexOf("http://") > -1)
            //{
            //    CookieHelper.SetCookie("photo", photo);
            //}

            string title = GetRankTitle(ranktype);

            ViewBag.ranktype = ranktype;
            ViewBag.useridx = useridx;
            //ViewBag.photo = photo;
            ViewBag.title = title;
            ViewBag.rankTxt = GetRankTxt(ranktype);

            return View();
        }

        /// <summary>
        /// 粉丝奉献榜（视图）
        /// live.9158.com/H5/Ranking/FansRank?useridx=60068188&curuseridx=60068188&photo=
        /// </summary>
        /// <returns></returns>
        public ActionResult FansRank(int useridx = 0, int curuseridx = 0, string photo = "")
        {
            if (useridx <= 0) { return Redirect("http://live.imeyoo.com"); }

            ViewBag.title = GetRankTitle(5);

            return View();
        }
        public ActionResult LiushuiRank(int useridx=0)
        {
            
            //return View();
            //return Redirect("http://live.imeyoo.com/Rank/meuRank");
            return RedirectToAction("meuRank", "../Rank");
            //return View();
        }

        #region 方法

        /// <summary>
        /// 获取排行榜标题
        /// </summary>
        /// <param name="ranktype"></param>
        /// <returns></returns>
        private static string GetRankTitle(int ranktype)
        {
            string title = "排行榜";
            if (ranktype == 1)
            {
                title = Resources.Resource.title_consume;
            }
            else if (ranktype == 2)
            {
                title = Resources.Resource.title_anchor;
            }
            else if (ranktype == 3)
            {
                title = Resources.Resource.title_family;
            }
            else if (ranktype == 4)
            {
                title = Resources.Resource.title_star;
            }
            else if (ranktype == 5)
            {
                title = Resources.Resource.title_Vermicelli;
            }

            return title;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ranktype"></param>
        /// <returns>返回：消费，喵粮 翻译</returns>
        public static string GetRankTxt(int ranktype)
        {
            string label = string.Empty;
            if (ranktype == 3)
            {
                label = Resources.Resource.lbl_catfood;
            }
            else if (ranktype == 4)//明星榜
            {
                label = Resources.Resource.lbl_rank_num;
            }
            else
            {
                label = Resources.Resource.lbl_consume;
            }

            return label;
        }

        #endregion
    }
}
