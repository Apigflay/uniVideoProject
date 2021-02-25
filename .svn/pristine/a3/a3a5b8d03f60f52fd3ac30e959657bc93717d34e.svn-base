using System.Collections.Generic;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using BLL;
using Model;
using Common;
using Common.Core;
using WebAPI.Controllers;
using System;
using Newtonsoft.Json;

namespace WebAPI.Areas.H5.Controllers
{
    [H5Error]
    public class MController : Controller
    {
        /// <summary>
        /// live.9158.com/H5/M/Index
        /// </summary>
        /// <returns></returns>
        [CheckLogin]
        public ActionResult Index()
        {
            string cookieKey = "Live_M";
            string cookieValue = CryptoHelper.FromBase64(CookieHelper.GetCookieValue(cookieKey));
            //cookieValue = CryptoHelper.DESDecrypt(cookieValue, "while(do");

            MemberInfo m_info = new MemberInfo();// JsonConvert.DeserializeObject<MemberInfo>(cookieValue);

            if (!string.IsNullOrEmpty(m_info.Account))
            {
                ViewBag.nickName = m_info.NickName;
            }

            return View();
        }

        /// <summary>
        /// 退出登陆
        /// live.9158.com/H5/M/LoginOut
        /// </summary>
        /// <returns></returns>
        public ActionResult loginOut()
        {
            string cookieKey = "Live_Manager_Sign";
            string cookieValue = CryptoHelper.FromBase64(CookieHelper.GetCookieValue(cookieKey));
            cookieValue = CryptoHelper.DESDecrypt(cookieValue, "while(do");
            string memKey = "Live_Manager_" + cookieValue;

            CookieHelper.ClearCookie(cookieKey);
            MemcachedHelper.Remove(memKey);

            return Redirect("/Home/Index");
        }

        /// <summary>
        /// 错误日志列表
        /// live.9158.com/H5/M/ErrorLog
        /// </summary>
        /// <returns></returns>
        [CheckLogin]
        public ActionResult ErrorLog(string date = "0", int page = 1)
        {
            var count = 0;
            var pageSize = 25;
            if (date == "0")
            {
                date = System.DateTime.Now.ToShortDateString();
            }

            PagedList<ErrorLog> list = LogBLL.GetLogList(date, page, pageSize, ref count)
                .ToPagedList(page, pageSize);

            list.TotalItemCount = count;
            list.CurrentPageIndex = page;

            return View(list);
        }

        /// <summary>
        /// 版本号列表
        /// live.9158.com/H5/M/GetVersion
        /// </summary>
        /// <returns></returns>
        [CheckLogin]
        public ActionResult GetVersion(int type = 2, int sort = 1)
        {
            List<LiveVersion> list = LiveBLL.GetAppVer_List().FindAll(f => f.type == type);
            if (sort == 2)
            {
                list.Sort((x, y) => -x.id.CompareTo(y.id));
            }

            return View(list);
        }

        /// <summary>
        /// 版本号编辑视图
        /// </summary>
        /// <param name="dataType">数据操作类型1：添加，2：修改</param>
        /// <param name="type">版本号分类</param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="isupdate"></param>
        /// <param name="version"></param>
        /// <param name="updateinfo"></param>
        /// <param name="updateURL"></param>
        /// <returns></returns>
        [CheckLogin]
        public ActionResult VerEditView(int dataType = 1, int type = 2, int id = 0, string name = "", int isupdate = 0, string version = "100", string updateinfo = "", string updateURL = "", string bundleid = "")
        {
            LiveVersion lv = new LiveVersion();

            if (Request.IsAjaxRequest())
            {
                lv.type = type;
                lv.id = id;
                lv.name = name;
                lv.version = version;
                lv.updateInfo = updateinfo;
                lv.updateURL = updateURL;
                lv.isUpdate = isupdate;
                lv.auditVersion = int.Parse(Request["auditVersion"]);
                lv.Bundleid = bundleid;

                if (type <= 0 || id <= 0 || string.IsNullOrEmpty(name.Trim()))
                {
                    return Content("-1");
                }

                int ret = LiveBLL.Version_Info_Save(dataType, lv);
                if (ret > 0)
                {
                    CacheHelper.Delete("Live_Get_AllAppVersion");
                    return Content("1");
                }
                else
                    return Content("0");
            }
            else
            {
                if (id == 0)
                    return View(lv);
                else
                    return View(LiveBLL.GetAppVer_ById(id));
            }
        }


        #region 配置文件
        /// <summary>
        /// 获取所有配置文件（获取配置文件列表）
        /// </summary>
        /// <returns></returns>
        [CheckLogin]
        public ActionResult GetConfigList()
        {
            Model.View.VMAppConfig appc = new Model.View.VMAppConfig();

            //将数据组装到ViewModel中
            appc.configs = LiveBLL.Get_LiveConfigList(0);
            if (appc.configs != null)
            {
                //appc.barrage = appc.configs.Find(f => f.id == 1).data;
                //appc.paySwitch = appc.configs.Find(f => f.id == 11).data;
                //appc.totalBarrage = appc.configs.Find(f => f.id == 4).data;
                //appc.hbPrice = appc.configs.Find(f => f.id == 5).data;
            }

            return View(appc);
        }
        /// <summary>
        /// 配置文件编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CheckLogin]
        public ActionResult ConfigEditView(int id = 0)
        {
            Model.View.VMAppConfig appcon = new Model.View.VMAppConfig();

            appcon.config = LiveBLL.Get_LiveConfigById(id);

            if (Request.IsAjaxRequest())
            {
                MobileResult mr = new MobileResult();
                mr.data = appcon;
                return Json(mr, JsonRequestBehavior.AllowGet);
            }

            return View(appcon);
        }
        /// <summary>
        /// 配置文件编辑
        /// </summary>
        /// <returns></returns>
        [CheckLogin]
        [HttpPost]
        public ActionResult ConfigEdit(int type = 0, int id = 0, string name = "", string data = "", string content = "")
        {
            if (type < 1 || id < 1)
            {
                return Content("-1");
            }
            var ret = LiveBLL.Config_Info_Save(type, id, name, data, content);
            if (ret > 0)
            {
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }
        #endregion

        public ActionResult Other()
        {
            return View();
        }

        #region 马甲版本号操作

        /// <summary>
        /// 喵播所有包审核版本号列表
        /// live.9158.com/H5/M/MajiaVersionView?key=zhongqing&type=2
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type">2：喵播，3：猫播</param>
        /// <returns></returns>
        public ActionResult MajiaVersionView(string key = "", int type = 2)
        {
            if (key != "zhongqing")
            {
                return View("Error");
            }
            if (type < 2)
            {
                type = 2;
            }
            List<LiveVersion> list = LiveBLL.GetAppVer_List().FindAll(f => f.type == type && f.id >= 10);

            return View(list);
        }
        /// <summary>
        /// 喵播所有包编辑试图
        /// </summary>
        /// <param name="id"></param>
        /// <param name="auditVersion"></param>
        /// <returns></returns>
        public ActionResult MajiaEditView(int id = 0, int auditVersion = 100)
        {
            LiveVersion lv = new LiveVersion();

            if (Request.IsAjaxRequest())
            {
                lv.id = id;
                lv.auditVersion = auditVersion;

                int ret = LiveBLL.Version_Info_Save(4, lv);
                if (ret > 0)
                {
                    CacheHelper.Delete("Live_Get_AllAppVersion");
                    return Content("1");
                }
                else
                    return Content("0");
            }
            else
            {
                if (id < 10) id = 10;
                return View(LiveBLL.GetAppVer_ById(id));
            }
        }

        #endregion
    }
}
