using System;
using System.Collections.Generic;
using System.Text;

namespace CTWebMgmt
{
    class clsSQLUtil
    {
        public static string fcnIIf(string _strEvalFld, string _strEvalCriter, string _strCaseTrue, string _strCaseFalse)
        {
            string strRes = "";

            if (CTWebMgmt.blnUseSQLServer)
                strRes = "CASE " + _strEvalFld + " WHEN " + _strEvalCriter + " THEN " + _strCaseTrue + " ELSE " + _strCaseFalse + " END";
            else
                strRes = "IIf(" + _strEvalFld + "=" + _strEvalCriter + "," + _strCaseTrue + "," + _strCaseFalse + ")";

            return strRes;
        }
    }
}