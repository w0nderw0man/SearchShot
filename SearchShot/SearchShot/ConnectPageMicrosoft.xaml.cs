using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Live;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

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
           //----------------------------------------------------------------------
           // Login to skydrive
            //----------------------------------------------------------------------
            await SkydriveLogin();
            ConnectionMode.ConnectWith = "Microsoft";
            ConnectionMode.Token = _login.Session.AccessToken;
            NavigationService.Navigate(new Uri("/Accueil.xaml", UriKind.Relative));
        }
     
       private async Task SkydriveLogin()
        {
            try
            {
                //----------------------------------------------------------------------
                // Initialize our auth client with the client Id for our specific application
                //----------------------------------------------------------------------
                LiveAuthClient authClient = new LiveAuthClient("000000004C107129");
     
                //----------------------------------------------------------------------
                // Using InitializeAsync we can check to see if we already have an connected session
                //----------------------------------------------------------------------
               _login = await authClient.InitializeAsync(Scopes);
   
              //----------------------------------------------------------------------
              // If not connected, bring up the login screen on the device
                //----------------------------------------------------------------------
                if (_login.Status != LiveConnectSessionStatus.Connected)
                {
                   _login = await authClient.LoginAsync(Scopes);
                }
    
               //----------------------------------------------------------------------
              // Initialize our connection client with our login result
                //----------------------------------------------------------------------
                _connection = new LiveConnectClient(_login.Session);
           }
           catch (Exception ex)
            {
                Dispatcher.BeginInvoke(() => MessageBox.Show(ex.Message));
            }
        }
    }
}