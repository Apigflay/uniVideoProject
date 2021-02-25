using System.Web.Mvc;
using WebAPI.Controllers.Attribute;
using Model;
using Common;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 所有Controller继承BaseController，则都会进行异常捕获，和接口安全验证
    /// </summary>
    //[AuthAttribute]
    [Error]
    [FilterAuth]
    public class BaseController : Controller
    {
        /// <summary>
        /// 操作错误调用
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public ActionResult ParamError(string msg = "")
        {
            var mr = new MobileResult { code = "101", msg = "Param Error" + msg };

            return Json(mr, "text/html; charset=UTF-8", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 用户信息校验失败调用
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public ActionResult LoginFail(string msg = "")
        {
            var mr = new MobileResult { code = "103", msg = "获取用户信息失败，请退出重新登录" };

            return Json(mr, "text/html; charset=UTF-8", JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 图片服务器连接失败
        /// </summary>
        /// <returns></returns>
        public ActionResult PicConnectFailed()
        {
            LogHelper.WriteLog(LogFile.Warning, "【图片服务器连接失败】");

            var mr = new MobileResult { code = "103", msg = "Picture Server Connect Failed" };

            return Json(mr, "text/html; charset=UTF-8", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 接口安全验证失败
        /// </summary>
        /// <returns></returns>
        public ActionResult Error(string i = "")
        {
            var mr = new MobileResult { code = "104", msg = "Network Error" + i };

            return InfoMsg(mr);
        }

        /// <summary>
        /// 用于H5页面返回时使用
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public ActionResult CommonView(int code = 1, string msg = "出错啦！")
        {
            return View("ErrorMsg", new MobileResult(code, msg));
        }

        public ActionResult InfoMsg(MobileResult mr)
        {
            return Json(mr, "text/html; charset=UTF-8", JsonRequestBehavior.AllowGet);
        }
    }
}
