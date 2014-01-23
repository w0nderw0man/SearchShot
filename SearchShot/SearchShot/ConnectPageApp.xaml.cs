using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
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

        private void AppAuth(object sender, AuthentificationCompletedEventArgs getAuthCompletedEventArgs)
        {
            if (getAuthCompletedEventArgs.Result > 0)
            {
                ConnectionMode.ConnectWith = "App";
                ConnectionMode.Name = Login.Text;
                ConnectionMode.Token = "Token";
                IsolatedStorageSettings.ApplicationSettings["ID"] = getAuthCompletedEventArgs.Result.ToString();
                NavigationService.Navigate(new Uri("/Accueil.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Mauvais identifiants.");
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
           WebService.Service.AuthentificationCompleted += AppAuth;
           WebService.Service.AuthentificationAsync(Login.Text, Password.Password);
        }

    }
}