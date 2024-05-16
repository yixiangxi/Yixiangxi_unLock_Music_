namespace UnLockMusic
{
    partial class Form5
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.songnameTextBox1 = new ReaLTaiizor.Controls.CrownTextBox();
            this.formTheme1 = new ReaLTaiizor.Forms.FormTheme();
            this.headerLabel1 = new ReaLTaiizor.Controls.HeaderLabel();
            this.headerLabel2 = new ReaLTaiizor.Controls.HeaderLabel();
            this.singerTextBox2 = new ReaLTaiizor.Controls.CrownTextBox();
            this.headerLabel3 = new ReaLTaiizor.Controls.HeaderLabel();
            this.classesTextBox3 = new ReaLTaiizor.Controls.CrownTextBox();
            this.crownButton1 = new ReaLTaiizor.Controls.CrownButton();
            this.headerLabel4 = new ReaLTaiizor.Controls.HeaderLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.aloneTextBox1 = new ReaLTaiizor.Controls.AloneTextBox();
            this.confirm = new ReaLTaiizor.Controls.Button();
            this.cancel = new ReaLTaiizor.Controls.Button();
            this.formTheme1.SuspendLayout();
            this.SuspendLayout();
            // 
            // songnameTextBox1
            // 
            this.songnameTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.songnameTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.songnameTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.songnameTextBox1.Location = new System.Drawing.Point(265, 56);
            this.songnameTextBox1.Name = "songnameTextBox1";
            this.songnameTextBox1.Size = new System.Drawing.Size(122, 25);
            this.songnameTextBox1.TabIndex = 2;
            // 
            // formTheme1
            // 
            this.formTheme1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.formTheme1.Controls.Add(this.cancel);
            this.formTheme1.Controls.Add(this.confirm);
            this.formTheme1.Controls.Add(this.aloneTextBox1);
            this.formTheme1.Controls.Add(this.headerLabel4);
            this.formTheme1.Controls.Add(this.crownButton1);
            this.formTheme1.Controls.Add(this.classesTextBox3);
            this.formTheme1.Controls.Add(this.headerLabel3);
            this.formTheme1.Controls.Add(this.singerTextBox2);
            this.formTheme1.Controls.Add(this.headerLabel2);
            this.formTheme1.Controls.Add(this.headerLabel1);
            this.formTheme1.Controls.Add(this.songnameTextBox1);
            this.formTheme1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formTheme1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.formTheme1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.formTheme1.Location = new System.Drawing.Point(0, 0);
            this.formTheme1.Name = "formTheme1";
            this.formTheme1.Padding = new System.Windows.Forms.Padding(3, 28, 3, 28);
            this.formTheme1.Sizable = true;
            this.formTheme1.Size = new System.Drawing.Size(509, 358);
            this.formTheme1.SmartBounds = false;
            this.formTheme1.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.formTheme1.TabIndex = 2;
            this.formTheme1.Text = "导入音乐";
            // 
            // headerLabel1
            // 
            this.headerLabel1.AutoSize = true;
            this.headerLabel1.BackColor = System.Drawing.Color.Transparent;
            this.headerLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.headerLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.headerLabel1.Location = new System.Drawing.Point(97, 56);
            this.headerLabel1.Name = "headerLabel1";
            this.headerLabel1.Size = new System.Drawing.Size(150, 24);
            this.headerLabel1.TabIndex = 3;
            this.headerLabel1.Text = "请输入歌曲名称";
            // 
            // headerLabel2
            // 
            this.headerLabel2.AutoSize = true;
            this.headerLabel2.BackColor = System.Drawing.Color.Transparent;
            this.headerLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.headerLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.headerLabel2.Location = new System.Drawing.Point(97, 97);
            this.headerLabel2.Name = "headerLabel2";
            this.headerLabel2.Size = new System.Drawing.Size(150, 24);
            this.headerLabel2.TabIndex = 4;
            this.headerLabel2.Text = "请输入歌手名称";
            // 
            // singerTextBox2
            // 
            this.singerTextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.singerTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.singerTextBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.singerTextBox2.Location = new System.Drawing.Point(265, 96);
            this.singerTextBox2.Name = "singerTextBox2";
            this.singerTextBox2.Size = new System.Drawing.Size(122, 25);
            this.singerTextBox2.TabIndex = 5;
            // 
            // headerLabel3
            // 
            this.headerLabel3.AutoSize = true;
            this.headerLabel3.BackColor = System.Drawing.Color.Transparent;
            this.headerLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.headerLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.headerLabel3.Location = new System.Drawing.Point(97, 138);
            this.headerLabel3.Name = "headerLabel3";
            this.headerLabel3.Size = new System.Drawing.Size(150, 24);
            this.headerLabel3.TabIndex = 6;
            this.headerLabel3.Text = "请输入专辑名称";
            // 
            // classesTextBox3
            // 
            this.classesTextBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.classesTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.classesTextBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.classesTextBox3.Location = new System.Drawing.Point(265, 137);
            this.classesTextBox3.Name = "classesTextBox3";
            this.classesTextBox3.Size = new System.Drawing.Size(122, 25);
            this.classesTextBox3.TabIndex = 7;
            // 
            // crownButton1
            // 
            this.crownButton1.Location = new System.Drawing.Point(265, 179);
            this.crownButton1.Name = "crownButton1";
            this.crownButton1.Padding = new System.Windows.Forms.Padding(5);
            this.crownButton1.Size = new System.Drawing.Size(122, 29);
            this.crownButton1.TabIndex = 8;
            this.crownButton1.Text = "......";
            this.crownButton1.Click += new System.EventHandler(this.button1_Click2);
            // 
            // headerLabel4
            // 
            this.headerLabel4.AutoSize = true;
            this.headerLabel4.BackColor = System.Drawing.Color.Transparent;
            this.headerLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.headerLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.headerLabel4.Location = new System.Drawing.Point(97, 181);
            this.headerLabel4.Name = "headerLabel4";
            this.headerLabel4.Size = new System.Drawing.Size(130, 24);
            this.headerLabel4.TabIndex = 9;
            this.headerLabel4.Text = "点击获取路径";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // aloneTextBox1
            // 
            this.aloneTextBox1.BackColor = System.Drawing.Color.Transparent;
            this.aloneTextBox1.EnabledCalc = true;
            this.aloneTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.aloneTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            this.aloneTextBox1.Location = new System.Drawing.Point(101, 223);
            this.aloneTextBox1.MaxLength = 32767;
            this.aloneTextBox1.MultiLine = false;
            this.aloneTextBox1.Name = "aloneTextBox1";
            this.aloneTextBox1.ReadOnly = false;
            this.aloneTextBox1.Size = new System.Drawing.Size(286, 29);
            this.aloneTextBox1.TabIndex = 10;
            this.aloneTextBox1.Text = "音乐路径";
            this.aloneTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.aloneTextBox1.UseSystemPasswordChar = false;
            // 
            // confirm
            // 
            this.confirm.BackColor = System.Drawing.Color.Transparent;
            this.confirm.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.confirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.confirm.EnteredBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.confirm.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.confirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.confirm.Image = null;
            this.confirm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.confirm.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.confirm.Location = new System.Drawing.Point(101, 272);
            this.confirm.Name = "confirm";
            this.confirm.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.confirm.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.confirm.Size = new System.Drawing.Size(120, 40);
            this.confirm.TabIndex = 11;
            this.confirm.Text = "确定";
            this.confirm.TextAlignment = System.Drawing.StringAlignment.Center;
            this.confirm.Click += new System.EventHandler(this.confirm_Click);
            // 
            // cancel
            // 
            this.cancel.BackColor = System.Drawing.Color.Transparent;
            this.cancel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancel.EnteredBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.cancel.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cancel.Image = null;
            this.cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cancel.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.cancel.Location = new System.Drawing.Point(265, 272);
            this.cancel.Name = "cancel";
            this.cancel.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.cancel.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.cancel.Size = new System.Drawing.Size(120, 40);
            this.cancel.TabIndex = 12;
            this.cancel.Text = "取消";
            this.cancel.TextAlignment = System.Drawing.StringAlignment.Center;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 358);
            this.Controls.Add(this.formTheme1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(126, 50);
            this.Name = "Form5";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "导入音乐";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.formTheme1.ResumeLayout(false);
            this.formTheme1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private ReaLTaiizor.Controls.CrownTextBox songnameTextBox1;
        private ReaLTaiizor.Forms.FormTheme formTheme1;
        private ReaLTaiizor.Controls.HeaderLabel headerLabel3;
        private ReaLTaiizor.Controls.CrownTextBox singerTextBox2;
        private ReaLTaiizor.Controls.HeaderLabel headerLabel2;
        private ReaLTaiizor.Controls.HeaderLabel headerLabel1;
        private ReaLTaiizor.Controls.HeaderLabel headerLabel4;
        private ReaLTaiizor.Controls.CrownButton crownButton1;
        private ReaLTaiizor.Controls.CrownTextBox classesTextBox3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private ReaLTaiizor.Controls.AloneTextBox aloneTextBox1;
        private ReaLTaiizor.Controls.Button confirm;
        private ReaLTaiizor.Controls.Button cancel;
    }
}