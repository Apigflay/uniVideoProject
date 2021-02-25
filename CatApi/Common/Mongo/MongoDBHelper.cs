/******************************************
* 文 件 名：MongoDBHelper
* Copyright(c) live.9158.com
* 创 建 人：zhaorui
* 创建日期：2017年5月
* 文件描述：Mongodb Helper Class(独立)
******************************************
* 修 改 人：
* 修改日期：
* 备注描述：
*****************************************/
using System;
using Common;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using Model;

namespace Common
{
    public class MongoDBHelper//<T> where T : new()
    {
        private MongoClient client;
        private MongoServer server;
        private MongoDatabase db;
        /// <summary>
        /// 数据库连接pwd里面不能包含特殊字符。/%$字符不行
        /// </summary>
        private static string mongoConn = ConfigHelper.GetDbString("mongodb", false);

        public MongoDBHelper()
        {
            try
            {
                client = new MongoClient(mongoConn);
                server = client.GetServer();
                db = server.GetDatabase("mobile");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public MongoDBHelper()
        //    : this(mongoConn, "mobile")
        //{

        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="_connString"></param>
        ///// <param name="_dbName"></param>
        //public MongoDBHelper(string _connString, string _dbName)
        //{
        //    client = new MongoClient(_connString);
        //    server = client.GetServer();

        //    db = server.GetDatabase(_dbName);
        //}

        #region Insert

        /// <summary>
        /// Mongodb Insert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">泛型名称</param>
        /// <param name="collName">集合名称</param>
        /// <returns></returns>
        public int Insert<T>(string collName, T t) where T : class, new()
        {
            try
            {
                var collection = db.GetCollection(collName);

                WriteConcernResult result = collection.Insert(t);

                return result.Ok ? 1 : 0;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogFile.Error, "Mongodb Insert Data Error.{0}", ex.Message);

                return 0;
            }
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collName"></param>
        /// <param name="list"></param>
        public void InsertBatch<T>(string collName, List<T> list)
        {
            var collection = db.GetCollection(collName);

            List<BsonDocument> bsonList = new List<BsonDocument>();

            list.ForEach(t => bsonList.Add(t.ToBsonDocument()));

            collection.InsertBatch(bsonList);
        }

        #endregion

        #region  Select

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collName"></param>
        /// <returns></returns>
        public BsonDocument FindOne(string collName, IMongoQuery query)
        {
            try
            {
                var collection = db.GetCollection(collName);
                var entity = collection.FindOne(query);

                return entity;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogFile.Error, "Mongodb Find Data Error。" + ex.Message);

                return null;
            }
        }

        public MongoCursor<T> FindAll<T>(string collName)//where T : class, new()
        {
            try
            {
                var collection = db.GetCollection(collName);
                var entity = collection.FindAllAs<T>();

                return entity;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogFile.Error, "Mongodb Find Data Error。" + ex.Message);

                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collName"></param>
        /// <param name="query"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public MongoCursor<BsonDocument> Find(string collName, QueryDocument query, SortByDocument sort, Model.Paging page)
        {
            MongoCursor<BsonDocument> bsonDoc_List = db.GetCollection(collName)
                .Find(query)
                .SetSortOrder(sort)
                .SetSkip((page.pageIndex - 1) * page.pageSize)
                //.SetLimit(page.pageSize*page.pageIndex); 写法错误
                .SetLimit(page.pageSize);
            //.SetLimit(200)

            return bsonDoc_List;
        }

        #endregion

        /// <summary>
        /// 更新文档
        /// </summary>
        /// <param name="collName">集合名称</param>
        /// <param name="query">查询条件</param>
        /// <param name="update">更新条件</param>
        /// <param name="flag">是否使用特殊更新,true：如果存在条件的则更新记录，否则插入一条数据</param>
        /// <returns></returns>
        public int Update(string collName, IMongoQuery query, IMongoUpdate update, UpdateFlags flag = UpdateFlags.None)
        {
            try
            {
                var collection = db.GetCollection(collName);

                WriteConcernResult result = collection.Update(query, update, flag);

                return result.Ok ? 1 : 0;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogFile.Error, "Mongodb Update Data Error。" + ex.Message);

                return 0;
            }
        }

        #region Remove

        /// <summary>
        /// 根据条件删除文档
        /// </summary>
        /// <param name="collName"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public int Remove(string collName, IMongoQuery query)
        {
            try
            {
                var collection = db.GetCollection(collName);
                WriteConcernResult result = collection.Remove(query);

                return result.Ok ? 1 : 0;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogFile.Error, "Mongodb Remove Data Error。" + ex.Message);

                return 0;
            }
        }
        /// <summary>
        /// 根据条件删除文档
        /// </summary>
        /// <param name="collName"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public int RemoveAll(string collName)
        {
            try
            {
                var collection = db.GetCollection(collName);
                WriteConcernResult result = collection.RemoveAll();

                return result.Ok ? 1 : 0;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogFile.Error, "Mongodb RemoveAll Data Error。" + ex.Message);

                return 0;
            }
        }
        #endregion
    }
}