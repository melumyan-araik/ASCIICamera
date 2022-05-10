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
        public void OpenImg(string path);

        /// <summary>
        /// Изменение размера картинки по ширине
        /// </summary>
        /// <param name="bitmapBegin"></param>
        /// <param name="maxWidth"></param>
        /// <param name="widthOffset"></param>
        /// <returns></returns>
        public void ResizeBitmap(int maxWidth, double widthOffset);
                
        /// <summary>
        /// Преобразовывает bitmap в массив ascii char
        /// </summary>
        /// <param name="bitmapBegin"></param>
        /// <returns></returns>
        public char[][] Convert();

        /// <summary>
        ///  Преобразовывает bitmap в массив ascii char в негативе
        /// </summary>
        /// <param name="bitmapBegin"></param>
        /// <returns></returns>
        public char[][] ConvertNegative();
    }
}
