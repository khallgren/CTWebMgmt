namespace CTWebMgmt.Admin
{
    partial class frmGoogleCalULOptions
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radProgramTypeFilter = new System.Windows.Forms.RadioButton();
            this.radProgramTypeAll = new System.Windows.Forms.RadioButton();
            this.fraStatus = new System.Windows.Forms.GroupBox();
            this.radStatusFilter = new System.Windows.Forms.RadioButton();
            this.radStatusAll = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radStartDateFilter = new System.Windows.Forms.RadioButton();
            this.radStartDateAll = new System.Windows.Forms.RadioButton();
            this.btnContinue = new System.Windows.Forms.Button();
            this.lstProgramTypes = new System.Windows.Forms.ListBox();
            this.lstStatus = new System.Windows.Forms.ListBox();
            this.txtStart = new System.Windows.Forms.TextBox();
            this.lblStart = new System.Windows.Forms.Label();
            this.lblEnd = new System.Windows.Forms.Label();
            this.txtEnd = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.fraStatus.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radProgramTypeFilter);
            this.groupBox1.Controls.Add(this.radProgramTypeAll);
            this.groupBox1.Location = new System.Drawing.Point(32, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(161, 66);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Program Type";
            // 
            // radProgramTypeFilter
            // 
            this.radProgramTypeFilter.AutoSize = true;
            this.radProgramTypeFilter.Location = new System.Drawing.Point(6, 42);
            this.radProgramTypeFilter.Name = "radProgramTypeFilter";
            this.radProgramTypeFilter.Size = new System.Drawing.Size(129, 17);
            this.radProgramTypeFilter.TabIndex = 1;
            this.radProgramTypeFilter.TabStop = true;
            this.radProgramTypeFilter.Text = "Select Program Types";
            this.radProgramTypeFilter.UseVisualStyleBackColor = true;
            this.radProgramTypeFilter.CheckedChanged += new System.EventHandler(this.subVisibility);
            // 
            // radProgramTypeAll
            // 
            this.radProgramTypeAll.AutoSize = true;
            this.radProgramTypeAll.Checked = true;
            this.radProgramTypeAll.Location = new System.Drawing.Point(6, 19);
            this.radProgramTypeAll.Name = "radProgramTypeAll";
            this.radProgramTypeAll.Size = new System.Drawing.Size(110, 17);
            this.radProgramTypeAll.TabIndex = 0;
            this.radProgramTypeAll.TabStop = true;
            this.radProgramTypeAll.Text = "All Program Types";
            this.radProgramTypeAll.UseVisualStyleBackColor = true;
            this.radProgramTypeAll.CheckedChanged += new System.EventHandler(this.subVisibility);
            // 
            // fraStatus
            // 
            this.fraStatus.Controls.Add(this.radStatusFilter);
            this.fraStatus.Controls.Add(this.radStatusAll);
            this.fraStatus.Location = new System.Drawing.Point(32, 216);
            this.fraStatus.Name = "fraStatus";
            this.fraStatus.Size = new System.Drawing.Size(161, 73);
            this.fraStatus.TabIndex = 1;
            this.fraStatus.TabStop = false;
            this.fraStatus.Text = "Status";
            // 
            // radStatusFilter
            // 
            this.radStatusFilter.AutoSize = true;
            this.radStatusFilter.Location = new System.Drawing.Point(6, 42);
            this.radStatusFilter.Name = "radStatusFilter";
            this.radStatusFilter.Size = new System.Drawing.Size(119, 17);
            this.radStatusFilter.TabIndex = 3;
            this.radStatusFilter.TabStop = true;
            this.radStatusFilter.Text = "Select Event Status";
            this.radStatusFilter.UseVisualStyleBackColor = true;
            this.radStatusFilter.CheckedChanged += new System.EventHandler(this.subVisibility);
            // 
            // radStatusAll
            // 
            this.radStatusAll.AutoSize = true;
            this.radStatusAll.Checked = true;
            this.radStatusAll.Location = new System.Drawing.Point(6, 19);
            this.radStatusAll.Name = "radStatusAll";
            this.radStatusAll.Size = new System.Drawing.Size(80, 17);
            this.radStatusAll.TabIndex = 2;
            this.radStatusAll.TabStop = true;
            this.radStatusAll.Text = "All Statuses";
            this.radStatusAll.UseVisualStyleBackColor = true;
            this.radStatusAll.CheckedChanged += new System.EventHandler(this.subVisibility);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radStartDateFilter);
            this.groupBox3.Controls.Add(this.radStartDateAll);
            this.groupBox3.Location = new System.Drawing.Point(32, 131);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(161, 69);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Start Date";
            // 
            // radStartDateFilter
            // 
            this.radStartDateFilter.AutoSize = true;
            this.radStartDateFilter.Location = new System.Drawing.Point(6, 42);
            this.radStartDateFilter.Name = "radStartDateFilter";
            this.radStartDateFilter.Size = new System.Drawing.Size(111, 17);
            this.radStartDateFilter.TabIndex = 3;
            this.radStartDateFilter.TabStop = true;
            this.radStartDateFilter.Text = "Select Start Dates";
            this.radStartDateFilter.UseVisualStyleBackColor = true;
            this.radStartDateFilter.CheckedChanged += new System.EventHandler(this.subVisibility);
            // 
            // radStartDateAll
            // 
            this.radStartDateAll.AutoSize = true;
            this.radStartDateAll.Checked = true;
            this.radStartDateAll.Location = new System.Drawing.Point(6, 19);
            this.radStartDateAll.Name = "radStartDateAll";
            this.radStartDateAll.Size = new System.Drawing.Size(92, 17);
            this.radStartDateAll.TabIndex = 2;
            this.radStartDateAll.TabStop = true;
            this.radStartDateAll.Text = "All Start Dates";
            this.radStartDateAll.UseVisualStyleBackColor = true;
            this.radStartDateAll.CheckedChanged += new System.EventHandler(this.subVisibility);
            // 
            // btnContinue
            // 
            this.btnContinue.Location = new System.Drawing.Point(362, 12);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(75, 23);
            this.btnContinue.TabIndex = 2;
            this.btnContinue.Text = "&Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // lstProgramTypes
            // 
            this.lstProgramTypes.FormattingEnabled = true;
            this.lstProgramTypes.Location = new System.Drawing.Point(199, 41);
            this.lstProgramTypes.Name = "lstProgramTypes";
            this.lstProgramTypes.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstProgramTypes.Size = new System.Drawing.Size(238, 82);
            this.lstProgramTypes.TabIndex = 3;
            this.lstProgramTypes.Visible = false;
            // 
            // lstStatus
            // 
            this.lstStatus.FormattingEnabled = true;
            this.lstStatus.Location = new System.Drawing.Point(199, 216);
            this.lstStatus.Name = "lstStatus";
            this.lstStatus.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstStatus.Size = new System.Drawing.Size(238, 69);
            this.lstStatus.TabIndex = 4;
            this.lstStatus.Visible = false;
            // 
            // txtStart
            // 
            this.txtStart.Location = new System.Drawing.Point(240, 149);
            this.txtStart.Name = "txtStart";
            this.txtStart.Size = new System.Drawing.Size(100, 20);
            this.txtStart.TabIndex = 5;
            this.txtStart.Visible = false;
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(199, 152);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(33, 13);
            this.lblStart.TabIndex = 6;
            this.lblStart.Text = "From:";
            this.lblStart.Visible = false;
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(199, 178);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(23, 13);
            this.lblEnd.TabIndex = 8;
            this.lblEnd.Text = "To:";
            this.lblEnd.Visible = false;
            // 
            // txtEnd
            // 
            this.txtEnd.Location = new System.Drawing.Point(240, 175);
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.Size = new System.Drawing.Size(100, 20);
            this.txtEnd.TabIndex = 7;
            this.txtEnd.Visible = false;
            // 
            // frmGoogleCalULOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 301);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.txtEnd);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.txtStart);
            this.Controls.Add(this.lstStatus);
            this.Controls.Add(this.lstProgramTypes);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.fraStatus);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmGoogleCalULOptions";
            this.Text = "Upload Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.fraStatus.ResumeLayout(false);
            this.fraStatus.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radProgramTypeFilter;
        private System.Windows.Forms.RadioButton radProgramTypeAll;
        private System.Windows.Forms.GroupBox fraStatus;
        private System.Windows.Forms.RadioButton radStatusFilter;
        private System.Windows.Forms.RadioButton radStatusAll;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radStartDateFilter;
        private System.Windows.Forms.RadioButton radStartDateAll;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.ListBox lstProgramTypes;
        private System.Windows.Forms.ListBox lstStatus;
        private System.Windows.Forms.TextBox txtStart;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.TextBox txtEnd;
    }
}