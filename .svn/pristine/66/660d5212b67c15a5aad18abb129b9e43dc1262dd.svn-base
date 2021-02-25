using BLL;
using BLL.Logic;
using Common;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;

namespace WebAPI.Controllers
{
    public class AboutController : BaseController
    {
        private LiveBLL live = new LiveBLL();
        private RoomBLL room = new RoomBLL();

        /// <summary>
        /// 获取IP信息
        /// live.9158.com/about/ip
        /// </summary>
        /// <returns></returns>
        public ActionResult ip()
        {
            IPModel ip_info = CommonBLL.Instance.GetAddressByIP();
            MobileResult mr = new MobileResult();

            mr.data = new { ip = Tools.GetRealIP(), ip_info = ip_info };

            return Content(JsonConvert.SerializeObject(mr));
        }
        public ActionResult GetEntrance()
        {
            MobileResult mr = new MobileResult();
            if (!Tools.IsMobile()) { return new BaseController().ParamError(""); }
            mr.code = "100";
            mr.msg = "success";
            mr.data = new
            {
                game1 = new { name = "拉霸", gameImgUrl = "http://live.imeyoo.com/Content/common/images/game/yxcq.png", GameUrl = "118" },
                game2 = new { name = "运动会", gameImgUrl = "http://live.imeyoo.com/Content/common/images/game/ydh.png", GameUrl = "101" }

            };
            return Content(JsonConvert.SerializeObject(mr));
        }

        public ActionResult Error()
        {
            return View("Error");
        }

        /// <summary>
        /// live.9158.com/About/Error404
        /// </summary>
        /// <returns></returns>
        public ActionResult Error404()
        {
            MobileResult mr = new MobileResult();
            mr.code = "404";
            mr.msg = "Server Error";

            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// App相关配置
        /// live.9158.com/About/AppConfig
        /// </summary>
        /// <returns></returns>
        //[OutputCache(Duration = 60 * 5)]
        public ActionResult AppConfig()
        {
            Result r = new Result();
            //获取所有的版本号列表
            List<LiveVersion> lvList = LiveBLL.GetAppVer_List();

            var bigGiftVer = lvList.Find(f => f.id == 5).version;

            r.code = "100";
            r.msg = "success";
            r.data = new
            {
                giftVer = LiveBLL.GetAppVer_ById(1).version,
                barrage = AppDataBLL.barrage,
                giftSendNum = AppDataBLL.GiftNum(),
                hbPrice = AppDataBLL.hongbaoPrice,
                totalBarrage = AppDataBLL.totalBarrage,
                transferPrice = AppDataBLL.transferPrice,
                bigGift = new
                {
                    path = "http://img.imeyoo.com/Gift/",
                    pathNew = "http://img.imeyoo.com/bigGift/",
                    config = AppDataBLL.bigGiftConfig,
                    configNew = "http://img.imeyoo.com/bigGift/AnimationConfig.json",
                    giftRange = AppDataBLL.BigGiftRange(),
                    version = bigGiftVer
                },
                h5Switch = new
                {
                    isShowMyIncome = AppDataBLL.myIncome,
                    isShowExChange = AppDataBLL.exchange,
                    isShowDevote = "0"// AppDataBLL.receive
                },
                gameConfig = new string[] { },
                gameCenter = live.GetAllGameInfo(1),//获取游戏中心配置文件,
                liveConfig = LiveBLL.Get_LiveConfigList(999)
            };
            return Content(JsonConvert.SerializeObject(r));
            //return Json(r, "text/html; charset=UTF-8", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 充值开关 来访IP为北京、香港、美国、新加坡时，只显示苹果支付。来自这4个地区的举报最多。针对IP地址对应不同地区，单独设置可用的支付方式（比如仅显示Apple支付）
        /// live.9158.com/About/PaySwitch
        /// </summary>
        /// <param name="id">bundleId</param>
        /// <param name="version">版本号</param>
        /// <returns></returns>
        //[OutputCache(Duration = 60 * 10)]
        public ActionResult PaySwitch(string version = "100")
        {
            //2：显示国内支付 1：显示苹果充值(包含黑名单地区) 0：都显示(不包含黑名单地区)

            string ip = Tools.GetRealIP();
            Result r = new Result();

            Location loc = PositionHelper.GetLocationInfo(ip);

            if (AppDataBLL.PayblackAddress(loc.Province) ||
                AppDataBLL.PayblackAddress(loc.City) ||
                AppDataBLL.PayblackAddress(loc.Country))
            {
                r.code = "100";
                r.data = "1";
                return Json(r, "text/html; charset=UTF-8", JsonRequestBehavior.AllowGet);
            }

            var payState = AppDataBLL.paySwitch;
            if (payState == "2")
            {
                r.code = "100";
                r.data = "2";
            }
            else if (payState == "1")
            {
                r.code = "100";
                r.data = "1";
            }
            else
            {
                r.code = "100";
                r.data = "0";
            }
            return Json(r, "text/html; charset=UTF-8", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 审核开关 Audit Switch，隐藏国内充值以及其他
        /// live.9158.com/About/isHidden?bundleid=500&apiVersion=2&curVersion=100
        /// </summary>
        /// <param name="bundleid">当前客户端对应的bundleid</param>
        /// <param name="curVersion">current App Version</param>
        /// <param name="apiVersion">当前API版本号</param>
        /// <returns></returns>
        [HttpGet]
        //[ActionName("AuditSwitch")]//原名字就不能用了
        public ActionResult LiveCenter(int bundleid = 20, int apiVersion = 1, string curVersion = "100")
        {
            curVersion = curVersion.Replace(".", "");
            if (curVersion == string.Empty) curVersion = "100";
            int currentVersion = int.Parse(curVersion);

            MobileResult mr = new MobileResult();

            //返回当前包版本号是否是审核状态  1：正常，0：审核
            int auditStatus = LiveBLL.AuditStatus(bundleid, currentVersion);

            mr.code = "100";
            mr.msg = "success";//不能改
            mr.data = auditStatus.ToString();


            ////if (apiVersion == 2)
            ////{
            ////    //pid: 11：浙江，31：北京
            ////    IPModel ip_info = CommonBLL.Instance.GetAddressByIP();
            ////    if (ip_info.Province == "美国" || ip_info.pid == 31)
            ////    {
            ////        areaStatus = 1;
            ////    }

            ////   // mr.data = new { auditStatus = auditStatus, blackAreaStatus = areaStatus, areaid = ip_info.areaid };
            ////    mr.data = new { auditStatus = auditStatus, blackAreaStatus = areaStatus, areaid = 0 };
            ////}

            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 查询当前版本是否需要更新(updateCheck)
        /// live.9158.com/About/getAppVersion?devType=ios&curVersion=100&bundleid=3
        /// </summary>
        /// <param name="devType">设备类型：android:主包,ios 主包,other 马甲包</param>
        /// <param name="curVersion">客户端当前版本号</param>
        /// <param name="bundleid">针对马甲包来查询对应的更新版本号</param>
        /// <returns></returns>
        public ActionResult GetAppVersion(string devType = "ios", string curVersion = "100", int bundleid = 0)
        {
            curVersion = curVersion.Replace(".", "");
            if (curVersion == string.Empty) curVersion = "100";

            if (devType.Contains("android"))
            {
                bundleid = bundleid > 10 ? bundleid : 10;
            }
            else if (devType.Contains("ios"))
            {
                bundleid = 20;
            }

            LiveVersion lv = LiveBLL.GetAppVer_ById(bundleid);
            if (lv == null || lv.id < 2) return new BaseController().ParamError();

            int updateVersion = int.Parse(lv.version);

            MobileResult mr = new MobileResult();

            if (int.Parse(curVersion) < updateVersion)
            {
                mr.code = "100";
                mr.msg = "有最新的包哦，亲快下载吧~";
                mr.data = new
                {
                    isForceUpdate = lv.isUpdate,//1强制更新
                    updateInfo = lv.updateInfo,
                    androidUrl = lv.updateURL,
                    iosUrl = lv.updateURL
                };
            }
            else
            {
                mr.code = "110";
                mr.msg = "当前版本已是最新版本";
            }
            return Json(mr, "text/html; charset=UTF-8", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 页面访问统计和下载统计
        /// </summary>
        /// <returns></returns>
        public ActionResult AccessStatis(int type = 2)
        {
            if (Request.IsAjaxRequest())
            {
                StatisticsBLL.LiveApi_Statis(type);
            }

            return Content("success");
        }

        /// <summary>
        /// 获取官方信息
        /// live.9158.com/About/OfficialInfo
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 20)]
        public ActionResult OfficialInfo()
        {
            MobileResult mr = new MobileResult();

            //官方联系方式
            System.Data.DataTable dt = LiveBLL.Get_Custom_Contact();
            if (dt != null && dt.Rows.Count > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = new { contact = dt };
            }
            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 本页用于生成验证码及其图片
        /// </summary>
        /// <returns></returns>
        public ActionResult getcaptcha()
        {
            Captcha();
            return View();
        }

        /// <summary>
        /// 搜索索引资源，每小时更新一次
        /// live.9158.com/about/createSIndex?sign=P2A58D1ME2DWSXKC
        /// </summary>
        /// <param name="sign"></param>
        /// <returns></returns>
        public ActionResult createSIndex(string sign = "")
        {
            SearchBLL s = new SearchBLL();

            if (sign.Equals("P2A58D1ME2DWSXKC"))
            {
                LuceneHelper.CreateIndex(s.Live_IndexSearch(), true);

                return Content("success");
            }
            return Content("-1");
        }

        #region 协议H5页面

        /// <summary>
        /// 注册协议
        /// </summary>
        /// <returns></returns>
        public ActionResult UserAgreeMent()
        {
            return View();
        }
        /// <summary>
        /// 主播协议
        /// </summary>
        /// <returns></returns>
        public ActionResult AnchorAgreeMent()
        {
            return View();
        }
        // GET: /About/Privacy
        /// <summary>
        /// 隐私条款
        /// </summary>
        /// <returns></returns>
        public ActionResult Privacy()
        {
            return View();
        }

        #endregion

        #region 普通验证码方法

        /// <summary>
        /// 获取普通验证码
        /// </summary>
        public void Captcha()
        {
            ////生成随机生成器
            Random random = new Random();

            string validateNum = "";
            string s = "QWERTYUIOPLKJHGFDSAZXCVBNM1234567890";

            for (int i = 0; i < 4; i++)
            {
                validateNum += s[random.Next(s.Length)];
            }
            Session["Ajax_ValidateNum"] = validateNum;

            // 在此处放置用户代码以初始化页面
            Bitmap image = new Bitmap((int)Math.Ceiling(validateNum.Length * 14.5), 22);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //清空图片背景色
                g.Clear(Color.White);
                //画图片的干扰线
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }

                Font font = new Font("Arial", 12, (FontStyle.Bold | FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.OrangeRed, Color.OrangeRed, 1.2f, true);
                g.DrawString(validateNum, font, brush, random.Next(1) + 1, random.Next(1) + 1);

                //画图片的前景干扰点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                //保存图片数据
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                //输出图片流
                Response.Clear();
                Response.ContentType = "image/jpeg";
                Response.BinaryWrite(stream.ToArray());
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        #endregion

    }
}
