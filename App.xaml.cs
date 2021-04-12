using System;
using System.Drawing;
using System.Windows;
using wf = System.Windows.Forms;

namespace NotifyIconOnlyWPF
{
    public partial class App : Application
    {
        private wf.NotifyIcon trayIcon;
        private bool isShieldIcon = true;
        private Icon shieldIcon = new Icon(SystemIcons.Shield, 40, 40);
        private Icon warningIcon = new Icon(SystemIcons.Warning, 40, 40);

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow = new MainWindow();
            MainWindow.Hide();
            SetupNotifyIcon();
        }
        private void SetupNotifyIcon(){
            trayIcon = new wf.NotifyIcon();
            trayIcon.Icon = shieldIcon;
            trayIcon.Visible = true;
            trayIcon.DoubleClick += (s, e) => OnDoubleClick(s,e);
            trayIcon.ContextMenuStrip = new wf.ContextMenuStrip();
            trayIcon.ContextMenuStrip.Items.Add("Do Something?").Click += (s, e) => OnClick(s,e);
            trayIcon.ContextMenuStrip.Items.Add($"Exit").Click += (s, e) => OnExit(s,e);
            trayIcon.ShowBalloonTip(500, "Status", "Started Up!", System.Windows.Forms.ToolTipIcon.Info);
        }
        private void OnClick(object sender, EventArgs e)
        {
            MessageBox.Show("You did something!","Congrats!");
        }
        private void OnDoubleClick(object sender, EventArgs e)
        {
            isShieldIcon = !isShieldIcon;
            trayIcon.Icon = isShieldIcon ? shieldIcon : warningIcon;
        }
        private void OnExit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            trayIcon.Dispose();
            MainWindow.Close();
        }
    }
}
