namespace CTWebMgmt.GGCC
{
    partial class frmUploadEvents
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
            this.txtEndDate = new System.Windows.Forms.TextBox();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.radAllEvents = new System.Windows.Forms.RadioButton();
            this.radSpecificEvent = new System.Windows.Forms.RadioButton();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.cboSpecificEvent = new System.Windows.Forms.ComboBox();
            this.fraPickEvent = new System.Windows.Forms.GroupBox();
            this.lblEventStartDates = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.txtStartDate = new System.Windows.Forms.TextBox();
            this.cboProgram = new System.Windows.Forms.ComboBox();
            this.lblProgram = new System.Windows.Forms.Label();
            this.lstStatus = new System.Windows.Forms.ListBox();
            this.chkClearExisting = new System.Windows.Forms.CheckBox();
            this.lblClearExisting = new System.Windows.Forms.Label();
            this.lblTooltip = new System.Windows.Forms.Label();
            this.fraPickEvent.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtEndDate
            // 
            this.txtEndDate.Location = new System.Drawing.Point(42, 29);
            this.txtEndDate.Name = "txtEndDate";
            this.txtEndDate.Size = new System.Drawing.Size(100, 20);
            this.txtEndDate.TabIndex = 7;
            this.txtEndDate.MouseLeave += new System.EventHandler(this.lblEventStartDates_MouseLeave);
            this.txtEndDate.Leave += new System.EventHandler(this.txtEndDate_Leave);
            this.txtEndDate.MouseEnter += new System.EventHandler(this.lblEventStartDates_MouseEnter);
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(4, 6);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(32, 13);
            this.lblStartDate.TabIndex = 6;
            this.lblStartDate.Text = "Start:";
            this.lblStartDate.MouseLeave += new System.EventHandler(this.lblEventStartDates_MouseLeave);
            this.lblStartDate.MouseEnter += new System.EventHandler(this.lblEventStartDates_MouseEnter);
            // 
            // radAllEvents
            // 
            this.radAllEvents.AutoSize = true;
            this.radAllEvents.Location = new System.Drawing.Point(7, 19);
            this.radAllEvents.Name = "radAllEvents";
            this.radAllEvents.Size = new System.Drawing.Size(72, 17);
            this.radAllEvents.TabIndex = 3;
            this.radAllEvents.TabStop = true;
            this.radAllEvents.Text = "All Events";
            this.radAllEvents.UseVisualStyleBackColor = true;
            this.radAllEvents.CheckedChanged += new System.EventHandler(this.subChooseEvent);
            // 
            // radSpecificEvent
            // 
            this.radSpecificEvent.AutoSize = true;
            this.radSpecificEvent.Location = new System.Drawing.Point(7, 42);
            this.radSpecificEvent.Name = "radSpecificEvent";
            this.radSpecificEvent.Size = new System.Drawing.Size(94, 17);
            this.radSpecificEvent.TabIndex = 2;
            this.radSpecificEvent.TabStop = true;
            this.radSpecificEvent.Text = "Specific Event";
            this.radSpecificEvent.UseVisualStyleBackColor = true;
            this.radSpecificEvent.CheckedChanged += new System.EventHandler(this.subChooseEvent);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(425, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 21;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(344, 12);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 20;
            this.btnUpload.Text = "&Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(4, 32);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(29, 13);
            this.lblEndDate.TabIndex = 8;
            this.lblEndDate.Text = "End:";
            this.lblEndDate.MouseLeave += new System.EventHandler(this.lblEventStartDates_MouseLeave);
            this.lblEndDate.MouseEnter += new System.EventHandler(this.lblEventStartDates_MouseEnter);
            // 
            // cboSpecificEvent
            // 
            this.cboSpecificEvent.FormattingEnabled = true;
            this.cboSpecificEvent.Location = new System.Drawing.Point(119, 121);
            this.cboSpecificEvent.Name = "cboSpecificEvent";
            this.cboSpecificEvent.Size = new System.Drawing.Size(359, 21);
            this.cboSpecificEvent.TabIndex = 19;
            // 
            // fraPickEvent
            // 
            this.fraPickEvent.Controls.Add(this.radAllEvents);
            this.fraPickEvent.Controls.Add(this.radSpecificEvent);
            this.fraPickEvent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fraPickEvent.Location = new System.Drawing.Point(12, 80);
            this.fraPickEvent.Name = "fraPickEvent";
            this.fraPickEvent.Size = new System.Drawing.Size(106, 62);
            this.fraPickEvent.TabIndex = 14;
            this.fraPickEvent.TabStop = false;
            this.fraPickEvent.Text = "Event Name";
            // 
            // lblEventStartDates
            // 
            this.lblEventStartDates.AutoSize = true;
            this.lblEventStartDates.Location = new System.Drawing.Point(9, 164);
            this.lblEventStartDates.Name = "lblEventStartDates";
            this.lblEventStartDates.Size = new System.Drawing.Size(144, 13);
            this.lblEventStartDates.TabIndex = 17;
            this.lblEventStartDates.Text = "Event Start Dates (m/d/yyyy)";
            this.lblEventStartDates.MouseLeave += new System.EventHandler(this.lblEventStartDates_MouseLeave);
            this.lblEventStartDates.MouseEnter += new System.EventHandler(this.lblEventStartDates_MouseEnter);
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.lblEndDate);
            this.Panel1.Controls.Add(this.txtEndDate);
            this.Panel1.Controls.Add(this.lblStartDate);
            this.Panel1.Controls.Add(this.txtStartDate);
            this.Panel1.Location = new System.Drawing.Point(12, 180);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(154, 57);
            this.Panel1.TabIndex = 18;
            // 
            // txtStartDate
            // 
            this.txtStartDate.Location = new System.Drawing.Point(42, 3);
            this.txtStartDate.Name = "txtStartDate";
            this.txtStartDate.Size = new System.Drawing.Size(100, 20);
            this.txtStartDate.TabIndex = 5;
            this.txtStartDate.MouseLeave += new System.EventHandler(this.lblEventStartDates_MouseLeave);
            this.txtStartDate.Leave += new System.EventHandler(this.txtStartDate_Leave);
            this.txtStartDate.MouseEnter += new System.EventHandler(this.lblEventStartDates_MouseEnter);
            // 
            // cboProgram
            // 
            this.cboProgram.FormattingEnabled = true;
            this.cboProgram.Location = new System.Drawing.Point(64, 271);
            this.cboProgram.Name = "cboProgram";
            this.cboProgram.Size = new System.Drawing.Size(121, 21);
            this.cboProgram.TabIndex = 15;
            // 
            // lblProgram
            // 
            this.lblProgram.AutoSize = true;
            this.lblProgram.Location = new System.Drawing.Point(9, 274);
            this.lblProgram.Name = "lblProgram";
            this.lblProgram.Size = new System.Drawing.Size(49, 13);
            this.lblProgram.TabIndex = 16;
            this.lblProgram.Text = "Program:";
            // 
            // lstStatus
            // 
            this.lstStatus.FormattingEnabled = true;
            this.lstStatus.Location = new System.Drawing.Point(64, 311);
            this.lstStatus.Name = "lstStatus";
            this.lstStatus.Size = new System.Drawing.Size(245, 121);
            this.lstStatus.TabIndex = 22;
            // 
            // chkClearExisting
            // 
            this.chkClearExisting.AutoSize = true;
            this.chkClearExisting.Location = new System.Drawing.Point(12, 57);
            this.chkClearExisting.Name = "chkClearExisting";
            this.chkClearExisting.Size = new System.Drawing.Size(125, 17);
            this.chkClearExisting.TabIndex = 23;
            this.chkClearExisting.Text = "Clear Existing Events";
            this.chkClearExisting.UseVisualStyleBackColor = true;
            this.chkClearExisting.MouseLeave += new System.EventHandler(this.chkClearExisting_MouseLeave);
            this.chkClearExisting.MouseEnter += new System.EventHandler(this.chkClearExisting_MouseEnter);
            this.chkClearExisting.CheckedChanged += new System.EventHandler(this.chkClearExisting_CheckedChanged);
            // 
            // lblClearExisting
            // 
            this.lblClearExisting.ForeColor = System.Drawing.Color.Red;
            this.lblClearExisting.Location = new System.Drawing.Point(143, 58);
            this.lblClearExisting.Name = "lblClearExisting";
            this.lblClearExisting.Size = new System.Drawing.Size(229, 44);
            this.lblClearExisting.TabIndex = 24;
            this.lblClearExisting.Text = "Please make sure you have downloaded all existing event registrations before clea" +
                "ring event options.";
            this.lblClearExisting.Visible = false;
            // 
            // lblTooltip
            // 
            this.lblTooltip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTooltip.Location = new System.Drawing.Point(12, 12);
            this.lblTooltip.Name = "lblTooltip";
            this.lblTooltip.Size = new System.Drawing.Size(297, 36);
            this.lblTooltip.TabIndex = 25;
            this.lblTooltip.Text = "Check this box to clear previously uploaded events from the web server before upl" +
                "oading the new event definitions.";
            this.lblTooltip.Visible = false;
            // 
            // frmUploadEvents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 448);
            this.Controls.Add(this.lblTooltip);
            this.Controls.Add(this.lblClearExisting);
            this.Controls.Add(this.chkClearExisting);
            this.Controls.Add(this.lstStatus);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.cboSpecificEvent);
            this.Controls.Add(this.fraPickEvent);
            this.Controls.Add(this.lblEventStartDates);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.cboProgram);
            this.Controls.Add(this.lblProgram);
            this.Name = "frmUploadEvents";
            this.Text = "Upload Group Events";
            this.Load += new System.EventHandler(this.frmUploadEvents_Load);
            this.fraPickEvent.ResumeLayout(false);
            this.fraPickEvent.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtEndDate;
        internal System.Windows.Forms.Label lblStartDate;
        internal System.Windows.Forms.RadioButton radAllEvents;
        internal System.Windows.Forms.RadioButton radSpecificEvent;
        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.Button btnUpload;
        internal System.Windows.Forms.Label lblEndDate;
        internal System.Windows.Forms.ComboBox cboSpecificEvent;
        internal System.Windows.Forms.GroupBox fraPickEvent;
        internal System.Windows.Forms.Label lblEventStartDates;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.TextBox txtStartDate;
        internal System.Windows.Forms.ComboBox cboProgram;
        internal System.Windows.Forms.Label lblProgram;
        private System.Windows.Forms.ListBox lstStatus;
        private System.Windows.Forms.CheckBox chkClearExisting;
        private System.Windows.Forms.Label lblClearExisting;
        private System.Windows.Forms.Label lblTooltip;
    }
}