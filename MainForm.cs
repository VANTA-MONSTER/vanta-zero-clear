using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Principal;
using System.Windows.Forms;

namespace VantaZeroClear
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            cbLang.SelectedIndex = 0;
            ApplyLanguage();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Закруглённые углы
            var path = new GraphicsPath();
            int radius = 12;
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(Width - radius, Height - radius, radius, radius, 0, 90);
            path.AddArc(0, Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            Region = new Region(path);
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if (ClientRectangle.Width <= 0 || ClientRectangle.Height <= 0)
                return;

            // Градиентный фон
            using (var brush = new LinearGradientBrush(
                ClientRectangle,
                Color.FromArgb(30, 30, 30),
                Color.FromArgb(50, 50, 50),
                90F))
            {
                e.Graphics.FillRectangle(brush, ClientRectangle);
            }
            // Неоновая линия под заголовком
            using (var pen = new Pen(Color.Cyan, 2))
            {
                int y = cbLang.Bottom + 4;
                e.Graphics.DrawLine(pen, 0, y, Width, y);
            }
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            // Перетаскивание окна
            if (e.Button == MouseButtons.Left)
            {
                this.Capture = false;
                var msg = Message.Create(Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                WndProc(ref msg);
            }
        }

        private void cbLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyLanguage();
        }

        private void ApplyLanguage()
        {
            bool rus = cbLang.SelectedIndex == 0;
            // Заголовок
            this.Text = "VANTA Zero Clear";
            // Чекбоксы
            chkCleanTemp.Text = rus ? "Очистка временных файлов" : "Clean temporary files";
            chkClearLogs.Text  = rus ? "Очистка логов"            : "Clean event logs";
            chkShadow.Text     = rus ? "Удаление Shadow Copies"   : "Remove Shadow Copies";
            chkRecycle.Text    = rus ? "Очистка корзины"          : "Empty Recycle Bin";
            chkCipher.Text     = rus ? "Cipher wipe"              : "Cipher wipe";
            // Кнопка
            btnStart.Text      = rus ? "Запустить"                : "Start";
        }

        private async void btnStart_Click(object sender, EventArgs e)
{
    bool rus = cbLang.SelectedIndex == 0;
    string title = rus ? "Подтвердите действие" : "Confirm action";
    string msg = rus ? "Вы уверены, что хотите продолжить?" : "Are you sure you want to proceed?";
    if (MessageBox.Show(msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;

    btnStart.Enabled = false;
    lblStatus.Text = rus ? "Выполняется очистка..." : "Processing cleanup...";
    btnStart.Text = rus ? "Выполняется..." : "Processing...";

    await Task.Run(() =>
    {
        if (chkCleanTemp.Checked)
        {
            Invoke(new Action(() => lblStatus.Text = rus ? "Удаление временных файлов..." : "Deleting temp files..."));
            RunCommand("cmd.exe", @"/c del /f /s /q %TEMP%\* & del /f /s /q %SystemRoot%\Temp\*");
        }
        if (chkClearLogs.Checked)
        {
            Invoke(new Action(() => lblStatus.Text = rus ? "Очистка логов..." : "Cleaning logs..."));
            RunCommand("wevtutil", "cl Application");
            RunCommand("wevtutil", "cl System");
            RunCommand("wevtutil", "cl Security");
        }
        if (chkShadow.Checked)
        {
            Invoke(new Action(() => lblStatus.Text = rus ? "Удаление теневых копий..." : "Deleting shadow copies..."));
            RunCommand("vssadmin", "delete shadows /for=C: /all /quiet");
        }
        if (chkRecycle.Checked)
        {
            Invoke(new Action(() => lblStatus.Text = rus ? "Очистка корзины..." : "Emptying Recycle Bin..."));
            RunCommand("powershell.exe", "-Command Clear-RecycleBin -Force");
        }
        if (chkCipher.Checked)
        {
            Invoke(new Action(() => lblStatus.Text = rus ? "Затирание свободного места..." : "Wiping free space..."));
            RunCommand("cmd.exe", "/c cipher /w:%SystemDrive%");
        }
    });

    lblStatus.Text = rus ? "Готово!" : "Done!";
    btnStart.Text = rus ? "Запустить" : "Start";
    btnStart.Enabled = true;

    MessageBox.Show(
        rus ? "Очистка завершена." : "Cleanup completed.",
        "VANTA",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information
    );
}


        private void RunCommand(string fileName, string arguments)
        {
            var psi = new ProcessStartInfo(fileName, arguments)
            {
                CreateNoWindow  = true,
                UseShellExecute = false
            };
            using (var p = Process.Start(psi))
                p.WaitForExit();
        }

        private void pbInfoCleanTemp_Click(object sender, EventArgs e) => ShowInfo(0);
        private void pbInfoClearLogs_Click(object sender, EventArgs e) => ShowInfo(1);
        private void pbInfoShadow_Click(object sender, EventArgs e) => ShowInfo(2);
        private void pbInfoRecycle_Click(object sender, EventArgs e) => ShowInfo(3);
        private void pbInfoCipher_Click(object sender, EventArgs e) => ShowInfo(4);

        private void ShowInfo(int idx)
        {
            bool rus = cbLang.SelectedIndex == 0;
            string title, msg;
            switch (idx)
            {
                case 0:
                    title = rus ? "Очистка временных файлов" : "Clean temporary files";
                    msg   = rus
                        ? "Удаляет все файлы из папок %TEMP% и %SystemRoot%\\Temp%."
                        : "Deletes all files in the %TEMP% and %SystemRoot%\\Temp% folders.";
                    break;
                case 1:
                    title = rus ? "Очистка логов" : "Clean event logs";
                    msg   = rus
                        ? "Очищает журналы событий Application, System, Security."
                        : "Clears the Application, System and Security event logs.";
                    break;
                case 2:
                    title = rus ? "Удаление Shadow Copies" : "Remove Shadow Copies";
                    msg   = rus
                        ? "Удаляет все теневые копии (VSS) тома C:."
                        : "Deletes all shadow copies (VSS) on the C: volume.";
                    break;
                case 3:
                    title = rus ? "Очистка корзины" : "Empty Recycle Bin";
                    msg   = rus
                        ? "Полностью очищает системную корзину."
                        : "Empties the Recycle Bin completely.";
                    break;
                default:
                    title = rus ? "Cipher wipe" : "Cipher wipe";
                    msg   = rus
                        ? "Затирает свободное пространство на диске командой cipher /w:."
                        : "Wipes free space on the disk using cipher /w:.";
                    break;
            }
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
