using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebAPI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*以下路由配置顺序很重要，如果第一个路由没有找到将继续寻找下一个路由地址*/

            /*多语言路由*/
            routes.MapRoute(
               "Globalization", // 路由名称
               "{lang}/{controller}/{action}/{id}", // 带有参数的 URL
                new { lang = "zh-cn", controller = "Home", action = "Index", id = UrlParameter.Optional }, // 参数默认值
                new { lang = "^[a-zA-Z]{2}(-[a-zA-Z]{2})?$" } //参数约束
            );
            
            /*默认路由*/
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "WebAPI.Controllers" }//设置域，与其他域区分 传统写法
            );
        }
    }
}