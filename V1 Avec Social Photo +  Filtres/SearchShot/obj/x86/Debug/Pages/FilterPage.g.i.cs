﻿#pragma checksum "C:\Users\Jon\Documents\Visual Studio 2013\Projects\SearchShot\SearchShot\Pages\FilterPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0F354395EDC805A18BD3DC5978BCE92C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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
    
    
    public partial class FilterPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Pivot Pivot;
        
        internal Microsoft.Phone.Controls.WrapPanel StandardFiltersWrapPanel;
        
        internal Microsoft.Phone.Controls.WrapPanel EnhancementFiltersWrapPanel;
        
        internal System.Windows.Controls.ProgressBar ProgressBar;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/SearchShot;component/Pages/FilterPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.Pivot = ((Microsoft.Phone.Controls.Pivot)(this.FindName("Pivot")));
            this.StandardFiltersWrapPanel = ((Microsoft.Phone.Controls.WrapPanel)(this.FindName("StandardFiltersWrapPanel")));
            this.EnhancementFiltersWrapPanel = ((Microsoft.Phone.Controls.WrapPanel)(this.FindName("EnhancementFiltersWrapPanel")));
            this.ProgressBar = ((System.Windows.Controls.ProgressBar)(this.FindName("ProgressBar")));
        }
    }
}

