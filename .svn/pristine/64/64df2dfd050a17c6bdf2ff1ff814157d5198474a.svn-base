using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Model
{
    public class AnswerGameModel
    {

    }

    public class AnswerGame_Start
    {
        public int turnid { get; set; }
        public int gameStatus { get; set; }
        public int fromidx { get; set; }
        public int roomid { get; set; }
        public string myname { get; set; }
        public DateTime createTime { get; set; }

        private string _timeStr;

        public string TimeStr
        {
            get { return _timeStr; }
            set { _timeStr = value; }
        }
    }

    /// <summary>
    /// 答题题库
    /// </summary>
    public class AnswerGame_Subjects
    {
        public int subjectId { get; set; }
        public string question { get; set; }
        public string options1 { get; set; }
        public string options2 { get; set; }
        public string options3 { get; set; }
        public string options4 { get; set; }
        public int answer { get; set; }
    }

    /// <summary>
    /// 排行榜数据展示
    /// </summary>
    public class AnswerGame_Ranking : UserInfo
    {
        public Int64 rowNo { get; set; }
        public int rewardType { get; set; }
        public double rewardCash { get; set; }
        public int score { get; set; }
        public int ranking { get; set; }
    }

    /// <summary>
    /// 1v1发起挑战模型
    /// </summary>
    public class AnswerGame_Single
    {
        public int turnid { get; set; }
        public int gameStatus { get; set; }
        public int fromidx { get; set; }
        public int toidx { get; set; }
        public string toName { get; set; }
        public string toUserPhoto { get; set; }
        public string myName { get; set; }
        public string myPhoto { get; set; }
    }

    /// <summary>
    /// 老挝游戏投注规则详细说明
    /// </summary>
    public class GameRuleInfoExp
    {
        public int gameId { get; set; }//游戏名称
        public string gameExp { get; set; }
        public int wanfaType { get; set; }//玩法
        public string wanfaExp { get; set; }
        public int weizhiType { get; set; }//位置
        public string weizhiExp { get; set; }
        public int  neirongType { get; set; }//内容
        public string neirongExp { get; set; }
    }
    #region 投注历史
    public class GameRuleInfo
    {

        /// <summary>
        /// 游戏名称
        /// </summary>
        public string gameName { get; set; }

        /// <summary>
        /// 规则名称
        /// </summary>
        public string ruleName { get; set; }
        /// <summary>
        /// 局数序列号
        /// </summary>
        public string sequence { get; set; }
        /// <summary>
        /// 开奖结果
        /// </summary>
        public string awardResults { get; set; }
        /// <summary>
        /// 规则
        /// </summary>
        public string ruleInfo { get; set; }
        /// <summary>
        /// 投注金额
        /// </summary>
        public string money { get; set; }
        /// <summary>
        /// 中奖结果
        /// </summary>
        public int winning { get; set; }
        /// <summary>
        /// 
        /// 结算的金额
        /// </summary>
        public string resultMoney { get; set; }

        /// <summary>
        /// 有参构造
        /// </summary>
        /// <param name="dr">原始数据dr</param>
        /// <param name="list">游戏规则</param>
        public GameRuleInfo(DataRow dr, List<GameRuleInfoExp> list)
        {
            if (dr==null)
            {
                return;
            }
            GameRuleInfoExp gexp = getResult(list, int.Parse(Transformation(dr, "cp_type", 2)), int.Parse(Transformation(dr, "wanfa", 2)), int.Parse(Transformation(dr, "weizhi", 2)), int.Parse(Transformation(dr, "neirong", 2)));
            this.gameName =gexp.gameExp;
            this.ruleName = gexp.wanfaExp;
            this.sequence = Transformation(dr, "expect",1);
            this.awardResults = Transformation(dr, "opencode", 1);
            this.ruleInfo = gexp.wanfaExp + ":" + gexp.weizhiExp + gexp.neirongExp;
            this.money =Transformation(dr, "jiner",2);
            this.winning = getWinning(dr);
            this.resultMoney =Transformation(dr, "win",1);
        }

        /// <summary>
        /// 位置
        /// </summary>
        /// <param name="list"></param>
        /// <param name="Result"></param>
        /// <param name="ruleId"></param>
        /// <returns></returns>
        private GameRuleInfoExp getResult(List<GameRuleInfoExp> list, int gameId, int wanfaType, int weizhi, int neirong)
        {
            GameRuleInfoExp gexp = new GameRuleInfoExp();
            gexp = list.Find(x => x.gameId == gameId && x.wanfaType == wanfaType&&x.weizhiType==weizhi&&x.neirongType==neirong);
            if (gexp==null|| gexp.gameExp=="")
            {
                gexp = list.Find(x => x.gameId == gameId && x.wanfaType == wanfaType && x.weizhiType == weizhi);
                gexp.neirongExp = neirong.ToString();
            }
            return gexp;
        }
    

        /// <summary>
        /// 获取中奖结果
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private int getWinning(DataRow dr)
        {
            
            if (Transformation(dr, "opentimestamp", 1) == "")
            {
                return 0;
            }
            else
            {

                if (Convert.ToDouble(Transformation(dr, "win", 2)) > 0)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }


        }

        /// <summary>
        /// 转换空验证
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string Transformation(DataRow dr, string key, int type)
        {
            if (type == 2)
            {
                if (dr[key] != null)
                {
                    if (dr[key].ToString()=="")
                    {
                        return "0";
                    }
                    return dr[key].ToString();
                }
            }
            if (type==1)
            {
                if (dr[key] != null)
                {
                    return dr[key].ToString();
                }
            }
            return "";
        }
    }
    #endregion
}
