using System;
using System.Collections.Generic;
using System.IO;
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
               WebService.Service.GetUserIdCompleted += CheckId;
               WebService.Service.GetUserIdAsync(mail);
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
                WebService.Service.GetUserInfosCompleted += GetUser;
                WebService.Service.GetUserInfosAsync(id);
            }
            else
            {
                WebService.Service.InscriptionSocialCompleted += FinInscription;
                LiveOperationResult operationResult = await _connection.GetAsync("me");
                var jsonResult = operationResult.Result as dynamic;
                string prenom = jsonResult.first_name ?? string.Empty;
                string nom = jsonResult.last_name ?? string.Empty;
                string email = jsonResult.emails.preferred;
                LiveOperationResult operationResult2 = await _connection.GetAsync("me/picture");
                dynamic result = operationResult2.Result;
                BitmapImage image = new BitmapImage(new Uri(result.location, UriKind.Absolute));
 
                byte[] data;
                using (MemoryStream ms = new MemoryStream())
                {
                    WriteableBitmap btmMap = new WriteableBitmap(image);
                    Extensions.SaveJpeg(btmMap, ms,
                        image.PixelWidth, image.PixelHeight, 0, 100);
                    ms.Seek(0, 0);
                    data = new byte[ms.Length];
                    ms.Read(data, 0, data.Length);
                }
                ConnectionMode.Image = data;
                ConnectionMode.Name = prenom;
                WebService.Service.InscriptionSocialAsync(prenom.ToString(), nom.ToString(), prenom.ToString(), email.ToString(), new byte[1], 4, ConnectionMode.Token);
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