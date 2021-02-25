using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace WebAPI.Controllers.Attribute
{
    public class EncryptResultAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 对返回客户端结果进行加密
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {

            var result = filterContext.Result as ContentResult;

            result.Content = AESHelper.Encrypt(result.Content, CryptoHelper.AES_KEY, AESHelper.iv);

            base.OnResultExecuting(filterContext);
        }
    }
}