using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace CTWebMgmt.ContactInfo.AddStdV2
{
    public partial class frmMORNCOAAlerts : Form
    {
        private BindingSource srcMORNCOAAlerts = new BindingSource();
        private OleDbDataAdapter daMORNCOAAlerts = new OleDbDataAdapter();

        public frmMORNCOAAlerts()
        {
            InitializeComponent();
        }

        private void frmMORNCOAAlerts_Load(object sender, System.EventArgs e)
        {
            // Bind the DataGridView to the BindingSource 
            // and load the data from the database.
            grdMORNCOAAlerts.DataSource = srcMORNCOAAlerts;

            string strSQL = "";

            strSQL = "SELECT tblMORNCOAAlerts.lngMORID, tblMORNCOAAlerts.strListName, tblMORNCOAAlerts.mmoAlertNotes, tblMORNCOAAlerts.blnResolved " +
                    "FROM tblMORNCOAAlerts " +
                    "WHERE blnResolved=False " +
                    "ORDER BY tblMORNCOAAlerts.strListName";

            subGetData(strSQL);
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            // Update the database with the user's changes.
            daMORNCOAAlerts.Update((DataTable)srcMORNCOAAlerts.DataSource);
        }

        private void subGetData(string _strSELECT)
        {
            try
            {
                // Specify a connection string. Replace the given value with a  
                // valid connection string for a Northwind SQL Server sample 
                // database accessible to your system.
                String strConn = clsAppSettings.GetAppSettings().strCTConn;

                // Create a new data adapter based on the specified query.
                daMORNCOAAlerts = new OleDbDataAdapter(_strSELECT, strConn);

                // Create a command builder to generate SQL update, insert, and 
                // delete commands based on _strSELECT. These are used to 
                // update the database.
                OleDbCommandBuilder cmdMORNCOAAlerts = new OleDbCommandBuilder(daMORNCOAAlerts);

                // Populate a new data table and bind it to the BindingSource.
                DataTable dtMORNCOAAlerts = new DataTable();
                dtMORNCOAAlerts.Locale = System.Globalization.CultureInfo.InvariantCulture;
                daMORNCOAAlerts.Fill(dtMORNCOAAlerts);
                srcMORNCOAAlerts.DataSource = dtMORNCOAAlerts;
            }
            catch (OleDbException)
            {
                MessageBox.Show("To run this example, replace the value of the " +
                    "strConn variable with a connection string that is " +
                    "valid for your system.");
            }
        }

        private void chkShowResolved_CheckedChanged(object sender, EventArgs e)
        {
            string strSQL = "";

            strSQL = "SELECT tblMORNCOAAlerts.lngMORID, tblMORNCOAAlerts.strListName, tblMORNCOAAlerts.mmoAlertNotes, tblMORNCOAAlerts.blnResolved " +
                    "FROM tblMORNCOAAlerts " +
                    (chkShowResolved.Checked ? "" : "WHERE blnResolved=False ") +
                    "ORDER BY tblMORNCOAAlerts.strListName";

            subGetData(strSQL);
        }
    }
}