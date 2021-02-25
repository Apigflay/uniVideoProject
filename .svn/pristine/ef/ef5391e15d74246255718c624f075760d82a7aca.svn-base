using BLL;
using System;
using System.Web;
using System.Web.Mvc;
using Model;
using Common;

namespace WebAPI.Controllers.Attribute
{
    /// <summary>
    /// 异常处理 自定义过滤器(自定义的一定要继承ActionFilterAttribute)
    /// </summary>
    public class ErrorAttribute : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            //1:获取异常对象
            Exception ex = filterContext.Exception;

            //2:记录异常日志
            ErrorLog log = new ErrorLog();

            string devicetype = filterContext.HttpContext.Request.Headers["devicetype"] ?? "";
            string version = filterContext.HttpContext.Request.Headers["version"] ?? "";
            string useridx = filterContext.HttpContext.Request.Headers["useridx"] ?? "0";

            log.useridx = int.Parse(useridx);
            log.Version = version;
            log.Message = ex.Message;
            log.Remark = ex.TargetSite.ToString();
            log.StackTrace = ex.StackTrace;

           // LogBLL.AddErrorLog(log);

            //3:重定向到错误接口(或错误页面)
            filterContext.Result = new BaseController().Error( ex.Message);
            //4:标记异常已经处理完毕
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();

            // 执行基类中的OnException（如果继承了IExceptionFilter则下面这句话可以不写）
            //base.OnException(filterContext);
        }
    }
}