using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using Common;
using System.Data;

namespace BLL
{
    public class GameBLL
    {

        private GameDAL _gamedll = new GameDAL();
        private UserInfoBLL _user = new UserInfoBLL();

        /// <summary>
        /// 判断当前用户进入的游戏房间是否可以答题
        /// </summary>
        /// <param name="turnid"></param>
        /// <param name="useridx"></param>
        /// <param name="anchoridx"></param>
        /// <returns></returns>
        public int Game_User_AnswerStart_Logic(ref int turnid, int useridx, int anchoridx, int gametype)
        {
            return _gamedll.Game_User_AnswerStart_Data(ref turnid, useridx, anchoridx, gametype);
        }

        /// <summary>
        /// 主播开启游戏
        /// </summary>
        /// <param name="gameType"></param>
        /// <param name="fromidx"></param>
        /// <param name="turnid"></param>
        /// <returns></returns>
        public int Game_Anchor_StartAnswer_Logic(int gameType, int fromidx, ref int turnid)
        {
            return _gamedll.Game_Anchor_StartAnswer_Data(gameType, fromidx, ref turnid);
        }

        public int Game_GameCenter_AddCoin(string uidx,  ref string  token)
        {
            return _gamedll.Game_GameCenter_AddCoin(uidx, ref token);
        }
        public int Game_GameCenter_CheckUser(long uidx, ref string token)
        {
            return _gamedll.Game_GameCenter_CheckUser(uidx, ref token);
        }
        public int Game_GameCenter_ChangeCoin(long uidx, int coin, int coinChangeType, int gameid, int gamefield,string describe,long order)
        {
            return _gamedll.Game_GameCenter_ChangeCoin(uidx, coin,coinChangeType,gameid,gamefield,describe,order);
        }
        public List<GameCenterUserInfo> GameCenterUserinfo(int uidx)
        {
            return _gamedll.GameCenterUserinfo(uidx);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public int Game_Get_AnswerNum_Logic(int useridx, ref int invitedNum)
        {
            return _gamedll.Game_Get_AnswerNum_Data(useridx, ref invitedNum);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="turnid"></param>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<Model.AnswerGame_Ranking> Get_GameRanking_Logic(int turnid, int useridx, ref int myscore)
        {
            return _gamedll.Get_GameRanking_Data(turnid, useridx, ref myscore);
        }

        /// <summary>
        /// 1v1挑战申请列表
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<Model.AnswerGame_Start> Get_SingleInviteList_Logic(int useridx)
        {
            var list = _gamedll.Get_SingleInviteList_Data(useridx);

            foreach (var item in list)
            {
                item.TimeStr = TimeHelper.DateStringFromNow(item.createTime);
            }

            return list;
        }

        /// <summary>
        /// 1v1发起邀请信息查询/创建
        /// </summary>
        /// <param name="fromidx"></param>
        /// <param name="toidx"></param>
        /// <returns></returns>
        public Model.AnswerGame_Single Get_SingleInviteInfo_Logic(int fromidx, int toidx, int anchoridx, ref int result)
        {
            result = 0;
            UserInfo u = _user.GetLiveUserInfoByIdx(toidx);

            if (u == null || u.useridx <= 0)
            {
                result = -1;
                return null;
            }
            AnswerGame_Single info = _gamedll.Get_SingleInviteInfo_Data(fromidx, toidx, anchoridx);
            info.toName = u.myname;
            info.toUserPhoto = u.smallpic;

            return info;
        }

        /// <summary>
        /// 1v1开局信息查询（查询当前1对1开局的邀请人信息(idx、头像、昵称)及被邀请人信息(idx、头像、昵称)
        /// </summary>
        /// <param name="fromidx"></param>
        /// <param name="toidx"></param>
        /// <returns></returns>
        public Model.AnswerGame_Single SingleStartGameLogic(int turnid, int useridx, int type)
        {
            return _gamedll.Get_SingleStartGame_Data(turnid, useridx, type);
        }


        public List< Model.GameRuleInfoExp >GetGameRuleInfoExp()
        { 
            return _gamedll.GetGameRuleInfoExp();
        }

        public List<Model.GameRuleInfo> GetGameBetLog(int useridx, int pageNum,int pageSize ,ref int recordCount)
        {
            DataTable dt = _gamedll.GetGameBetLog( useridx,  pageNum, pageSize,ref  recordCount);
            if (dt ==null ||dt.Rows.Count<=0)
            {
                return new List<Model.GameRuleInfo>(); 
            }
            List<Model.GameRuleInfoExp> GameRuleInfoExp = GetGameRuleInfoExp();
            List<Model.GameRuleInfo> list = new List<GameRuleInfo>();
            foreach (DataRow item in dt.Rows)
            {
                list.Add(new GameRuleInfo(item, GameRuleInfoExp));
            }
            return list;
        }
    }
}
