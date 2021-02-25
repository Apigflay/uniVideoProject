using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class GameDAL
    {
        /// <summary>
        /// 判断当前用户进入的游戏房间是否可以答题
        /// </summary>
        /// <param name="turnid"></param>
        /// <param name="useridx"></param>
        /// <param name="anchoridx"></param>
        /// <returns></returns>
        public int Game_User_AnswerStart_Data(ref int turnid, int useridx, int anchoridx, int gametype)
        {
            string procName = "Game_Answer_UserJoin";
            int result = 0;
            try
            {
                SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx),
                                SqlHelper.MakeInParam("@fromidx",SqlDbType.Int,10,anchoridx),
                                SqlHelper.MakeOutParam("@result",SqlDbType.Int,10,0),
                                SqlHelper.MakeOutParam("@usercash",SqlDbType.Int,10,0),
                                SqlHelper.MakeOutParam("@turnid",SqlDbType.Int,4,0),
                                SqlHelper.MakeInParam("@gametype",SqlDbType.Int,10,gametype),
                                };

                //DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, procName, sp);
                DbHelper.ExecuteNonQuery(procName, sp);
                result = int.Parse(sp[2].Value.ToString());
                turnid = int.Parse(sp[4].Value.ToString());
            }
            catch (Exception ex)
            {
                turnid = 0;
            }

            return result;
        }

        /// <summary>
        /// 主播准备游戏
        /// </summary>
        /// <param name="gameType"></param>
        /// <param name="fromidx"></param>
        /// <param name="turnid"></param>
        /// <returns></returns>
        public int Game_Anchor_StartAnswer_Data(int gameType, int fromidx, ref int turnid)
        {
            const string procName = "Game_Answer_Prepare";
            SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@gameType",SqlDbType.Int,10,gameType),
                                SqlHelper.MakeInParam("@fromidx",SqlDbType.Int,10,fromidx),
                                SqlHelper.MakeOutParam("@turnid",SqlDbType.Int,4,0),
                                SqlHelper.MakeOutParam("@result",SqlDbType.Int,10,0),
                                };

            //DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, procName, sp);
            DbHelper.ExecuteNonQuery(procName, sp);

            turnid = int.Parse(sp[2].Value.ToString());
            int result = int.Parse(sp[3].Value.ToString());

            return result;
        }
        public int Game_GameCenter_AddCoin(string uidx, ref string gametoken)
        {
            const string procName = "GameCenter_User_Login";
            SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@uidx",SqlDbType.VarChar,30,uidx),
                                SqlHelper.MakeInParam("@gametoken",SqlDbType.VarChar,100,gametoken),
                                SqlHelper.MakeOutParam("@result",SqlDbType.Int,10,0),
                                };

            //DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, procName, sp);
            DbHelper.ExecuteNonQuery(procName, sp);
            int result = int.Parse(sp[2].Value.ToString());

            return result;
        }
        public int Game_GameCenter_CheckUser(long uidx, ref string gametoken)
        {
            const string procName = "GameCenter_User_Login";
            SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@uidx",SqlDbType.Int,20,uidx),
                                SqlHelper.MakeInParam("@gametoken",SqlDbType.VarChar,100,gametoken),
                                SqlHelper.MakeOutParam("@result",SqlDbType.Int,10,0),
                                };

            //DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, procName, sp);
            DbHelper.ExecuteNonQuery(procName, sp);
            int result = int.Parse(sp[2].Value.ToString());

            return result;
        }

        public int Game_GameCenter_ChangeCoin(long uidx, int coin,int coinChangeType,int  gameid,int gamefield,string describe,long order)
        {
            const string procName = "GameCenter_UserCoinChange";
            SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,20,uidx),
                                SqlHelper.MakeInParam("@coin",SqlDbType.Int,20,coin),
                                SqlHelper.MakeInParam("@coinChangeType",SqlDbType.Int,20,coinChangeType),
                                SqlHelper.MakeInParam("@gameid",SqlDbType.Int,20,gameid),
                                SqlHelper.MakeInParam("@gamefield",SqlDbType.Int,20,gamefield),
                                SqlHelper.MakeOutParam("@result",SqlDbType.Int,10,0),
                                SqlHelper.MakeInParam("@order",SqlDbType.BigInt,30,order),
                                SqlHelper.MakeInParam("@describe",SqlDbType.NVarChar,20,describe)
                                };

            //DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, procName, sp);
            DbHelper.ExecuteNonQuery(procName, sp);
            int result = int.Parse(sp[5].Value.ToString());

            return result;
        }
        public List<Model.GameCenterUserInfo> GameCenterUserinfo(int useridx)
        {
            const string procName = "GameCenter_UserInfo";

            try
            {
                SqlParameter[] sp = {
                     SqlHelper.MakeInParam("@useridx",SqlDbType.Int,4,useridx)
                };
                DataTable dt = DbHelper.GetTable(procName, sp);
                return RFHelper<Model.GameCenterUserInfo>.ConvertToList(dt);
            }
            catch (Exception ex)
            {

                return null;
            }

            //return DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, procName, sp);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public int Game_Get_AnswerNum_Data(int useridx, ref int invitedNum)
        {
            const string procName = "Game_Get_AnswerNum";
            invitedNum = 0;
            SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx),
                                SqlHelper.MakeOutParam("@remainNum",SqlDbType.Int,4,0),
                                SqlHelper.MakeOutParam("@invitedNum",SqlDbType.Int,4,0),
                                };

            DbHelper.ExecuteNonQuery(procName, sp);
            invitedNum = int.Parse(sp[2].Value.ToString());
            int result = int.Parse(sp[1].Value.ToString());

            return result;
        }

        /// <summary>
        /// 获取排行榜
        /// </summary>
        /// <param name="turnid"></param>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<Model.AnswerGame_Ranking> Get_GameRanking_Data(int turnid, int useridx, ref int myscore)
        {
            const string procName = "Game_Get_RewardRanking";

            SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@turnid",SqlDbType.Int,10,turnid),
                                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx),
                                SqlHelper.MakeInParam("@myscore",SqlDbType.Int,10,0)
                                };
            DataTable dt = DbHelper.GetTable(procName, sp);
            myscore = int.Parse(sp[2].Value.ToString());

            return RFHelper<Model.AnswerGame_Ranking>.ConvertToList(dt);
        }

        /// <summary>
        /// 1v1挑战申请列表
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<Model.AnswerGame_Start> Get_SingleInviteList_Data(int useridx)
        {
            const string procName = "Game_Get_SingleInviteList";

            SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx)
                                };
            DataTable dt = DbHelper.GetTable(procName, sp);
            return RFHelper<Model.AnswerGame_Start>.ConvertToList(dt);
        }

        /// <summary>
        /// 1v1发起邀请信息查询/创建
        /// </summary>
        /// <param name="fromidx"></param>
        /// <param name="toidx"></param>
        /// <returns></returns>
        public Model.AnswerGame_Single Get_SingleInviteInfo_Data(int fromidx, int toidx, int anchoridx)
        {
            const string procName = "Game_Answer_Prepare_1v1";

            SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@fromidx",SqlDbType.Int,10,fromidx),
                                SqlHelper.MakeInParam("@toidx",SqlDbType.Int,10,toidx),
                                //SqlHelper.MakeInParam("@roomid",SqlDbType.Int,10,0),
                                SqlHelper.MakeInParam("@anchoridx",SqlDbType.Int,10,anchoridx)
                                };
            DataTable dt = DbHelper.GetTable(procName, sp);

            return RFHelper<Model.AnswerGame_Single>.GetEntity(dt);
        }

        /// <summary>
        /// 1v1开局信息查询
        /// </summary>
        /// <param name="fromidx"></param>
        /// <param name="toidx"></param>
        /// <returns></returns>
        public Model.AnswerGame_Single Get_SingleStartGame_Data(int turnid, int useridx, int type)
        {
            const string procName = "Game_Answer_SingleStartGame_1v1";

            SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@turnid",SqlDbType.Int,10,turnid),
                                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx),
                                SqlHelper.MakeInParam("@type",SqlDbType.Int,10,type)
                                };
            DataTable dt = DbHelper.GetTable(procName, sp);

            return RFHelper<Model.AnswerGame_Single>.GetEntity(dt);
        }

        /// <summary>
        /// 获取游戏投注规则详细说明
        /// </summary>
        /// <returns></returns>
        public List<Model.GameRuleInfoExp> GetGameRuleInfoExp()
        {
            const string procName = "I_getGameRuleInfoExp";

            SqlParameter[] sp = {
                                };
            DataTable dt = DbHelper.GetTable(procName, sp);

            return RFHelper<Model.GameRuleInfoExp>.ConvertToList(dt);
        }

        public DataTable GetGameBetLog(int useridx, int pageNum, int pageSize, ref int recordCount)
        {

            const string procName = "cp_ht_userBetLog";

            SqlParameter[] sp = {
                                    SqlHelper.MakeInParam("@userid",SqlDbType.Int,4,useridx),
                                     SqlHelper.MakeInParam("@pageNum",SqlDbType.Int,4,pageNum),
                                     SqlHelper.MakeInParam("@pageSize",SqlDbType.Int,4,pageSize),
                                      SqlHelper.MakeOutParam("@recordCount",SqlDbType.Int,4,recordCount),
                                };
            DataTable dt = DbHelper.GetTable(DbHelper.lawoGame_124, procName, sp);
            recordCount = int.Parse((sp[3].Value ?? "0").ToString());

            if (dt != null && dt.Rows.Count == 0)
            {
                dt = null;

            }

            return dt;
        }
    }
}
