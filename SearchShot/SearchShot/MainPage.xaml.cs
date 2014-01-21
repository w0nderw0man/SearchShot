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
            if (IsolatedStorageSettings.ApplicationSettings.Contains("ID"))
            {
                if (!IsolatedStorageSettings.ApplicationSettings["ID"].Equals(""))
                {
                    //Verif FB APP TW MS + ConnectWith + Token + Id + Nom + Image
                    //Stocker infos dans ConnectionMode.
                    NavigationService.Navigate(new Uri("/Accueil.xaml", UriKind.Relative));
                }
            }
            
            InitializeComponent();
        }

        public void test(Object sender, getPicCompletedEventArgs e)
        {
            MessageBox.Show(e.Result.ToString());
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