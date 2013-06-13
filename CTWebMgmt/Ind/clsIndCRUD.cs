using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;

namespace CTWebMgmt.Ind
{
    class clsIndCRUD
    {
        public static long fcnGetIRRegCount()
        {
            //get current count of web registrations

            string strSQL;

            long lngRes = 0;

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT Count(lngRegistrationWebID) AS lngRegCount " +
                        "FROM tblWebIndRegistrations;";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    try { lngRes = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                    catch { lngRes = 0; }
                }

                conDB.Close();
            }

            return lngRes;
        }

        public static int fcnPending1stChoice(OleDbCommand _cmdDB, long _lngBlockID)
        {
            int intRes = 0;

            string strSQL = "";

            strSQL = "SELECT Count(tblWebIndRegistrations.lngRegistrationWebID) AS intPending1stChoice " +
                    "FROM tblWebIndRegistrations " +
                        "INNER JOIN tblWebIndRegBlockChoices ON tblWebIndRegistrations.lngRegistrationWebID = tblWebIndRegBlockChoices.lngRegistrationWebID " +
                    "WHERE tblWebIndRegistrations.blnProcessed=0 AND " +
                        "tblWebIndRegBlockChoices.lngBlockID=" + _lngBlockID.ToString() + " AND tblWebIndRegBlockChoices.lngChoice=1";

            _cmdDB.Parameters.Clear();
            _cmdDB.CommandText = strSQL;

            try { intRes = Convert.ToInt32(_cmdDB.ExecuteScalar()); }
            catch { intRes = 0; }

            return intRes;
        }
    }
}
