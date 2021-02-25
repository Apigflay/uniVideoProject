using Common.Core;
using System.Web.Mvc;
using Common;
using BLL;
using Model;
using System.Collections.Generic;
using WebAPI.Controllers;
using Newtonsoft.Json;

namespace WebAPI
{
    /// <summary>
    /// 访问版本号页面进行验证
    /// </summary>
    public class CheckLoginAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// OnActionExecuting是Action执行前的操作
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            bool isExist = false;
            var userip = Tools.GetRealIP();
            List<string> list = new List<string>() { "浙江", "湖北" };

            Location loc = PositionHelper.GetLocationInfo(userip);

            bool b = list.Exists(f => loc.Province.Contains(f));
            if (!b)
            {
                LogHelper.WriteLog(LogFile.Log, "地理位置不存在" + loc.Province);
                context.Result = new RedirectResult("/Error");
                return;
            }

            var m_jsonInfo = CookieHelper.GetCookieValue("Live_M");
            m_jsonInfo = CryptoHelper.FromBase64(m_jsonInfo);
            //MemberInfo m_info = JsonConvert.DeserializeObject<MemberInfo>(m_jsonInfo);

            string memKey = "Live_M" + m_jsonInfo;

            LogHelper.WriteLog(LogFile.Log, "2【管理后台开始调用】jsonInfo:" + m_jsonInfo);

            if (!string.IsNullOrEmpty(m_jsonInfo))
            {
                object memValue = MemcachedHelper.Get(memKey);
                if (MemcachedHelper.Exists(memKey))
                {
                    //获取用户信息
                    //var user = MemcachedHelper.Get(memKey);

                    isExist = true;
                }
            }

            //TODO 用户没有登陆 此处根据用户权限去判断是否有权限访问该页面           
            //if (!isExist && cookieValue != "E1DB195456CAE69CD85315B010B614B4")
            if (!isExist)
            {
                LogHelper.WriteLog(LogFile.Log, "【Cookie不存在】cookie解密后:" + m_jsonInfo);

                context.Result = new BaseController().CommonView(1, "啊哦，你访问的页面不存在！");
                return;
            }
            int result = LiveBLL.Get_TestAccount(2, 0, m_jsonInfo);
            if (result <= 0)
            {
                LogHelper.WriteLog(LogFile.Log, "【DB账号不存在】cookie:" + m_jsonInfo);

                context.Result = new RedirectResult("/Home/Index");
            }
            LogHelper.WriteLog(LogFile.Log, "3【管理登陆操作记录】result:" + result + ",info:" + MemcachedHelper.Get(memKey));

            base.OnActionExecuting(context);
        }
    }
}