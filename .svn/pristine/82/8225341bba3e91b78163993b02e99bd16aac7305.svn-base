using System.Web.Mvc;
using BLL;
using Common;
using Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using Common.Core;
using WebAPI.Controllers.Attribute;

namespace WebAPI.Controllers
{
    [Error]
    public class GameCenterController : Controller
    {
        LiveBLL live = new LiveBLL();
        AccountBLL _account = new AccountBLL();
        GameBLL _gamebll = new GameBLL();
        IncomeBLL income = new IncomeBLL();
        UserInfoBLL _userbll = new UserInfoBLL();

        #region [ 游戏中心页面相关 ] 

        /// <summary>
        /// 游戏中心首页
        /// </summary>
        /// <param name="curuseridx"></param>
        /// <returns></returns>
        public ActionResult GameIndex()
        {
            if (!Tools.IsMobile()) return View("Error");

            return View();
        }
        [HttpPost]
        public ActionResult GameLogin()
        {
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            MobileResult mr = new MobileResult();
            if (dic == null) return new BaseController().ParamError("");

            int userIdx = Convert.ToInt32(dic["userIdx"]);
            string Token = dic["token"];

            if (1 == 1)
            {
                mr.code = "100";
                mr.msg = "success";
            }

            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 获取游戏数据ajax请求时使用
        /// </summary>
        /// <param name="type"></param>
        /// <param name="gameid"></param>
        /// <returns></returns>
        public ActionResult GameData(int useridx = 0)
        {
            string userip = Tools.GetRealIP();

            //bool result = CommonBLL.Instance.BlackIPState(2, userip);
            List<GameConfig> _list = null;// 
            List<GameConfig> _adlist = null;//轮播图查询
            List<GameConfig> _Toplist = null;//最受欢迎的游戏
            List<GameConfig> _Morelist = null;//最受欢迎的游戏

            //暂时用新浪API 到期 2017-04-17
            Location loc = PositionHelper.GetLocationInfo(userip);
            //黑名单地区不显示游戏数据（江苏， 连云港2017-03-16 add）
            if (!AppDataBLL.GameblackAddress(loc.Province))
            {
                _list = live.GetAllGameInfo(0);//所有的游戏
                _adlist = _list.FindAll(m => !string.IsNullOrEmpty(m.activeBigPic) && m.advflag == 1);
                _Toplist = live.GetAllGameInfo(999);
                _Morelist = live.GetAllGameInfo(1000);
            }

            //游戏中心测试账号访问数据
            if ((userip == "115.231.93.68" || userip == "127.0.0.1") && LiveBLL.Get_TestAccount(1, useridx, "") == 1)
            {
                _Toplist = live.GetAllGameInfo(-1);
            }

            MobileResult mr = new MobileResult();

            mr.code = "100";
            mr.data = new { adList = _adlist, topList = _Toplist, moreList = _Morelist };

            return Content(JsonConvert.SerializeObject(mr));
        }
        /// <summary>
        /// 游戏加币
        /// 183.131.213.10   183.131.69.101
        /// </summary>
        /// <param name="uidx"></param>
        /// <param name="coin"></param>
        /// <param name="gamefield"></param>
        /// <param name="gameid"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        ////[GameAuth]
        public ActionResult gamecoin2(string token, int uidx = 0, int coin = 0, int gamefield = 0, int gameid = 0,string sign="", long order = 0)
        {
            MobileResult mr = new MobileResult();
            string Param = "uidx=" + uidx + "&coin=" + coin + "&gameid=" + gameid + "&token=" + token + CryptoHelper.AES_KEY;
            string Signnew = CryptoHelper.ToMD5(Param).ToLower();
            if (Signnew != sign)
            {
                mr.code = "101";
                mr.msg = "ParamError";
                //  mr.data = string.Format("[游戏]参数错误uidx:{0}，coin:{1}，gameid:{2},field:{3}", uidx, coin, gameid, gamefield);
                return Content(JsonConvert.SerializeObject(mr));
            }

            //string[] serverIP = { "173.248.226.27", "183.131.69.101", "183.131.69.35" };
            //string userIP = Tools.GetRealIP();
            //bool flag = false;

            //foreach (var item in serverIP)
            //{
            //    if (item.Equals(userIP)) { flag = true; }
            //}
            //if (uidx == 60005726 || uidx == 60136341 || uidx == 11114)
            //{
            //    flag = true;
            //}
            //if (!flag)
            //{
            //    mr.code = "103";
            //    mr.msg = "没有权限访问";
            //    //mr.data = string.Format("[游戏]加币没权限访问uidx:{0}，coin:{1}", uidx, coin);
            //    return Content(JsonConvert.SerializeObject(mr));
            //}

            if (string.IsNullOrEmpty(token) || uidx <= 0 || coin <= 0 || gamefield <= 0 || gameid <= 0)
            {
                mr.code = "101";
                mr.msg = "ParamError";
              //  mr.data = string.Format("[游戏]参数错误uidx:{0}，coin:{1}，gameid:{2},field:{3}", uidx, coin, gameid, gamefield);
                return Content(JsonConvert.SerializeObject(mr));
            }
            string mySign = _userbll.GetUserToken(uidx, 1);
            if (string.IsNullOrEmpty(mySign) || !token.Equals(mySign))
            {
                mr.code = "102";
                mr.msg = "签名不正确或已失效";
               // mr.data = "[游戏]验证签名错误,idx：{0},sign：{1},mySign：{2},coin：{3}";
                return Content(JsonConvert.SerializeObject(mr));
            }
            string describe = "";
            if (order > 0)
            {
                describe = "91智投加币";
            }
            else
            {
                describe = "游戏中心加币";
            }
            int result = _gamebll.Game_GameCenter_ChangeCoin(uidx, coin, 79, gameid, gamefield, describe,order);  // profitServie.ChangeGameCoin(uidx, 2, gameid, gamefield, coin);
            if (result == 0)
            {
                // memberOwn mo = userService.getMemberOwnByidx(uidx);
                // ServerHelper.CashUpdate(uidx, mo.owncash);
                mr.code = "100";
                mr.msg = "加币成功";

                mr.data=new { ownCash = _userbll.GetUserCashByIdx(uidx) };
            }
            else
            {
                mr.code = "106";
                mr.msg = "加币失败";

            }

            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 游戏扣币 78
        /// 183.131.213.10   183.131.69.101
        /// </summary>
        /// <param name="uidx"></param>
        /// <param name="coin"></param>
        /// <param name="gamefield"></param>
        /// <param name="gameid"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        ////[GameAuth]
        public ActionResult gamecoin1(string token, int uidx = 0, int coin = 0, int gamefield = 0, int gameid = 0,string sign="",long order = 0)
        {
            MobileResult mr = new MobileResult();
            string Param = "uidx=" + uidx + "&coin=" + coin + "&gameid=" + gameid + "&token=" + token+ CryptoHelper.AES_KEY;
            string Signnew = CryptoHelper.ToMD5(Param).ToLower();
            if (Signnew != sign)
            {
                mr.code = "101";
                mr.msg = "ParamError";
                //  mr.data = string.Format("[游戏]参数错误uidx:{0}，coin:{1}，gameid:{2},field:{3}", uidx, coin, gameid, gamefield);
                return Content(JsonConvert.SerializeObject(mr));
            }

            //string[] serverIP = { "173.248.226.27", "183.131.69.101", "183.131.69.35" };
            //string userIP = Tools.GetRealIP();
            //bool flag = false;

            //foreach (var item in serverIP)
            //{
            //    if (item.Equals(userIP)) { flag = true; }
            //}
            //if (uidx == 60005726 || uidx == 60136341 || uidx == 11114)
            //{
            //    flag = true;
            //}
            //if (!flag)
            //{
            //    mr.code = "103";
            //    mr.msg = "没有权限访问";
            //    //mr.data = string.Format("[游戏]加币没权限访问uidx:{0}，coin:{1}", uidx, coin);
            //    return Content(JsonConvert.SerializeObject(mr));
            //}

            if (string.IsNullOrEmpty(token) || uidx <= 0 || coin <= 0 || gamefield <= 0 || gameid <= 0)
            {
                mr.code = "101";
                mr.msg = "ParamError";
                //  mr.data = string.Format("[游戏]参数错误uidx:{0}，coin:{1}，gameid:{2},field:{3}", uidx, coin, gameid, gamefield);
                return Content(JsonConvert.SerializeObject(mr));
            }
            string mySign = _userbll.GetUserToken(uidx, 1);
            if (string.IsNullOrEmpty(mySign) || !token.Equals(mySign))
            {
                mr.code = "102";
                mr.msg = "签名不正确或已失效";
                // mr.data = "[游戏]验证签名错误,idx：{0},sign：{1},mySign：{2},coin：{3}";
                return Content(JsonConvert.SerializeObject(mr));
            }
            string describe = "";
            if (order > 0)
            {
                describe = "91智投扣币";
            }
            else
            {
                describe = "游戏中心扣币";
            }
            int result = _gamebll.Game_GameCenter_ChangeCoin(uidx, coin, 78, gameid, gamefield,describe,order);  // profitServie.ChangeGameCoin(uidx, 2, gameid, gamefield, coin);
            if (result == 0)
            {
                // memberOwn mo = userService.getMemberOwnByidx(uidx);
                // ServerHelper.CashUpdate(uidx, mo.owncash);
                mr.code = "100";
                mr.msg = "扣币成功";

                mr.data = new { ownCash = _userbll.GetUserCashByIdx(uidx) };
            }
            else
            {
                mr.code = "106";
                mr.msg = "扣币失败";

              
            }

            return Content(JsonConvert.SerializeObject(mr));
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="uidx"></param>
        /// <returns></returns>
        public ActionResult GetUserinfo(string token, int uidx = 0)
        {
            MobileResult mr = new MobileResult();
            //Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (uidx == 0 || token == "")
            {
                mr.code = "101";
                mr.msg = "ParamError";
                //  mr.data = string.Format("[游戏]参数错误uidx:{0}，coin:{1}，gameid:{2},field:{3}", uidx, coin, gameid, gamefield);
                return Content(JsonConvert.SerializeObject(mr));
            }
            string mySign = _userbll.GetUserToken(uidx, 0);
            LogHelper.WriteLog(LogFile.Debug, "token=" + token + "mySign=" + mySign+ "uidx="+ uidx);
            if (string.IsNullOrEmpty(mySign) || !token.Equals(mySign))
            {
                mr.code = "102";
                mr.msg = "签名不正确或已失效";
                // mr.data = "[游戏]验证签名错误,idx：{0},sign：{1},mySign：{2},coin：{3}";
                return Content(JsonConvert.SerializeObject(mr));
            }
            string gToken = CryptoHelper.ToBase64(uidx + "&mbch*kof*lin79&" + token);
            LiveBLL.gamelogin_save(uidx, gToken);

            List<GameCenterUserInfo> list = _gamebll.GameCenterUserinfo(uidx);
            mr.code = "100";
            mr.msg = "success";
            mr.data = new
            {
                list = list[0],
                gameToken = gToken
            };
            return Content(JsonConvert.SerializeObject(mr));
        }
        /// <summary>
        /// 获取用户金币
        /// </summary>
        /// <param name="token"></param>
        /// <param name="uidx"></param>
        /// <returns></returns>
        public ActionResult GetUserCoin(string token, int uidx = 0)
        {
            MobileResult mr = new MobileResult();
            //Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (uidx == 0 || token == "")
            {
                mr.code = "101";
                mr.msg = "ParamError";
                //  mr.data = string.Format("[游戏]参数错误uidx:{0}，coin:{1}，gameid:{2},field:{3}", uidx, coin, gameid, gamefield);
                return Content(JsonConvert.SerializeObject(mr));
            }
            string mySign = _userbll.GetUserToken(uidx, 1);
            if (string.IsNullOrEmpty(mySign) || !token.Equals(mySign))
            {
                mr.code = "102";
                mr.msg = "签名不正确或已失效";
                // mr.data = "[游戏]验证签名错误,idx：{0},sign：{1},mySign：{2},coin：{3}";
                return Content(JsonConvert.SerializeObject(mr));
            }
           
            mr.code = "100";
            mr.msg = "success";
            mr.data = new { ownCash = _userbll.GetUserCashByIdx(uidx) };
            return Content(JsonConvert.SerializeObject(mr));
        }
        #endregion

        /// <summary>
        /// 进入游戏（客户端调用）
        /// </summary>
        /// <param name="curuseridx"></param>
        /// <param name="timestamp"></param>
        /// <param name="gameid"></param>
        /// <param name="signature"></param>
        /// <param name="usertoken"></param>
        /// <returns></returns>
        public ActionResult GetGameInfo(int curuseridx = 0, string timestamp = "", int gameid = 0, string signature = "", string usertoken = "")
        {
            string param = Request.QueryString.ToString();
            string mySign = CryptoHelper.ToMD5("a=" + curuseridx + "&b=" + gameid + "&c=" + timestamp + "&d=mbGame&GameInfo*");
            MobileResult mr = new MobileResult();

            //if (string.IsNullOrEmpty(signature) || signature != mySign)
            //{
            //    LogHelper.WriteLog(LogFile.Error, string.Format("【游戏中心进入游戏验证Sign错误】{0},{1},{2},{3},{4}", curuseridx, gameid, signature, mySign, AppDataBLL.AppVersion));
            //    mr.code = "101";
            //    mr.msg = "param error";
            //    return Content(JsonConvert.SerializeObject(mr));
            //}
            // string tokenKey = CacheKeys.LIVE_USER_TOKEN + curuseridx;

            if (curuseridx <= 0 || gameid <= 0)
            {
                mr.code = "101";
                mr.msg = "param error";
                return Content(JsonConvert.SerializeObject(mr));
            }
            string tokenValue = _userbll.GetUserToken(curuseridx,0);

            if (!string.IsNullOrEmpty(tokenValue) && tokenValue != usertoken /*&& !Tools.GetdeviceName().Equals("iPhone") && !Tools.GetdeviceName().Equals("iPad")*/)
            {
                LogHelper.WriteLog(LogFile.Game, "【游戏中心进入游戏获取信息失败】" + curuseridx + ",token:" + usertoken + ",memToken:" + tokenValue);

                mr.code = "103";
                mr.msg = "获取用户信息失败";
                return Content(JsonConvert.SerializeObject(mr));
            }
      
            var list = live.GetAllGameInfo(gameid);
            if (list != null && list.Count > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = list;

                //string pwd = CryptoHelper.ToBase64(curuseridx + "&mbch*kof*lin79&" + list[0].token);
                //LiveBLL.gamelogin_save(curuseridx, pwd, gameid);

                LiveBLL.gamelogin_save(curuseridx, list[0].token);
            }

            /////运动会地址特殊处理
            //if (list != null && gameid == 101)
            //{
            //    list[0].iosurl = list[0].androidurl = "";
            //    string DES_useridx = CryptoHelper.DESEncrypt(curuseridx.ToString(), "idx&game");
            //    string sportURL = "http://newsport.tsk888.com/LoginH5MB.aspx?platform=mb";// list[0].iosurl;

            //    //H5游戏多拼接参数
            //    if (string.IsNullOrEmpty(list[0].iosName) && sportURL.IndexOf("?") > -1)
            //    {
            //        list[0].iosurl = sportURL + "&useridx=" + DES_useridx;
            //        list[0].androidurl = sportURL + "&useridx=" + DES_useridx;
            //    }
            //}
            
            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 游戏中奖通知
        /// live.9158.com/GameCenter/GameLuckyNotice?msgtype=1&useridx=60068188&gameid=101&nickName=---&content=1000000&sign=ca842b5603895777964e6f2ba3d074d8&timestamp=1484030462
        /// </summary>
        /// <param name="msgtype">msgtype 1:游戏 2：活动</param>
        /// <param name="useridx"></param>
        /// <param name="gameid">10:活动</param>
        /// <returns></returns>
        public ActionResult GameLuckyNotice(int msgtype = 1, int useridx = 91589158, int gameid = 1)
        {
            string nickName = Request["nickName"];
            string content = Request["content"];//eg:1000000
            string timestamp = Request["timestamp"];
            string sign = Request["sign"];
            string mySign = CryptoHelper.ToMD5(string.Format("a={0}&b={1}&c={2}&d=mbGame&notice*", useridx, gameid, timestamp));

            if (string.IsNullOrEmpty(nickName) ||
                string.IsNullOrEmpty(content) ||
                string.IsNullOrEmpty(sign))
            {
                return Content("-1|param empty");
            }

            if (mySign != sign) { return Content("-2|sign false"); }

            var gametype = (msgtype == 2 ? -1 : 0);

            var model = live.GetAllGameInfo(gametype).Find(f => f.gameid == gameid);
            if (model != null && model.gameid > 0)
            {
                var message = (msgtype == 2 ? content : string.Format("恭喜{0}玩『{1}』赢取{2}游戏币大奖", nickName, model.gameName, content));

                var obj = new { gameid = gameid, gameName = model.gameName, msg = message };

                var data = JsonConvert.SerializeObject(obj);

                var ret = DllInvokeMethod.NotifyGameLucky(data);

                LogHelper.WriteLog(LogFile.Game, "【飘屏中奖通知】uidx:{0},gameid:{1},msg:{2}", useridx, gameid, message);

                return Content("1|success");
            }
            else
            {
                return Content("-3|fail");
            }
        }

        /// <summary>
        /// 生成游戏token记录到数据库
        /// DHFUVNCJ_60068188_mb*game_MjE2NTAzODQmbWJjaCprb2YqbGluNzkmMjAxNi8xMi8yMiAxODo0OTo0NQ==_10
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        //private string Get_GameToken(string user, string pwd, int gameid)
        //{
        //    string tokenstr = "";
        //    pwd = pwd.Insert(3, "m");
        //    pwd = pwd.Insert(13, "b");
        //    pwd = CryptoHelper.ToBase64(user + "&mbch*kof*lin79&" + System.DateTime.Now);

        //    //tokenstr = CryptoHelper.DESEncrypt("DHFUVNCJ_" + user + "_mb*game_" + pwd, "D9)3!|WU");//老版本

        //    return tokenstr;
        //}

        /// <summary>
        /// http://live.9158.com/GameCenter/AnswerIndex
        /// </summary>
        /// <param name="anchor">主播idx</param>
        /// <param name="idx">用户idx</param>
        /// <param name="token"></param>
        /// <param name="type">登录账号类型</param>
        /// <param name="gameid"></param>
        /// <returns></returns>
        public ActionResult answerindex(int anchor = 0, int idx = 0, string token = "", int type = 0)
        {
            //0：不需要密码 1：需要密码 第三方登录时传1
            int needPass = 0, lType = 0, isAnchor = 0;
            if (type == 1)
            {
                lType = 1;
                needPass = 1;
            }

            string userip = Tools.GetRealIP();

            Model.View.VMAnswer vmAnswer = new Model.View.VMAnswer();

            vmAnswer.Userinfo = _account.Login(idx.ToString(), token, lType, userip, needPass);

            //TODO 正式上线开启
            //if (vmAnswer.Userinfo == null || vmAnswer.Userinfo.useridx <= 0)
            //{
            //    ViewBag.message = "获取用户信息失败，请退出房间重新连接！";
            //    return View("noPage");
            //}
            int invitedNum = 0;
            vmAnswer.remainNum = _gamebll.Game_Get_AnswerNum_Logic(idx, ref invitedNum);
            vmAnswer.invitedNum = invitedNum;
            vmAnswer.MyWallet = income.Get_AlipayInfo_MyWallet(idx);
            vmAnswer.isAnchor = isAnchor;

            ViewBag.shareURL = "http://answer.twu8.com/H5/Active/ia?idx=" + idx;
            ViewBag.isAnchor = (anchor == idx) ? 1 : 0;
            ViewBag.useridx = vmAnswer.MyWallet.useridx;

            //if (!Tools.IsCompanyIP)
            //{
            //    ViewBag.message = "暂无体验资格，敬请期待！";
            //    return View("noPage");
            //}
            return View(vmAnswer);
        }



        #region  Data

        /// <summary>
        /// 多人挑战用户加入游戏
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="anchoridx"></param>
        /// <param name="gametype"></param>
        /// <returns></returns>
        public ActionResult userJoinAnswerData(int useridx = 0, int anchoridx = 0, int gametype = 0)
        {
            MobileResult mr = new MobileResult();
            int turnid = 0;
            int result = _gamebll.Game_User_AnswerStart_Logic(ref turnid, useridx, anchoridx, gametype);

            mr.code = result.ToString();
            mr.data = new { turnid = turnid };

            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 主播开启游戏
        /// </summary>
        /// <param name="fromidx"></param>
        /// <param name="gameType"></param>
        /// <returns></returns>
        public ActionResult startGameData(int anchoridx = 0, int gameType = 1)
        {
            MobileResult mr = new MobileResult();
            int _turnid = 0;
            int result = _gamebll.Game_Anchor_StartAnswer_Logic(gameType, anchoridx, ref _turnid);

            if (result > 0 && _turnid > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = new { turnid = _turnid };
            }
            else
            {
                mr.code = result.ToString();
            }

            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 排行榜数据
        /// live.9158.com/GameCenter/rankingData?turnid=10&useridx=60068188
        /// </summary>
        /// <param name="turnid"></param>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public ActionResult rankingData(int turnid = 0, int useridx = 0)
        {
            MobileResult mr = new MobileResult();
            int _myScore = 0;

            List<Model.AnswerGame_Ranking> _ranking = _gamebll.Get_GameRanking_Logic(turnid, useridx, ref _myScore);

            mr.code = "100";
            mr.msg = "success";
            mr.data = new { rankList = _ranking, myscore = _myScore, totalCount = _ranking.Count, useridx = useridx };

            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 1v1挑战申请列表
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public ActionResult SingleInviteData(int useridx = 0)
        {
            MobileResult mr = new MobileResult();
            List<Model.AnswerGame_Start> _list = _gamebll.Get_SingleInviteList_Logic(useridx);

            mr.code = "100";
            mr.msg = "success";
            mr.data = new { list = _list, totalCount = _list.Count };

            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 1v1挑战发起信息查询/创建
        /// </summary>
        /// <param name="fromidx"></param>
        /// <param name="toidx"></param>
        /// <returns></returns>
        public ActionResult SingleInviteInfo(int fromidx = 0, int toidx = 0, int anchoridx = 0)
        {
            MobileResult mr = new MobileResult();
            int result = 0;

            if (fromidx == toidx)
            {
                mr.code = "101";
                mr.msg = "自己不能向自己发送邀请";
                return Content(JsonConvert.SerializeObject(mr));
            }

            Model.AnswerGame_Single inviteInfo = _gamebll.Get_SingleInviteInfo_Logic(fromidx, toidx, anchoridx, ref result);

            if (result == -1)
            {
                mr.code = "102";
                mr.msg = "该用户不存在";
                return Content(JsonConvert.SerializeObject(mr));
            }

            if (inviteInfo != null && inviteInfo.turnid > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = inviteInfo;
            }
            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 1v1信息查询
        /// </summary>
        /// <param name="turnid"></param>
        /// <param name="useridx"></param>
        /// <param name="type">1：自己是邀请人，2：自己是被邀请人</param>
        /// <returns></returns>
        public ActionResult SingleStartGameData(int turnid = 0, int useridx = 0, int type = 0)
        {
            MobileResult mr = new MobileResult();

            var _info = _gamebll.SingleStartGameLogic(turnid, useridx, type);

            if (_info != null && _info.fromidx > 0)
            {
                mr.code = "100";
                mr.msg = "success";
                mr.data = _info;
            }

            return Content(JsonConvert.SerializeObject(mr));
        }

        #endregion
        #region  游戏投注信息
        public ActionResult GetGameBetLog()
        {
            MobileResult mr = new MobileResult();
            Dictionary<string, string> dic = CryptoHelper.GetBinaryParam(CryptoHelper.Live_KEY);
            if (dic == null || dic["userIdx"] == "")
            {
                return new BaseController().ParamError("CheckMobile");
            }
            int userIdx = int.Parse(dic["userIdx"]);
            int pageNum = int.Parse(dic["pageNum"]);
            int pageSize = int.Parse(dic["pageSize"]);
            int count = 0;
            //int userIdx = 20191;
            //int pageNum = 1;
            //int pageSize = 8;
            //int count = 0;
            List<GameRuleInfo> list = _gamebll.GetGameBetLog(userIdx, pageNum, pageSize, ref count);
            if (list != null && list.Count > 0)
            {
                mr.code = "100";
            }
            else
            {
                mr.code = "102";
            }
            mr.msg = "success";
            mr.data = new
            {
                list = list,
                count = count
            };
            return Content(JsonConvert.SerializeObject(mr));
        }

        /// <summary>
        /// 游戏封盘
        /// </summary>
        /// <param name="nGameType">游戏类别</param>
        /// <param name="nGameID">游戏id</param>
        /// <param name="nStage">开奖期数</param>
        /// <param name="szGameName">游戏名称</param>
        /// <param name="timeNum">开奖分钟数</param>
        /// <returns></returns>
        public ActionResult httpLotteryRemind(int roomid = 60424678, int nGameType = 0, int nGameID = 1, string nStage = "20180426123", string szGameName = "重庆实时彩", int timeNum = 2)
        {
            MobileResult mr = new MobileResult();
            string iRet = ServerHelper.HTTP_LOTTERY_REMIND(roomid, nGameType, nGameID, nStage, szGameName, timeNum);
            mr.code = "100";
            mr.msg = "success";
            mr.data = iRet;
            return Content(iRet);
        }

        /// <summary>
        /// 游戏开奖
        /// </summary>
        /// <param name="nGameType">游戏类别</param>
        /// <param name="nGameID">游戏id</param>
        /// <param name="nStage">游戏期数</param>
        /// <param name="nOpenNum">开奖号码</param>
        /// <param name="szGameName">游戏名称</param>
        /// <param name="sContent">描述内容</param>
        /// <returns></returns>
        public ActionResult httpLotteryInfo(int roomid = 60424678, int nGameType = 0, int nGameID = 1, string nStage = "20180426123", string nOpenNum = "102382321231", string szGameName = "重庆实时彩", int timeNum = 2)
        {
            MobileResult mr = new MobileResult();
            string iRet = ServerHelper.HTTP_LOTTERY_INFO(roomid, nGameType, nGameID, nStage, nOpenNum, szGameName, timeNum);
            mr.code = "100";
            mr.msg = "success";
            mr.data = iRet;
            return Content(iRet);
        }

        #endregion
    }
}
