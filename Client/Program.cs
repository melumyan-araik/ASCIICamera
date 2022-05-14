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
        private static UdpClient udpClient = new UdpClient();
        [STAThread]
        static void Main(string[] args)
        {
            Application.Run(new Settings.Settings());
            Listener();
        }
        static async void Listener()
        {
            var convertor = new ASCIIConvertor.ASCIIConvertor();
            try
            {
                var ip = ConfigurationManager.AppSettings.Get("socket").Split(':')[0];
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

                        //Console.Clear();
                        foreach (var row in rows)
                        {
                            Console.WriteLine(row);
                        }
                        Console.SetCursorPosition(0, 0);
                    }
                    Console.Title = $"Получено байт: {data.Buffer.Length * sizeof(byte)}; Адрес источника {port}";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }


    }
}
