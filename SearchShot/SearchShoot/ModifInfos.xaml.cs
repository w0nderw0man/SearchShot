using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
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
            FillBlocks();
        }
        Service1Client ws = new Service1Client();

        private void Back(object sender, GetInfosCompletedEventArgs getInfoCompleted)
        {

            Nom.Text = getInfoCompleted.Result.nom;
            Prenom.Text = getInfoCompleted.Result.prenom;
            Pseudo.Text = getInfoCompleted.Result.login;
            Ville.Text = getInfoCompleted.Result.ville;

        }

        private void FillBlocks()
        {
//            Service1Client ws = new Service1Client();
            ws.GetInfosCompleted += Back;
            ws.GetInfosAsync(1); //ConnectionMode.Id);
        }

        private void BackModif(object sender, SetInfosCompletedEventArgs setInfoCompleted)
        {
            if (setInfoCompleted.Result != -1)
            {
                NavigationService.Navigate(new Uri("/Accueil.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Erreur lors de la mise à jour.");
            }
        }

        private void BtnValider_Click(object sender, RoutedEventArgs e)
        {
            if (!Pseudo.Text.Equals("") && !Pseudo.Text.Replace(" ", "").Equals(""))
            {
                Service1Client ws = new Service1Client();
                ws.SetInfosCompleted += BackModif;
                ws.SetInfosAsync(Nom.Text, Prenom.Text, Pseudo.Text, Ville.Text, 1); //ConnectionMode.Id);
            }
            else
            {
                MessageBox.Show("Le pseudo est obligatoire.");
            }
        }
    }
}