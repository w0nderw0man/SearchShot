﻿#pragma checksum "C:\Users\yechoua\Documents\GitHub\SearchShot\SearchShot\SearchShot\InscriptionPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "49214780D38AA01DFC1B59482C6F836D"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.34003
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace SearchShot {
    
    
    public partial class InscriptionPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBox Pseudo;
        
        internal System.Windows.Controls.TextBox Mail;
        
        internal System.Windows.Controls.TextBox Password;
        
        internal System.Windows.Controls.TextBox PasswordConfirm;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/SearchShot;component/InscriptionPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.Pseudo = ((System.Windows.Controls.TextBox)(this.FindName("Pseudo")));
            this.Mail = ((System.Windows.Controls.TextBox)(this.FindName("Mail")));
            this.Password = ((System.Windows.Controls.TextBox)(this.FindName("Password")));
            this.PasswordConfirm = ((System.Windows.Controls.TextBox)(this.FindName("PasswordConfirm")));
        }
    }
}
