using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows.Forms;

namespace VantaZeroClear
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.Favicon;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!IsAdministrator())
            {
                MessageBox.Show("This application requires administrator privileges.", "VANTA Zero Clear", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool cleanTemp = chkCleanTemp.Checked;
            bool clearLogs = chkClearLogs.Checked;
            bool shadow = chkShadow.Checked;
            bool recycle = chkRecycle.Checked;
            bool cipher = chkCipher.Checked;

            if (cleanTemp)
            {
                RunCommand("cmd.exe", "/c del /f /s /q %TEMP%\* & del /f /s /q %SystemRoot%\Temp\*");
            }
            if (clearLogs)
            {
                RunCommand("wevtutil", "cl Application");
                RunCommand("wevtutil", "cl System");
                RunCommand("wevtutil", "cl Security");
            }
            if (shadow)
            {
                RunCommand("vssadmin", "delete shadows /for=C: /all /quiet");
            }
            if (recycle)
            {
                RunCommand("powershell.exe", "-Command Clear-RecycleBin -Force");
            }
            if (cipher)
            {
                RunCommand("cmd.exe", "/c cipher /w:%SystemDrive%");
            }

            MessageBox.Show("Cleanup completed.", "VANTA Zero Clear", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void RunCommand(string fileName, string arguments)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = arguments,
                    CreateNoWindow = true,
                    UseShellExecute = false
                }
            };
            process.Start();
            process.WaitForExit();
        }
    }
}
