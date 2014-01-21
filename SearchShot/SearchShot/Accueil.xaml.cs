using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
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
            if (ConnectionMode.ConnectWith.Equals("Facebook"))
            {
               FacebookGetFeed();
            }
            if (ConnectionMode.ConnectWith.Equals("Twitter"))
            {
                TwitterGetFeed();
            }
            ConnectionMode.Id = Int32.Parse((string)IsolatedStorageSettings.ApplicationSettings["ID"]);
            PlayerName.Text = ConnectionMode.Name;
            Service1Client ws = new Service1Client();
            ws.GetScoreCompleted += AffScore;
            ws.GetScoreAsync(ConnectionMode.Id);
            ws.GetScoreCompleted += AffScore;
            ws.SetLastConCompleted += LastCon;
            ws.SetLastConAsync(ConnectionMode.Id);
        }

        private void LastCon(Object sender, SetLastConCompletedEventArgs e)
        {
        }

        private async void TwitterGetFeed()
        {
            Service1Client ws = new Service1Client();
            ws.GetUserIdTwitterCompleted += CheckIdTwitter;
            ws.GetUserIdTwitterAsync(ConnectionMode.Name);
        }

        private void CheckIdTwitter(Object sender, GetUserIdTwitterCompletedEventArgs e)
        {
            int id = e.Result;
            if (id != 0)
            {
                IsolatedStorageSettings.ApplicationSettings["ID"] = id;
                ConnectionMode.Id = id;
                Service1Client ws = new Service1Client();
                //Manque Photo
                ws.GetUserInfosCompleted += GetUser;
                ws.GetUserInfosAsync(id);
            }
            else
            {
                Service1Client ws = new Service1Client();
                ws.InscriptionTwitterCompleted += FinInscriptionTwitter;
                ws.InscriptionTwitterAsync(ConnectionMode.Name, new byte[1], 3, ConnectionMode.Token);
            }
        }
        private void FinInscriptionTwitter(Object sender, InscriptionTwitterCompletedEventArgs e)
        {
            int id = e.Result;
            IsolatedStorageSettings.ApplicationSettings["ID"] = id;
        }

        private async void FacebookGetFeed()
        {
            FacebookClient _client = new FacebookClient(ConnectionMode.Token);
            dynamic flux = await _client.GetTaskAsync("me");
            dynamic id = flux.id;
            dynamic name = flux.name;
            dynamic email = flux.email;
            string mail = email.ToString();
          //  MessageBox.Show(flux.name + flux.id + flux.email);
         //   PlayerName.Text = flux.name;
            Service1Client ws = new Service1Client();
            ws.GetUserIdCompleted += CheckId;
            ws.GetUserIdAsync(mail);
        }

        private async void CheckId(Object sender, GetUserIdCompletedEventArgs e)
        {
            int id = e.Result;
            if (id != 0)
            {
                IsolatedStorageSettings.ApplicationSettings["ID"] = id;
                ConnectionMode.Id = id;
                Service1Client ws = new Service1Client();
                //Manque Photo
                ws.GetUserInfosCompleted += GetUser;
                ws.GetUserInfosAsync(id);
            }
            else
            {
                FacebookClient _client = new FacebookClient(ConnectionMode.Token);
                dynamic flux = await _client.GetTaskAsync("me");
                dynamic login = flux.name;
                dynamic email = flux.email;
                dynamic nom = flux.last_name;
                dynamic prenom = flux.first_name;
                dynamic photo = flux.picture;
                string mail = email.ToString();
                Service1Client ws = new Service1Client();
                ws.InscriptionSocialCompleted += FinInscription;
                ws.InscriptionSocialAsync(login.ToString(), nom.ToString(), prenom.ToString(), email.ToString(), new byte[1], 2, ConnectionMode.Token);
            }
        }

        private void FinInscription(Object sender, InscriptionSocialCompletedEventArgs e)
        {
            int id = e.Result;
            IsolatedStorageSettings.ApplicationSettings["ID"] = id;
        }

        private void GetUser(Object sender, GetUserInfosCompletedEventArgs e)
        {
            ConnectionMode.Name = e.Result.login;
            ConnectionMode.Token = e.Result.token;
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

        private void BtnDeconnexion_Click(object sender, RoutedEventArgs e)
        {
            IsolatedStorageSettings.ApplicationSettings["ID"] = "";
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void InfosButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/InfosPage.xaml", UriKind.Relative));
        }

        private void HowToButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/HowToPage.xaml", UriKind.Relative));
        }

        private void AffScore(Object sender, GetScoreCompletedEventArgs e)
        {
            int point = e.Result;
            Score.Text = point.ToString() + " Points";
        }


    }
}