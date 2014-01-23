using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using SearchShot.FacebookUtils;
using SearchShot.ServiceReference1;

namespace SearchShot
{
    public partial class MainPage
    {
        public MainPage()
        {
          
            InitializeComponent();
            if (IsolatedStorageSettings.ApplicationSettings.Contains("ID"))
            {
                if (!((string)IsolatedStorageSettings.ApplicationSettings["ID"]).Equals(""))
                {
                    int id = Int32.Parse(IsolatedStorageSettings.ApplicationSettings["ID"].ToString());
                    MessageBox.Show(id.ToString());
                    WebService.Service.GetInfosCompleted += stockInfos;
                    WebService.Service.GetInfosAsync(id);

                    //Verif FB APP TW MS + ConnectWith + Token + Id + Nom + Image
                    //Stocker infos dans ConnectionMode.
                }
            }
        }

        public void stockInfos(Object sender, GetInfosCompletedEventArgs e)
        {
            WebService.Service.GetInfosCompleted -= stockInfos;

            if (e.Result.connection.Equals(1))
            {
                ConnectionMode.ConnectWith = "App";
            }
            else if (e.Result.connection.Equals(2))
            {
                ConnectionMode.ConnectWith = "Facebook";

            }
            else if (e.Result.connection.Equals(3))
            {
                ConnectionMode.ConnectWith = "Twitter";

            }
            else if (e.Result.connection.Equals(4))
            {
                ConnectionMode.ConnectWith = "Microsoft";

            }
            ConnectionMode.Name = e.Result.login;
            ConnectionMode.Id = Int32.Parse(IsolatedStorageSettings.ApplicationSettings["ID"].ToString());
            ConnectionMode.Token = e.Result.token;
            ConnectionMode.TwitterLogin = e.Result.twitter;
            NavigationService.Navigate(new Uri("/Accueil.xaml", UriKind.Relative));

        }

        private void SetLoggedInState(bool loggedIn)
        {
            if (loggedIn)
            {
                //MTextBlockAccessToken.Text = "-";
                BtnConnectFacebook.IsEnabled = true;
               // BtnDisconnectFacebook.IsEnabled = false;
            }
            else
            {
               // MTextBlockAccessToken.Text = FacebookClient.Instance.AccessToken;
                BtnConnectFacebook.IsEnabled = false;
               // BtnDisconnectFacebook.IsEnabled = true;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Update Buttons' status everytime this page is navigated to
            SetLoggedInState("".Equals(FacebookClient.Instance.AccessToken));
        }

        private void BtnDisconnectFacebook_Click(object sender, RoutedEventArgs e)
        {
            // Disconnect from Facebook
            FacebookClient.Instance.AccessToken = "";
            SetLoggedInState(true);
        }

        private void BtnConnectFacebook_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ConnectPageFacebook.xaml", UriKind.Relative));
        }

        private void BtnConnectTwitter_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ConnectPageTwitter.xaml", UriKind.Relative));
        }

        private void BtnConnectMicrosoft_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ConnectPageMicrosoft.xaml", UriKind.Relative));
        }

        private void BtnConnectApp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ConnectPageApp.xaml", UriKind.Relative));
        }

        private void BtnInscription_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/InscriptionPage.xaml", UriKind.Relative));
        }
    }
}