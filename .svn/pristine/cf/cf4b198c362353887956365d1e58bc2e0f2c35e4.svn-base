using System;
using System.Web.Mvc;
using BLL;
using Model.View;
using WebAPI.Controllers;
using Common;
using Common.Core;
using Model;
using DAL;
using System.Web;
using BLL.Common;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebAPI.Areas.H5.Controllers
{
    [H5Error]
    public class UserController : Controller
    {
        public UserInfoBLL user = new UserInfoBLL();
        public RoomBLL _room = new RoomBLL();
        PayBLL pay = new PayBLL();
        private LiveDAL live = new LiveDAL();
        UserInfoBLL _userbll = new UserInfoBLL();

        /// <summary>
        /// 主播等级/管理
        /// live.9158.com/H5/User/AnchorLevel?useridx=63583358&token=WRUFiGWH76Ey3uD3oj3gF3VPa26hI&apiversion=2
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public ActionResult AnchorLevel(int useridx = 0, string token = "", int apiversion = 1)
        {
            //if (useridx <= 0 || string.IsNullOrEmpty(token)) return new BaseController().CommonView(1, "参数错误");

            string userTokenKey = CacheKeys.LIVE_USER_TOKEN + useridx;
            string userTokenValue = MemcachedHelper.MemGet(userTokenKey);

            if (string.IsNullOrEmpty(userTokenValue) || !token.Equals(userTokenValue))
            {
                return new BaseController().CommonView(1, "获取用户信息失败，请退出重新登录");
            }

            VMAnchorInfo vm = new VMAnchorInfo();

            vm.User = user.GetLiveUserInfoByIdx(useridx);
            vm.Anchor = user.Get_AnchorLevel_Info(useridx);
            vm.Task = user.Get_AnchorTask_Info(useridx);
            vm.Anchor.livingTime = _room.Get_UserLivingTime(useridx) / 60;

            vm.StageLevel = AppDataBLL.Get_AnchorGrade_Rank(vm.Anchor.anchorLevel);

            double curExp = vm.Anchor.curExp;
            double nextExp = vm.Anchor.nextExp;
            double progress = (curExp - vm.Anchor.beginExp) / vm.Anchor.upRequire;
            progress = Math.Floor(progress * 100);

            vm.Anchor.shortExp = (nextExp - curExp) + 1;
            vm.Progress = progress;

            ViewBag.isShowTask = vm.Anchor.anchorLevel > 0 ? 1 : 0;
            ViewBag.living_Rank = vm.StageLevel;

            //喵播全球版页面
            if (apiversion == 2)
            {
                return View("AnchorLevelNew", vm);
            }

            return View(vm);
        }

        /// <summary>
        /// 我的等级界面
        /// live.9158.com/H5/user/MyLevel?useridx=60068188&token={1}
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="chkcode"></param>
        /// <returns></returns>
        //[OutputCache(Duration = 60 * 10, VaryByParam = "useridx")]
        public ActionResult MyLevel(int useridx = 0,string token = "")
        {
            if (useridx <= 0) return View("Error");

            UserInfo u = user.GetLiveUserInfoByIdx(useridx);
            int grade = u.grade;

            int mylevel = AppDataBLL.Get_Grade_Rank(grade);

            double curExp = u.curexp;
            double nextExp = (grade + 1) * (grade) * (grade) * 30 * 2.5 - 1;
            double progress = Math.Floor(curExp / nextExp * 100);
            if (progress > 0 && progress < 3) progress = progress + 4;
            ViewBag.curexp = u.curexp;
            ViewBag.nextExp = nextExp;


            ViewBag.leaveValue = nextExp - u.curexp;
            ViewBag.progress = progress;
            ViewBag.grade = grade;
            ViewBag.curGrade = grade;
            ViewBag.nextGrade = (grade + 1);
            ViewBag.photo = u.smallpic;

            return View();
        }
        /// <summary>
        /// 联系客服
        /// </summary>
        /// <returns></returns>
        public  ActionResult Service()
        {
            return View();
        }
        /// <summary>
        /// 主播后台
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public ActionResult AnchorAdmin(int useridx)
        {
            if (useridx <= 0 ) return View("Error");
            //if (useridx <= 0 || !Tools.IsMobile()) return View("Error");
            decimal sale = 0;  //可提现蜜币
            decimal money = 0; //当前余额
            pay.LW_GoldLimitSet(useridx, ref sale, ref money);
            ViewBag.useridx = useridx;
            ViewBag.sale = sale;
            ViewBag.money = money;
            return View();
        }
        public ActionResult AnchorTixianList(int useridx=0,int pageIndex=0 ,int pageSize=10)
        {
            if (useridx <= 0) return View("Error");
            var List= pay.anchorTixianLists(useridx, pageIndex, pageSize);
            ViewData["data"] = List;
            return View();
        }
        /// <summary>
        /// 下载地址 第三方
        /// </summary>
        /// <returns></returns>
        public ActionResult GoDownload()
        {
            return Redirect(LiveBLL.GetDownUrl(1));
        }
        /// <summary>
        /// 下载地址
        /// </summary>
        /// <returns></returns>
        public ActionResult GoDownloadAnchor()
        {
            return View();
        }
        /// <summary>
        /// 招募下载地址
        /// </summary>
        /// <returns></returns>
        public ActionResult WebDownload()
        {
            return View();
        }
        /// <summary>
        /// 招募下载地址 嵌入APP
        /// </summary>
        /// <returns></returns>
        public ActionResult WebDownload_1()
        {
            return View();
        }
        /// <summary>
        /// 招募
        /// </summary>
        /// <returns></returns>
        public ActionResult JoinPeople()
        {
            return View();
        }
        /// <summary>
        /// 使用说明
        /// </summary>
        /// <returns></returns>
        public ActionResult Instructions()
        {
            return View();
        }
        public ActionResult RechargeActivity()
        {
            return View();
        }
        public ActionResult Authentication(int useridx=0,string key="")
        {
            string myKey = CryptoHelper.ToMD5(useridx + "&mianju.tiange.com"+ useridx).ToLower();
            if (!myKey.Equals(key))
            {
                return View("Error");
            }
            int ret1 = _userbll.AuthenticationUpload(useridx, 3);
            if (ret1 == 0)
            {
                return RedirectToAction("PassAuthentication", new { useridx=useridx});
            }
            ViewBag.useridx = useridx;
            return View();
        }
        /// <summary>
        /// 上传认证视频
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public JsonResult AuthenticationUpload(int useridx=0,int type=0)
        {
            MobileResult mr = new MobileResult();
            
            int ret = _userbll.AuthenticationUpload(useridx, 0);
            if (ret == 0)
            {
                mr.code = "101";
                mr.msg = "存在";
                mr.data = 1;
                return Json(mr, JsonRequestBehavior.AllowGet);
            }
            HttpFileCollection files = HttpContext.ApplicationInstance.Request.Files;
            HttpPostedFileBase file = HttpContext.Request.Files[0];

            if (files == null || file == null)
            {
                mr.code = "113";
                mr.msg = "upload Fail";
                return Json(mr, JsonRequestBehavior.AllowGet);
            }
            string url = "";
            PicSaveBLL.SaveVideo(useridx, file, ref url);
            int ret1 = _userbll.AuthenticationUpload(useridx, 1,type,url);
            if (ret1 == 1)
            {
                mr.code = "100";
                mr.msg = url;
                mr.data = 1;
                return Json(mr, JsonRequestBehavior.AllowGet);
            }
            else
            {
                mr.code = "102";
                mr.msg = "上传失败";
                mr.data = 1;
                return Json(mr, JsonRequestBehavior.AllowGet);
            }
            
        }

        /// <summary>
        /// 通过认证的页面
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public ActionResult PassAuthentication(int useridx = 0)
        {

            ViewBag.videourl = _userbll.AuthenticationVideo(useridx);
            ViewBag.useridx = useridx;
            return View();
        }

    }
}
