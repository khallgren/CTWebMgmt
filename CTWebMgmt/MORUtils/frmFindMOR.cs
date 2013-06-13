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
    public partial class frmFindMOR : Form
    {
        public long lngMORID = 0;

        public frmFindMOR()
        {
            InitializeComponent();
            subFillCbos();
        }

        public frmFindMOR(string _strMORName, long _lngMORTypeID)
        {
            InitializeComponent();

            subFillCbos(_lngMORTypeID);
            txtMORName.Text = _strMORName;
        }

        private void subFillCbos()
        {
            subFillCbos(0);
        }

        private void subFillCbos(long _lngMORTypeID)
        {
            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT lngMORTypeID, " +
                            "strMORType " +
                        "FROM tlkpMORTypes " +
                        "ORDER BY strMORType;";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drMORTypes = cmdDB.ExecuteReader())
                    {
                        while (drMORTypes.Read())
                        {
                            clsCboItem cboNew = new clsCboItem(Convert.ToInt32(drMORTypes["lngMORTypeID"]), Convert.ToString(drMORTypes["strMORType"]));

                            cboMORType.Items.Add(cboNew);

                            if (Convert.ToInt32(drMORTypes["lngMORTypeID"]) == _lngMORTypeID)
                                cboMORType.SelectedItem = cboNew;
                        }

                        drMORTypes.Close();
                    }
                }

                conDB.Close();
            }
        }

        private void subRefreshGrid()
        {
            try
            {
                string strSQL = "";

                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    strSQL = "SELECT tblMOR.lngMORID, " +
                                "tblMOR.strMORName, [tblMOR].[strMORCity] & \", \" & [tlkpStates].[strState] AS strCityState " +
                            "FROM tblMOR " +
                                "LEFT JOIN tlkpStates ON tblMOR.lngMORStateID = tlkpStates.lngStateID " +
                            "WHERE tblMOR.strMORName Like @strMORName AND " +
                                "tblMOR.lngMORTypeID=" + ((clsCboItem)cboMORType.SelectedItem).ID.ToString() + " " +
                            "ORDER BY tblMOR.strMORName, [tblMOR].[strMORCity] & \", \" & [tlkpStates].[strState];";

                    if (clsAppSettings.GetAppSettings().blnDebugMode)
                        clsErr.subWriteDebugLog("Executing SQL: " + strSQL + "|Parameter Val: " + txtMORName.Text);

                    using (OleDbCommand cmdMOR = new OleDbCommand(strSQL, conDB))
                    {
                        cmdMOR.Parameters.Add(new OleDbParameter("@strMORName", txtMORName.Text));

                        using (OleDbDataAdapter daMOR = new OleDbDataAdapter(cmdMOR))
                        {
                            // Populate a new data table and bind it to the BindingSource.
                            DataTable tblMOR = new DataTable();

                            daMOR.Fill(tblMOR);

                            grdMOR.DataSource = tblMOR;
                        }
                    }

                    conDB.Close();
                }

                grdMOR.AutoResizeColumns();
            }
            catch (Exception ex) { if (clsAppSettings.GetAppSettings().blnDebugMode)clsErr.subWriteDebugLog("Error subRefreshGrid: " + ex.Message); }
        }

        private void frmFindMOR_Load(object sender, EventArgs e)
        {
            subRefreshGrid();
        }

        private void grdMOR_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != grdMOR.Columns["colJumpTo"].Index) return;

            try { lngMORID = Convert.ToInt32(grdMOR[e.ColumnIndex, e.RowIndex].Value); }
            catch { lngMORID = 0; }

            DialogResult = DialogResult.OK;

            Close();

            return;
        }

        private void btnAddNewMOR_Click(object sender, EventArgs e)
        {
            //set mor id to 0, result to ok (flags parent to add new mor)
            lngMORID = 0;

            DialogResult = DialogResult.OK;

            Close();

            return;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            subRefreshGrid();
        }
    }
}
