using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace SearchShot
{
    public partial class InscriptionPage : PhoneApplicationPage
    {
        public InscriptionPage()
        {
            InitializeComponent();
        }

        private void BtnValider_Click(object sender, RoutedEventArgs e)
        {
            if (!Password.Equals(PasswordConfirm))
            {
                MessageBox.Show("Les mots de passe ne correspondent pas.");
            }
            else
            {
             
            }
        }
    }
}