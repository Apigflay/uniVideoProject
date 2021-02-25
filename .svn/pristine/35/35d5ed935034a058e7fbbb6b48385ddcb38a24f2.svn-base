using Common;
using Common.Core;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using WebAPI.Controllers;

namespace WebAPI
{
    /// <summary>
    /// 此类是对某些Action单独进行认证
    /// </summary>
    public class Auth : ActionFilterAttribute
    {
        /// <summary>
        /// 对OnActionExecuting 进行重写
        /// </summary>
        /// <param name="context"></param>
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
            if (string.IsNullOrEmpty(userTokenValue) && string.IsNullOrEmpty(token) && token != userTokenValue)
            {
                LogHelper.WriteLog(LogFile.Warning,"【接口安全错误】" +token + "|" + userTokenValue);
                context.Result = new BaseController().Error("");
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}