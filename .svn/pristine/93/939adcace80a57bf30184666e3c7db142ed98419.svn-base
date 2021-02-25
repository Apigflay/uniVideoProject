using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
using Newtonsoft.Json;
using Common;

/*********************此API提供给第三方使用********************
 * CreatedAt：2017-09-19 16:03:42
 * AuthorBy： zhaorui
 */
namespace WebAPI.Controllers
{
    public class thirdAPIController : BaseController
    {
        private UserInfoBLL _user = new UserInfoBLL();
        private RoomBLL room = new RoomBLL();
        private PasswordBLL _password = new PasswordBLL();
        private CommonBLL _common = new CommonBLL();

        /// <summary>
        /// 游戏那边使用(判断是否是主播)
        /// live.9158.com/thirdAPI/AnchorInfo?sign=TGhzGame&useridx=60068188
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public ActionResult AnchorInfo(string sign, int useridx = 0)
        {
            MobileResult mr = new MobileResult();

            if (string.IsNullOrEmpty(sign) || !sign.Equals("TGhzGame"))
            {
                return Content("0");
            }

            int result = _user.Game_IsAnchor(useridx);

            return Content(result.ToString());
        }

        /// <summary>
        /// 开播前检测房间是否存在（ClientPc控制器还未切过来）
        /// https://live.9158.com/thirdApi/CheckRoomExist?useridx=60068188&
        /// </summary>
        /// <param name="uIdx"></param>
        /// <returns></returns>
        public ActionResult CheckRoomExist(int userIdx = 0, string CODE = "")
        {
            //Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);

            //if (dic == null) return new BaseController().ParamError();

            //int userIdx = int.Parse(dic["userIdx"].ToString());

            if (userIdx == 0 || CODE == "") return new BaseController().ParamError();
            string keyStr = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(userIdx + CryptoHelper.LivePc_key, "md5").ToLower();
            if (CODE != keyStr) return new BaseController().Error();

            int serverId, roomid, lianMai = 0, isStar = 0;
            string familyName = "";
            Room r = room.GetServeridByUserIdx_PC(userIdx, ref isStar);
            if (r != null && r.roomid > 0)
            {
                serverId = r.serverid;
                roomid = r.roomid;
                familyName = r.familyName;
            }
            else
            {
                serverId = -1;
                roomid = userIdx;
                familyName = "";
            }
            lianMai = room.LianMaiStatus(userIdx);

            MobileResult mr = new MobileResult();

            mr.code = "100";
            mr.msg = "success";
            mr.data = new { roomId = roomid, Id = serverId, familyName = familyName, lianMaiStatus = lianMai, theVoice = isStar };
            return Json(mr, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 是否绑定手机号，提供给喵拍使用
        /// https://live.9158.com/ThirdAPI/IsBindPhone?useridx=&sign=TGhzCert
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="useridx"></param>
        /// <returns>1：未绑定，0：绑定，-2：不需要绑定/开关未开启</returns>
        public ActionResult IsBindPhone(string sign, int useridx = 0)
        {
            MobileResult mr = new MobileResult();

            if (string.IsNullOrEmpty(sign) || !sign.Equals("TGhzCert"))
            {
                return Content("-1");
            }

            //绑定手机号开关判断
            LiveConfig lc = LiveBLL.Get_LiveConfigById(29);
            if (lc.data == "0")
            {
                return Content("-2");
            }

            //IPModel ip_info = _common.GetAddressByIP();

            int result = _password.IsCheckPhonebyUseridx(useridx);

            return Content(result.ToString());
        }

        /// <summary>
        /// live.9158.com/thirdApi/CheckLiveid?liveid=6bfcf29cb653620021490d33901a06ff
        /// </summary>
        /// <param name="liveid"></param>
        /// <returns></returns>
        public ActionResult CheckLiveid(string liveid)
        {
            MobileResult mr = new MobileResult();

            if (string.IsNullOrEmpty(liveid))
            {
                return Content("0");
            }
            string[] sArrary = liveid.Split('_');
            //liveid = liveid.Remove(liveid.IndexOf("_", StringComparison.CurrentCultureIgnoreCase));

            liveid = sArrary.Length > 0 ? sArrary[0] : "";

            string result = room.Check_LiveId(liveid);
            return Content(result);
        }

        /// <summary>
        /// 获取用户头像（黄金海那边调用）
        /// live.9158.com/thirdAPI/GetUserPhoto?sign=TGhz9158&useridx=60068188
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public ActionResult GetUserPhoto(string sign, int useridx = 0)
        {
            if (string.IsNullOrEmpty(sign) || !sign.Equals("TGhz9158") || useridx <= 0)
            {
                return Content("");
            }

            UserInfo user = _user.GetLiveUserInfoByIdx(useridx);

            return Content(user.smallpic);
        }
    }
}
