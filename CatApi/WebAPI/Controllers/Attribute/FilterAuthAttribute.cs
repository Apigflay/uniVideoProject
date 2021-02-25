using System.Web.Mvc;
using Common;
using BLL.Mongo;

namespace WebAPI.Controllers.Attribute
{
    /// <summary>
    /// 过滤非法字符特性2016-6-18
    /// update 2017-05-05 13:44:04 修改当传入的参数值为null时给默认值空
    /// </summary>
    public class FilterAuthAttribute : FilterAttribute, IActionFilter
    {
        /// <summary>
        /// OnActionExecuting是在Action执行之前运行的方法。
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //ActionResult中有定义参数的名称才会被获取到
            var actionParameters = filterContext.ActionDescriptor.GetParameters();

            foreach (var p in actionParameters)
            {
                if (p.ParameterType == typeof(string))
                {
                    var obj = filterContext.ActionParameters[p.ParameterName];

                    if (obj != null)
                    {
                        //获取字符串参数原值
                        var orginalValue = obj as string;
                        //获取过滤后的参数值
                        var filterdValue = TextHelper.FilterSpecial(orginalValue);

                        filterContext.ActionParameters[p.ParameterName] = filterdValue;
                    }
                    else
                    {
                        filterContext.ActionParameters[p.ParameterName] = string.Empty;
                    }
                }
            }
        }

        /// <summary>
        /// OnActionExecuted是在Action执行之后运行的方法。
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //此段代码对控制器内结果集进行加密输出
            //var result = filterContext.Result as ContentResult;

            ////如果结果不为空并且ContentType不为application/json对其结果进行加密处理在进行输出
            //if (result != null && result.ContentType != "application/json")
            //{
            //    result.Content = AESHelper.Encrypt(result.Content, AESHelper.AES_Key, AESHelper.iv);
            //}

            //MongoService.APIAccessStat(1, UtilHelper.GetPath(), UtilHelper.GetHostAndPath());
        }

    }
}