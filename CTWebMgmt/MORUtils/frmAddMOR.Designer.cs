namespace CTWebMgmt.MORUtils
{
    partial class frmAddMOR
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
            this.btnContinue = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddMember = new System.Windows.Forms.Button();
            this.btnAddFather = new System.Windows.Forms.Button();
            this.btnAddMother = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtMORCity = new System.Windows.Forms.TextBox();
            this.txtMORPhone = new System.Windows.Forms.TextBox();
            this.txtMotherFName = new System.Windows.Forms.TextBox();
            this.txtMOREmail = new System.Windows.Forms.TextBox();
            this.txtFatherFName = new System.Windows.Forms.TextBox();
            this.txtMotherEmail = new System.Windows.Forms.TextBox();
            this.txtMotherCellPhone = new System.Windows.Forms.TextBox();
            this.txtMotherLName = new System.Windows.Forms.TextBox();
            this.txtMORZip = new System.Windows.Forms.TextBox();
            this.txtMORAddress = new System.Windows.Forms.TextBox();
            this.txtMORName = new System.Windows.Forms.TextBox();
            this.cboMORState = new System.Windows.Forms.ComboBox();
            this.cboMORCountry = new System.Windows.Forms.ComboBox();
            this.cboPrimaryContact = new System.Windows.Forms.ComboBox();
            this.cboMORType = new System.Windows.Forms.ComboBox();
            this.txtFatherEmail = new System.Windows.Forms.TextBox();
            this.txtFatherCellPhone = new System.Windows.Forms.TextBox();
            this.txtFatherLName = new System.Windows.Forms.TextBox();
            this.lstMembers = new System.Windows.Forms.ListBox();
            this.fraMotherType = new System.Windows.Forms.GroupBox();
            this.radMotherSpouse = new System.Windows.Forms.RadioButton();
            this.radMotherHead = new System.Windows.Forms.RadioButton();
            this.fraFatherType = new System.Windows.Forms.GroupBox();
            this.radFatherSpouse = new System.Windows.Forms.RadioButton();
            this.radFatherHead = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fraMotherType.SuspendLayout();
            this.fraFatherType.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnContinue
            // 
            this.btnContinue.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnContinue.Location = new System.Drawing.Point(626, 12);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(75, 23);
            this.btnContinue.TabIndex = 0;
            this.btnContinue.Text = "&Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(707, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "C&ancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAddMember
            // 
            this.btnAddMember.Location = new System.Drawing.Point(482, 33);
            this.btnAddMember.Name = "btnAddMember";
            this.btnAddMember.Size = new System.Drawing.Size(88, 23);
            this.btnAddMember.TabIndex = 2;
            this.btnAddMember.Text = "Add &Member";
            this.btnAddMember.UseVisualStyleBackColor = true;
            this.btnAddMember.Click += new System.EventHandler(this.btnAddMember_Click);
            // 
            // btnAddFather
            // 
            this.btnAddFather.Location = new System.Drawing.Point(707, 341);
            this.btnAddFather.Name = "btnAddFather";
            this.btnAddFather.Size = new System.Drawing.Size(75, 23);
            this.btnAddFather.TabIndex = 4;
            this.btnAddFather.Text = "Add Father";
            this.btnAddFather.UseVisualStyleBackColor = true;
            this.btnAddFather.Click += new System.EventHandler(this.btnAddFather_Click);
            // 
            // btnAddMother
            // 
            this.btnAddMother.Location = new System.Drawing.Point(707, 293);
            this.btnAddMother.Name = "btnAddMother";
            this.btnAddMother.Size = new System.Drawing.Size(75, 23);
            this.btnAddMother.TabIndex = 6;
            this.btnAddMother.Text = "Add Mother";
            this.btnAddMother.UseVisualStyleBackColor = true;
            this.btnAddMother.Click += new System.EventHandler(this.btnAddMother_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "City, State, Zip:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Country:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Address:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Email:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 202);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Primary Contact:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 175);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "MOR Type:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(495, 278);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Email";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(371, 278);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "Cell Phone";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(42, 346);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "Father:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(42, 298);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 13);
            this.label12.TabIndex = 15;
            this.label12.Text = "Mother:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(17, 123);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 13);
            this.label13.TabIndex = 19;
            this.label13.Text = "Phone:";
            // 
            // txtMORCity
            // 
            this.txtMORCity.Location = new System.Drawing.Point(116, 66);
            this.txtMORCity.Name = "txtMORCity";
            this.txtMORCity.Size = new System.Drawing.Size(150, 20);
            this.txtMORCity.TabIndex = 20;
            // 
            // txtMORPhone
            // 
            this.txtMORPhone.Location = new System.Drawing.Point(116, 120);
            this.txtMORPhone.Name = "txtMORPhone";
            this.txtMORPhone.Size = new System.Drawing.Size(150, 20);
            this.txtMORPhone.TabIndex = 21;
            // 
            // txtMotherFName
            // 
            this.txtMotherFName.Location = new System.Drawing.Point(141, 295);
            this.txtMotherFName.Name = "txtMotherFName";
            this.txtMotherFName.Size = new System.Drawing.Size(100, 20);
            this.txtMotherFName.TabIndex = 23;
            // 
            // txtMOREmail
            // 
            this.txtMOREmail.Location = new System.Drawing.Point(116, 146);
            this.txtMOREmail.Name = "txtMOREmail";
            this.txtMOREmail.Size = new System.Drawing.Size(150, 20);
            this.txtMOREmail.TabIndex = 22;
            // 
            // txtFatherFName
            // 
            this.txtFatherFName.Location = new System.Drawing.Point(141, 343);
            this.txtFatherFName.Name = "txtFatherFName";
            this.txtFatherFName.Size = new System.Drawing.Size(100, 20);
            this.txtFatherFName.TabIndex = 27;
            // 
            // txtMotherEmail
            // 
            this.txtMotherEmail.Location = new System.Drawing.Point(459, 295);
            this.txtMotherEmail.Name = "txtMotherEmail";
            this.txtMotherEmail.Size = new System.Drawing.Size(100, 20);
            this.txtMotherEmail.TabIndex = 26;
            // 
            // txtMotherCellPhone
            // 
            this.txtMotherCellPhone.Location = new System.Drawing.Point(353, 295);
            this.txtMotherCellPhone.Name = "txtMotherCellPhone";
            this.txtMotherCellPhone.Size = new System.Drawing.Size(100, 20);
            this.txtMotherCellPhone.TabIndex = 25;
            // 
            // txtMotherLName
            // 
            this.txtMotherLName.Location = new System.Drawing.Point(247, 295);
            this.txtMotherLName.Name = "txtMotherLName";
            this.txtMotherLName.Size = new System.Drawing.Size(100, 20);
            this.txtMotherLName.TabIndex = 24;
            // 
            // txtMORZip
            // 
            this.txtMORZip.Location = new System.Drawing.Point(349, 66);
            this.txtMORZip.Name = "txtMORZip";
            this.txtMORZip.Size = new System.Drawing.Size(84, 20);
            this.txtMORZip.TabIndex = 30;
            // 
            // txtMORAddress
            // 
            this.txtMORAddress.Location = new System.Drawing.Point(116, 40);
            this.txtMORAddress.Name = "txtMORAddress";
            this.txtMORAddress.Size = new System.Drawing.Size(317, 20);
            this.txtMORAddress.TabIndex = 29;
            // 
            // txtMORName
            // 
            this.txtMORName.Location = new System.Drawing.Point(116, 14);
            this.txtMORName.Name = "txtMORName";
            this.txtMORName.Size = new System.Drawing.Size(317, 20);
            this.txtMORName.TabIndex = 28;
            // 
            // cboMORState
            // 
            this.cboMORState.FormattingEnabled = true;
            this.cboMORState.Location = new System.Drawing.Point(272, 66);
            this.cboMORState.Name = "cboMORState";
            this.cboMORState.Size = new System.Drawing.Size(71, 21);
            this.cboMORState.TabIndex = 31;
            // 
            // cboMORCountry
            // 
            this.cboMORCountry.FormattingEnabled = true;
            this.cboMORCountry.Location = new System.Drawing.Point(116, 93);
            this.cboMORCountry.Name = "cboMORCountry";
            this.cboMORCountry.Size = new System.Drawing.Size(150, 21);
            this.cboMORCountry.TabIndex = 32;
            // 
            // cboPrimaryContact
            // 
            this.cboPrimaryContact.FormattingEnabled = true;
            this.cboPrimaryContact.Location = new System.Drawing.Point(116, 199);
            this.cboPrimaryContact.Name = "cboPrimaryContact";
            this.cboPrimaryContact.Size = new System.Drawing.Size(227, 21);
            this.cboPrimaryContact.TabIndex = 34;
            // 
            // cboMORType
            // 
            this.cboMORType.FormattingEnabled = true;
            this.cboMORType.Location = new System.Drawing.Point(116, 172);
            this.cboMORType.Name = "cboMORType";
            this.cboMORType.Size = new System.Drawing.Size(150, 21);
            this.cboMORType.TabIndex = 33;
            // 
            // txtFatherEmail
            // 
            this.txtFatherEmail.Location = new System.Drawing.Point(459, 343);
            this.txtFatherEmail.Name = "txtFatherEmail";
            this.txtFatherEmail.Size = new System.Drawing.Size(100, 20);
            this.txtFatherEmail.TabIndex = 37;
            // 
            // txtFatherCellPhone
            // 
            this.txtFatherCellPhone.Location = new System.Drawing.Point(353, 343);
            this.txtFatherCellPhone.Name = "txtFatherCellPhone";
            this.txtFatherCellPhone.Size = new System.Drawing.Size(100, 20);
            this.txtFatherCellPhone.TabIndex = 36;
            // 
            // txtFatherLName
            // 
            this.txtFatherLName.Location = new System.Drawing.Point(247, 343);
            this.txtFatherLName.Name = "txtFatherLName";
            this.txtFatherLName.Size = new System.Drawing.Size(100, 20);
            this.txtFatherLName.TabIndex = 35;
            // 
            // lstMembers
            // 
            this.lstMembers.FormattingEnabled = true;
            this.lstMembers.Location = new System.Drawing.Point(482, 62);
            this.lstMembers.Name = "lstMembers";
            this.lstMembers.Size = new System.Drawing.Size(213, 199);
            this.lstMembers.TabIndex = 38;
            // 
            // fraMotherType
            // 
            this.fraMotherType.Controls.Add(this.radMotherSpouse);
            this.fraMotherType.Controls.Add(this.radMotherHead);
            this.fraMotherType.Location = new System.Drawing.Point(565, 278);
            this.fraMotherType.Name = "fraMotherType";
            this.fraMotherType.Size = new System.Drawing.Size(136, 42);
            this.fraMotherType.TabIndex = 39;
            this.fraMotherType.TabStop = false;
            this.fraMotherType.Text = "Add As";
            // 
            // radMotherSpouse
            // 
            this.radMotherSpouse.AutoSize = true;
            this.radMotherSpouse.Location = new System.Drawing.Point(67, 18);
            this.radMotherSpouse.Name = "radMotherSpouse";
            this.radMotherSpouse.Size = new System.Drawing.Size(61, 17);
            this.radMotherSpouse.TabIndex = 1;
            this.radMotherSpouse.TabStop = true;
            this.radMotherSpouse.Text = "Spouse";
            this.radMotherSpouse.UseVisualStyleBackColor = true;
            // 
            // radMotherHead
            // 
            this.radMotherHead.AutoSize = true;
            this.radMotherHead.Location = new System.Drawing.Point(10, 18);
            this.radMotherHead.Name = "radMotherHead";
            this.radMotherHead.Size = new System.Drawing.Size(51, 17);
            this.radMotherHead.TabIndex = 0;
            this.radMotherHead.TabStop = true;
            this.radMotherHead.Text = "Head";
            this.radMotherHead.UseVisualStyleBackColor = true;
            // 
            // fraFatherType
            // 
            this.fraFatherType.Controls.Add(this.radFatherSpouse);
            this.fraFatherType.Controls.Add(this.radFatherHead);
            this.fraFatherType.Location = new System.Drawing.Point(565, 326);
            this.fraFatherType.Name = "fraFatherType";
            this.fraFatherType.Size = new System.Drawing.Size(136, 42);
            this.fraFatherType.TabIndex = 40;
            this.fraFatherType.TabStop = false;
            this.fraFatherType.Text = "Add As";
            // 
            // radFatherSpouse
            // 
            this.radFatherSpouse.AutoSize = true;
            this.radFatherSpouse.Location = new System.Drawing.Point(67, 18);
            this.radFatherSpouse.Name = "radFatherSpouse";
            this.radFatherSpouse.Size = new System.Drawing.Size(61, 17);
            this.radFatherSpouse.TabIndex = 1;
            this.radFatherSpouse.TabStop = true;
            this.radFatherSpouse.Text = "Spouse";
            this.radFatherSpouse.UseVisualStyleBackColor = true;
            // 
            // radFatherHead
            // 
            this.radFatherHead.AutoSize = true;
            this.radFatherHead.Location = new System.Drawing.Point(10, 18);
            this.radFatherHead.Name = "radFatherHead";
            this.radFatherHead.Size = new System.Drawing.Size(51, 17);
            this.radFatherHead.TabIndex = 0;
            this.radFatherHead.TabStop = true;
            this.radFatherHead.Text = "Head";
            this.radFatherHead.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.cboPrimaryContact);
            this.panel1.Controls.Add(this.cboMORType);
            this.panel1.Controls.Add(this.cboMORCountry);
            this.panel1.Controls.Add(this.cboMORState);
            this.panel1.Controls.Add(this.txtMORZip);
            this.panel1.Controls.Add(this.txtMORAddress);
            this.panel1.Controls.Add(this.txtMORName);
            this.panel1.Controls.Add(this.txtMOREmail);
            this.panel1.Controls.Add(this.txtMORPhone);
            this.panel1.Controls.Add(this.txtMORCity);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(23, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(445, 238);
            this.panel1.TabIndex = 41;
            // 
            // frmAddMOR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(806, 415);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.fraFatherType);
            this.Controls.Add(this.fraMotherType);
            this.Controls.Add(this.lstMembers);
            this.Controls.Add(this.txtFatherEmail);
            this.Controls.Add(this.txtFatherCellPhone);
            this.Controls.Add(this.txtFatherLName);
            this.Controls.Add(this.txtFatherFName);
            this.Controls.Add(this.txtMotherEmail);
            this.Controls.Add(this.txtMotherCellPhone);
            this.Controls.Add(this.txtMotherLName);
            this.Controls.Add(this.txtMotherFName);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnAddMother);
            this.Controls.Add(this.btnAddFather);
            this.Controls.Add(this.btnAddMember);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnContinue);
            this.Name = "frmAddMOR";
            // 
            // 
            // 
            
            this.Text = "Add MOR";
            this.Load += new System.EventHandler(this.frmAddMOR_Load);
            this.fraMotherType.ResumeLayout(false);
            this.fraMotherType.PerformLayout();
            this.fraFatherType.ResumeLayout(false);
            this.fraFatherType.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAddMember;
        private System.Windows.Forms.Button btnAddFather;
        private System.Windows.Forms.Button btnAddMother;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtMORCity;
        private System.Windows.Forms.TextBox txtMORPhone;
        private System.Windows.Forms.TextBox txtMotherFName;
        private System.Windows.Forms.TextBox txtMOREmail;
        private System.Windows.Forms.TextBox txtFatherFName;
        private System.Windows.Forms.TextBox txtMotherEmail;
        private System.Windows.Forms.TextBox txtMotherCellPhone;
        private System.Windows.Forms.TextBox txtMotherLName;
        private System.Windows.Forms.TextBox txtMORZip;
        private System.Windows.Forms.TextBox txtMORAddress;
        private System.Windows.Forms.TextBox txtMORName;
        private System.Windows.Forms.ComboBox cboMORState;
        private System.Windows.Forms.ComboBox cboMORCountry;
        private System.Windows.Forms.ComboBox cboPrimaryContact;
        private System.Windows.Forms.ComboBox cboMORType;
        private System.Windows.Forms.TextBox txtFatherEmail;
        private System.Windows.Forms.TextBox txtFatherCellPhone;
        private System.Windows.Forms.TextBox txtFatherLName;
        private System.Windows.Forms.ListBox lstMembers;
        private System.Windows.Forms.GroupBox fraMotherType;
        private System.Windows.Forms.RadioButton radMotherSpouse;
        private System.Windows.Forms.RadioButton radMotherHead;
        private System.Windows.Forms.GroupBox fraFatherType;
        private System.Windows.Forms.RadioButton radFatherSpouse;
        private System.Windows.Forms.RadioButton radFatherHead;
        private System.Windows.Forms.Panel panel1;
    }
}