using IdentitySample.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;

namespace IdentitySample.Controllers
{
    public class BreadCrumbItem
    {
        public BreadCrumbItem()
        {
            Routes = new Dictionary<int, string>();
        }
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public Dictionary<int, string> Routes { get; set; }
    }

    public class Button : Entity<int>
    {
        public string Caption { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Target { get; set; }
        public string Type { get; set; }
    }

    public class PageHeader
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string GroupName { get; set; }
        public List<string> Description { get; set; }
        public List<Button> Buttons { get; set; }
        public List<BreadCrumbItem> BreadCrumbs { get; set; }

        public PageHeader()
        {
            Description = new List<string>();
            Buttons = new List<Button>();
        }
    }

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

    public static class MailTemplateBody
    {
        public static string GetBody(string FullName, string TextBody)
        {
            string path = string.Format("{0}EmailTemplate.htm", HttpContext.Current.Server.MapPath("~/"));
            StreamReader srTemplate = new StreamReader(path);
            string body = srTemplate.ReadToEnd();
            return body.Replace("@FullName", FullName).Replace("@TextBody",TextBody).Replace("@Date", string.Format("{0:d}",DateTime.Now )) ;
        }
    }
}