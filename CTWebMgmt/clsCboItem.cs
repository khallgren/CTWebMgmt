using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CTWebMgmt
{
    class clsCboItem
    {

        string strItem;
        string strID;

        long lngID;

        public clsCboItem(long _lngNewID, string _strNewItem)
        {

            strItem = _strNewItem;
            lngID = _lngNewID;
        }

        public clsCboItem(string _strNewID, string _strNewItem)
        {
            strID = _strNewID;
            strItem = _strNewItem;
            lngID = 0;
        }

        public string Item
        {
            get
            {
                return strItem;
            }

            set
            {
                strItem = value;
            }
        }

        public long ID
        {
            get
            {
                return lngID;
            }
            set
            {
                lngID = value;
            }
        }

        public string STRID
        {
            get
            {
                return strID;
            }
            set
            {
                strID = value;
            }
        }

        public override string ToString()
        {
            return strItem;
        }

        public static void subSetSelectedIndex(ref ComboBox _cboToSet, long _lngID)
        {
            for (int intI = 0; intI < _cboToSet.Items.Count; intI++)
            {
                clsCboItem cboVal = (clsCboItem)_cboToSet.Items[intI];

                if (cboVal.lngID == _lngID)
                    _cboToSet.SelectedIndex = intI;
            }
        }

        public static void subSetSelectedIndex(ref ComboBox _cboToSet, string _strID)
        {
            for (int intI = 0; intI < _cboToSet.Items.Count; intI++)
            {
                clsCboItem cboVal = (clsCboItem)_cboToSet.Items[intI];

                if (cboVal.strID == _strID)
                    _cboToSet.SelectedIndex = intI;
            }
        }
    }
}
