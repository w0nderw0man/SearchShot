using System;
using System.Collections.Generic;
using System.IO;
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
        public  Accueil()
        {
            InitializeComponent();
           /* while (NavigationService.CanGoBack)
            {
                NavigationService.RemoveBackEntry();
            }*/
            if (ConnectionMode.ConnectWith.Equals("Facebook"))
            {
               FacebookGetFeed();
            }
            if (ConnectionMode.ConnectWith.Equals("Twitter"))
            {
                TwitterGetFeed();
            }
            ConnectionMode.IsModifInfos = false;
            ConnectionMode.IsModifPicture = false;
            WebService.Service.IsInfosCompletedCompleted += Infos;
            WebService.Service.IsInfosCompletedAsync(ConnectionMode.Id);
            ConnectionMode.Id = Int32.Parse((string)IsolatedStorageSettings.ApplicationSettings["ID"]);
            PlayerName.Text = ConnectionMode.Name;

            WebService.Service.IsPictureCompletedCompleted += Photo;
            WebService.Service.IsPictureCompletedAsync(ConnectionMode.Id);
            WebService.Service.GetScoreAsync(ConnectionMode.Id);
            WebService.Service.GetScoreCompleted += AffScore;
            WebService.Service.SetLastConCompleted += LastCon;
            WebService.Service.SetLastConAsync(ConnectionMode.Id);
        }

        private void Infos(Object sender, IsInfosCompletedCompletedEventArgs e)
        {
            WebService.Service.IsInfosCompletedCompleted -= Infos;

            bool ret = e.Result;
            if (ret == false)
            {
                ConnectionMode.IsModifInfos = true;
                NavigationService.Navigate(new Uri("/ModifInfos.xaml", UriKind.Relative));
            }
        }

        private void UserPic(Object sender, GetUserPictureCompletedEventArgs e)
        {
            byte[] image = e.Result;
            ConnectionMode.Image = image;
            MemoryStream stream = new MemoryStream(ConnectionMode.Image);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.SetSource(stream);
            UserImage.Source = bitmapImage;
        }


        private void Photo(Object sender, IsPictureCompletedCompletedEventArgs e)
        {
            WebService.Service.IsPictureCompletedCompleted -= Photo;
            bool ret = e.Result;
            if (ret == false)
            {
                ConnectionMode.IsModifPicture = true;
                NavigationService.Navigate(new Uri("/Pages/StreamPage.xaml", UriKind.Relative));
            }
            else
            {
                    WebService.Service.GetUserPictureCompleted += UserPic;
                    WebService.Service.GetUserPictureAsync(ConnectionMode.Id);
            }
        }

        private void LastCon(Object sender, SetLastConCompletedEventArgs e)
        {
        }

        private async void TwitterGetFeed()
        {
            WebService.Service.GetUserIdTwitterCompleted += CheckIdTwitter;
            WebService.Service.GetUserIdTwitterAsync(ConnectionMode.Name);
        }

        private void CheckIdTwitter(Object sender, GetUserIdTwitterCompletedEventArgs e)
        {
            int id = e.Result;
            if (id != 0)
            {
                IsolatedStorageSettings.ApplicationSettings["ID"] = id;
                ConnectionMode.Id = id;
                //Manque Photo
                WebService.Service.GetUserInfosCompleted += GetUser;
                WebService.Service.GetUserInfosAsync(id);
            }
            else
            {
                WebService.Service.InscriptionTwitterCompleted += FinInscriptionTwitter;
                WebService.Service.InscriptionTwitterAsync(ConnectionMode.Name, new byte[1], 3, ConnectionMode.Token);
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
            WebService.Service.GetUserIdCompleted += CheckId;
            WebService.Service.GetUserIdAsync(mail);
        }

        private async void CheckId(Object sender, GetUserIdCompletedEventArgs e)
        {
            int id = e.Result;
            if (id != 0)
            {
                IsolatedStorageSettings.ApplicationSettings["ID"] = id;
                ConnectionMode.Id = id;
                //Manque Photo
                WebService.Service.GetUserInfosCompleted += GetUser;
                WebService.Service.GetUserInfosAsync(id);
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
                WebService.Service.InscriptionSocialCompleted += FinInscription;
                WebService.Service.InscriptionSocialAsync(login.ToString(), nom.ToString(), prenom.ToString(), email.ToString(), new byte[1], 2, ConnectionMode.Token);
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