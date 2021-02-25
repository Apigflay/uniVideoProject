using DAL;
using System.Collections.Generic;
using Model;
using Common;
using System;

namespace BLL
{
    public class GiftBLL
    {
        private GiftDAL gift = new GiftDAL();

        /// <summary>
        ///  获取礼物分类
        /// </summary>
        /// <param name="param1">预留参数</param>
        /// <returns></returns>
        public List<GiftTab> GetGiftTabList(int param1)
        {
            List<GiftTab> tabList = gift.GetGiftTabList_Data(param1);

            return tabList;
        }

        /// <summary>
        /// 获取礼物列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="platform"></param>
        /// <param name="isNewApp"></param>
        /// <returns></returns>
        public List<GiftModel> getGiftList(int type, int isNewApp)
        {
            string auditStatus = AppDataBLL.AuditStatus;
            string areaid = AppDataBLL.GetAreaid;
            string CK = "Live_GetGift_List_" + type + "_" + isNewApp + auditStatus + areaid;

            List<GiftModel> giftList = (List<GiftModel>)CacheHelper.GetCache(CK);

            if (giftList == null)
            {
                giftList = gift.GetGiftList_Data(type, isNewApp);

                CacheHelper.SetCache(CK, giftList, 5);
            }

            if (isNewApp == 1)
            {
                foreach (var item in giftList)
                {
                    if (auditStatus == "0")
                    {
                        item.content = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(item.iconTemp))
                    {
                        item.icon = item.iconTemp;
                        item.hoticon = item.iconTemp;
                    }
                }

                //海外版排序
                if (areaid != "0")
                {
                    giftList.Sort((x, y) => x.sortid.CompareTo(y.sortid));//升序
                }
            }
            return giftList;
        }

        /// <summary>
        /// 娃娃兑换申请(目前只针对签约主播)
        /// </summary>
        /// <param name="t"></param>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public int BabyExchange_Submit(int t, int useridx)
        {
            int num = 0;
            int type = 0;

            if (isSign_Anchor(useridx) == 1)
            {
                type = 1;
            }
            else if (isRetail_Anchor(useridx) == 1)
            {
                type = 2;
            }
            if (type == 0) { return -1; }

            return gift.BabyExchange_Data(type, useridx, num);
        }

        /// <summary>
        /// 获取上周及当前周娃娃数
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public GiftExchange Get_BabyInfo(int useridx)
        {
            //本周第一天
            string FirstDay = TimeHelper.GetWeekFirstDayMon(DateTime.Now).ToShortDateString();
            string stardate = DateTime.Parse(FirstDay).AddDays(-7).ToString("yyyy-MM-dd");
            string enddate = FirstDay;

            GiftExchange babyInfo = gift.getWaWaNums(useridx, stardate, enddate);
            babyInfo.isSanhu = isRetail_Anchor(useridx);
            //babyInfo.isSignAnchor = isSign_Anchor(useridx);

            //签约主播和散户主播 宝宝数查询
            int babyNum = 0;
            if (babyInfo.isSignAnchor == 1)
            {
                babyNum = Get_BabyNumsByuseridx(useridx, 2);
            }
            else if (babyInfo.isSanhu == 1)
            {
                babyNum = Get_BabyNumsByuseridx_sanhu(useridx, 2);
            }

            babyInfo.wawaNum = babyNum;

            return babyInfo;
        }

        /// <summary>
        /// 获取宝宝兑换记录
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<GiftExchange> Get_BabyExchange_Record(int useridx)
        {
            return gift.Get_BabyExchange_Record_Data(useridx);
        }

        /// <summary>
        /// 获取签约主播宝宝数
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="dataAction"></param>
        /// <returns></returns>
        private int Get_BabyNumsByuseridx(int useridx, int dataAction)
        {
            int babyNum = 0;
            gift.Get_BabyNumsByuseridx_Data(useridx, dataAction, ref babyNum);
            return babyNum;
        }

        /// <summary>
        /// 获取散户主播未兑换宝宝数
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="dataAction"></param>
        /// <returns></returns>
        private int Get_BabyNumsByuseridx_sanhu(int useridx, int dataAction)
        {
            int babyNum = 0;
            gift.Get_BabyNumsByuseridx_Sanhu_Data(useridx, dataAction, ref babyNum);
            return babyNum;
        }

        public int isRetail_Anchor(int useridx)
        {
            return gift.isRetail_Anchor_Data(useridx);
        }

        public int isSign_Anchor(int useridx)
        {
            return gift.isSign_Anchor_Data(useridx);
        }
    }
}
