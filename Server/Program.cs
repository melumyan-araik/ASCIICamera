using ASCIIConvertor;
using Settings;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace ASCIICamera
{
    class Program
    {
        private static IPEndPoint consumerationEndPoint;
        static void Main(string[] args)
        {
            Application.Run(new Settings.Settings());
            Listener();
        }

        static async void Listener()
        {
            var convertor = new ASCIIConvertor.ASCIIConvertor();

            var ip = ConfigurationManager.AppSettings.Get("socket").Split(':')[0];
            var port = int.Parse(ConfigurationManager.AppSettings.Get("socket").Split(':')[1]);
            consumerationEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            
            FilterInfoCollection 
                using (var ms = new MemoryStream(data.Buffer))
                {
                    convertor.OpenImg(new Bitmap(ms));
                    convertor.ResizeBitmap();
                    var rows = convertor.Convert();
                    foreach (var row in rows)
                    {
                        Console.WriteLine(row);
                    }
                
                Console.Title = $"Получено байт: {data.Buffer.Length * sizeof(byte)}";
            }

        }
    }
}
