using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SearchShot.ServiceReference1;

namespace SearchShot
{
    public partial class InscriptionPage : PhoneApplicationPage
    {
        public InscriptionPage()
        {
            InitializeComponent();
        }

        private void testRetour(object sender,InscriptionCompletedEventArgs  getTestCompletedEventArgs)
        {
            MessageBox.Show(getTestCompletedEventArgs.Result.ToString());
        }

        private void BtnValider_Click(object sender, RoutedEventArgs e)
        {
            if (!Password.Text.Equals(PasswordConfirm.Text))
            {
                MessageBox.Show("Les mots de passe ne correspondent pas.");
            }
            else
            {
               // Service1Client ws = new Service1Client();
             //   ws.InscriptionCompleted += testRetour;

               // ws.InscriptionAsync(Password.Text, Pseudo.Text, Mail.Text);
                Service1Client a = new Service1Client();
                a.InscriptionAsync(Password.Text, Pseudo.Text, Mail.Text);
                //NavigationService.Navigate(new Uri("/Accueil.xaml", UriKind.Relative));
            }
        }
    }
}