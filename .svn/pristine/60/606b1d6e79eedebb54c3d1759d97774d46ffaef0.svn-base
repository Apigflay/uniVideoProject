using System;
using System.Web.Mvc;
using BLL;
using WebAPI.Controllers;
using Common;
using Model;
using System.Collections.Generic;
using BLL.Mongo;

namespace WebAPI.Areas.H5.Controllers
{
    [H5Error]
    public class ActiveController : BaseController
    {
        UserInfoBLL user = new UserInfoBLL();
        IncomeBLL income = new IncomeBLL();

        [HttpPost]
        public void clear()
        {
            CookieHelper.ClearCookie("Live_inviteCode");
            CookieHelper.SetCookie("Live_inviteCode", "");
        }

        /// <summary>
        /// 功能已关闭
        /// live.9158.com/H5/Active/inviteNew?useridx=29371898&token=c783e57a4f288cd48bc99904976bf4ea&timestamp=1496456303
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public ActionResult inviteNew(int useridx = 0, string token = "", string timestamp = "", string appversion = "")
        {

            return Redirect("http://live.9158.com/");

            string device = Tools.GetdeviceName();
            string mySign = CryptoHelper.ToMD5("/h5/active/invitenew" + timestamp + useridx + appversion + "1811b681-2322-lda1-b3d6k876");

            if (useridx <= 0) return Content("Parameter Error");

            //!device.Equals("WeiXin") ||
            if (!mySign.Equals(token))
            {
                //////MongoService.InsertInviteAccess(1, useridx, appversion, "非微信扫码客户端");

                return Redirect("http://live.9158.com/");
            }

            UserInfo u = new UserInfo();
            u = user.GetLiveUserInfoByIdx(useridx);

            //判断当前用户是否打开过邀请页面，如果打开过就不再进行分配邀请码
            string inviteCode = CookieHelper.GetCookieValue("Live_inviteCode");

            if (string.IsNullOrEmpty(inviteCode))
            {
                int result = income.GetInviteCodeByIdx(useridx, device, ref inviteCode);
                if (result > 0)
                {
                    CookieHelper.SetCookie("Live_inviteCode", inviteCode, DateTime.Now.AddDays(15));
                }
                else if (result == -4)
                {
                    inviteCode = "仅限微信用户";
                }
                else
                {
                    inviteCode = "邀请码已用完！";
                }
                //MongoService.InsertInviteAccess(1, useridx, inviteCode, "", appversion, u.userId, result, inviteCode);
            }

            ViewBag.inviteCode = inviteCode;
            ViewBag.photo = u.smallpic;
            ViewBag.nickName = u.myname;

            return View(u);
        }
        
        /// <summary>
        /// 脑洞大比拼分享页
        /// https://live.9158.com/H5/Active/ia/?idx=60068188
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public ActionResult ia(int idx = 0)
        {
            string device = Tools.GetdeviceName();

            if (idx <= 0) return Content("Parameter Error");

            //判断当前用户是否打开过邀请页面，如果打开过就不再进行分配邀请码
            string inviteCode = CookieHelper.GetCookieValue("Live_inviteCode");

            if (string.IsNullOrEmpty(inviteCode))
            {
                int result = income.GetInviteCodeByIdx(idx, device, ref inviteCode);
                if (result > 0)
                {
                    CookieHelper.SetCookie("Live_inviteCode", inviteCode, DateTime.Now.AddDays(15));
                }
                else if (result == -4)
                {
                    inviteCode = "仅限微信用户";
                }
                else
                {
                    inviteCode = "邀请码已用完！";
                }
                //////MongoService.InsertInviteAccess(1, useridx, inviteCode, "", appversion, u.userId, result, inviteCode);
            }

            ViewBag.inviteCode = inviteCode;
            //ViewBag.photo = u.smallpic;
            //ViewBag.nickName = u.myname;

            return View();
        }
    }
}
