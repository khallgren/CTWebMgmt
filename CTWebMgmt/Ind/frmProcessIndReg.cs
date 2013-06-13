using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;


namespace CTWebMgmt.Ind
{
    public partial class frmProcessIndReg : Form
    {
        public frmProcessIndReg()
        {
            InitializeComponent();
        }

        private void subFillGrid()
        {
            try
            {
                string strSQL = "";

                string strWhere = "";

                if (radUnprocessedCampers.Checked)
                    strWhere = "WHERE tblWebIndRegistrations.blnProcessed=0 AND " +
                            "tblWebIndRegBlockChoices.lngChoice=1 ";
                else
                    strWhere = "WHERE tblWebIndRegBlockChoices.lngChoice=1 ";

                strSQL = "SELECT tblWebIndRegistrations.lngRegistrationWebID, " +
                            "tblWebIndRegistrations.dteRegistrationDate, " +
                            "[tblWebRecords].[strLastCoName] & \", \" & [tblWebRecords].[strFirstName] AS strName, tblBlock.strBlockCode " +
                        "FROM ((tblWebIndRegistrations " +
                            "INNER JOIN tblWebIndRegBlockChoices ON tblWebIndRegistrations.lngRegistrationWebID = tblWebIndRegBlockChoices.lngRegistrationWebID) " +
                            "INNER JOIN tblBlock ON tblWebIndRegBlockChoices.lngBlockID = tblBlock.lngBlockID) " +
                            "INNER JOIN tblWebRecords ON tblWebIndRegistrations.lngRecordWebID = tblWebRecords.lngRecordWebID " +
                        strWhere +
                        "ORDER BY tblWebIndRegistrations.dteRegistrationDate";

                using (OleDbDataAdapter daIndReg = new OleDbDataAdapter(strSQL, clsAppSettings.GetAppSettings().strCTConn))
                {
                    using (OleDbCommandBuilder cmdGGCCReg = new OleDbCommandBuilder(daIndReg))
                    {
                        // Populate a new data table and bind it to the BindingSource.
                        using (DataTable tblIndReg = new DataTable())
                        {
                            tblIndReg.Locale = System.Globalization.CultureInfo.InvariantCulture;

                            daIndReg.Fill(tblIndReg);

                            grdRegistrations.DataSource = tblIndReg;
                        }
                    }
                }

                grdRegistrations.AutoResizeColumns();

                //subReOrderCols();

            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmProcessIndReg.subFillGrid", ex);
            }
        }

        private void subReOrderCols()
        {
            grdRegistrations.Columns["colName"].DisplayIndex = 0;
            grdRegistrations.Columns["colRegistrationDate"].DisplayIndex = 1;
            grdRegistrations.Columns["colBlockCode"].DisplayIndex = 2;
            grdRegistrations.Columns["colDetails"].DisplayIndex = 3;
        }

        private void frmProcessIndReg_Load(object sender, EventArgs e)
        {
            radUnprocessedCampers.Checked = true;

            subFillGrid();
        }

        private void grdRegistrations_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            long lngRegWebID = 0;

            try
            {
                //if col idx is < 0 then there is no active column clicked (probably a row click)
                if (e.ColumnIndex >= 0)
                {
                    if (grdRegistrations.Columns[e.ColumnIndex].Name == "colDetails")
                    {
                        if (e.RowIndex >= 0)
                        {
                            lngRegWebID = Convert.ToInt32(grdRegistrations.Rows[e.RowIndex].Cells["colRegWebID"].Value.ToString());

                            using (frmIndRegDetails objIndRegDetails = new frmIndRegDetails(lngRegWebID))
                            {
                                objIndRegDetails.ShowDialog();
                                subFillGrid();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmProcessIndReg.grdRegistrations_CellClick", ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();// clsNav.subCloseProcessIndReg();
        }

        private void radUnprocessedCampers_CheckedChanged(object sender, EventArgs e)
        {
            subFillGrid();

            btnClearProcessedReg.Visible = radAllCampers.Checked;
        }

        private void btnClearProcessedReg_Click(object sender, EventArgs e)
        {
            string strMsg = "";

            strMsg = "This will clear registrations that have already been processed from the queue.\n\nThe process cannot be reversed. Are you sure you wish to continue?";

            if (MessageBox.Show(strMsg, "CampTrak", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string strSQL = "";

                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    strSQL = "DELETE tblWebIndRegBlockChoices.* " +
                            "FROM tblWebIndRegBlockChoices " +
                            "WHERE tblWebIndRegBlockChoices.lngRegistrationWebID In " +
                                "(SELECT tblWebIndRegistrations.lngRegistrationWebID " +
                                "FROM tblWebIndRegistrations " +
                                "WHERE tblWebIndRegistrations.blnProcessed=True)";

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        try { cmdDB.ExecuteNonQuery(); }
                        catch { }

                        strSQL = "DELETE tblWebIndRegistrations.* " +
                                "FROM tblWebIndRegistrations " +
                                "WHERE tblWebIndRegistrations.blnProcessed=True";

                        cmdDB.CommandText = strSQL;
                        cmdDB.Parameters.Clear();

                        try { cmdDB.ExecuteNonQuery(); }
                        catch { }
                    }

                    conDB.Close();
                }

                subFillGrid();
            }
        }
    }
}