using System;
using System.Drawing;
using System.IO;

namespace ASCIIConvertor
{
    public class ASCIIConvertor : IASSCIIConvertor
    {
        public static readonly string[] IMG_EXTENTION = { ".bmp", ".png", ".jpg", ".JPEG" };

        public Bitmap OpenImg(string path)
        {
            try
            {
                var exten = Path.GetExtension(path);
                if (!CheckExtention(exten, IMG_EXTENTION))
                    throw new Exception("Тип не поддерживается!");
                return new Bitmap(path);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Bitmap ResizeBitmap(Bitmap bitmapBegin, int maxWidth, double widthOffset = 1.5)
        {
            try
            {
                var newHeight = bitmapBegin.Height / widthOffset * maxWidth / bitmapBegin.Width;
                if (bitmapBegin.Width > maxWidth || bitmapBegin.Height > newHeight)
                    bitmapBegin = new Bitmap(bitmapBegin, new Size(maxWidth, (int)newHeight));
                return bitmapBegin;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Bitmap ToGrayscale(Bitmap bitmapBegin)
        {
            for(var y = 0; y < bitmapBegin.Height; y++)
            {
                for (var x = 0; x < bitmapBegin.Width; x++)
                {
                    var pixel = bitmapBegin.GetPixel(x, y);
                    int avg = (pixel.R + pixel.G + pixel.B) / 3;
                    bitmapBegin.SetPixel(x, y, Color.FromArgb(pixel.A, avg, avg, avg));
                }
            }

            return bitmapBegin;
        }

        private bool CheckExtention(string ext, string[] extentionArr)
        {
            foreach (var item in extentionArr)
            {
                if (ext == item)
                    return true;
            }
            return false;
        }
    }
}
