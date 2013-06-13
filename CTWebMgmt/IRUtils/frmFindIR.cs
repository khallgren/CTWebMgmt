using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;


namespace CTWebMgmt
{
    public partial class frmFindIR : Form
    {
        OleDbDataAdapter daRecords = new OleDbDataAdapter();
        BindingSource srcRecords = new BindingSource();

        public clsIR irToSearch;

        public bool blnAddNew = false;
        private bool blnINITLOADRESULTS = true;

        public frmFindIR(clsIR _irToSearch)
        {
            InitializeComponent();
            irToSearch = _irToSearch;
        }

        public frmFindIR(clsIR _irToSearch, string _strTitle)
        {
            InitializeComponent();
            irToSearch = _irToSearch;
            this.Text = _strTitle;
        }

        public frmFindIR(clsIR _irToSearch, string _strTitle, bool _blnInitLoadResults)
        {
            InitializeComponent();
            irToSearch = _irToSearch;
            this.Text = _strTitle;
            blnINITLOADRESULTS = _blnInitLoadResults;
        }

        public frmFindIR(clsIR _irToSearch, string _strTitle, bool _blnInitLoadResults, string _strFullText)
        {
            InitializeComponent();
            irToSearch = _irToSearch;
            this.Text = _strTitle;
            blnINITLOADRESULTS = _blnInitLoadResults;
            txtFullTextFilter.Text = _strFullText;
        }

        private void frmFindIR_Load(object sender, EventArgs e)
        {
            txtCompany.Text = irToSearch.strCompany.Trim();
            txtFName.Text = irToSearch.strFName.Trim();
            txtLName.Text = irToSearch.strLName.Trim();
            txtRecordID.Text = irToSearch.lngRecordID.ToString();

            //refresh list
            grdIRs.DataSource = srcRecords;
            if (blnINITLOADRESULTS) subRefresh();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            //set id
            try
            {
                if (grdIRs.SelectedRows.Count > 0)
                {
                    irToSearch.lngRecordID = (long)(int)grdIRs.SelectedRows[0].Cells["colRecordID"].Value;

                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                    MessageBox.Show("Please select a record or click 'Cancel'.");
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmFindIR.btnContinue", ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            irToSearch.lngRecordID = -1;
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            //add new camper ir
            using (frmAddIR objAddIR = new frmAddIR(irToSearch))
            {
                if (objAddIR.ShowDialog() == DialogResult.OK)
                {
                    irToSearch.lngRecordID = objAddIR.lngRecordID;
                    blnAddNew = true;
                }
                else
                    irToSearch.lngRecordID = 0;
            }

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private string fcnGetSQL()
        {
            string strSQL = "";
            string strWhere = "";

            long lngRecordID = 0;

            try { lngRecordID = Convert.ToInt32(txtRecordID.Text); }
            catch { lngRecordID = 0; }

            if (lngRecordID > 0)
            {
                strWhere = "WHERE lngRecordID=" + lngRecordID.ToString() + " ";

                txtFullTextFilter.Text = "";
                txtFName.Text = "";
                txtLName.Text = "";
                txtCompany.Text = "";
            }
            else if (txtFullTextFilter.Text != "")
            {
                txtFName.Text = "";
                txtLName.Text = "";
                txtCompany.Text = "";
                txtRecordID.Text = "";

                strWhere = "WHERE strFirstName LIKE '%" + txtFullTextFilter.Text.Replace("'", "''").Trim() + "%' OR " +
                                "strLastCoName LIKE '%" + txtFullTextFilter.Text.Replace("'", "''").Trim() + "%' OR " +
                                "strCompanyName LIKE '%" + txtFullTextFilter.Text.Replace("'", "''").Trim() + "%' ";
            }
            else
            {
                txtRecordID.Text = "";
                txtFullTextFilter.Text = "";

                if (txtFName.Text != "")
                {
                    if (strWhere == "")
                        strWhere = "WHERE strFirstName LIKE '" + txtFName.Text.Replace("'", "''").Trim() + "%' ";
                    else
                        strWhere += "AND strFirstName LIKE '" + txtFName.Text.Replace("'", "''").Trim() + "%' ";
                }

                if (txtLName.Text != "")
                {
                    if (strWhere == "")
                        strWhere = "WHERE strLastCoName LIKE '" + txtLName.Text.Replace("'", "''").Trim() + "%' ";
                    else
                        strWhere += "AND strLastCoName LIKE '" + txtLName.Text.Replace("'", "''").Trim() + "%' ";
                }

                if (txtCompany.Text != "")
                {
                    if (strWhere == "")
                        strWhere = "WHERE strCompanyName LIKE '" + txtCompany.Text.Replace("'", "''").Trim() + "%' ";
                    else
                        strWhere += "AND strCompanyName LIKE '" + txtCompany.Text.Replace("'", "''").Trim() + "%' ";
                }
            }

            //matches
            strSQL = "SELECT tblRecords.lngRecordID, " +
                        "tblRecords.strCompanyName AS strCompany, tblRecords.strFirstName AS strFName, tblRecords.strLastCoName AS strLName, " + clsSQLUtil.fcnIIf("tblRecords.blnUseMORAddress", "0", "[tblRecords].[strCity]", "tblMOR.strMORCity") + " AS strCity, " + clsSQLUtil.fcnIIf("tblRecords.blnUseMORAddress", "0", "[tlkpStates].[strState]", "tlkpStates_MOR.strState") + " AS strState " +
                    "FROM ((tblRecords " +
                        "LEFT JOIN tlkpStates ON tblRecords.lngStateID = tlkpStates.lngStateID) " +
                        "LEFT JOIN tblMOR ON tblRecords.lngPrimaryMORID = tblMOR.lngMORID) " +
                        "LEFT JOIN tlkpStates AS tlkpStates_MOR ON tblMOR.lngMORStateID = tlkpStates_MOR.lngStateID " +
                    strWhere +
                    "ORDER BY tblRecords.strCompanyName, tblRecords.strLastCoName, tblRecords.strFirstName, [tlkpStates].[strState], [tblRecords].[strCity];";

            return strSQL;
        }

        private void subRefresh()
        {
            OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn);
            OleDbDataAdapter daIrs;

            string strSQL;

            conDB.Open();

            strSQL = "";

            try
            {
                daIrs = new OleDbDataAdapter();

                strSQL = fcnGetSQL();

                // Create a new data adapter based on the specified query.
                daRecords = new OleDbDataAdapter(strSQL, clsAppSettings.GetAppSettings().strCTConn);
                // Populate a new data table and bind it to the BindingSource.
                DataTable tblRecords = new DataTable();

                daRecords.Fill(tblRecords);
                srcRecords.DataSource = tblRecords;

                grdIRs.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmFindIR.subRefresh", ex);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            subRefresh();
        }

        private void txtFullTextFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                subRefresh();
        }
    }
}