using System;
using System.Web.Mvc;
using BLL;
using Common;
using BLL.Mongo;

namespace WebAPI.Controllers
{
    [H5Error]
    public class HomeController : Controller
    {
        public static int[] idx = { 29371898, 63583358 };
        private int uidx = idx[new Random((int)DateTime.Now.Ticks).Next(0, idx.Length)];

        public ActionResult Index()
        {
            return Content("Index");
            //IncomeBLL income = new IncomeBLL();

            //string device = Tools.GetdeviceName();
            //string inviteCode = "";
            //int result = income.GetInviteCodeByIdx(uidx, device, ref inviteCode);
            //ViewBag.code = inviteCode;

            //if (Tools.IsMobile()) return View("Mobile");

            //return View("Index");
        }

        /// <summary>
        /// 自动下载跳转
        /// live.9158.com/Home/Autodownload?isAuto=true
        /// </summary>
        /// <returns></returns>
        public ActionResult Autodownload()
        {
            IncomeBLL income = new IncomeBLL();

            string device = Tools.GetdeviceName();
            string inviteCode = "";
            int result = income.GetInviteCodeByIdx(uidx, device, ref inviteCode);
            ViewBag.code = inviteCode;

            //MongoService.InsertInviteAccess(1, uidx, inviteCode, "", "", "", 0);
            StatisticsBLL.LiveApi_Statis(1);

            return View();
        }

        public ActionResult copystati(string code = "")
        {
            LogHelper.WriteLog(LogFile.Debug, "【copy统计】" + code);

            return Content("1");
        }
    }
}
