using System;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;

using Model;
using Common;
using BLL;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
namespace WebAPI.Controllers
{
    public class ShareController : BaseController
    {
        private const string AppID = "wxc75938df28a5b317";
        private const string AppSecret = "8bfa20276f836a1304e92a433664010e";
        private const string AstokenUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
        private const string TicketUrl = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi";
        private const int CacheTime = 7200;

        private const string UserCachekey = "UserInfo_CacheKey_";
        private RoomBLL roombll = new RoomBLL();

        /// <summary>
        /// 分享
        /// </summary>
        /// <param name="Idx">房间Idx</param>
        /// <param name="UserIdx">主播Idx</param>
        /// <param name="shareuseridx">分享者Idx</param>
        /// <returns></returns>
        public ActionResult Index(int Idx = 0, int UserIdx = 0, int shareuseridx = 0)
        {
            if (Idx < 1 || UserIdx < 1) return new BaseController().ParamError();

            string areaid = AppDataBLL.GetAreaid;
            string linkURL = (areaid == "0") ? "http://live.9158.com/Share/Play2" : "http://tw1.livemiao.com/Share/Play";
            MobileResult r = new MobileResult();
            UserInfo user = new UserInfoBLL().GetLiveUserInfoByIdx(UserIdx);

            if (user != null && user.useridx > 0)
            {
                r.code = "100";
                r.msg = "success";
                r.data = new
                {
                    roomid = Idx,
                    useridx = user.useridx,
                    user = string.Format("「{0}」正在直播", user.myname),
                    photo = user.smallpic.Contains("http://wx.qlogo.cn") ? user.smallpic.Replace("/0", "/132") : user.smallpic,//微信分享不动时把大图替换成小图 2016-6-7
                    desc = AppDataBLL.GetShareLettes(user.useridx, user.myname),/* + "正在直播，一起来看!*/
                    linkURL = linkURL
                };
            }
            else
            {
                r.code = "102";
                r.msg = "fail";
                r.data = "{}";
            }
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        #region 无用代码
        //public ActionResult SharePlay(int useridx = 0, int roomid = 0)
        //{
        //    bool ismobile = Tools.IsMobile();
        //    ViewBag.IsMobile = ismobile;

        //    ShareModel mod = LiveBLL.GetShareInfo(int.Parse(Idx), int.Parse(UserIdx));
        //    if (mod != null)
        //    {
        //        UserInfo user;
        //        Object o = CacheHelper.GetCache(UserCachekey + useridx);
        //        if (o != null)
        //        {
        //            user = (UserInfo)o;
        //        }
        //        else
        //        {
        //            user = (new UserInfoBLL()).GetLiveUserInfoByIdx(useridx);
        //            CacheHelper.SetCache(UserCachekey + useridx, user);
        //        }

        //        ViewBag.Idx = useridx;
        //        ViewBag.UserIdx = user.useridx;
        //        ViewBag.Nick = user.myname + "(" + user.useridx + ")" ?? "开始直播了，朋友们来捧捧场啦!";
        //        ViewBag.HeadPic = user.smallpic;
        //        ViewBag.Photo = user.bigpic;
        //        ViewBag.Flv = mod.flv;
        //        ViewBag.M3u8 = mod.m3u8;

        //        int isStar = 0;
        //        Room room = new RoomBLL().GetServeridByUserIdx(useridx, ref isStar);
        //        if (room != null)
        //        {
        //            ViewBag.ServerId = room.serverid;
        //        }
        //        else
        //        {
        //            ViewBag.ServerId = 0;
        //        }

        //        ViewBag.Timespan = TimeHelper.GetTimeStamp();
        //        ViewBag.Noncestr = RandomHelper.GetRadString(16).ToLower();
        //        try
        //        {
        //            string jsapi_ticket_key = "share_mpwx_ticket";
        //            string jsapi_astoken_key = "share_mpwx_astoken";
        //            string ticket = "", astoken = "";
        //            if (HttpRuntime.Cache[jsapi_ticket_key] != null)
        //            {
        //                ticket = HttpRuntime.Cache[jsapi_ticket_key].ToString();
        //                ViewBag.Sign = ticket;
        //            }
        //            else
        //            {
        //                if (HttpRuntime.Cache[jsapi_astoken_key] != null)
        //                {
        //                    astoken = HttpRuntime.Cache[jsapi_astoken_key].ToString();
        //                }
        //                else
        //                {
        //                    JObject o = (JObject)HttpHelper.HttpGet(string.Format(AstokenUrl, AppID, AppSecret));
        //                    dynamic dy = JsonHelper.DynamicConvertJson(HttpHelper.HttpGet(string.Format(AstokenUrl, AppID, AppSecret)));
        //                    if (!String.IsNullOrEmpty(dy.access_token))
        //                    {
        //                        astoken = dy.access_token.ToString();

        //                        HttpRuntime.Cache.Insert(jsapi_astoken_key, astoken, null, DateTime.Now.AddMinutes(CacheTime), System.Web.Caching.Cache.NoSlidingExpiration);
        //                    }
        //                    else
        //                    {
        //                        astoken = dy.errcode;
        //                    }
        //                }
        //                if (!String.IsNullOrEmpty(astoken))
        //                {
        //                    dynamic dc = JsonHelper.DynamicConvertJson(HttpHelper.HttpGet(string.Format(TicketUrl, astoken)));
        //                    if (dc.errcode.ToString() == "0" && !String.IsNullOrEmpty(dc.ticket))
        //                    {
        //                        ticket = dc.ticket.ToString();

        //                        HttpRuntime.Cache.Insert(jsapi_ticket_key, ticket, null, DateTime.Now.AddMinutes(CacheTime), System.Web.Caching.Cache.NoSlidingExpiration);
        //                    }
        //                }
        //                ViewBag.Sign = astoken + " | " + ticket;
        //            }
        //            if (!string.IsNullOrEmpty(ticket))
        //            {
        //                string sign = string.Format("jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}", ticket, ViewBag.Noncestr, ViewBag.Timespan, Request.Url.AbsoluteUri);
        //                ViewBag.BeforeHash = ticket + " | " + sign;
        //                ViewBag.Sign = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sign, "SHA1").ToLower();

        //            }
        //            else
        //            {
        //                ViewBag.Sign = "";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ViewBag.Sign = ex.Message;
        //        }
        //    }

        //    return View();
        //}

        #endregion

        // GET: /Share/Play
        /// <summary>
        /// 分享出去的视频播放
        /// </summary>
        /// <param name="shareuseridx">分享者useridx</param>
        /// <param name="Idx">房间Id</param>
        /// <param name="UserIdx">主播Idx</param>
        /// <returns></returns>
        public ActionResult Play(int shareuseridx = 0, int Idx = 0, int UserIdx = 0, int serverid = 0, string isappinstalled = "0", string sharetype = "")
        {
            if (Idx == 0 || UserIdx == 0)
            {
                return View("Error");
            }

            string linkURL = ShareLink(shareuseridx, Idx, UserIdx, serverid, isappinstalled, sharetype);

            return Redirect(linkURL);
        }

        public ActionResult Play2(int shareuseridx = 0, int Idx = 0, int UserIdx = 0, int serverid = 0, string isappinstalled = "0", string sharetype = "")
        {
            if (Idx == 0 || UserIdx == 0)
            {
                return View("Error");
            }
            string linkURL = ShareLink(shareuseridx, Idx, UserIdx, serverid, isappinstalled, sharetype);

            return Redirect(linkURL);
        }

        public ActionResult Play3(int shareuseridx = 0, int Idx = 0, int UserIdx = 0, int serverid = 0, string isappinstalled = "0", string sharetype = "")
        {
            if (Idx == 0 || UserIdx == 0)
            {
                return View("Error");
            }
            string linkURL = ShareLink(shareuseridx, Idx, UserIdx, serverid, isappinstalled, sharetype);

            return Redirect(linkURL);
        }
        public ActionResult Play4(int shareuseridx = 0, int Idx = 0, int UserIdx = 0, int serverid = 0, string isappinstalled = "0", string sharetype = "")
        {
            if (Idx == 0 || UserIdx == 0)
            {
                return View("Error");
            }
            string linkURL = ShareLink(shareuseridx, Idx, UserIdx, serverid, isappinstalled, sharetype);

            return Redirect(linkURL);
        }

        private string ShareLink(int shareuseridx, int Idx, int UserIdx, int serverid, string isappinstalled, string sharetype)
        {
            bool ismobile = Tools.IsMobile();
            if (ismobile)
            {
                StatisticsBLL.PlayShare_Record(shareuseridx, UserIdx, Idx, Tools.GetRealIP(), sharetype);
            }
            int ytype = 4;
            if (sharetype.ToLower() == "sinaweibo")
            {
                ytype = 3;
            }
            int sid, isStar = 0;
            int.TryParse(serverid.ToString(), out sid);
            if (sid <= 0)
            {
                Room room = roombll.GetServeridByUserIdx(Idx, ref isStar);
                sid = room.serverid;
            }
            string linkURL = string.Format("http://www.miaobolive.com/loginT.html?yaoroomid={0}&yaoroomidx={1}&fenIdx={2}&yaoType={3}&isappinstalled={4}&yaoServerid={5}", Idx, UserIdx, shareuseridx, ytype, isappinstalled, sid);
            return linkURL;
        }


        public ActionResult invitationShare(int userIdx)
        {
            // string useridx = "60424678";
            //if (!HttpHelper.GetClientType())
            //{
            //    return new BaseController().ParamError("请在APP中打开");
            //}
            if (userIdx == 0)
            {
                return new BaseController().ParamError("CheckMobile");
            }
            UserInfoBLL user = new UserInfoBLL();
            List<UserInvitationInfo> list = user.GetUserInvitationInfoByUseridxSet(userIdx);
            if (list.Count > 0)
            {
                ViewBag.useridx = userIdx;
                ViewBag.myName = list[0].myName;
                ViewBag.InvitationCode = userIdx;
                return View();
            }
            else
            {
                return new BaseController().ParamError("用户不存在");

            }



        }

    }
}