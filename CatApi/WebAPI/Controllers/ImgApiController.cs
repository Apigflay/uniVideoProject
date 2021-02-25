using Common;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPI.Controllers
{
    public class ImgApiController : Controller
    {
 

        public ActionResult HTimgUpload()
        {
            LogHelper.WriteLog(LogFile.Warning, "修改头像过于频繁：43,次数：234");
            string giftid_s = Request.Form["giftId"].ToString();
            string gname_s = Request.Form["gname"].ToString();
            string price_s = Request.Form["price"].ToString();
            string content_s = Request.Form["content"].ToString();
            string unit = Request.Form["txtunit"].ToString();
            string rate_s = Request.Form["rate"].ToString();
            string roomRate_s = Request.Form["roomRate"].ToString();

            LogHelper.WriteLog(LogFile.Debug, giftid_s+ gname_s);

            MobileResult mr = new MobileResult();
            HttpFileCollection fileList = System.Web.HttpContext.Current.Request.Files;
            string ImgUrl1 = "";
            string ImgUrl2 = "";
            string UploadResult = Common.Tools.SaveImgae(fileList, "gift", ref ImgUrl1, ref ImgUrl2);
            if (UploadResult == "0")
            {
                LogHelper.WriteLog(LogFile.Debug, "【相册上传上传成功：");
                mr.code = "100";
                mr.msg = "成功";
            }
            else
            {
                mr.code = "101";
                mr.msg = UploadResult;
            }


            return Content(JsonConvert.SerializeObject(mr));
        }

    }
}
