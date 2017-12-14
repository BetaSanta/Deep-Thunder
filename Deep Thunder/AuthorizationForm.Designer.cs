namespace Deep_Thunder
{
    partial class AuthorizationForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthorizationForm));
            this.label1 = new System.Windows.Forms.Label();
            this.labelTopTxt1 = new MetroFramework.Controls.MetroLabel();
            this.labelTopTxt2 = new MetroFramework.Controls.MetroLabel();
            this.panelLogin = new System.Windows.Forms.Panel();
            this.btnNext = new System.Windows.Forms.Button();
            this.tbLogin = new MetroFramework.Controls.MetroTextBox();
            this.labelBottonTxt = new MetroFramework.Controls.MetroLabel();
            this.panelPass = new System.Windows.Forms.Panel();
            this.btnEnterAccount = new System.Windows.Forms.Button();
            this.tbPass = new MetroFramework.Controls.MetroTextBox();
            this.btnCreateAccount = new System.Windows.Forms.Label();
            this.cbRememberMe = new MetroFramework.Controls.MetroCheckBox();
            this.btnBack = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelLogin.SuspendLayout();
            this.panelPass.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(19, 60);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(380, 1);
            this.label1.TabIndex = 0;
            // 
            // labelTopTxt1
            // 
            this.labelTopTxt1.Enabled = false;
            this.labelTopTxt1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.labelTopTxt1.Location = new System.Drawing.Point(22, 92);
            this.labelTopTxt1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTopTxt1.Name = "labelTopTxt1";
            this.labelTopTxt1.Size = new System.Drawing.Size(377, 24);
            this.labelTopTxt1.TabIndex = 1;
            this.labelTopTxt1.Text = "Один аккаунт. Весь мир Deep Thunder!";
            this.labelTopTxt1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelTopTxt2
            // 
            this.labelTopTxt2.Enabled = false;
            this.labelTopTxt2.Location = new System.Drawing.Point(22, 129);
            this.labelTopTxt2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTopTxt2.Name = "labelTopTxt2";
            this.labelTopTxt2.Size = new System.Drawing.Size(377, 19);
            this.labelTopTxt2.TabIndex = 2;
            this.labelTopTxt2.Text = "Войдите, используя аккаунт Deep Thunder";
            this.labelTopTxt2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelLogin
            // 
            this.panelLogin.Controls.Add(this.btnNext);
            this.panelLogin.Controls.Add(this.tbLogin);
            this.panelLogin.Location = new System.Drawing.Point(86, 301);
            this.panelLogin.Margin = new System.Windows.Forms.Padding(2);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new System.Drawing.Size(248, 83);
            this.panelLogin.TabIndex = 4;
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.Location = new System.Drawing.Point(5, 45);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(239, 35);
            this.btnNext.TabIndex = 11;
            this.btnNext.Text = "Далее";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // tbLogin
            // 
            // 
            // 
            // 
            this.tbLogin.CustomButton.Image = null;
            this.tbLogin.CustomButton.Location = new System.Drawing.Point(207, 1);
            this.tbLogin.CustomButton.Name = "";
            this.tbLogin.CustomButton.Size = new System.Drawing.Size(31, 31);
            this.tbLogin.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbLogin.CustomButton.TabIndex = 1;
            this.tbLogin.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbLogin.CustomButton.UseSelectable = true;
            this.tbLogin.CustomButton.Visible = false;
            this.tbLogin.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Deep_Thunder.Properties.Settings.Default, "FirstName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbLogin.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbLogin.Lines = new string[0];
            this.tbLogin.Location = new System.Drawing.Point(5, 6);
            this.tbLogin.MaxLength = 32767;
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.PasswordChar = '\0';
            this.tbLogin.PromptText = "Введите логин";
            this.tbLogin.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbLogin.SelectedText = "";
            this.tbLogin.SelectionLength = 0;
            this.tbLogin.SelectionStart = 0;
            this.tbLogin.ShortcutsEnabled = true;
            this.tbLogin.ShowClearButton = true;
            this.tbLogin.Size = new System.Drawing.Size(239, 33);
            this.tbLogin.TabIndex = 0;
            this.tbLogin.Text = global::Deep_Thunder.Properties.Settings.Default.FirstName;
            this.tbLogin.UseSelectable = true;
            this.tbLogin.WaterMark = "Введите логин";
            this.tbLogin.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbLogin.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            // 
            // labelBottonTxt
            // 
            this.labelBottonTxt.Enabled = false;
            this.labelBottonTxt.FontSize = MetroFramework.MetroLabelSize.Small;
            this.labelBottonTxt.Location = new System.Drawing.Point(22, 547);
            this.labelBottonTxt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelBottonTxt.Name = "labelBottonTxt";
            this.labelBottonTxt.Size = new System.Drawing.Size(377, 17);
            this.labelBottonTxt.TabIndex = 6;
            this.labelBottonTxt.Text = "© 2017 Deep Thunder. Все права защищены.";
            this.labelBottonTxt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelPass
            // 
            this.panelPass.Controls.Add(this.btnEnterAccount);
            this.panelPass.Controls.Add(this.tbPass);
            this.panelPass.Location = new System.Drawing.Point(446, 301);
            this.panelPass.Margin = new System.Windows.Forms.Padding(2);
            this.panelPass.Name = "panelPass";
            this.panelPass.Size = new System.Drawing.Size(248, 83);
            this.panelPass.TabIndex = 4;
            // 
            // btnEnterAccount
            // 
            this.btnEnterAccount.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnEnterAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnterAccount.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnEnterAccount.ForeColor = System.Drawing.Color.White;
            this.btnEnterAccount.Location = new System.Drawing.Point(5, 45);
            this.btnEnterAccount.Name = "btnEnterAccount";
            this.btnEnterAccount.Size = new System.Drawing.Size(239, 35);
            this.btnEnterAccount.TabIndex = 11;
            this.btnEnterAccount.Text = "Войти";
            this.btnEnterAccount.UseVisualStyleBackColor = false;
            this.btnEnterAccount.Click += new System.EventHandler(this.btnEnterAccount_Click);
            // 
            // tbPass
            // 
            // 
            // 
            // 
            this.tbPass.CustomButton.Image = null;
            this.tbPass.CustomButton.Location = new System.Drawing.Point(207, 1);
            this.tbPass.CustomButton.Name = "";
            this.tbPass.CustomButton.Size = new System.Drawing.Size(31, 31);
            this.tbPass.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbPass.CustomButton.TabIndex = 1;
            this.tbPass.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbPass.CustomButton.UseSelectable = true;
            this.tbPass.CustomButton.Visible = false;
            this.tbPass.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Deep_Thunder.Properties.Settings.Default, "FirstPassword", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbPass.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.tbPass.Lines = new string[0];
            this.tbPass.Location = new System.Drawing.Point(5, 6);
            this.tbPass.MaxLength = 32767;
            this.tbPass.Name = "tbPass";
            this.tbPass.PasswordChar = '●';
            this.tbPass.PromptText = "Пароль";
            this.tbPass.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbPass.SelectedText = "";
            this.tbPass.SelectionLength = 0;
            this.tbPass.SelectionStart = 0;
            this.tbPass.ShortcutsEnabled = true;
            this.tbPass.ShowClearButton = true;
            this.tbPass.Size = new System.Drawing.Size(239, 33);
            this.tbPass.TabIndex = 0;
            this.tbPass.Text = global::Deep_Thunder.Properties.Settings.Default.FirstPassword;
            this.tbPass.UseSelectable = true;
            this.tbPass.UseSystemPasswordChar = true;
            this.tbPass.WaterMark = "Пароль";
            this.tbPass.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbPass.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            // 
            // btnCreateAccount
            // 
            this.btnCreateAccount.AutoSize = true;
            this.btnCreateAccount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateAccount.Font = new System.Drawing.Font("Segoe UI Light", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCreateAccount.Location = new System.Drawing.Point(153, 499);
            this.btnCreateAccount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.btnCreateAccount.Name = "btnCreateAccount";
            this.btnCreateAccount.Size = new System.Drawing.Size(114, 20);
            this.btnCreateAccount.TabIndex = 5;
            this.btnCreateAccount.Text = "Создать аккаунт";
            this.btnCreateAccount.Click += new System.EventHandler(this.btnCreateAccount_Click);
            this.btnCreateAccount.MouseEnter += new System.EventHandler(this.btnCreateAccount_MouseEnter);
            this.btnCreateAccount.MouseLeave += new System.EventHandler(this.btnCreateAccount_MouseLeave);
            // 
            // cbRememberMe
            // 
            this.cbRememberMe.AutoSize = true;
            this.cbRememberMe.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbRememberMe.Checked = global::Deep_Thunder.Properties.Settings.Default.RememberMe;
            this.cbRememberMe.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Deep_Thunder.Properties.Settings.Default, "RememberMe", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbRememberMe.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.cbRememberMe.Location = new System.Drawing.Point(145, 397);
            this.cbRememberMe.Margin = new System.Windows.Forms.Padding(2);
            this.cbRememberMe.Name = "cbRememberMe";
            this.cbRememberMe.Size = new System.Drawing.Size(131, 19);
            this.cbRememberMe.TabIndex = 10;
            this.cbRememberMe.Text = "Запомнить меня";
            this.cbRememberMe.UseSelectable = true;
            this.cbRememberMe.Visible = false;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Transparent;
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.Image = global::Deep_Thunder.Properties.Resources.Arrow_Back2;
            this.btnBack.Location = new System.Drawing.Point(91, 178);
            this.btnBack.Margin = new System.Windows.Forms.Padding(2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(20, 20);
            this.btnBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnBack.TabIndex = 7;
            this.btnBack.TabStop = false;
            this.btnBack.Visible = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Enabled = false;
            this.pictureBox1.Image = global::Deep_Thunder.Properties.Resources.imageAccount;
            this.pictureBox1.Location = new System.Drawing.Point(163, 178);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(90, 90);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // AuthorizationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 569);
            this.Controls.Add(this.cbRememberMe);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.labelBottonTxt);
            this.Controls.Add(this.btnCreateAccount);
            this.Controls.Add(this.panelPass);
            this.Controls.Add(this.panelLogin);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelTopTxt2);
            this.Controls.Add(this.labelTopTxt1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "AuthorizationForm";
            this.Opacity = 0.98D;
            this.Padding = new System.Windows.Forms.Padding(15, 60, 15, 16);
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Deep Thunder";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AuthorizationForm_FormClosing);
            this.Load += new System.EventHandler(this.AuthorizationForm_Load);
            this.panelLogin.ResumeLayout(false);
            this.panelPass.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroLabel labelTopTxt1;
        private MetroFramework.Controls.MetroLabel labelTopTxt2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panelLogin;
        private MetroFramework.Controls.MetroTextBox tbLogin;
        private MetroFramework.Controls.MetroLabel labelBottonTxt;
        private System.Windows.Forms.Panel panelPass;
        private MetroFramework.Controls.MetroTextBox tbPass;
        private MetroFramework.Controls.MetroCheckBox cbRememberMe;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnEnterAccount;
        private System.Windows.Forms.Label btnCreateAccount;
        private System.Windows.Forms.PictureBox btnBack;
    }
}

