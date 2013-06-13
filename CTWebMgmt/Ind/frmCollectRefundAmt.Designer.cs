namespace CTWebMgmt.Ind
{
    partial class frmCollectRefundAmt
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDeposit = new System.Windows.Forms.Label();
            this.lblDonation = new System.Windows.Forms.Label();
            this.lblSpending = new System.Windows.Forms.Label();
            this.txtAmt = new System.Windows.Forms.TextBox();
            this.btnContinue = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter the amount you would like to refund:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Deposit Amount:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Spending Money:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Donation:";
            // 
            // lblDeposit
            // 
            this.lblDeposit.AutoSize = true;
            this.lblDeposit.Location = new System.Drawing.Point(108, 41);
            this.lblDeposit.Name = "lblDeposit";
            this.lblDeposit.Size = new System.Drawing.Size(34, 13);
            this.lblDeposit.TabIndex = 4;
            this.lblDeposit.Text = "$0.00";
            // 
            // lblDonation
            // 
            this.lblDonation.AutoSize = true;
            this.lblDonation.Location = new System.Drawing.Point(108, 87);
            this.lblDonation.Name = "lblDonation";
            this.lblDonation.Size = new System.Drawing.Size(34, 13);
            this.lblDonation.TabIndex = 5;
            this.lblDonation.Text = "$0.00";
            // 
            // lblSpending
            // 
            this.lblSpending.AutoSize = true;
            this.lblSpending.Location = new System.Drawing.Point(108, 64);
            this.lblSpending.Name = "lblSpending";
            this.lblSpending.Size = new System.Drawing.Size(34, 13);
            this.lblSpending.TabIndex = 6;
            this.lblSpending.Text = "$0.00";
            // 
            // txtAmt
            // 
            this.txtAmt.Location = new System.Drawing.Point(224, 130);
            this.txtAmt.Name = "txtAmt";
            this.txtAmt.Size = new System.Drawing.Size(72, 20);
            this.txtAmt.TabIndex = 7;
            // 
            // btnContinue
            // 
            this.btnContinue.Location = new System.Drawing.Point(185, 12);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(75, 23);
            this.btnContinue.TabIndex = 8;
            this.btnContinue.Text = "&Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(266, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "C&ancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmCollectRefundAmt
            // 
            this.AcceptButton = this.btnContinue;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(352, 187);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.txtAmt);
            this.Controls.Add(this.lblSpending);
            this.Controls.Add(this.lblDonation);
            this.Controls.Add(this.lblDeposit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmCollectRefundAmt";
            this.Text = "Enter Refund Amount";
            this.Load += new System.EventHandler(this.frmCollectRefundAmt_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDeposit;
        private System.Windows.Forms.Label lblDonation;
        private System.Windows.Forms.Label lblSpending;
        private System.Windows.Forms.TextBox txtAmt;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Button btnCancel;
    }
}