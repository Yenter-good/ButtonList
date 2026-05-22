using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ButtonList.Helper
{
    internal static class BitmapColorHelper
    {
        public static Bitmap ToGray(this Bitmap original)
        {
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);//建立空白bitmap
            Graphics g = Graphics.FromImage(newBitmap);
            //g.Clear(Color.Black);

            ColorMatrix CM0 = new ColorMatrix(
            new float[][]
            {
                new float[]{0,0,0,0,0 },
                new float[]{0,0,0,0,0 },
                new float[]{0,0,0,0,0 },
                new float[]{0,0,0,1,0 },
                new float[]{0.5f,0.5f,0.5f,0,1 }
            });

            //ColorMatrix CM1 = new ColorMatrix(
            //new float[][]
            //{
            //      new float[]{.30f,.30f,.30f,0,0 },
            //      new float[]{.59f,.59f,.59f,0,0 },
            //      new float[]{.11f,.11f,.11f,0,0 },
            //      new float[]{0,0,0,1,0 },
            //      new float[]{0,0,0,0,1 }
            //});


            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(CM0);
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            g.Dispose();
            return newBitmap;
        }

        /// <summary>
        /// 使用 Marshal.Copy 高性能去除黑色背景
        /// </summary>
        /// <param name="originalImage">原始图片</param>
        /// <param name="threshold">黑色阈值 (0-255)</param>
        /// <returns>处理后的 Bitmap</returns>
        public static Bitmap RemoveBlackBackground(this Bitmap originalImage, int threshold = 0)
        {
            // 1. 创建一个新的 Bitmap 用于存放结果，格式必须是带 Alpha 通道的 (Format32bppArgb)
            Bitmap resultImage = new Bitmap(originalImage.Width, originalImage.Height, PixelFormat.Format32bppArgb);

            // 2. 锁定原始图片的内存，只读
            Rectangle rect = new Rectangle(0, 0, originalImage.Width, originalImage.Height);

            // 锁定源图像
            BitmapData sourceData = originalImage.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            // 锁定目标图像
            BitmapData destData = resultImage.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            try
            {
                // 3. 计算字节数组的大小 (宽度 * 高度 * 每个像素的字节数)
                int bytes = Math.Abs(sourceData.Stride) * originalImage.Height;
                byte[] sourceBytes = new byte[bytes];
                byte[] destBytes = new byte[bytes];

                // 4. 使用 Marshal.Copy 将非托管内存拷贝到托管字节数组
                Marshal.Copy(sourceData.Scan0, sourceBytes, 0, bytes);

                // 5. 遍历字节数组进行处理
                // 注意：Stride 可能大于 Width * 4，因为内存对齐。
                // 我们通常只需要处理 Width * 4 的部分，或者按行处理。
                int stride = sourceData.Stride;
                int width = originalImage.Width;
                int height = originalImage.Height;

                for (int y = 0; y < height; y++)
                {
                    // 计算当前行的起始位置
                    int rowOffset = y * stride;

                    for (int x = 0; x < width; x++)
                    {
                        // 计算当前像素在数组中的索引 (BGRA 格式)
                        int i = rowOffset + x * 4;

                        byte b = sourceBytes[i];     // Blue
                        byte g = sourceBytes[i + 1]; // Green
                        byte r = sourceBytes[i + 2]; // Red
                        byte a = sourceBytes[i + 3]; // Alpha

                        // --- 核心逻辑开始 ---

                        // 计算亮度
                        int brightness = (r + g + b) / 3;

                        if (brightness < threshold)
                        {
                            // 如果是黑色：将 Alpha 设为 0 (完全透明)
                            // 同时为了防止边缘锯齿发黑，通常将 RGB 设为 255 (白色) 或保持原样
                            destBytes[i] = 255;     // B
                            destBytes[i + 1] = 255; // G
                            destBytes[i + 2] = 255; // R
                            destBytes[i + 3] = 0;   // A (透明)
                        }
                        else
                        {
                            // 如果不是黑色：保持原样
                            destBytes[i] = b;
                            destBytes[i + 1] = g;
                            destBytes[i + 2] = r;
                            destBytes[i + 3] = a;


                        }
                        // --- 核心逻辑结束 ---
                    }
                }

                // 6. 将处理好的字节数组拷贝回非托管内存
                Marshal.Copy(destBytes, 0, destData.Scan0, bytes);
            }
            finally
            {
                // 7. 务必解锁内存，否则图片会被占用
                originalImage.UnlockBits(sourceData);
                resultImage.UnlockBits(destData);
            }

            return resultImage;
        }

    }
}
