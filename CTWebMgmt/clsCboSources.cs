using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;

namespace CTWebMgmt
{
    class clsCboSources
    {
        public static void subFillReferredByCbo(ref System.Windows.Forms.ComboBox _cboToFill, string _strReferredBy)
        {
            OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn);
            OleDbCommand cmdDB;
            OleDbDataReader drReferredBy;

            string strSQL;

            int intSelIdx = 0;
            int intI = 0;

            strSQL = "SELECT tblReferredBy.strReferredBy " +
                    "FROM tblReferredBy " +
                    "ORDER BY tblReferredBy.strReferredBy;";

            conDB.Open();

            cmdDB = new OleDbCommand(strSQL, conDB);

            drReferredBy = cmdDB.ExecuteReader();

            _cboToFill.Items.Clear();

            _cboToFill.Items.Add("");

            intI++;

            while (drReferredBy.Read())
            {
                _cboToFill.Items.Add(drReferredBy["strReferredBy"].ToString());

                if (drReferredBy["strReferredBy"].ToString()== _strReferredBy)
                    intSelIdx = intI;

                intI++;
            }

            _cboToFill.SelectedIndex = intSelIdx;

            drReferredBy.Close();

            conDB.Close();

            drReferredBy.Dispose();
            cmdDB.Dispose();
            conDB.Dispose();
        }

        public static void subFillTitleCbo(ref System.Windows.Forms.ComboBox _cboToFill)
        {
            clsCboItem cboNew = new clsCboItem("", "");

            _cboToFill.Items.Add(cboNew);

            cboNew = new clsCboItem("Mr.", "Mr.");
            _cboToFill.Items.Add(cboNew);

            cboNew = new clsCboItem("Mrs.", "Mrs.");
            _cboToFill.Items.Add(cboNew);

            cboNew = new clsCboItem("Dr.", "Dr.");
            _cboToFill.Items.Add(cboNew);

            cboNew = new clsCboItem("Miss", "Miss");
            _cboToFill.Items.Add(cboNew);

            cboNew = new clsCboItem("Ms.", "Ms.");
            _cboToFill.Items.Add(cboNew);
           
        }

        public static void subFillStateCbo(ref System.Windows.Forms.ComboBox _cboToFill)
        {
            subFillStateCbo(ref _cboToFill, 0);
        }

        public static void subFillStateCbo(ref System.Windows.Forms.ComboBox _cboToFill, long _lngStateID)
        {
            OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn);
            OleDbCommand cmdDB;
            OleDbDataReader drStates;

            string strSQL;

            int intSelIdx = 0;
            int intI = 0;

            strSQL = "SELECT lngStateID, " +
                        "strState " +
                    "FROM tlkpStates " +
                    "ORDER BY strState;";

            conDB.Open();

            cmdDB = new OleDbCommand(strSQL, conDB);

            drStates = cmdDB.ExecuteReader();

            _cboToFill.Items.Clear();

            subAddItem(ref _cboToFill, 0, "");
            intI++;

            while (drStates.Read())
            {                
                long lngStateID = long.Parse(drStates["lngStateID"].ToString());
             
                subAddItem(ref _cboToFill, lngStateID, drStates["strState"].ToString());

                if (lngStateID == _lngStateID)
                    intSelIdx = intI;

                intI++;
            }

            _cboToFill.SelectedIndex = intSelIdx;

            drStates.Close();

            conDB.Close();

            drStates.Dispose();
            cmdDB.Dispose();
            conDB.Dispose();
        }

        public static void subFillPmtTypeCbo(ref System.Windows.Forms.ComboBox _cboToFill, long _lngPmtTypeID)
        {
            int intSelIdx = 0;

            _cboToFill.Items.Clear();

            subAddItem(ref _cboToFill, 0, "");
            subAddItem(ref _cboToFill, 2, "Credit Card");
            subAddItem(ref _cboToFill, 11, "EFT");

            switch (_lngPmtTypeID)
            {
                case 2:
                    intSelIdx = 1;
                    break;

                case 11:
                    intSelIdx = 2;
                    break;

                default:
                    intSelIdx = 0;
                    break;
            }

            _cboToFill.SelectedIndex = intSelIdx;
        }

        public static void subFillPledgeFreqCbo(ref System.Windows.Forms.ComboBox _cboToFill, int _lngPledgeFreqID)
        {
            _cboToFill.Items.Clear();

            subAddItem(ref _cboToFill, 1, "Weekly");
            subAddItem(ref _cboToFill, 2, "Monthly");
            subAddItem(ref _cboToFill, 3, "Quarterly");
            subAddItem(ref _cboToFill, 4, "Semi-Annually");
            subAddItem(ref _cboToFill, 5, "Annually");
            subAddItem(ref _cboToFill, 6, "Bi-Monthly");

            _cboToFill.SelectedIndex = _lngPledgeFreqID - 1;
        }

        private static void subAddItem(ref System.Windows.Forms.ComboBox _cboAddTo, long _lngIDToAdd, string _strItem)
        {
            clsCboItem cboNew = new clsCboItem(_lngIDToAdd, _strItem);

            _cboAddTo.Items.Add(cboNew);
        }
    }
}
