using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// File: 用户信息类
    /// </summary>
    /// <remarks>
    /// <para>Author: "赵锐"</para>
    /// <para>Date: "2016年13月19日"</para>
    /// </remarks>
    public class UserInfoDAL
    {
        private DBContext db = new DBContext();

        #region 用户信息

        /// <summary>
        ///    type=0 返回 0存在 1不存在 type=1 返回0未插入 1插入 type=2 返回1 成功 0失败
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="type"> 0 验证是否有上传信息  --1 上传 信息 2--审核信息</param>
        /// <returns></returns>
        public int AuthenticationUpload(int useridx, int type,int state=0,string videourl="")
        {
            UserInfo u = new UserInfo();
            var sql = "[Live_GetAuthentication]";
            try
            {
                SqlParameter[] p ={
                              SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx),
                              SqlHelper.MakeInParam("@type",SqlDbType.Int,10,type),
                              SqlHelper.MakeOutParam("@Ret",SqlDbType.Int,10,0),
                              SqlHelper.MakeInParam("@state",SqlDbType.Int,10,state),
                              SqlHelper.MakeInParam("@videourl",SqlDbType.NVarChar,256,videourl)
                          };
                DataTable dt = DbHelper.GetDataTable(sql, p);
                return (int)p[2].Value;
            }
            catch
            {
                return 2;
            }
            ;
        }
        public string AuthenticationVideo(int useridx)
        {
            UserInfo u = new UserInfo();
            var sql = "[Live_GetAuthenticationVideo]";
            try
            {
                SqlParameter[] p ={
                              SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx),
                              SqlHelper.MakeOutParam("@Ret",SqlDbType.NVarChar,256,0),

                          };
                DataTable dt = DbHelper.GetDataTable(sql, p);
                return p[1].Value.ToString();
            }
            catch(Exception ex)
            {
                return "";
            }
    ;
        }
        /// <summary>
        /// 根据useridx获取用户单个信息
        /// </summary>
        /// <param name="uidx"></param>
        /// <returns></returns>
        public UserInfo GetLiveUserInfoByIdx(int uidx)
        {
            UserInfo u = new UserInfo();
            var sql = "[Live_GetUserInfoByIdx]";

            SqlParameter[] p ={
                              SqlHelper.MakeInParam("@userIdx",SqlDbType.Int,10,uidx)
                          };
            DataTable dt = DbHelper.GetDataTable(sql, p);

            return RFHelper<UserInfo>.GetEntity(dt);
        }
        /// <summary>
        /// 我的用户信息（详细用户信息）
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public MyUserInfo getUserInfo(int useridx)
        {
            var p = new DynamicParameters();
            p.Add("@useridx", useridx);

            return db.Write(
                c => c.Query<MyUserInfo>("Live_Select_Userinfo", p).SingleOrDefault()
                );
        }
        /// <summary>
        /// 获取用户其他详细信息
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public OtherInfo GetOtherUserInfo_Data(int useridx)
        {
            var sql = "[Live_Get_OtherUserInfo]";
            SqlParameter[] p ={
                              SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx)
                          };
            DataTable dt = DbHelper.GetDataTable(sql, p);

            return RFHelper<OtherInfo>.GetEntity(dt);
        }

        /// <summary>
        /// 根据用户短号获取长号
        /// </summary>
        /// <param name="shortidx"></param>
        /// <returns></returns>
        public int GetUseridxByshortidx_Data(int shortidx)
        {
            var sql = "[f_getuseridxbyshortidx]";
            int useridx = 0;
            SqlParameter[] p ={
                              SqlHelper.MakeInParam("@shortidx",SqlDbType.Int,10,shortidx),
                              SqlHelper.MakeOutParam("@useridx",SqlDbType.Int,10,0)
                          };
            DbHelper.ExecuteNonQuery(sql, p);
            useridx = (int)p[1].Value;
            return useridx;
        }

        /// <summary>
        /// 根据用户长号获取短号
        /// </summary>
        /// <param name="shortidx"></param>
        /// <returns></returns>
        public int Get_shortidxByUseridx_Data(int shortidx)
        {
            var sql = "[f_getshortidxbyuseridx]";
            int useridx = 0;
            SqlParameter[] p ={
                              SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,shortidx),
                              SqlHelper.MakeOutParam("@shortidx",SqlDbType.Int,10,0)
                          };
            DbHelper.ExecuteNonQuery(sql, p);
            useridx = (int)p[1].Value;
            return useridx;
        }

        #endregion

        #region 主播相关

        /// <summary>
        /// 获取主播等级信息
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public AnchorInfo Get_Anchor_Level_Info_Data(int useridx)
        {
            var sql = "[Live_Get_AnchorLevel_Info]";
            SqlParameter[] p ={
                              SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx)
                          };
            DataTable dt = DbHelper.GetDataTable(sql, p);

            return RFHelper<AnchorInfo>.GetEntity(dt);
        }

        /// <summary>
        /// 主播当前任务信息
        /// </summary>
        /// <param name="dataAction"></param>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public AnchorTask Get_AnchorTask_Info_Data(int dataAction, int useridx)
        {
            var sql = "[Live_Get_Anchor_Task]";
            SqlParameter[] p ={
                              SqlHelper.MakeInParam("@dataAction",SqlDbType.Int,10,dataAction),
                              SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx)
                          };
            DataTable dt = DbHelper.GetDataTable(sql, p);

            return RFHelper<AnchorTask>.GetEntity(dt);
        }

        /// <summary>
        /// 主播当前任务限制查询
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<AnchorTaskLimit> Get_AnchorTaskLimit_Info_Data(int useridx)
        {
            var sql = "[Live_Get_Anchor_Task]";
            SqlParameter[] p ={
                              SqlHelper.MakeInParam("@dataAction",SqlDbType.Int,10,2),
                              SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx)
                          };
            DataTable dt = DbHelper.GetDataTable(sql, p);

            return RFHelper<AnchorTaskLimit>.ConvertToList(dt);
        }

        /// <summary>
        /// 获取我的家族信息
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public Family Get_MyFamilyInfo_data(int dataType, int useridx)
        {
            const string sql = "Live_Get_FamilyInfo";
            SqlParameter[] p ={
                                SqlHelper.MakeInParam("@dataType",SqlDbType.Int,10,dataType),
                                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx)
                          };
            DataTable dt = DbHelper.GetDataTable(sql, p);
            return RFHelper<Family>.GetEntity(dt);
        }

        /// <summary>
        /// 判断是否是主播
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public int Game_IsAnchor_Data(int useridx)
        {
            SqlParameter[] sqlParam ={
                                      SqlHelper.MakeInParam("@useridx",SqlDbType.Int,4,useridx),
                                      SqlHelper.MakeOutParam("@result",SqlDbType.Int,4,0),
                                      };
            DbHelper.ExecuteNonQuery("Game_ISAnchor", sqlParam);

            return (int)sqlParam[1].Value;
        }

        /// <summary>
        /// 获取主播时长
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="roomid"></param>
        /// <param name="stardate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        //public List<ASongerInfo> GetUserLiveingTime(int useridx, int roomid, string stardate, string enddate)
        //{
        //    List<ASongerInfo> list = new List<ASongerInfo>();

        //    const string sql = "[AS_Select_UserTime]";
        //    using (IDbConnection conn = DbHelper.OpenConnection())
        //    {
        //        var p = new DynamicParameters();
        //        p.Add("@useridx", useridx);
        //        p.Add("@roomid", roomid);
        //        p.Add("@stardate", stardate);
        //        p.Add("@enddate", enddate);

        //        list = conn.Query<ASongerInfo>(sql, p, commandType: CommandType.StoredProcedure).ToList();
        //    }
        //    return list;
        //}

        #endregion

        #region 头像，昵称，签名修改

        /// <summary>
        /// 修改用户头像 Live
        /// </summary>
        /// <param name="userIdx"></param>
        /// <param name="nickName"></param>
        /// <param name="signatrues"></param>
        /// <returns></returns>
        public int UpdateUserPhoto(int userIdx, string smallpic, string bigpic)
        {
            int ret = 0;
            SqlParameter[] p = {
                               SqlHelper.MakeInParam("@useridx",SqlDbType.Int,20,userIdx),
                               SqlHelper.MakeInParam("@smallpic",SqlDbType.VarChar,200,smallpic),
                               SqlHelper.MakeInParam("@bigpic",SqlDbType.VarChar,200,bigpic)
                               };
            ret = DbHelper.ExecuteNonQuery("Live_UpdateUserPhoto", p);

            return ret;
        }
        /// <summary>
        /// 主播头像审核
        /// </summary>
        /// <param name="userIdx"></param>
        /// <param name="smallpic"></param>
        /// <param name="bigpic"></param>
        /// <returns></returns>
        public int UserPhotoAudit_Data(int type, int useridx, string nickName, string photo, string bigpic)
        {
            int ret = 0;
            SqlParameter[] p = {
                                   SqlHelper.MakeInParam("@type",SqlDbType.Int,4,type),
                                   SqlHelper.MakeInParam("@useridx",SqlDbType.Int,20,useridx),
                                   SqlHelper.MakeInParam("@photo",SqlDbType.VarChar,200,photo),
                                   SqlHelper.MakeInParam("@bigpic",SqlDbType.VarChar,200,bigpic),
                                   SqlHelper.MakeOutParam("@ret",SqlDbType.Int,4,0),
                                   SqlHelper.MakeInParam("@nickName",SqlDbType.VarChar,20,nickName)
                               };
            SqlHelper.ExecuteNonQuery(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, "Live_UserPhoto_Audit", p);
            ret = (int)p[4].Value;
            return ret;
        }
        public int updateNickName_Data(int useridx, string nickName)
        {
            int ret = 0;
            try
            {
                SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx),
                                SqlHelper.MakeInParam("@nickName",SqlDbType.NVarChar,15,nickName),
                                SqlHelper.MakeOutParam("@ret",SqlDbType.Int,4,0)
                                };

                SqlHelper.ExecuteNonQuery(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, "[f_changeNickName]", sp);
                ret = (int)sp[2].Value;
            }
            catch (Exception ex)
            {
                ret = 0;
            }
            return ret;
        }

        /// <summary>
        /// 修改签名
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        public int updateSignature_Data(int useridx, string signature)
        {
            SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx),
                                SqlHelper.MakeInParam("@Signatures",SqlDbType.NVarChar,15,signature),
                                };

            return SqlHelper.ExecuteNonQuery(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, "[f_changeSignatures]", sp);
        }
        #endregion

        /// <summary>
        /// 用户封号操作
        /// </summary>
        /// <param name="userIdx"></param>
        /// <param name="limitTime"></param>
        /// <param name="type">0:添加 1:删除</param>
        /// <param name="adminUser">操作者</param>
        /// <returns></returns>
        public int UserBlackInsert(int userIdx, int roomid, DateTime limitTime, string content)
        {
            string sql = "AS_UserBlacklist_Insert";

            SqlParameter[] ps = {
                                SqlHelper.MakeInParam("@type",SqlDbType.Int,4,0),
                                SqlHelper.MakeInParam("@roomid",SqlDbType.Int,4,roomid),
                                SqlHelper.MakeInParam("@idx",SqlDbType.Int,4,userIdx),
                                SqlHelper.MakeInParam("@limitTime",SqlDbType.DateTime,30,limitTime),
                                SqlHelper.MakeInParam("@admin",SqlDbType.VarChar,10,"系统"),
                                SqlHelper.MakeInParam("@content",SqlDbType.NVarChar,200,content)
                                };

            return SqlHelper.ExecuteNonQuery(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, sql, ps);
        }

        /// <summary>
        /// 获取广告列表用户
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public DataTable GetAdUser_Data(string content)
        {
            SqlParameter[] p = {
                               SqlHelper.MakeInParam("@content",SqlDbType.NVarChar,20,content)
                               };
            return SqlHelper.ExecuteDataTable(DbHelper.conn63_MobileMiaobo, CommandType.StoredProcedure, "AS_Get_AdUser", p);
        }

        /// <summary>
        /// 电子签约记录
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public int ElectronicSigningRecord_Data(int useridx)
        {
            SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx)
                                };

            return DbHelper.ExecuteNonQuery("[Live_Insert_eSigning]", sp);
        }
        /// <summary>
        /// 获取用户token
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public string User_GetUserToken(int useridx, int tokenType)
        {
            SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@idx",SqlDbType.Int,10,useridx),
                                SqlHelper.MakeInParam("@tokenType",SqlDbType.Int,10,tokenType),
                                SqlHelper.MakeOutParam("@token",SqlDbType.VarChar,256,"")
                                };
            DbHelper.ExecuteNonQuery("[getUserToken]", sp);
            string token = sp[2].Value.ToString();
            return token;
        }
        /// <summary>
        /// 获取用户总 币
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public Int64 GetUserCashByIdx(int useridx)
        {
            SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@idx",SqlDbType.Int,10,useridx),
                                SqlHelper.MakeOutParam("@coin",SqlDbType.Int,50,0)
                                };
            DbHelper.ExecuteNonQuery("[getUserCoin]", sp);

            return Convert.ToInt64(sp[1].Value.ToString());
        }

        #region 相册功能

        /// <summary>
        /// 是否有权限上传相册
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="albumtype"></param>
        /// <returns></returns>
        public int UploadAlbum_Power_Data(int useridx, int albumtype, ref int groupid)
        {
            SqlParameter[] sp = {
                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,4,useridx),
                SqlHelper.MakeInParam("@albumtype",SqlDbType.Int,4,albumtype),
                SqlHelper.MakeOutParam("@groupid",SqlDbType.Int,4,0),
            };
            object obj = DbHelper.ExecuteScalar("Live_uploadAlbum_Authority", sp);
            //object obj = DbHelper.ExecuteScalar(DbHelper.conn112_Mobile, "Live_uploadAlbum_Authority", sp);

            groupid = sp[2].Value is int ? (int)sp[2].Value : 1;
            return obj == null ? 0 : (int)obj;
        }

        /// <summary>
        /// 上传我的相册
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="albumType"></param>
        /// <param name="imgURL"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public int Insert_MyAlbum_Data(int useridx, Album album)
        {
            SqlParameter[] sp = {
                                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx),
                                SqlHelper.MakeInParam("@albumType",SqlDbType.Int,4,album.albumType),
                                SqlHelper.MakeInParam("@imgURL",SqlDbType.VarChar,200,album.imgURL)
                                };

            return DbHelper.ExecuteNonQuery("[Live_insert_MyAlbum]", sp);
            //return DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, "[Live_insert_MyAlbum]", sp);
        }

        /// <summary>
        /// 相册查询
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="touseridx"></param>
        /// <param name="albumType"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="totalIncome"></param>
        /// <returns></returns>
        public List<Album> Get_CardAlbumList_Data(int useridx, int touseridx, ref int PrivatePhotosNum)
        {
            SqlParameter[] sp = {
                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx),
                SqlHelper.MakeInParam("@touseridx",SqlDbType.Int,10,touseridx),
                SqlHelper.MakeOutParam("@privatePhotosNum",SqlDbType.Int,4,0),
            };

            DataTable dt = DbHelper.GetTable("Live_Get_CardAlbumBy_uidx", sp);
            //DataTable dt = DbHelper.GetTable(DbHelper.conn112_Mobile, "Live_Get_CardAlbumBy_uidx", sp);

            PrivatePhotosNum = (int)sp[2].Value;
            return RFHelper<Album>.ConvertToList(dt);
        }

        /// <summary>
        /// 相册查询
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="touseridx"></param>
        /// <param name="albumType"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="totalIncome"></param>
        /// <returns></returns>
        public List<Album> Get_AlbumList_Data(int useridx, int touseridx, int albumType, int page, int pageSize, ref int totalCount, ref int totalIncome)
        {
            SqlParameter[] sp = {
                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx),
                SqlHelper.MakeInParam("@touseridx",SqlDbType.Int,10,touseridx),
                SqlHelper.MakeInParam("@albumType",SqlDbType.Int,10,albumType),
                SqlHelper.MakeInParam("@pageIndex",SqlDbType.Int,4,page),
                SqlHelper.MakeInParam("@pageSize",SqlDbType.Int,4,pageSize),
                SqlHelper.MakeOutParam("@totalCount",SqlDbType.Int,4,0),
                SqlHelper.MakeOutParam("@totalIncome",SqlDbType.Int,4,0),
            };

            DataTable dt = DbHelper.GetTable("Live_Get_AlbumListBy_uidx", sp);
            //DataTable dt = DbHelper.GetTable(DbHelper.conn112_Mobile, "Live_Get_AlbumListBy_uidx", sp);

            totalCount = (int)sp[5].Value;
            totalIncome = (int)sp[6].Value;

            return RFHelper<Album>.ConvertToList(dt);
        }/// <summary>  
         /// 获取相册   类型0不带当前用户是否阅后即焚字段
         /// </summary>
         /// <param name="useridx"></param>
         /// <param name="page"></param>
         /// <param name="pageIndex"></param>
         /// <returns></returns>
        public List<AlbumNew> Get_AlbumList(int useridx,int page, int pageIndex,ref  int count,int type=0,int operuseridx=0)
        {
            SqlParameter[] sp = {
                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx),
                SqlHelper.MakeInParam("@page",SqlDbType.Int,3,page),
                SqlHelper.MakeInParam("@pageIndex",SqlDbType.Int,4,pageIndex),
                SqlHelper.MakeOutParam("@count",SqlDbType.Int,4,0),
                SqlHelper.MakeInParam("@type",SqlDbType.Int,4,type),
                SqlHelper.MakeInParam("@operuseridx",SqlDbType.Int,8,operuseridx),
            };
            DataTable dt = DbHelper.GetTable("Live_Get_AlbumList", sp);
            count = (int)sp[3].Value;
            return RFHelper<AlbumNew>.ConvertToList(dt);
        }
        /// <summary>
        /// 图片位置互换
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="albumid"></param>
        /// <param name="nowgroupid"> 当前位置</param>
        /// <param name="togroupid">目标位置</param>
        /// <returns></returns>
        public int Get_AlbumListChangeposition(int useridx, int albumid, int nowgroupid, int togroupid )
        {
            SqlParameter[] sp = {
                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,10,useridx),
                SqlHelper.MakeInParam("@albumid",SqlDbType.Int,8,albumid),
                SqlHelper.MakeInParam("@nowgroupid",SqlDbType.Int,4,nowgroupid),
                SqlHelper.MakeInParam("@togroupid",SqlDbType.Int,4,togroupid),
                SqlHelper.MakeOutParam("@ret",SqlDbType.Int,8,0),
            };
            DbHelper.ExecuteNonQuery("[Live_Get_AlbumChangeposition]", sp);
            int ret = (int)sp[4].Value;
            return ret;
        }

        /// <summary>
        /// 删除相册
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="albumid"></param>
        /// <returns></returns>
        public int Delete_MyAlbum_Data(int useridx, int albumid)
        {
            SqlParameter[] sp = {
                SqlHelper.MakeInParam("@deltype",SqlDbType.Int,4,1),
                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,4,useridx),
                SqlHelper.MakeInParam("@albumid",SqlDbType.Int,4,albumid),
            };

            return DbHelper.ExecuteNonQuery("Live_delete_MyAlbum", sp);
            //return DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, "Live_delete_MyAlbum", sp);
        }

        /// <summary>
        /// 举报相册
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="albumid"></param>
        /// <returns></returns>
        public int Report_MyAlbum_Data(int useridx, int albumid)
        {
            SqlParameter[] sp = {
                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,4,useridx),
                SqlHelper.MakeInParam("@albumid",SqlDbType.Int,4,albumid),
            };

            return DbHelper.ExecuteNonQuery("Live_Report_Album", sp);
            //return DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, "Live_Report_Album", sp);
        }

        /// <summary>
        /// 分享统计
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="albumid"></param>
        /// <returns></returns>
        public int Share_MyAlbum_Data(int useridx, int albumid)
        {
            const string procName = "Live_Share_Album";
            SqlParameter[] sp = {
                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,4,useridx),
                SqlHelper.MakeInParam("@albumid",SqlDbType.Int,4,albumid),
            };

            return DbHelper.ExecuteNonQuery(procName, sp);
            //return DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, procName, sp);
        }


        #endregion

        #region 老挝用户信息

        /// <summary>
        /// 老挝用户信息查询
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="albumid"></param>
        /// <returns></returns>
        public List<UserInvitationInfo> UserRealNameInfoSet(int useridx)
        {
            const string procName = "I_UserRealNameInfoSet";

            try
            {
                SqlParameter[] sp = {
                     SqlHelper.MakeInParam("@useridx",SqlDbType.Int,4,useridx)
                };
                DataTable dt = DbHelper.GetTable(procName, sp);
                return RFHelper<UserInvitationInfo>.ConvertToList(dt);
            }
            catch (Exception)
            {

                return null;
            }

            //return DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, procName, sp);
        }
        /// <summary>
        ///  面具公园用户信息
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<UserInfomation> UserRealNameInfo(int useridx)
        {
            const string procName = "I_UserRealNameInfo";

            try
            {
                SqlParameter[] sp = {
                     SqlHelper.MakeInParam("@useridx",SqlDbType.Int,4,useridx)
                };
                DataTable dt = DbHelper.GetTable(procName, sp);
                return RFHelper<UserInfomation>.ConvertToList(dt);
            }
            catch (Exception ex)
            {

                return null;
            }

            //return DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, procName, sp);
        }
        /// <summary>
        /// 我的信息
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<UserMynameInfo> UserMyNameInfo(int useridx)
        {
            const string procName = "UserMyNameInfo";

            try
            {
                SqlParameter[] sp = {
                     SqlHelper.MakeInParam("@useridx",SqlDbType.Int,4,useridx)
                };
                DataTable dt = DbHelper.GetTable(procName, sp);
                return RFHelper<UserMynameInfo>.ConvertToList(dt);
            }
            catch (Exception ex)
            { 

                return null;
            }

            //return DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, procName, sp);
        }
        /// <summary>
        ///  面具公园用户评价
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<Userevaluate> userevaluates(int useridx)
        {
            const string procName = "[I_userevaluate]";

            try
            {
                SqlParameter[] sp = {
                     SqlHelper.MakeInParam("@useridx",SqlDbType.Int,4,useridx)
                };
                DataTable dt = DbHelper.GetTable(procName, sp);
                return RFHelper<Userevaluate>.ConvertToList(dt);
            }
            catch (Exception)
            {

                return null;
            }

            //return DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, procName, sp);
        }
        /// <summary>
        /// 查看是否是粉丝  1是粉丝 0不是粉丝
        /// </summary>
        /// <param name="useridx">当前用户</param>
        /// <param name="tuseridx">被查看用户</param>
        /// <returns></returns>
        public int isFans(int useridx,int tuseridx)
        {
            const string procName = "[I_userisFans]";

            try
            {
                SqlParameter[] sp = {
                     SqlHelper.MakeInParam("@useridx",SqlDbType.Int,8,useridx),
                     SqlHelper.MakeInParam("@tuseridx",SqlDbType.Int,8,tuseridx),
                     SqlHelper.MakeOutParam("@Ret",SqlDbType.Int,4,0)
                };
                DataTable dt = DbHelper.GetTable(procName, sp);
                return (int)sp[2].Value;
            }
            catch (Exception)
            {

                return 0;
            }

            //return DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, procName, sp);
        }
        public int isBlack(int useridx, int tuseridx)
        {
            const string procName = "[I_userisBlack]";

            try
            {
                SqlParameter[] sp = {
                     SqlHelper.MakeInParam("@useridx",SqlDbType.Int,8,useridx),
                     SqlHelper.MakeInParam("@tuseridx",SqlDbType.Int,8,tuseridx),
                     SqlHelper.MakeOutParam("@Ret",SqlDbType.Int,4,0)
                };
                DataTable dt = DbHelper.GetTable(procName, sp);
                return (int)sp[2].Value;
            }
            catch (Exception)
            {

                return 0;
            }

            //return DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, procName, sp);
        }
        
        /// <summary>
        /// 个人信息获取约会期望职业约会节目数据
        /// </summary>
        /// <returns></returns>
        public List<UserDataInfo> UserDataInfo()
        {
            const string procName = "I_UserDataInfo";

            try
            {
                SqlParameter[] sp = {

                };
                DataTable dt = DbHelper.GetTable(procName, sp);
                return RFHelper<UserDataInfo>.ConvertToList(dt);
            }
            catch (Exception ex)
            {

                return null;
            }

            //return DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, procName, sp);
        }
        /// <summary>
        /// 添加评价
        /// </summary>
        /// <param name="useridx"> 当前用户 </param>
        /// <param name="tuseridx"> 目标用户</param>
        /// <param name="evaluates">标签</param>
        /// <returns></returns>
        public int Getuserevaluates(int useridx, int tuseridx, string evaluates)
        {
            const string procName = "I_usere_Insertvaluate";
            try
            {
                SqlParameter[] sp = {
                    SqlHelper.MakeInParam("@useridx",SqlDbType.Int,8,useridx),
                    SqlHelper.MakeInParam("@tuseridx",SqlDbType.Int,8,tuseridx),
                    SqlHelper.MakeInParam("@evaluates",SqlDbType.NVarChar,20,evaluates),
                    SqlHelper.MakeOutParam("@Ret",SqlDbType.Int,4,0)
                };
                DbHelper.ExecuteNonQuery(procName, sp);
                int iRet = (int)sp[3].Value;
                return iRet;
            }
            catch (Exception ex)
            {
                return 0;
            }

            //return DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, procName, sp);
        }
        /// <summary>
        /// 获取评价列表
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="tuseridx"></param>
        /// <param name="evaluates"></param>
        /// <returns></returns>
        public List<Userevaluates> GetuserevaluateList(int useridx, int page, int pagecount)
        {
            const string procName = "[I_usere_InsertvaluateInfo]";
            try
            {
                SqlParameter[] sp = {
                    SqlHelper.MakeInParam("@useridx",SqlDbType.Int,8,useridx),
                    SqlHelper.MakeInParam("@page",SqlDbType.Int,3,page),
                    SqlHelper.MakeInParam("@pagecount",SqlDbType.Int,3,pagecount)

                };
                DataTable dt = DbHelper.GetTable(procName, sp);
                return RFHelper<Userevaluates>.ConvertToList(dt);
            }
            catch (Exception ex)
            {
                return null;
            }

            //return DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, procName, sp);
        }
        /// <summary>
        /// 喜欢列表
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="page"></param>
        /// <param name="pagecount"></param>
        /// <returns></returns>
        public List<UserListInfo> GetLikeList(int useridx, int page, int pagecount)
        {
            const string procName = "[I_user_GetLikeList]";
            try
            {
                SqlParameter[] sp = {
                    SqlHelper.MakeInParam("@useridx",SqlDbType.Int,8,useridx),
                    SqlHelper.MakeInParam("@page",SqlDbType.Int,3,page),
                    SqlHelper.MakeInParam("@pagecount",SqlDbType.Int,3,pagecount)

                };
                DataTable dt = DbHelper.GetTable(procName, sp);
                return RFHelper<UserListInfo>.ConvertToList(dt);
            }
            catch (Exception ex)
            {
                return null;
            }

            //return DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, procName, sp);
        }
        /// <summary>
        /// 收益列表
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="page"></param>
        /// <param name="pagecount"></param>
        /// <returns></returns>
        public List<UserListInfo> GetProfitList(int useridx, int page, int pagecount)
        {
            const string procName = "[I_user_GetProfitList]";
            try
            {
                SqlParameter[] sp = {
                    SqlHelper.MakeInParam("@useridx",SqlDbType.Int,8,useridx),
                    SqlHelper.MakeInParam("@page",SqlDbType.Int,3,page),
                    SqlHelper.MakeInParam("@pagecount",SqlDbType.Int,3,pagecount)

                };
                DataTable dt = DbHelper.GetTable(procName, sp);
                return RFHelper<UserListInfo>.ConvertToList(dt);
            }
            catch (Exception ex)
            {
                return null;
            }

            //return DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, procName, sp);
        }
        /// <summary>
        /// 获取黑名单列表
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="page"></param>
        /// <param name="pagecount"></param>
        /// <returns></returns>
        public List<UserListInfo> GetBlackList(int useridx, int page, int pagecount)
        {
            const string procName = "[I_user_GetBlackList]";
            try
            {
                SqlParameter[] sp = {
                    SqlHelper.MakeInParam("@useridx",SqlDbType.Int,8,useridx),
                    SqlHelper.MakeInParam("@page",SqlDbType.Int,3,page),
                    SqlHelper.MakeInParam("@pagecount",SqlDbType.Int,3,pagecount)

                };
                DataTable dt = DbHelper.GetTable(procName, sp);
                return RFHelper<UserListInfo>.ConvertToList(dt);
            }
            catch (Exception ex)
            {
                return null;
            }

            //return DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, procName, sp);
        }
        public long CoinVip(int useridx, ref string date)
        {
            const string procName = "[f_GetUserCashandVipByUserIdx]";
            try
            {
                SqlParameter[] sp = {
                    SqlHelper.MakeInParam("@useridx",SqlDbType.Int,8,useridx),
                    SqlHelper.MakeOutParam("@cash",SqlDbType.BigInt,8,0),
                    SqlHelper.MakeOutParam("@date",SqlDbType.DateTime,20,0)

                };
                DataTable dt = DbHelper.GetTable(procName, sp);
                date = sp[2].Value.ToString();
                return (long)sp[1].Value;
            }
            catch (Exception ex)
            {
                return 0;
            }

            //return DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, procName, sp);
        }
   


        /// <summary>
        /// 我的代理明细周结束明细
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public List<myUserAgentInfo> myUserAgentInfoSet(int useridx)
        {
            const string procName = "I_myUserAgentInfoSet";

            try
            {
                SqlParameter[] sp = {
                     SqlHelper.MakeInParam("@useridx",SqlDbType.Int,4,useridx)
                };
                DataTable dt = DbHelper.GetTable(procName, sp);
                return RFHelper<myUserAgentInfo>.ConvertToList(dt);
            }
            catch (Exception)
            {

                return null;
            }

            //return DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, procName, sp);
        }

        /// <summary>
        /// 用户登陆后产生登陆token
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public int UserLoginTokenInset(int userIdx, string token)
        {

            const string procName = "userLoginTokenInsert";
            try
            {
                SqlParameter[] sp = {
                         SqlHelper.MakeInParam("@userIdx",SqlDbType.Int,4,userIdx),
                         SqlHelper.MakeInParam("@token",SqlDbType.VarChar,100,token)

                };
                int iRet = DbHelper.ExecuteNonQuery(procName, sp);
                return iRet;
            }
            catch (Exception)
            {
                return 0;
            }

        }
        /// <summary>
        ///面具公园用户信息查询
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="albumid"></param>
        /// <returns></returns>
        public List<UserInvitationInfo> phoneNumGetUserRealNameInfoSet(string phoneNum)
        {
            const string procName = "I_phoneNumGetUserRealNameInfoSet";

            try
            {
                SqlParameter[] sp = {
                     SqlHelper.MakeInParam("@phoneNum",SqlDbType.VarChar,20
                     ,phoneNum)
                };
                DataTable dt = DbHelper.GetTable(procName, sp);
                return RFHelper<UserInvitationInfo>.ConvertToList(dt);
            }
            catch (Exception)
            {

                return null;
            }

            //return DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, procName, sp);
        }
        /// <summary>
        /// 面具公园用户信息修改
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public int UserRealNameInfoUpdate(UserInfomation Model)
        {
            
            try
            {
                
                const string procName = "I_UserRealNameInfoUpdate";
                SqlParameter[] sp = {
              SqlHelper.MakeInParam("@userIdx",SqlDbType.Int,10,Model.userIdx),
              //SqlHelper.MakeInParam("@Myname",SqlDbType.NVarChar,50,Model.Myname),
              SqlHelper.MakeInParam("@Age",SqlDbType.Int,4,Model.Age),
              SqlHelper.MakeInParam("@Province",SqlDbType.NVarChar,50,Model.Province),
              SqlHelper.MakeInParam("@City",SqlDbType.NVarChar,50,Model.City),
              SqlHelper.MakeInParam("@Job",SqlDbType.NVarChar,50,Model.Job),
              SqlHelper.MakeInParam("@AppointmentRage",SqlDbType.NVarChar,200,Model.AppointmentRage),
              SqlHelper.MakeInParam("@AppointmentProgram",SqlDbType.NVarChar,200,Model.AppointmentProgram),
              SqlHelper.MakeInParam("@AppointmentExpect",SqlDbType.NVarChar,200,Model.AppointmentExpect),
              SqlHelper.MakeInParam("@MonthlyIncome",SqlDbType.NVarChar,50,Model.MonthlyIncome),
              SqlHelper.MakeInParam("@Shape",SqlDbType.NVarChar,20,Model.Shape),
              SqlHelper.MakeInParam("@WeChat",SqlDbType.NVarChar,50,Model.WeChat),
              SqlHelper.MakeInParam("@QQ",SqlDbType.NVarChar,50,Model.QQ),
              SqlHelper.MakeInParam("@Height",SqlDbType.Int,4,Model.Height),
              SqlHelper.MakeInParam("@Weight",SqlDbType.Int,4,Model.Weight),
              SqlHelper.MakeInParam("@Bust",SqlDbType.NVarChar,20,Model.Bust),
              SqlHelper.MakeInParam("@Introduction",SqlDbType.NVarChar,256,Model.Introduction),
              SqlHelper.MakeInParam("@State",SqlDbType.Int,4,Model.State),
               SqlHelper.MakeOutParam("@iRet",SqlDbType.Int,4,0),
               SqlHelper.MakeInParam("@Sex",SqlDbType.Int,4,Model.Sex),

            };
                DbHelper.ExecuteNonQuery(procName, sp);
                int iRet = (int)sp[17].Value;
                return iRet;
            }
            catch(Exception ex)
            {
                LogHelper.WriteLog(LogFile.Debug, "【SQL_执行出错】过程：msg:" + ex.Message);
                return -1;
            }
        }


        /// <summary>
        /// 老挝用户信息查询
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="albumid"></param>
        /// <returns></returns>
        public int UserRealNameInfoUp(int useridx, string phoneNumber, string userName, string myName, int gender, string dateBirth, string reserveInfo, string address, string remarksr)
        {
            const string procName = "I_UserRealNameInfoUp";
            SqlParameter[] sp = {
              SqlHelper.MakeInParam("@useridx",SqlDbType.Int,4,useridx),
              SqlHelper.MakeInParam("@phoneNumber",SqlDbType.VarChar,20,phoneNumber),
              SqlHelper.MakeInParam("@userName",SqlDbType.VarChar,10,userName),
              SqlHelper.MakeInParam("@myname",SqlDbType.NVarChar,50,myName),
              SqlHelper.MakeInParam("@dateBirth",SqlDbType.VarChar,20,dateBirth),
              SqlHelper.MakeInParam("@gender",SqlDbType.Int,4,gender),
              SqlHelper.MakeInParam("@reserveInfo",SqlDbType.VarChar,200,reserveInfo),
              SqlHelper.MakeInParam("@address",SqlDbType.VarChar,200,address),
              SqlHelper.MakeInParam("@remarks",SqlDbType.VarChar,200,remarksr),
               SqlHelper.MakeOutParam("@iRet",SqlDbType.Int,4,0)
            };
            DbHelper.ExecuteNonQuery(procName, sp);
            int iRet = (int)sp[9].Value;
            return iRet;
            //return DbHelper.ExecuteNonQuery(DbHelper.conn112_Mobile, procName, sp);
        }

        //用户币兑换
        public int userExchangeCion(int useridx, ref int gameCion, ref int userCion)
        {

            const string procName = "I_GameCionExchangeUserCion";
            SqlParameter[] sp = {
              SqlHelper.MakeInParam("@useridx",SqlDbType.Int,4,useridx),
                 SqlHelper.MakeInParam("@gameCion",SqlDbType.Int,4,gameCion),
                SqlHelper.MakeOutParam("@myGameCion",SqlDbType.Int,4,0),
               SqlHelper.MakeOutParam("@userCion",SqlDbType.Int,4,userCion),
                SqlHelper.MakeOutParam("@iRet",SqlDbType.Int,4,0)
            };
            DbHelper.ExecuteNonQuery(procName, sp);
            gameCion = (int)sp[2].Value;
            userCion = (int)sp[3].Value;
            int iRet = (int)sp[4].Value;
            return iRet;
        }
        /// <summary>
        /// 查看用户是否查看过当前用户的照片或者qq微信
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="tuseridx"></param>
        /// <param name="isphoto"></param>
        /// <param name="isComu"></param>
        /// <returns></returns>
        public int I_userToFoudInfo(int useridx, int tuseridx, ref int isphoto, ref int isComu)
        {

            const string procName = "I_userToFoudInfo";
            SqlParameter[] sp = {
                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,8,useridx),
                SqlHelper.MakeInParam("@tuseridx",SqlDbType.Int,8,tuseridx),
                SqlHelper.MakeOutParam("@isphoto",SqlDbType.Int,4,0),
                SqlHelper.MakeOutParam("@isComu",SqlDbType.Int,4,0),
                SqlHelper.MakeOutParam("@iRet",SqlDbType.Int,4,0)
            };
            DbHelper.ExecuteNonQuery(procName, sp);
            isphoto = (int)sp[2].Value;
            isComu = (int)sp[3].Value;
            int iRet = (int)sp[4].Value;
            return iRet;
        }
        /// <summary>
        /// 查看照片价格
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public decimal I_userToPhotoMoney(int useridx)
        {

            const string procName = "I_userToPhotoMoney";
            SqlParameter[] sp = {
                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,8,useridx),
                SqlHelper.MakeOutParam("@iRet",SqlDbType.Float,20,0)
            };
            DbHelper.ExecuteNonQuery(procName, sp);

            string iRet = sp[1].Value.ToString();

            return decimal.Parse(iRet.ToString());
        }
        
        public string I_userInfoExpand(int useridx, int tuseridx, ref int isVIP, ref int IsRealState, ref string Time)
        {

            const string procName = "I_userInfoExpand";
            SqlParameter[] sp = {
                SqlHelper.MakeInParam("@useridx",SqlDbType.Int,8,useridx),
                SqlHelper.MakeInParam("@tuseridx",SqlDbType.Int,8,tuseridx),
                SqlHelper.MakeOutParam("@isVIP",SqlDbType.Int,4,0),
                SqlHelper.MakeOutParam("@IsRealState",SqlDbType.Int,4,0),
                SqlHelper.MakeOutParam("@Time",SqlDbType.NVarChar,20,0),
                SqlHelper.MakeOutParam("@distance",SqlDbType.NVarChar,20,0)
            };
            DbHelper.ExecuteNonQuery(procName, sp);
            isVIP = (int)sp[2].Value;
            IsRealState = (int)sp[3].Value;
            Time = sp[4].Value.ToString();
            string iRet = sp[5].Value.ToString();
            return iRet;
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
        public List<UserListInfo> GetNearList(float longitude, float latitude, string Area,int page,int pagesize,int sex,int vip,int real,string MonthlyIncome,string Bust,int Agesmall,int Agebig,int LastLogin)
        {
            try
            {
                const string procName = "[AS_Select_UserinfoList]";
                SqlParameter[] sp = {
                SqlHelper.MakeInParam("@longitude",SqlDbType.Real,20,longitude),
                SqlHelper.MakeInParam("@latitude",SqlDbType.Real,20,latitude),
                SqlHelper.MakeInParam("@Area",SqlDbType.NVarChar,20,Area),
                SqlHelper.MakeInParam("@page",SqlDbType.Int,4,page),
                SqlHelper.MakeInParam("@pagesize",SqlDbType.Int,4,pagesize),
                SqlHelper.MakeInParam("@sex",SqlDbType.Int,2,sex),
                SqlHelper.MakeInParam("@vip",SqlDbType.Int,4,vip),
                SqlHelper.MakeInParam("@real",SqlDbType.Int,4,real),
                SqlHelper.MakeInParam("@MonthlyIncome",SqlDbType.NVarChar,20,MonthlyIncome),
                SqlHelper.MakeInParam("@Bust",SqlDbType.NVarChar,20,Bust),
                SqlHelper.MakeInParam("@Agesmall",SqlDbType.Int,4,Agesmall),
                SqlHelper.MakeInParam("@Agebig",SqlDbType.Int,4,Agebig),
                SqlHelper.MakeInParam("@LastLogin",SqlDbType.Int,5,LastLogin),
                SqlHelper.MakeInParam("@longitudemin",SqlDbType.Float,15,0),
                SqlHelper.MakeInParam("@longitudemax",SqlDbType.Float,15,0),
                SqlHelper.MakeInParam("@latitudemin",SqlDbType.Float,15,0),
                SqlHelper.MakeInParam("@latitudemax",SqlDbType.Float,15,0),
            };
                DataTable dt = DbHelper.GetTable(procName, sp);
                return RFHelper<UserListInfo>.ConvertToList(dt);
            }
            catch(Exception ex)
            {
                return null;
            }
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
        /// <returns></returns>
        public List<UserListInfo> GetHomeList(float longitude, float latitude, int type,int page,int pagesize, double longitudemin, double longitudemax, double latitudemin, double latitudemax)
        {
            try
            {
                const string procName = "[AS_Select_UserHomeList]";
                SqlParameter[] sp = {
                SqlHelper.MakeInParam("@longitude",SqlDbType.Real,20,longitude),
                SqlHelper.MakeInParam("@latitude",SqlDbType.Real,20,latitude),
                SqlHelper.MakeInParam("@type",SqlDbType.Int,4,type),
                SqlHelper.MakeInParam("@page",SqlDbType.Int,4,page),
                SqlHelper.MakeInParam("@pagesize",SqlDbType.Int,4,pagesize),
                SqlHelper.MakeInParam("@longitudemin",SqlDbType.Float,15,longitudemin),
                SqlHelper.MakeInParam("@longitudemax",SqlDbType.Float,15,longitudemax),
                SqlHelper.MakeInParam("@latitudemin",SqlDbType.Float,15,latitudemin),
                SqlHelper.MakeInParam("@latitudemax",SqlDbType.Float,15,latitudemax),
            };
                DataTable dt = DbHelper.GetTable(procName, sp);
                return RFHelper<UserListInfo>.ConvertToList(dt);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 可查看次数查询
        /// </summary>
        /// tueridx 只需要type=0的时候传递
        /// <param name="type">类型 0 查看主页次数  1查看收费照片 2聊天信息次数   </param>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public int GetUserPower( int useridx)
        {
            try
            {
                const string procName = "AS_Select_UserPower";
                SqlParameter[] sp = {
                 SqlHelper.MakeInParam("@useridx",SqlDbType.Int,8,useridx),
                SqlHelper.MakeOutParam("@iRet",SqlDbType.Int,4,0),
            };
                DbHelper.ExecuteNonQuery(procName, sp);
                int iRet = (int)sp[1].Value;
                return iRet;
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 可查看次数查询
        /// </summary>
        /// tueridx 只需要type=0的时候传递
        /// <param name="type">类型 0 查看主页次数  1查看收费照片 2聊天信息次数   </param>
        /// <param name="useridx"></param>
        /// <returns></returns>
        public int UserCountInfo(int type, int useridx, int tueridx,int sex)
        {
            try
            {
                const string procName = "AS_Select_UserCountInfo";
                SqlParameter[] sp = {
              SqlHelper.MakeInParam("@type",SqlDbType.Int,4,type),
                 SqlHelper.MakeInParam("@useridx",SqlDbType.Int,8,useridx),
                SqlHelper.MakeOutParam("@iRet",SqlDbType.Int,4,0),
                SqlHelper.MakeInParam("@tueridx",SqlDbType.Int,8,tueridx),
                SqlHelper.MakeInParam("@sex",SqlDbType.Int,2,sex)
            };
                DbHelper.ExecuteNonQuery(procName, sp);
                int iRet = (int)sp[2].Value;
                return iRet;
            }
            catch
            {
                return -1;
            }
        }
        /// <summary>
        /// 访客记录查询
        /// </summary>
        /// <param name="type"> 0 查询是否有记录 1加入记录</param>
        /// <param name="useridx"></param>
        /// <param name="tuseridx"></param>
        /// <returns></returns>
        public int UservisitorsInfo(int type, int useridx, int tuseridx)
        {
            const string procName = "AS_Select_visitorsInfo";
            try
            {
                SqlParameter[] sp = {
              SqlHelper.MakeInParam("@type",SqlDbType.Int,4,type),
                 SqlHelper.MakeInParam("@useridx",SqlDbType.Int,8,useridx),
                 SqlHelper.MakeInParam("@tuseridx",SqlDbType.Int,8,tuseridx),
                SqlHelper.MakeOutParam("@iRet",SqlDbType.Int,4,0)
            };
                DbHelper.ExecuteNonQuery(procName, sp);
                int iRet = (int)sp[3].Value;
                return iRet;
            }
            catch
            {
                return 0;
            }

        }
        /// <summary>
        /// 阅后即焚   iRet=0 成功记录 iRet=1 已经查看过了
        /// </summary>
        /// <param name="Albumid"></param>
        /// <param name="useridx"></param>
        /// <param name="tuseridx"></param>
        /// <returns></returns>
        public int GetLookClearPhoto(int Albumid, int useridx, int tuseridx)
        {
            const string procName = "[Live_Get_AlbumLookList]";
            try
            {
                SqlParameter[] sp = {
              SqlHelper.MakeInParam("@Albumid",SqlDbType.Int,10,Albumid),
                 SqlHelper.MakeInParam("@useridx",SqlDbType.Int,8,useridx),
                 SqlHelper.MakeInParam("@tuseridx",SqlDbType.Int,8,tuseridx),
                SqlHelper.MakeOutParam("@iRet",SqlDbType.Int,4,0)
            };
                DbHelper.ExecuteNonQuery(procName, sp);
                int iRet = (int)sp[3].Value;
                return iRet;
            }
            catch
            {
                return 1;
            }

        }
        /// <summary>
        ///  收费   0 照片收费 1 聊天信息收费
        /// </summary>
        /// <param name="type"></param>
        /// <param name="useridx"></param>
        /// <param name="tuseridx"></param>
        /// <returns></returns>
        public int GetChargeInfo(int type, int useridx, int tuseridx)
        {
            const string procName = "[Live_Get_ChargeLook]";
            try
            {
                SqlParameter[] sp = {
              SqlHelper.MakeInParam("@type",SqlDbType.Int,2,type),
                 SqlHelper.MakeInParam("@useridx",SqlDbType.Int,8,useridx),
                 SqlHelper.MakeInParam("@tuseridx",SqlDbType.Int,8,tuseridx),
                SqlHelper.MakeOutParam("@iRet",SqlDbType.Int,4,0)
            };
                DbHelper.ExecuteNonQuery(procName, sp);
                int iRet = (int)sp[3].Value;
                return iRet;
            }
            catch
            {
                return 1;
            }

        }
        
        /// <summary>
        /// 银行列表查询
        /// </summary>
        /// <returns></returns>
        public List<BankCardImgConfigure> BankCardImgConfigureSet(int id)
        {
            const string procName = "I_BankCardImgConfigureSet";

            try
            {
                SqlParameter[] sp = {
                    SqlHelper.MakeInParam("@id",SqlDbType.Int,4,id)
                };
                DataTable dt = DbHelper.GetTable(procName, sp);
                return RFHelper<BankCardImgConfigure>.ConvertToList(dt);
            }
            catch (Exception)
            {


                return null;
            }
        }


        /// <summary>
        /// 用户银行卡列表查询
        /// </summary>
        /// <param name="userIdx"></param>
        /// <returns></returns>
        public List<LaoWo_UserBankInfo> LaoWo_UserBankInfoSet(int userIdx)
        {
            const string procName = "I_LaoWo_UserBankInfoSet";

            try
            {
                SqlParameter[] sp = {
                           SqlHelper.MakeInParam("@userIdx",SqlDbType.Int,4,userIdx)
                };
                DataTable dt = DbHelper.GetTable(procName, sp);
                return RFHelper<LaoWo_UserBankInfo>.ConvertToList(dt);
            }
            catch (Exception)
            {

                return null;
            }
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
            const string procName = "I_LaoWo_UserBankInfoInsert";
            int iRet = 0;
            try
            {
                SqlParameter[] sp = {
                         SqlHelper.MakeInParam("@userIdx",SqlDbType.Int,4,ubi.userIdx),
                         SqlHelper.MakeInParam("@bankType",SqlDbType.Int,4,ubi.bankType),
                         SqlHelper.MakeInParam("@bankCardType",SqlDbType.Int,4,ubi.bankCardType),
                         SqlHelper.MakeInParam("@bankUserName",SqlDbType.VarChar,20,ubi.bankUserName),
                         SqlHelper.MakeInParam("@bankId",SqlDbType.VarChar,30,ubi.bankId),
                         SqlHelper.MakeInParam("@rProvince",SqlDbType.VarChar,50,ubi.rProvince),
                         SqlHelper.MakeInParam("@rCity",SqlDbType.VarChar,50,ubi.rCity),
                          SqlHelper.MakeInParam("@dot",SqlDbType.VarChar,200,ubi.dot),
                           SqlHelper.MakeOutParam("@iRet",SqlDbType.Int,4,iRet)
                };
                iRet = DbHelper.ExecuteNonQuery(procName, sp);
                iRet = (int)sp[8].Value;
                return iRet;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }



        /// <summary>
        /// 用户银行卡删除
        /// </summary>
        /// <param name="userIdx"></param>
        /// <param name="bankId"></param>
        /// <returns></returns>
        public int LaoWo_UserBankInfoDel(int userIdx, string bankId)
        {
            const string procName = "I_LaoWo_UserBankInfoDel";

            try
            {
                SqlParameter[] sp = {
                         SqlHelper.MakeInParam("@userIdx",SqlDbType.Int,4,userIdx),
                         SqlHelper.MakeInParam("@bankId",SqlDbType.VarChar,30,bankId)
                };
                int iRet = DbHelper.ExecuteNonQuery(procName, sp);
                return iRet;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        /// <summary>
        /// 获取用户代理信息
        /// </summary>
        /// <param name="userIdx"></param>
        /// <returns></returns>
        public List<UserInvitationInfo> GetUserInvitationInfoByUseridxSet(int userIdx)
        {
            const string procName = "I_GetUserInvitationInfoByUseridxSet";

            try
            {
                SqlParameter[] sp = {
                           SqlHelper.MakeInParam("@userIdx",SqlDbType.Int,4,userIdx)
                };
                DataTable dt = DbHelper.GetTable(procName, sp);
                return RFHelper<UserInvitationInfo>.ConvertToList(dt);
            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>
        /// 结算表(每周跑作业计入数据)
        /// </summary>
        /// <param name="userIdx"></param>
        /// <returns></returns>
        public List<LW_IncomeLog> LW_IncomeLogSet(int userIdx, int page, int pageSize, ref int count)
        {
            const string procName = "I_LW_IncomeLogSet";

            try
            {
                SqlParameter[] sp = {
                           SqlHelper.MakeInParam("@userIdx",SqlDbType.Int,4,userIdx),
                             SqlHelper.MakeInParam("@page",SqlDbType.Int,4,page),
                               SqlHelper.MakeInParam("@pageSize",SqlDbType.Int,4,pageSize),
                                SqlHelper.MakeOutParam("@count",SqlDbType.Int,4,count)
                };
                DataTable dt = DbHelper.GetTable(procName, sp);
                count = (int)sp[3].Value;
                return RFHelper<LW_IncomeLog>.ConvertToList(dt);
            }
            catch (Exception)
            {

                return null;
            }
        }


        /// <summary>
        /// 交易明细(投注记录排除)
        /// </summary>
        /// <param name="userIdx"></param>
        /// <returns></returns>
        public List<LW_Accounts_MoneyTradeDetail> LW_Accounts_MoneyTradeDetailSet(int userIdx, int page, int pageSize, ref int count)
        {
            const string procName = "I_LW_Accounts_MoneyTradeDetailSet";

            try
            {
                SqlParameter[] sp = {
                           SqlHelper.MakeInParam("@userIdx",SqlDbType.Int,4,userIdx),
                             SqlHelper.MakeInParam("@page",SqlDbType.Int,4,page),
                               SqlHelper.MakeInParam("@pageSize",SqlDbType.Int,4,pageSize),
                                SqlHelper.MakeOutParam("@count",SqlDbType.Int,4,count)
                };
                DataTable dt = DbHelper.GetTable(procName, sp);
                count = (int)sp[3].Value;
                return RFHelper<LW_Accounts_MoneyTradeDetail>.ConvertToList(dt);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public int pwdCheck(int userIdx, string pwd)
        {
            const string procName = "DESEncryptPwd";

            try
            {
                SqlParameter[] sp = {

                        SqlHelper.MakeInParam("@type",SqlDbType.Int,4,2),
                        SqlHelper.MakeInParam("@userIdx",SqlDbType.Int,4,userIdx),
                        SqlHelper.MakeInParam("@pwdMd5",SqlDbType.VarChar,255,pwd),
                        SqlHelper.MakeOutParam("@pwd",SqlDbType.VarChar,255,""),
                        SqlHelper.MakeOutParam("@iRet",SqlDbType.Int,4,0)
                };
                int iRet = DbHelper.ExecuteNonQuery(procName, sp);
                return iRet;
            }
            catch (Exception)
            {

                return 0;
            }

        }
        //EXEC DESEncryptPwd 2,@useridx,@OriginalPwd,@pwd OUT,@iRest OUT
        #endregion
    }
}
