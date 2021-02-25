using System.Collections.Generic;
using System.Web.Mvc;
using Model;
using WebAPI.Controllers;
using BLL;
using Common.Core;
using Common;
using Model.View;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebAPI.Areas.H5.Controllers
{
    public class IncomeController : Controller
    {
        IncomeBLL income = new IncomeBLL();
        GiftBLL gift = new GiftBLL();

        /// <summary>
        /// 邀新红包记录
        /// live.9158.com/H5/Income/RedPacket?useridx=63583358&token=1e922f5f8019a9556578143f40c10458
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public ActionResult RedPacket(int useridx = 0, int page = 1, string token = "")
        {
            if (useridx <= 0) { return View("Error"); }
            var memValue = MemcachedHelper.Get("Cache_WithdrawView_" + useridx);

            //if (string.IsNullOrEmpty(token) || memValue == null || !memValue.Equals(token))
            //{
            //    return new BaseController().CommonView(1, "请求超时,请重新登录");
            //}
            int pageSize = 1;
            var redDetailsList = income.Get_RedPacketDetails_Byidx(useridx, page, pageSize);

            if (redDetailsList != null && redDetailsList.Count > 0)
            {
                return View();
            }
            else
            {
                return new BaseController().CommonView(1, "你还没有红包哦，快去分享拿红包吧~");
            }
        }

        /// <summary>
        /// 现金提现记录
        /// live.9158.com/H5/Income/withdrawRecord?useridx=60068188&token=Cl3ARQNdV5rcJXS8ymLbnv6gBxi3x
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public ActionResult withdrawRecord(int useridx = 0, string token = "")
        {
            if (useridx <= 0) { return View("Error"); }
            var memValue = MemcachedHelper.Get("Cache_WithdrawView_" + useridx);

            //if (string.IsNullOrEmpty(token) || memValue == null || !memValue.Equals(token))
            //{
            //    return new BaseController().CommonView(1, "请求超时");
            //}

            var recordList = income.Get_RedPacketWithDraw_Details_Byidx(useridx, 1, 100);

            if (recordList != null && recordList.Count > 0)
            {
                return View(recordList);
            }
            else
            {
                return new BaseController().CommonView(1, "你还没有提现记录哦~");
            }
        }

        /// <summary>
        /// 宝宝兑换
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public ActionResult babyView(int useridx = 0, string token = "")
        {


            return View();
        }

        /// <summary>
        /// 宝宝提现记录
        /// live.9158.com/H5/Income/BabyRecord?useridx=
        /// </summary>
        /// <returns></returns>
        public ActionResult BabyRecord(int useridx = 0)
        {
            var memValue = MemcachedHelper.Get("Cache_WithdrawView_" + useridx);

            if (useridx < 1) { return new BaseController().CommonView(1, "参数错误啦！"); }
            if (memValue == null) { return new BaseController().CommonView(1, "请求超时，请重新连接！"); }

            VMIncome vmBabyRecord = new VMIncome();
            vmBabyRecord.BabyRecordList = gift.Get_BabyExchange_Record(useridx);

            if (vmBabyRecord.BabyRecordList == null || vmBabyRecord.BabyRecordList.Count <= 0)
                return new BaseController().CommonView(1, "你还没有提现记录哦~");

            return View(vmBabyRecord);
        }

        #region Data

        /// <summary>
        /// 红包记录Ajax请求
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult RedPacketData(int useridx = 0, int page = 1)
        {
            int _totalCount = 0;
            int _totalPage = 0;
            int _pageSize = 20;

            var redDetailsList = income.Get_RedPacketDetails_Byidx(useridx, page, _pageSize);

            if (redDetailsList != null && redDetailsList.Count > 0)
            {
                _totalCount = redDetailsList[0].totalCount;
            }
            _totalPage = Tools.GetPageCount(_totalCount, _pageSize);

            MobileResult mr = new MobileResult();

            mr.code = "100";
            mr.msg = "success";
            mr.data = new { dataList = redDetailsList, totalPage = _totalPage };

            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            timeFormat.DateTimeFormat = "MM-dd HH:mm";

            return Content(JsonConvert.SerializeObject(mr, Formatting.Indented, timeFormat));
        }

        #endregion
    }
}
