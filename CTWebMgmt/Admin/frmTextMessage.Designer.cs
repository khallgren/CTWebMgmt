namespace CTWebMgmt.Admin
{
    partial class frmTextMessage
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
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCharRemaining = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtClientID = new System.Windows.Forms.TextBox();
            this.fraSendTo = new System.Windows.Forms.GroupBox();
            this.radTickedRecords = new System.Windows.Forms.RadioButton();
            this.radIndReg = new System.Windows.Forms.RadioButton();
            this.radCCReg = new System.Windows.Forms.RadioButton();
            this.lstRecipients = new System.Windows.Forms.ListBox();
            this.lblRecipients = new System.Windows.Forms.Label();
            this.fraBlockFilter = new System.Windows.Forms.GroupBox();
            this.radAllBlocks = new System.Windows.Forms.RadioButton();
            this.radBlockDateRange = new System.Windows.Forms.RadioButton();
            this.radSpecificBlock = new System.Windows.Forms.RadioButton();
            this.fraEventFilter = new System.Windows.Forms.GroupBox();
            this.radSpecificEvent = new System.Windows.Forms.RadioButton();
            this.radEventDateRange = new System.Windows.Forms.RadioButton();
            this.radAllEvents = new System.Windows.Forms.RadioButton();
            this.cboStart = new System.Windows.Forms.DateTimePicker();
            this.lblStart = new System.Windows.Forms.Label();
            this.lblEnd = new System.Windows.Forms.Label();
            this.cboEnd = new System.Windows.Forms.DateTimePicker();
            this.chkTicked = new System.Windows.Forms.CheckBox();
            this.cboSpecific = new System.Windows.Forms.ComboBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.fraSendTo.SuspendLayout();
            this.fraBlockFilter.SuspendLayout();
            this.fraEventFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(12, 162);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(352, 73);
            this.txtMessage.TabIndex = 0;
            this.txtMessage.TextChanged += new System.EventHandler(this.txtMessage_TextChanged);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(479, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enter Text Message:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 38);
            this.label2.MaximumSize = new System.Drawing.Size(640, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(520, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "You must have an account established with the third party TextRipple service in o" +
                "rder to send text messages.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 63);
            this.label3.MaximumSize = new System.Drawing.Size(520, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(519, 26);
            this.label3.TabIndex = 4;
            this.label3.Text = "Text messages are limited to 160 characters.  This includes the camp name that is" +
                " automatically appended to the beginning of the message.";
            // 
            // txtCharRemaining
            // 
            this.txtCharRemaining.Enabled = false;
            this.txtCharRemaining.Location = new System.Drawing.Point(309, 136);
            this.txtCharRemaining.Name = "txtCharRemaining";
            this.txtCharRemaining.Size = new System.Drawing.Size(55, 20);
            this.txtCharRemaining.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(189, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Characters Remaining:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Client ID:";
            // 
            // txtClientID
            // 
            this.txtClientID.Enabled = false;
            this.txtClientID.Location = new System.Drawing.Point(82, 116);
            this.txtClientID.Name = "txtClientID";
            this.txtClientID.Size = new System.Drawing.Size(98, 20);
            this.txtClientID.TabIndex = 7;
            // 
            // fraSendTo
            // 
            this.fraSendTo.Controls.Add(this.radCCReg);
            this.fraSendTo.Controls.Add(this.radIndReg);
            this.fraSendTo.Controls.Add(this.radTickedRecords);
            this.fraSendTo.Location = new System.Drawing.Point(15, 241);
            this.fraSendTo.Name = "fraSendTo";
            this.fraSendTo.Size = new System.Drawing.Size(166, 100);
            this.fraSendTo.TabIndex = 9;
            this.fraSendTo.TabStop = false;
            this.fraSendTo.Text = "Send Messages To:";
            // 
            // radTickedRecords
            // 
            this.radTickedRecords.AutoSize = true;
            this.radTickedRecords.Location = new System.Drawing.Point(6, 19);
            this.radTickedRecords.Name = "radTickedRecords";
            this.radTickedRecords.Size = new System.Drawing.Size(101, 17);
            this.radTickedRecords.TabIndex = 0;
            this.radTickedRecords.TabStop = true;
            this.radTickedRecords.Text = "Ticked Records";
            this.radTickedRecords.UseVisualStyleBackColor = true;
            this.radTickedRecords.CheckedChanged += new System.EventHandler(this.radTickedRecords_CheckedChanged);
            // 
            // radIndReg
            // 
            this.radIndReg.AutoSize = true;
            this.radIndReg.Location = new System.Drawing.Point(6, 42);
            this.radIndReg.Name = "radIndReg";
            this.radIndReg.Size = new System.Drawing.Size(145, 17);
            this.radIndReg.TabIndex = 1;
            this.radIndReg.TabStop = true;
            this.radIndReg.Text = "Individual Event Campers";
            this.radIndReg.UseVisualStyleBackColor = true;
            this.radIndReg.CheckedChanged += new System.EventHandler(this.radIndReg_CheckedChanged);
            // 
            // radCCReg
            // 
            this.radCCReg.AutoSize = true;
            this.radCCReg.Location = new System.Drawing.Point(6, 65);
            this.radCCReg.Name = "radCCReg";
            this.radCCReg.Size = new System.Drawing.Size(129, 17);
            this.radCCReg.TabIndex = 2;
            this.radCCReg.TabStop = true;
            this.radCCReg.Text = "Group Event Campers";
            this.radCCReg.UseVisualStyleBackColor = true;
            this.radCCReg.CheckedChanged += new System.EventHandler(this.radCCReg_CheckedChanged);
            // 
            // lstRecipients
            // 
            this.lstRecipients.FormattingEnabled = true;
            this.lstRecipients.Location = new System.Drawing.Point(370, 136);
            this.lstRecipients.Name = "lstRecipients";
            this.lstRecipients.Size = new System.Drawing.Size(184, 121);
            this.lstRecipients.TabIndex = 10;
            // 
            // lblRecipients
            // 
            this.lblRecipients.AutoSize = true;
            this.lblRecipients.Location = new System.Drawing.Point(367, 117);
            this.lblRecipients.Name = "lblRecipients";
            this.lblRecipients.Size = new System.Drawing.Size(60, 13);
            this.lblRecipients.TabIndex = 11;
            this.lblRecipients.Text = "Recipients:";
            // 
            // fraBlockFilter
            // 
            this.fraBlockFilter.Controls.Add(this.radSpecificBlock);
            this.fraBlockFilter.Controls.Add(this.radBlockDateRange);
            this.fraBlockFilter.Controls.Add(this.radAllBlocks);
            this.fraBlockFilter.Location = new System.Drawing.Point(192, 241);
            this.fraBlockFilter.Name = "fraBlockFilter";
            this.fraBlockFilter.Size = new System.Drawing.Size(156, 100);
            this.fraBlockFilter.TabIndex = 12;
            this.fraBlockFilter.TabStop = false;
            this.fraBlockFilter.Text = "Campers registered for:";
            // 
            // radAllBlocks
            // 
            this.radAllBlocks.AutoSize = true;
            this.radAllBlocks.Location = new System.Drawing.Point(6, 19);
            this.radAllBlocks.Name = "radAllBlocks";
            this.radAllBlocks.Size = new System.Drawing.Size(71, 17);
            this.radAllBlocks.TabIndex = 0;
            this.radAllBlocks.TabStop = true;
            this.radAllBlocks.Text = "All Blocks";
            this.radAllBlocks.UseVisualStyleBackColor = true;
            this.radAllBlocks.CheckedChanged += new System.EventHandler(this.radAllBlocks_CheckedChanged);
            // 
            // radBlockDateRange
            // 
            this.radBlockDateRange.AutoSize = true;
            this.radBlockDateRange.Location = new System.Drawing.Point(6, 42);
            this.radBlockDateRange.Name = "radBlockDateRange";
            this.radBlockDateRange.Size = new System.Drawing.Size(122, 17);
            this.radBlockDateRange.TabIndex = 1;
            this.radBlockDateRange.TabStop = true;
            this.radBlockDateRange.Text = "Blocks by Start Date";
            this.radBlockDateRange.UseVisualStyleBackColor = true;
            this.radBlockDateRange.CheckedChanged += new System.EventHandler(this.radBlockDateRange_CheckedChanged);
            // 
            // radSpecificBlock
            // 
            this.radSpecificBlock.AutoSize = true;
            this.radSpecificBlock.Location = new System.Drawing.Point(6, 65);
            this.radSpecificBlock.Name = "radSpecificBlock";
            this.radSpecificBlock.Size = new System.Drawing.Size(93, 17);
            this.radSpecificBlock.TabIndex = 2;
            this.radSpecificBlock.TabStop = true;
            this.radSpecificBlock.Text = "Specific Block";
            this.radSpecificBlock.UseVisualStyleBackColor = true;
            this.radSpecificBlock.CheckedChanged += new System.EventHandler(this.radSpecificBlock_CheckedChanged);
            // 
            // fraEventFilter
            // 
            this.fraEventFilter.Controls.Add(this.radSpecificEvent);
            this.fraEventFilter.Controls.Add(this.radEventDateRange);
            this.fraEventFilter.Controls.Add(this.radAllEvents);
            this.fraEventFilter.Location = new System.Drawing.Point(192, 241);
            this.fraEventFilter.Name = "fraEventFilter";
            this.fraEventFilter.Size = new System.Drawing.Size(156, 100);
            this.fraEventFilter.TabIndex = 13;
            this.fraEventFilter.TabStop = false;
            this.fraEventFilter.Text = "Campers registered for:";
            // 
            // radSpecificEvent
            // 
            this.radSpecificEvent.AutoSize = true;
            this.radSpecificEvent.Location = new System.Drawing.Point(6, 65);
            this.radSpecificEvent.Name = "radSpecificEvent";
            this.radSpecificEvent.Size = new System.Drawing.Size(94, 17);
            this.radSpecificEvent.TabIndex = 2;
            this.radSpecificEvent.TabStop = true;
            this.radSpecificEvent.Text = "Specific Event";
            this.radSpecificEvent.UseVisualStyleBackColor = true;
            this.radSpecificEvent.CheckedChanged += new System.EventHandler(this.radSpecificEvent_CheckedChanged);
            // 
            // radEventDateRange
            // 
            this.radEventDateRange.AutoSize = true;
            this.radEventDateRange.Location = new System.Drawing.Point(6, 42);
            this.radEventDateRange.Name = "radEventDateRange";
            this.radEventDateRange.Size = new System.Drawing.Size(123, 17);
            this.radEventDateRange.TabIndex = 1;
            this.radEventDateRange.TabStop = true;
            this.radEventDateRange.Text = "Events by Start Date";
            this.radEventDateRange.UseVisualStyleBackColor = true;
            this.radEventDateRange.CheckedChanged += new System.EventHandler(this.radEventDateRange_CheckedChanged);
            // 
            // radAllEvents
            // 
            this.radAllEvents.AutoSize = true;
            this.radAllEvents.Location = new System.Drawing.Point(6, 19);
            this.radAllEvents.Name = "radAllEvents";
            this.radAllEvents.Size = new System.Drawing.Size(72, 17);
            this.radAllEvents.TabIndex = 0;
            this.radAllEvents.TabStop = true;
            this.radAllEvents.Text = "All Events";
            this.radAllEvents.UseVisualStyleBackColor = true;
            this.radAllEvents.CheckedChanged += new System.EventHandler(this.radAllEvents_CheckedChanged);
            // 
            // cboStart
            // 
            this.cboStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.cboStart.Location = new System.Drawing.Point(425, 281);
            this.cboStart.Name = "cboStart";
            this.cboStart.Size = new System.Drawing.Size(130, 20);
            this.cboStart.TabIndex = 13;
            this.cboStart.ValueChanged += new System.EventHandler(this.cboStart_ValueChanged);
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(367, 285);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(52, 13);
            this.lblStart.TabIndex = 14;
            this.lblStart.Text = "Between:";
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(389, 311);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(29, 13);
            this.lblEnd.TabIndex = 16;
            this.lblEnd.Text = "And:";
            // 
            // cboEnd
            // 
            this.cboEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.cboEnd.Location = new System.Drawing.Point(424, 307);
            this.cboEnd.Name = "cboEnd";
            this.cboEnd.Size = new System.Drawing.Size(130, 20);
            this.cboEnd.TabIndex = 15;
            this.cboEnd.ValueChanged += new System.EventHandler(this.cboEnd_ValueChanged);
            // 
            // chkTicked
            // 
            this.chkTicked.AutoSize = true;
            this.chkTicked.Location = new System.Drawing.Point(370, 261);
            this.chkTicked.Name = "chkTicked";
            this.chkTicked.Size = new System.Drawing.Size(126, 17);
            this.chkTicked.TabIndex = 17;
            this.chkTicked.Text = "Only Ticked Records";
            this.chkTicked.UseVisualStyleBackColor = true;
            this.chkTicked.CheckedChanged += new System.EventHandler(this.chkTicked_CheckedChanged);
            // 
            // cboSpecific
            // 
            this.cboSpecific.FormattingEnabled = true;
            this.cboSpecific.Location = new System.Drawing.Point(367, 305);
            this.cboSpecific.Name = "cboSpecific";
            this.cboSpecific.Size = new System.Drawing.Size(121, 21);
            this.cboSpecific.TabIndex = 18;
            this.cboSpecific.SelectedIndexChanged += new System.EventHandler(this.cboSpecific_SelectedIndexChanged);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(398, 12);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 19;
            this.btnSend.Text = "&Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // frmTextMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 356);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.fraEventFilter);
            this.Controls.Add(this.cboSpecific);
            this.Controls.Add(this.chkTicked);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.cboEnd);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.cboStart);
            this.Controls.Add(this.fraBlockFilter);
            this.Controls.Add(this.lblRecipients);
            this.Controls.Add(this.lstRecipients);
            this.Controls.Add(this.fraSendTo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtClientID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCharRemaining);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtMessage);
            this.Name = "frmTextMessage";
            this.Text = "Send Text Message(s)";
            this.Load += new System.EventHandler(this.frmTextMessage_Load);
            this.fraSendTo.ResumeLayout(false);
            this.fraSendTo.PerformLayout();
            this.fraBlockFilter.ResumeLayout(false);
            this.fraBlockFilter.PerformLayout();
            this.fraEventFilter.ResumeLayout(false);
            this.fraEventFilter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCharRemaining;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtClientID;
        private System.Windows.Forms.GroupBox fraSendTo;
        private System.Windows.Forms.RadioButton radCCReg;
        private System.Windows.Forms.RadioButton radIndReg;
        private System.Windows.Forms.RadioButton radTickedRecords;
        private System.Windows.Forms.ListBox lstRecipients;
        private System.Windows.Forms.Label lblRecipients;
        private System.Windows.Forms.GroupBox fraBlockFilter;
        private System.Windows.Forms.RadioButton radSpecificBlock;
        private System.Windows.Forms.RadioButton radBlockDateRange;
        private System.Windows.Forms.RadioButton radAllBlocks;
        private System.Windows.Forms.GroupBox fraEventFilter;
        private System.Windows.Forms.RadioButton radSpecificEvent;
        private System.Windows.Forms.RadioButton radEventDateRange;
        private System.Windows.Forms.RadioButton radAllEvents;
        private System.Windows.Forms.DateTimePicker cboStart;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.DateTimePicker cboEnd;
        private System.Windows.Forms.CheckBox chkTicked;
        private System.Windows.Forms.ComboBox cboSpecific;
        private System.Windows.Forms.Button btnSend;
    }
}