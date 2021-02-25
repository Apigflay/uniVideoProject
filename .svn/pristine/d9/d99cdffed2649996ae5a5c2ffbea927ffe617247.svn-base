using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data;

using Model;
using Newtonsoft.Json;
using Common;
using BLL;
namespace WebAPI.Controllers
{
    public class BaiduPushController : Controller
    {
        private const string BaiduCachekey = "BaiduHot_CacheKey_";

        // 单发通知、消息 
        // GET: /SingleDevice/
        public void SingleDevice(string ChannelId, string MsgType, string Message)
        {
            Result r = new Result();
            if (ChannelId == "" || Message == "")
            {
                r.code = "101";
                r.msg = "参数错误";
                r.data = "{}";
                //LogHelper.WriteLog(LogFile.Test, r.code + "," + r.msg);
                //return Content(JsonHelper.Serialize<Result>(r));
            }
            //string result = "";
            if (MsgType == "1")
            {
                try
                {
                    /*
                    string apiKey = "ySw3zHKGl3pn3FZ6647zw27O";
                    string secretKey = "GgnGZ7I2k01lnY84CP9o627GvTQ7EMyB";
                    dynamic dc = JsonHelper.DynamicConvertJson(Message);    //System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(Message))
                    //string custom_content = (dc.serverinfo == null ? "" : "{\"roomid\":\"" + dc.serverinfo.roomid + "\",\"serverid\":\"" + dc.serverinfo.serverid + "\",\"useridx\":\"" + dc.serverinfo.useridx + "\"}");
                    //string custom_content = JsonConvert.SerializeObject(dc.serverinfo);
                    BaiduMsgModel bm = new BaiduMsgModel(dc.description, dc.description, "2");
                    if (dc.serverinfo != null)
                    {
                        Custom_content content = new Custom_content(dc.serverinfo.roomid.ToString(), dc.serverinfo.serverid.ToString(), dc.serverinfo.useridx.ToString());
                        bm.custom_content = content;
                        //JsonConvert.SerializeObject(content, Formatting.None);
                    }
                    //result = JsonConvert.SerializeObject(bm);
                    string json = JsonConvert.SerializeObject(bm);
                    BaiduNotice bn = new BaiduNotice(apiKey, ChannelId, json, ToolsHelper.GetDefauleTimestamp(), (int)Baidu_Helper.Message_Type.Notice);
                    Push_Single_Device psd = new Push_Single_Device(secretKey, bn);
                    result = psd.PushMessage();
                    */
                    XingeApp app = new XingeApp("2100216444", "2a13a1b9b7c1affb142b82dac38ef916");//AN54V9TC42ZS
                    Dictionary<string, object> dc = JsonConvert.DeserializeObject<Dictionary<string, object>>(Message);
                    //dynamic dc = JsonHelper.DynamicConvertJson(Message);
                    Msg_Android msg = new Msg_Android(dc["description"].ToString());//dc.description
                    //msg.action = "{\"action_type\":1,\"intent\":\"com.tiange.miaolive\"}";
                    if (dc.ContainsKey("serverinfo"))   //dc.serverinfo != null
                    {
                        //IDictionary<string, string> content = new Dictionary<string, string>();
                        //content.Add("roomid", dc.serverinfo.roomid.ToString());
                        //content.Add("serverid", dc.serverinfo.serverid.ToString());
                        //content.Add("useridx", dc.serverinfo.useridx.ToString());
                        msg.custom_content = dc["serverinfo"].ToString().Replace("\r\n", "").Replace(" ", "");//dc.serverinfo.ToString();//JsonConvert.SerializeObject(content);
                    }
                    r = app.PushToSingleDevice(ChannelId, msg);
                    //LogHelper.WriteLog(LogFile.Test, r.code + "," + r.msg);
                    //return Json(r, JsonRequestBehavior.AllowGet);
                    //return new EmptyResult();
                }
                catch
                {
                    r.code = "102";
                    r.msg = "json参数异常";
                    r.data = "{}";
                    //LogHelper.WriteLog(LogFile.Test, r.code + "," + r.msg);
                    //return Content(JsonHelper.Serialize<Result>(r));
                }
            }
            //return Content(result);
        }

        
        // 推送批量通知、消息 
        // GET: /MultiDevice/
        public void MultiDevice(string MsgType, string Message)
        {
            Result r = new Result();
            if (Message == "")
            {
                r.code = "101";
                r.msg = "参数错误";
                r.data = "{}";
                //LogHelper.WriteLog(LogFile.Test, r.code + "," + r.msg);
                //return Content(JsonHelper.Serialize<Result>(r));
            }
            //string result = "";
            if (MsgType == "1")
            {
                try
                {
                    XingeApp app = new XingeApp("2100216444", "2a13a1b9b7c1affb142b82dac38ef916");//AN54V9TC42ZS
                    Dictionary<string, object> dc = JsonConvert.DeserializeObject<Dictionary<string, object>>(Message);
                    //dynamic dc = JsonHelper.DynamicConvertJson(Message);
                    Msg_Android msg = new Msg_Android(dc["description"].ToString());//dc.description
                    //msg.action = "{\"action_type\":1,\"intent\":\"com.tiange.miaolive\"}";
                    if (dc.ContainsKey("serverinfo"))   //dc.serverinfo != null
                    {
                        //IDictionary<string, string> content = new Dictionary<string, string>();
                        //content.Add("roomid", dc.serverinfo.roomid.ToString());
                        //content.Add("serverid", dc.serverinfo.serverid.ToString());
                        //content.Add("useridx", dc.serverinfo.useridx.ToString());
                        msg.custom_content = dc["serverinfo"].ToString().Replace("\r\n", "").Replace(" ", "");//dc.serverinfo.ToString();//JsonConvert.SerializeObject(content);
                    }
                    r = app.PushToMultiDevice(msg);
                    //LogHelper.WriteLog(LogFile.Test, r.code + "," + r.msg);
                    //return Json(r, JsonRequestBehavior.AllowGet);
                    //return new EmptyResult();
                }
                catch
                {
                    r.code = "102";
                    r.msg = "json参数异常";
                    r.data = "{}";
                    //LogHelper.WriteLog(LogFile.Test, r.code + "," + r.msg);
                    //return Content(JsonHelper.Serialize<Result>(r));
                }
            }
            //return Content(result);
        }

        // 百度热门主播
        // GET: /HotLive/
        public ActionResult HotLive()
        {
            Result r = new Result();
            DataTable dt = new DataTable();
            Object o = CacheHelper.GetCache(BaiduCachekey);
            if (o != null)
            {
                dt = (DataTable)o;
            }
            else
            {
                dt = LiveBLL.GetBaiduHotLive();
                CacheHelper.SetCache(BaiduCachekey, dt, 3);
            }
            string[] type = { "才艺女神", "气质女神", "清纯女神", "萌妹", "明星", "萝莉", "女汉子" };
            if (dt != null && dt.Rows.Count>0)
            {
                r.code = "100";
                r.msg = "";
                //r.data = dt;// JsonHelper.Serialize(dt);
                DataTable dt1 = new DataTable("dt1");
                dt1.Columns.Add(new DataColumn("u", Type.GetType("System.Int32")));
                dt1.Columns.Add(new DataColumn("i", Type.GetType("System.Int32")));
                dt1.Columns.Add(new DataColumn("n", Type.GetType("System.String")));
                dt1.Columns.Add(new DataColumn("l", Type.GetType("System.Int32")));
                dt1.Columns.Add(new DataColumn("p", Type.GetType("System.String")));
                dt1.Columns.Add(new DataColumn("t", Type.GetType("System.String")));
                dt1.Columns.Add(new DataColumn("o", Type.GetType("System.Int32")));
                dt1.Columns.Add(new DataColumn("m", Type.GetType("System.Int32")));
                dt1.Columns.Add(new DataColumn("r", Type.GetType("System.String")));
                dt1.Columns.Add(new DataColumn("a", Type.GetType("System.String")));
                dt1.Columns.Add(new DataColumn("ts", Type.GetType("System.String")));
                Random rd = new Random();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int uidx = (int)dt.Rows[i]["useridx"];
                    int ridx = (int)dt.Rows[i]["roomid"];
                    int sex = (int)dt.Rows[i]["gender"];
                    if (sex == 1)
                        continue;
                    DateTime time = Convert.ToDateTime(dt.Rows[i]["stardate"].ToString());
                    TimeSpan ts1 = new TimeSpan(TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970,1,1)).Ticks);
                    TimeSpan ts2 = new TimeSpan(time.Ticks);
                    double ts = ts2.Subtract(ts1).TotalSeconds;
                    int n = rd.Next(1, type.Length);
                    DataRow dr = dt1.NewRow();
                    dr["u"] = uidx;
                    dr["i"] = ridx;
                    dr["n"] = dt.Rows[i]["myname"].ToString();
                    dr["l"] = dt.Rows[i]["starlevel"].ToString();
                    dr["p"] = dt.Rows[i]["bigpic"].ToString();
                    dr["t"] = type[n];
                    dr["o"] = 1;
                    dr["m"] = dt.Rows[i]["allnum"].ToString();
                    dr["r"] = "http://www.miaobolive.com/play.aspx?idx=" + ridx + "&useridx=" + uidx + "&serverid=" + dt.Rows[i]["serverid"].ToString();
                    dr["a"] = dt.Rows[i]["gps"].ToString();
                    dr["ts"] = ts;
                    dt1.Rows.Add(dr);
                }
                r.data = dt1;
            }
            else
            {
                r.code = "102";
                r.msg = "fail";
                r.data = null;
            }
            return Content(JsonConvert.SerializeObject(r));
            //return Json(r, JsonRequestBehavior.AllowGet);
        }

        // 百度热门主播
        // GET: /HotLiveShow/
        public ActionResult HotLiveShow(string Ts, string Sign, string Ids)
        {
            if (string.IsNullOrEmpty(Ts) || string.IsNullOrEmpty(Sign) || String.IsNullOrEmpty(Ids))
            {
                
            }
            else
            {
                string key = "vwvibyz48shkp4zcbdbwya5usd59xa6y";
                string md5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key + Ts, "md5").ToLower();
                if(md5.Equals(Sign))
                {
                    DataTable dt1 = new DataTable();
                    Object o = CacheHelper.GetCache(BaiduCachekey);
                    if (o != null)
                    {
                        dt1 = (DataTable)o;
                    }
                    else
                    {
                        dt1 = LiveBLL.GetBaiduHotLive();
                        CacheHelper.SetCache(BaiduCachekey, dt1, 3);
                    }
                    if(dt1!=null && dt1.Rows.Count>0)
                    {
                        string[] id = Ids.Split(',');
                        DataTable dt2 = new DataTable();
                        DataRow dr;
                        dt2.Columns.Add("u", typeof(int));
                        for (int i = 0; i < id.Length; i++)
                        {
                            dr = dt2.NewRow();
                            dr["u"] = id[i];
                            dt2.Rows.Add(dr);
                        }
                        /*
                        List<LiveInfo> p = (from t1 in dt1.AsEnumerable()
                                 join t2 in dt2.AsEnumerable() 
                                 on t1.Field<int>("u") equals t2.Field<int>("u")
                                 where t1.Field<int>("o") == 1
                                select new LiveInfo
                                { 
                                     U = t1.Field<int>("u"),
                                     N = t1.Field<string>("n"),
                                     P = t1.Field<string>("p"),
                                     Area = t1.Field<string>("pro") + t1.Field<string>("city")
                                }).ToList<LiveInfo>();
                        */
                        List<LiveInfo> p = (from t1 in dt1.AsEnumerable()
                                 join t2 in dt2.AsEnumerable()
                                 on t1.Field<int>("useridx") equals t2.Field<int>("u")
                                 where t1.Field<int>("gender") == 0
                                select new LiveInfo
                                { 
                                     U = t1.Field<int>("useridx"),
                                     N = t1.Field<string>("myname"),
                                     P = t1.Field<string>("bigpic"),
                                     Area = t1.Field<string>("gps")
                                }).ToList<LiveInfo>();
                        ViewData["data"] = p;
                    }
                }
            }
            return View();
        }
    }
}