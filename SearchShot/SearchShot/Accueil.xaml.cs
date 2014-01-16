using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Facebook;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SearchShot.ServiceReference1;

namespace SearchShot
{
    public partial class Accueil : PhoneApplicationPage
    {
        public Accueil()
        {
            InitializeComponent();
            MessageBox.Show(ConnectionMode.ConnectWith + "\n" + ConnectionMode.Token);
            if (ConnectionMode.ConnectWith.Equals("Facebook"))
            {
              FacebookGetFeed();  
            }
            PlayerName.Text = ConnectionMode.Name;
            Service1Client ws = new Service1Client();
            ws.GetScoreCompleted += AffScore;
            ws.GetScoreAsync(1);
        }

        private async void FacebookGetFeed()
        {
            FacebookClient _client = new FacebookClient(ConnectionMode.Token);
            MessageBox.Show("OK");
            dynamic flux = await _client.GetTaskAsync("me");
            dynamic id = flux.id;
            dynamic name = flux.name;
            MessageBox.Show(flux.name);
            PlayerName.Text = flux.name;
        }

        private void BtnJouer_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/StreamPage.xaml", UriKind.Relative));
        }

        private void BtnClassement_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Classements.xaml", UriKind.Relative));
        }

        private void BtnOptions_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/OptionsMenu.xaml", UriKind.Relative));
        }

        private void BtnAmis_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/FriendPage.xaml", UriKind.Relative));
        }

        private void BtnQuitter_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AffScore(Object sender, GetScoreCompletedEventArgs e)
        {
            int point = e.Result;
            Score.Text = point.ToString() + " Points";
        }
    }
}