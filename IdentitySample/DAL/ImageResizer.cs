using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;

namespace IdentitySample.DAL
{
    public static class ImageResizer
    {
        public static byte[] ResizeImage(byte[] image, int width)
        {
            Bitmap bmp = new Bitmap(new MemoryStream(image));
            Double ratio = width / (double)bmp.Width;
            int newWidth = (int)(Math.Truncate(bmp.Width * ratio));
            int newHeight = (int)(Math.Truncate(bmp.Height * ratio));

            Bitmap newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.DrawImage(bmp, 0, 0, newWidth, newHeight);
                MemoryStream streamstore = new MemoryStream();
                newImage.Save(streamstore, System.Drawing.Imaging.ImageFormat.Jpeg);
                image = streamstore.ToArray();
            }
            return image;
        }
    }
}