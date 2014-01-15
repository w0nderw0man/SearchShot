using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhoneApp1.Resources;

namespace PhoneApp1
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


        private void OnSessionStateChanged(object sender, Facebook.Client.Controls.SessionStateChangedEventArgs e)
        {
         // this.ContentPanel.Visibility = (e.SessionState == Facebook.Client.Controls.FacebookSessionState.Opened) ?
          //                      Visibility.Visible : Visibility.Collapsed;
          if (e.SessionState == Facebook.Client.Controls.FacebookSessionState.Opened)
          {
              this.shareButton.Visibility = Visibility.Visible;
          }
          else if (e.SessionState == Facebook.Client.Controls.FacebookSessionState.Closed)
          {
              this.shareButton.Visibility = Visibility.Collapsed;
          }
        }

        private void OnShareButtonClick(object sender, RoutedEventArgs e)
        {
            this.PublishStory();
        }

        private async void PublishStory()
        {
            await this.loginButton.RequestNewPermissions("publish_stream");

            var facebookClient = new Facebook.FacebookClient(this.loginButton.CurrentSession.AccessToken);

            var postParams = new
            {
                name = "Coucou je teste",
                caption = "Vive le boulot !",
                description = "Casse les couilles ce projet !!!",
                link = "http://www.exia.cesi.fr/‎",
                picture = "http://exiacesiarras.files.wordpress.com/2012/06/exia-logo-20111.png"
            };

            try
            {
                dynamic fbPostTaskResult = await facebookClient.PostTaskAsync("/me/feed", postParams);
                var result = (IDictionary<string, object>)fbPostTaskResult;

                Dispatcher.BeginInvoke(() =>
                {
                    MessageBox.Show("Publication effectuée avec succès", "Result", MessageBoxButton.OK);
                });
            }
            catch (Exception ex)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    MessageBox.Show("Erreur durant la publication, rééssayez plus tard", "Error", MessageBoxButton.OK);
                });
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