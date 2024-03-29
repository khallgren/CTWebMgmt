﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5466
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.5466.
// 
#pragma warning disable 1591

namespace CTWebMgmt.wsXferEventInfoV2 {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="xfereventinfov2Soap", Namespace="http://tempuri.org/")]
    public partial class xfereventinfov2 : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback fcnProcessSyncDataULOperationCompleted;
        
        private System.Threading.SendOrPostCallback fcnGenerateSyncDLOperationCompleted;
        
        private System.Threading.SendOrPostCallback fcnProcessSyncResultsOperationCompleted;
        
        private System.Threading.SendOrPostCallback fcnProcessDataULBlindCommitOperationCompleted;
        
        private System.Threading.SendOrPostCallback fcnUpdateLocalIDOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public xfereventinfov2() {
            this.Url = global::CTWebMgmt.Properties.Settings.Default.CTWebMgmt_wsXferEventInfoV2_xfereventinfov2;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event fcnProcessSyncDataULCompletedEventHandler fcnProcessSyncDataULCompleted;
        
        /// <remarks/>
        public event fcnGenerateSyncDLCompletedEventHandler fcnGenerateSyncDLCompleted;
        
        /// <remarks/>
        public event fcnProcessSyncResultsCompletedEventHandler fcnProcessSyncResultsCompleted;
        
        /// <remarks/>
        public event fcnProcessDataULBlindCommitCompletedEventHandler fcnProcessDataULBlindCommitCompleted;
        
        /// <remarks/>
        public event fcnUpdateLocalIDCompletedEventHandler fcnUpdateLocalIDCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/fcnProcessSyncDataUL", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string fcnProcessSyncDataUL(string _strFileName, string _strTableName, string _strWebConn) {
            object[] results = this.Invoke("fcnProcessSyncDataUL", new object[] {
                        _strFileName,
                        _strTableName,
                        _strWebConn});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void fcnProcessSyncDataULAsync(string _strFileName, string _strTableName, string _strWebConn) {
            this.fcnProcessSyncDataULAsync(_strFileName, _strTableName, _strWebConn, null);
        }
        
        /// <remarks/>
        public void fcnProcessSyncDataULAsync(string _strFileName, string _strTableName, string _strWebConn, object userState) {
            if ((this.fcnProcessSyncDataULOperationCompleted == null)) {
                this.fcnProcessSyncDataULOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfcnProcessSyncDataULOperationCompleted);
            }
            this.InvokeAsync("fcnProcessSyncDataUL", new object[] {
                        _strFileName,
                        _strTableName,
                        _strWebConn}, this.fcnProcessSyncDataULOperationCompleted, userState);
        }
        
        private void OnfcnProcessSyncDataULOperationCompleted(object arg) {
            if ((this.fcnProcessSyncDataULCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.fcnProcessSyncDataULCompleted(this, new fcnProcessSyncDataULCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/fcnGenerateSyncDL", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string fcnGenerateSyncDL(string _strSQL, string _strTableName, long _lngCTUserID, string _strWebConn) {
            object[] results = this.Invoke("fcnGenerateSyncDL", new object[] {
                        _strSQL,
                        _strTableName,
                        _lngCTUserID,
                        _strWebConn});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void fcnGenerateSyncDLAsync(string _strSQL, string _strTableName, long _lngCTUserID, string _strWebConn) {
            this.fcnGenerateSyncDLAsync(_strSQL, _strTableName, _lngCTUserID, _strWebConn, null);
        }
        
        /// <remarks/>
        public void fcnGenerateSyncDLAsync(string _strSQL, string _strTableName, long _lngCTUserID, string _strWebConn, object userState) {
            if ((this.fcnGenerateSyncDLOperationCompleted == null)) {
                this.fcnGenerateSyncDLOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfcnGenerateSyncDLOperationCompleted);
            }
            this.InvokeAsync("fcnGenerateSyncDL", new object[] {
                        _strSQL,
                        _strTableName,
                        _lngCTUserID,
                        _strWebConn}, this.fcnGenerateSyncDLOperationCompleted, userState);
        }
        
        private void OnfcnGenerateSyncDLOperationCompleted(object arg) {
            if ((this.fcnGenerateSyncDLCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.fcnGenerateSyncDLCompleted(this, new fcnGenerateSyncDLCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/fcnProcessSyncResults", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string fcnProcessSyncResults(string _strFileName, string _strTableName, string _strWebConn) {
            object[] results = this.Invoke("fcnProcessSyncResults", new object[] {
                        _strFileName,
                        _strTableName,
                        _strWebConn});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void fcnProcessSyncResultsAsync(string _strFileName, string _strTableName, string _strWebConn) {
            this.fcnProcessSyncResultsAsync(_strFileName, _strTableName, _strWebConn, null);
        }
        
        /// <remarks/>
        public void fcnProcessSyncResultsAsync(string _strFileName, string _strTableName, string _strWebConn, object userState) {
            if ((this.fcnProcessSyncResultsOperationCompleted == null)) {
                this.fcnProcessSyncResultsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfcnProcessSyncResultsOperationCompleted);
            }
            this.InvokeAsync("fcnProcessSyncResults", new object[] {
                        _strFileName,
                        _strTableName,
                        _strWebConn}, this.fcnProcessSyncResultsOperationCompleted, userState);
        }
        
        private void OnfcnProcessSyncResultsOperationCompleted(object arg) {
            if ((this.fcnProcessSyncResultsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.fcnProcessSyncResultsCompleted(this, new fcnProcessSyncResultsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/fcnProcessDataULBlindCommit", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string fcnProcessDataULBlindCommit(string _strFileName, string _strTableName, string _strWebConn) {
            object[] results = this.Invoke("fcnProcessDataULBlindCommit", new object[] {
                        _strFileName,
                        _strTableName,
                        _strWebConn});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void fcnProcessDataULBlindCommitAsync(string _strFileName, string _strTableName, string _strWebConn) {
            this.fcnProcessDataULBlindCommitAsync(_strFileName, _strTableName, _strWebConn, null);
        }
        
        /// <remarks/>
        public void fcnProcessDataULBlindCommitAsync(string _strFileName, string _strTableName, string _strWebConn, object userState) {
            if ((this.fcnProcessDataULBlindCommitOperationCompleted == null)) {
                this.fcnProcessDataULBlindCommitOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfcnProcessDataULBlindCommitOperationCompleted);
            }
            this.InvokeAsync("fcnProcessDataULBlindCommit", new object[] {
                        _strFileName,
                        _strTableName,
                        _strWebConn}, this.fcnProcessDataULBlindCommitOperationCompleted, userState);
        }
        
        private void OnfcnProcessDataULBlindCommitOperationCompleted(object arg) {
            if ((this.fcnProcessDataULBlindCommitCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.fcnProcessDataULBlindCommitCompleted(this, new fcnProcessDataULBlindCommitCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/fcnUpdateLocalID", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string fcnUpdateLocalID(long _lngRecordWebID, long _lngRecordID, long _lngCTUserID, string _strWebConn) {
            object[] results = this.Invoke("fcnUpdateLocalID", new object[] {
                        _lngRecordWebID,
                        _lngRecordID,
                        _lngCTUserID,
                        _strWebConn});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void fcnUpdateLocalIDAsync(long _lngRecordWebID, long _lngRecordID, long _lngCTUserID, string _strWebConn) {
            this.fcnUpdateLocalIDAsync(_lngRecordWebID, _lngRecordID, _lngCTUserID, _strWebConn, null);
        }
        
        /// <remarks/>
        public void fcnUpdateLocalIDAsync(long _lngRecordWebID, long _lngRecordID, long _lngCTUserID, string _strWebConn, object userState) {
            if ((this.fcnUpdateLocalIDOperationCompleted == null)) {
                this.fcnUpdateLocalIDOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfcnUpdateLocalIDOperationCompleted);
            }
            this.InvokeAsync("fcnUpdateLocalID", new object[] {
                        _lngRecordWebID,
                        _lngRecordID,
                        _lngCTUserID,
                        _strWebConn}, this.fcnUpdateLocalIDOperationCompleted, userState);
        }
        
        private void OnfcnUpdateLocalIDOperationCompleted(object arg) {
            if ((this.fcnUpdateLocalIDCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.fcnUpdateLocalIDCompleted(this, new fcnUpdateLocalIDCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void fcnProcessSyncDataULCompletedEventHandler(object sender, fcnProcessSyncDataULCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class fcnProcessSyncDataULCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal fcnProcessSyncDataULCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void fcnGenerateSyncDLCompletedEventHandler(object sender, fcnGenerateSyncDLCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class fcnGenerateSyncDLCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal fcnGenerateSyncDLCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void fcnProcessSyncResultsCompletedEventHandler(object sender, fcnProcessSyncResultsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class fcnProcessSyncResultsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal fcnProcessSyncResultsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void fcnProcessDataULBlindCommitCompletedEventHandler(object sender, fcnProcessDataULBlindCommitCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class fcnProcessDataULBlindCommitCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal fcnProcessDataULBlindCommitCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void fcnUpdateLocalIDCompletedEventHandler(object sender, fcnUpdateLocalIDCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class fcnUpdateLocalIDCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal fcnUpdateLocalIDCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591