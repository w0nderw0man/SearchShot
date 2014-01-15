using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SearchShot.ServiceReference1;


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
            Service1Client ws = new Service1Client();
            ws.GetPeopleInfosCompleted += GetPeople;
            ws.GetPeopleInfosAsync();
        }

        private void GetPeople(object sender, GetPeopleInfosCompletedEventArgs e)
        {
            List<People> peopleList = new List<People>();
            for (int i =0; i< e.Result.id.Count ; i++)
            {
                int id = (int)e.Result.id.ToArray().GetValue(i);
                int score = (int) e.Result.score.ToArray().GetValue(i);
                DateTime dateinsc = (DateTime) e.Result.date_insc.ToArray().GetValue(i);
                string login = (string) e.Result.login.ToArray().GetValue(i);
                Image image = (Image)e.Result.img.ToArray().GetValue(i);
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
                peopleList.Add(new People(login, score, new Image(), classement, dateinsc, id));
            }
            GeneralList.ItemsSource = peopleList;


        }

        private void Pivot_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }

    public class People
    {
        public string Pseudo { get; set; }
        public int Score { get; set; }
        public System.Windows.Controls.Image Image { get; set; }
        public DateTime DateInsc { get; set; }
        public string Classement { get; set; }
        public int Id { get; set; }

        public People(string pseudo, int score, Image img, string classement, DateTime dateinsc, int id)
        {
            this.Pseudo = pseudo;
            this.Score = score;
            this.Image = img;
            this.DateInsc = dateinsc;
            this.Id = id;
            this.Classement = classement;
        }
    }
}