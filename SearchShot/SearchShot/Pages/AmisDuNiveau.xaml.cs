using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using ComputerBeacon.Facebook.Fql;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SearchShot.ServiceReference1;
using WP7Contrib.Services.Storage;

namespace SearchShot.Pages
{
    public partial class AmisDuNiveau : PhoneApplicationPage
    {
        public AmisDuNiveau()
        {
            InitializeComponent();
            Loaded += AmisDuNiveau_Loaded;
        }

        private void AmisDuNiveau_Loaded(object sender, RoutedEventArgs einiArgs)
        {
            //implementation du web service
            Service1Client ws1 = new Service1Client();
           
            ws1.GetFriendLevelCompleted += GetAmis;
            ws1.GetFriendLevelAsync(6, 1);
        }

        private void GetAmis(object sender, GetFriendLevelCompletedEventArgs emArgs)
        {
            List<PeopleBinding> listeamis = new List<PeopleBinding>();

            for (int i = 0; i < emArgs.Result.id.ToArray().Length; i++)
            {
                byte[] img = emArgs.Result.img.ToArray()[i];
                MemoryStream img2 = new MemoryStream(img);
                BitmapImage b = new BitmapImage();
                b.SetSource(img2);

                listeamis.Add(new PeopleBinding
                {
                    
                    Avatar = b,
                    Pseudo = emArgs.Result.login.ToArray()[i]
                    
                });
            }

            AmisList.ItemsSource = listeamis;
        }


        public class PeopleBinding
        {
            public string Pseudo { get; set; }
            public BitmapImage Avatar { get; set; }
        }
    }
}
