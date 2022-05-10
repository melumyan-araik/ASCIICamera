using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ASCIIConvertor
{
    interface IASSCIIConvertor
    {
        /// <summary>
        /// Открытие картинки
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Bitmap OpenImg(string path);

        /// <summary>
        /// Изменение размера картинки по ширине
        /// </summary>
        /// <param name="bitmapBegin"></param>
        /// <param name="maxWidth"></param>
        /// <param name="widthOffset"></param>
        /// <returns></returns>
        Bitmap ResizeBitmap(Bitmap bitmapBegin, int maxWidth, double widthOffset);
        
        /// <summary>
        /// Возвращает картинку в градиенте серого
        /// </summary>
        /// <param name="bitmapBegin"></param>
        /// <returns></returns>
        public Bitmap ToGrayscale(Bitmap bitmapBegin);
    }
}
