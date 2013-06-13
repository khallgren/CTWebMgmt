namespace CTWebMgmt.Donor
{
    partial class frmProcessGifts
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
            this.grdWebGifts = new System.Windows.Forms.DataGridView();
            this.colGiftWebID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiftDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetails = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grdDonorExpress = new System.Windows.Forms.DataGridView();
            this.colDonorExpressID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiftAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubmitted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFNameDX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLNameDX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetailsDX = new System.Windows.Forms.DataGridViewButtonColumn();
            this.fraDonorExpress = new System.Windows.Forms.GroupBox();
            this.radAll = new System.Windows.Forms.RadioButton();
            this.radUnprocessed = new System.Windows.Forms.RadioButton();
            this.btnDeleteProcessed = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdWebGifts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDonorExpress)).BeginInit();
            this.fraDonorExpress.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(525, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // grdWebGifts
            // 
            this.grdWebGifts.AllowUserToAddRows = false;
            this.grdWebGifts.AllowUserToDeleteRows = false;
            this.grdWebGifts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdWebGifts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdWebGifts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colGiftWebID,
            this.colAmount,
            this.colGiftDate,
            this.colFName,
            this.colLName,
            this.colDetails});
            this.grdWebGifts.Location = new System.Drawing.Point(12, 41);
            this.grdWebGifts.Name = "grdWebGifts";
            this.grdWebGifts.Size = new System.Drawing.Size(588, 236);
            this.grdWebGifts.TabIndex = 1;
            this.grdWebGifts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.btnDetails_Click);
            // 
            // colGiftWebID
            // 
            this.colGiftWebID.DataPropertyName = "lngGiftWebID";
            this.colGiftWebID.HeaderText = "Gift ID";
            this.colGiftWebID.Name = "colGiftWebID";
            this.colGiftWebID.ReadOnly = true;
            this.colGiftWebID.Visible = false;
            // 
            // colAmount
            // 
            this.colAmount.DataPropertyName = "curAmount";
            this.colAmount.HeaderText = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.ReadOnly = true;
            // 
            // colGiftDate
            // 
            this.colGiftDate.DataPropertyName = "dteGiftDate";
            this.colGiftDate.HeaderText = "Gift Date";
            this.colGiftDate.Name = "colGiftDate";
            this.colGiftDate.ReadOnly = true;
            // 
            // colFName
            // 
            this.colFName.DataPropertyName = "strFName";
            this.colFName.HeaderText = "Donor First Name";
            this.colFName.Name = "colFName";
            this.colFName.ReadOnly = true;
            // 
            // colLName
            // 
            this.colLName.DataPropertyName = "strLName";
            this.colLName.HeaderText = "Donor Last Name";
            this.colLName.Name = "colLName";
            this.colLName.ReadOnly = true;
            // 
            // colDetails
            // 
            this.colDetails.HeaderText = "Details";
            this.colDetails.Name = "colDetails";
            this.colDetails.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Online Gifts:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 306);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Donor Express Gifts:";
            // 
            // grdDonorExpress
            // 
            this.grdDonorExpress.AllowUserToAddRows = false;
            this.grdDonorExpress.AllowUserToDeleteRows = false;
            this.grdDonorExpress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDonorExpress.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDonorExpress.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDonorExpressID,
            this.colGiftAmount,
            this.colSubmitted,
            this.colFNameDX,
            this.colLNameDX,
            this.colDetailsDX});
            this.grdDonorExpress.Location = new System.Drawing.Point(15, 325);
            this.grdDonorExpress.Name = "grdDonorExpress";
            this.grdDonorExpress.Size = new System.Drawing.Size(585, 204);
            this.grdDonorExpress.TabIndex = 4;
            this.grdDonorExpress.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdDonorExpress_CellClick);
            // 
            // colDonorExpressID
            // 
            this.colDonorExpressID.DataPropertyName = "lngDonorExpressID";
            this.colDonorExpressID.HeaderText = "ID";
            this.colDonorExpressID.Name = "colDonorExpressID";
            this.colDonorExpressID.Visible = false;
            // 
            // colGiftAmount
            // 
            this.colGiftAmount.DataPropertyName = "curGiftAmt";
            this.colGiftAmount.HeaderText = "Amount";
            this.colGiftAmount.Name = "colGiftAmount";
            // 
            // colSubmitted
            // 
            this.colSubmitted.DataPropertyName = "dteSubmitted";
            this.colSubmitted.HeaderText = "Gift Date";
            this.colSubmitted.Name = "colSubmitted";
            // 
            // colFNameDX
            // 
            this.colFNameDX.DataPropertyName = "strFName";
            this.colFNameDX.HeaderText = "Donor First Name";
            this.colFNameDX.Name = "colFNameDX";
            // 
            // colLNameDX
            // 
            this.colLNameDX.DataPropertyName = "strLName";
            this.colLNameDX.HeaderText = "Donor Last Name";
            this.colLNameDX.Name = "colLNameDX";
            // 
            // colDetailsDX
            // 
            this.colDetailsDX.HeaderText = "Details";
            this.colDetailsDX.Name = "colDetailsDX";
            // 
            // fraDonorExpress
            // 
            this.fraDonorExpress.Controls.Add(this.radAll);
            this.fraDonorExpress.Controls.Add(this.radUnprocessed);
            this.fraDonorExpress.Location = new System.Drawing.Point(313, 283);
            this.fraDonorExpress.Name = "fraDonorExpress";
            this.fraDonorExpress.Size = new System.Drawing.Size(287, 36);
            this.fraDonorExpress.TabIndex = 5;
            this.fraDonorExpress.TabStop = false;
            // 
            // radAll
            // 
            this.radAll.AutoSize = true;
            this.radAll.Location = new System.Drawing.Point(177, 12);
            this.radAll.Name = "radAll";
            this.radAll.Size = new System.Drawing.Size(60, 17);
            this.radAll.TabIndex = 2;
            this.radAll.TabStop = true;
            this.radAll.Text = "All Gifts";
            this.radAll.UseVisualStyleBackColor = true;
            this.radAll.CheckedChanged += new System.EventHandler(this.radDonorExpress_CheckedChanged);
            // 
            // radUnprocessed
            // 
            this.radUnprocessed.AutoSize = true;
            this.radUnprocessed.Location = new System.Drawing.Point(6, 12);
            this.radUnprocessed.Name = "radUnprocessed";
            this.radUnprocessed.Size = new System.Drawing.Size(115, 17);
            this.radUnprocessed.TabIndex = 1;
            this.radUnprocessed.TabStop = true;
            this.radUnprocessed.Text = "Un-processed Gifts";
            this.radUnprocessed.UseVisualStyleBackColor = true;
            this.radUnprocessed.CheckedChanged += new System.EventHandler(this.radDonorExpress_CheckedChanged);
            // 
            // btnDeleteProcessed
            // 
            this.btnDeleteProcessed.Location = new System.Drawing.Point(156, 292);
            this.btnDeleteProcessed.Name = "btnDeleteProcessed";
            this.btnDeleteProcessed.Size = new System.Drawing.Size(151, 23);
            this.btnDeleteProcessed.TabIndex = 6;
            this.btnDeleteProcessed.Text = "Remove Processed Gifts";
            this.btnDeleteProcessed.UseVisualStyleBackColor = true;
            this.btnDeleteProcessed.Click += new System.EventHandler(this.btnDeleteProcessed_Click);
            // 
            // frmProcessGifts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 541);
            this.Controls.Add(this.btnDeleteProcessed);
            this.Controls.Add(this.fraDonorExpress);
            this.Controls.Add(this.grdDonorExpress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grdWebGifts);
            this.Controls.Add(this.btnClose);
            this.Name = "frmProcessGifts";
            this.Text = "Process Gifts";
            this.Load += new System.EventHandler(this.frmProcessGifts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdWebGifts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDonorExpress)).EndInit();
            this.fraDonorExpress.ResumeLayout(false);
            this.fraDonorExpress.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView grdWebGifts;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiftWebID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiftDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLName;
        private System.Windows.Forms.DataGridViewButtonColumn colDetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView grdDonorExpress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonorExpressID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiftAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubmitted;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFNameDX;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLNameDX;
        private System.Windows.Forms.DataGridViewButtonColumn colDetailsDX;
        private System.Windows.Forms.GroupBox fraDonorExpress;
        private System.Windows.Forms.RadioButton radAll;
        private System.Windows.Forms.RadioButton radUnprocessed;
        private System.Windows.Forms.Button btnDeleteProcessed;
    }
}