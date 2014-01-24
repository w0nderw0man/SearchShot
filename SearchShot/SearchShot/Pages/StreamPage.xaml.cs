using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using SearchShot.Helpers;
using SearchShot.Models;
using SearchShot.Resources;
using SearchShot.ServiceReference1;
using SearchShot.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace SearchShot
{
    public partial class StreamPage : PhoneApplicationPage
    {
        #region Members

        private StreamPageViewModel _viewModel = new StreamPageViewModel();

        private CameraCaptureTask _cameraCaptureTask = new CameraCaptureTask();

        private ApplicationBarIconButton _cameraButton;

        private DispatcherTimer _timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 1000) };


        private bool _busy;

        #endregion
        private int Description { get; set; }

        public List<Niveau> levelTab = new List<Niveau>();
        List<ItemsPourBinding> items = new List<ItemsPourBinding>();

        #region Properties

        private bool Busy
        {
            get
            {
                return _busy;
            }

            set
            {
                if (_busy != value)
                {
                    _busy = value;

                    ProgressBar.IsIndeterminate = _busy;
                    ProgressBar.Visibility = _busy ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        #endregion

        public StreamPage()
        {

            DataContext = _viewModel;

            InitializeComponent();
            /*
            _cameraCaptureTask.Completed += Task_Completed;

            _cameraButton = new ApplicationBarIconButton();
            _cameraButton.Text = "Prendre Photo";
            _cameraButton.IsEnabled = true;
            _cameraButton.IconUri = new Uri("/Assets/Icons/camera.png", UriKind.Relative);
            _cameraButton.Click += CameraButton_Click;

            ApplicationBar.Buttons.Add(_cameraButton);


            Loaded += StreamPage_Loaded;
            App.PhotoStreamHelper.PropertyChanged += PhotoStreamHelper_PropertyChanged;
            */

            //définition du nombre de niveaux de l'application
            demandeList();
            int nombreDeNiveaux = levelTab.Count;
            MessageBox.Show("info: " + items.Count);
            

            //MessageBox.Show("info: " + levelTab.ElementAt(2).description);


            /*for (int i = 0; i < nombreDeNiveaux-140; i++)
            {

                items.Add(new ItemsPourBinding { Description = levelTab.ElementAt(i).description, Id = levelTab[i].ID, Image = OtientImage()});

            }*/



        }



        public void testRetour2(object sender, listNiveauxCompletedEventArgs listeNiveau)
        {
            if (listeNiveau.Result != null)
            {

                levelTab = listeNiveau.Result;
                for (int i = 0; i < levelTab.Count; i++)
                {

                    items.Add(new ItemsPourBinding
                    {
                        Description = levelTab.ElementAt(i).description,
                        IdNiveau = levelTab[i].ID,
                        Image = OtientImage(i),
                        Avatar = ObtientAvatar(i),
                        Speudo = ObtientSpeudo(i)
                    });

                }
                listedesniveaux.ItemsSource =
                    items.Select(e => new ItemsPourBinding() { Description = e.Description, IdNiveau = e.IdNiveau, Image = e.Image, Avatar = e.Avatar, Speudo = e.Speudo});
                
                //récupération du niveau en cours du joueur
                int niveaujoueur = 43;
                ItemsPourBinding item = listedesniveaux.Items.OfType<ItemsPourBinding>().Single(it => it.IdNiveau == niveaujoueur);
         

            }
            else
            {
                MessageBox.Show("erreur webservice");
            }
        }


        //demande au web service
        public void demandeList()
        {

            Service1Client s = new Service1Client();
            s.listNiveauxCompleted += testRetour2;
            s.listNiveauxAsync(6);
        }


        //déclenchement de l'évenement quand un niveau est sélectionné
        private void listedesniveaux_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                string text = "Aller au niveau " + (e.AddedItems[0] as ItemsPourBinding).IdNiveau.ToString() + " ?";
                if (MessageBox.Show("Valider ?", text, MessageBoxButton.OKCancel).Equals(MessageBoxResult.OK))
                {
                    if (e.AddedItems.Count > 0)
                    {
                        MessageBox.Show("Vous avez selectionner le niveau : " +
                                        (e.AddedItems[0] as ItemsPourBinding).IdNiveau);
                        //insertion de la récupération du niveau cliqué
                    }
                }
                else
                {
                    listedesniveaux.SelectedIndex = -1;
                }
            }
        }

        private void Avatar_OnMouseEnter(object sender, GestureEventArgs gestureEventArgs)
        {
                MessageBox.Show("Vous avez selectionné un avatar d'un ami");
                //compter le nombre d'amis sur le niveau
            int nombreAmis = 10;

            if (nombreAmis > 1)
            {
                //redirection vers une page d'affichage des amis
                NavigationService.Navigate(new Uri("/Pages/AmisDuNiveau.xaml", UriKind.Relative));

            }
            else
            {
                MessageBox.Show("Vous n'avez qu'un seul amis sur ce niveau");
            }



        }

        //permet de charger l'image par défaut ou l'historique d'image prise par niveau
        private static BitmapImage OtientImage(int i)
        {
            //récupération des images du niveau

            //si pas d'historique photo
            return new BitmapImage(new Uri("/Assets/Graphics/pelliculetransparenteFINAL.png", UriKind.Relative));

            //si un historique photo
            //return new BitmapImage(new Uri("/Assets/Graphics/pelliculetransparenteFINAL.png", UriKind.Relative));
        }

        //permet de récupérer les avatars des amis correspondant à un niveau
        private static BitmapImage ObtientAvatar(int i)
        {
            //si pas d'amis sur le niveau

            //si amis sur le niveau
            return new BitmapImage(new Uri("/Assets/Graphics/testAvatar.jpg", UriKind.Relative));
        }

        private static String ObtientSpeudo(int i)
        {
            //si pas d'amis sur le niveau

            //si amis sur le niveau
            return "Utilisateur amis";
        }
    }

    //classe pour le binding
    public class ItemsPourBinding
    {
        public BitmapImage Image { get; set; }
        public string Description { get; set; }
        public int IdNiveau { get; set; }
        public BitmapImage Avatar { get; set; }
        public string Speudo { get; set; }
    }
}
