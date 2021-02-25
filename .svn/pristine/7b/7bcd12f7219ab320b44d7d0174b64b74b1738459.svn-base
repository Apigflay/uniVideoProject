#region << 版 本 注 释 >>
/****************************************************
* 文 件 名：ImageHelper
* Copyright(c) www.ITdos.com
* CLR 版本: 4.0.30319.17929
* 创 建 人：ITdos
* 电子邮箱：admin@itdos.com
* 创建日期：2010/04/01 11:00:49
* 文件描述：
******************************************************
* 修 改 人：
* 修改日期：
* 备注描述：
*******************************************************/
#endregion
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Common
{
    /// <summary>
    /// 图片水印处理类
    /// </summary>
    public class ImageHelper
    {
        #region 配置文件

        /// <summary>
        /// 生成缩略图的模式， WH-指定宽高缩放（可能变形） W-指定宽，高按比例  H-指定高，宽按比例 CUT-指定高宽裁减（不变形,推荐用这个）。
        /// </summary>
        public enum ThumbnailModeOption : byte
        {
            /// <summary>
            /// 指定宽高缩放（可能变形）
            /// </summary>
            WH,
            /// <summary>
            /// 指定宽，高按比例
            /// </summary>
            W,
            /// <summary>
            /// 指定高，宽按比例
            /// </summary>
            H,
            /// <summary>
            /// 指定高宽裁减（不变形,推荐用这个）
            /// </summary>
            CUT,
            /// <summary>
            /// 自定义(如果宽高超过自定义的就按照指定宽高裁剪)
            /// </summary>
            CUSTOM
        }

        /// <summary>
        /// 加图片水印的位置，TopLeft-左上角 TopCenter-上中间 TopRight-右上角 BottomLeft-左下角 BottomCenter-下中间 右下角-右下角 Middle-正中间。
        /// </summary>
        public enum WaterPositionOption : byte
        {
            /// <summary>
            /// 左上角
            /// </summary>
            LeftTop,
            /// <summary>
            /// 上中间
            /// </summary>
            CenterTop,
            /// <summary>
            /// 右上角
            /// </summary>
            RightTop,
            /// <summary>
            /// 左下角
            /// </summary>
            LeftBottom,
            /// <summary>
            /// 下中间
            /// </summary>
            CenterBottom,
            /// <summary>
            /// 右下角
            /// </summary>
            RightBottom,
            /// <summary>
            /// 正中间
            /// </summary>
            Middle
        }

        /// <summary>
        /// 获取图片格式。
        /// </summary>
        /// <Param name="fileName">文件名</Param>
        /// <returns></returns>
        public static ImageFormat GetImageFormat(string fileName)
        {
            string extension = fileName.Substring(fileName.LastIndexOf(".")).Trim().ToLower();

            switch (extension)
            {
                case ".jpg":
                    return System.Drawing.Imaging.ImageFormat.Jpeg;
                case ".jpeg":
                    return System.Drawing.Imaging.ImageFormat.Jpeg;
                case ".gif":
                    return System.Drawing.Imaging.ImageFormat.Gif;
                case ".png":
                    return System.Drawing.Imaging.ImageFormat.Png;
                case ".bmp":
                    return System.Drawing.Imaging.ImageFormat.Bmp;
                case ".ico":
                    return System.Drawing.Imaging.ImageFormat.Icon;
                default:
                    goto case ".jpg";
            }
        }

        #endregion

        /// <summary>
        /// 加水印图片并保存。(最好不要单独调用此接口)
        /// </summary>
        /// <Param name="originalImageStream">Stream</Param>
        /// <Param name="strFileName">源图路径（物理路径）</Param>
        /// <Param name="savePath">图片保存路径（物理路径）</Param>
        /// <Param name="waterPath">水印图路径（物理路径）</Param>
        /// <Param name="edge">水印图离原图边界的距离</Param>
        /// <Param name="position">加图片水印的位置</Param>
        /// <returns>是否成功</returns>
        private static bool MakeWaterImage(Stream originalImageStream, string strFileName, string savePath, string waterPath, int edge, WaterPositionOption position)
        {
            bool success = false;

            int x = 0;
            int y = 0;
            Image waterImage = null;
            Image image = null;
            Bitmap bitmap = null;
            Graphics graphics = null;

            try
            {
                //加载原图
                image = Image.FromStream(originalImageStream);
                //加载水印图
                waterImage = Image.FromFile(waterPath);
                bitmap = new Bitmap(image);
                graphics = Graphics.FromImage(bitmap);

                int newEdge = edge;
                if (newEdge >= image.Width + waterImage.Width) newEdge = 10;

                switch (position)
                {
                    case WaterPositionOption.LeftTop:
                        x = newEdge;
                        y = newEdge;
                        break;
                    case WaterPositionOption.CenterTop:
                        x = (image.Width - waterImage.Width) / 2;
                        y = newEdge;
                        break;
                    case WaterPositionOption.RightTop:
                        x = image.Width - waterImage.Width - newEdge;
                        y = newEdge;
                        break;
                    case WaterPositionOption.LeftBottom:
                        x = newEdge;
                        y = image.Height - waterImage.Height - newEdge;
                        break;
                    case WaterPositionOption.CenterBottom:
                        x = (image.Width - waterImage.Width) / 2;
                        y = image.Height - waterImage.Height - newEdge;
                        break;
                    case WaterPositionOption.RightBottom:
                        x = image.Width - waterImage.Width - newEdge;
                        y = image.Height - waterImage.Height - newEdge;
                        break;
                    case WaterPositionOption.Middle:
                        x = (image.Width - waterImage.Width) / 2;
                        y = (image.Height - waterImage.Height) / 2;
                        break;
                    default:
                        goto case WaterPositionOption.RightBottom;
                }

                // 画水印图片
                graphics.DrawImage(waterImage, new Rectangle(x, y, waterImage.Width, waterImage.Height), 0, 0, waterImage.Width, waterImage.Height, GraphicsUnit.Pixel);

                // 关闭打开着的文件并保存（覆盖）新图片
                originalImageStream.Close();
                bitmap.Save(savePath, GetImageFormat(strFileName));

                success = true;
            }
            catch (Exception ex)
            {
                success = false;
                throw;
                //throw new Exception(ex.Message.Replace("'", " ").Replace("\n", " ").Replace("\\", "/"));
            }
            finally
            {
                if (graphics != null) graphics.Dispose();
                if (bitmap != null) bitmap.Dispose();
                if (image != null) image.Dispose();
                if (waterImage != null) waterImage.Dispose();
            }

            return success;
        }

        /// <summary>
        /// 生成缩略图并保存。
        /// </summary>
        /// <Param name="originalImageStream">Stream</Param>
        /// <Param name="strFileName">源图路径（物理路径）</Param>
        /// <Param name="thumbnailPath">缩略图路径（物理路径）</Param>
        /// <Param name="maxWidth">缩略图最大宽度</Param>
        /// <Param name="maxheight">缩略图最大高度</Param>
        /// <Param name="mode">生成缩略图的方式</Param>
        /// <returns>是否成功</returns>
        public static bool MakeThumbnail(Stream originalImageStream, string strFileName, string thumbnailPath, int maxWidth, int maxheight, ThumbnailModeOption mode)
        {
            bool success = false;

            int x = 0;
            int y = 0;
            int toW = maxWidth;
            int toH = maxheight;
            Image image = null;
            Image bitmap = null;
            Graphics graphics = null;
            try
            {
                image = Image.FromStream(originalImageStream);
                int w = image.Width;
                int h = image.Height;

                /*上传相册最小尺寸*/
                if (w <= 500 || h <= 500 || w >= 4080) { return false; }

                #region upload Mode Select

                switch (mode)
                {
                    case ThumbnailModeOption.WH:
                        break;
                    case ThumbnailModeOption.W:
                        if (w < maxWidth)
                        {
                            toW = w;
                            toH = h;
                        }
                        else
                        {
                            toH = h * maxWidth / w;
                        }
                        break;
                    case ThumbnailModeOption.H:
                        if (h < maxheight)
                        {
                            toW = w;
                            toH = h;
                        }
                        else
                        {
                            toW = w * maxheight / h;
                        }
                        break;
                    case ThumbnailModeOption.CUT:
                        if (((double)w / (double)h) > ((double)toW / (double)toH))
                        {
                            w = h * toW / toH;
                            y = 0;
                            x = (image.Width - w) / 2;
                        }
                        else
                        {
                            h = w * toH / toW;
                            x = 0;
                            y = (image.Height - h) / 2;
                        }
                        break;
                    case ThumbnailModeOption.CUSTOM:
                        if (h <= maxheight)
                        {
                            toW = w;
                            toH = h;
                        }
                        else
                        {
                            LogHelper.WriteLog(LogFile.Debug, "【上传图片尺寸不对】w:{0},h:{1}", w, h);
                            goto case ThumbnailModeOption.CUT;
                        }
                        break;
                    default:
                        goto case ThumbnailModeOption.CUT;
                }
                #endregion

                bitmap = new Bitmap(toW, toH);
                graphics = Graphics.FromImage(bitmap);
                graphics.InterpolationMode = InterpolationMode.High;   //设置高质量,低速度呈现平滑程度
                graphics.SmoothingMode = SmoothingMode.HighQuality;    //清空画布并以透明背景色填充
                graphics.Clear(Color.Transparent);
                // 在指定位置并且按指定大小绘制原图片的指定部分
                graphics.DrawImage(image, new Rectangle(0, 0, toW, toH), new Rectangle(x, y, w, h), GraphicsUnit.Pixel);

                // 打文字水印
                //Brush b = new SolidBrush(Color.White);
                //Font f = new Font("宋体", 15);
                //graphics.DrawString("@喵播直播", f, b, new Rectangle(0, 0, toW / 2, toH / 2 + 10));

                // 保存缩略图
                //bitmap.Save(thumbnailPath, GetImageFormat(strFileName));
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                success = true;
            }
            catch (Exception ex)
            {
                success = false;
                throw ex;
            }
            finally
            {
                if (graphics != null) graphics.Dispose();
                if (bitmap != null) bitmap.Dispose();
                if (image != null) image.Dispose();
            }
            return success;
        }

        /// <summary>
        /// 生成缩略图并打水印再保存。
        /// </summary>
        /// <Param name="originalImageStream">Stream</Param>
        /// <Param name="strFileName">源图路径（物理路径）</Param>
        /// <Param name="thumbnailPath">缩略图路径（物理路径）</Param>
        /// <Param name="maxWidth">缩略图最大宽度</Param>
        /// <Param name="maxheight">缩略图最大高度</Param>
        /// <Param name="mode">生成缩略图的方式</Param>
        /// <Param name="waterPath">水印图路径（物理路径）</Param>
        /// <Param name="edge">水印图离原图边界的距离</Param>
        /// <Param name="position">加图片水印的位置</Param>
        /// <returns>是否成功</returns>
        public static bool MakeThumbnailWater(Stream originalImageStream, string strFileName, string thumbnailPath, int maxWidth, int maxheight, ThumbnailModeOption mode, string waterPath, int edge, WaterPositionOption position)
        {
            bool success = false;
            try
            {
                // 生成缩略图
                MakeThumbnail(originalImageStream, strFileName, thumbnailPath, maxWidth, maxheight, mode);
                Stream stream = File.Open(thumbnailPath, FileMode.Open);
                //打水印
                MakeWaterImage(stream, strFileName, thumbnailPath, waterPath, edge, position);
                success = true;
            }
            catch (Exception ex)
            {
                success = false;
                throw;
            }
            return success;
        }

        /// <summary>
        /// 马赛克处理
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="effectWidth"> 影响范围 每一个格子数 </param>
        /// <returns></returns>
        public static Bitmap AdjustTobMosaic(System.Drawing.Bitmap bitmap, int effectWidth)
        {
            // 差异最多的就是以照一定范围取样 玩之后直接去下一个范围
            for (int heightOfffset = 0; heightOfffset < bitmap.Height; heightOfffset += effectWidth)
            {
                for (int widthOffset = 0; widthOffset < bitmap.Width; widthOffset += effectWidth)
                {
                    int avgR = 0, avgG = 0, avgB = 0;
                    int blurPixelCount = 0;

                    for (int x = widthOffset; (x < widthOffset + effectWidth && x < bitmap.Width); x++)
                    {
                        for (int y = heightOfffset; (y < heightOfffset + effectWidth && y < bitmap.Height); y++)
                        {
                            System.Drawing.Color pixel = bitmap.GetPixel(x, y);

                            avgR += pixel.R;
                            avgG += pixel.G;
                            avgB += pixel.B;

                            blurPixelCount++;
                        }
                    }

                    // 计算范围平均
                    avgR = avgR / blurPixelCount;
                    avgG = avgG / blurPixelCount;
                    avgB = avgB / blurPixelCount;


                    // 所有范围内都设定此值
                    for (int x = widthOffset; (x < widthOffset + effectWidth && x < bitmap.Width); x++)
                    {
                        for (int y = heightOfffset; (y < heightOfffset + effectWidth && y < bitmap.Height); y++)
                        {

                            System.Drawing.Color newColor = System.Drawing.Color.FromArgb(avgR, avgG, avgB);
                            bitmap.SetPixel(x, y, newColor);
                        }
                    }
                }
            }

            return bitmap;
        }



        #region 图片压缩

        //生成缩略图函数
        //顺序参数：源图文件流、缩略图存放地址、模版宽、模版高
        //注：缩略图大小控制在模版区域内
        public static void MakeSmallImg(Stream fromFileStream, string fileSaveUrl, System.Double templateWidth, System.Double templateHeight)
        {
            //从文件取得图片对象，并使用流中嵌入的颜色管理信息
            System.Drawing.Image myImage = System.Drawing.Image.FromStream(fromFileStream, true);
            //缩略图宽、高
            System.Double newWidth = myImage.Width, newHeight = myImage.Height;
            //宽大于模版的横图
            if (myImage.Width > myImage.Height || myImage.Width == myImage.Height)
            {
                if (myImage.Width > templateWidth)
                {
                    //宽按模版，高按比例缩放
                    newWidth = templateWidth;
                    newHeight = myImage.Height * (newWidth / myImage.Width);
                }
            }
            //高大于模版的竖图
            else
            {
                if (myImage.Height > templateHeight)
                {
                    //高按模版，宽按比例缩放
                    newHeight = templateHeight;
                    newWidth = myImage.Width * (newHeight / myImage.Height);
                }
            }
            //取得图片大小
            System.Drawing.Size mySize = new Size((int)newWidth, (int)newHeight);
            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(mySize.Width, mySize.Height);
            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空一下画布
            g.Clear(Color.White);
            //在指定位置画图
            g.DrawImage(myImage, new Rectangle(0, 0, bitmap.Width, bitmap.Height), new Rectangle(0, 0, myImage.Width, myImage.Height), GraphicsUnit.Pixel);
            ///文字水印
            //System.Drawing.Graphics G = System.Drawing.Graphics.FromImage(bitmap);
            //System.Drawing.Font f = new Font("宋体", 13);
            //System.Drawing.Brush b = new SolidBrush(Color.Black);
            //G.DrawString("9158直播", f, b, 10, 10);
            //G.Dispose();
            ///图片水印
            //System.Drawing.Image copyImage = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath("pic/1.gif"));
            //Graphics a = Graphics.FromImage(bitmap);
            //a.DrawImage(copyImage, new Rectangle(bitmap.Width-copyImage.Width,bitmap.Height-copyImage.Height,copyImage.Width, copyImage.Height),0,0, copyImage.Width, copyImage.Height, GraphicsUnit.Pixel);
            //copyImage.Dispose();
            //a.Dispose();
            //copyImage.Dispose();
            //保存缩略图
            bitmap.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);
            g.Dispose();
            myImage.Dispose();
            bitmap.Dispose();
        }
        public static void MakeSmallImg(Stream fromFileStream, string fileSaveUrl, System.Double templateWidth, System.Double templateHeight, ref int Widthnew , ref int Heightnew )
        {
            //从文件取得图片对象，并使用流中嵌入的颜色管理信息
            System.Drawing.Image myImage = System.Drawing.Image.FromStream(fromFileStream, true);
            //缩略图宽、高
            System.Double newWidth = myImage.Width, newHeight = myImage.Height;
            //宽大于模版的横图
            if (myImage.Width > myImage.Height || myImage.Width == myImage.Height)
            {
                if (myImage.Width > templateWidth)
                {
                    //宽按模版，高按比例缩放
                    newWidth = templateWidth;
                    newHeight = myImage.Height * (newWidth / myImage.Width);
                }
            }
            //高大于模版的竖图
            else
            {
                if (myImage.Height > templateHeight)
                {
                    //高按模版，宽按比例缩放
                    newHeight = templateHeight;
                    newWidth = myImage.Width * (newHeight / myImage.Height);
                }
            }
            Widthnew = (int)newWidth;
            Heightnew = (int)newHeight;
            //取得图片大小
            System.Drawing.Size mySize = new Size((int)newWidth, (int)newHeight);
            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(mySize.Width, mySize.Height);
            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空一下画布
            g.Clear(Color.White);
            //在指定位置画图
            g.DrawImage(myImage, new Rectangle(0, 0, bitmap.Width, bitmap.Height), new Rectangle(0, 0, myImage.Width, myImage.Height), GraphicsUnit.Pixel);
            ///文字水印
            //System.Drawing.Graphics G = System.Drawing.Graphics.FromImage(bitmap);
            //System.Drawing.Font f = new Font("宋体", 13);
            //System.Drawing.Brush b = new SolidBrush(Color.Black);
            //G.DrawString("9158直播", f, b, 10, 10);
            //G.Dispose();
            ///图片水印
            //System.Drawing.Image copyImage = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath("pic/1.gif"));
            //Graphics a = Graphics.FromImage(bitmap);
            //a.DrawImage(copyImage, new Rectangle(bitmap.Width-copyImage.Width,bitmap.Height-copyImage.Height,copyImage.Width, copyImage.Height),0,0, copyImage.Width, copyImage.Height, GraphicsUnit.Pixel);
            //copyImage.Dispose();
            //a.Dispose();
            //copyImage.Dispose();
            //保存缩略图
            bitmap.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);
            g.Dispose();
            myImage.Dispose();
            bitmap.Dispose();
        }

        #endregion
    }
}