using System;
using System.Web;
using System.Web.Mvc;

using BLL;
using Common;
using Common.Core;
using Model;
using System.Collections.Generic;
using Newtonsoft.Json;
using BLL.Common;
using Model.Param;
using System.Linq;
using static Common.PositionHelper;
using System.Threading;
using static BLL.UserInfoBLL;

namespace WebAPI.Controllers
{
    public class UserInfoController : BaseController
    {
        private UserInfoBLL user = new UserInfoBLL();
        private RoomBLL room = new RoomBLL();
        private SearchBLL s = new SearchBLL();
        private RankBLL _rank = new RankBLL();
        UserInfoBLL _userbll = new UserInfoBLL();

        /// <summary>
        /// 获取我的卡片信息（个人中心vip到期查询）
        /// live.9158.com/UserInfo/GetUserInfo?touseridx=60068188&curuseridx=1
        /// </summary>
        /// <param name="touseridx">谁的用户信息</param>
        /// <param name="curuseridx">当前login用户useridx</param>
        /// <param name="signature">签名</param>
        /// <param name="timestamp">时间戳</param>
        /// <returns></returns>
        //[OutputCache(Duration = 20, VaryByParam = "touseridx")]
        public ActionResult GetUserInfo(int touseridx = 0, int curuseridx = 0, string signature = "", string timestamp = "")
        {
            string md5Token = "1811a681-1312-11e6-b3d6khj2";
            string mySign = CryptoHelper.ToMD5("/userinfo/getuserinfo" + timestamp + curuseridx + md5Token);
            string _userLabel = "";
            MobileResult mr = new MobileResult();

            if (touseridx <= 0 || curuseridx <= 0 || (!Tools.IsMobile() && AppDataBLL.AppVersion == "0")) { return new BaseController().ParamError(""); }

            var _baseinfo = user.GetLiveUserInfoByIdx(touseridx);
            if (_baseinfo != null && _baseinfo.useridx > 0)
            {
                var _otherinfo = user.GetOtherUserInfo(touseridx);
                var _familyinfo = user.Get_MyFamilyInfo(2, touseridx);//2017-06-13 16:55:15 dataType参数更改为2，客户端不用H5页面了

                //2017-12-21 add by zhaorui
                if (_otherinfo.anchorLevel > 0)
                {
                    _userLabel = AppDataBLL.GetAnchorLabelName(_otherinfo.anchorLevel);
                }
                else if (_baseinfo.grade > 0)
                {
                    _userLabel = AppDataBLL.GetUserLabelName(_baseinfo.grade);
                }

                mr.code = "100";
                mr.msg = "success";
                mr.data = new
                {
                    baseInfo = _baseinfo,
                    otherInfo = _otherinfo,
                    familyInfo = _familyinfo,
                    userLabel = _userLabel
                };
            }

            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 搜索用户
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        [ValidateInput(false)]
        public ActionResult SearchUser()
        {
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null)
            {
                //LogHelper.WriteLog(LogFile.Log, "【搜索参数错误】");
                return new BaseController().ParamError();
            }

            int pageSize = 15, count = 0;
            int pageIndex = int.Parse(dic["Page"]);
            int Sex = int.Parse(dic["Sex"]);
            var condition = TextHelper.FilterSpecial(dic["Where"].Trim());
            //int pageIndex = 1;
            //int Sex = 1;
            //var condition = "13";

            List<UserSearchInfo> dataList = new List<UserSearchInfo>();
            var mr = new MobileResult();
            if (!Tools.IsMobile() && AppDataBLL.AppVersion == "0")
            {
                //LogHelper.WriteLog(LogFile.Log, "【搜索设备异常】{0}", UtilHelper.GetUserAgent());

                return Content(JsonConvert.SerializeObject(mr));
            }

            if (string.IsNullOrEmpty(condition))
            {
                //LogHelper.WriteLog(LogFile.Log, "【搜索关键词为空】{0}", condition);

                return Content(JsonConvert.SerializeObject(mr));
            }
            if ((Tools.numRegex.IsMatch(condition) && (condition.Length < 4 || condition.Length > 10)))
            {
                //LogHelper.WriteLog(LogFile.Log, "【搜索条件不满足】{0}", condition);

                return Content(JsonConvert.SerializeObject(mr));
            }
            dataList = s.Live_SearchNew(condition, pageIndex, pageSize, Sex, ref count);

            //pagecount = Tools.GetPageCount(count, pageSize);

            if (dataList != null && dataList.Count > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = new { list = dataList, totalCount = count };
            }
            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="userIdx"></param>
        /// <param name="token"></param>
        /// <param name="platform">1：喵播 ，2：喵拍</param>
        /// <returns></returns>
        public ActionResult Upload(int userIdx = 0, string token = "", int platform = 1)
        {
            MobileResult r = new MobileResult();

            #region 业务判断

            //if (userIdx <= 0 || !Tools.IsMobile())
            //{
            //    r.code = "101";
            //    r.msg = "param error";
            //    return Content(JsonConvert.SerializeObject(r));
            //}
            UserInfoBLL _userbll = new UserInfoBLL();
            string userTokenValue = _userbll.GetUserToken(userIdx, 0);

            if (!string.IsNullOrEmpty(userTokenValue) && !token.Equals(userTokenValue))
            {
                r.code = "103";
                r.msg = "获取用户信息失败,请退出重新登录";
                return Content(JsonConvert.SerializeObject(r));
            }

            ////远程连接图片服务器连接是否成功
            //if (!ToolsHelper.Connect("60.191.208.117", "9158music", "9158music"))
            //{
            //    return PicConnectFailed();
            //}

            HttpFileCollection files = HttpContext.ApplicationInstance.Request.Files;
            HttpPostedFileBase file = HttpContext.Request.Files["picture"];

            if (files == null || file == null)
            {
                r.code = "113";
                r.msg = "upload Fail";
                return Content(JsonConvert.SerializeObject(r));
            }
            //int count = 0;
            //if (!CacheBLL.VerifyUpdatePhoto(2, userIdx, ref count))
            //{
            //    r.code = "114";
            //    r.msg = "修改图像过于频繁，请予24小时后再来修改。";
            //    LogHelper.WriteLog(LogFile.Warning, "修改头像过于频繁：{0},次数：{1}", userIdx, count);
            //    return Content(JsonConvert.SerializeObject(r));
            //}

            string webpic_min = "", webpic_max = "";

            #endregion

            ////step1 先判断是否是喵拍过来的主播
            ////step1 先判断是主播还是用户
            ////step2 如果是主播先判断是否有待审核的数据
            //var myRoom = room.GetMyLiveRoom(5, userIdx);
            //if (myRoom != null && myRoom.roomid > 0 && platform == 2)
            //{
            //    r.code = "119";
            //    r.msg = "UpdateFailed";
            //    return Content(JsonConvert.SerializeObject(r));
            //}
            //int audit = 0;

            ////add by zhaorui 用户上传头像也需要进行审核2018-1-3
            ////if (myRoom != null && myRoom.roomid > 0 && platform == 1)
            ////{
            //audit = user.UserPhotoAudit(1, userIdx, "", "", "");
            //if (audit == -2)
            //{
            //    r.code = "118";
            //    r.msg = "已有头像正在审核，请等待结果";
            //}
            //else if (audit == 1)
            //{
            //    PicSaveBLL.SavePhoto(1, platform, userIdx, file, ref webpic_min, ref webpic_max);

            //    audit = user.UserPhotoAudit(2, userIdx, "", webpic_min, webpic_max);
            //}
            //if (audit == 10)
            //{
            //    r.code = "120";
            //    r.msg = "提交成功，工作喵正在加紧审核";

            //    CacheBLL.VerifyUpdatePhoto(1, userIdx, ref count);
            //}
            //}
            //else//用户
            //{
            PicSaveBLL.SavePhoto(1, platform, userIdx, file, ref webpic_min, ref webpic_max);

            int ret = user.UpdateUserPhoto(userIdx, webpic_min, webpic_max);
            if (ret > 0)
            {
                r.code = "100";
                r.msg = "上传成功";
                r.data = new { pic_140 = webpic_min, pic_640 = webpic_max };

                //CacheBLL.VerifyUpdatePhoto(1, userIdx, ref count);
            }
            //}
            return Content(JsonConvert.SerializeObject(r));
        }

        /// <summary>
        /// 我的守护位
        /// live.9158.com/UserInfo/MyWard?useridx=60068188
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        [OutputCache(VaryByParam = "useridx", Duration = 60 * 10)]
        public ActionResult MyWard(int useridx = 0)
        {
            if (useridx <= 0) return new BaseController().ParamError();

            List<RankInfo> rankList = null;// _rank.Get_MyWard(useridx);//TODO 查询该用户用喵币购买守护的数据

            if (rankList == null)
            {
                rankList = _rank.Get_MyWard_SendGift(useridx);
            }
            var mr = new MobileResult();

            if (rankList != null && rankList.Count > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = rankList;
            }
            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 系统封号操作（第三方调用）
        /// live.9158.com/UserInfo/UserBlack?useridx=0&roomid=0&key=&content=test
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="roomid"></param>
        /// <param name="key"></param>
        /// <param name="content">发送内容</param>
        /// <returns></returns>
        public ActionResult UserBlack(int useridx, int roomid, string key, string content = "")
        {
            DateTime date = DateTime.Now.AddYears(100);
            string m = "JLJ7-VB3H-CFDE-5ZDG-LA8S";
            string myKey = CryptoHelper.ToMD5(useridx + "|" + roomid + "|" + m).ToLower();

            if (myKey != key)
            {
                return Content("-1");
            }

            int ret = user.UserBlackInsert(useridx, roomid, date, content);

            return Content(ret.ToString());
        }

        /// <summary>
        /// 家族详细信息
        /// live.9158.com/UserInfo/FamilyInfoView?roomid=61542879&useridx=61542879
        /// </summary>
        /// <param name="roomid"></param>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public ActionResult FamilyInfoView(int roomid = 0, int useridx = 0)
        {
            if (roomid <= 0 || useridx <= 0) { return View("Error"); }

            var _myfamily = user.Get_MyFamilyInfo(2, roomid);
            if (_myfamily != null && _myfamily.useridx > 0)
            {
                //如果联系方式都没有
                if (!string.IsNullOrEmpty(_myfamily.mobilePhone) ||
                    !string.IsNullOrEmpty(_myfamily.weixin) ||
                    !string.IsNullOrEmpty(_myfamily.qq))
                {
                    ViewBag.haveContact = 1;
                }

                return View(_myfamily);
            }
            else
            {
                return View("Error");
            }
        }

        /// <summary>
        /// 修改用户昵称和签名
        /// </summary>
        /// <returns></returns>
        public ActionResult updateInfo()
        {
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null || !dic.ContainsKey("useridx"))
            {
                return new BaseController().ParamError("");
            }
            int type = int.Parse(dic["type"]);//1:修改昵称，2：修改签名
            int useridx = int.Parse(dic["useridx"]);
            var token = dic["token"];
            var content = TextHelper.FilterSpecial(dic["content"]);
            content = TextHelper.FilterSql(content);

            MobileResult mr = new MobileResult();

            if (useridx <= 0)
            {
                mr.code = "101";
                mr.msg = "param error";
                return Json(mr, JsonRequestBehavior.AllowGet);
            }

            string userTokenKey = CacheKeys.MiaoPai_USER_TOKEN + useridx;
            string userTokenValue = MemcachedHelper.Get<string>(userTokenKey);

            if (string.IsNullOrEmpty(userTokenValue) || !token.Equals(userTokenValue))
            {
                LogHelper.WriteLog(LogFile.Log, "【喵拍修改昵称登陆信息过期】" + useridx + ",token:" + token + ",memToken:" + userTokenValue);
                mr.code = "103";
                mr.msg = "登陆信息过期";
                return Json(mr, JsonRequestBehavior.AllowGet);
            }

            var ret = user.updateInfo(type, useridx, content);

            if (ret == 1)
            {
                mr.code = "100";
                mr.msg = "success";
            }
            return Json(mr, "text/html; charset=UTF-8", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 电子签约确定
        /// </summary>
        /// <returns></returns>
        public ActionResult SigningConfirm()
        {
            Dictionary<string, string> dicParam = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            MobileResult mr = new MobileResult();

            if (dicParam == null || !dicParam.ContainsKey("useridx")) return new BaseController().ParamError();
            int useridx = int.Parse(dicParam["useridx"]);

            int result = user.ElectronicSigning_Record(useridx);
            if (result > 0)
            {
                mr.code = "100";
                mr.msg = "success";
            }
            return Content(JsonConvert.SerializeObject(mr));
        }

        #region 相册功能

        /// <summary>
        /// 相册上传
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="token"></param>
        /// <param name="albumtype">0：普通照片，1：阅后及焚</param>
        /// <returns></returns>
        //[HttpPost]
        public ActionResult uploadAlbum(int userIdx = 0, string token = "", int albumtype = 0)
        {
            MobileResult mr = new MobileResult();
            HttpFileCollection fileList = System.Web.HttpContext.Current.Request.Files;

            #region 逻辑判断

            if (userIdx == 0)
            {
                return ParamError();
            }
            UserInfoBLL _userbll = new UserInfoBLL();
            string myToken = _userbll.GetUserToken(userIdx, 0);
            if (string.IsNullOrEmpty(myToken) || !token.Equals(myToken))
            {
                return LoginFail();
            }
            if (fileList == null || fileList.AllKeys.Length <= 0 || fileList.AllKeys.Length > 5)
            {
                mr.code = "102";
                mr.msg = "请选择5张以内的照片";
                return Content(JsonConvert.SerializeObject(mr));
            }

            //远程连接图片服务器连接是否成功
            //if (!ToolsHelper.Connect("10.191.208.117", "9158music", "9158music"))
            //{
            //    return PicConnectFailed();
            //}

            //进入到保存图片前判断是否有权限可以上传
            //int groupid = 1;
            //int result = user.UploadAlbum_Power(userIdx, albumtype, ref groupid);
            //if (result == -2)
            //{
            //    mr.code = "111";
            //    mr.msg = "你还未实名，快去实名吧";
            //}
            //else if (result == -3 && !Tools.IsCompanyIP)
            //{
            //    mr.code = "112";
            //    mr.msg = "主播等级未达到，快去开播升级吧";
            //}
            //else if (result == -4)
            //{
            //    mr.code = "113";
            //    mr.msg = "上传已受限，请于明日再来上传";
            //}
            //else if (result == -5)
            //{
            //    mr.code = "114";
            //    mr.msg = "上传私照前必须上传3张随拍哦~";
            //}

            //if (result != 1)
            //{
            //    LogHelper.WriteLog(LogFile.Log, "【相册上传】{0},{1},{2}", userIdx, mr.code, mr.msg);
            //    return Content(JsonConvert.SerializeObject(mr));
            //}

            #endregion

            List<Album> albumList = new List<Album>();      //接受上传图片的URL
            List<string> failPhotoList = new List<string>();//接受上传失败的图片名称列表

            //一次性处理上传图片
            int savePhotoSuccessNum = PicSaveBLL.SaveAlbumPhoto(userIdx, albumtype, fileList, ref failPhotoList, ref albumList);
            int dataResult = 0;
            int fileNum = fileList.AllKeys.Length;

            if ((fileNum - savePhotoSuccessNum) == fileNum)
            {
                mr.code = "115";
                mr.msg = "上传失败";
                return Content(JsonConvert.SerializeObject(mr));
            }

            //循环插入数据到数据库
            foreach (var item in albumList)
            {
                Album album = new Album();
                album.albumType = albumtype;
                album.imgURL = item.imgURL;
                album.phyPath = item.phyPath;
                album.groupid = 0;

                dataResult += user.Insert_MyAlbum(userIdx, album);
            }

            mr.code = "100";
            mr.msg = "success";
            mr.data = new { successNum = savePhotoSuccessNum, failFile = failPhotoList};

            LogHelper.WriteLog(LogFile.Debug, "【相册上传】{0}|db：{1}|successNum：{2}|fileNum：", userIdx, dataResult, savePhotoSuccessNum, fileList.AllKeys.Length);

            return Content(JsonConvert.SerializeObject(mr));
        }

        /// 获取相册
        /// live.9158.com/userInfo/GetAlbumList?openType=1&albumtype=2&token=ulXk7ncogXx7nHxd51US7f1asXbuh&useridx=63583358&touseridx=60068188
        /// </summary>
        /// <param name="useridx">当前登录用户idx</param>
        /// <param name="touseridx">查看谁的相册</param>
        /// <param name="albumtype">1:随拍，2：私照</param>
        /// <param name="openType">0：卡片，1：详情页的</param>
        /// <param name="page"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        //[EncryptResult]
        public ActionResult GetAlbumList(int useridx = 0, int touseridx = 0, int albumtype = 1, int openType = 0, int page = 1, string token = "")
        {
            MobileResult mr = new MobileResult();
            int _pageSize = 10, _totalCount = 0, _totalIncome = 0, _priPhotosNum = 0;

            if (useridx == 0)
            {
                mr.code = "101";
                mr.msg = "ParamError";
                return Content(JsonConvert.SerializeObject(mr));
            }
            string myToken = MemcachedHelper.MemGet(CacheKeys.LIVE_USER_TOKEN + useridx);
            if (string.IsNullOrEmpty(myToken) || !token.Equals(myToken))
            {
                mr.code = "103";
                mr.msg = "获取用户信息失败，请退出重新登录";
                return Content(JsonConvert.SerializeObject(mr));
            }

            var _albumList = new List<Album>();
            if (openType == 0)
            {
                _albumList = user.Get_CardAlbumList(useridx, touseridx, ref _priPhotosNum);
            }
            else
            {
                _albumList = user.Get_AlbumList(useridx, touseridx, albumtype, page, _pageSize, ref _totalCount, ref _totalIncome);
            }

            var _totalPage = Tools.GetPageCount(_totalCount, _pageSize);

            var dataObj = new
            {
                albumList = _albumList,
                totalPage = _totalPage,
                totalCount = _totalCount,
                totalIncome = _totalIncome,
                privatePhotos = _priPhotosNum,
                watchPriImgLevel = LiveBLL.Get_LiveConfigById(106).data
            };

            string EncryptData = AESHelper.Encrypt(JsonConvert.SerializeObject(dataObj), AESHelper.AES_Key, AESHelper.iv);

            if ((_albumList != null && _albumList.Count > 0))
            {
                mr.code = "100";
                mr.msg = "success";
            }
            mr.data = EncryptData;
            //if (useridx == 60068188) mr.data = dataObj;

            return Content(JsonConvert.SerializeObject(mr), "text/plain", System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// 相册删除，举报，分享
        /// 关于useridx：删除时是当前登录用户idx，举报时是当前登录用户idx
        /// </summary>
        /// <returns></returns>
        public ActionResult AlbumOperate()
        {
            AlbumParam param = CryptoHelper.GetAESBinaryModelParam<AlbumParam>(CryptoHelper.Live_KEY);
            MobileResult mr = new MobileResult();

            if (param == null || param.useridx <= 0) return ParamError();
            //string myToken = (string)MemcachedHelper.Get(CacheKeys.LIVE_USER_TOKEN + param.useridx);
            //if (string.IsNullOrEmpty(myToken) || !param.token.Equals(myToken))
            //{
            //    return LoginFail();
            //}
            int result = 0;

            result = user.Operate_MyAlbum(param.opertype, param.useridx, param.albumid);

            if (result > 0)
            {
                mr.code = "100";
                mr.msg = "success";
            }
            return Content(JsonConvert.SerializeObject(mr));
        }

        #endregion

        #region 老挝账户信息

        /// <summary>
        /// 个人资料查询   获取登录token
        /// </summary>
        /// <returns></returns>
        public ActionResult UserRealNameInfoSet()
        {
            MobileResult mr = new MobileResult();
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);

            if (dic == null || dic["userIdx"] == "")
            {

                return new BaseController().ParamError("CheckMobile12312");
            }
            AccountBLL account = new AccountBLL();
            int useridx = int.Parse(dic["userIdx"].ToString());
            string token = System.Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            List<UserInvitationInfo> list = user.UserRealNameInfoSet(useridx);
            user.UserLoginTokenInset(useridx, token);
            mr.code = "100";
            mr.msg = "success";
            mr.data = new
            {
                list = list[0],
                loginToken = token
            };
            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 个人质料修改
        /// </summary>
        /// <returns></returns>
        public ActionResult UserRealNameInfoUp()
        {
            MobileResult mr = new MobileResult();
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null || dic["userIdx"] == "")
            {
                return new BaseController().ParamError("CheckMobile");
            }
            //int useridx, string phoneNumber, string userName, int gender, string dateBirth, string reserveInfo, string address, string remarksr
            int useridx = int.Parse(dic["userIdx"].ToString());
            string phoneNumber = dic["phoneNumber"].ToString();
            string userName = dic["userName"].ToString();
            string myName = dic["myName"].ToString();
            int gender = int.Parse(dic["gender"].ToString());
            string dateBirth = dic["dateBirth"].ToString();
            string reserveInfo = dic["reserveInfo"].ToString();
            string address = dic["address"].ToString();
            string remarks = dic["remarks"].ToString();
            int iRet = user.UserRealNameInfoUp(useridx, phoneNumber, userName, myName, gender, dateBirth, reserveInfo, address, remarks);
            if (iRet == 1)
            {
                mr.code = "100";
                mr.msg = "success";
            }
            else
            {
                mr.code = "101";
                if (iRet == -1)
                {
                    mr.msg = "手机号已被占用";
                }
                if (iRet == -2)
                {
                    mr.msg = "姓名已存在";
                }
                mr.data = iRet;
            }

            return Content(JsonConvert.SerializeObject(mr));
        }

        public ActionResult userExchangeCion(string useridx)
        {

            // string useridx = "60424678";
            //if (!HttpHelper.GetClientType())
            //{

            //    return new BaseController().ParamError("请在APP中打开");
            //}
            //if (useridx == "")
            //{
            //    return new BaseController().ParamError("CheckMobile");
            //}
            //if (rawurl != "" && (rawurl.IndexOf("ok.sina.com.cn") > -1 || rawurl.IndexOf("www.9see.com") > -1))
            PayBLL paybll = new PayBLL();
            //取得用户喵币余额
            long myCion = paybll.getUsetCashByUseridx(int.Parse(useridx));
            //取得用户金币余额
            long gameCion = paybll.getUserGameCashByuseridx(int.Parse(useridx));
            ViewBag.useridx = useridx;
            ViewBag.gameCion = gameCion;
            ViewBag.userCion = myCion;
            return View();
        }
        public ActionResult userExchangeCionExec(int useridx, int cion)
        {

            //string rawurl = (Request.ServerVariables["HTTP_REFERER"] == null ? "" : Request.ServerVariables["HTTP_REFERER"].Trim());
            //if (rawurl == "" || rawurl.IndexOf("live.quboshow.com") < 0)
            //{
            //    return new BaseController().ParamError("CheckMobile");
            //}
            if (useridx <= 0 && cion <= 0)
            {
                return new BaseController().ParamError("CheckMobile");
            }
            MobileResult mr = new MobileResult();
            int gameCion = 0 - cion;
            int userCion = 0;
            int iRet = user.userExchangeCion(useridx, ref gameCion, ref userCion);
            if (iRet == 1)
            {
                mr.code = "100";
                mr.msg = "兑换成功";
                mr.data = new
                {
                    gameCion = gameCion,
                    userCion = userCion,
                    iRet = iRet,
                };
            }
            else
            {
                //string [] msgArry={"扣币失败","加币失败","兑换成功"};
                mr.code = "101";
                mr.msg = iRet == -2 ? "加币失败" : (iRet == -3 ? "扣币失败" : "参数异常");
                mr.data = iRet;
            }
            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 银行列表查询
        /// </summary>
        /// <returns></returns>
        public ActionResult BankCardImgConfigureSet()
        {
            MobileResult mr = new MobileResult();
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null)
            {
                return new BaseController().ParamError("CheckMobile");
            }
            int id = int.Parse(dic["id"].ToString());
            List<BankCardImgConfigure> list = user.BankCardImgConfigureSet(id);
            mr.code = "100";
            mr.msg = "success";
            mr.data = list;
            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        ///用户银行卡列表查询
        /// </summary>
        /// <returns></returns>
        public ActionResult LaoWo_UserBankInfoSet()
        {
            MobileResult mr = new MobileResult();
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null || dic["userIdx"] == "")
            {
                return new BaseController().ParamError("CheckMobile");
            }
            int userIdx = int.Parse(dic["userIdx"].ToString());
            //int useridx = 60424678;
            List<LaoWo_UserBankInfo> list = user.LaoWo_UserBankInfoSet(userIdx);


            mr.code = "100";
            mr.msg = "success";
            mr.data = list;
            return Content(JsonConvert.SerializeObject(mr));
        }


        /// <summary>
        ///用户银行卡列表查询
        /// </summary>
        /// <returns></returns>
        public ActionResult LaoWo_UserBankInfoGetSet(int userIdx)
        {
            //string rawurl = (Request.ServerVariables["HTTP_REFERER"] == null ? "" : Request.ServerVariables["HTTP_REFERER"].Trim());
            //if (rawurl == "" || rawurl.IndexOf("live.quboshow.com") < 0)
            //{
            //    return new BaseController().ParamError("CheckMobile");
            //}
            //if (userIdx <= 0)
            //{
            //    return new BaseController().ParamError("CheckMobile");
            //}
            MobileResult mr = new MobileResult();
            //int useridx = 60424678;
            List<LaoWo_UserBankInfo> list = user.LaoWo_UserBankInfoSet(userIdx);

            mr.code = "100";
            mr.msg = "success";
            mr.data = list;
            return Content(JsonConvert.SerializeObject(mr));
        }
        /// <summary>
        /// 用户银行卡添加
        /// </summary>
        /// <returns></returns>
        public ActionResult LaoWo_UserBankInfoInsert()
        {
            MobileResult mr = new MobileResult();
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);

            if (dic == null || dic["userIdx"] == "")
            {
                return new BaseController().ParamError("CheckMobile");
            }
            LogHelper.WriteLog(LogFile.Debug, "持卡人名称：" + dic["bankUserName"]);
            LaoWo_UserBankInfo ubi = new LaoWo_UserBankInfo(dic);
            int iRet = user.LaoWo_UserBankInfoInsert(ubi);
            if (iRet == 1)
            {
                mr.code = "100";
                mr.msg = "success";
            }
            else
            {
                if (iRet == -1)
                {
                    mr.code = "101";
                    mr.msg = "该银行卡已存在";

                }
                else
                {
                    mr.code = "102";
                    mr.msg = "参数异常";
                }

            }
            mr.data = iRet;
            return Content(JsonConvert.SerializeObject(mr));
        }



        /// <summary>
        /// 用户银行卡删除
        /// </summary>
        /// <returns></returns>
        public ActionResult LaoWo_UserBankInfoDel()
        {
            MobileResult mr = new MobileResult();
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null || dic["userIdx"] == "")
            {
                return new BaseController().ParamError("CheckMobile");
            }
            int userIdx = int.Parse(dic["userIdx"].ToString());
            string bankId = dic["bankId"].ToString();
            int iRet = user.LaoWo_UserBankInfoDel(userIdx, bankId);
            if (iRet == 1)
            {
                mr.code = "100";
                mr.msg = "success";
            }
            else
            {
                mr.code = "101";
                mr.msg = "参数异常";
                mr.data = iRet;
            }
            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        ///我的收益
        /// </summary>
        /// <returns></returns>
        public ActionResult LW_IncomeLogSetSet()
        {
            MobileResult mr = new MobileResult();
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null || dic["userIdx"] == "")
            {
                return new BaseController().ParamError("CheckMobile");
            }
            int userIdx = int.Parse(dic["userIdx"].ToString());
            int page = int.Parse(dic["pageNum"].ToString());
            int pageSize = int.Parse(dic["pageSize"].ToString());
            //int useridx = 60424678;
            int count = 0;
            List<LW_IncomeLog> list = user.LW_IncomeLogSet(userIdx, page, pageSize, ref count);

            mr.code = "100";
            mr.msg = "success";
            mr.data = new
            {
                list = list,
                count = count
            };
            return Content(JsonConvert.SerializeObject(mr, JsonHelper.dtConverter));
        }

        /// <summary>
        ///用户交易记录查询
        /// </summary>
        /// <returns></returns>
        public ActionResult LW_Accounts_MoneyTradeDetailSet()
        {
            MobileResult mr = new MobileResult();
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null || dic["userIdx"] == "")
            {
                return new BaseController().ParamError("CheckMobile");
            }
            int userIdx = int.Parse(dic["userIdx"].ToString());
            int page = int.Parse(dic["pageNum"].ToString());
            int pageSize = int.Parse(dic["pageSize"].ToString());
            //int useridx = 60424678;
            int count = 0;
            //int useridx = 60424678;
            List<LW_Accounts_MoneyTradeDetail> list = user.LW_Accounts_MoneyTradeDetailSet(userIdx, page, pageSize, ref count);

            mr.code = "100";
            mr.msg = "success";
            mr.data = new
            {
                list = list,
                count = count
            };
            return Content(JsonConvert.SerializeObject(mr, JsonHelper.dtConverter));
        }

        public ActionResult CommissionPutForwardSet(int userIdx)
        {
            //if (!HttpHelper.GetClientType())
            //{
            //    return new BaseController().ParamError("请在APP中打开");
            //}
            //if (userIdx <= 0)
            //{
            //    return new BaseController().ParamError("CheckMobile");
            //}
            List<myUserAgentInfo> list = user.myUserAgentInfoSet(userIdx);
            myUserAgentInfo my = new myUserAgentInfo();
            if (list != null && list.Count > 0)
            {
                my = list[0];
            }

            Decimal sale = 0;
            Decimal money = 0;
            PayBLL pay = new PayBLL();
            //取得用户金币余额及可提现余额
            pay.LW_GoldLimitSet(userIdx, ref sale, ref money);
            ViewBag.myUserAgentInfo = JsonConvert.SerializeObject(my);
            ViewBag.money = money;
            ViewBag.sale = sale;
            ViewBag.userIdx = userIdx;
            string Settlement = "";
            if (my.Settlement == new DateTime())
            {
                DateTime dt = System.DateTime.Now;
                Settlement = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d")) - 7).ToString("yyyy.MM.dd") + "-" + dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d")) - 1).ToString("yyyy.MM.dd");
            }
            else
            {
                Settlement = my.Settlement.AddDays(-7).ToString("yyyy.MM.dd") + "-" + my.Settlement.ToString("yyyy.MM.dd");
            }
            ViewBag.Settlement = Settlement;
            return View();
        }
        #endregion;

        #region 面具账户信息
        /// <summary>
        /// 个人资料查询   获取登录token     state状态 1表示没有完善资料跳转完善资料  2社交隐藏 3社交不隐藏
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserRealNameInfo()
        {
            MobileResult mr = new MobileResult();
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null)
            {
                return new BaseController().ParamError("CheckMobile12312");
            }
            int useridx = int.Parse(dic["userIdx"].ToString());
            string token = System.Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            List<UserInfomation> list = user.UserRealNameInfo(useridx);
            //user.UserLoginTokenInset(useridx, token);
            mr.code = "100";
            mr.msg = "success";
            mr.data = new
            {
                list = list[0],
                //loginToken = token
            };
            return Content(JsonConvert.SerializeObject(mr));
        }
        /// <summary>
        /// 我的信息查询
        /// </summary>
        /// <returns></returns>
        public ActionResult UserMyNameInfo()
        {
            MobileResult mr = new MobileResult();
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null)
            {
                return new BaseController().ParamError("CheckMobile12312");
            }
            int useridx = int.Parse(dic["userIdx"].ToString());
            List<UserMynameInfo> list = user.UserMyNameInfo(useridx);
            int count = 0;
            List<AlbumNew> albums = user.Get_AlbumList(useridx, 1, 8, ref count);
            mr.code = "100";
            mr.msg = "success";
            mr.data = new
            {
                list = list[0],
                albums= albums,
                albumsCount= count
            };
            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 个人质料修改
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserRealNameInfoUpdate()
        {
            MobileResult mr = new MobileResult();
            var Model = CryptoHelper.GetAESBinaryModelParam<UserInfomation>(CryptoHelper.Live_KEY);
            if (Model == null)
            {
                return new BaseController().ParamError("CheckMobile");
            }
            //var Model = new UserInfomation();
            //Model.userIdx = int.Parse(dic["userIdx"].ToString());
            //Model.Sex = int.Parse(dic["Sex"].ToString());
            ////Model.Myname = dic["Myname"].ToString();
            //Model.Age = int.Parse(dic["Age"].ToString());
            //Model.Province = dic["Province"].ToString();
            //Model.City = dic["City"].ToString();
            //Model.Job = dic["Job"].ToString();
            //Model.AppointmentRage = dic["AppointmentRage"].ToString();
            //Model.AppointmentProgram = dic["AppointmentProgram"].ToString();
            //Model.AppointmentExpect = dic["AppointmentExpect"].ToString();
            //Model.MonthlyIncome = dic["MonthlyIncome"].ToString();
            //Model.Shape = dic["Shape"].ToString();
            //Model.WeChat = dic["WeChat"].ToString();
            //Model.QQ = dic["QQ"].ToString();
            //Model.Height = int.Parse(dic["Height"].ToString());
            //Model.Weight = int.Parse(dic["Weight "].ToString());
            //Model.Bust = dic["Bust"].ToString();
            //Model.Introduction = dic["Introduction"].ToString();
            //Model.State = int.Parse(dic["State"].ToString());



            //Model.userIdx = 10000191;
            //Model.Sex = 1;
            //Model.Myname = "12312";
            //Model.Age = 24;
            //Model.Province = "浙江省";
            //Model.City = "杭州市";
            //Model.Job = "信息技术|it";
            //Model.AppointmentRage = "杭州|南京|上海";
            //Model.AppointmentProgram = "看电影|唱歌";
            //Model.AppointmentExpect = "美女|御姐|萝莉";
            //Model.MonthlyIncome = "5000-8000";
            //Model.Shape = "苗条";
            //Model.WeChat = "33321";
            //Model.QQ = "213";
            //Model.Height = 170;
            //Model.Weight = 55;
            //Model.Bust = "38|C";
            //Model.Introduction = "欢迎使用";
            int iRet = user.UserRealNameInfoUpdate(Model);
            if (iRet == 1)
            {
                asyncCall call = new asyncCall();
                call.useridx = Model.userIdx;
                call.type = 3;
                Thread thread = new Thread(call.ToCall);
                thread.Start();
                mr.code = "100";
                mr.msg = "success";
            }
            else
            {
                mr.code = "101";
                if (iRet == -1)
                {
                    mr.msg = "Useridx不存在 ";
                }
                if (iRet == -2)
                {
                    mr.msg = "修改失败";
                }
                mr.data = iRet;
            }

            return Content(JsonConvert.SerializeObject(mr));
        }
        #endregion
        /// <summary>
        /// 个人信息获取约会期望职业约会节目数据
        /// </summary>
        /// <param name="userIdx"></param>
        /// <param name="type"> 0-男 1女 默认男</param>
        /// <returns></returns>
        public ActionResult GetInfoList(int userIdx = 0,int type=0)
        {
            if (userIdx <= 0)
            {
                return new BaseController().ParamError("CheckMobile12312");
            }
            MobileResult mr = new MobileResult();
            List<UserDataInfo> list = user.UserDataInfo(type);
            mr.code = "100";
            mr.msg = "success";
            mr.data = list;
            return Content(JsonConvert.SerializeObject(mr));
        }
        /// <summary>
        /// 查看他人主页
        /// </summary>
        /// <param name="userIdx"></param>
        /// <returns></returns>
       // [HttpPost]
        public ActionResult GetAccountInfo()
        {
            MobileResult mr = new MobileResult();
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null || !dic.ContainsKey("userIdx") || !dic.ContainsKey("TouserIdx"))
            {
                return new BaseController().ParamError("CheckMobile");
            }
            string userIdx = dic["userIdx"].ToString();
            string Tuseridx = dic["TouserIdx"].ToString();

            //string userIdx = "10000205";
            //string Tuseridx = "10000191";
            //string Token = dic["Token"].ToString();
            //string myToken = _userbll.GetUserToken(int.Parse(userIdx), 0);
            //if (string.IsNullOrEmpty(myToken) || !Token.Equals(myToken))
            //{
            //    return LoginFail();
            //}
            #region 逻辑
            List<UserInfomation> lisex = user.UserRealNameInfo(int.Parse(userIdx));
            //iret返回1 表示查看过了，返回信息  返回3 被拉黑
            int iret = _userbll.UservisitorsInfo(0, int.Parse(userIdx), int.Parse(Tuseridx));
            int ret = 0;//剩余次数 10000表示无限次
            if (iret == 0)
            {
                 ret = _userbll.UserCountInfo(0, int.Parse(userIdx), int.Parse(Tuseridx),lisex[0].Sex);//ret=-1 没有次数 否则返回剩余次数  or返回剩余次数  -2 vip不限次数
                if (ret == -1)
                {
                    mr.code = "101";
                    mr.msg = "今日次数已经用完，成为VIP查看";
                    mr.data = -1;
                    return Content(JsonConvert.SerializeObject(mr));
                }
            }
            if(iret == 3)
            {
                mr.code = "102";
                mr.msg = "用户被拉黑";
                mr.data = -1;
                return Content(JsonConvert.SerializeObject(mr));
            }
            //查看用户是否已经查看过照片和聊天信息  收费信息
            int isphoto = 0;
            int isComu = 0;
            int iRet = user.I_userToFoudInfo(int.Parse(userIdx), int.Parse(Tuseridx), ref isphoto, ref isComu);
            #endregion
            List<UserInfomation> list = user.UserRealNameInfo(int.Parse(Tuseridx));
            int count = 0;
            List<AlbumNew> albums = user.Get_AlbumList(int.Parse(Tuseridx),1,8,ref count,1, int.Parse(userIdx));
            var model = list[0];
            if (model.Sex == 1&& isComu == 0)
            {
                if (model.WeChat != "")
                {
                    model.WeChat = "已填写，点击查看";
                }
                if (model.QQ != "")
                {
                    model.QQ = "已填写，点击查看";
                }
            }
            else if (model.Sex == 0 && model.realState == 1)
            {
                if (model.WeChat != "")
                {
                    model.WeChat = "请通过私聊向我索取";
                }
                if (model.QQ != "")
                {
                    model.QQ = "请通过私聊向我索取";
                }  
            }
            //var albumst = albums.Select(n => new { n.albumid,n.albumType,n.imgURL,n.groupid}).ToList();
            if (isphoto == 0 && model.PhotoMoney > 0)
            {
                albums.ForEach(n => n.imgURL = "");
            }

            string distance = "未知";
            int isVIP = 0;   //vip
            int IsRealState = 0;  //认证
            string Time = "";
            distance = user.I_userInfoExpand(int.Parse(userIdx), int.Parse(Tuseridx), ref isVIP, ref IsRealState, ref Time);
            if (Time != "当前在线")
            {
                int ti = int.Parse(Time);
                if (ti < 60)
                {
                    Time = Time + "分前";
                }
                else if (ti < 1440)
                {
                    Time = Math.Round((double.Parse(ti.ToString()) / 60)).ToString() + "时前";
                }
                else if (ti < 43200)
                {
                    Time = Math.Round((double.Parse(ti.ToString()) / 1440)).ToString() + "天前";
                }
                else
                {
                    Time = "30天前";
                }
            }
            if(distance!= "未知")
            {
                int ds= int.Parse(distance);
                if (ds < 1000)
                {
                    distance = distance + "m";
                }
                else
                {
                    distance = Math.Round((double.Parse(distance.ToString()) / 1000)).ToString() + "km";
                }
            }
            List<Userevaluate> userevaluates = user.userevaluates(int.Parse(Tuseridx));
            int Fans = user.isFans(int.Parse(userIdx), int.Parse(Tuseridx));
            int Black = user.isBlack(int.Parse(userIdx), int.Parse(Tuseridx));
            mr.code = "100";
            mr.msg = "success";
            mr.data = new
            {
                userinfo = model,
                userinfoExpand = new { distance = distance, isVIP = isVIP, IsRealState = IsRealState, Time = Time },
                photo = albums,
                evaluates = userevaluates,
                fans= Fans,
                Black= Black
            };
            return Content(JsonConvert.SerializeObject(mr));
        }
        /// <summary>
        /// 添加评价
        /// </summary>
        /// <returns></returns>
        public ActionResult Getuserevaluates()
        {
            MobileResult mr = new MobileResult();
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null || !dic.ContainsKey("userIdx") || !dic.ContainsKey("TouserIdx") || dic["Evaluates"] == "")
            {
                return new BaseController().ParamError("CheckMobile");
            }
            string userIdx = dic["userIdx"].ToString();
            string Tuseridx = dic["TouserIdx"].ToString();
            string Evaluates = dic["Evaluates"].ToString();
            //string userIdx = "10000190";
            //string Tuseridx = "10000015";
            //var Evaluates = "喜欢|快乐|tt";
            var evalist = Evaluates.Split('|');
            int Ret = 0;
            foreach (var item in evalist)
            {
                Ret += user.Getuserevaluates(int.Parse(userIdx), int.Parse(Tuseridx), item);
            }
            if (Ret == evalist.Length)
            {
                asyncCall call = new asyncCall();
                call.useridx = int.Parse(Tuseridx);
                call.type = 1;
                Thread thread = new Thread(call.ToCall);
                thread.Start();
                // user.CallServer(int.Parse(Tuseridx),1);
                mr.code = "100";
                mr.msg = "success";
                mr.data = Ret;

            }
            else
            {
                mr.code = "101";
                mr.msg = "添加失败";
                mr.data = Ret;
            }
            return Content(JsonConvert.SerializeObject(mr));
        }
        public ActionResult tt()
        {
            MobileResult mr = new MobileResult();
            List<UserInfomation> lisex = user.UserRealNameInfo(10000190);
            var t = lisex.Select(s => new UserInfomation { Sex = 5,QQ=s.QQ,City=s.City }).ToList();
            return Content(JsonConvert.SerializeObject(mr));
        }
        /// <summary>
        /// 查看收费照片
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTuserAlbum()
        {
            MobileResult mr = new MobileResult();
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null || !dic.ContainsKey("userIdx") || !dic.ContainsKey("TouserIdx"))
            {
                return new BaseController().ParamError("CheckMobile");
            }
            string userIdx = dic["userIdx"].ToString();
            string Tuseridx = dic["TouserIdx"].ToString();
            //string userIdx = "10000190";
            //string Tuseridx = "10000015";
            List<UserInfomation> lisex = user.UserRealNameInfo(int.Parse(userIdx));
            int ret = _userbll.UserCountInfo(1, int.Parse(userIdx), int.Parse(Tuseridx),lisex[0].Sex);//ret=-1 没有次数 否则返回剩余次数  or返回剩余次数
            if (ret == -1)
            {
                mr.code = "101";
                mr.msg = "没有免费次数";
                mr.data = -1;
                return Content(JsonConvert.SerializeObject(mr));
            }
            int count = 0;
            List<AlbumNew> albums = user.Get_AlbumList(int.Parse(Tuseridx),1,8,ref count,1,int.Parse(userIdx));
            mr.code = "100";
            mr.msg = "success";
            mr.data = new
            {
                albums= albums,
                count= ret
            };
            return Content(JsonConvert.SerializeObject(mr));
        }
        /// <summary>
        /// 查看阅后即焚照片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetLookClearPhoto()
        {
            MobileResult mr = new MobileResult();
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null || !dic.ContainsKey("userIdx") || !dic.ContainsKey("TouserIdx"))
            {
                return new BaseController().ParamError("CheckMobile");
            }
            string userIdx = dic["userIdx"].ToString();
            string Tuseridx = dic["TouserIdx"].ToString();
            string Albumid = dic["Albumid"].ToString();
            //string userIdx = "10000190";
            //string Tuseridx = "10000015";
            //string Albumid = "5";
            int ret = _userbll.GetLookClearPhoto(int.Parse(Albumid), int.Parse(userIdx), int.Parse(Tuseridx));
            if (ret == 1)
            {
                mr.code = "101";
                mr.msg = "已经查看过了";
                mr.data = -1;
                return Content(JsonConvert.SerializeObject(mr));
            }
            mr.code = "100";
            mr.msg = "success";
            mr.data = "已经记录";
            return Content(JsonConvert.SerializeObject(mr));
        }
        /// <summary>
        /// 收费获取照片 聊天信息 Type 0 照片收费 1 聊天信息收费
        /// </summary>
        /// <returns></returns>
        public ActionResult GetChargeInfo()
        {
            MobileResult mr = new MobileResult();
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null || !dic.ContainsKey("userIdx") || !dic.ContainsKey("TouserIdx"))
            {
                return new BaseController().ParamError("CheckMobile");
            }
            string userIdx = dic["userIdx"].ToString();
            string Tuseridx = dic["TouserIdx"].ToString();
            string Type = dic["Type"].ToString();
            //string userIdx = "10000191";
            //string Tuseridx = "10000197";
            //string Type = "0";

            int ret = _userbll.GetChargeInfo(int.Parse(Type), int.Parse(userIdx), int.Parse(Tuseridx));
            if (ret == 1)
            {
                mr.code = "101";
                mr.msg = "余额不足";
                mr.data = 1;
                return Content(JsonConvert.SerializeObject(mr));
            }
            else if (ret == 2)
            {
                if (Type == "0")
                {
                    int count = 0;
                    List<AlbumNew> albums = user.Get_AlbumList(int.Parse(Tuseridx), 1, 80, ref count,1,int.Parse(userIdx));
                    asyncCall call = new asyncCall();
                    call.useridx = int.Parse(Tuseridx);
                    call.type = 2;
                    Thread thread = new Thread(call.ToCall);
                    thread.Start();
                    mr.code = "100";
                    mr.msg = "success";
                    mr.data = albums;
                }
                else
                {
                    List<UserInfomation> list = user.UserRealNameInfo(int.Parse(Tuseridx));
                    var userinfo = list.Select(n => new { n.QQ, n.WeChat });
                    mr.code = "100";
                    mr.msg = "success";
                    mr.data = userinfo;
                }
            }
            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 查看用户聊天信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTuserChat()
        {
            MobileResult mr = new MobileResult();
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null || !dic.ContainsKey("userIdx") || !dic.ContainsKey("TouserIdx"))
            {
                return new BaseController().ParamError("CheckMobile");
            }
            string userIdx = dic["userIdx"].ToString();
            string Tuseridx = dic["TouserIdx"].ToString();
            //string userIdx = "10000190";
            //string Tuseridx = "10000015";
            List<UserInfomation> lisex = user.UserRealNameInfo(int.Parse(userIdx));
            int ret = _userbll.UserCountInfo(2, int.Parse(userIdx), int.Parse(Tuseridx),lisex[0].Sex);//ret=-1 没有次数 否则返回剩余次数  or返回剩余次数
            if (ret == -1)
            {
                mr.code = "101";
                mr.msg = "没有免费次数";
                mr.data = -1;
                return Content(JsonConvert.SerializeObject(mr));
            }
            List<UserInfomation> list = user.UserRealNameInfo(int.Parse(Tuseridx));
            var userchat = list.Select(n => new { n.QQ, n.WeChat });
            mr.code = "100";
            mr.msg = "success";
            mr.data = new
            {
                userchat = userchat,
                count = ret
            };
            return Content(JsonConvert.SerializeObject(mr));
        }
        /// <summary>
        /// 获取相册
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUserAlbumList()

        {
            MobileResult mr = new MobileResult();
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null || !dic.ContainsKey("userIdx") || !dic.ContainsKey("TouserIdx"))
            {
                return new BaseController().ParamError("CheckMobile");
            }
            string Type = dic["Type"].ToString();  //type=0 查看自己的相册  1--查看他人相册
            string userIdx = dic["userIdx"].ToString();
            string Tuseridx = dic["TouserIdx"].ToString();
            string Page = dic["Page"].ToString();
            string PageCount = dic["PageCount"].ToString();
            //string Type = "1";  //type=0 查看自己的相册  1--查看他人相册
            //string userIdx = "10000191";
            //string Tuseridx = "10000197";
            //string Page = "1";
            //string PageCount = "50";
            List<AlbumNew> albums = new List<AlbumNew>();
            int count = 0;
            if (int.Parse(Type) == 1)
            {
                int isphoto = 0;
                int isComu = 0;
                int iRet = user.I_userToFoudInfo(int.Parse(userIdx), int.Parse(Tuseridx), ref isphoto, ref isComu);
                if (isphoto == 0)
                {
                    decimal rt = user.I_userToPhotoMoney(int.Parse(Tuseridx));
                    if (rt > 0)
                    {
                        mr.code = "101";
                        mr.msg = "vip用户使用免费次数非VIP用户需要收费才能使用";
                        mr.data = rt;
                        return Content(JsonConvert.SerializeObject(mr));
                    }
                }
                albums = user.Get_AlbumList(int.Parse(Tuseridx), int.Parse(Page), int.Parse(PageCount),ref  count ,1,int.Parse(userIdx));
            }
            else
            {
                albums = user.Get_AlbumList(int.Parse(userIdx), int.Parse(Page), int.Parse(PageCount),ref count);
            }
            mr.code = "100";
            mr.msg = "success";
            mr.data = new { albums = albums , count = count };
            return Content(JsonConvert.SerializeObject(mr));
        }
        /// <summary>
        /// 查询附近的人
        /// </summary>
        /// <param name="Longitude">经度</param>
        /// <param name="Latitude">纬度</param>
        /// <param name="userIdx"></param>
        /// <param name="Type">类型 0普通 1-高级帅选</param>
        /// <param name="Area">地区</param>
        /// <param name="VIP">0 不是VIP 1是 vip 2所有</param>
        /// <param name="Real">0 不是Real 1是 Real 2所有</param>
        /// <param name="Sex">0 男 1女 2所有</param>
        /// <param name="MonthlyIncome"> 收入范围</param>
        /// <param name="Bust">胸围</param>
        /// <param name="LastTime">0不限  60-1小时 1440-1天内  4320-3天内</param>
        /// <param name="AgeRange"> 1-100</param>
        /// <param name="page">1</param>
        /// <returns></returns>
        public ActionResult GetNearList(float Longitude=0,float Latitude=0 , int userIdx = 0,int Type=0,string Area="",int VIP=2,int Real=2, int Sex=2,string MonthlyIncome="",string Bust="",int LastTime=0,string AgeRange="1-100",int page=1,string token="")
        {
            MobileResult mr = new MobileResult();
            if (userIdx <= 0)
            {
                return new BaseController().ParamError("CheckMobile");
            }
            string userTokenValue = _userbll.GetUserToken(userIdx, 0);

            if (!string.IsNullOrEmpty(userTokenValue) && !token.Equals(userTokenValue))
            {
                mr.code = "103";
                mr.msg = "获取用户信息失败,请退出重新登录";
                return Content(JsonConvert.SerializeObject(mr));
            }
            if (Type == 1)
            {
                int ret = user.GetUserPower(userIdx);
                if (ret == 0)
                {
                    mr.code = "101";
                    mr.msg = "权限不足请成为vip";
                    mr.data = 1;
                    return Content(JsonConvert.SerializeObject(mr));
                }
            }
            string[] t = AgeRange.Split('-'); 
            List<UserListInfo> list = user.GetNearList(Longitude, Latitude, Area,page,20,Sex,VIP,Real,MonthlyIncome,Bust,int.Parse(t[0]),int.Parse(t[1]),LastTime);
            //PositionModel positionModel = PositionHelper.FindNeighPosition(116.397125, 39.916527, 200);    //距离限制
            mr.code = "100";
            mr.msg = "success";
            mr.data = list;
            return Content(JsonConvert.SerializeObject(mr));
        }
        /// <summary>
        ///   0牵线墙， 1-新人卡
        /// </summary>
        /// <param name="userIdx"></param>
        /// <param name="Type"></param>
        /// <param name="Longitude"></param>
        /// <param name="Latitude"></param>
        /// <returns></returns>
        public ActionResult GetHomeList( int userIdx = 0, int Type = 0, float Longitude = 0, float Latitude = 0,int page=1,string token="")
        {
            MobileResult mr = new MobileResult();
            if (userIdx <= 0)
            {
                return new BaseController().ParamError("CheckMobile");
            }
            string userTokenValue = _userbll.GetUserToken(userIdx, 0);
            if (!string.IsNullOrEmpty(userTokenValue) && !token.Equals(userTokenValue))
            {
                mr.code = "103";
                mr.msg = "获取用户信息失败,请退出重新登录";
                return Content(JsonConvert.SerializeObject(mr));
            }
            PositionModel positionModel = new PositionModel();
            if (Longitude != 0 && Latitude != 0 && Type==1)
            {
                positionModel = PositionHelper.FindNeighPosition(Longitude, Latitude, 100);//距离限制   100KM
            }
            List<UserListInfo> list = user.GetHomeList(Longitude, Latitude, Type,page,20,positionModel.MinLng,positionModel.MaxLng,positionModel.MinLat,positionModel.MaxLat);
            
            mr.code = "100";
            mr.msg = "success";
            mr.data = list;
            return Content(JsonConvert.SerializeObject(mr));
        }
        /// <summary>
        /// 获取评论列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetevaluateList()
        {
            MobileResult mr = new MobileResult();
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null || !dic.ContainsKey("userIdx"))
            {
                return new BaseController().ParamError("CheckMobile");
            }
            string userIdx = dic["userIdx"].ToString();
            string Page = dic["Page"].ToString();
            string PageCount = dic["PageCount"].ToString();

            //string userIdx = "10000015";
            //string Page = "2";
            //string PageCount ="3" ;
            var list =  user.GetuserevaluateList(int.Parse(userIdx), int.Parse(Page), int.Parse(PageCount));
            mr.code = "100";
            mr.msg = "success";
            mr.data = list;
            return Content(JsonConvert.SerializeObject(mr));
        }
        /// <summary>
        /// 获取喜欢列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLikeList()
        {
            MobileResult mr = new MobileResult();
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null || !dic.ContainsKey("userIdx"))
            {
                return new BaseController().ParamError("CheckMobile");
            }
            string userIdx = dic["userIdx"].ToString();
            string Page = dic["Page"].ToString();
            string PageCount = dic["PageCount"].ToString();

            //string userIdx = "10000000";
            //string Page = "1";
            //string PageCount = "3";
            var list = user.GetLikeList(int.Parse(userIdx), int.Parse(Page), int.Parse(PageCount));
            mr.code = "100";
            mr.msg = "success";
            mr.data = list.Select(n=>new { n.userIdx,n.Smallpic,n.Myname,n.IsVip,n.Authentication,n.Weight,n.Height,n.Age,n.Sex,n.Addtime});
            return Content(JsonConvert.SerializeObject(mr));
        }
        /// <summary>
        /// 获取喜欢列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProfitList()
        {
            MobileResult mr = new MobileResult();
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null || !dic.ContainsKey("userIdx"))
            {
                return new BaseController().ParamError("CheckMobile");
            }
            string userIdx = dic["userIdx"].ToString();
            string Page = dic["Page"].ToString();
            string PageCount = dic["PageCount"].ToString();

            //string userIdx = "10000194";
            //string Page = "1";
            //string PageCount = "3";
            var list = user.GetProfitList(int.Parse(userIdx), int.Parse(Page), int.Parse(PageCount));
            mr.code = "100";
            mr.msg = "success";
            mr.data = list.Select(n => new { n.userIdx, n.Smallpic, n.Myname, n.IsVip, n.Authentication, n.Weight, n.Height, n.Age, n.Sex, n. Addtime,n.Number});
            return Content(JsonConvert.SerializeObject(mr));
        }
        /// <summary>
        /// 黑名单列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetBlackList()
        {
            MobileResult mr = new MobileResult();
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null || !dic.ContainsKey("userIdx"))
            {
                return new BaseController().ParamError("CheckMobile");
            }
            string userIdx = dic["userIdx"].ToString();
            string Page = dic["Page"].ToString();
            string PageCount = dic["PageCount"].ToString();

            //string userIdx = "10000190";
            //string Page = "1";
            //string PageCount = "3";
            var list = user.GetBlackList(int.Parse(userIdx), int.Parse(Page), int.Parse(PageCount));
            mr.code = "100";
            mr.msg = "success";
            mr.data = list.Select(n => new { n.userIdx, n.Smallpic, n.Myname, n.IsVip, n.Authentication, n.Weight, n.Height, n.Age, n.Sex, n.Addtime, n.Number });
            return Content(JsonConvert.SerializeObject(mr));
        }
        /// <summary>
        /// 消息列表图片语音上传
        /// </summary>
        /// <param name="userIdx"></param>
        /// <param name="token"></param>
        /// <param name="type">0-图片 1-语音</param>
        /// <returns></returns>
        public ActionResult newsUpload(int userIdx = 0, string token = "", int type = 0)
        {
            MobileResult r = new MobileResult();

            #region 业务判断

            if (string.IsNullOrEmpty(token) || userIdx <= 0)
            {
                r.code = "101";
                r.msg = "ParamError";
                //  mr.data = string.Format("[游戏]参数错误uidx:{0}，coin:{1}，gameid:{2},field:{3}", uidx, coin, gameid, gamefield);
                return Content(JsonConvert.SerializeObject(r));
            }
            UserInfoBLL _userbll = new UserInfoBLL();
            string userTokenValue = _userbll.GetUserToken(userIdx, 0);

            if (!string.IsNullOrEmpty(userTokenValue) && !token.Equals(userTokenValue))
            {
                r.code = "103";
                r.msg = "获取用户信息失败,请退出重新登录";
                return Content(JsonConvert.SerializeObject(r));
            }
            HttpFileCollection files = HttpContext.ApplicationInstance.Request.Files;
            HttpPostedFileBase file = HttpContext.Request.Files[0];

            if (files == null || file == null)
            {
                r.code = "113";
                r.msg = "upload Fail";
                return Content(JsonConvert.SerializeObject(r));
            }
            string url = "";
            string urlbig = "";
            #endregion

            bool t= PicSaveBLL.SaveNews(userIdx, file, type,  ref url,ref urlbig);
            if (t)
            {
                r.code = "100";
                r.msg = "上传成功";
                r.data = new { url1=url,url2=urlbig};

                //CacheBLL.VerifyUpdatePhoto(1, userIdx, ref count);
            }
            //}
            return Content(JsonConvert.SerializeObject(r));
        }

        /// <summary>
        /// 消息列表图片语音上传
        /// </summary>
        /// <param name="userIdx"></param>
        /// <param name="token"></param>
        /// <param name="type">0-图片 1-语音</param>
        /// <returns></returns>
        public ActionResult newsUploadImg(int userIdx = 0, string token = "", int type = 0,int width=640,int Height=640)
        {
            MobileResult r = new MobileResult();

            #region 业务判断

            UserInfoBLL _userbll = new UserInfoBLL();
            string userTokenValue = _userbll.GetUserToken(userIdx, 0);
            if (string.IsNullOrEmpty(token) || userIdx <= 0 )
            {
                r.code = "101";
                r.msg = "ParamError";
                //  mr.data = string.Format("[游戏]参数错误uidx:{0}，coin:{1}，gameid:{2},field:{3}", uidx, coin, gameid, gamefield);
                return Content(JsonConvert.SerializeObject(r));
            }
            if (!string.IsNullOrEmpty(userTokenValue) && !token.Equals(userTokenValue))
            {
                r.code = "103";
                r.msg = "获取用户信息失败,请退出重新登录";
                return Content(JsonConvert.SerializeObject(r));
            }
            HttpFileCollection files = HttpContext.ApplicationInstance.Request.Files;
            HttpPostedFileBase file = HttpContext.Request.Files[0];

            if (files == null || file == null)
            {
                r.code = "113";
                r.msg = "upload Fail";
                return Content(JsonConvert.SerializeObject(r));
            }
            string url = "";
            string urlbig = "";
            #endregion
            int thumwidth = 0;
            int thumheight = 0;
            bool t = PicSaveBLL.SaveNews(userIdx, file, type, ref url, ref urlbig,ref thumwidth, ref thumheight, width,Height);
            if (t)
            {
                r.code = "100";
                r.msg = "上传成功";
                r.data = new
                {
                    thum  = new { url = url, width = thumwidth, height = thumheight },
                    orgin = new { url = urlbig, width = width, height = Height }
                };
            };
            //CacheBLL.VerifyUpdatePhoto(1, userIdx, ref count);
            //}
            return Content(JsonConvert.SerializeObject(r));
        }

        /// <summary>
        ///获取金币和VIP时间
        /// </summary>
        /// <returns></returns>
        public ActionResult CoinVip()
        {
            MobileResult mr = new MobileResult();
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null || !dic.ContainsKey("userIdx"))
            {
                return new BaseController().ParamError("CheckMobile");
            }
            string userIdx = dic["userIdx"].ToString();
            //string userIdx = "10009998";
            string date = "";
            long coin = user.CoinVip(int.Parse(userIdx), ref date);
            mr.code = "100";
            mr.msg = "success";
            mr.data = new { coin=coin,vipdate= date };
            return Content(JsonConvert.SerializeObject(mr));
        }

        public ActionResult Get_AlbumListChangeposition()
        {
            MobileResult mr = new MobileResult();
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null || !dic.ContainsKey("userIdx"))
            {
                return new BaseController().ParamError("CheckMobile");
            }
            string userIdx = dic["userIdx"];
            string Albumid = dic["Albumid"];
            string nowgroupid = dic["Nowgroupid"];
            string togroupid = dic["Togroupid"];

            //string userIdx = "10000191";
            //string Albumid = "87";
            //string nowgroupid = "1";
            //string togroupid = "4";
            int ret = _userbll.Get_AlbumListChangeposition( int.Parse(userIdx), int.Parse(Albumid), int.Parse(nowgroupid), int.Parse(togroupid));
            if (ret == 1)
            {
                mr.code = "100";
                mr.msg = "成功";
                mr.data = -1;
                return Content(JsonConvert.SerializeObject(mr));
            }
            mr.code = "101";
            mr.msg = "失败";
            mr.data = 0;
            return Content(JsonConvert.SerializeObject(mr));
        }

        public ActionResult test()
        {
            MobileResult mr = new MobileResult();
            asyncCall call = new asyncCall();
            call.useridx = 100000;
            call.type = 1;
            Thread thread = new Thread(call.ToCall);
            thread.Start();
            mr.code = "100";
            mr.msg = "success";
            mr.data = 123;
            return Content(JsonConvert.SerializeObject(mr));
        }


    }

}
