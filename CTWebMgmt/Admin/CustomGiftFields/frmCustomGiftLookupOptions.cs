using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace CTWebMgmt.Admin.CustomGiftFields
{
    public partial class frmCustomGiftLookupOptions : Form
    {
        private OleDbDataAdapter daLookupOptions = new OleDbDataAdapter();
        private BindingSource srcLookupOptions = new BindingSource();
        private string strFieldName = "";

        public frmCustomGiftLookupOptions(string _strFieldName)
        {
            InitializeComponent();

            strFieldName = _strFieldName;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void subFillGrid()
        {
            try
            {
                string strSQL = "";

                strSQL = "SELECT tblCustomFieldsGiftLookupOptions.intSortOrder, " +
                            "tblCustomFieldsGiftLookupOptions.strLookupOption, tblCustomFieldsGiftLookupOptions.strFieldName " +
                        "FROM tblCustomFieldsGiftLookupOptions " +
                        "WHERE tblCustomFieldsGiftLookupOptions.strFieldName=@strFieldName " +
                        "ORDER BY tblCustomFieldsGiftLookupOptions.intSortOrder, " +
                            "tblCustomFieldsGiftLookupOptions.strLookupOption";

                daLookupOptions = new OleDbDataAdapter(strSQL, clsAppSettings.GetAppSettings().strCTConn);
                daLookupOptions.SelectCommand.Parameters.Add(new OleDbParameter("@strFieldName", OleDbType.VarChar, 255));
                daLookupOptions.SelectCommand.Parameters[0].Value = strFieldName;

                OleDbCommandBuilder cmdLookupOptions = new OleDbCommandBuilder(daLookupOptions);

                // Populate a new data table and bind it to the BindingSource.
                DataTable tblLookupOptions = new DataTable();

                tblLookupOptions.Locale = System.Globalization.CultureInfo.InvariantCulture;

                daLookupOptions.Fill(tblLookupOptions);

                srcLookupOptions.DataSource = tblLookupOptions;

                grdLookupOptions.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmReferredBy.fcnFillSrc", ex);
            }
        }

        private void frmCustomGiftLookupOptions_Load(object sender, EventArgs e)
        {
            grdLookupOptions.DataSource = srcLookupOptions;
            subFillGrid();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            subUpdate();
        }

        private void grdLookupOptions_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            subUpdate();
        }

        private void grdLookupOptions_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            subUpdate();
        }

        private void grdLookupOptions_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            subUpdate();
        }
        private void subUpdate()
        {
            try { daLookupOptions.Update((DataTable)srcLookupOptions.DataSource); }
            catch { MessageBox.Show("There was an error updating the custom field choice.\n\nThis is often because of blank entries and invalid indexing.\n\nPlease contact CampTrak Software for additional help."); }
        }

        private void frmCustomGiftLookupOptions_FormClosed(object sender, FormClosedEventArgs e)
        {
            subUpdate();
        }

        private void grdLookupOptions_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["colFieldName"].Value = strFieldName;
        }
    }
}