using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace CTWebMgmt
{
    class clsErr
    {
        public static void subLogErr(string strFrom, Exception ex)
        {

            string strErrNotes = "";
            string strWrite;

            //strErrNotes = InputBox.ShowInputBox("An error has occurred and a log has been generated.\r\rPlease enter any notes describing the conditions that raised the error.\r\r(" + ex.Message + "|" + strFrom + ")");
            MessageBox.Show("An error has occurred and a log has been generated.\r\r(" + ex.Message + "|" + strFrom + ")");
            strWrite = ex.Message + " | " + strFrom + "|" + CTWebMgmt.lngUserID + " | " + strErrNotes;

            subWriteLog(strWrite);
        }

        public static void subWriteLog(string strLine)
        {
            string strLog = "";

            try
            {

                strLog = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "/CTErr.log";

                if (strLog.Substring(0, 6) == "file:\\")
                    strLog = strLog.Substring(6, (strLog.Length - 6));

                using (StreamWriter stwOutFile = new StreamWriter(strLog, true))
                {
                    stwOutFile.WriteLine(DateTime.Now.ToString() + ": " + strLine);

                    stwOutFile.Flush();

                    stwOutFile.Close();
                }
            }
            catch (Exception ex2)
            {
                MessageBox.Show("Error writing log to " + strLog + ": " + ex2.Message);
            }
        }

        public static void subWriteDebugLog(string _strErr)
        {
            try
            {
                string strFile = DateTime.Now.ToString("yyyyMMdd") + "_CTWebErrLog.log";
                string strPath = System.IO.Path.GetDirectoryName(Properties.Settings.Default.MainPath) + "\\Logs";

                if (!Directory.Exists(strPath)) Directory.CreateDirectory(strPath);

                string strFileName = strPath + "\\" + strFile;

                using (StreamWriter stwOutFile = new StreamWriter(strFileName, true))
                {
                    stwOutFile.WriteLine(DateTime.Now.ToString() + ": " + _strErr);

                    stwOutFile.Flush();

                    stwOutFile.Close();
                }
            }
            catch { }
        }
    }
}