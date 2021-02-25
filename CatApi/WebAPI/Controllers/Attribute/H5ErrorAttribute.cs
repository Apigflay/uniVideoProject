using System;
using System.Web.Mvc;
using Common;

namespace WebAPI.Controllers
{
    public class H5ErrorAttribute : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            //1:获取异常对象
            Exception ex = filterContext.Exception;

            LogHelper.WriteLog(LogFile.Trace, "【H5页面错误】{0}", ex.Message);

            //3:重定向到错误接口(或错误页面)
            filterContext.Result = new AboutController().CommonView();
            //4:标记异常已经处理完毕
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
        }
    }
}