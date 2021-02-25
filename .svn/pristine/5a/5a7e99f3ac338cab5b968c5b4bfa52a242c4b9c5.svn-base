using BLL;
using Common;
using Common.Core;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPI.Controllers;

namespace WebAPI.Areas.H5.Controllers
{
    [H5Error]
    public class H5PayController : BaseController
    {
        RankBLL rank = new RankBLL();

        /// <summary>
        /// 守护商品购买列表
        /// live.9158.com/Rank/GoodsPriceList
        /// </summary>
        /// <returns></returns>
        public ActionResult GoodsPriceList()
        {
            MobileResult mr = new MobileResult();

            var goodsList = rank.Get_GoodsPriceList();

            mr.code = "100";
            mr.msg = "success";
            mr.data = new { goosList = goodsList };

            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 守护购买页面
        /// http://live.9158.com/H5/H5Pay/GuardianBuyView?useridx=60068188&touseridx=60068188&roomid=0&token=1&sign=djlallHJLGD780GLD
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="toUseridx"></param>
        /// <param name="token"></param>
        /// <param name="goodsId"></param>
        /// <param name="roomid"></param>
        /// <returns></returns>
        public ActionResult GuardianBuyView(int useridx = 0, int toUseridx = 0, int roomid = 0, string token = "", string sign = "")
        {
            //if (useridx <= 0 || toUseridx <= 0 || string.IsNullOrEmpty(token))
            //{
            //    return new BaseController().CommonView(1, "参数错误啦");
            //}
            //string userTokenKey = CacheKeys.LIVE_USER_TOKEN + useridx;
            //string userTokenValue = MemcachedHelper.MemGet(userTokenKey);

            //if (string.IsNullOrEmpty(userTokenValue) || !token.Equals(userTokenValue))
            //{
            //    LogHelper.WriteLog(LogFile.Log, "【守护购买校验用户信息失败】" + useridx);
            //    return new BaseController().CommonView(1, "获取用户信息失败，请退出重新登录");
            //}
            string CK = "Live-GuardianBuy-" + useridx;
            MemcachedHelper.Set(CK, sign, 5);

            return View();
        }

        /// <summary>
        /// 守护购买
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="toUseridx"></param>
        /// <param name="token"></param>
        /// <param name="goodsId"></param>
        /// <param name="roomid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PurchaseGuardData(int useridx = 0, int toUseridx = 0, int roomid = 0, string token = "", int goodsId = 0, string sign = "")
        {
            MobileResult mr = new MobileResult();

            int ret = 0;
            string mySign = "";
            string userip = Tools.GetRealIP();

            if (useridx == 0 || toUseridx == 0 || goodsId == 0 || string.IsNullOrEmpty(token))
            {
                mr.code = "101";
                mr.msg = "参数错误";
                return Content(JsonConvert.SerializeObject(mr));
            }
            //string userTokenKey = CacheKeys.LIVE_USER_TOKEN + useridx;
            //string userTokenValue = MemcachedHelper.MemGet(userTokenKey);

            //if (string.IsNullOrEmpty(userTokenValue) || !token.Equals(userTokenValue))
            //{
            //    mr.code = "103";
            //    mr.msg = "获取登录信息失败，请退出APP重新登录";
            //    return Content(JsonConvert.SerializeObject(mr));
            //}
            if (useridx == toUseridx)
            {
                mr.code = "102";
                mr.msg = "自己不能购买自己哦";
                return Content(JsonConvert.SerializeObject(mr));
            }

            ret = rank.Purchase_Guard(useridx, toUseridx, roomid, goodsId, userip);

            mr.code = ret.ToString();
            mr.msg = GetPurchaseMsg(ret);

            LogHelper.WriteLog(LogFile.Debug, "【守护购买日志】{0},{1}", ret, mr.msg);

            return Content(JsonConvert.SerializeObject(mr));
        }

        public string GetPurchaseMsg(int code)
        {
            string msg = "";
            switch (code)
            {
                case -2:
                    msg = "此商品不存在";
                    break;
                case -3:
                    msg = "余额不足请充值";
                    break;
                case -4:
                    msg = "购买失败";
                    break;
                case -5:
                    msg = "守护天数不能超过365天";
                    break;
                default:
                    break;
            }
            return msg;
        }

    }
}
