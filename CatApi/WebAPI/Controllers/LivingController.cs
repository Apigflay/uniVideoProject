using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BLL;
using Model;
using Common;
using Newtonsoft.Json;
using ThirdAPI;
using Model.Param;

namespace WebAPI.Controllers
{
    public class LivingController : BaseController
    {
        RoomBLL room = new RoomBLL();
        GiftBLL gift = new GiftBLL();
        LiveBLL livebll = new LiveBLL();

        /// <summary>
        /// 获取服务相关信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        //[OutputCache(Duration = 10, VaryByParam = "servertype,key")]
        public ActionResult GetServerInfo(string key = "", int servertype = -1)
        {
            var mr = new MobileResult();
            if (servertype <= -1) return new BaseController().ParamError();

            //大于0的金汉服务端调用
            if (string.IsNullOrEmpty(key) && key != "90iuewrwerGHJD" && servertype > 0)
            {
                mr.code = "106";
                mr.msg = "没有查询到数据";

                return Json(mr, JsonRequestBehavior.AllowGet);
            }
            var list = room.GetServerInfoByType(servertype).ToList();
            if (list != null)
            {
                mr.code = "100";
                mr.msg = "操作成功";
                mr.data = list;
            }
            return Json(mr, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取礼物列表
        /// live.9158.com/Living/GetGiftList?type=3&platform=0&isNewapp=1
        /// </summary>
        /// <param name="type">0:8个礼物 (所有老版本2016-6-1 add) 1,2：显示所有礼物(ios的是1，Android的有1,2不知道为何) 3:礼物分类</param>
        /// <param name="platform">平台类型，0：喵播</param>
        /// <param name="isNewapp">是否新版本喵播：1：是，0：否（上新版本喵播时临时使用）</param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult GetGiftList(int type = 0, int isNewapp = 0)
        {
            MobileResult mr = new MobileResult();
            var _tabList = gift.GetGiftTabList(0);
            //var _giftList = gift.getGiftList(type, isNewapp);
            var _giftList = gift.getGiftList(type, 0);
            if (_giftList != null && _giftList.Count > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = _giftList;

                if (type == 3)
                {
                    mr.data = new { giftList = _giftList, tabList = _tabList };
                }
            }
            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 开播前检测房间是否存在
        /// </summary>
        /// <param name="uIdx"></param>
        /// <returns></returns>
        public ActionResult CheckRoomExist()
        {
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);

            if (dic == null) return new BaseController().ParamError();

            int userIdx = int.Parse(dic["userIdx"].ToString());

            if (userIdx <= 0) return new BaseController().ParamError();

            int serverId, roomid, lianMai = 0, isStar = 0;

            Room r = room.GetServeridByUserIdx(userIdx, ref isStar);

            if (r != null && r.roomid > 0)
            {
                serverId = r.serverid;
                roomid = r.roomid;
            }
            else
            {
                serverId = -1;
                roomid = userIdx;
            }
            lianMai = room.LianMaiStatus(userIdx);

            MobileResult mr = new MobileResult();

            mr.code = "100";
            mr.msg = "success";
            mr.data = new { roomId = roomid, Id = serverId, lianMaiStatus = lianMai, theVoice = isStar };
            return Json(mr, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取地理位置 v2.0(金汉服务端主播开播调用)
        /// live.9158.com/Living/GetPosition?sign=TGhz9158&lat=28.846395&lng=112.087866
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="lng">经度 119.649506</param>
        /// <param name="lat">纬度 29.089524</param>
        /// <returns></returns>
        public ActionResult GetPosition(int useridx = 0, string sign = "", double lng = 0, double lat = 0)
        {
            string _country = "来自喵星", _province = "来自喵星", _city = "来自喵星";

            GeoPosition p = PositionHelper.Get_GeoPosition(lng, lat);
            MobileResult mr = new MobileResult();

            if (p.status != 0)
            {
                mr.code = "100";
                mr.msg = "操作成功";
                mr.data = new { country = _country, province = _province, city = _city };

                return Content(JsonConvert.SerializeObject(mr));
            }
            else if (p.status == 0)
            {
                _country = p.result.addressComponent.country;
                _province = p.result.addressComponent.province;
                _city = p.result.addressComponent.city;
            }
            if (p.result.cityCode != 0 && _country != "中国")
            {
                //记录海外城市以便转成中文
                //LiveBLL.AbroadCity_Save(p.result.cityCode, _province, "");

                _city = _province;
                _country = "海外";
                _province = "海外";
            }

            mr.code = "100";
            mr.msg = "操作成功";
            mr.data = new { country = _country, province = _province, city = _city };

            return Json(mr, "text/html; charset=UTF-8", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 广告位(如果data为空客户端版本会闪退)
        /// live.9158.com/Living/GetAD?type=2&version=261&id=20
        /// </summary>
        /// <param name="type">0:大厅广告图 1:充值页面广告图 2:启动App广告</param>
        /// <returns></returns>
        //[OutputCache(Duration = 60 * 10, VaryByParam = "type")]
        public ActionResult GetAD(int type = 0, string version = "10", int id = 0)
        {
            MobileResult mr = new MobileResult();
            string auditStatus = AppDataBLL.AuditStatus;
            string areaid = AppDataBLL.GetAreaid;
            List<ADsportFull> adFull = null;
            List<ADspotsRoom> adRoom = null;

            //if (auditStatus == "0" && type != 1)
            //{
            //    mr.code = "101";
            //    mr.msg = "No data";
            //    return Content(JsonConvert.SerializeObject(mr));
            //}

            if (type == 1)
            {
                adRoom = livebll.GetList(type);
                mr.data = adRoom;
            }
            else
            {
                adFull = livebll.GetListByFull(type, areaid);
                mr.data = adFull;
            }

            if ((type == 1 && adRoom.Count > 0) || (type != 1 && adFull.Count > 0))
            {
                mr.code = "100";
                mr.msg = "success";
            }

            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 热门列表广告位
        /// live.9158.com/Living/GetHotListAds?devtype=1
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 60 * 10, VaryByParam = "devtype")]
        public ActionResult GetHotListAds(int devtype = 1)
        {
            MobileResult mr = new MobileResult();

            List<HotAds> list = new LiveBLL().Get_HotAdsList(devtype);
            if (list != null && list.Count > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = list;
            }
            return Json(mr, "text/html; charset=UTF-8", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPayMenuList()
        {
            Result r = new Result();
            var list = new PayBLL().getPayMenuList();
            if (list != null && list.Count > 0)
            {
                r.code = "100";
                r.msg = "success";
                r.data = list;
            }

            return Json(r, "text/html; charset=UTF-8", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取往期活动列表
        /// live.9158.com/Living/Active
        /// </summary>
        /// <returns></returns>
        public ActionResult Active(int useridx = 0, int page = 1)
        {
            var result = new MobileResult();

            var ActiveList = LiveBLL.Get_ActiveList();

            if (ActiveList != null && ActiveList.Count > 0)
            {
                result.code = "100";
                result.msg = "success";
                result.data = new { activeList = ActiveList, totalPage = 1 };
            }

            return Content(JsonConvert.SerializeObject(result));
        }

        /// <summary>
        /// 获取房间内活动
        /// http://live.9158.com/Living/GetRoomActive?roomid=0
        /// </summary>
        /// <param name="areaid"></param>
        /// <returns></returns>
        public ActionResult GetRoomActive(int roomid = 0)
        {
            MobileResult mr = new MobileResult();

            int areaid = int.Parse(AppDataBLL.GetAreaid);

            var ActiveList = LiveBLL.Get_ActiveRoomList(roomid, areaid);

            if (ActiveList != null && ActiveList.Count > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = new { activeList = ActiveList };
            }
            return Json(mr, JsonRequestBehavior.AllowGet);
            //return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 脸部识别文件以及特效礼物
        /// live.9158.com/Living/FaceStickers
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 60 * 10)]
        public ActionResult FaceStickers()
        {
            var mr = new MobileResult();
            var list = LiveBLL.Get_AllFaceStickers();

            if (list != null & list.Count > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = new
                {
                    facePath = "http://liveimg.9158.com/FaceResource/",
                    faceGift = list.FindAll(f => f.faceType == 1),
                    faceStickers = list.FindAll(f => f.faceType == 2)
                };
            }
            return Json(mr, "text/html; charset=UTF-8", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 视频存储(金汉服务器调用)live.9158.com/Living/StoredVideo?useridx=&flv=&key=TGhzMiaobolive01980
        /// Time:2016-12-1
        /// </summary>
        /// <param name="useridx">88888888</param>
        /// <param name="flv">://hdl.9158.com/live/jdlajl708dgjod9.flv</param>
        /// <returns></returns>
        public ActionResult StoredVideo(int useridx = 0, string flv = "", string key = "")
        {
            if (string.IsNullOrEmpty(flv) || useridx <= 0 || key != "TGhzMiaobolive01980")
            {
                return Content("-1");
            }

            string msg = MNSHelper.StoredVideo(flv);

            string flvURL = flv.Replace("http://hdl.9158.com/live/", "").Replace(".flv", "");
            string storedURL = string.Format("http://miaobolive.oss-cn-hangzhou.aliyuncs.com/{0}/vod.m3u8", flvURL);

            if (msg != "fail")
            {
                //自己业务逻辑,把阿里的视频存储地址保存到自己的数据库
                LiveBLL.StoredVideo_Save(useridx, flvURL, storedURL);
            }

            return Content(msg);
        }

        /// <summary>
        /// Describe：意见反馈
        /// createAt：2017-05-31 17:19:25
        /// createAuthor:zhaorui
        /// </summary>
        /// <returns></returns>
        public ActionResult feedback()
        {
            FeedBackParam dicParam = CryptoHelper.GetAESBinaryModelParam<FeedBackParam>(CryptoHelper.Live_KEY);

            if (dicParam == null) return new BaseController().ParamError();

            int uidx = dicParam.useridx;
            var content = dicParam.content;
            var contact_Way = dicParam.contact;
            var osVersion = dicParam.systemVersion;
            var appVersion = dicParam.version;
            var deviceType = dicParam.deviceType;

            content = TextHelper.FilterSpecial(content);
            contact_Way = TextHelper.FilterSpecial(contact_Way);

            MobileResult mr = new MobileResult();

            if (string.IsNullOrEmpty(content) || content.Length < 4)
            {
                mr.code = "101";
                mr.msg = "提交内容过短";

                return Content(JsonConvert.SerializeObject(mr));
            }

            int result = LiveBLL.Insert_Feedback(uidx, content, contact_Way, osVersion, appVersion, deviceType);
            if (result > 0)
            {
                mr.code = "100";
                mr.msg = "success";
            }

            return Content(JsonConvert.SerializeObject(mr));
        }

    }
}
