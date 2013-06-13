using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;


namespace CTWebMgmt.Admin
{
    public partial class frmGoogleCalULOptions : Form
    {
        private long lngProgramType = 0;
        //1=Block
        //2=CC
        //3=GG

        public frmGoogleCalULOptions(long _lngProgramType)
        {
            InitializeComponent();

            lngProgramType = _lngProgramType;

            subPopLsts();

            switch (lngProgramType)
            {
                case 1:
                    subLoadPrefsBlock();
                    break;

                case 2:
                    subLoadPrefsCC();
                    break;

                case 3:
                    subLoadPrefsGG();
                    break;
            }
        }

        private void subLoadPrefsGG()
        {
            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT blnFilterByProgramGG, blnFilterByStartDateGG, blnFilterByStatusGG, " +
                            "dteStartDateGG, dteEndDateGG " +
                        "FROM tblGoogleCalOptions";

                //set filter options (y/n) from options table
                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drPref = cmdDB.ExecuteReader())
                    {
                        if (drPref.Read())
                        {
                            try
                            {
                                if (Convert.ToBoolean(drPref["blnFilterByProgramGG"]))
                                    radProgramTypeFilter.Checked = true;
                                else
                                    radProgramTypeAll.Checked = true;
                            }
                            catch { radProgramTypeAll.Checked = true; }

                            try
                            {
                                if (Convert.ToBoolean(drPref["blnFilterByStartDateGG"]))
                                {
                                    radStartDateFilter.Checked = true;

                                    lblStart.Visible = true;
                                    lblEnd.Visible = true;

                                    txtStart.Visible = true;
                                    txtEnd.Visible = true;

                                    txtStart.Text = Convert.ToDateTime(drPref["dteStartDateGG"]).ToString("MM/dd/yyyy");
                                    txtEnd.Text = Convert.ToDateTime(drPref["dteEndDateGG"]).ToString("MM/dd/yyyy");
                                }
                                else
                                    radStartDateAll.Checked = true;
                            }
                            catch { radStartDateAll.Checked = true; }

                            try
                            {
                                if (Convert.ToBoolean(drPref["blnFilterByStatusGG"]))
                                    radStatusFilter.Checked = true;
                                else
                                    radStatusAll.Checked = true;
                            }
                            catch { radStatusAll.Checked = true; }
                        }

                        drPref.Close();
                    }

                    //auto select saved program filter options
                    strSQL = "SELECT lngProgramTypeID " +
                            "FROM tblGoogleCalFilteredPrograms";

                    cmdDB.Parameters.Clear();
                    cmdDB.CommandText = strSQL;

                    using (OleDbDataReader drPrograms = cmdDB.ExecuteReader())
                    {
                        while (drPrograms.Read())
                        {
                            long lngIDToSelect = 0;

                            try { lngIDToSelect = Convert.ToInt32(drPrograms["lngProgramTypeID"]); }
                            catch { lngIDToSelect = 0; }

                            for (int intI = 0; intI <= lstProgramTypes.Items.Count; intI++)
                            {
                                try
                                {
                                    clsCboItem cboItem = (clsCboItem)lstProgramTypes.Items[intI];

                                    if (cboItem.ID == lngIDToSelect)
                                        lstProgramTypes.SetSelected(intI, true);
                                }
                                catch { }
                            }
                        }

                        drPrograms.Close();
                    }

                    //auto select saved status filter options
                    strSQL = "SELECT lngStatusID " +
                            "FROM tblGoogleCalFilteredStatus";

                    cmdDB.Parameters.Clear();
                    cmdDB.CommandText = strSQL;

                    using (OleDbDataReader drStatus = cmdDB.ExecuteReader())
                    {
                        while (drStatus.Read())
                        {
                            long lngIDToSelect = 0;

                            try { lngIDToSelect = Convert.ToInt32(drStatus["lngStatusID"]); }
                            catch { lngIDToSelect = 0; }

                            for (int intI = 0; intI <= lstStatus.Items.Count; intI++)
                            {
                                try
                                {
                                    clsCboItem cboItem = (clsCboItem)lstStatus.Items[intI];

                                    if (cboItem.ID == lngIDToSelect)
                                        lstStatus.SetSelected(intI, true);
                                }
                                catch { }
                            }
                        }

                        drStatus.Close();
                    }
                }

                conDB.Close();
            }
        }

        private void subLoadPrefsCC()
        {
            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT blnFilterByProgramCC, blnFilterByStartDateCC, blnFilterByStatusCC, " +
                            "dteStartDateCC, dteEndDateCC " +
                "FROM tblGoogleCalOptions";

                //set filter options (y/n) from options table
                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drPref = cmdDB.ExecuteReader())
                    {
                        if (drPref.Read())
                        {
                            try
                            {
                                if (Convert.ToBoolean(drPref["blnFilterByProgramCC"]))
                                    radProgramTypeFilter.Checked = true;
                                else
                                    radProgramTypeAll.Checked = true;
                            }
                            catch { radProgramTypeAll.Checked = true; }

                            try
                            {
                                if (Convert.ToBoolean(drPref["blnFilterByStartDateCC"]))
                                {
                                    radStartDateFilter.Checked = true;

                                    lblStart.Visible = true;
                                    lblEnd.Visible = true;

                                    txtStart.Visible = true;
                                    txtEnd.Visible = true;

                                    txtStart.Text = Convert.ToDateTime(drPref["dteStartDateCC"]).ToString("MM/dd/yyyy");
                                    txtEnd.Text = Convert.ToDateTime(drPref["dteEndDateCC"]).ToString("MM/dd/yyyy");
                                }
                                else
                                    radStartDateAll.Checked = true;
                            }
                            catch { radStartDateAll.Checked = true; }

                            try
                            {
                                if (Convert.ToBoolean(drPref["blnFilterByStatusCC"]))
                                    radStatusFilter.Checked = true;
                                else
                                    radStatusAll.Checked = true;
                            }
                            catch { radStatusAll.Checked = true; }
                        }

                        drPref.Close();
                    }

                    //auto select saved program filter options
                    strSQL = "SELECT lngProgramTypeID " +
                            "FROM tblGoogleCalFilteredPrograms";

                    cmdDB.Parameters.Clear();
                    cmdDB.CommandText = strSQL;

                    using (OleDbDataReader drPrograms = cmdDB.ExecuteReader())
                    {
                        while (drPrograms.Read())
                        {
                            long lngIDToSelect = 0;

                            try { lngIDToSelect = Convert.ToInt32(drPrograms["lngProgramTypeID"]); }
                            catch { lngIDToSelect = 0; }

                            for (int intI = 0; intI <= lstProgramTypes.Items.Count; intI++)
                            {
                                try
                                {
                                    clsCboItem cboItem = (clsCboItem)lstProgramTypes.Items[intI];

                                    if (cboItem.ID == lngIDToSelect)
                                        lstProgramTypes.SetSelected(intI, true);
                                }
                                catch { }
                            }
                        }

                        drPrograms.Close();
                    }

                    //auto select saved status filter options
                    strSQL = "SELECT lngStatusID " +
                            "FROM tblGoogleCalFilteredStatus";

                    cmdDB.Parameters.Clear();
                    cmdDB.CommandText = strSQL;

                    using (OleDbDataReader drStatus = cmdDB.ExecuteReader())
                    {
                        while (drStatus.Read())
                        {
                            long lngIDToSelect = 0;

                            try { lngIDToSelect = Convert.ToInt32(drStatus["lngStatusID"]); }
                            catch { lngIDToSelect = 0; }

                            for (int intI = 0; intI <= lstStatus.Items.Count; intI++)
                            {
                                try
                                {
                                    clsCboItem cboItem = (clsCboItem)lstStatus.Items[intI];

                                    if (cboItem.ID == lngIDToSelect)
                                        lstStatus.SetSelected(intI, true);
                                }
                                catch { }
                            }
                        }

                        drStatus.Close();
                    }
                }

                conDB.Close();
            }
        }

        private void subLoadPrefsBlock()
        {
            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT blnFilterByProgramBlock, blnFilterByStartDateBlock, " +
                            "dteStartDateBlock, dteEndDateBlock " +
                        "FROM tblGoogleCalOptions";

                //set filter options (y/n) from options table
                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drPref = cmdDB.ExecuteReader())
                    {
                        if (drPref.Read())
                        {
                            try
                            {
                                if (Convert.ToBoolean(drPref["blnFilterByProgramBlock"]))
                                    radProgramTypeFilter.Checked = true;
                                else
                                    radProgramTypeAll.Checked = true;
                            }
                            catch { radProgramTypeAll.Checked = true; }

                            try
                            {
                                if (Convert.ToBoolean(drPref["blnFilterByStartDateBlock"]))
                                {
                                    radStartDateFilter.Checked = true;

                                    lblStart.Visible = true;
                                    lblEnd.Visible = true;

                                    txtStart.Visible = true;
                                    txtEnd.Visible = true;

                                    txtStart.Text = Convert.ToDateTime(drPref["dteStartDateBlock"]).ToString("MM/dd/yyyy");
                                    txtEnd.Text = Convert.ToDateTime(drPref["dteEndDateBlock"]).ToString("MM/dd/yyyy");
                                }
                                else
                                    radStartDateAll.Checked = true;
                            }
                            catch { radStartDateAll.Checked = true; }
                        }

                        drPref.Close();
                    }

                    //auto select saved program filter options
                    strSQL = "SELECT lngProgramTypeID " +
                            "FROM tblGoogleCalFilteredPrograms";

                    cmdDB.Parameters.Clear();
                    cmdDB.CommandText = strSQL;

                    using (OleDbDataReader drPrograms = cmdDB.ExecuteReader())
                    {
                        while (drPrograms.Read())
                        {
                            long lngIDToSelect = 0;

                            try { lngIDToSelect = Convert.ToInt32(drPrograms["lngProgramTypeID"]); }
                            catch { lngIDToSelect = 0; }

                            for (int intI = 0; intI <= lstProgramTypes.Items.Count; intI++)
                            {
                                try
                                {
                                    clsCboItem cboItem = (clsCboItem)lstProgramTypes.Items[intI];

                                    if (cboItem.ID == lngIDToSelect)
                                        lstProgramTypes.SetSelected(intI, true);
                                }
                                catch { }
                            }
                        }

                        drPrograms.Close();
                    }                    
                }

                conDB.Close();
            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            //validate
            DateTime dteVal;

            try { dteVal = Convert.ToDateTime(txtStart.Text); }
            catch
            {
                MessageBox.Show("Please enter a valid date criteria.");
                txtStart.Focus();
                return;
            }

            try { dteVal = Convert.ToDateTime(txtEnd.Text); }
            catch
            {
                MessageBox.Show("Please enter a valid date criteria.");
                txtEnd.Focus();
                return;
            }

            //update filter data
            switch (lngProgramType)
            {
                case 1:
                    subSavePrefsBlock();
                    break;

                case 2:
                    subSavePrefsCC();
                    break;

                case 3:
                    subSavePrefsGG();
                    break;
            }

            //close form
            this.Close();
        }

        private void subSavePrefsBlock()
        {
            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "UPDATE tblGoogleCalOptions " +
                        "SET blnFilterByProgramBlock=@blnFilterByProgramBlock, blnFilterByStartDateBlock=@blnFilterByStartDateBlock, " +
                            "dteStartDateBlock=@dteStartDateBlock, dteEndDateBlock=@dteEndDateBlock";

                //set filter options (y/n) from options table
                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    cmdDB.Parameters.Add(new OleDbParameter("@blnFilterByProgramBlock", radProgramTypeFilter.Checked));
                    cmdDB.Parameters.Add(new OleDbParameter("@blnFilterByStartDateBlock", radStartDateFilter.Checked));
                    cmdDB.Parameters.Add(new OleDbParameter("@dteStartDateBlock", Convert.ToDateTime(txtStart.Text)));
                    cmdDB.Parameters.Add(new OleDbParameter("@dteEndDateBlock", Convert.ToDateTime(txtEnd.Text)));

                    cmdDB.ExecuteNonQuery();

                    //clear existing program filters for current program
                    strSQL = "DELETE tblGoogleCalFilteredPrograms.* " +
                            "FROM tblGoogleCalFilteredPrograms " +
                            "WHERE tblGoogleCalFilteredPrograms.lngProgramTypeID In " +
                                "(SELECT tblGoogleCalFilteredPrograms.lngProgramTypeID " +
                                "FROM tblGoogleCalFilteredPrograms " +
                                    "INNER JOIN tlkpCampName ON tblGoogleCalFilteredPrograms.lngProgramTypeID=tlkpCampName.lngCampID " +
                                "WHERE tlkpCampName.lngProgramTypeID=" + lngProgramType.ToString() + ")";

                    cmdDB.Parameters.Clear();
                    cmdDB.CommandText = strSQL;

                    cmdDB.ExecuteNonQuery();

                    //loop through program list and add selected items to db
                    for (int intI = 0; intI <= lstProgramTypes.SelectedItems.Count; intI++)
                    {
                        try
                        {
                            clsCboItem cboItem = (clsCboItem)lstProgramTypes.SelectedItems[intI];

                            strSQL = "INSERT INTO tblGoogleCalFilteredPrograms " +
                                    "(lngProgramTypeID) " +
                                    "VALUES " +
                                    "(" + cboItem.ID.ToString() + ")";

                            cmdDB.Parameters.Clear();
                            cmdDB.CommandText = strSQL;

                            cmdDB.ExecuteNonQuery();
                        }
                        catch { }
                    }
                }

                conDB.Close();
            }
        }

        private void subSavePrefsCC()
        {
            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "UPDATE tblGoogleCalOptions " +
                        "SET blnFilterByProgramCC=@blnFilterByProgramCC, blnFilterByStartDateCC=@blnFilterByStartDateCC, blnFilterByStatusCC=@blnFilterByStatusCC, " +
                            "dteStartDateCC=@dteStartDateCC, dteEndDateCC=@dteEndDateCC";

                //set filter options (y/n) from options table
                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    cmdDB.Parameters.Add(new OleDbParameter("@blnFilterByProgramCC", radProgramTypeFilter.Checked));
                    cmdDB.Parameters.Add(new OleDbParameter("@blnFilterByStartDateCC", radStartDateFilter.Checked));
                    cmdDB.Parameters.Add(new OleDbParameter("@blnFilterByStatusCC", radStatusFilter.Checked));
                    cmdDB.Parameters.Add(new OleDbParameter("@dteStartDateCC", Convert.ToDateTime(txtStart.Text)));
                    cmdDB.Parameters.Add(new OleDbParameter("@dteEndDateCC", Convert.ToDateTime(txtEnd.Text)));

                    cmdDB.ExecuteNonQuery();

                    //clear existing program filters for current program
                    strSQL = "DELETE tblGoogleCalFilteredPrograms.* " +
                            "FROM tblGoogleCalFilteredPrograms " +
                            "WHERE tblGoogleCalFilteredPrograms.lngProgramTypeID In " +
                                "(SELECT tblGoogleCalFilteredPrograms.lngProgramTypeID " +
                                "FROM tblGoogleCalFilteredPrograms " +
                                    "INNER JOIN tlkpCampName ON tblGoogleCalFilteredPrograms.lngProgramTypeID=tlkpCampName.lngCampID " +
                                "WHERE tlkpCampName.lngProgramTypeID=" + lngProgramType.ToString() + ")";

                    cmdDB.Parameters.Clear();
                    cmdDB.CommandText = strSQL;

                    cmdDB.ExecuteNonQuery();

                    //loop through program list and add selected items to db
                    for (int intI = 0; intI <= lstProgramTypes.SelectedItems.Count; intI++)
                    {
                        try
                        {
                            clsCboItem cboItem = (clsCboItem)lstProgramTypes.SelectedItems[intI];

                            strSQL = "INSERT INTO tblGoogleCalFilteredPrograms " +
                                    "(lngProgramTypeID) " +
                                    "VALUES " +
                                    "(" + cboItem.ID.ToString() + ")";

                            cmdDB.Parameters.Clear();
                            cmdDB.CommandText = strSQL;

                            cmdDB.ExecuteNonQuery();
                        }
                        catch { }
                    }

                    //clear existing status filters
                    strSQL = "DELETE tblGoogleCalFilteredStatus.* " +
                            "FROM tblGoogleCalFilteredStatus ";

                    cmdDB.Parameters.Clear();
                    cmdDB.CommandText = strSQL;

                    cmdDB.ExecuteNonQuery();

                    //loop through status list and add selected items to db
                    for (int intI = 0; intI <= lstStatus.SelectedItems.Count; intI++)
                    {
                        try
                        {
                            clsCboItem cboItem = (clsCboItem)lstStatus.SelectedItems[intI];

                            strSQL = "INSERT INTO tblGoogleCalFilteredStatus " +
                                    "(lngStatusID) " +
                                    "VALUES " +
                                    "(" + cboItem.ID.ToString() + ")";

                            cmdDB.Parameters.Clear();
                            cmdDB.CommandText = strSQL;

                            cmdDB.ExecuteNonQuery();
                        }
                        catch { }
                    }
                }

                conDB.Close();
            }
        }

        private void subSavePrefsGG()
        {
            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "UPDATE tblGoogleCalOptions " +
                        "SET blnFilterByProgramGG=@blnFilterByProgramGG, blnFilterByStartDateGG=@blnFilterByStartDateGG, blnFilterByStatusGG=@blnFilterByStatusGG, " +
                            "dteStartDateGG=@dteStartDateGG, dteEndDateGG=@dteEndDateGG";

                //set filter options (y/n) from options table
                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    cmdDB.Parameters.Add(new OleDbParameter("@blnFilterByProgramGG", radProgramTypeFilter.Checked));
                    cmdDB.Parameters.Add(new OleDbParameter("@blnFilterByStartDateGG", radStartDateFilter.Checked));
                    cmdDB.Parameters.Add(new OleDbParameter("@blnFilterByStatusGG", radStatusFilter.Checked));
                    cmdDB.Parameters.Add(new OleDbParameter("@dteStartDateGG", Convert.ToDateTime(txtStart.Text)));
                    cmdDB.Parameters.Add(new OleDbParameter("@dteEndDateGG", Convert.ToDateTime(txtEnd.Text)));

                    cmdDB.ExecuteNonQuery();

                    //clear existing program filters for current program
                    strSQL = "DELETE tblGoogleCalFilteredPrograms.* " +
                            "FROM tblGoogleCalFilteredPrograms " +
                            "WHERE tblGoogleCalFilteredPrograms.lngProgramTypeID In " +
                                "(SELECT tblGoogleCalFilteredPrograms.lngProgramTypeID " +
                                "FROM tblGoogleCalFilteredPrograms " +
                                    "INNER JOIN tlkpCampName ON tblGoogleCalFilteredPrograms.lngProgramTypeID=tlkpCampName.lngCampID " +
                                "WHERE tlkpCampName.lngProgramTypeID=" + lngProgramType.ToString() + ")";

                    cmdDB.Parameters.Clear();
                    cmdDB.CommandText = strSQL;

                    cmdDB.ExecuteNonQuery();

                    //loop through program list and add selected items to db
                    for (int intI = 0; intI <= lstProgramTypes.SelectedItems.Count; intI++)
                    {
                        try
                        {
                            clsCboItem cboItem = (clsCboItem)lstProgramTypes.SelectedItems[intI];

                            strSQL = "INSERT INTO tblGoogleCalFilteredPrograms " +
                                    "(lngProgramTypeID) " +
                                    "VALUES " +
                                    "(" + cboItem.ID.ToString() + ")";

                            cmdDB.Parameters.Clear();
                            cmdDB.CommandText = strSQL;

                            cmdDB.ExecuteNonQuery();
                        }
                        catch { }
                    }

                    //clear existing status filters
                    strSQL = "DELETE tblGoogleCalFilteredStatus.* " +
                            "FROM tblGoogleCalFilteredStatus ";

                    cmdDB.Parameters.Clear();
                    cmdDB.CommandText = strSQL;

                    cmdDB.ExecuteNonQuery();

                    //loop through status list and add selected items to db
                    for (int intI = 0; intI <= lstStatus.SelectedItems.Count; intI++)
                    {
                        try
                        {
                            clsCboItem cboItem = (clsCboItem)lstStatus.SelectedItems[intI];

                            strSQL = "INSERT INTO tblGoogleCalFilteredStatus " +
                                    "(lngStatusID) " +
                                    "VALUES " +
                                    "(" + cboItem.ID.ToString() + ")";

                            cmdDB.Parameters.Clear();
                            cmdDB.CommandText = strSQL;

                            cmdDB.ExecuteNonQuery();
                        }
                        catch { }
                    }
                }

                conDB.Close();
            }
        }

        private void subVisibility(object sender, EventArgs e)
        {
            lstProgramTypes.Visible = false;
            lstStatus.Visible = false;
            lblStart.Visible = false;
            lblEnd.Visible = false;
            txtStart.Visible = false;
            txtEnd.Visible = false;

            if (radProgramTypeFilter.Checked) lstProgramTypes.Visible = true;

            if (radStartDateFilter.Checked)
            {
                lblStart.Visible = true;
                lblEnd.Visible = true;
                txtStart.Visible = true;
                txtEnd.Visible = true;
            }

            if (radStatusFilter.Checked) lstStatus.Visible = true;
        }

        private void subPopLsts()
        {
            //load program type, status options; set default vals for dates
            txtStart.Text = DateTime.Now.ToString("MM/dd/yyyy");
            txtEnd.Text = txtStart.Text;

            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT lngCampID, " +
                            "strCampName " +
                        "FROM tlkpCampName " +
                        "WHERE lngProgramTypeID=" + lngProgramType + " " +
                        "ORDER BY strCampName";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drPrograms = cmdDB.ExecuteReader())
                    {
                        while (drPrograms.Read())
                        {
                            clsCboItem lstItem;

                            long lngID = 0;
                            string strVal = "";

                            try { lngID = Convert.ToInt32(drPrograms["lngCampID"]); }
                            catch { lngID = 0; }

                            try { strVal = Convert.ToString(drPrograms["strCampName"]); }
                            catch { strVal = ""; }

                            lstItem = new clsCboItem(lngID, strVal);

                            lstProgramTypes.Items.Add(lstItem);
                        }

                        drPrograms.Close();
                    }

                    //only load/display status for gg or cc
                    if (lngProgramType > 1)
                    {
                        strSQL = "SELECT lngGroupStatusID, " +
                                    "strGroupStatus " +
                                "FROM tlkpGroupStatus " +
                                "ORDER BY strGroupStatus";

                        cmdDB.Parameters.Clear();
                        cmdDB.CommandText = strSQL;

                        using (OleDbDataReader drStatus = cmdDB.ExecuteReader())
                        {
                            while (drStatus.Read())
                            {
                                clsCboItem lstItem;

                                long lngID = 0;
                                string strVal = "";

                                try { lngID = Convert.ToInt32(drStatus["lngGroupStatusID"]); }
                                catch { lngID = 0; }

                                try { strVal = Convert.ToString(drStatus["strGroupStatus"]); }
                                catch { strVal = ""; }

                                lstItem = new clsCboItem(lngID, strVal);

                                lstStatus.Items.Add(lstItem);
                            }

                            drStatus.Close();
                        }
                    }
                    else
                        fraStatus.Visible = false;
                        
                }

                conDB.Close();
            }
        }
    }
}
