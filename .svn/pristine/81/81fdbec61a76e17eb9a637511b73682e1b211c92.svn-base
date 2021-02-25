using BLL.Mongo;
using Common;
using DAL;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;

namespace BLL
{
    public class UserInfoBLL
    {
        UserInfoDAL dal = new UserInfoDAL();


        public int AuthenticationUpload(int useridx,int type, int state = 0, string videourl = "")
        {
            return dal.AuthenticationUpload(useridx,type, state, videourl);
        }
        public string AuthenticationVideo(int useridx)
        {
            return dal.AuthenticationVideo(useridx);
        }
        /// <summary>
        /// 我的卡片用户信息（老版本使用）
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public MyUserInfo GetMyUserInfo(int useridx)
        {
            return dal.getUserInfo(useridx);
        }

        /// <summary>
        /// 根据useridx获取用户单个信息
        /// </summary>
        /// <param name="uidx"></param>
        /// <returns></returns>
        public UserInfo GetLiveUserInfoByIdx(int uidx)
        {
            var key = CacheKeys.LIVE_PHONE_LIVE_USER_INFO_KEY + uidx;
            UserInfo u = CacheHelper.GetCache(key) as UserInfo;
            if (CacheHelper.GetCache(key) == null)
            {
                u = dal.GetLiveUserInfoByIdx(uidx);
                u.logintype = AppDataBLL.GetLoginType(u.userId);

                CacheHelper.SetCache(key, u, 10);
            }
            u.signatures = " ";

            return u;
        }


        /// <summary>
        /// 获取用户其他详细信息(粉丝数，关注数，猫粮，奉献)
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public OtherInfo GetOtherUserInfo(int useridx)
        {
            OtherInfo oi = dal.GetOtherUserInfo_Data(useridx);
            //消费太低
            if (oi != null && (useridx == 60068188))
            {
                oi.fansnum = oi.fansnum + 10000;
            }
            //临时修复 关注数为负数bug
            oi.friendnum = oi.friendnum < 0 ? 0 : Math.Abs(oi.friendnum);
            oi.isV = (oi.isSign == 1 && oi.starlevel > 0) ? 1 : 0;

            return oi;
        }

        /// <summary>
        /// 获取用户靓号
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public int GetUseridxByshortidx(int shortidx)
        {
            return dal.GetUseridxByshortidx_Data(shortidx);
        }

        /// <summary>
        /// 根据用户长号获取短号
        /// </summary>
        /// <param name="shortidx"></param>
        /// <returns></returns>
        public int Get_shortidxByUseridx(int shortidx)
        {
            return dal.Get_shortidxByUseridx_Data(shortidx);
        }

        /// <summary>
        /// 获取用户总 币
        /// </summary>
        /// <param name="uidx"></param>
        /// <returns></returns>
        public Int64 GetUserCashByIdx(int uidx)
        {
            return dal.GetUserCashByIdx(uidx);
        }

        /// <summary>
        /// 获取主播等级信息
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public AnchorInfo Get_AnchorLevel_Info(int useridx)
        {
            return dal.Get_Anchor_Level_Info_Data(useridx);
        }

        /// <summary>
        /// 主播当前任务信息
        /// </summary>
        /// <param name="dataAction"></param>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public AnchorTask Get_AnchorTask_Info(int useridx)
        {
            var task = dal.Get_AnchorTask_Info_Data(1, useridx);
            //var taskLimit = dal.Get_AnchorTaskLimit_Info_Data(useridx);

            return task;
        }

        /// <summary>
        /// 修改用户头像
        /// </summary>
        /// <param name="userIdx"></param>
        /// <param name="nickName"></param>
        /// <param name="signatrues"></param>
        /// <returns></returns>
        public int UpdateUserPhoto(int userIdx, string smallPic, string bigPic)
        {
            int result = dal.UpdateUserPhoto(userIdx, smallPic, bigPic);
            if (result > 0)
            {
                //清理缓存 个人信息
                var key = CacheKeys.LIVE_PHONE_LIVE_USER_INFO_KEY + userIdx;
                CacheHelper.Delete(key);
            }
            return result;
        }
        /// <summary>
        /// 主播头像审核
        /// </summary>
        /// <param name="userIdx"></param>
        /// <param name="smallpic"></param>
        /// <param name="bigpic"></param>
        /// <returns></returns>
        public int UserPhotoAudit(int type, int useridx, string nickName, string photo, string bigpic)
        {
            int result = dal.UserPhotoAudit_Data(type, useridx, nickName, photo, bigpic);

            return result;
        }

        /// <summary>
        /// 查询主播开播时长
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="roomid"></param>
        /// <param name="stardate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        //public List<ASongerInfo> GetUserLiveingTime(int useridx, int roomid, string stardate, string enddate)
        //{
        //    return dal.GetUserLiveingTime(useridx, roomid, stardate, enddate);
        //}

        /// <summary>
        /// 获取我的家族信息
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public Family Get_MyFamilyInfo(int dataType, int useridx)
        {
            string key = "Live_Get_FamilyInfo_" + dataType + "_" + useridx;

            Family fam = CacheHelper.GetCache(key) as Family;

            if (CacheHelper.GetCache(key) == null || fam.useridx <= 0)
            {
                fam = dal.Get_MyFamilyInfo_data(dataType, useridx);
                CacheHelper.SetCache(key, fam, 10);
            }
            return fam;
        }

        /// <summary>
        /// 用户封号操作
        /// </summary>
        /// <param name="userIdx"></param>
        /// <param name="limitTime"></param>
        /// <param name="type"></param>
        /// <param name="adminUser"></param>
        /// <returns></returns>
        public int UserBlackInsert(int useridx, int roomid, DateTime limitTime, string content)
        {
            UserInfo user = GetLiveUserInfoByIdx(useridx);

            if (user.useridx < 1)
            {
                return -3;
            }
            if (user.level > 15)
            {
                return -2;
            }

            int ret = dal.UserBlackInsert(useridx, roomid, limitTime, content);
            string status = ServerHelper.BlackUser(useridx);

            int result = 0;
            if (ret > 0 && status.Equals("40"))
            {
                result = 1;
            }
            string msg = string.Format("【系统封号日志】useridx:{0},roomid:{4},数据库结果:{1},Socket结果:{2},内容：{3}", useridx, ret, status, content, roomid);

            LogHelper.WriteLog(LogFile.Data, msg);

            return result;
        }

        /// <summary>
        /// 加关注封号操作
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="viplevel"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public int UserBlackInsert(int useridx, int viplevel, int grade, string content)
        {
            DateTime time = DateTime.Now.AddYears(100);

            if (viplevel > 15 || grade > 10)
            {
                LogHelper.WriteLog(LogFile.Data, "【加关注系统封号失败】" + useridx + ",level:" + viplevel + ",内容：" + content);
                return 0;
            }

            //顺序不能变（先插入到数据库根据用户等级判断）
            var ret = dal.UserBlackInsert(useridx, 0, time, content);
            if (ret > 0)
            {
                LogHelper.WriteLog(LogFile.Data, "【加关注系统封号成功】" + useridx + ",level:" + viplevel + ",内容:" + content + "," + UtilHelper.GetUserAgent());
                ServerHelper.BlackUser(useridx);
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取广告列表用户
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public DataTable GetAdUser(string content)
        {
            return dal.GetAdUser_Data(content);
        }

        /// <summary>
        /// 修改昵称和修改签名
        /// </summary>
        /// <param name="type"></param>
        /// <param name="useridx"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public int updateInfo(int type, int useridx, string content)
        {
            int length = type == 1 ? 15 : 60;
            content = ThirdLoginBLL.VerifyNickName(content, length);

            bool result = AppDataBLL.VertifyContent(useridx, useridx, 503, content);
            if (result)
            {
                //LogHelper.WriteLog(LogFile.Test, "【喵拍修改昵称和签名】" + useridx + "," + content);
                return -1;
            }

            if (type == 1)
            {
                return dal.updateNickName_Data(useridx, content);
            }
            else
            {
                return dal.updateSignature_Data(useridx, content);
            }
        }

        /// <summary>
        /// 电子签约记录
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public int ElectronicSigning_Record(int useridx)
        {
            return dal.ElectronicSigningRecord_Data(useridx);
        }

        /// <summary>
        /// 判断是否是主播
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public int Game_IsAnchor(int useridx)
        {
            return dal.Game_IsAnchor_Data(useridx);
        }
        /// <summary>
        /// 获取用户token
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public string GetUserToken(int useridx, int tokenType)
        {
            return dal.User_GetUserToken(useridx, tokenType);
        }
        /// <summary>
        /// 获取用户游戏中心token
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public string Get_GameCenter_UserToken(int useridx)
        {
            return dal.User_GetUserToken(useridx, 1);
        }
        #region 相册功能

        /// <summary>
        /// 是否有权限上传相册
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="albumtype"></param>
        /// <returns></returns>
        public int UploadAlbum_Power(int useridx, int albumtype, ref int groupid)
        {
            return dal.UploadAlbum_Power_Data(useridx, albumtype, ref groupid);
        }

        /// <summary>
        /// 上传我的相册
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="albumType"></param>
        /// <param name="imgURL"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public int Insert_MyAlbum(int useridx, Album album)
        {
            return dal.Insert_MyAlbum_Data(useridx, album);
        }

        /// <summary>
        /// 相册查询(卡片上)
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="touseridx"></param>
        /// <param name="albumType"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="totalIncome"></param>
        /// <returns></returns>
        public List<Album> Get_CardAlbumList(int useridx, int touseridx, ref int PrivatePhotosNum)
        {
            return dal.Get_CardAlbumList_Data(useridx, touseridx, ref PrivatePhotosNum);
        }

        /// <summary>
        /// 相册查询(详情页)
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="touseridx"></param>
        /// <param name="albumType"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="totalIncome"></param>
        /// <returns></returns>
        public List<Album> Get_AlbumList(int useridx, int touseridx, int albumType, int page, int pageSize, ref int totalCount, ref int totalIncome)
        {
            var albumList = dal.Get_AlbumList_Data(useridx, touseridx, albumType, page, pageSize, ref totalCount, ref totalIncome);

            //测试代码
            if (useridx == 60068188)
            {
                foreach (var item in albumList)
                {
                    item.isBought = 1;
                }
            }
            return albumList;
        }
        /// <summary>
        /// 类型0不带当前用户是否阅后即焚字段 查看照片
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="page"></param>
        /// <param name="pageIndex"></param>
        /// <param name="count"></param>
        /// <param name="type"></param>
        /// <param name="operuseridx"></param>
        /// <returns></returns>
        public List<AlbumNew> Get_AlbumList(int useridx, int page, int pageIndex ,ref int count,int type=0,int operuseridx=0)
        {
            return dal.Get_AlbumList(useridx, page,pageIndex,ref count, type, operuseridx);
        }
        public int Get_AlbumListChangeposition(int useridx, int albumid, int nowgroupid, int togroupid)
        {
            return dal.Get_AlbumListChangeposition(useridx, albumid, nowgroupid, togroupid);
        }
        /// <summary>
        /// 删除/举报/分享相册
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="albumid"></param>
        /// <returns></returns>
        public int Operate_MyAlbum(int opertype, int useridx, int albumid)
        {
            int result = 0;
            if (opertype == 1)
            {
                result = dal.Delete_MyAlbum_Data(useridx, albumid);
            }
            else if (opertype == 2)
            {
                result = dal.Report_MyAlbum_Data(useridx, albumid);
            }
            else if (opertype == 3)
            {
                result = dal.Share_MyAlbum_Data(useridx, albumid);
            }
            else
            {
                result = 0;
            }
            //LogHelper.WriteLog(LogFile.Debug, "【相册删除/举报/分享】opertype:{0},useridx:{1},albumid:{2},ret:{3}", opertype, useridx, albumid, result);
            return result;
        }

        #endregion

        #region 老挝用户信息
        public int UserRealNameInfoUp(int useridx, string phoneNumber, string userName, string myName, int gender, string dateBirth, string reserveInfo, string address, string remarksr)
        {
            return dal.UserRealNameInfoUp(useridx, phoneNumber, userName, myName, gender, dateBirth, reserveInfo, address, remarksr);
        }

        public int UserRealNameInfoUpdate(UserInfomation userInfomation)
        {
            return dal.UserRealNameInfoUpdate(userInfomation);
        }

        public List<UserInvitationInfo> UserRealNameInfoSet(int useridx)
        {
            return dal.UserRealNameInfoSet(useridx);
        }
        public List<UserInfomation> UserRealNameInfo(int useridx)
        {
            return dal.UserRealNameInfo(useridx);
        }
        public List<UserMynameInfo> UserMyNameInfo(int useridx)
        {
            return dal.UserMyNameInfo(useridx);
        }
        public List<Userevaluate> userevaluates(int useridx)
        {
            return dal.userevaluates(useridx);
        }
        /// </summary>
        /// <param name="useridx">当前用户</param>
        /// <param name="tuseridx">被查看用户</param>
        public int isFans(int useridx,int tuseridx)
        {
            return dal.isFans(useridx, tuseridx);
        }
        public int isBlack(int useridx, int tuseridx)
        {
            return dal.isBlack(useridx, tuseridx);
        }
        
        public List<UserDataInfo> UserDataInfo(int Type)
        {
            string CK = "UserDataInfo_List_"+Type ;
            List<UserDataInfo> iplist = (List<UserDataInfo>)CacheHelper.GetCache(CK);
            if (iplist == null)
            {
                iplist = dal.UserDataInfo();
                //if (Type == 0)
                //{
                //    iplist.RemoveAt(11);
                //    iplist.RemoveAt(14);
                //    iplist.RemoveAt(14);
                //}
                //else
                //{
                //    iplist.RemoveAt(10);
                //    iplist.RemoveAt(12);
                //    iplist.RemoveAt(12);
                //}
                CacheHelper.SetCache(CK, iplist, 10);
            }
            return iplist;
        }
        public int Getuserevaluates(int useridx,int tuseridx,string evaluates)
        {
            return dal.Getuserevaluates(useridx, tuseridx, evaluates);
        }
        public List<Userevaluates> GetuserevaluateList(int useridx, int page, int pagecount)
        {
            return dal.GetuserevaluateList(useridx, page, pagecount);
        }
        public List<UserListInfo> GetLikeList(int useridx, int page, int pagecount)
        {
            return dal.GetLikeList(useridx, page, pagecount);
        }
        public List<UserListInfo> GetProfitList(int useridx, int page, int pagecount)
        {
            return dal.GetProfitList(useridx, page, pagecount);
        }
        public List<UserListInfo> GetBlackList(int useridx, int page, int pagecount)
        {
            return dal.GetBlackList(useridx, page, pagecount);
        }
        public long CoinVip(int useridx,ref string date)
        {
            return dal.CoinVip(useridx, ref date);
        }

        public int UserLoginTokenInset(int useridx, string token)
        {
            return dal.UserLoginTokenInset(useridx, token);
        }
        public List<UserInvitationInfo> phoneNumGetUserRealNameInfoSet(string phoneNum)
        {
            return dal.phoneNumGetUserRealNameInfoSet(phoneNum);
        }
        public int userExchangeCion(int useridx, ref int gameCion, ref int userCion)
        {
            return dal.userExchangeCion(useridx, ref gameCion, ref userCion);
        }
        public  int UserCountInfo(int type ,int useridx,int tueridx,int sex)
        {
            return dal.UserCountInfo(type,useridx, tueridx, sex);
        }
        public int UservisitorsInfo(int type, int useridx,int tuseridx)
        {
            return dal.UservisitorsInfo(type, useridx, tuseridx);
        }
        /// <summary>
        /// 阅后即焚  iRet=0 成功记录 iRet=1 已经查看过了
        /// </summary>
        /// <param name="Albumid"></param>
        /// <param name="useridx"></param>
        /// <param name="tuseridx"></param>
        /// <returns></returns>
        public int GetLookClearPhoto(int Albumid, int useridx, int tuseridx)
        {
            return dal.GetLookClearPhoto(Albumid, useridx, tuseridx);
        }
        /// <summary>
        /// 收费   0 照片收费 1 聊天信息收费
        /// </summary>
        /// <param name="Albumid"></param>
        /// <param name="useridx"></param>
        /// <param name="tuseridx"></param>
        /// <returns></returns>
        public int GetChargeInfo(int Albumid, int useridx, int tuseridx)
        {
            return dal.GetChargeInfo(Albumid, useridx, tuseridx);
        }
        
        /// <summary>
        /// 查看用户是否查看过当前用户的照片或者qq微信
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="tuseridx"></param>
        /// <param name="isphoto"></param>
        /// <param name="isComu"></param>
        /// <returns></returns>
        public int I_userToFoudInfo(int useridx, int tuseridx,ref int isphoto, ref int isComu)
        {
            return dal.I_userToFoudInfo(useridx, tuseridx, ref isphoto, ref isComu);
        }
        public decimal I_userToPhotoMoney(int useridx)
        {
            return dal.I_userToPhotoMoney(useridx);
        }
        
        /// <summary>
        /// 查看距离 vip 认证  时间
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="tuseridx"></param>
        /// <param name="isphoto"></param>
        /// <param name="isComu"></param>
        /// <returns></returns>
        public string I_userInfoExpand(int useridx, int tuseridx, ref int isVIP, ref int IsRealState, ref string Time)
        {
            return dal.I_userInfoExpand(useridx, tuseridx, ref isVIP, ref IsRealState, ref Time);
        }
        /// <summary>
        ///  附近的人
        /// </summary>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        /// <param name="Area">区域漫游</param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="sex">2所有性别  0男 1-女</param>
        /// <param name="vip">0 不是VIP 1是 vip 2所有</param>
        /// <param name="real">0未认证 1-已经认证   2所有</param>
        /// <param name="MonthlyIncome">收入范围</param>
        /// <param name="Bust">胸围</param>
        /// <param name="Agesmall">年龄小</param>
        /// <param name="Agebig">年龄大</param>
        /// <param name="LastLogin">0不限  60-1小时 1440-1天内  4320-3天内</param>
        /// <returns></returns>
        public List<UserListInfo> GetNearList(float longitude, float latitude, string Area, int page, int pagesize, int sex, int vip, int real, string MonthlyIncome, string Bust, int Agesmall, int Agebig, int LastLogin)
        {
            return dal.GetNearList(longitude,latitude,Area,page,pagesize,sex,vip,real,MonthlyIncome,Bust,Agesmall,Agebig,LastLogin);
        }
        /// <summary>
        ///  0牵线墙， 1-新人卡
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="type"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="longitudemin"></param>
        /// <param name="longitudemax"></param>
        /// <param name="latitudemin"></param>
        /// <param name="latitudemax"></param>
        public List<UserListInfo> GetHomeList(float longitude, float latitude, int type, int page, int pagesize, double longitudemin, double longitudemax, double latitudemin, double latitudemax)
        {
            return dal.GetHomeList(longitude, latitude, type,page,pagesize,longitudemin, longitudemax, latitudemin, latitudemax);
        }
        public int GetUserPower(int useridx)
        {
            return dal.GetUserPower(useridx);
        }
        /// <summary>
        /// 银行列表查询
        /// </summary>
        /// <returns></returns>
        public List<BankCardImgConfigure> BankCardImgConfigureSet(int id)
        {
            return dal.BankCardImgConfigureSet(id);
        }


        /// <summary>
        /// 用户银行卡列表查询
        /// </summary>
        /// <param name="userIdx"></param>
        /// <returns></returns>
        public List<LaoWo_UserBankInfo> LaoWo_UserBankInfoSet(int userIdx)
        {
            return dal.LaoWo_UserBankInfoSet(userIdx);
        }

        /// <summary>
        /// 用户银行卡添加
        /// </summary>
        /// <param name="userIdx"></param>
        /// <param name="bankType"></param>
        /// <param name="BankCardType"></param>
        /// <param name="bankUserName"></param>
        /// <param name="bankId"></param>
        /// <param name="rProvince"></param>
        /// <param name="rCity"></param>
        /// <param name="Dot"></param>
        /// <returns></returns>
        public int LaoWo_UserBankInfoInsert(LaoWo_UserBankInfo ubi)
        {
            return dal.LaoWo_UserBankInfoInsert(ubi);
        }

        /// <summary>
        /// 用户银行卡删除
        /// </summary>
        /// <param name="userIdx"></param>
        /// <param name="bankId"></param>
        /// <returns></returns>
        public int LaoWo_UserBankInfoDel(int userIdx, string bankId)
        {
            return dal.LaoWo_UserBankInfoDel(userIdx, bankId);
        }

        /// <summary>
        /// 用户代理信息查询
        /// </summary>
        /// <param name="userIdx"></param>
        /// <returns></returns>
        public List<UserInvitationInfo> GetUserInvitationInfoByUseridxSet(int userIdx)
        {
            return dal.GetUserInvitationInfoByUseridxSet(userIdx);
        }
        public List<LW_IncomeLog> LW_IncomeLogSet(int userIdx, int page, int pageSize, ref int count)
        {

            return dal.LW_IncomeLogSet(userIdx, page, pageSize, ref count);
        }
        public List<LW_Accounts_MoneyTradeDetail> LW_Accounts_MoneyTradeDetailSet(int userIdx, int page, int pageSize, ref int count)
        {
            return dal.LW_Accounts_MoneyTradeDetailSet(userIdx, page, pageSize, ref count);
        }
        public int pwdCheck(int userIdx, string pwd)
        {
            return dal.pwdCheck(userIdx, pwd);
        }


        /// <summary>
        /// 我的代理明细周结束明细
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<myUserAgentInfo> myUserAgentInfoSet(int useridx)
        {
            return dal.myUserAgentInfoSet(useridx);
        }
        #region  通知服务器
        public class asyncCall
        {
            public int useridx, type;

            public void ToCall()
            {
                CallServer(useridx, type);
            }

        }
        /// <summary>
        /// 通知服务器
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="type">-1 评价   2-收益 3-个人信息更新</param>
        /// <returns></returns>
        public static void  CallServer(int useridx ,int type)
        {
            string url = "http://192.168.84.221:8888/";
            if (type == 1)
            {
                url += "CommentRemind";
            }
            else if (type == 2)
            {
                url = "IncomeRemind";
            }
            else if (type == 3)
            {
                url = "UserInfoUpdate";
            }
            else if (type == 4)
            {
                url = "VipBuy";
            }
            else if (type == 5)
            {
                url = "CashUpdate";
            }
            string res = HttpHelper.HttpPost(url,"useridx="+ useridx);
            Dictionary<string,string> value= JsonConvert.DeserializeObject<Dictionary<string, string>>(res);
            if (value["code"] != "100")
            {
                LogHelper.WriteLog(LogFile.Warning, "通知服务器失败useridx{0}type={1}", useridx,type);
            }

        }
        #endregion
        #endregion

    }
}
