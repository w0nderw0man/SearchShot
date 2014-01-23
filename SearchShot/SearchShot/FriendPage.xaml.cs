using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SearchShot.ServiceReference1;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace SearchShot
{
    public partial class FriendPage : PhoneApplicationPage
    {
        public FriendPage()
        {
            InitializeComponent();
            WebService.Service.GetFriendsCompleted += GetFriend;
            WebService.Service.GetFriendsAsync(ConnectionMode.Id);
        }

        private void GetFriend(object sender, GetFriendsCompletedEventArgs e)
        {
            WebService.Service.GetFriendsCompleted -= GetFriend;
            List<Friend> friendList = new List<Friend>();
            List<Friend> askFriendList = new List<Friend>();

            for (int i = 0; i < (int)e.Result.id.Count; i++)
            {
                int id = e.Result.id.ToArray()[i];
                string login = e.Result.login.ToArray()[i];
                /*byte[] img = (byte[])e.Result.img.ToArray()[i];
                BitmapImage image = new BitmapImage();
                Image imageToReturn = new Image();
                using (MemoryStream ms = new MemoryStream(img, 0, img.Length))
                {
                    ms.Write(img, 0, img.Length);
                    image.SetSource(ms);
                    imageToReturn.Source = image;
                }*/
                int confirm = e.Result.confirm.ToArray()[i];
                if (confirm == 1)
                {
                    friendList.Add(new Friend(login, new Image(), id, confirm));

                }
                else
                {
                    askFriendList.Add(new Friend(login, new Image(), id, confirm));
                }           
            }
            AmisList.ItemsSource = friendList;
            ConfirmAmisList.ItemsSource = askFriendList;
        }

        private void Selected(object sender, SelectionChangedEventArgs e)
        {
            Friend select = (Friend)ConfirmAmisList.SelectedItem;
            int id = select.Id;
            WebService.Service.GetUserNameCompleted += Name;
            WebService.Service.GetUserNameAsync(id);
        }

        private void Name(Object sender, GetUserNameCompletedEventArgs e)
        {
            string name = e.Result;
            Friend select = (Friend)ConfirmAmisList.SelectedItem;
            int id = select.Id;
            if (MessageBox.Show("Ajouter ?", name, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                WebService.Service.ValidFriendCompleted += Valid;
                WebService.Service.ValidFriendAsync(ConnectionMode.Id, id);
            }
        }

         private void Valid(Object sender, ValidFriendCompletedEventArgs e)
        {
            WebService.Service.ValidFriendCompleted -= Valid;
            bool val = e.Result;
             if (val == true)
             {
                 MessageBox.Show("Ajout Validé.");
                 NavigationService.Navigate(new Uri("/Accueil.xaml", UriKind.Relative));
             }
             else
             {
                 MessageBox.Show("Erreur lors de l'opération.");
             }
        }

        private void Pivot_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonRechercherTap(object sender, GestureEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
    public class Friend
    {
        public string Pseudo { get; set; }
        public System.Windows.Controls.Image Image { get; set; }
        public int Id { get; set; }
        public int Confirm { get; set; }

        public Friend(string pseudo,  Image img, int id, int confirm)
        {
            this.Pseudo = pseudo;
            this.Image = img;
            this.Id = id;
            this.Confirm = confirm;
        }
    }
}