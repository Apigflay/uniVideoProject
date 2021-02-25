using Common;
using Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace BLL.Mongo
{
    public class MongoService
    {
        private static MongoDBHelper mh = new MongoDBHelper();

        #region Insert
        /// <summary>
        /// 站点出错记录插入
        /// </summary>
        /// <param name="keyword"></param>
        //public static int InsertErrorLog(string rawURL, Exception ex)
        //{
        //    //return 1;
        //    BsonDocument bd = new BsonDocument();
        //    bd.Add("rawURL", CryptoHelper.ToBase64(rawURL));
        //    bd.Add("message", ex.Message);
        //    bd.Add("stateTrace", ex.StackTrace);
        //    bd.Add("createTime", DateTime.Now.ToString());

        //    return mh.Insert("liveLog", bd);
        //}

        /// <summary>
        /// 项目日志记录
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        //public static int InsertLiveLog(WriteLog log)
        //{
        //    return mh.Insert("live_log", log);
        //}

        public static int Insert_RegisterRiskLog<T>(T t) where T : class, new()
        {
            return mh.Insert("register_risk", t);
        }

        public static void Insert_HotRankList<T>(List<T> list)
        {
            mh.Insert("HotRankCache", list);
        }

        /// <summary>
        /// 搜索统计
        /// </summary>
        /// <param name="keyword"></param>
        public static int SearchStat(int useridx, string keyword, TimeSpan time, int haveResult)
        {
            //return 1;
            var ip = Tools.GetRealIP();
            var date = DateTime.Now.ToString();
            var idx = AppDataBLL.GetUseridx;
            var version = AppDataBLL.AppVersion;
            var deviceType = AppDataBLL.AppDeviceType;

            //定义查询的条件
            //var query = Query.EQ("keyword", keyword);
            //var query = new QueryDocument { { "keyword", keyword }, { "date", date }, { "userip", ip } };
            //var update = new UpdateDocument { { "$inc", new QueryDocument { { "number", 1 } } } };

            //return mh.Update("search_log", query, update, UpdateFlags.Upsert);

            BsonDocument bs = new BsonDocument();
            bs.Add("useridx", idx);
            bs.Add("userip", ip);
            bs.Add("keyword", keyword);
            bs.Add("searchTime", date);
            bs.Add("useTime", time.ToString());
            bs.Add("appVersion", version);
            bs.Add("deviceType", deviceType);
            bs.Add("haveResult", haveResult);

            return mh.Insert("search_log", bs);
        }

        /// <summary>
        /// 接口访问统计
        /// 知识点：$inc如果query条件存在则更新update条件，如果不存在则根据query条件插入一条数据（注：$inc只能用于数字类型）
        /// </summary>
        /// <param name="keyword"></param>
        //public static int APIAccessStat(int accessType, string path, string rawURL)
        //{
        //    var date = DateTime.Now.ToString("yyyy-MM-dd");
        //    //定义查询的条件
        //    var query = new QueryDocument { { "path", path }, { "date", date }, { "type", accessType }, { "rawURL", rawURL } };
        //    var update = new UpdateDocument { { "$inc", new QueryDocument { { "number", 1 } } } };

        //    return mh.Update("apiAccess_log", query, update, UpdateFlags.Upsert);
        //}

        /// <summary>
        /// 修改头像统计
        /// </summary>
        /// <param name="keyword"></param>
        //public static int InsertPhotoStat(int useridx, string photo, string bigPic)
        //{
        //    //return 1;
        //    BsonDocument bd = new BsonDocument();
        //    bd.Add("useridx", useridx);
        //    bd.Add("photo", photo);
        //    bd.Add("bigPicture", bigPic);
        //    bd.Add("createTime", DateTime.Now);

        //    return mh.Insert("userPhoto_log", bd);
        //}

        /// <summary>
        /// 第三方新用户注册插入
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        //public static int Insert_Member(Model.MemberInfo m)
        //{
        //    return mh.Insert<Model.MemberInfo>("member_info", m);
        //}

        /// <summary>
        /// 下载统计插入
        /// </summary>
        /// <param name="type"></param>
        /// <param name="path"></param>
        /// <param name="rawurl"></param>
        /// <param name="referer"></param>
        /// <returns></returns>
        public static int Insert_Download(int type, string path, string rawurl, string referer)
        {
            string user_ip = Tools.GetRealIP();
            string ternimal_name = Tools.GetdeviceName();

            var query = new QueryDocument { { "userip", user_ip } };
            var update = new UpdateDocument { { "$setOnInsert", new UpdateDocument {
                                                  {"type",type},
                                                  {"rawURL",rawurl},
                                                  {"deviceName",ternimal_name},
                                                  {"userip",user_ip},
                                                  {"referer",referer},
                                                  {"createTime",DateTime.Now}
            } } };

            return mh.Update("download_stat", query, update, UpdateFlags.Upsert);
        }

        /// <summary>
        /// 邀请用户奖励统计
        /// </summary>
        /// <param name="type">1：扫码生成邀请码 2：新用户奖励数据库返回结果记录 4：邀请码不符合要求</param>
        /// <param name="inviteridx"></param>
        /// <param name="appVersion"></param>
        /// <param name="explain"></param>
        /// <returns></returns>
        public static int InsertInviteAccess(int type, int useridx, string inviteCode, string deviceid, string appVersion, string thirdName, int result, string message = "")
        {
            return 0;
            string userip = Tools.GetRealIP();
            string device = Tools.GetdeviceName();
            string agentName = UtilHelper.GetUserAgent();

            BsonDocument bd = new BsonDocument();
            bd.Add("_id", Guid.NewGuid().ToString());
            bd.Add("type", type);
            bd.Add("inviteCode", inviteCode);
            bd.Add("useridx", useridx);
            bd.Add("thirdName", thirdName);
            bd.Add("deviceid", deviceid);
            bd.Add("deviceName", device);
            bd.Add("userip", userip);
            bd.Add("appVersion", appVersion);
            bd.Add("message", message);
            bd.Add("result", result);
            bd.Add("agentName", agentName);
            bd.Add("createTime", DateTime.Now);

            return mh.Insert("invite_log", bd);
        }

        //public static int InsertdeviceNumber(int osType, int useridx, string deviceNumber, string unionid)
        //{
        //    BsonDocument bd = new BsonDocument();
        //    bd.Add("_id", Guid.NewGuid().ToString());
        //    bd.Add("osType", osType);
        //    bd.Add("useridx", useridx);
        //    bd.Add("deviceNumber", deviceNumber);
        //    bd.Add("androidUnionid", unionid);

        //    return mh.Insert("device_log", bd);
        //}

        /// <summary>
        /// 关注插入
        /// </summary>
        /// <param name="type"></param>
        /// <param name="from_idx"></param>
        /// <param name="to_idx"></param>
        /// <param name="deviceid"></param>
        /// <returns></returns>
        public static int InsertSetFollow(int type, int from_idx, int to_idx, string deviceid)
        {
            string userip = Tools.GetRealIP();

            BsonDocument bd = new BsonDocument();
            bd.Add("_id", Guid.NewGuid().ToString());
            bd.Add("type", type);
            bd.Add("fuseridx", from_idx);
            bd.Add("useridx", to_idx);
            bd.Add("userip", userip);
            bd.Add("deviceid", deviceid);
            bd.Add("createTime", DateTime.Now);

            return mh.Insert("fans_log", bd);
        }

        /// <summary>
        /// H5页面访问记录
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="apiName"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static int Inseret_H5WebAccess(int useridx, string apiName, string title)
        {
            string userip = Tools.GetRealIP();

            BsonDocument bd = new BsonDocument();

            bd.Add("_id", Guid.NewGuid().ToString());
            bd.Add("useridx", useridx);
            bd.Add("apiName", apiName);
            bd.Add("title", title);
            bd.Add("userip", userip);
            bd.Add("createTime", DateTime.Now);

            return mh.Insert("H5WebAccess", bd);
        }
        #endregion

        #region Update

        /// <summary>
        /// 第三方注册唯一ID统计
        /// 知识点：$setOnInsert如果query条件存在则不更新update条件，如果query条件不存在则插入数据根据update
        /// </summary>
        /// <param name="uidx"></param>
        /// <param name="thirdAccount"></param>
        /// <param name="Introduce"></param>
        //public static int ThirdAccountStat(int uidx, string thirdAccount, string Introduce)
        //{
        //    //return 1;
        //    var query = new QueryDocument { { "thirdAccount", thirdAccount } };
        //    var update = new UpdateDocument { { "$setOnInsert", new UpdateDocument {
        //                                          {"useridx",uidx},
        //                                          {"date",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")},//大写HH表示24小时制
        //                                          {"thirdType",Introduce},
        //                                          {"thirdAccount",thirdAccount},
        //                                          {"regIP",Tools.GetRealIP()},
        //                                          {"serverName",System.Net.Dns.GetHostName()},
        //                                          {"createAt",MongoTime.NowTime}
        //    } } };

        //    return mh.Update("thirdAccountBind", query, update, UpdateFlags.Upsert);
        //}

        #endregion

        public static MongoCursor<RoomOnline_V1> Get_HotRoomOnlineList()
        {
            MongoCursor<RoomOnline_V1> roomList = mh.FindAll<RoomOnline_V1>("HotRankCache");

            return roomList;
        }
    }
    public class MongoTime
    {
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public static DateTime NowTime { get { return DateTime.Now; } }
    }
}
