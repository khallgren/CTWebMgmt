namespace CTWebMgmt.Admin.Reports
{
    partial class frmReconcileMerchantGatewaySetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReconcileMerchantGatewaySetup));
            this.label1 = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radUnmatched = new System.Windows.Forms.RadioButton();
            this.radCTWeb = new System.Windows.Forms.RadioButton();
            this.radCTLocal = new System.Windows.Forms.RadioButton();
            this.radAllCTTrans = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(546, 104);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // dtpStart
            // 
            this.dtpStart.CustomFormat = "M/d/yyyy h:mm tt";
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Location = new System.Drawing.Point(137, 205);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(200, 20);
            this.dtpStart.TabIndex = 1;
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "M/d/yyyy h:mm tt";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(137, 231);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(200, 20);
            this.dtpEnd.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 211);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Start Date:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 237);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "End Date:";
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreview.Location = new System.Drawing.Point(447, 12);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(68, 23);
            this.btnPreview.TabIndex = 5;
            this.btnPreview.Text = "Pre&view";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(521, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radUnmatched);
            this.groupBox1.Controls.Add(this.radCTWeb);
            this.groupBox1.Controls.Add(this.radCTLocal);
            this.groupBox1.Controls.Add(this.radAllCTTrans);
            this.groupBox1.Location = new System.Drawing.Point(39, 277);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(309, 124);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // radUnmatched
            // 
            this.radUnmatched.AutoSize = true;
            this.radUnmatched.Location = new System.Drawing.Point(21, 88);
            this.radUnmatched.Name = "radUnmatched";
            this.radUnmatched.Size = new System.Drawing.Size(143, 17);
            this.radUnmatched.TabIndex = 14;
            this.radUnmatched.TabStop = true;
            this.radUnmatched.Text = "Un-matched transactions";
            this.radUnmatched.UseVisualStyleBackColor = true;
            // 
            // radCTWeb
            // 
            this.radCTWeb.AutoSize = true;
            this.radCTWeb.Location = new System.Drawing.Point(21, 65);
            this.radCTWeb.Name = "radCTWeb";
            this.radCTWeb.Size = new System.Drawing.Size(243, 17);
            this.radCTWeb.TabIndex = 13;
            this.radCTWeb.TabStop = true;
            this.radCTWeb.Text = "Transactions from CampTrak web applications";
            this.radCTWeb.UseVisualStyleBackColor = true;
            // 
            // radCTLocal
            // 
            this.radCTLocal.AutoSize = true;
            this.radCTLocal.Location = new System.Drawing.Point(21, 42);
            this.radCTLocal.Name = "radCTLocal";
            this.radCTLocal.Size = new System.Drawing.Size(240, 17);
            this.radCTLocal.TabIndex = 12;
            this.radCTLocal.TabStop = true;
            this.radCTLocal.Text = "Transactions from local CampTrak application";
            this.radCTLocal.UseVisualStyleBackColor = true;
            // 
            // radAllCTTrans
            // 
            this.radAllCTTrans.AutoSize = true;
            this.radAllCTTrans.Location = new System.Drawing.Point(21, 19);
            this.radAllCTTrans.Name = "radAllCTTrans";
            this.radAllCTTrans.Size = new System.Drawing.Size(220, 17);
            this.radAllCTTrans.TabIndex = 10;
            this.radAllCTTrans.TabStop = true;
            this.radAllCTTrans.Text = "All transactions originated from CampTrak";
            this.radAllCTTrans.UseVisualStyleBackColor = true;
            // 
            // frmReconcileMerchantGatewaySetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 472);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.label1);
            this.Name = "frmReconcileMerchantGatewaySetup";
            this.Text = "Merchant Gateway Transactions";
            this.Load += new System.EventHandler(this.frmReconcileMerchantGatewaySetup_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radUnmatched;
        private System.Windows.Forms.RadioButton radCTWeb;
        private System.Windows.Forms.RadioButton radCTLocal;
        private System.Windows.Forms.RadioButton radAllCTTrans;
    }
}