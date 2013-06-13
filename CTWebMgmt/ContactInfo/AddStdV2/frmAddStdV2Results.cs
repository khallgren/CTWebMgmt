using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace CTWebMgmt.ContactInfo.AddStdV2
{
    public partial class frmAddStdV2Results : Form
    {
        BindingSource srcCertRes = new BindingSource();
        OleDbDataAdapter daCertRes = new OleDbDataAdapter();

        string strListName = "";

        public frmAddStdV2Results(string _strListName)
        {
            InitializeComponent();
            strListName = _strListName;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            AddressStandardization.clsCertRes.subProcessCertRes(strListName);

            Close();
        }

        public void subLoadGrid()
        {
            try
            {
                string strSQL = "SELECT tblRecords.lngRecordID, " +
                                    "IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])<>\"\" And IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName])<>\"\",\" - \",\"\") & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & \" \" & IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) AS strName, tblRecordCert.strStatus " +
                                "FROM tblRecords " +
                                    "INNER JOIN tblRecordCert ON tblRecords.lngRecordID=tblRecordCert.lngRecordID " +
                                "WHERE tblRecordCert.strListName='" + strListName + "'";

                // Create a new data adapter based on the specified query.
                daCertRes = new OleDbDataAdapter(strSQL, clsAppSettings.GetAppSettings().strCTConn);
                // Populate a new data table and bind it to the BindingSource.
                DataTable tblCertRes = new DataTable();

                daCertRes.Fill(tblCertRes);
                srcCertRes.DataSource = tblCertRes;

                grdCertRes.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmAddStd_4.subLoadGrid", ex);
            }
        }

        private void frmAddStdV2Results_Load(object sender, EventArgs e)
        {
            grdCertRes.DataSource = srcCertRes;

            subLoadGrid();

            MessageBox.Show("Addresses have been sent to Melissa Data for processing.\n\nAddresses in CampTrak will not be updated with the results until you click the 'Finish' button on this screen!");
        }
    }
}
