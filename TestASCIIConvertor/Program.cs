using System;
using System.Windows.Forms;
using ASCIIConvertor;


namespace TestASCIIConvertor
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var convertor = new ASCIIConvertor.ASCIIConvertor();

            var openFileDialog = new OpenFileDialog
            {
                 Filter = "Images | *.bmp; *.png; *.jpg; *.JPEG"
            };

            Console.WriteLine("Нажмите Enter, чтобы начать...\n");

            while (true)
            {
                Console.ReadLine();

                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    continue;

                Console.Clear();

                convertor.OpenImg(openFileDialog.FileName);
                convertor.ResizeBitmap();
                var rows = convertor.Convert();
                foreach(var row in rows)
                {
                    Console.WriteLine(row);
                }

                Console.SetCursorPosition(0, 0);
            }
        }
    }
}
