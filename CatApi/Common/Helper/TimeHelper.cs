/******************************************
* 文 件 名：TimeHelper
* Copyright(c) live.9158.com
* 创 建 人：zhaorui
* 创建日期：2016年7月
* 文件描述：时间帮助类
******************************************
* 修 改 人：
* 修改日期：
* 备注描述：
*****************************************/
using System;
using Model;

namespace Common
{
    public class TimeHelper
    {
        /// <summary>  
        /// 得到本周第一天(以星期一为第一天)  
        /// </summary>  
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime GetWeekFirstDayMon(DateTime datetime)
        {
            //星期一为第一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);

            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。  
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            int daydiff = (-1) * weeknow;

            //本周第一天  
            string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(FirstDay);
        }

        /// <summary>  
        /// 得到本周最后一天(以星期天为最后一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public static DateTime GetWeekLastDaySun(DateTime datetime)
        {
            //星期天为最后一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            weeknow = (weeknow == 0 ? 7 : weeknow);
            int daydiff = (7 - weeknow);

            //本周最后一天  
            string LastDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(LastDay);
        }

        public static void GetTheWeekFirst_Last(DateTime dt, ref DateTime FirstDay, ref DateTime lastDay)
        {
            //星期一为第一天  
            int weekNow = Convert.ToInt32(dt.DayOfWeek);
            int weekEnd = Convert.ToInt32(dt.DayOfWeek);

            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。  
            weekNow = (weekNow == 0 ? (7 - 1) : (weekNow - 1));
            weekEnd = (weekEnd == 0 ? 7 : weekEnd);

            int daydiffS = (-1) * weekNow;
            int daydiffE = (7 - weekEnd);
            //本周第一天  
            FirstDay = Convert.ToDateTime(dt.AddDays(daydiffS).ToString("yyyy-MM-dd"));
            //本周最后一天
            lastDay = Convert.ToDateTime(dt.AddDays(daydiffE).ToString("yyyy-MM-dd") + " 23:59:59");
        }

        /// <summary>
        /// 获取本月第一天和最后一天
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        public static void GetTheMonthFirst_Last(ref string dt1, ref string dt2)
        {
            DateTime now = DateTime.Now;
            DateTime dtF = new DateTime(now.Year, now.Month, 1);
            dt1 = new DateTime(now.Year, now.Month, 1).ToString();
            dt2 = dtF.AddMonths(1).AddDays(-1).ToShortDateString() + " 23:59:59";
        }

        public static void GetTheMonthFirst_Last(ref DateTime dt1, ref DateTime dt2)
        {
            DateTime now = DateTime.Now;
            dt1 = new DateTime(now.Year, now.Month, 1);
            dt2 = Convert.ToDateTime(dt1.AddMonths(1).AddDays(-1).ToShortDateString() + " 23:59:59");
        }

        /// <summary>
        /// 获取上个月第一天和最后一天
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        public static void GetLastMonthFirst_Last(ref DateTime dt1, ref DateTime dt2)
        {
            DateTime now = DateTime.Now;
            dt1 = new DateTime(now.Year, now.Month - 1, 1);
            dt2 = Convert.ToDateTime(dt1.AddMonths(1).AddDays(-1).ToShortDateString() + " 23:59:59");
        }
        
        #region TimeStamp 时间戳
        
        /// <summary>  
        /// 获取时间戳  
        /// </summary>  
        /// <returns></returns>  
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        public static string GetTimeStamps()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name=”timeStamp”></param>
        /// <returns></returns>
        public static DateTime UnixToTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime); return dtStart.Add(toNow);
        }

        #endregion

        /// <summary>
        /// 计算两个日期的时间间隔,返回的是时间间隔的日期差的绝对值.
        /// </summary>
        /// <param name="DateTime1">第一个日期和时间</param>
        /// <param name="DateTime2">第二个日期和时间</param>
        /// <returns></returns>
        public static int DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            int dateDiff = 0;
            try
            {
                TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();
                dateDiff = ts.Hours * 60 + ts.Minutes;
            }
            catch
            {
                return 0;
            }
            return dateDiff;
        }

        /// <summary>
        /// 计算某个时间是否在指定范围内
        /// </summary>
        /// <param name="time"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static bool IsInTimeInterval(DateTime time, DateTime startTime, DateTime endTime)
        {
            //判断时间段开始时间是否小于时间段结束时间，如果不是就交换
            if (startTime > endTime)
            {
                DateTime tempTime = startTime;
                startTime = endTime;
                endTime = tempTime;
            }

            //获取以公元元年元旦日时间为基础的新判断时间
            DateTime newTime = new DateTime();
            newTime = newTime.AddHours(time.Hour);
            newTime = newTime.AddMinutes(time.Minute);
            newTime = newTime.AddSeconds(time.Second);

            //获取以公元元年元旦日时间为基础的区间开始时间
            DateTime newStartTime = new DateTime();
            newStartTime = newStartTime.AddHours(startTime.Hour);
            newStartTime = newStartTime.AddMinutes(startTime.Minute);
            newStartTime = newStartTime.AddSeconds(startTime.Second);

            //获取以公元元年元旦日时间为基础的区间结束时间
            DateTime newEndTime = new DateTime();
            if (startTime.Hour > endTime.Hour)
            {
                newEndTime = newEndTime.AddDays(1);
            }
            newEndTime = newEndTime.AddHours(endTime.Hour);
            newEndTime = newEndTime.AddMinutes(endTime.Minute);
            newEndTime = newEndTime.AddSeconds(endTime.Second);

            if (newTime > newStartTime && newTime < newEndTime)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 将时间转换成本地时间格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime GetLocalTime(object time)
        {
            return Convert.ToDateTime(time).ToLocalTime();
        }

        public static TimeModel GetCurTime(DateTime time)
        {
            TimeModel t = new TimeModel();
            t.time = time.ToString();
            t.shortDate = time.ToShortDateString();
            t.shortTime = time.ToString("HH:mm:ss");
            t.year = time.Year;
            t.month = time.Month;
            t.day = time.Day;
            t.hour = time.Hour;
            t.minute = time.Minute;
            t.second = time.Second;
            t.weekday = time.DayOfWeek;
            return t;
        }

        public static string DateStringFromNow(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.TotalDays > 60)
            {
                return dt.ToString("yyyy-MM-dd");
            }
            else
            {
                if (span.TotalDays > 30)
                {
                    return
                    "1个月前";
                }
                else
                {
                    if (span.TotalDays > 14)
                    {
                        return
                        "2周前";
                    }
                    else
                    {
                        if (span.TotalDays > 7)
                        {
                            return
                            "1周前";
                        }
                        else
                        {
                            if (span.TotalDays > 1)
                            {
                                return
                                string.Format("{0}天前", (int)Math.Floor(span.TotalDays));
                            }
                            else
                            {
                                if (span.TotalHours > 1)
                                {
                                    return
                                    string.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
                                }
                                else
                                {
                                    if (span.TotalMinutes > 1)
                                    {
                                        return
                                        string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
                                    }
                                    else
                                    {
                                        //if (span.TotalSeconds >= 1)
                                        //{
                                        //    return
                                        //    string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
                                        //}
                                        //else
                                        //{
                                        return "刚刚";
                                        //}
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
