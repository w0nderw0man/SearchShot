using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SearchShot.ServiceReference1;

namespace SearchShot
{
    public partial class ConnectPageApp : PhoneApplicationPage
    {
        public ConnectPageApp()
        {
            InitializeComponent();
        }

        private void testRetour(object sender, AuthentificationCompletedEventArgs getAuthCompletedEventArgs)
        {
            if (getAuthCompletedEventArgs.Result == true)
            {
                ConnectionMode.ConnectWith = "App";
                ConnectionMode.Name = Login.Text;
                ConnectionMode.Token = "TEST1";
                NavigationService.Navigate(new Uri("/Accueil.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Mauvais identifiants.");
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
           
           var ws = new Service1Client();
           ws.AuthentificationCompleted += testRetour;
           ws.AuthentificationAsync(Login.Text, Password.Password);
        }

    }
}