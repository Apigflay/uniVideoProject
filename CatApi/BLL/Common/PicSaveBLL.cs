using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Web;

namespace BLL.Common
{
    public class PicSaveBLL
    {
        public enum Picdir
        {
            /// <summary>
            /// 头像
            /// </summary>
            avator,
            /// <summary>
            /// 实名认证
            /// </summary>
            idcard,
            /// <summary>
            /// 相册
            /// </summary>
            album
        }

        private static string picturePath = ConfigHelper.GetAppSettings("picturePath");
        private static string pictureWebPath = ConfigHelper.GetAppSettings("pictureDomainHttps");

        /// <summary>
        /// 修改用户头像
        /// </summary>
        /// <param name="type"></param>
        /// <param name="platForm">1：喵播 ，2：喵拍</param>
        /// <param name="useridx"></param>
        /// <param name="file"></param>
        /// <param name="webpic_min">返回小图</param>
        /// <param name="webpic_max">返回大图</param>
        /// <returns></returns>
        public static bool SavePhoto(int type, int platForm, int useridx, HttpPostedFileBase file, ref string webpic_min, ref string webpic_max)
        {
            string avatarFilePath = picturePath + Picdir.avator + "\\";// ConfigurationManager.AppSettings["avatarPhyPath"];
            string filePath = DateTime.Now.ToString("yyyy-MM/dd/HH/");
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + useridx;
            string phyPath = Path.Combine(avatarFilePath, filePath.Replace("/", "\\"));
            //图片web路径
            string imgDomain = ConfigurationManager.AppSettings["avatarWebPath"];
            
            //F:\\TianGe\\liveimg.9158.com\\pic\\avator\\2017-03\16\15\
            if (!Directory.Exists(phyPath)) { Directory.CreateDirectory(phyPath); }

            //F:\\TianGe\\liveimg.9158.com\\pic\\avator\\2017-03\16\15\20170316151231_67415276_250.png
            avatarFilePath = Path.Combine(phyPath + fileName + "_250.png");
            ImageHelper.MakeSmallImg(file.InputStream, avatarFilePath, 250, 250);

            if (platForm == 1)
            {
                avatarFilePath = Path.Combine(phyPath + fileName + "_640.png");
                ImageHelper.MakeSmallImg(file.InputStream, avatarFilePath, 640, 640);
            }
            
            webpic_min = imgDomain + filePath + fileName + "_250.png";
            webpic_max = imgDomain + filePath + fileName + "_640.png";

            //如果平台是喵拍则小图和大图一样
            if (platForm == 2) webpic_max = webpic_min;

            return true;
        }

        public static bool SaveVideo( int useridx, HttpPostedFileBase file, ref string url)
        {
            string avatarFilePath = picturePath + Picdir.idcard + "\\";// ConfigurationManager.AppSettings["avatarPhyPath"];
            string filePath = DateTime.Now.ToString("yyyy-MM/dd/HH/");
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + useridx;
            string phyPath = Path.Combine(avatarFilePath, filePath.Replace("/", "\\"));
            //Videoweb路径
            string VideoHttps = ConfigurationManager.AppSettings["VideoHttps"];


            //F:\\TianGe\\liveimg.9158.com\\pic\\avator\\2017-03\16\15\
            if (!Directory.Exists(phyPath)) { Directory.CreateDirectory(phyPath); }

            //F:\\TianGe\\liveimg.9158.com\\pic\\avator\\2017-03\16\15\20170316151231_67415276_250.png
            avatarFilePath = Path.Combine(phyPath + fileName + "_250.mp4");
            try
            {
               // string path = System.Web.HttpContext.Current.Server.MapPath(avatarFilePath);
                file.SaveAs(avatarFilePath);
            }
            catch(Exception ex)
            { }
            
            url = VideoHttps+ filePath+ fileName+ "_250.mp4";
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="file"> 0--图片  1 语音</param>
        /// <param name="type"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool SaveNews(int useridx, HttpPostedFileBase file,int type, ref string url,ref string urlbig,int width=640,int Height=640)
        {
            string avatarFilePath = picturePath + "News" + "\\";// ConfigurationManager.AppSettings["avatarPhyPath"];
            string filePath = DateTime.Now.ToString("yyyy-MM/dd/HH/");
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + useridx;
            string phyPath = Path.Combine(avatarFilePath, filePath.Replace("/", "\\"));
            //Videoweb路径
            string VideoHttps = ConfigurationManager.AppSettings["NewsHttps"];

            if (!Directory.Exists(phyPath)) { Directory.CreateDirectory(phyPath); }
            if (type == 0)
            {
                avatarFilePath = Path.Combine(phyPath + fileName + "_250.png");
                string avatarFilePath2 = Path.Combine(phyPath + fileName + "_640.png");
                ImageHelper.MakeSmallImg(file.InputStream, avatarFilePath, 250, 250);
                ImageHelper.MakeSmallImg(file.InputStream, avatarFilePath2, width, Height);
                url = VideoHttps + filePath + fileName + "_250.png";
                urlbig= VideoHttps + filePath + fileName + "_640.png";
            }
            else
            {
                avatarFilePath = Path.Combine(phyPath + fileName + "_250.amr");
                url = VideoHttps + filePath + fileName + "_250.amr";
                try
                {
                    file.SaveAs(avatarFilePath);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
        public static bool SaveNews(int useridx, HttpPostedFileBase file, int type, ref string url, ref string urlbig, ref int widthnew, ref int heightnew, int width = 640, int Height = 640)
        {
            string avatarFilePath = picturePath + "News" + "\\";// ConfigurationManager.AppSettings["avatarPhyPath"];
            string filePath = DateTime.Now.ToString("yyyy-MM/dd/HH/");
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + useridx;
            string phyPath = Path.Combine(avatarFilePath, filePath.Replace("/", "\\"));
            //Videoweb路径
            string VideoHttps = ConfigurationManager.AppSettings["NewsHttps"];

            if (!Directory.Exists(phyPath)) { Directory.CreateDirectory(phyPath); }
            if (type == 0)
            {
                avatarFilePath = Path.Combine(phyPath + fileName + "_250.png");
                string avatarFilePath2 = Path.Combine(phyPath + fileName + "origin.png");
                ImageHelper.MakeSmallImg(file.InputStream, avatarFilePath, 250, 250, ref widthnew, ref heightnew);
                ImageHelper.MakeSmallImg(file.InputStream, avatarFilePath2, width, Height);
                url = VideoHttps + filePath + fileName + "_250.png";
                urlbig = VideoHttps + filePath + fileName + "origin.png";
            }
            else
            {
                avatarFilePath = Path.Combine(phyPath + fileName + "_250.amr");
                url = VideoHttps + filePath + fileName + "_250.amr";
                try
                {
                    file.SaveAs(avatarFilePath);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 实名认证图片保存
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="webPic_1"></param>
        /// <param name="webPic_2"></param>
        /// <param name="webPic_3"></param>
        public static bool SaveCertPhoto(int useridx, HttpFileCollection files, out string webPic_1, out string webPic_2, out string webPic_3)
        {
            //图片存储相关变量
            string imgDirPath = picturePath + Picdir.idcard + "\\";// ConfigHelper.GetAppSettings("certPhyPath");
            string imgDomainPath = ConfigHelper.GetAppSettings("certWebPath");

            DateTime date = DateTime.Now;
            string imgName = HashFileName(date.ToString("yyyyMMddHHmmss") + useridx);
            string imgPath = GetdirPath();
            string phyPath = Path.Combine(imgDirPath, imgPath.Replace("/", "\\"));

            #region 保存图片

            if (!Directory.Exists(phyPath)) { Directory.CreateDirectory(phyPath); }

            for (int i = 0; i < files.AllKeys.Length; i++)
            {
                HttpPostedFile file = files[i];

                string tempName = imgName + "@" + i + ".png";
                imgDirPath = Path.Combine(phyPath + tempName);

                ImageHelper.MakeSmallImg(file.InputStream, imgDirPath, 640, 640);
            }

            webPic_1 = imgDomainPath + imgPath + imgName + "@0.png";
            webPic_2 = imgDomainPath + imgPath + imgName + "@1.png";
            webPic_3 = imgDomainPath + imgPath + imgName + "@2.png";

            #endregion

            return true;
        }

        public static bool SaveCertPhoto(int useridx, HttpFileCollection files, ref List<string> urlList)
        {
            //图片存储相关变量
            string imgDirPath = picturePath + Picdir.idcard + "\\";// ConfigHelper.GetAppSettings("certPhyPath");
            string imgDomainPath = ConfigHelper.GetAppSettings("certWebPath");

            DateTime date = DateTime.Now;
            string fileExt = ".png";
            string imgName = HashFileName(date.ToString("yyyyMMddHHmmss") + useridx);
            string imgPath = GetdirPath();
            string phyPath = Path.Combine(imgDirPath, imgPath.Replace("/", "\\"));

            #region 保存图片

            if (!Directory.Exists(phyPath)) { Directory.CreateDirectory(phyPath); }

            for (int i = 0; i < files.AllKeys.Length; i++)
            {
                HttpPostedFile file = files[i];

                string tempName = imgName + "@" + i + fileExt;
                imgDirPath = Path.Combine(phyPath + tempName);

                ImageHelper.MakeSmallImg(file.InputStream, imgDirPath, 640, 640);

                urlList.Add(imgDomainPath + imgPath + tempName);
            }
            #endregion

            return true;
        }

        /// <summary>
        /// 相册上传
        /// </summary>
        /// <param name="useridx"></param>
        /// <param name="files"></param>
        /// <param name="imgURL"></param>
        /// <returns></returns>
        public static int SaveAlbumPhoto(int useridx, int albumtype, HttpFileCollection files, ref List<string> failPhotoList, ref List<Album> albumList)
        {
            int saveSuccessNum = 0;
            string fileExt = ".png";
            string imgSavePath = picturePath + Picdir.album + "\\";
            string imgDomainPath = pictureWebPath + Picdir.album + "/";
            string imgName, dirPath, phyPath;

            dirPath = GetdirPath();
            phyPath = Path.Combine(imgSavePath, dirPath.Replace("/", "\\"));

            if (!Directory.Exists(phyPath)) { Directory.CreateDirectory(phyPath); }

            for (int i = 0; i < files.AllKeys.Length; i++)
            {
                imgName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                imgName = HashFileName(imgName + useridx);

                HttpPostedFile file = files[i];

                bool result = false;
                //double fileSize = file.ContentLength / 1024;//unit (k)
                string tempfileName = imgName + i + "_" + albumtype + fileExt;

                //完整虚拟路径带图片扩展名
                imgSavePath = Path.Combine(phyPath + tempfileName);
                result = ImageHelper.MakeThumbnail(file.InputStream, tempfileName, imgSavePath, 1242, 2208, ImageHelper.ThumbnailModeOption.CUSTOM);

                #region 数据库操作

                Album albumModel = new Album();
                albumModel.imgURL = imgDomainPath + dirPath + tempfileName;
                albumModel.phyPath = dirPath.Replace("/", "\\") + tempfileName;

                if (result) saveSuccessNum++;
                if (result) albumList.Add(albumModel);
                if (!result) failPhotoList.Add(file.FileName);

                #endregion
            }

            return saveSuccessNum;
        }

        private static string GetdirPath()
        {
            DateTime date = DateTime.Now;
            return date.ToString("yyyyMM/dd/HH/");
        }

        /// <summary>
        /// 取文件名的MD5值
        /// </summary>
        /// <param name="name">文件名，不包括扩展名</param>
        /// <returns></returns>
        private static string HashFileName(string name)
        {
            byte[] b = System.Text.Encoding.Default.GetBytes(name);
            MD5 md5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(md5.ComputeHash(b)).Replace("-", String.Empty);
        }
    }
}
