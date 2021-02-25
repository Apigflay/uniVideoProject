using BLL;
using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPI.Controllers
{
    public class ClientPcController : Controller
    {
        //
        // GET: /Default1/
        RoomBLL room = new RoomBLL();

        /// <summary>
        /// 开播前检测房间是否存在
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
    }
}
