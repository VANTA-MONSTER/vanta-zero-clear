using System;
using System.Reflection; 
using System.IO;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace VantaZeroClear
{
    partial class MainForm
    {
        private IContainer components = null;
        private Label lblStatus;
        private ComboBox cbLang;
        private CheckBox chkCleanTemp, chkClearLogs, chkShadow, chkRecycle, chkCipher;
        private PictureBox pbInfoCleanTemp, pbInfoClearLogs, pbInfoShadow, pbInfoRecycle, pbInfoCipher;
        private Button btnStart;
        private ToolTip toolTip1;
        // MacOS-style traffic lights
        private Panel pnlTraffic;
        private PictureBox btnClose, btnMinimize, btnMaximize;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.toolTip1  = new ToolTip(this.components);

            // Panel for traffic lights
            this.pnlTraffic = new Panel
            {
                Size = new Size(90, 30),
                Location = new Point(10, 10),
                BackColor = Color.Transparent
            };

            // Traffic lights buttons
            btnClose    = CreateCircleButton(Color.FromArgb(255, 95, 86), 0);
            btnMinimize = CreateCircleButton(Color.FromArgb(255, 189, 46), 25);
            btnMaximize = CreateCircleButton(Color.FromArgb(39, 201, 63), 50);
            pnlTraffic.Controls.AddRange(new Control[]{ btnClose, btnMinimize, btnMaximize });

            // ComboBox языка
            this.cbLang = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Items         = { "Русский", "English" },
                SelectedIndex = 0,
                Location      = new Point(110, 12),
                Size          = new Size(150, 24),
                Font          = new Font("Segoe UI", 9F),
                ForeColor     = Color.White,
                BackColor     = Color.FromArgb(40, 40, 40)
            };
            this.cbLang.SelectedIndexChanged += cbLang_SelectedIndexChanged;

            // Чекбоксы
            this.chkCleanTemp = CreateCheckBox(20, 60);
            this.chkClearLogs = CreateCheckBox(20, 90);
            this.chkShadow    = CreateCheckBox(20,120);
            this.chkRecycle   = CreateCheckBox(20,150);
            this.chkCipher    = CreateCheckBox(20,180);

            // Инфо-иконки (уменьшены до 10x10)
            this.pbInfoCleanTemp  = CreateInfoIcon(320, 62, pbInfoCleanTemp_Click);
            this.pbInfoClearLogs  = CreateInfoIcon(320, 92, pbInfoClearLogs_Click);
            this.pbInfoShadow     = CreateInfoIcon(320,122, pbInfoShadow_Click);
            this.pbInfoRecycle    = CreateInfoIcon(320,152, pbInfoRecycle_Click);
            this.pbInfoCipher     = CreateInfoIcon(320,182, pbInfoCipher_Click);

            // Кнопка запуска
            this.btnStart = new Button
            {
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.DimGray,
                ForeColor = Color.White,
                Font      = new Font("Segoe UI", 9F),
                Location  = new Point(20, 220),
                Size      = new Size(310, 30)
            };
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.Click += btnStart_Click;

            // Настройки формы
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.BackColor       = Color.FromArgb(30, 30, 30);
            this.ClientSize      = new Size(360, 280);
            this.Padding         = new Padding(2);
            this.Load           += MainForm_Load;
            this.Paint          += MainForm_Paint;
            this.MouseDown      += MainForm_MouseDown; // enable drag

            // Добавляем контролы
            this.Controls.AddRange(new Control[]{
                pnlTraffic,
                cbLang,
                chkCleanTemp, pbInfoCleanTemp,
                chkClearLogs, pbInfoClearLogs,
                chkShadow,    pbInfoShadow,
                chkRecycle,   pbInfoRecycle,
                chkCipher,    pbInfoCipher,
                btnStart
            });
            // lblStatus — индикатор статуса
this.lblStatus = new Label
{
    Text = "",
    AutoSize = true,
    ForeColor = Color.Cyan,
    Font = new Font("Segoe UI", 9F),
    Location = new Point(22, 260)
};
this.Controls.Add(lblStatus);

            this.ResumeLayout(false);

            // Устанавливаем тексты и состояния
            cbLang_SelectedIndexChanged(this, EventArgs.Empty);
        }

        // Создание чекбокса
        private CheckBox CreateCheckBox(int x, int y)
            => new CheckBox {
                AutoSize  = true,
                Location  = new Point(x, y),
                Font      = new Font("Segoe UI", 9F),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };

        // Создание инфо-иконки
        private PictureBox CreateInfoIcon(int x, int y, EventHandler onClick)
{
    // получаем сборку
    var asm = Assembly.GetExecutingAssembly();
    // загружаем встроенный ресурс по логическому имени
    using var stream = asm.GetManifestResourceStream("VantaZeroClear.info.png");
    if (stream == null)
        throw new InvalidOperationException("Не найден ресурс info.png");

    var img = Image.FromStream(stream);

    var pb = new PictureBox
    {
        Image        = img,
        SizeMode     = PictureBoxSizeMode.StretchImage,
        Size         = new Size(16, 16),
        Location     = new Point(x, y),
        Cursor       = Cursors.Hand,
        BackColor    = Color.Transparent
    };
    pb.Click += onClick;
    toolTip1.SetToolTip(pb, "Что это делает? / What this does?");
    return pb;
}



        // Создание MacOS-style кнопки
        private PictureBox CreateCircleButton(Color color, int offset)
        {
            var btn = new PictureBox
            {
                Size     = new Size(12,12),
                Location = new Point(offset, 9),
                BackColor = color,
                Cursor    = Cursors.Hand
            };
            btn.Paint += (s, e) =>
            {
                using (var b = new SolidBrush(color))
                    e.Graphics.FillEllipse(b, 0, 0, 12, 12);
            };
            if (offset == 0)  btn.Click += (s, e) => this.Close();
            if (offset == 25) btn.Click += (s, e) => this.WindowState = FormWindowState.Minimized;
            if (offset == 50) btn.Click += (s, e) => 
                this.WindowState = this.WindowState == FormWindowState.Maximized
                    ? FormWindowState.Normal : FormWindowState.Maximized;
            return btn;
        }
    }
}
