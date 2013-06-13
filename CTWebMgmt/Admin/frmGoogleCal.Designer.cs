namespace CTWebMgmt
{
    partial class frmGoogleCal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGoogleCal));
            this.btnPost = new System.Windows.Forms.Button();
            this.btnRefreshCals = new System.Windows.Forms.Button();
            this.chkGroupRentals = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkGroupEvents = new System.Windows.Forms.CheckBox();
            this.chkIndEvents = new System.Windows.Forms.CheckBox();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.cboGGCalendar = new System.Windows.Forms.ComboBox();
            this.cboBlockCalendar = new System.Windows.Forms.ComboBox();
            this.cboCCCalendar = new System.Windows.Forms.ComboBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.lblGGStatus = new System.Windows.Forms.Label();
            this.lblCCStatus = new System.Windows.Forms.Label();
            this.lblBlockStatus = new System.Windows.Forms.Label();
            this.btnGGOptions = new System.Windows.Forms.Button();
            this.btnCCOptions = new System.Windows.Forms.Button();
            this.btnBlockOptions = new System.Windows.Forms.Button();
            this.lstOutput = new System.Windows.Forms.ListBox();
            this.btnClearCals = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPost
            // 
            this.btnPost.Location = new System.Drawing.Point(12, 232);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(428, 23);
            this.btnPost.TabIndex = 0;
            this.btnPost.Text = "Post events to selected calendar(s)";
            this.btnPost.UseVisualStyleBackColor = true;
            this.btnPost.Click += new System.EventHandler(this.btnPost_Click);
            // 
            // btnRefreshCals
            // 
            this.btnRefreshCals.Location = new System.Drawing.Point(108, 162);
            this.btnRefreshCals.Name = "btnRefreshCals";
            this.btnRefreshCals.Size = new System.Drawing.Size(173, 23);
            this.btnRefreshCals.TabIndex = 7;
            this.btnRefreshCals.Text = "&Refresh Calendar Choices";
            this.btnRefreshCals.UseVisualStyleBackColor = true;
            this.btnRefreshCals.Click += new System.EventHandler(this.btnRefreshCals_Click);
            // 
            // chkGroupRentals
            // 
            this.chkGroupRentals.AutoSize = true;
            this.chkGroupRentals.Location = new System.Drawing.Point(108, 91);
            this.chkGroupRentals.Name = "chkGroupRentals";
            this.chkGroupRentals.Size = new System.Drawing.Size(94, 17);
            this.chkGroupRentals.TabIndex = 8;
            this.chkGroupRentals.Text = "Group Rentals";
            this.chkGroupRentals.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(364, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Select the event types you wish to upload and the calendar to use for each:";
            // 
            // chkGroupEvents
            // 
            this.chkGroupEvents.AutoSize = true;
            this.chkGroupEvents.Location = new System.Drawing.Point(108, 114);
            this.chkGroupEvents.Name = "chkGroupEvents";
            this.chkGroupEvents.Size = new System.Drawing.Size(91, 17);
            this.chkGroupEvents.TabIndex = 10;
            this.chkGroupEvents.Text = "Group Events";
            this.chkGroupEvents.UseVisualStyleBackColor = true;
            // 
            // chkIndEvents
            // 
            this.chkIndEvents.AutoSize = true;
            this.chkIndEvents.Location = new System.Drawing.Point(108, 137);
            this.chkIndEvents.Name = "chkIndEvents";
            this.chkIndEvents.Size = new System.Drawing.Size(107, 17);
            this.chkIndEvents.TabIndex = 11;
            this.chkIndEvents.Text = "Individual Events";
            this.chkIndEvents.UseVisualStyleBackColor = true;
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveSettings.Location = new System.Drawing.Point(582, 89);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(100, 23);
            this.btnSaveSettings.TabIndex = 12;
            this.btnSaveSettings.Text = "&Save Settings";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // cboGGCalendar
            // 
            this.cboGGCalendar.FormattingEnabled = true;
            this.cboGGCalendar.Location = new System.Drawing.Point(221, 89);
            this.cboGGCalendar.Name = "cboGGCalendar";
            this.cboGGCalendar.Size = new System.Drawing.Size(199, 21);
            this.cboGGCalendar.TabIndex = 13;
            // 
            // cboBlockCalendar
            // 
            this.cboBlockCalendar.FormattingEnabled = true;
            this.cboBlockCalendar.Location = new System.Drawing.Point(221, 135);
            this.cboBlockCalendar.Name = "cboBlockCalendar";
            this.cboBlockCalendar.Size = new System.Drawing.Size(199, 21);
            this.cboBlockCalendar.TabIndex = 14;
            // 
            // cboCCCalendar
            // 
            this.cboCCCalendar.FormattingEnabled = true;
            this.cboCCCalendar.Location = new System.Drawing.Point(221, 112);
            this.cboCCCalendar.Name = "cboCCCalendar";
            this.cboCCCalendar.Size = new System.Drawing.Size(199, 21);
            this.cboCCCalendar.TabIndex = 15;
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(12, 12);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(428, 45);
            this.textBox2.TabIndex = 18;
            this.textBox2.Text = resources.GetString("textBox2.Text");
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Location = new System.Drawing.Point(12, 202);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(428, 24);
            this.textBox3.TabIndex = 19;
            this.textBox3.Text = "Any items on the calendar will be cleared before uploading the new calendar event" +
                "s.";
            // 
            // lblGGStatus
            // 
            this.lblGGStatus.AutoSize = true;
            this.lblGGStatus.Location = new System.Drawing.Point(426, 92);
            this.lblGGStatus.Name = "lblGGStatus";
            this.lblGGStatus.Size = new System.Drawing.Size(0, 13);
            this.lblGGStatus.TabIndex = 20;
            // 
            // lblCCStatus
            // 
            this.lblCCStatus.AutoSize = true;
            this.lblCCStatus.Location = new System.Drawing.Point(426, 115);
            this.lblCCStatus.Name = "lblCCStatus";
            this.lblCCStatus.Size = new System.Drawing.Size(0, 13);
            this.lblCCStatus.TabIndex = 21;
            // 
            // lblBlockStatus
            // 
            this.lblBlockStatus.AutoSize = true;
            this.lblBlockStatus.Location = new System.Drawing.Point(426, 138);
            this.lblBlockStatus.Name = "lblBlockStatus";
            this.lblBlockStatus.Size = new System.Drawing.Size(0, 13);
            this.lblBlockStatus.TabIndex = 22;
            // 
            // btnGGOptions
            // 
            this.btnGGOptions.Location = new System.Drawing.Point(9, 87);
            this.btnGGOptions.Name = "btnGGOptions";
            this.btnGGOptions.Size = new System.Drawing.Size(93, 23);
            this.btnGGOptions.TabIndex = 23;
            this.btnGGOptions.Text = "Upload Options";
            this.btnGGOptions.UseVisualStyleBackColor = true;
            this.btnGGOptions.Click += new System.EventHandler(this.btnGGOptions_Click);
            // 
            // btnCCOptions
            // 
            this.btnCCOptions.Location = new System.Drawing.Point(9, 110);
            this.btnCCOptions.Name = "btnCCOptions";
            this.btnCCOptions.Size = new System.Drawing.Size(93, 23);
            this.btnCCOptions.TabIndex = 24;
            this.btnCCOptions.Text = "Upload Options";
            this.btnCCOptions.UseVisualStyleBackColor = true;
            this.btnCCOptions.Click += new System.EventHandler(this.btnCCOptions_Click);
            // 
            // btnBlockOptions
            // 
            this.btnBlockOptions.Location = new System.Drawing.Point(9, 133);
            this.btnBlockOptions.Name = "btnBlockOptions";
            this.btnBlockOptions.Size = new System.Drawing.Size(93, 23);
            this.btnBlockOptions.TabIndex = 25;
            this.btnBlockOptions.Text = "Upload Options";
            this.btnBlockOptions.UseVisualStyleBackColor = true;
            this.btnBlockOptions.Click += new System.EventHandler(this.btnBlockOptions_Click);
            // 
            // lstOutput
            // 
            this.lstOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstOutput.FormattingEnabled = true;
            this.lstOutput.Location = new System.Drawing.Point(12, 264);
            this.lstOutput.Name = "lstOutput";
            this.lstOutput.Size = new System.Drawing.Size(692, 316);
            this.lstOutput.TabIndex = 26;
            // 
            // btnClearCals
            // 
            this.btnClearCals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearCals.Location = new System.Drawing.Point(582, 232);
            this.btnClearCals.Name = "btnClearCals";
            this.btnClearCals.Size = new System.Drawing.Size(122, 23);
            this.btnClearCals.TabIndex = 27;
            this.btnClearCals.Text = "Clear Calendars";
            this.btnClearCals.UseVisualStyleBackColor = true;
            this.btnClearCals.Click += new System.EventHandler(this.btnClearCals_Click);
            // 
            // frmGoogleCal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 584);
            this.Controls.Add(this.btnClearCals);
            this.Controls.Add(this.lstOutput);
            this.Controls.Add(this.btnBlockOptions);
            this.Controls.Add(this.btnCCOptions);
            this.Controls.Add(this.btnGGOptions);
            this.Controls.Add(this.lblBlockStatus);
            this.Controls.Add(this.lblCCStatus);
            this.Controls.Add(this.lblGGStatus);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.cboCCCalendar);
            this.Controls.Add(this.cboBlockCalendar);
            this.Controls.Add(this.cboGGCalendar);
            this.Controls.Add(this.btnSaveSettings);
            this.Controls.Add(this.chkIndEvents);
            this.Controls.Add(this.chkGroupEvents);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkGroupRentals);
            this.Controls.Add(this.btnRefreshCals);
            this.Controls.Add(this.btnPost);
            this.Name = "frmGoogleCal";
            this.Text = "Google Calendar Integration";
            this.Load += new System.EventHandler(this.frmGoogleCal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPost;
        private System.Windows.Forms.Button btnRefreshCals;
        private System.Windows.Forms.CheckBox chkGroupRentals;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkGroupEvents;
        private System.Windows.Forms.CheckBox chkIndEvents;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.ComboBox cboGGCalendar;
        private System.Windows.Forms.ComboBox cboBlockCalendar;
        private System.Windows.Forms.ComboBox cboCCCalendar;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label lblGGStatus;
        private System.Windows.Forms.Label lblCCStatus;
        private System.Windows.Forms.Label lblBlockStatus;
        private System.Windows.Forms.Button btnGGOptions;
        private System.Windows.Forms.Button btnCCOptions;
        private System.Windows.Forms.Button btnBlockOptions;
        private System.Windows.Forms.ListBox lstOutput;
        private System.Windows.Forms.Button btnClearCals;
    }
}