using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Live;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SearchShot.ServiceReference1;

namespace SearchShot
{
    public partial class ConnectPageMicrosoft : PhoneApplicationPage
    {
      /*  public SkyDriveFileHandler Skydrive;

        public ConnectPageMicrosoft()
        {
            InitializeComponent();
            Skydrive =  new SkyDriveFileHandler(App.LiveSession, "000000004C107129", "skydrivetestapp");
        } */
        private static readonly string[] Scopes =
            new[] { 
            "wl.signin", 
            "wl.basic",
            "wl.emails",
            "wl.skydrive",
            "wl.offline_access",
            "wl.skydrive_update" };
    
        private LiveConnectClient _connection;
        private LiveLoginResult _login;

        public ConnectPageMicrosoft()
       {
            Loaded += OnLoaded;
       }
    
       private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
       {

            await SkydriveLogin();
            ConnectionMode.ConnectWith = "Microsoft";
            ConnectionMode.Token = _login.Session.AccessToken;
            await GetAccountInformations();
            NavigationService.Navigate(new Uri("/Accueil.xaml", UriKind.Relative));
        }
     
       private async Task SkydriveLogin()
        {
            try
            {
                LiveAuthClient authClient = new LiveAuthClient("000000004C107129");
               _login = await authClient.InitializeAsync(Scopes);
                if (_login.Status != LiveConnectSessionStatus.Connected)
                {
                   _login = await authClient.LoginAsync(Scopes);
                }
                _connection = new LiveConnectClient(_login.Session);
           }
           catch (Exception ex)
            {
                Dispatcher.BeginInvoke(() => MessageBox.Show(ex.Message));
            }
        }

       private async Task GetAccountInformations()
       {
           try
           {
               LiveOperationResult operationResult = await _connection.GetAsync("me");
               var jsonResult = operationResult.Result as dynamic;
               string mail = jsonResult.emails.preferred;
               Service1Client ws = new Service1Client();
               ws.GetUserIdCompleted += CheckId;
               ws.GetUserIdAsync(mail);
            }
           catch (Exception e)
           {
               MessageBox.Show(e.Message);
           }
       }

        private async void CheckId(Object sender, GetUserIdCompletedEventArgs e)
        {
            int id = e.Result;
            if (id != 0)
            {
                IsolatedStorageSettings.ApplicationSettings["ID"] = id;
                Service1Client ws = new Service1Client();
                ws.GetUserInfosCompleted += GetUser;
                ws.GetUserInfosAsync(id);
            }
            else
            {
                Service1Client ws = new Service1Client();
                ws.InscriptionSocialCompleted += FinInscription;
                LiveOperationResult operationResult = await _connection.GetAsync("me");
                var jsonResult = operationResult.Result as dynamic;
                string prenom = jsonResult.first_name ?? string.Empty;
                string nom = jsonResult.last_name ?? string.Empty;
                string email = jsonResult.emails.preferred;
                LiveOperationResult operationResult2 = await _connection.GetAsync("me/picture");
                dynamic result = operationResult2.Result;
                BitmapImage image = new BitmapImage(new Uri(result.location, UriKind.Absolute));
                Image imgResult = new Image();
                imgResult.Source = image;
                ConnectionMode.Image = image;
                ConnectionMode.Name = prenom;
                ws.InscriptionSocialAsync(prenom.ToString(), nom.ToString(), prenom.ToString(), email.ToString(), new byte[1], 4, ConnectionMode.Token);
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


    }
}