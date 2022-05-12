using Settings;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.Run(new Settings.Settings());
            Listener();
        }
        static async void Listener()
        {
            var convertor = new ASCIIConvertor.ASCIIConvertor();

            var port = int.Parse(ConfigurationManager.AppSettings.Get("socket").Split(':')[1]);
            var client = new UdpClient(port);
            while (true)
            {
                var data = await client.ReceiveAsync();
                using (var ms = new MemoryStream(data.Buffer))
                {
                    convertor.OpenImg(new Bitmap(ms));
                    convertor.ResizeBitmap();
                    var rows = convertor.Convert();
                    foreach (var row in rows)
                    {
                        Console.WriteLine(row);
                    }
                }
                Console.Title = $"Получено байт: {data.Buffer.Length * sizeof(byte)}";
            }
        }


    }
}
