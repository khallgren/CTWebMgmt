using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;


namespace CTWebMgmt.Donor
{
    public partial class frmProcessGifts : Form
    {
        int intCurrentRowIdx = 0;

        private BindingSource srcUnProcessedGifts = new BindingSource();
        private OleDbDataAdapter daUnProcessedGifts = new OleDbDataAdapter();

        private BindingSource srcDonorExpress = new BindingSource();
        private OleDbDataAdapter daDonorExpress = new OleDbDataAdapter();

        public frmProcessGifts()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            clsNav.subCloseProcessGifts();
        }

        private void frmProcessGifts_Load(object sender, EventArgs e)
        {
            grdWebGifts.DataSource = srcUnProcessedGifts;
            grdDonorExpress.DataSource = srcDonorExpress;

            radAll.Checked = false;
            radUnprocessed.Checked = true;

            subFillGrids();
        }

        private void subFillGrids()
        {
            try
            {
                string strWHERE = "";

                if (radUnprocessed.Checked)
                {
                    if (strWHERE == "")
                        strWHERE = "WHERE tblWebGift.blnProcessed=0 ";
                    else
                        strWHERE += "AND tblWebGift.blnProcessed=0 ";

                    btnDeleteProcessed.Visible = false;
                }
                else
                    btnDeleteProcessed.Visible = true;

                string strSQL = "SELECT tblWebGift.lngGiftWebID, " +
                                    "tblWebGift.curAmount, " +
                                    "tblWebGift.dteGiftDate, " +
                                    "tblWebRecords.strFirstName AS strFName, tblWebRecords.strLastCoName AS strLName " +
                                "FROM tblWebGift " +
                                    "INNER JOIN tblWebRecords ON tblWebGift.lngRecordWebID = tblWebRecords.lngRecordWebID " +
                                strWHERE +
                                "ORDER BY tblWebGift.dteGiftDate;";

                grdWebGifts.DataSource = srcUnProcessedGifts;

                daUnProcessedGifts = new OleDbDataAdapter(strSQL, clsAppSettings.GetAppSettings().strCTConn);

                OleDbCommandBuilder cbdUnProcessedGifts = new OleDbCommandBuilder(daUnProcessedGifts);

                // Populate a new data table and bind it to the BindingSource.
                DataTable tblGift = new DataTable();

                tblGift.Locale = System.Globalization.CultureInfo.InvariantCulture;

                daUnProcessedGifts.Fill(tblGift);

                srcUnProcessedGifts.DataSource = tblGift;

                grdWebGifts.AutoResizeColumns();

                grdWebGifts.Columns["colDetails"].Width = 100;

                subFillDXGrid();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmProcessGifts.fcnFillSrc", ex);
            }
        }

        private void subFillDXGrid()
        {
            string strSQL = "";

            try
            {
                string strWHERE = "";

                if (radUnprocessed.Checked)
                {
                    if (strWHERE == "")
                        strWHERE = "WHERE tblDonorExpress.blnProcessed=0 ";
                    else
                        strWHERE += "AND tblDonorExpress.blnProcessed=0 ";
                }

                //Fill donor express grid
                strSQL = "SELECT tblDonorExpress.lngDonorExpressID, " +
                            "tblDonorExpress.curGiftAmt, " +
                            "tblDonorExpress.dteSubmitted, " +
                            "tblDonorExpress.strFName, tblDonorExpress.strLName " +
                        "FROM tblDonorExpress " +
                        strWHERE +
                        "ORDER BY tblDonorExpress.dteSubmitted";

                grdDonorExpress.DataSource = srcDonorExpress;

                daDonorExpress = new OleDbDataAdapter(strSQL, clsAppSettings.GetAppSettings().strCTConn);

                OleDbCommandBuilder cbdDonorExpress = new OleDbCommandBuilder(daDonorExpress);

                // Populate a new data table and bind it to the BindingSource.
                DataTable tblDonorExpress = new DataTable();

                tblDonorExpress.Locale = System.Globalization.CultureInfo.InvariantCulture;

                daDonorExpress.Fill(tblDonorExpress);

                srcDonorExpress.DataSource = tblDonorExpress;

                grdDonorExpress.AutoResizeColumns();

                grdDonorExpress.Columns["colDetailsDX"].Width = 100;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error loading the donor express grid: " + ex.Message);
            }
        }

        private void btnDetails_Click(object sender, DataGridViewCellEventArgs e)
        {
            long lngGiftWebID = 0;

            try
            {
                if (e.ColumnIndex >= 0)
                {
                    if (grdWebGifts.Columns[e.ColumnIndex].Name == "colDetails")
                    {
                        try { lngGiftWebID = Convert.ToInt32(grdWebGifts.Rows[e.RowIndex].Cells["colGiftWebID"].Value); }
                        catch { lngGiftWebID = 0; }

                        if (lngGiftWebID > 0)
                        {
                            clsNav.subShowWebGiftDetails(this.MdiParent, lngGiftWebID);
                            intCurrentRowIdx = e.RowIndex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmProcessGifts.btnDetails_Click", ex);
            }
        }

        public void subRefreshGrid()
        {
            subFillGrids();
        }

        private void grdDonorExpress_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            long lngDonorExpressID = 0;

            try
            {
                if (e.ColumnIndex >= 0)
                {
                    if (grdDonorExpress.Columns[e.ColumnIndex].Name == "colDetailsDX")
                    {
                        try { lngDonorExpressID = Convert.ToInt32(grdDonorExpress.Rows[e.RowIndex].Cells["colDonorExpressID"].Value.ToString()); }
                        catch { lngDonorExpressID = 0; }

                        using (frmDonorExpressDetails objDonorExpressDetails = new frmDonorExpressDetails(lngDonorExpressID))
                        {
                            if (objDonorExpressDetails.ShowDialog() == DialogResult.OK)
                                subRefreshGrid();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmProcessGifts.btnDetails_Click", ex);
            }

        }

        private void radDonorExpress_CheckedChanged(object sender, EventArgs e)
        {
            subFillGrids();
        }

        private void btnDeleteProcessed_Click(object sender, EventArgs e)
        {
            string strSQL = "";
            string strMsg = "";

            strMsg = "This will clear donations that have already been processed from the queue.\n\nThe process cannot be reversed. Are you sure you wish to continue?";

            if (MessageBox.Show(strMsg, "CampTrak", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    strSQL = "DELETE tblDonorExpress.* " +
                            "FROM tblDonorExpress " +
                            "WHERE tblDonorExpress.blnProcessed=True";

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        try { cmdDB.ExecuteNonQuery(); }
                        catch { }

                        strSQL = "DELETE tblWebGift.* " +
                                "FROM tblWebGift " +
                                "WHERE tblWebGift.blnProcessed=True";

                        cmdDB.CommandText = strSQL;
                        cmdDB.Parameters.Clear();

                        try { cmdDB.ExecuteNonQuery(); }
                        catch { }
                    }

                    conDB.Close();
                }

                subFillGrids();
            }
        }
    }
}