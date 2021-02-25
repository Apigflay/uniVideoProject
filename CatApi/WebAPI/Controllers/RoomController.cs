using Common;
using System.Web.Mvc;
using BLL;
using System.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using Model;

namespace WebAPI.Controllers
{
    public class RoomController : BaseController
    {
        RoomBLL room = new RoomBLL();

        /// <summary>
        /// 大厅tab标签
        /// live.9158.com/Room/GetHotTab?isEnglish=1
        /// </summary>
        /// <returns></returns>
        //[OutputCache(Duration = 60 * 30)]
        public ActionResult GetHotTab(int isEnglish = 0)
        {
            MobileResult mr = new MobileResult();

            var list = room.Get_HotTab(isEnglish);//Accept-Language: zh-CN,en-US;q=0.8
            if (list != null && list.Count > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = new { tabList = list };
            }
            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        ///1：获取热门直播列表(陈春森那边调用2016-5-12)
        ///live.9158.com/Room/GetOnlieHotLive
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 60, VaryByParam = "page")]
        public ActionResult GetOnlieHotLive(int page = 1)
        {
            int pagesize = 10, count = 0;
            var mr = new MobileResult();

            DataTable dt = RoomBLL.GetHotOnlineRoom(page, pagesize, ref count);
            if (dt != null && dt.Rows.Count > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = dt;
                //data = JsonConvert.SerializeObject(dt, new DataTableConverter());
            }
            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        ///1：获取热门tab直播列表v2
        ///live.9158.com/Room/GetHotLive_v2?type=1&useridx=60606644&isNewapp=1&page=1
        /// </summary>
        /// <param name="type">0：新版热门 1:热门 2：同城 3:红人 4:北京IP 5:人气 6:魅力 7:声望</param>
        /// <param name="useridx"></param>
        /// <param name="page"></param>
        /// <param name="checking">是否审核模式(Android)</param>
        /// <param name="isNewApp">是否新版喵播(IOS,Android)</param>
        /// <returns></returns>
        public ActionResult GetHotLive_v2(int type = 1, int useridx = 0, int page = 1, int checking = 0, int isNewapp = 0)
        {
            MobileResult mr = new MobileResult();

            int _totalCount = 0, _nbool = 0;
            var _roomList = room.GetHotRank_v2(type, useridx, page, isNewapp, checking, ref _totalCount, ref _nbool);
            var _totalPage = Tools.GetPageCount(_totalCount, 20);
            var _timeConfig = new List<HotConfig>();

            if (_roomList != null && _roomList.Count > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = new
                {
                    list = _roomList,
                    totalPage = _totalPage,
                    samecity = _nbool,//同城是否有数据
                    hotswitch = _timeConfig.Count > 0 ? _timeConfig[0] : null,//兼容老版本时间段内显示
                    hotswitch2 = _timeConfig,//v1.8.0要求多时间段
                    hotConfig = 0
                };
            }

            return Content(JsonConvert.SerializeObject(mr));
        }


        /// <summary>
        /// 获取同城,附近数据2016-8-28 
        /// live.9158.com/Room/GetSameCity?apiversion=1&lon=120.151344&lat=30.34391&page=1
        /// </summary>
        /// <param name="apiversion">0:取同城的数据 1:附近的主播(Add 2016-12-28)</param>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult GetSameCity(int useridx = 0, double lon = 0, double lat = 0, int page = 1)
        {
            int _totalCount = 0, _nbool = 0;
            List<RoomOnline_V1> _list = null;
            MobileResult mr = new MobileResult();
            Paging p = new Paging();

            p.pageIndex = page;
            p.pageSize = 20;
            _list = room.Get_Anchor_Nearby(p, lon, lat, ref _totalCount);

            var _totalPage = Tools.GetPageCount(_totalCount, 20);
            var _timeConfig = LiveBLL.GetHotTimeConfigList();

            if (_list != null && _list.Count > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = new { list = _list, totalPage = _totalPage, samecity = _nbool };//同城是否有数据
            }
            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        ///2：获取最新 v:1.2.0
        ///http://live.9158.com/Room/GetNewRoomOnline
        /// </summary>
        /// <returns></returns>
        //[OutputCache(Duration = 60, VaryByParam = "page")]
        public ActionResult GetNewRoomOnline(int page = 1)
        {
            int pagecount = 0, pageSize = 18, totalCount = 0;

            var mr = new Result();
            var list = room.GetNewOnlineRoom(page, pageSize, ref totalCount);
            pagecount = Tools.GetPageCount(totalCount, pageSize);

            if (list != null && list.Count > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = new { totalPage = pagecount, list = list };
            }

            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 我的房间的管理员
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        [OutputCache(Duration = 60, VaryByParam = "useridx")]
        public ActionResult GetMyRoomAdmin(int useridx = 0)
        {
            MobileResult mr = new MobileResult();
            if (useridx <= 0) return new BaseController().ParamError();

            var list = room.GetMyRoomAdmin(3, useridx);
            if (list != null && list.Count > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = list;
            }

            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 我的直播间
        /// live.9158.com/Room/GetMyLiveRoom?useridx=60068188
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [OutputCache(Duration = 60, VaryByParam = "useridx")]
        public ActionResult GetMyLiveRoom(int useridx = 0)
        {
            if (useridx <= 0) return new BaseController().ParamError();

            var _myRoom = room.GetMyLiveRoom(1, useridx);
            var _myManageRoom = room.GetMyLiveRoom(2, useridx);
            var mr = new Result();

            mr.code = "100";
            mr.msg = "success";
            mr.data = new { myRoom = _myRoom, myManageRoom = _myManageRoom };

            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 当前用户是否在播
        /// live.9158.com/Room/GetOnlineUserInfo?useridx=60068188
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public ActionResult GetOnlineUserInfo(int useridx = 0)
        {
            if (useridx <= 0) { return ParamError(); }

            var mr = new MobileResult();
            if (!Tools.IsMobile() && AppDataBLL.AppVersion == "0")
            {
                return Content(JsonConvert.SerializeObject(mr));
            }
            var model = room.GetOnlineUserInfoByIdx(useridx);
            if (model != null && model.useridx > 0)
            {
                model.lianMaiStatus = room.LianMaiStatus(useridx);

                mr.code = "100";
                mr.msg = "success";
                mr.data = model;
            }

            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 获取热搜城市列表
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 60 * 60)]
        public ActionResult GetHotCity()
        {
            var list = room.Get_HotCity();
            var mr = new MobileResult();
            if (list.Rows != null && list.Rows.Count > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = list;
            }
            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 获取当前用户所在城市的主播（v:150）
        /// live.9158.com/Room/GetRandomAnchor?useridx=60068188&lat=30.297089&lon=120.112718
        /// </summary>
        /// <param name="lon">经度:120.112718</param>
        /// <param name="lat">纬度:30.297089</param>
        /// <returns></returns>
        [OutputCache(Duration = 60 * 10, VaryByParam = "useridx")]
        public ActionResult GetRandomAnchor(int useridx = 0, double lon = 0.0, double lat = 0.0)
        {
            var list = room.Get_Anchor_ByRandom(lon, lat);
            var mr = new MobileResult();

            if (list != null && list.Count > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = list;
            }

            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 根据城市搜索主播（v:150）
        /// </summary>
        /// <returns></returns>
        public ActionResult SearchByPosition()
        {
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null) return new BaseController().ParamError();
            int _pageSize = 12;
            int _count = 0;
            int _totalPage = 0;
            int _pageIndex = int.Parse(dic["page"]);
            var _condition = TextHelper.FilterSpecial(dic["where"].Trim());

            //List<RoomOnline> list = new List<RoomOnline>();
            var mr = new Result();
            var list = room.Get_Anchor_ByPosition(_condition, _pageIndex, _pageSize, ref _count);

            _totalPage = Tools.GetPageCount(_count, _pageSize);

            if (list != null && list.Count > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = new { list = list, totalPage = _totalPage };
            }

            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 获取充值代理房间
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 20)]
        public ActionResult GetPayAdviceRoom()
        {
            MobileResult mr = new MobileResult();
            var list = room.Get_AgentRoom();

            if (list != null && list.Count > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = list;
            }
            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 金汉使用
        /// </summary>
        /// <param name="rtmp"></param>
        /// <returns></returns>
        public ActionResult GetOnlineRoomByrtmp(string param)
        {
            string rtmp = CryptoHelper.FromBase64(param);
            var mr = new Result();
            if (string.IsNullOrEmpty(param))
            {
                mr.code = "101";
                return Json(mr);
            }
            var model = room.GetRoomOnlineUserBy_rtmp(rtmp);
            if (model != null)
            {
                mr.code = "100";
                mr.data = model;
            }

            return Json(mr, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新注册用户推荐和关注推荐
        /// live.9158.com/Room/GetRecAnchor?type=1&sex=1
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="type">1：推荐一个在播</param>
        /// <returns></returns>
        public ActionResult GetRecAnchor(int type = 1, int sex = 0)
        {
            MobileResult mr = new MobileResult();

            List<RoomOnline> datalist = room.Get_RecAnchorList(type, 0, sex);

            if (datalist != null && datalist.Count > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = datalist;
            }

            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 热门推荐主播
        /// live.9158.com/Room/GetHotRecAnchor
        /// </summary>
        /// <returns></returns>
        public ActionResult GetHotRecAnchor()
        {
            MobileResult mr = new MobileResult();

            int _totalCount = 0, _nbool = 0;
            var _roomList = room.GetHotRecRank(ref _totalCount, ref _nbool);

            if (_roomList != null && _roomList.Count > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = new { roomList = _roomList };
            }

            return Content(JsonConvert.SerializeObject(mr));
        }
    }
}
