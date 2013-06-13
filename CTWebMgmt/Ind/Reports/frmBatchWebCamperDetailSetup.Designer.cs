namespace CTWebMgmt.Ind.Reports
{
    partial class frmBatchWebCamperDetailSetup
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
            this.radUnprocessedReg = new System.Windows.Forms.RadioButton();
            this.radProcessedReg = new System.Windows.Forms.RadioButton();
            this.radAllReg = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radSpecificDate = new System.Windows.Forms.RadioButton();
            this.radDateRange = new System.Windows.Forms.RadioButton();
            this.radAllDates = new System.Windows.Forms.RadioButton();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radUnprocessedReg);
            this.groupBox1.Controls.Add(this.radProcessedReg);
            this.groupBox1.Controls.Add(this.radAllReg);
            this.groupBox1.Location = new System.Drawing.Point(12, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(182, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Processed?";
            // 
            // radUnprocessedReg
            // 
            this.radUnprocessedReg.AutoSize = true;
            this.radUnprocessedReg.Location = new System.Drawing.Point(6, 42);
            this.radUnprocessedReg.Name = "radUnprocessedReg";
            this.radUnprocessedReg.Size = new System.Drawing.Size(156, 17);
            this.radUnprocessedReg.TabIndex = 4;
            this.radUnprocessedReg.TabStop = true;
            this.radUnprocessedReg.Text = "Un-Processed Registrations";
            this.radUnprocessedReg.UseVisualStyleBackColor = true;
            // 
            // radProcessedReg
            // 
            this.radProcessedReg.AutoSize = true;
            this.radProcessedReg.Location = new System.Drawing.Point(6, 65);
            this.radProcessedReg.Name = "radProcessedReg";
            this.radProcessedReg.Size = new System.Drawing.Size(139, 17);
            this.radProcessedReg.TabIndex = 3;
            this.radProcessedReg.TabStop = true;
            this.radProcessedReg.Text = "Processed Registrations";
            this.radProcessedReg.UseVisualStyleBackColor = true;
            // 
            // radAllReg
            // 
            this.radAllReg.AutoSize = true;
            this.radAllReg.Location = new System.Drawing.Point(6, 19);
            this.radAllReg.Name = "radAllReg";
            this.radAllReg.Size = new System.Drawing.Size(100, 17);
            this.radAllReg.TabIndex = 2;
            this.radAllReg.TabStop = true;
            this.radAllReg.Text = "All Registrations";
            this.radAllReg.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radSpecificDate);
            this.groupBox2.Controls.Add(this.radDateRange);
            this.groupBox2.Controls.Add(this.radAllDates);
            this.groupBox2.Location = new System.Drawing.Point(12, 163);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(182, 100);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Registration Date";
            // 
            // radSpecificDate
            // 
            this.radSpecificDate.AutoSize = true;
            this.radSpecificDate.Location = new System.Drawing.Point(6, 42);
            this.radSpecificDate.Name = "radSpecificDate";
            this.radSpecificDate.Size = new System.Drawing.Size(89, 17);
            this.radSpecificDate.TabIndex = 4;
            this.radSpecificDate.TabStop = true;
            this.radSpecificDate.Text = "Specific Date";
            this.radSpecificDate.UseVisualStyleBackColor = true;
            this.radSpecificDate.CheckedChanged += new System.EventHandler(this.rad_CheckedChanged);
            // 
            // radDateRange
            // 
            this.radDateRange.AutoSize = true;
            this.radDateRange.Location = new System.Drawing.Point(6, 65);
            this.radDateRange.Name = "radDateRange";
            this.radDateRange.Size = new System.Drawing.Size(83, 17);
            this.radDateRange.TabIndex = 3;
            this.radDateRange.TabStop = true;
            this.radDateRange.Text = "Date Range";
            this.radDateRange.UseVisualStyleBackColor = true;
            this.radDateRange.CheckedChanged += new System.EventHandler(this.rad_CheckedChanged);
            // 
            // radAllDates
            // 
            this.radAllDates.AutoSize = true;
            this.radAllDates.Location = new System.Drawing.Point(6, 19);
            this.radAllDates.Name = "radAllDates";
            this.radAllDates.Size = new System.Drawing.Size(67, 17);
            this.radAllDates.TabIndex = 2;
            this.radAllDates.TabStop = true;
            this.radAllDates.Text = "All Dates";
            this.radAllDates.UseVisualStyleBackColor = true;
            this.radAllDates.CheckedChanged += new System.EventHandler(this.rad_CheckedChanged);
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(374, 12);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(75, 23);
            this.btnPreview.TabIndex = 6;
            this.btnPreview.Text = "Pre&view";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(455, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(200, 207);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(58, 13);
            this.lblStartDate.TabIndex = 10;
            this.lblStartDate.Text = "Start Date:";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(200, 230);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(55, 13);
            this.lblEndDate.TabIndex = 11;
            this.lblEndDate.Text = "End Date:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(330, 202);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(200, 20);
            this.dtpStartDate.TabIndex = 12;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(330, 225);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(200, 20);
            this.dtpEndDate.TabIndex = 13;
            // 
            // frmBatchWebCamperDetailSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 275);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmBatchWebCamperDetailSetup";
            this.Text = "Batch Web Camper Detail Report Setup";
            this.Load += new System.EventHandler(this.frmBatchWebCamperDetailSetup_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radUnprocessedReg;
        private System.Windows.Forms.RadioButton radProcessedReg;
        private System.Windows.Forms.RadioButton radAllReg;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radSpecificDate;
        private System.Windows.Forms.RadioButton radDateRange;
        private System.Windows.Forms.RadioButton radAllDates;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
    }
}