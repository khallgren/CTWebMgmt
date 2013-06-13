namespace CTWebMgmt.Ind.Messaging
{
    partial class frmImgUL
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnUL = new System.Windows.Forms.Button();
            this.lstFiles = new System.Windows.Forms.ListBox();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.btnSelectDir = new System.Windows.Forms.Button();
            this.btnRemoveFile = new System.Windows.Forms.Button();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.cboProgram = new System.Windows.Forms.ComboBox();
            this.cboHousing = new System.Windows.Forms.ComboBox();
            this.cboWeek = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(499, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(65, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnUL
            // 
            this.btnUL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUL.Location = new System.Drawing.Point(489, 350);
            this.btnUL.Name = "btnUL";
            this.btnUL.Size = new System.Drawing.Size(75, 23);
            this.btnUL.TabIndex = 1;
            this.btnUL.Text = "&Upload";
            this.btnUL.UseVisualStyleBackColor = true;
            this.btnUL.Click += new System.EventHandler(this.btnUL_Click);
            // 
            // lstFiles
            // 
            this.lstFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstFiles.FormattingEnabled = true;
            this.lstFiles.Location = new System.Drawing.Point(12, 41);
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Size = new System.Drawing.Size(552, 173);
            this.lstFiles.TabIndex = 2;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectFile.Location = new System.Drawing.Point(12, 350);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFile.TabIndex = 3;
            this.btnSelectFile.Text = "Select File";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // btnSelectDir
            // 
            this.btnSelectDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectDir.Location = new System.Drawing.Point(93, 350);
            this.btnSelectDir.Name = "btnSelectDir";
            this.btnSelectDir.Size = new System.Drawing.Size(112, 23);
            this.btnSelectDir.TabIndex = 4;
            this.btnSelectDir.Text = "Select Directory";
            this.btnSelectDir.UseVisualStyleBackColor = true;
            this.btnSelectDir.Click += new System.EventHandler(this.btnSelectDir_Click);
            // 
            // btnRemoveFile
            // 
            this.btnRemoveFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveFile.Location = new System.Drawing.Point(211, 350);
            this.btnRemoveFile.Name = "btnRemoveFile";
            this.btnRemoveFile.Size = new System.Drawing.Size(82, 23);
            this.btnRemoveFile.TabIndex = 5;
            this.btnRemoveFile.Text = "Remove File";
            this.btnRemoveFile.UseVisualStyleBackColor = true;
            this.btnRemoveFile.Click += new System.EventHandler(this.btnRemoveFile_Click);
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveAll.Location = new System.Drawing.Point(299, 350);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(81, 23);
            this.btnRemoveAll.TabIndex = 6;
            this.btnRemoveAll.Text = "Remove All";
            this.btnRemoveAll.UseVisualStyleBackColor = true;
            this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
            // 
            // cboProgram
            // 
            this.cboProgram.FormattingEnabled = true;
            this.cboProgram.Location = new System.Drawing.Point(72, 287);
            this.cboProgram.Name = "cboProgram";
            this.cboProgram.Size = new System.Drawing.Size(221, 21);
            this.cboProgram.TabIndex = 7;
            // 
            // cboHousing
            // 
            this.cboHousing.FormattingEnabled = true;
            this.cboHousing.Location = new System.Drawing.Point(72, 260);
            this.cboHousing.Name = "cboHousing";
            this.cboHousing.Size = new System.Drawing.Size(221, 21);
            this.cboHousing.TabIndex = 8;
            // 
            // cboWeek
            // 
            this.cboWeek.FormattingEnabled = true;
            this.cboWeek.Location = new System.Drawing.Point(72, 233);
            this.cboWeek.Name = "cboWeek";
            this.cboWeek.Size = new System.Drawing.Size(221, 21);
            this.cboWeek.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 290);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Program:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 263);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Housing:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 236);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Week:";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(13, 334);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 13;
            // 
            // frmImgUL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 385);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboWeek);
            this.Controls.Add(this.cboHousing);
            this.Controls.Add(this.cboProgram);
            this.Controls.Add(this.btnRemoveAll);
            this.Controls.Add(this.btnRemoveFile);
            this.Controls.Add(this.btnSelectDir);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.lstFiles);
            this.Controls.Add(this.btnUL);
            this.Controls.Add(this.btnClose);
            this.Name = "frmImgUL";
            this.Text = "Camper Messaging Image Upload";
            this.Load += new System.EventHandler(this.frmImgUL_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnUL;
        private System.Windows.Forms.ListBox lstFiles;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Button btnSelectDir;
        private System.Windows.Forms.Button btnRemoveFile;
        private System.Windows.Forms.Button btnRemoveAll;
        private System.Windows.Forms.ComboBox cboProgram;
        private System.Windows.Forms.ComboBox cboHousing;
        private System.Windows.Forms.ComboBox cboWeek;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblStatus;
    }
}