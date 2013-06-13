using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using CTWebMgmt.Properties;

namespace CTWebMgmt
{
    public partial class frmReferredBy : Form
    {
        private OleDbDataAdapter daReferredBy = new OleDbDataAdapter();
        private BindingSource srcReferredBy = new BindingSource();

        public frmReferredBy()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            clsNav.subCloseReferredBy();
        }

        private void subFillGrid()
        {
            try
            {
                string strSQL = "SELECT tblReferredBy.strReferredBy " +
                                "FROM tblReferredBy " +
                                "ORDER BY tblReferredBy.strReferredBy;";

                daReferredBy = new OleDbDataAdapter(strSQL, clsAppSettings.GetAppSettings().strCTConn);

                OleDbCommandBuilder cmdReferredBy = new OleDbCommandBuilder(daReferredBy);

                // Populate a new data table and bind it to the BindingSource.
                DataTable tblReferredBy = new DataTable();

                tblReferredBy.Locale = System.Globalization.CultureInfo.InvariantCulture;

                daReferredBy.Fill(tblReferredBy);

                srcReferredBy.DataSource = tblReferredBy;

                grdReferredBy.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmReferredBy.fcnFillSrc", ex);
            }
        }

        private void frmReferredBy_Load(object sender, EventArgs e)
        {
            grdReferredBy.DataSource = srcReferredBy;
            subFillGrid();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            subUpdate();
        }

        private void grdReferredBy_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            subUpdate();
        }

        private void grdReferredBy_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            subUpdate();
        }

        private void grdReferredBy_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            subUpdate();
        }

        private void subUpdate()
        {
            daReferredBy.Update((DataTable)srcReferredBy.DataSource);
        }

        private void frmReferredBy_FormClosed(object sender, FormClosedEventArgs e)
        {
            subUpdate();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            string strSQL;
            string strULRes = "";

            this.Cursor = Cursors.WaitCursor;

            //upload referred by options
            strSQL = "SELECT " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, " +
                        "tblReferredBy.strReferredBy " +
                    "FROM tblReferredBy " +
                    "ORDER BY tblReferredBy.strReferredBy;";

            strULRes = clsWebTalk.fcnUploadData(strSQL, "tblReferredBy", "strReferredBy", "", "spAppendReferredBy", true, "string", "referred by");

            MessageBox.Show("Upload Complete");

            this.Cursor = Cursors.Default;
        }
    }
}