using AForge.Video;
using AForge.Video.DirectShow;
using ASCIIConvertor;
using Settings;
using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ASCIICamera
{
    class Program
    {
        private static IPEndPoint consumerationEndPoint;
        private static UdpClient udpClient = new UdpClient();
        static void Main(string[] args)
        {
            Application.Run(new Settings.Settings());
            Listener();
        }

        static void Listener()
        {
            var ip = ConfigurationManager.AppSettings.Get("socket").Split(':')[0];
            var port = int.Parse(ConfigurationManager.AppSettings.Get("socket").Split(':')[1]);
            consumerationEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            FilterInfoCollection videoDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevice[0].MonikerString);

            videoSource.NewFrame += VideoSource_NewFrame;
            videoSource.Start();

            Console.ReadLine();
        }

        private static void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            var bmp = new Bitmap(eventArgs.Frame, 800, 600);

            try
            {
                using var ms = new MemoryStream();
                bmp.Save(ms, ImageFormat.Jpeg);
                var bytes = ms.ToArray();

                udpClient.Send(bytes, bytes.Length, consumerationEndPoint);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
             
    }
}
