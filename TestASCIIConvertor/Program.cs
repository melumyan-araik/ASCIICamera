using System;
using System.IO;
using System.Linq;
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

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Text files (*.txt) | *.txt" 
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
                foreach (var row in rows)
                {
                    Console.WriteLine(row);
                }

                Console.SetCursorPosition(0, 0);

                DialogResult dr = MessageBox.Show("Хотите сохранить результат в файл?",
                      "Сохранить", MessageBoxButtons.YesNo);
                switch (dr)
                {
                    case DialogResult.Yes:
                        if (saveFileDialog.ShowDialog() != DialogResult.OK)
                            break;
                        var rowsNegativ = convertor.ConvertNegative();
                        File.WriteAllLines(saveFileDialog.FileName, rowsNegativ.Select(r => new string(r))) ;
                        break;
                    case DialogResult.No:
                        break;
                }
            }
        }
    }
}
