using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Windows.Foundation.Metadata;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SearchShot.ServiceReference1;

namespace SearchShot
{
    public partial class ModifInfos : PhoneApplicationPage
    {
        public ModifInfos()
        {
            InitializeComponent();
            if (ConnectionMode.IsModifInfos == true)
            {
                MessageBox.Show("Obtenez vos premiers points en complétant votre profil.");
            }
            FillBlocks();
        }



        private void Back(object sender, GetInfosCompletedEventArgs getInfoCompleted)
        {
            WebService.Service.GetInfosCompleted -= Back;

            
            Nom.Text = getInfoCompleted.Result.nom;
            Prenom.Text = getInfoCompleted.Result.prenom;
            Pseudo.Text = getInfoCompleted.Result.login;
            Ville.Text = getInfoCompleted.Result.ville;
            

        }

        private void FillBlocks()
        {

            WebService.Service.GetInfosCompleted += Back;
            WebService.Service.GetInfosAsync(ConnectionMode.Id);

        }

        private void BackModif(object sender, SetInfosCompletedEventArgs setInfoCompleted)
        {
            if (setInfoCompleted.Result == "ok")
            {
                ConnectionMode.IsModifInfos = false;

                NavigationService.Navigate(new Uri("/Accueil.xaml", UriKind.Relative));
            }
            else
            {
                ConnectionMode.IsModifInfos = false;
                MessageBox.Show("Erreur lors de la mise à jour.");
            }
        }

        private void BtnValid_Click(object sender, RoutedEventArgs e)
        {
            if (!Pseudo.Text.Equals("") && !Pseudo.Text.Replace(" ", "").Equals("") && !Prenom.Text.Equals("") && !Prenom.Text.Replace(" ", "").Equals("")
                && !Nom.Text.Equals("") && !Nom.Text.Replace(" ", "").Equals("") && !Ville.Text.Equals("") && !Ville.Text.Replace(" ", "").Equals(""))
            {
                WebService.Service.SetInfosCompleted += BackModif;
                WebService.Service.SetInfosAsync(Nom.Text, Prenom.Text, Pseudo.Text, Ville.Text, ConnectionMode.Id);
            }
            else
            {
                MessageBox.Show("Les champs ne peuvent être vides.");
            }
        }
    }
}