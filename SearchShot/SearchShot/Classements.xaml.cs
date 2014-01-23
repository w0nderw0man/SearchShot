using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Net.NetworkInformation;
using Microsoft.Phone.Shell;
using SearchShot.ServiceReference1;
using TweetSharp;


namespace SearchShot
{
    public partial class Classements : PhoneApplicationPage
    {
        public Classements()
        {
            InitializeComponent();
            Loaded += ClassementPageLoaded;
        }

        private void ClassementPageLoaded(object sender, RoutedEventArgs e)
        {
            WebService.Service.GetPeopleInfosCompleted += GetPeople;
            WebService.Service.GetPeopleInfosAsync();
            WebService.Service.GetFriendInfosCompleted += GetFriend;
            WebService.Service.GetFriendInfosAsync(ConnectionMode.Id);
        }

        private void GetPeople(object sender, GetPeopleInfosCompletedEventArgs e)
        {
            List<People> peopleList = new List<People>();
            
            for (int i =0; i< (int)e.Result.id.Count ; i++)
            {/*
                int id = (int)e.Result.id;
                int score = (int) e.Result.score;
               // DateTime dateinsc = (DateTime) e.Result.date_insc.ToArray().GetValue(i);
                string login = (string) e.Result.login;*/
                /*byte[] img = (byte[])e.Result.img.ToArray().GetValue(i);
                BitmapImage image = new BitmapImage();
                Image imageToReturn = new Image();
                using (MemoryStream ms = new MemoryStream(img, 0, img.Length))
                {
                    ms.Write(img, 0, img.Length);
                    image.SetSource(ms);
                    imageToReturn.Source = image;
                }*/
                int id = e.Result.id.ToArray()[i];
                int score = e.Result.score.ToArray()[i];
                string login = e.Result.login.ToArray()[i];
                DateTime date_insc = e.Result.date_insc.ToArray()[i];
                byte[] img = e.Result.img.ToArray()[i];
                MemoryStream stream = new MemoryStream(img);
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.SetSource(stream);
                TimeSpan diff = DateTime.Today - date_insc;
                int difference = score / diff.Days;
                int j = i + 1;
                string classement = j.ToString();
                if (i == 0)
                {
                    classement += "er";
                }
                else
                {
                    classement += "eme";
                }
                peopleList.Add(new People(login, score, bitmapImage , classement, date_insc, id, difference));
            }
            List<People> sorted = peopleList;
            sorted.Sort(delegate(People p1, People p2) { return p1.Score.CompareTo(p2.Score); });

            GeneralList.ItemsSource = peopleList;
            GetPeopleProg(peopleList);
        }


        private void GetPeopleProg(List<People> peopleList)
        {
            List<People> sorted = peopleList;
            sorted.Sort(delegate(People p1, People p2) { return p1.Diff.CompareTo(p2.Diff); });
            ProgressionGeneralList.ItemsSource = sorted;
        }

        private void GetFriend(object sender, GetFriendInfosCompletedEventArgs e)
        {
            List<People> peopleList = new List<People>();

            for (int i = 0; i < (int)e.Result.id.Count; i++)
            {/*
                int id = (int)e.Result.id;
                int score = (int) e.Result.score;
               // DateTime dateinsc = (DateTime) e.Result.date_insc.ToArray().GetValue(i);
                string login = (string) e.Result.login;*/
                /*byte[] img = (byte[])e.Result.img.ToArray().GetValue(i);
                BitmapImage image = new BitmapImage();
                Image imageToReturn = new Image();
                using (MemoryStream ms = new MemoryStream(img, 0, img.Length))
                {
                    ms.Write(img, 0, img.Length);
                    image.SetSource(ms);
                    imageToReturn.Source = image;
                }*/
                int id = e.Result.id.ToArray()[i];
                int score = e.Result.score.ToArray()[i];
                string login = e.Result.login.ToArray()[i];
                DateTime date_insc = e.Result.date_insc.ToArray()[i];
                TimeSpan diff = DateTime.Today - date_insc;
                int difference = score / diff.Days;
                byte[] img = e.Result.img.ToArray()[i];
                MemoryStream stream = new MemoryStream(img);
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.SetSource(stream);
                int j = i + 1;
                string classement = j.ToString();
                if (i == 0)
                {
                    classement += "er";
                }
                else
                {
                    classement += "eme";
                }
                peopleList.Add(new People(login, score, bitmapImage, classement, date_insc, id, difference));
            }
            List<People> sorted = peopleList;
            sorted.Sort(delegate(People p1, People p2) { return p1.Score.CompareTo(p2.Score); });
            AmisList.ItemsSource = peopleList;
            GetFriendProg(peopleList);
        }


        private void GetFriendProg(List<People> peopleList)
        {
            List<People> sorted = peopleList;
            sorted.Sort(delegate(People p1, People p2) { return p1.Diff.CompareTo(p2.Diff); });
            ProgressionAmisList.ItemsSource = sorted;
        }

        private void Pivot_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }

    public class People
    {
        public string Pseudo { get; set; }
        public int Score { get; set; }
        public BitmapImage Image { get; set; }
        public DateTime DateInsc { get; set; }
        public string Classement { get; set; }
        public int Id { get; set; }
        public int Diff { get; set; }
    
        public People(string pseudo, int score, BitmapImage img, string classement, DateTime dateinsc, int id, int diff)
        {
            this.Pseudo = pseudo;
            this.Score = score;
            this.Image = img;
            this.DateInsc = dateinsc;
            this.Id = id;
            this.Classement = classement;
            this.Diff = diff;
        }
    }
}