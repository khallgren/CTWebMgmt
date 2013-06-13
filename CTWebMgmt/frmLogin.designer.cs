namespace CTWebMgmt
{
    partial class frmLogin
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
            this.Label4 = new System.Windows.Forms.Label();
            this.lblUpdateVersion = new System.Windows.Forms.Label();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.chkAdmin = new System.Windows.Forms.CheckBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.btnContinue = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.cboUsers = new System.Windows.Forms.ComboBox();
            this.btnDev = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(208, 42);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(161, 13);
            this.Label4.TabIndex = 24;
            this.Label4.Text = "Press Enter to validate password";
            // 
            // lblUpdateVersion
            // 
            this.lblUpdateVersion.AutoSize = true;
            this.lblUpdateVersion.Location = new System.Drawing.Point(9, 157);
            this.lblUpdateVersion.Name = "lblUpdateVersion";
            this.lblUpdateVersion.Size = new System.Drawing.Size(80, 13);
            this.lblUpdateVersion.TabIndex = 23;
            this.lblUpdateVersion.Text = "Update Version";
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.Enabled = false;
            this.btnChangePassword.Location = new System.Drawing.Point(92, 131);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(110, 23);
            this.btnChangePassword.TabIndex = 17;
            this.btnChangePassword.Text = "Change Password";
            this.btnChangePassword.UseVisualStyleBackColor = true;
            this.btnChangePassword.Visible = false;
            // 
            // btnLogout
            // 
            this.btnLogout.Enabled = false;
            this.btnLogout.Location = new System.Drawing.Point(208, 131);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 23);
            this.btnLogout.TabIndex = 19;
            this.btnLogout.Text = "&Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(289, 131);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 21;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // chkAdmin
            // 
            this.chkAdmin.AutoSize = true;
            this.chkAdmin.Enabled = false;
            this.chkAdmin.Location = new System.Drawing.Point(81, 65);
            this.chkAdmin.Name = "chkAdmin";
            this.chkAdmin.Size = new System.Drawing.Size(86, 17);
            this.chkAdmin.TabIndex = 22;
            this.chkAdmin.TabStop = false;
            this.chkAdmin.Text = "Administrator";
            this.chkAdmin.UseVisualStyleBackColor = true;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(9, 39);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(56, 13);
            this.Label2.TabIndex = 20;
            this.Label2.Text = "Password:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(9, 12);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(63, 13);
            this.Label1.TabIndex = 18;
            this.Label1.Text = "User Name:";
            // 
            // btnContinue
            // 
            this.btnContinue.Enabled = false;
            this.btnContinue.Location = new System.Drawing.Point(12, 131);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(75, 23);
            this.btnContinue.TabIndex = 16;
            this.btnContinue.Text = "&Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(81, 39);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(121, 20);
            this.txtPassword.TabIndex = 15;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // cboUsers
            // 
            this.cboUsers.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cboUsers.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboUsers.FormattingEnabled = true;
            this.cboUsers.Location = new System.Drawing.Point(81, 12);
            this.cboUsers.Name = "cboUsers";
            this.cboUsers.Size = new System.Drawing.Size(121, 21);
            this.cboUsers.TabIndex = 14;
            this.cboUsers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cboUsers_MouseDown);
            // 
            // btnDev
            // 
            this.btnDev.Location = new System.Drawing.Point(13, 102);
            this.btnDev.Name = "btnDev";
            this.btnDev.Size = new System.Drawing.Size(75, 23);
            this.btnDev.TabIndex = 25;
            this.btnDev.Text = "Dev";
            this.btnDev.UseVisualStyleBackColor = true;
            this.btnDev.Visible = false;
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(386, 191);
            this.Controls.Add(this.btnDev);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.lblUpdateVersion);
            this.Controls.Add(this.btnChangePassword);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.chkAdmin);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.cboUsers);
            this.Name = "frmLogin";
            // 
            // 
            // 
            this.Text = "Login";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label lblUpdateVersion;
        internal System.Windows.Forms.Button btnChangePassword;
        internal System.Windows.Forms.Button btnLogout;
        internal System.Windows.Forms.Button btnExit;
        internal System.Windows.Forms.CheckBox chkAdmin;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button btnContinue;
        internal System.Windows.Forms.TextBox txtPassword;
        internal System.Windows.Forms.ComboBox cboUsers;
        private System.Windows.Forms.Button btnDev;
    }
}

