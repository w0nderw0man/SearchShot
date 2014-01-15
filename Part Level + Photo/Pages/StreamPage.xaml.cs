using SearchShot.Helpers;
using SearchShot.Models;
using SearchShot.Resources;
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

            _cameraCaptureTask.Completed += Task_Completed;

            _cameraButton = new ApplicationBarIconButton();
            _cameraButton.Text = "Prendre Photo";
            _cameraButton.IsEnabled = true;
            _cameraButton.IconUri = new Uri("/Assets/Icons/camera.png", UriKind.Relative);
            _cameraButton.Click += CameraButton_Click;

            ApplicationBar.Buttons.Add(_cameraButton);


            Loaded += StreamPage_Loaded;
            App.PhotoStreamHelper.PropertyChanged += PhotoStreamHelper_PropertyChanged;
        }


        #region Protected methods

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            App.PhotoStreamHelper.Enabled = true;

            _timer.Start();
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            _timer.Stop();

            App.PhotoStreamHelper.Enabled = false;
        }

        #endregion

        #region Private methods


        private void StreamPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (_viewModel.ListBoxItems.Count == 0)
            {
                _viewModel.RefreshPhotoStream();
            }
        }

        private void PhotoStreamHelper_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Busy")
            {
                Busy = App.PhotoStreamHelper.Busy;
            }
        }


        private void CameraButton_Click(object sender, EventArgs e)
        {
            _cameraCaptureTask.Show();
        }


        private void Task_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                if (FileHelpers.IsValidPicture(e.OriginalFileName))
                {
                    if (App.PhotoModel != null)
                    {
                        App.PhotoModel.Dispose();
                        App.PhotoModel = null;

                        GC.Collect();
                    }

                    using (MemoryStream stream = new MemoryStream())
                    {
                        e.ChosenPhoto.CopyTo(stream);

                        App.PhotoModel = new PhotoModel() { Buffer = stream.GetWindowsRuntimeBuffer() };
                        App.PhotoModel.Captured = (sender == _cameraCaptureTask);
                        App.PhotoModel.Dirty = App.PhotoModel.Captured;
                        App.PhotoModel.Path = e.OriginalFileName;
                    }

                    Dispatcher.BeginInvoke(() => NavigationService.Navigate(new Uri("/Pages/PhotoPage.xaml", UriKind.Relative)));
                }
                else
                {
                    MessageBox.Show("Malheureusement, ce format d'image n'est pas pris en chage par l'application.",
                        "Format non supporté.", MessageBoxButton.OK);
                }
            }
        }

        private T GetVisualChild<T>(UIElement parent) where T : UIElement
        {
            T child = null;

            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < numVisuals; i++)
            {
                UIElement element = (UIElement)VisualTreeHelper.GetChild(parent, i);

                child = element as T;

                if (child == null)
                {
                    child = GetVisualChild<T>(element);
                }

                if (child != null)
                {
                    break;
                }
            }

            return child;
        }

        private List<T> GetVisualChildren<T>(UIElement parent) where T : UIElement
        {
            List<T> children = new List<T>();

            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < numVisuals; i++)
            {
                UIElement element = (UIElement)VisualTreeHelper.GetChild(parent, i);

                T child = element as T;

                if (child != null)
                {
                    children.Add(child);
                }
                else
                {
                    foreach (T s in GetVisualChildren<T>(element))
                    {
                        children.Add(s);
                    }
                }
            }

            return children;
        }

        #endregion
    }
}