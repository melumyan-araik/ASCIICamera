using System;
using System.Drawing;
using System.IO;

namespace ASCIIConvertor
{
    public class ASCIIConvertor : IASSCIIConvertor
    {
        private readonly string[] IMG_EXTENTION = { ".bmp", ".png", ".jpg", ".JPEG" };
        private readonly char[] ASCII_TABLE = { '.', ',', ':', '+', '*', '?', '%', '$', '#', '@' };
        private readonly char[] ASCII_TABLE_NEGATIVE = { '@', '#', '$', '%', '?', '*', '+', ':', ',', '.' };
        public Bitmap bitmap { set; get; }
        public ASCIIConvertor()
        {

        }
        public ASCIIConvertor(string path)
        {
            OpenImg(path);
        }

        public ASCIIConvertor(Bitmap bm)
        {
            OpenImg(bm);
        }
        public void OpenImg(string path)
        {
            try
            {
                var exten = Path.GetExtension(path);
                if (!CheckExtention(exten, IMG_EXTENTION))
                    throw new Exception("Тип не поддерживается!");
                bitmap = new Bitmap(path);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void OpenImg(Bitmap bm)
        {
            try
            {
                bitmap = bm;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void ResizeBitmap(int maxWidth = 350, double widthOffset = 1.5)
        {
            try
            {
                var newHeight = bitmap.Height / widthOffset * maxWidth / bitmap.Width;
                if (bitmap.Width > maxWidth || bitmap.Height > newHeight)
                    bitmap = new Bitmap(bitmap, new Size(maxWidth, (int)newHeight));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public char[][] Convert()
        {
            return Convert(bitmap, ASCII_TABLE);
        }
        public char[][] ConvertNegative()
        {
            return Convert(bitmap, ASCII_TABLE_NEGATIVE);
        }
        private char[][] Convert(Bitmap bitmapBegin, char[] asciiTable)
        {
            ToGrayscale();
            var result = new char[bitmapBegin.Height][];
            for (var y = 0; y < bitmapBegin.Height; y++)
            {
                result[y] = new char[bitmapBegin.Width];
                for (var x = 0; x < bitmapBegin.Width; x++)
                {
                    int mapIndex = (int)Map(bitmapBegin.GetPixel(x, y).R, 0, 255, 0, asciiTable.Length - 1);
                    result[y][x] = asciiTable[mapIndex];
                }
            }
            return result;
        }
        private void ToGrayscale()
        {
            for (var y = 0; y < bitmap.Height; y++)
            {
                for (var x = 0; x < bitmap.Width; x++)
                {
                    var pixel = bitmap.GetPixel(x, y);
                    int avg = (pixel.R + pixel.G + pixel.B) / 3;
                    bitmap.SetPixel(x, y, Color.FromArgb(pixel.A, avg, avg, avg));
                }
            }
        }
        private float Map(float valueToMap, float start1, float stop1, float start2, float stop2)
        {
            return ((valueToMap - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
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
