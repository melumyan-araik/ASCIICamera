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
            Console.ReadLine();

            Application.Run(new Settings());



            Console.ReadLine();
        }
    }
}
