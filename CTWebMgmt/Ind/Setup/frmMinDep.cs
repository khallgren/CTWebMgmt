using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace CTWebMgmt.Ind.Setup
{
    public partial class frmMinDep : Form
    {
        private OleDbDataAdapter daBlocks = new OleDbDataAdapter();
        private BindingSource srcBlocks = new BindingSource();

        public frmMinDep()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmMinDep_Load(object sender, EventArgs e)
        {
            grdBlocks.DataSource = srcBlocks;
            subFillGrid();
        }

        private void subFillGrid()
        {
            try
            {
                string strSQL = "";

                strSQL = "SELECT tblBlock.lngBlockID, " +
                            "tblBlock.curCharge, tblBlock.curMinDep, " +
                            "tblBlock.strBlockCode " +
                        "FROM tblBlock";

                daBlocks = new OleDbDataAdapter(strSQL, clsAppSettings.GetAppSettings().strCTConn);

                OleDbCommandBuilder cmdBlocks = new OleDbCommandBuilder(daBlocks);

                // Populate a new data table and bind it to the BindingSource.
                DataTable tblBlocks = new DataTable();

                tblBlocks.Locale = System.Globalization.CultureInfo.InvariantCulture;

                daBlocks.Fill(tblBlocks);

                srcBlocks.DataSource = tblBlocks;

                grdBlocks.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmMinDep.subFillGrid", ex);
            }
        }

        private void subSave()
        {
            daBlocks.Update((DataTable)srcBlocks.DataSource);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            subSave();
        }

        private void grdBlocks_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            subSave();
        }

        private void grdBlocks_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            subSave();
        }

        private void grdBlocks_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            subSave();
        }

        private void frmMinDep_FormClosed(object sender, FormClosedEventArgs e)
        {
            subSave();
        }

        private void btnApplyMethod_Click(object sender, EventArgs e)
        {
            //update min dep, refresh grid
            string strSQL = "";

            //save any changes first
            subSave();

            decimal decMinDep = 0;

            try { decMinDep = Convert.ToDecimal(txtMinDep.Text); }
            catch { decMinDep = 0; }

            if (decMinDep <= 0)
            {
                MessageBox.Show("Please enter a value to apply minimum deposits.");
                txtMinDep.Focus();
                return;
            }

            if (radPercent.Checked)
                strSQL = "UPDATE tblBlock " +
                       "SET tblBlock.curMinDep = (" + decMinDep.ToString() + "/100) * [tblBlock].[curCharge]";
            else if (radDollars.Checked)
                strSQL = "UPDATE tblBlock " +
                       "SET tblBlock.curMinDep = " + decMinDep.ToString() + "";
            else
            {
                MessageBox.Show("Please select a method to apply minimum deposits.");
                radPercent.Focus();
                return;
            }

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    try { cmdDB.ExecuteNonQuery(); }
                    catch { }
                }

                conDB.Close();
            }

            subFillGrid();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (Ind.frmIndRegUL objIndRegUL = new frmIndRegUL())
            {
                objIndRegUL.ShowDialog();
            }

            Close();
        }
    }
}