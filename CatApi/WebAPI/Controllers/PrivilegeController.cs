using System.Web.Mvc;

using Model;
using BLL;
using Common;

namespace WebAPI.Controllers
{
    public class PrivilegeController : Controller
    {
        UserInfoBLL user = new UserInfoBLL();

        /// <summary>
        /// 我的头衔页面
        /// live.9158.com/Privilege/index?useridx=60068188&inroom=0&areaid=1
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        [OutputCache(Duration = 60 * 10, VaryByParam = "useridx")]
        public ActionResult Index(int useridx = 0, int areaid = 0, int language = 0)
        {
            if (areaid != 0)
            {
                return Redirect(string.Format("https://tw1.livemiao.com/Privilege?useridx={0}&language={1}", useridx, language));
            }

            if (useridx <= 0) return View("Error");
            ViewBag.Level = 1;
            ViewBag.Vipname = "";

            UserInfo u = user.GetLiveUserInfoByIdx(useridx);
            if (user != null)
            {
                ViewBag.Level = u.level;
                ViewBag.Vipname = AppDataBLL.GetVipName(u.level);
            }
            
            return View();
        }

        /// <summary>
        /// 直播视频预览咨询
        /// live.9158.com/Privilege/LivePreview?useridx=60068188
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public ActionResult LivePreview(int useridx = 0)
        {
            if (useridx <= 0)
            {
                return View("Error");
            }
            LiveConfig lc = LiveBLL.Get_LiveConfigById(7);
            LiveConfig lc2 = LiveBLL.Get_LiveConfigById(8);

            ViewBag.vipLevel = lc.data;
            ViewBag.grade = lc2.data;
            ViewBag.vipName = AppDataBLL.GetVipName(int.Parse(lc.data));

            return View();
        }

    }
}
