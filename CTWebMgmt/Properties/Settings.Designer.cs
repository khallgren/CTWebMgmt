﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5466
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CTWebMgmt.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string SQLServer {
            get {
                return ((string)(this["SQLServer"]));
            }
            set {
                this["SQLServer"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string SQLDatabase {
            get {
                return ((string)(this["SQLDatabase"]));
            }
            set {
                this["SQLDatabase"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string SQLUserName {
            get {
                return ((string)(this["SQLUserName"]));
            }
            set {
                this["SQLUserName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string SQLPassword {
            get {
                return ((string)(this["SQLPassword"]));
            }
            set {
                this["SQLPassword"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public long lngWSID {
            get {
                return ((long)(this["lngWSID"]));
            }
            set {
                this["lngWSID"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool UseSQLServer {
            get {
                return ((bool)(this["UseSQLServer"]));
            }
            set {
                this["UseSQLServer"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool FirstSetup {
            get {
                return ((bool)(this["FirstSetup"]));
            }
            set {
                this["FirstSetup"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string MainPath {
            get {
                return ((string)(this["MainPath"]));
            }
            set {
                this["MainPath"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://webservices.primerchants.com/CheckVerifyWS/CheckVerifyWS.asmx")]
        public string CTWebMgmt_wsXCharge_CheckVerify {
            get {
                return ((string)(this["CTWebMgmt_wsXCharge_CheckVerify"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://dlq4.donatelinq.net/cqwebservice/CQ.asmx")]
        public string CTWebMgmt_wsCashLinq_CQ {
            get {
                return ((string)(this["CTWebMgmt_wsCashLinq_CQ"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://www.camptrak.com/xfereventinfo/xfereventinfo.asmx")]
        public string CTWebMgmt_wsXferEventInfo_XferEventInfo {
            get {
                return ((string)(this["CTWebMgmt_wsXferEventInfo_XferEventInfo"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://smartmover.melissadata.net/v2b/smartmover.asmx")]
        public string CTWebMgmt_wsMDSmartMoverV2B_SmartMover {
            get {
                return ((string)(this["CTWebMgmt_wsMDSmartMoverV2B_SmartMover"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://services.elementexpress.com/express.asmx")]
        public string CTWebMgmt_wsEPSServices_Express {
            get {
                return ((string)(this["CTWebMgmt_wsEPSServices_Express"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://transaction.elementexpress.com/express.asmx")]
        public string CTWebMgmt_wsEPSTrans_Express {
            get {
                return ((string)(this["CTWebMgmt_wsEPSTrans_Express"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://reporting.elementexpress.com/express.asmx")]
        public string CTWebMgmt_wsEPSReporting_Express {
            get {
                return ((string)(this["CTWebMgmt_wsEPSReporting_Express"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://www.camptrak.com/xfereventinfov2/xfereventinfov2.asmx")]
        public string CTWebMgmt_wsXferEventInfoV2_xfereventinfov2 {
            get {
                return ((string)(this["CTWebMgmt_wsXferEventInfoV2_xfereventinfov2"]));
            }
        }
    }
}
