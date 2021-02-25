using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Aliyun.MNS;
using Aliyun.MNS.Model;
using Common;

namespace ThirdAPI
{
    /// <summary>
    /// 阿里云视频存储类
    /// </summary>
    public class MNSHelper
    {

        private const string _accessKeyId = "LTAIfZOd91ujmXwj";
        private const string _secretAccessKey = "kZtsB9HvhK6OgeuOMyh6fIHMSpmcuj";
        private const string _endpoint = "https://1715479374846550.mns.cn-hangzhou.aliyuncs.com/";

        private const string _queueName = "miaobolive";
        private const string _queueNamePrefix = "my";
        private const int _receiveTimes = 1;
        private const int _receiveInterval = 2;
        private const int batchSize = 6;
        private static string _receiptHandle;


        /// <summary>
        /// 存储视频
        /// </summary>
        /// <param name="flv"></param>
        /// <returns></returns>
        public static string StoredVideo(string flv)
        {

            IMNS client = new Aliyun.MNS.MNSClient(_accessKeyId, _secretAccessKey, _endpoint);

            /* 7. Send message */
            try
            {
                var nativeQueue = client.GetNativeQueue(_queueName);
                //var sendMessageResponse = nativeQueue.SendMessage("Hello world!", 10, 4);
                var sendMessageRequest = new SendMessageRequest(flv);
                sendMessageRequest.DelaySeconds = 2;
                var sendMessageResponse = nativeQueue.SendMessage(sendMessageRequest);

                Console.WriteLine("Send message successfully,{0}", sendMessageResponse.ToString());

                //Thread.Sleep(2000);
                return "success";
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Send message failed, exception info: " + ex.Message);
                LogHelper.WriteLog(LogFile.Error, "【存储视频错误】" + ex.Message);
            }
            /* 8. Receive message */
            //try
            //{
            //    var nativeQueue = client.GetNativeQueue(_queueName);
            //    for (int i = 0; i < _receiveTimes; i++)
            //    {
            //        var receiveMessageResponse = nativeQueue.ReceiveMessage(30);
            //        Message message = receiveMessageResponse.Message;

            //        _receiptHandle = message.ReceiptHandle;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //Console.WriteLine("Receive message failed, exception info: " + ex.Message);
            //}

            return "fail";
        }

    }
}
