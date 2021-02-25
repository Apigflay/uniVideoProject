/******************************************
* 文 件 名：LuceneHelper
* 创 建 人：zhaorui
* 创建日期：2017年7月14日
* 文件描述：搜索引擎帮助类
******************************************
* 修 改 人：
* 修改日期：
* 备注描述：
*****************************************/
using Common;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BLL.Logic
{
    public class LuceneHelper
    {
        private static string indexPath = ConfigHelper.GetAppSettings("indexPath");
        private static Directory index_dir = FSDirectory.Open(indexPath);

        private static UserInfoBLL userService = new UserInfoBLL();

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<int> searchBykeyword(string keyword, int count, ref int totalCount)
        {
            List<int> idxList = new List<int>();

            int top = 200 - count;
            string spitKeyword = GetKeyWordSplitBySpace(keyword, true);//Spit Keyword
            string[] fileds = { "nickName", "signatures" };//Query Filed

            if (!IndexReader.IndexExists(index_dir)) { return idxList; }

            //1.创建检索对象(获取建立的索引)
            //true mean is Readonly
            IndexSearcher searcher = new IndexSearcher(index_dir, true);
            PanGuAnalyzer analyzer = new PanGuAnalyzer();

            //2.创建查询条件对象Query
            //Single Filed to Query
            //QueryParser parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "nickName", analyzer);
            //Multi Filed to Query
            QueryParser parser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30, fileds, analyzer);
            //注入查询条件
            Query query = parser.Parse(spitKeyword);

            //复合查询(和上面的多字段查询好像是一样的？待验证)
            //BooleanQuery bq = new BooleanQuery();
            //bq.Add(query, Occur.MUST);

            TopDocs docs = searcher.Search(query, null, top);
            totalCount = count + docs.TotalHits;

            ScoreDoc[] scoreDocs = docs.ScoreDocs;

            foreach (ScoreDoc item in scoreDocs)
            {
                Document doc = searcher.Doc(item.Doc);
                SearchUserInfo user = new SearchUserInfo();
                user.userIdx = int.Parse(doc.Get("uidx"));
                user.nickName = doc.Get("nickName");
                user.signatures = doc.Get("signatures");
                
                idxList.Add(user.userIdx);
            }

            return idxList;
        }


        #region 建立索引

        /// <summary>
        /// 创建索引
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="isCreated">IsCreate是索引创建方式（true：重新新建索引(删除之前存在的索引记录)，false：从旧的索引执行追加）</param>
        public static void CreateIndex(List<SearchUserInfo> dataList, bool isCreated)
        {
            PanGuAnalyzer analyzer = new PanGuAnalyzer();
            IndexWriter writer = null;

            try
            {
                writer = new IndexWriter(index_dir, analyzer, isCreated, IndexWriter.MaxFieldLength.LIMITED);

                Insert_document(writer, dataList);
            }
            catch (System.IO.IOException e)
            {
                LogHelper.WriteLog(LogFile.Error, "【创建索引时出错】" + e.Message.ToString());
            }
            finally
            {
                writer.Optimize();
                writer.Dispose();
            }
        }

        #region 私有方法

        /// <summary>
        /// 向索引中插入文档
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="writer"></param>
        private static void Insert_document(IndexWriter writer, List<SearchUserInfo> dataList)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            foreach (var item in dataList)
            {
                writer.AddDocument(GetDoc(item));
            }
            sw.Stop();

            LogHelper.WriteLog(LogFile.Data, "【创建索引时间】{0}", sw.Elapsed);
        }
        private static void Insert_document(IndexWriter writer, SearchUserInfo item)
        {
            if (item != null && item.userIdx > 0)
            {
                writer.AddDocument(GetDoc(item));
            }
        }

        /// <summary>
        /// 添加要索引的项
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private static Document GetDoc(SearchUserInfo item)
        {
            Document doc = new Document();

            // 参数说明：   
            // Field.Store：
            // Field.Store.NO：表示该Field不需要存储。   
            // Field.Store.Yes：表示该Field需要存储。   
            // Field.Store.COMPRESS：表示使用压缩方式来存储。   
            // Field.Index：
            // Field.Index.NO：表示该Field不需要索引。   
            // Field.Index.TOKENIZED：表示该Field先被分词再索引。   
            // Field.Index.UN_TOKENIZED：表示不对该Field进行分词，但要对其索引。   
            // Field.Index.NO_NORMS：表示该Ｆield进行索引，但是要对它用Analyzer，同时禁止它参加评分，主要是为了减少内在的消耗。
            /**
             * Field.Store 表示“是否存储”，即该Field内的信息是否要被原封不动的保存在索引中。
             * Field.Index 表示“是否索引”，即在这个Field中的数据是否在将来检索时需要被用户检索到，一个“不索引”的Field通常仅是提供辅助信息储存的功能。
             * Field.TermVector 表示“是否切词”，即在这个Field中的数据是否需要被切词。
             */
            //只有对需要全文检索的字段才ANALYZED
            doc.Add(new Field("uidx", item.userIdx.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("nickName", item.nickName, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("signatures", item.signatures, Field.Store.YES, Field.Index.ANALYZED));

            return doc;
        }

        #endregion

        #endregion

        #region 分词

        /// <summary>  
        /// 对关键字进行分词(分词后得到的字符串进行查询可参考下面注释掉的代码)  
        /// </summary>  
        /// <param name="keyWords">要搜索的词</param> 
        /// <param name="IsComma">分的词用逗号分隔true 以{0}^{1}.0的形式false</param>
        /// <returns>分词后的结果</returns>  
        public static string GetKeyWordSplitBySpace(string keyWords, bool IsComma)
        {
            PanGuTokenizer panGuTokenizer = new PanGuTokenizer();

            StringBuilder result = new StringBuilder();
            ICollection<PanGu.WordInfo> words = panGuTokenizer.SegmentToWordInfos(keyWords);

            foreach (PanGu.WordInfo word in words)
            {
                if (word == null)
                {
                    continue;
                }
                if (IsComma)
                {
                    result.Append(word.Word + ",");
                }
                else
                {
                    result.AppendFormat("{0}^{1}.0 ", word.Word, (int)Math.Pow(3, word.Rank));
                }
            }

            return result.ToString().Trim();
        }

        #endregion
    }
}
