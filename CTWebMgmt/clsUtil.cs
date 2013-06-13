using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;

/// <summary>
/// Summary description for clsUtils
/// </summary>
public class clsUtils
{
    public clsUtils()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}

public class CustomRecordData
{
    public CustomRecordData()
    {
        Records = new List<Record>();
    }

    public List<Record> Records;
}

[Serializable()]
public class Record
{
    private long RECORDWEBID;

    public long RecordWebID
    {
        get { return RECORDWEBID; }
        set { RECORDWEBID = value; }
    }

    private List<CustomField> CUSTOMFIELDS = new List<CustomField>();

    public List<CustomField> CustomFields
    {
        get { return CUSTOMFIELDS; }
        set { CUSTOMFIELDS = value; }
    }
}

[Serializable()]
public class CustomField
{
    private string LOCALCAPTION;
    public string LocalCaption
    {
        get { return LOCALCAPTION; }
        set { LOCALCAPTION = value; }
    }

    private string CUSTOMVALUE;
    public string CustomValue
    {
        get { return CUSTOMVALUE; }
        set { CUSTOMVALUE = value; }
    }
}