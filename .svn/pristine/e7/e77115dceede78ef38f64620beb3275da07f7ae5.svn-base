using System;
using System.Web;
using System.Text;
using Common;

/// <summary>
/// DLL 调用客户端接口 调用方法类
/// </summary>
public class DllInvokeMethod
{
    //正式服务器
    private static byte[] ipArray = Encoding.ASCII.GetBytes("122.227.58.246");
    //测试
    //private static byte[] ipArray = Encoding.ASCII.GetBytes("122.227.23.109");

    /// <summary>
    /// 更新喵播币通知
    /// </summary>
    /// <param name="fromUserIdx"></param>
    /// <param name="cash">总币数</param>
    /// <returns></returns>
    public static int NotifyCashUpdate(int fromUserIdx, int cash)
    {
        int ret = 0;
        try
        {
            object[] obj = { ipArray, ipArray.Length, 2004, fromUserIdx, cash };
            DllInvoke di = new DllInvoke(HttpContext.Current.Server.MapPath("~/bin/LiveWebActiveNotify.dll"), "NotifyCashUpdate");

            ret = (int)di.Invoke(obj, typeof(int));
        }
        catch
        {
            return -1;
        }
        return ret;
    }
    /// <summary>
    /// 封号通知
    /// </summary>
    /// <param name="userIdx"></param>
    /// <returns></returns>
    public static int NotifyKickPreside(int userIdx)
    {
        int ret = 0;
        try
        {
            DllInvoke di = new DllInvoke(HttpContext.Current.Server.MapPath("~/bin/LiveWebActiveNotify.dll"), "NotifyKickPreside");

            ret = (int)di.Invoke(new object[] { ipArray, ipArray.Length, 2004, userIdx }, typeof(int));
        }
        catch (Exception ex)
        {
            LogHelper.WriteLog(LogFile.Error, "【封号通知出错】" + ex.Message);
            return -1;
        }
        return ret;
    }

    /// <summary>
    /// 游戏中奖通知
    /// </summary>
    /// <param name="userIdx"></param>
    /// <returns></returns>
    public static int NotifyGameLucky(string message)
    {
        byte[] bytes = Encoding.Default.GetBytes(message);
        int ret = 0;
        try
        {
            DllInvoke di = new DllInvoke(HttpContext.Current.Server.MapPath("~/bin/LiveWebActiveNotify.dll"), "NotifyGameLucky");

            ret = (int)di.Invoke(new object[] { ipArray, ipArray.Length, 2004, bytes, bytes.Length }, typeof(int));
        }
        catch (Exception ex)
        {
            LogHelper.WriteLog(LogFile.Game, "【游戏中奖通知出错】" + ex.Message);
            return -4;
        }
        return ret;
    }

    /// <summary>
    /// 系统消息接口通知
    /// </summary>
    /// <param name="message">要发送的系统消息</param>
    /// <param name="length">系统消息长度</param>
    /// <returns></returns>
    //public static int NotifySysInfo(byte[] message, int length)
    //{
    //    try
    //    {
    //        DllInvoke di = new DllInvoke(HttpContext.Current.Server.MapPath("~/bin/LiveWebActiveNotify.dll"), "NotifySysInfo");

    //        return (int)di.Invoke(new object[] { ipArray, ipArray.Length, 2004, message, length }, typeof(int));
    //    }
    //    catch
    //    {
    //        return 0;
    //    }
    //}
    /// <summary>
    /// 聊天关键字屏蔽通知
    /// </summary>
    /// <param name="type">0 删除关键字  1增加关键字</param>
    /// <param name="id">关键字的id</param>
    /// <param name="message">屏蔽关键字的内容</param>
    /// <returns></returns>
    //public static int NotifyKeyBlock(int type, int id, byte[] message, int length)
    //{
    //    //ipArray = Encoding.ASCII.GetBytes("122.226.86.96");
    //    try
    //    {
    //        DllInvoke di = new DllInvoke(HttpContext.Current.Server.MapPath("~/bin/LiveWebActiveNotify.dll"), "NotifyKeyBlock");

    //        int ret = (int)di.Invoke(new object[] { ipArray, ipArray.Length, 2004, type, id, message, length }, typeof(int));
    //    }
    //    catch (Exception ex)
    //    {
    //        return 0;
    //    }
    //    return 0;
    //}

    /// <summary>
    /// 踢人下麦通知
    /// </summary>
    /// <param name="userIdx"></param>
    /// <returns></returns>
    //public static int NotifyKicking(int userIdx, int time)
    //{
    //    try
    //    {
    //        DllInvoke di = new DllInvoke(HttpContext.Current.Server.MapPath("~/bin/LiveWebActiveNotify.dll"), "NotifyBlockPreside");

    //        int ret = (int)di.Invoke(new object[] { ipArray, ipArray.Length, 2004, userIdx, time }, typeof(int));
    //    }
    //    catch
    //    {
    //        return 0;
    //    }
    //    return 0;
    //}
    /// <summary>
    /// 解麦通知
    /// </summary>
    /// <param name="userIdx"></param>
    /// <returns></returns>
    //public static int NotifyFreeKicking(int userIdx)
    //{
    //    try
    //    {
    //        DllInvoke di = new DllInvoke(HttpContext.Current.Server.MapPath("~/bin/LiveWebActiveNotify.dll"), "NotifyFreePreside");

    //        int ret = (int)di.Invoke(new object[] { ipArray, ipArray.Length, 2004, userIdx }, typeof(int));
    //    }
    //    catch (Exception ex)
    //    {
    //        return 0;
    //    }
    //    return 0;
    //}

}
