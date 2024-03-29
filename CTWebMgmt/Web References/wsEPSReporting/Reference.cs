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

namespace CTWebMgmt.wsEPSReporting {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="ExpressSoap", Namespace="https://reporting.elementexpress.com")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ExpressResponse))]
    public partial class Express : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback TransactionQueryOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Express() {
            this.Url = global::CTWebMgmt.Properties.Settings.Default.CTWebMgmt_wsEPSReporting_Express;
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
        public event TransactionQueryCompletedEventHandler TransactionQueryCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://reporting.elementexpress.com/TransactionQuery", RequestNamespace="https://reporting.elementexpress.com", ResponseNamespace="https://reporting.elementexpress.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("response")]
        public Response TransactionQuery(Credentials credentials, Application application, Parameters parameters, [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)] ExtendedParameters[] extendedParameters) {
            object[] results = this.Invoke("TransactionQuery", new object[] {
                        credentials,
                        application,
                        parameters,
                        extendedParameters});
            return ((Response)(results[0]));
        }
        
        /// <remarks/>
        public void TransactionQueryAsync(Credentials credentials, Application application, Parameters parameters, ExtendedParameters[] extendedParameters) {
            this.TransactionQueryAsync(credentials, application, parameters, extendedParameters, null);
        }
        
        /// <remarks/>
        public void TransactionQueryAsync(Credentials credentials, Application application, Parameters parameters, ExtendedParameters[] extendedParameters, object userState) {
            if ((this.TransactionQueryOperationCompleted == null)) {
                this.TransactionQueryOperationCompleted = new System.Threading.SendOrPostCallback(this.OnTransactionQueryOperationCompleted);
            }
            this.InvokeAsync("TransactionQuery", new object[] {
                        credentials,
                        application,
                        parameters,
                        extendedParameters}, this.TransactionQueryOperationCompleted, userState);
        }
        
        private void OnTransactionQueryOperationCompleted(object arg) {
            if ((this.TransactionQueryCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.TransactionQueryCompleted(this, new TransactionQueryCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://reporting.elementexpress.com")]
    public partial class Credentials {
        
        private string accountIDField;
        
        private string accountTokenField;
        
        private string acceptorIDField;
        
        private string newAccountTokenField;
        
        /// <remarks/>
        public string AccountID {
            get {
                return this.accountIDField;
            }
            set {
                this.accountIDField = value;
            }
        }
        
        /// <remarks/>
        public string AccountToken {
            get {
                return this.accountTokenField;
            }
            set {
                this.accountTokenField = value;
            }
        }
        
        /// <remarks/>
        public string AcceptorID {
            get {
                return this.acceptorIDField;
            }
            set {
                this.acceptorIDField = value;
            }
        }
        
        /// <remarks/>
        public string NewAccountToken {
            get {
                return this.newAccountTokenField;
            }
            set {
                this.newAccountTokenField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Response))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://reporting.elementexpress.com")]
    public abstract partial class ExpressResponse {
        
        private string expressResponseCodeField;
        
        private string expressResponseMessageField;
        
        private string expressTransactionDateField;
        
        private string expressTransactionTimeField;
        
        private string expressTransactionTimezoneField;
        
        private ExtendedParameters[] extendedParametersField;
        
        /// <remarks/>
        public string ExpressResponseCode {
            get {
                return this.expressResponseCodeField;
            }
            set {
                this.expressResponseCodeField = value;
            }
        }
        
        /// <remarks/>
        public string ExpressResponseMessage {
            get {
                return this.expressResponseMessageField;
            }
            set {
                this.expressResponseMessageField = value;
            }
        }
        
        /// <remarks/>
        public string ExpressTransactionDate {
            get {
                return this.expressTransactionDateField;
            }
            set {
                this.expressTransactionDateField = value;
            }
        }
        
        /// <remarks/>
        public string ExpressTransactionTime {
            get {
                return this.expressTransactionTimeField;
            }
            set {
                this.expressTransactionTimeField = value;
            }
        }
        
        /// <remarks/>
        public string ExpressTransactionTimezone {
            get {
                return this.expressTransactionTimezoneField;
            }
            set {
                this.expressTransactionTimezoneField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public ExtendedParameters[] ExtendedParameters {
            get {
                return this.extendedParametersField;
            }
            set {
                this.extendedParametersField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://reporting.elementexpress.com")]
    public partial class ExtendedParameters {
        
        private string keyField;
        
        private object valueField;
        
        /// <remarks/>
        public string Key {
            get {
                return this.keyField;
            }
            set {
                this.keyField = value;
            }
        }
        
        /// <remarks/>
        public object Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://reporting.elementexpress.com")]
    public partial class Response : ExpressResponse {
        
        private string reportingDataField;
        
        private string reportingIDField;
        
        /// <remarks/>
        public string ReportingData {
            get {
                return this.reportingDataField;
            }
            set {
                this.reportingDataField = value;
            }
        }
        
        /// <remarks/>
        public string ReportingID {
            get {
                return this.reportingIDField;
            }
            set {
                this.reportingIDField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://reporting.elementexpress.com")]
    public partial class Parameters {
        
        private string transactionIDField;
        
        private string terminalIDField;
        
        private string applicationIDField;
        
        private string approvalNumberField;
        
        private string approvedAmountField;
        
        private string expressTransactionDateField;
        
        private string expressTransactionTimeField;
        
        private string hostBatchIDField;
        
        private string hostItemIDField;
        
        private string hostReversalQueueIDField;
        
        private string originalAuthorizedAmountField;
        
        private string referenceNumberField;
        
        private string shiftIDField;
        
        private string sourceTransactionIDField;
        
        private TerminalType terminalTypeField;
        
        private string trackingIDField;
        
        private string transactionAmountField;
        
        private string transactionStatusField;
        
        private string transactionTypeField;
        
        private string xIDField;
        
        private string sourceIPAddressField;
        
        private string externalInterfaceField;
        
        private LogTraceLevel logTraceLevelField;
        
        private string logTraceLevelNameField;
        
        private string machineNameField;
        
        private string sourceObjectField;
        
        private string processIDField;
        
        private string threadIDField;
        
        private BooleanType reverseOrderField;
        
        private string transactionDateTimeBeginField;
        
        private string transactionDateTimeEndField;
        
        private string transactionSetupIDField;
        
        /// <remarks/>
        public string TransactionID {
            get {
                return this.transactionIDField;
            }
            set {
                this.transactionIDField = value;
            }
        }
        
        /// <remarks/>
        public string TerminalID {
            get {
                return this.terminalIDField;
            }
            set {
                this.terminalIDField = value;
            }
        }
        
        /// <remarks/>
        public string ApplicationID {
            get {
                return this.applicationIDField;
            }
            set {
                this.applicationIDField = value;
            }
        }
        
        /// <remarks/>
        public string ApprovalNumber {
            get {
                return this.approvalNumberField;
            }
            set {
                this.approvalNumberField = value;
            }
        }
        
        /// <remarks/>
        public string ApprovedAmount {
            get {
                return this.approvedAmountField;
            }
            set {
                this.approvedAmountField = value;
            }
        }
        
        /// <remarks/>
        public string ExpressTransactionDate {
            get {
                return this.expressTransactionDateField;
            }
            set {
                this.expressTransactionDateField = value;
            }
        }
        
        /// <remarks/>
        public string ExpressTransactionTime {
            get {
                return this.expressTransactionTimeField;
            }
            set {
                this.expressTransactionTimeField = value;
            }
        }
        
        /// <remarks/>
        public string HostBatchID {
            get {
                return this.hostBatchIDField;
            }
            set {
                this.hostBatchIDField = value;
            }
        }
        
        /// <remarks/>
        public string HostItemID {
            get {
                return this.hostItemIDField;
            }
            set {
                this.hostItemIDField = value;
            }
        }
        
        /// <remarks/>
        public string HostReversalQueueID {
            get {
                return this.hostReversalQueueIDField;
            }
            set {
                this.hostReversalQueueIDField = value;
            }
        }
        
        /// <remarks/>
        public string OriginalAuthorizedAmount {
            get {
                return this.originalAuthorizedAmountField;
            }
            set {
                this.originalAuthorizedAmountField = value;
            }
        }
        
        /// <remarks/>
        public string ReferenceNumber {
            get {
                return this.referenceNumberField;
            }
            set {
                this.referenceNumberField = value;
            }
        }
        
        /// <remarks/>
        public string ShiftID {
            get {
                return this.shiftIDField;
            }
            set {
                this.shiftIDField = value;
            }
        }
        
        /// <remarks/>
        public string SourceTransactionID {
            get {
                return this.sourceTransactionIDField;
            }
            set {
                this.sourceTransactionIDField = value;
            }
        }
        
        /// <remarks/>
        public TerminalType TerminalType {
            get {
                return this.terminalTypeField;
            }
            set {
                this.terminalTypeField = value;
            }
        }
        
        /// <remarks/>
        public string TrackingID {
            get {
                return this.trackingIDField;
            }
            set {
                this.trackingIDField = value;
            }
        }
        
        /// <remarks/>
        public string TransactionAmount {
            get {
                return this.transactionAmountField;
            }
            set {
                this.transactionAmountField = value;
            }
        }
        
        /// <remarks/>
        public string TransactionStatus {
            get {
                return this.transactionStatusField;
            }
            set {
                this.transactionStatusField = value;
            }
        }
        
        /// <remarks/>
        public string TransactionType {
            get {
                return this.transactionTypeField;
            }
            set {
                this.transactionTypeField = value;
            }
        }
        
        /// <remarks/>
        public string XID {
            get {
                return this.xIDField;
            }
            set {
                this.xIDField = value;
            }
        }
        
        /// <remarks/>
        public string SourceIPAddress {
            get {
                return this.sourceIPAddressField;
            }
            set {
                this.sourceIPAddressField = value;
            }
        }
        
        /// <remarks/>
        public string ExternalInterface {
            get {
                return this.externalInterfaceField;
            }
            set {
                this.externalInterfaceField = value;
            }
        }
        
        /// <remarks/>
        public LogTraceLevel LogTraceLevel {
            get {
                return this.logTraceLevelField;
            }
            set {
                this.logTraceLevelField = value;
            }
        }
        
        /// <remarks/>
        public string LogTraceLevelName {
            get {
                return this.logTraceLevelNameField;
            }
            set {
                this.logTraceLevelNameField = value;
            }
        }
        
        /// <remarks/>
        public string MachineName {
            get {
                return this.machineNameField;
            }
            set {
                this.machineNameField = value;
            }
        }
        
        /// <remarks/>
        public string SourceObject {
            get {
                return this.sourceObjectField;
            }
            set {
                this.sourceObjectField = value;
            }
        }
        
        /// <remarks/>
        public string ProcessID {
            get {
                return this.processIDField;
            }
            set {
                this.processIDField = value;
            }
        }
        
        /// <remarks/>
        public string ThreadID {
            get {
                return this.threadIDField;
            }
            set {
                this.threadIDField = value;
            }
        }
        
        /// <remarks/>
        public BooleanType ReverseOrder {
            get {
                return this.reverseOrderField;
            }
            set {
                this.reverseOrderField = value;
            }
        }
        
        /// <remarks/>
        public string TransactionDateTimeBegin {
            get {
                return this.transactionDateTimeBeginField;
            }
            set {
                this.transactionDateTimeBeginField = value;
            }
        }
        
        /// <remarks/>
        public string TransactionDateTimeEnd {
            get {
                return this.transactionDateTimeEndField;
            }
            set {
                this.transactionDateTimeEndField = value;
            }
        }
        
        /// <remarks/>
        public string TransactionSetupID {
            get {
                return this.transactionSetupIDField;
            }
            set {
                this.transactionSetupIDField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://reporting.elementexpress.com")]
    public enum TerminalType {
        
        /// <remarks/>
        Unknown,
        
        /// <remarks/>
        PointOfSale,
        
        /// <remarks/>
        ECommerce,
        
        /// <remarks/>
        MOTO,
        
        /// <remarks/>
        FuelPump,
        
        /// <remarks/>
        ATM,
        
        /// <remarks/>
        Voice,
    }
    
    /// <remarks/>
    [System.FlagsAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://reporting.elementexpress.com")]
    public enum LogTraceLevel {
        
        /// <remarks/>
        None = 1,
        
        /// <remarks/>
        Fatal = 2,
        
        /// <remarks/>
        Error = 4,
        
        /// <remarks/>
        Warning = 8,
        
        /// <remarks/>
        Information = 16,
        
        /// <remarks/>
        Trace = 32,
        
        /// <remarks/>
        Debug = 64,
        
        /// <remarks/>
        All = 128,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://reporting.elementexpress.com")]
    public enum BooleanType {
        
        /// <remarks/>
        False,
        
        /// <remarks/>
        True,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://reporting.elementexpress.com")]
    public partial class Application {
        
        private string applicationIDField;
        
        private string applicationNameField;
        
        private string applicationVersionField;
        
        /// <remarks/>
        public string ApplicationID {
            get {
                return this.applicationIDField;
            }
            set {
                this.applicationIDField = value;
            }
        }
        
        /// <remarks/>
        public string ApplicationName {
            get {
                return this.applicationNameField;
            }
            set {
                this.applicationNameField = value;
            }
        }
        
        /// <remarks/>
        public string ApplicationVersion {
            get {
                return this.applicationVersionField;
            }
            set {
                this.applicationVersionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void TransactionQueryCompletedEventHandler(object sender, TransactionQueryCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TransactionQueryCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal TransactionQueryCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public Response Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((Response)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591