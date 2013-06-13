using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace CTWebMgmt.GGCC
{
    public partial class frmProcessGGCCReg : Form
    {
        public frmProcessGGCCReg()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            clsNav.subCloseProcessGGCCReg();
        }

        private void frmProcessGGCCReg_Load(object sender, EventArgs e)
        {
            //     Bind the DataGridView to the BindingSource
            //     and load the data from the database.
            subFillSrc();
        }

        private void subFillSrc()
        {
            try
            {
                string strSQL = "SELECT tblWebGGCCRegistrations.lngGGCCRegistrationWebID, tblWebRecordsGGCCReg.strFirstName, tblWebRecordsGGCCReg.strLastCoName, tblWebRecordsGGCCReg.strCompanyName, tblGGCC.strGGCCName, tblWebGGCCRegistrations.dteDateRegistered, tblWebGGCCRegistrations.dteLastModified " +
                        "FROM (tblWebGGCCRegistrations " +
                            "INNER JOIN tblWebRecordsGGCCReg ON tblWebGGCCRegistrations.lngRecordWebID = tblWebRecordsGGCCReg.lngRecordWebID) " +
                            "INNER JOIN tblGGCC ON tblWebGGCCRegistrations.lngGGCCID = tblGGCC.lngGGCCID " +
                        "WHERE tblWebGGCCRegistrations.blnProcessed=False " +
                        "ORDER BY tblWebGGCCRegistrations.dteDateRegistered;";

                BindingSource srcEventReg = new BindingSource();

                grdGGCCReg.DataSource = srcEventReg;

                OleDbDataAdapter daGGCCReg = new OleDbDataAdapter(strSQL, clsAppSettings.GetAppSettings().strCTConn);

                OleDbCommandBuilder cmdGGCCReg = new OleDbCommandBuilder(daGGCCReg);

                // Populate a new data table and bind it to the BindingSource.
                DataTable tblGGCCReg = new DataTable();

                tblGGCCReg.Locale = System.Globalization.CultureInfo.InvariantCulture;

                daGGCCReg.Fill(tblGGCCReg);

                srcEventReg.DataSource = tblGGCCReg;
               
                grdGGCCReg.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

                grdGGCCReg.Columns["colDetails"].Width = 100;

                daGGCCReg.Dispose();                
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmProcessGGCCReg.fcnFillSrc", ex);
            }
        }

        private void btnDetails_Click(object sender, DataGridViewCellEventArgs e)
        {
            long lngGGCCRegID = 0;

            try
            {
                if (e.ColumnIndex >= 0)
                {
                    if (grdGGCCReg.Columns[e.ColumnIndex].Name == "colDetails" && e.RowIndex >= 0)
                    {
                        long.TryParse(grdGGCCReg.Rows[e.RowIndex].Cells["lngGGCCRegistrationWebID"].Value.ToString(), out lngGGCCRegID);

                        clsNav.subShowGGCCRegDetails(this.MdiParent, lngGGCCRegID);
                    }
                }
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmProcessGGCCReg.fcnFillSrc", ex);
            }
        }

        public void subRefreshQueue()
        {
            grdGGCCReg.Rows.Clear();
            subFillSrc();
        }
    }
}