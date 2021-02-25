using Common;
using Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPI.Controllers.Attribute
{
    /// <summary>
    /// 所有Action安全验证
    /// </summary>
    public class AuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.IsLocal)
            {
                base.OnActionExecuting(context);
                return;
            }

            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);

            ///没有获取到参数时
            if (dic == null)
            {
                context.Result = new BaseController().ParamError();
                return;
            }
            
            string userIdx = dic["userIdx"];
            string token = dic["token"];
            string userTokenKey = CacheKeys.LIVE_USER_TOKEN + userIdx;
            string userTokenValue = (string)MemcachedHelper.MemGet(userTokenKey);

            //如果token 缓存服务器端的Key为空
            //if ((string.IsNullOrEmpty(userTokenValue) || string.IsNullOrEmpty(token)) && token != userTokenValue)
            //{
            //    LogHelper.WriteLog(LogFile.Warning, "【接口安全错误】" + token + "|" + userTokenValue);
            //    context.Result = new BaseController().Error("");
            //    return;
            //}

            //if (context.HttpContext.Request.IsLocal)
            //{
            //    base.OnActionExecuting(context);
            //    return;
            //}
            //HttpRequestBase bases = (HttpRequestBase)filterContext.HttpContext.Request;
            //string url = bases.RawUrl.ToString().ToLower();
            ////获取url中的参数
            //string queryString = bases.QueryString.ToString();
            ////对获取到的参数进行UrlDecode处理
            //queryString = HttpUtility.UrlDecode(queryString).Replace("amp;", "");
            //获取访问Action参数的描述，主要是参数的类型和参数名称

            base.OnActionExecuting(context);
        }
    }
}