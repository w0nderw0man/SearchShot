using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace SearchShot
{
    public partial class ConnectPageApp : PhoneApplicationPage
    {
        public ConnectPageApp()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            ConnectionMode.ConnectWith = "App";
            ConnectionMode.Name = "Test";
            ConnectionMode.Token = "TEST1";
            NavigationService.Navigate(new Uri("/Accueil.xaml", UriKind.Relative));
        }
    }
}