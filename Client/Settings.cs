using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Client
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            var socket = ConfigurationManager.AppSettings.Get("socket");
            if (socket != null)
            {
                textBoxIp.Text = socket.Split(':')[0];
                textBoxPort.Text = socket.Split(':')[1];
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            var checkIp = false;
            var checkPort = false;
            Regex regIP = new Regex(@"\b(([01]?\d?\d|2[0-4]\d|25[0-5])\.){3}([01]?\d?\d|2[0-4]\d|25[0-5])\b");
            if (regIP.IsMatch(textBoxIp.Text))
            {
                checkIp = true;
            }
            else
            {
                errorIp.Text = "Неверный Ip";
            }

            if (int.Parse(textBoxPort.Text) >= 0 && int.Parse(textBoxPort.Text) < 65536)
            {
                checkPort = true;
            }
            else
            {
                errorPort.Text = "Неверный порт";
            }

            if (checkPort && checkIp)
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings["socket"] == null)
                {
                    settings.Add("socket", $"{textBoxIp.Text}:{textBoxPort.Text}");
                }
                else
                {
                    settings["socket"].Value = $"{textBoxIp.Text}:{textBoxPort.Text}";
                }

                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);

                Close();
            }
        }

        private void textBoxPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void textBoxIp_Enter(object sender, EventArgs e)
        {
            errorIp.Text = "";
        }

        private void textBoxPort_Enter(object sender, EventArgs e)
        {
            errorPort.Text = "";
        }
    }
}
