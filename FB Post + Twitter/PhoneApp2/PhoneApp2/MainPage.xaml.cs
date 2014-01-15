using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhoneApp2.Resources;

namespace PhoneApp2
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructeur
        public MainPage()
        {
            InitializeComponent();

            // Exemple de code pour la localisation d'ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void FacebookPostStatus(string status)
        {
            var parameters = new Dictionary<string, object>();
            parameters["message"] = String.Format(status);

            FacebookClient _client = new FacebookClient(SettingsManager.GetSetting("FacebookToken"));
            _client.PostTaskAsync("me/feed", parameters);
        }

        private async void FacebookAuthAsync()
        {
            FacebookSession _session;
            FacebookClient _client;
            string _accessToken;
            try
            {
                FacebookSessionClient facebookSessionClient = new FacebookSessionClient(FacebookAppId);
                if (!string.IsNullOrWhiteSpace(SettingsManager.GetSetting("FacebookToken")))
                {
                    _accessToken = SettingsManager.GetSetting("FacebookToken");
                }
                else
                {
                    _session = await facebookSessionClient.LoginAsync("user_about_me,read_stream,status_update");
                    _accessToken = _session.AccessToken;
                    SettingsManager.SetSetting("FacebookToken", _accessToken);
                }
                _client = new FacebookClient(_accessToken);
            }
            catch (InvalidOperationException e)
            {
                string message = "Erreur d'authentification " + e.Message;
                MessageBox.Show(message);
            }
        }

        // Exemple de code pour la conception d'une ApplicationBar localisée
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Définit l'ApplicationBar de la page sur une nouvelle instance d'ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Crée un bouton et définit la valeur du texte sur la chaîne localisée issue d'AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Crée un nouvel élément de menu avec la chaîne localisée d'AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}