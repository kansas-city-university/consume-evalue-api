﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace EValueApi.EValueSiteApi {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="Site_1_0.cfcSoapBinding", Namespace="http://DefaultNamespace")]
    public partial class Site_1_0Service : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback getOperationCompleted;
        
        private System.Threading.SendOrPostCallback updateOperationCompleted;
        
        private System.Threading.SendOrPostCallback createOperationCompleted;
        
        private System.Threading.SendOrPostCallback unlinkOperationCompleted;
        
        private System.Threading.SendOrPostCallback linkOperationCompleted;
        
        private System.Threading.SendOrPostCallback getByExtIdOperationCompleted;
        
        private System.Threading.SendOrPostCallback addExternalIdOperationCompleted;
        
        private System.Threading.SendOrPostCallback rmvExternalIdOperationCompleted;
        
        private System.Threading.SendOrPostCallback setExtInfoOperationCompleted;
        
        private System.Threading.SendOrPostCallback getAllLinkedOperationCompleted;
        
        private System.Threading.SendOrPostCallback getExtInfoOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Site_1_0Service() {
            this.Url = global::EValueApi.Properties.Settings.Default.EValueApi_EValueSiteApi_Site_1_0Service;
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
        public event getCompletedEventHandler getCompleted;
        
        /// <remarks/>
        public event updateCompletedEventHandler updateCompleted;
        
        /// <remarks/>
        public event createCompletedEventHandler createCompleted;
        
        /// <remarks/>
        public event unlinkCompletedEventHandler unlinkCompleted;
        
        /// <remarks/>
        public event linkCompletedEventHandler linkCompleted;
        
        /// <remarks/>
        public event getByExtIdCompletedEventHandler getByExtIdCompleted;
        
        /// <remarks/>
        public event addExternalIdCompletedEventHandler addExternalIdCompleted;
        
        /// <remarks/>
        public event rmvExternalIdCompletedEventHandler rmvExternalIdCompleted;
        
        /// <remarks/>
        public event setExtInfoCompletedEventHandler setExtInfoCompleted;
        
        /// <remarks/>
        public event getAllLinkedCompletedEventHandler getAllLinkedCompleted;
        
        /// <remarks/>
        public event getExtInfoCompletedEventHandler getExtInfoCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://DefaultNamespace", ResponseNamespace="http://DefaultNamespace")]
        [return: System.Xml.Serialization.SoapElementAttribute("getReturn")]
        public string get(string msg) {
            object[] results = this.Invoke("get", new object[] {
                        msg});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void getAsync(string msg) {
            this.getAsync(msg, null);
        }
        
        /// <remarks/>
        public void getAsync(string msg, object userState) {
            if ((this.getOperationCompleted == null)) {
                this.getOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetOperationCompleted);
            }
            this.InvokeAsync("get", new object[] {
                        msg}, this.getOperationCompleted, userState);
        }
        
        private void OngetOperationCompleted(object arg) {
            if ((this.getCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getCompleted(this, new getCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://DefaultNamespace", ResponseNamespace="http://DefaultNamespace")]
        [return: System.Xml.Serialization.SoapElementAttribute("updateReturn")]
        public string update(string msg) {
            object[] results = this.Invoke("update", new object[] {
                        msg});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void updateAsync(string msg) {
            this.updateAsync(msg, null);
        }
        
        /// <remarks/>
        public void updateAsync(string msg, object userState) {
            if ((this.updateOperationCompleted == null)) {
                this.updateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnupdateOperationCompleted);
            }
            this.InvokeAsync("update", new object[] {
                        msg}, this.updateOperationCompleted, userState);
        }
        
        private void OnupdateOperationCompleted(object arg) {
            if ((this.updateCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.updateCompleted(this, new updateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://DefaultNamespace", ResponseNamespace="http://DefaultNamespace")]
        [return: System.Xml.Serialization.SoapElementAttribute("createReturn")]
        public string create(string msg) {
            object[] results = this.Invoke("create", new object[] {
                        msg});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void createAsync(string msg) {
            this.createAsync(msg, null);
        }
        
        /// <remarks/>
        public void createAsync(string msg, object userState) {
            if ((this.createOperationCompleted == null)) {
                this.createOperationCompleted = new System.Threading.SendOrPostCallback(this.OncreateOperationCompleted);
            }
            this.InvokeAsync("create", new object[] {
                        msg}, this.createOperationCompleted, userState);
        }
        
        private void OncreateOperationCompleted(object arg) {
            if ((this.createCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.createCompleted(this, new createCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://DefaultNamespace", ResponseNamespace="http://DefaultNamespace")]
        [return: System.Xml.Serialization.SoapElementAttribute("unlinkReturn")]
        public string unlink(string msg) {
            object[] results = this.Invoke("unlink", new object[] {
                        msg});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void unlinkAsync(string msg) {
            this.unlinkAsync(msg, null);
        }
        
        /// <remarks/>
        public void unlinkAsync(string msg, object userState) {
            if ((this.unlinkOperationCompleted == null)) {
                this.unlinkOperationCompleted = new System.Threading.SendOrPostCallback(this.OnunlinkOperationCompleted);
            }
            this.InvokeAsync("unlink", new object[] {
                        msg}, this.unlinkOperationCompleted, userState);
        }
        
        private void OnunlinkOperationCompleted(object arg) {
            if ((this.unlinkCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.unlinkCompleted(this, new unlinkCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://DefaultNamespace", ResponseNamespace="http://DefaultNamespace")]
        [return: System.Xml.Serialization.SoapElementAttribute("linkReturn")]
        public string link(string msg) {
            object[] results = this.Invoke("link", new object[] {
                        msg});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void linkAsync(string msg) {
            this.linkAsync(msg, null);
        }
        
        /// <remarks/>
        public void linkAsync(string msg, object userState) {
            if ((this.linkOperationCompleted == null)) {
                this.linkOperationCompleted = new System.Threading.SendOrPostCallback(this.OnlinkOperationCompleted);
            }
            this.InvokeAsync("link", new object[] {
                        msg}, this.linkOperationCompleted, userState);
        }
        
        private void OnlinkOperationCompleted(object arg) {
            if ((this.linkCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.linkCompleted(this, new linkCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://DefaultNamespace", ResponseNamespace="http://DefaultNamespace")]
        [return: System.Xml.Serialization.SoapElementAttribute("getByExtIdReturn")]
        public string getByExtId(string msg) {
            object[] results = this.Invoke("getByExtId", new object[] {
                        msg});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void getByExtIdAsync(string msg) {
            this.getByExtIdAsync(msg, null);
        }
        
        /// <remarks/>
        public void getByExtIdAsync(string msg, object userState) {
            if ((this.getByExtIdOperationCompleted == null)) {
                this.getByExtIdOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetByExtIdOperationCompleted);
            }
            this.InvokeAsync("getByExtId", new object[] {
                        msg}, this.getByExtIdOperationCompleted, userState);
        }
        
        private void OngetByExtIdOperationCompleted(object arg) {
            if ((this.getByExtIdCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getByExtIdCompleted(this, new getByExtIdCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://DefaultNamespace", ResponseNamespace="http://DefaultNamespace")]
        [return: System.Xml.Serialization.SoapElementAttribute("addExternalIdReturn")]
        public string addExternalId(string msg) {
            object[] results = this.Invoke("addExternalId", new object[] {
                        msg});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void addExternalIdAsync(string msg) {
            this.addExternalIdAsync(msg, null);
        }
        
        /// <remarks/>
        public void addExternalIdAsync(string msg, object userState) {
            if ((this.addExternalIdOperationCompleted == null)) {
                this.addExternalIdOperationCompleted = new System.Threading.SendOrPostCallback(this.OnaddExternalIdOperationCompleted);
            }
            this.InvokeAsync("addExternalId", new object[] {
                        msg}, this.addExternalIdOperationCompleted, userState);
        }
        
        private void OnaddExternalIdOperationCompleted(object arg) {
            if ((this.addExternalIdCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.addExternalIdCompleted(this, new addExternalIdCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://DefaultNamespace", ResponseNamespace="http://DefaultNamespace")]
        [return: System.Xml.Serialization.SoapElementAttribute("rmvExternalIdReturn")]
        public string rmvExternalId(string msg) {
            object[] results = this.Invoke("rmvExternalId", new object[] {
                        msg});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void rmvExternalIdAsync(string msg) {
            this.rmvExternalIdAsync(msg, null);
        }
        
        /// <remarks/>
        public void rmvExternalIdAsync(string msg, object userState) {
            if ((this.rmvExternalIdOperationCompleted == null)) {
                this.rmvExternalIdOperationCompleted = new System.Threading.SendOrPostCallback(this.OnrmvExternalIdOperationCompleted);
            }
            this.InvokeAsync("rmvExternalId", new object[] {
                        msg}, this.rmvExternalIdOperationCompleted, userState);
        }
        
        private void OnrmvExternalIdOperationCompleted(object arg) {
            if ((this.rmvExternalIdCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.rmvExternalIdCompleted(this, new rmvExternalIdCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://DefaultNamespace", ResponseNamespace="http://DefaultNamespace")]
        [return: System.Xml.Serialization.SoapElementAttribute("setExtInfoReturn")]
        public string setExtInfo(string msg) {
            object[] results = this.Invoke("setExtInfo", new object[] {
                        msg});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void setExtInfoAsync(string msg) {
            this.setExtInfoAsync(msg, null);
        }
        
        /// <remarks/>
        public void setExtInfoAsync(string msg, object userState) {
            if ((this.setExtInfoOperationCompleted == null)) {
                this.setExtInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsetExtInfoOperationCompleted);
            }
            this.InvokeAsync("setExtInfo", new object[] {
                        msg}, this.setExtInfoOperationCompleted, userState);
        }
        
        private void OnsetExtInfoOperationCompleted(object arg) {
            if ((this.setExtInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.setExtInfoCompleted(this, new setExtInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://DefaultNamespace", ResponseNamespace="http://DefaultNamespace")]
        [return: System.Xml.Serialization.SoapElementAttribute("getAllLinkedReturn")]
        public string getAllLinked(string msg) {
            object[] results = this.Invoke("getAllLinked", new object[] {
                        msg});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void getAllLinkedAsync(string msg) {
            this.getAllLinkedAsync(msg, null);
        }
        
        /// <remarks/>
        public void getAllLinkedAsync(string msg, object userState) {
            if ((this.getAllLinkedOperationCompleted == null)) {
                this.getAllLinkedOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetAllLinkedOperationCompleted);
            }
            this.InvokeAsync("getAllLinked", new object[] {
                        msg}, this.getAllLinkedOperationCompleted, userState);
        }
        
        private void OngetAllLinkedOperationCompleted(object arg) {
            if ((this.getAllLinkedCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getAllLinkedCompleted(this, new getAllLinkedCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://DefaultNamespace", ResponseNamespace="http://DefaultNamespace")]
        [return: System.Xml.Serialization.SoapElementAttribute("getExtInfoReturn")]
        public string getExtInfo(string msg) {
            object[] results = this.Invoke("getExtInfo", new object[] {
                        msg});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void getExtInfoAsync(string msg) {
            this.getExtInfoAsync(msg, null);
        }
        
        /// <remarks/>
        public void getExtInfoAsync(string msg, object userState) {
            if ((this.getExtInfoOperationCompleted == null)) {
                this.getExtInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetExtInfoOperationCompleted);
            }
            this.InvokeAsync("getExtInfo", new object[] {
                        msg}, this.getExtInfoOperationCompleted, userState);
        }
        
        private void OngetExtInfoOperationCompleted(object arg) {
            if ((this.getExtInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getExtInfoCompleted(this, new getExtInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    public delegate void getCompletedEventHandler(object sender, getCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    public delegate void updateCompletedEventHandler(object sender, updateCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class updateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal updateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    public delegate void createCompletedEventHandler(object sender, createCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class createCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal createCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    public delegate void unlinkCompletedEventHandler(object sender, unlinkCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class unlinkCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal unlinkCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    public delegate void linkCompletedEventHandler(object sender, linkCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class linkCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal linkCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    public delegate void getByExtIdCompletedEventHandler(object sender, getByExtIdCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getByExtIdCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getByExtIdCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    public delegate void addExternalIdCompletedEventHandler(object sender, addExternalIdCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class addExternalIdCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal addExternalIdCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    public delegate void rmvExternalIdCompletedEventHandler(object sender, rmvExternalIdCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class rmvExternalIdCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal rmvExternalIdCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    public delegate void setExtInfoCompletedEventHandler(object sender, setExtInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class setExtInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal setExtInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    public delegate void getAllLinkedCompletedEventHandler(object sender, getAllLinkedCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getAllLinkedCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getAllLinkedCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    public delegate void getExtInfoCompletedEventHandler(object sender, getExtInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getExtInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getExtInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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