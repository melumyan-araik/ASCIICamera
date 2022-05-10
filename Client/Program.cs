using System;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.ReadLine();

            Application.Run(new Settings());



            Console.ReadLine();
        }
    }
}
