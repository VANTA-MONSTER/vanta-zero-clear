namespace VantaZeroClear
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cbLang;
        private System.Windows.Forms.CheckBox chkCleanTemp;
        private System.Windows.Forms.CheckBox chkClearLogs;
        private System.Windows.Forms.CheckBox chkShadow;
        private System.Windows.Forms.CheckBox chkRecycle;
        private System.Windows.Forms.CheckBox chkCipher;
        private System.Windows.Forms.Button btnStart;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cbLang = new System.Windows.Forms.ComboBox();
            this.chkCleanTemp = new System.Windows.Forms.CheckBox();
            this.chkClearLogs = new System.Windows.Forms.CheckBox();
            this.chkShadow = new System.Windows.Forms.CheckBox();
            this.chkRecycle = new System.Windows.Forms.CheckBox();
            this.chkCipher = new System.Windows.Forms.CheckBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbLang
            // 
            this.cbLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLang.FormattingEnabled = true;
            this.cbLang.Items.AddRange(new object[] { "Русский", "English" });
            this.cbLang.Location = new System.Drawing.Point(12, 12);
            this.cbLang.Name = "cbLang";
            this.cbLang.Size = new System.Drawing.Size(200, 24);
            this.cbLang.TabIndex = 0;
            // 
            // chkCleanTemp
            // 
            this.chkCleanTemp.AutoSize = true;
            this.chkCleanTemp.ForeColor = System.Drawing.Color.White;
            this.chkCleanTemp.Location = new System.Drawing.Point(12, 50);
            this.chkCleanTemp.Name = "chkCleanTemp";
            this.chkCleanTemp.Size = new System.Drawing.Size(207, 21);
            this.chkCleanTemp.TabIndex = 1;
            this.chkCleanTemp.Text = "Очистка временных файлов";
            this.chkCleanTemp.UseVisualStyleBackColor = true;
            // 
            // chkClearLogs
            // 
            this.chkClearLogs.AutoSize = true;
            this.chkClearLogs.ForeColor = System.Drawing.Color.White;
            this.chkClearLogs.Location = new System.Drawing.Point(12, 77);
            this.chkClearLogs.Name = "chkClearLogs";
            this.chkClearLogs.Size = new System.Drawing.Size(140, 21);
            this.chkClearLogs.TabIndex = 2;
            this.chkClearLogs.Text = "Очистка логов";
            this.chkClearLogs.UseVisualStyleBackColor = true;
            // 
            // chkShadow
            // 
            this.chkShadow.AutoSize = true;
            this.chkShadow.ForeColor = System.Drawing.Color.White;
            this.chkShadow.Location = new System.Drawing.Point(12, 104);
            this.chkShadow.Name = "chkShadow";
            this.chkShadow.Size = new System.Drawing.Size(196, 21);
            this.chkShadow.TabIndex = 3;
            this.chkShadow.Text = "Удаление Shadow Copies";
            this.chkShadow.UseVisualStyleBackColor = true;
            // 
            // chkRecycle
            // 
            this.chkRecycle.AutoSize = true;
            this.chkRecycle.ForeColor = System.Drawing.Color.White;
            this.chkRecycle.Location = new System.Drawing.Point(12, 131);
            this.chkRecycle.Name = "chkRecycle";
            this.chkRecycle.Size = new System.Drawing.Size(138, 21);
            this.chkRecycle.TabIndex = 4;
            this.chkRecycle.Text = "Очистка корзины";
            this.chkRecycle.UseVisualStyleBackColor = true;
            // 
            // chkCipher
            // 
            this.chkCipher.AutoSize = true;
            this.chkCipher.ForeColor = System.Drawing.Color.White;
            this.chkCipher.Location = new System.Drawing.Point(12, 158);
            this.chkCipher.Name = "chkCipher";
            this.chkCipher.Size = new System.Drawing.Size(101, 21);
            this.chkCipher.TabIndex = 5;
            this.chkCipher.Text = "Cipher wipe";
            this.chkCipher.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.DimGray;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(12, 195);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(200, 30);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "Запустить";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // MainForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(30,30,30);
            this.ClientSize = new System.Drawing.Size(424, 241);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.chkCipher);
            this.Controls.Add(this.chkRecycle);
            this.Controls.Add(this.chkShadow);
            this.Controls.Add(this.chkClearLogs);
            this.Controls.Add(this.chkCleanTemp);
            this.Controls.Add(this.cbLang);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VANTA Zero Clear";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
