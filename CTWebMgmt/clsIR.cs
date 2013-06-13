using System;
using System.Collections.Generic;
using System.Text;

namespace CTWebMgmt
{
    public class clsIR
    {
        public bool blnGender { get; set; }
        public bool blnCamper { get; set; }
        public bool blnParent { get; set; }

        public int intGrade { get; set; }

        public long lngRecordWebID { get; set; }
        public long lngRecordID { get; set; }
        public long lngStateID { get; set; }
        public long lngBillStateID { get; set; }

        public DateTime dteBDate { get; set; }

        public string strMI { get; set; }
        public string strEmail { get; set; }
        public string strConfEmail { get; set; }
        public string strWorkPhone { get; set; }
        public string strHomePhone { get; set; }
        public string strCellPhone { get; set; }
        public string strZip { get; set; }
        public string strCity { get; set; }
        public string strAddress { get; set; }
        public string strCompany { get; set; }
        public string strLName { get; set; }
        public string strFName { get; set; }
        public string strPmtType { get; set; }
        public string strSpecialNeeds { get; set; }
        public string strNotes { get; set; }
        public string strPmtRef { get; set; }
        public string strBankName { get; set; }
        public string strFather { get; set; }
        public string strMother { get; set; }
        public string strBillName { get; set; }
        public string strBillAddress { get; set; }
        public string strBillCity { get; set; }
        public string strBillZip { get; set; }
        public string strBillPhone { get; set; }

        public List<string[]> strCustom { get; set; }
        
        public clsIR(long _lngRecordID, long _lngStateID, string _strFName, string _strLName, string _strCompany, string _strAddress, string _strCity, string _strZip, string _strHomePhone, string _strWorkPhone,string _strCellPhone, string _strEmail)
        {
            lngRecordID = _lngRecordID;
            lngStateID = _lngStateID;

            strFName = _strFName;
            strLName = _strLName;
            strCompany = _strCompany;
            strAddress = _strAddress;
            strCity = _strCity;
            strZip = _strZip;
            strHomePhone = _strHomePhone;
            strWorkPhone = _strWorkPhone;
            strCellPhone = _strCellPhone;
            strEmail = _strEmail;

            strMI = "";
            strPmtType = "";
            strSpecialNeeds = "";
            strNotes = "";
            strPmtRef = "";
            strBankName = "";
            strFather = "";
            strMother = "";
            strBillName = "";
            strBillAddress = "";
            strBillCity = "";
            strBillZip = "";
            strBillPhone = "";

            strCustom = new List<string[]>();
        }
    }

    public class clsCustomFieldIRDef
    {
        public bool blnRequired { get; set; }
        public bool blnUseLocal { get; set; }
        public bool blnUseProfile { get; set; }
        public bool blnUseCamper { get; set; }
        public long lngCustomFieldDefIRID { get; set; }
        public long lngProgramID { get; set; }
        public long lngSortOrder { get; set; }
        public string strLocalCaption { get; set; }
        public string strFieldType { get; set; }
        public string mmoWebCaption { get; set; }
        public string mmoHeader { get; set; }
        public string mmoFooter { get; set; }
        public List<string> strDropdownOptions { get; set; }

        public clsCustomFieldIRDef()
        {
            strDropdownOptions = new List<string>();
        }
    }

    public class clsCustomFieldRegDef
    {
        public bool blnRequired { get; set; }
        public bool blnUseLocal { get; set; }
        public bool blnUseOnline { get; set; }
        public long lngCustomFieldDefRegID { get; set; }
        public long lngProgramID { get; set; }
        public long lngSortOrder { get; set; }
        public decimal decCharge { get; set; }
        public string strLocalCaption { get; set; }
        public string strFieldType { get; set; }
        public string mmoWebCaption { get; set; }
        public string mmoHeader { get; set; }
        public string mmoFooter { get; set; }
        public List<string> strDropdownOptions { get; set; }

        public clsCustomFieldRegDef()
        {
            strDropdownOptions = new List<string>();
        }
    }
}