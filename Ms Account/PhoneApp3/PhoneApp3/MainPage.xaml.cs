using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Live;
using Microsoft.Live.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhoneApp3.Resources;

namespace PhoneApp3
{
    public partial class MainPage : PhoneApplicationPage
    {
        private LiveConnectClient liveClient;

        public MainPage()
        {
            InitializeComponent();
            PhotoList.ItemsSource = new Photos;
            liveSignIn.Scopes = "wl.signin wl.photos wl.skydrive";
            liveSignIn.Scopes = "wl.signin wl.basic wl.offline_access";
            liveSignIn.ClientId = "<< CLIENT ID >>";
        }

        private void liveSignIn_SessionChanged(object sender, LiveConnectSessionChangedEventArgs e)
        {
            if (e.Session != null)
            {
                liveClient = new LiveConnectClient(e.Session);
                liveClient.GetCompleted += OnGetLiveData;
                liveClient.GetAsync("me", null);
            }
            else
            {
                liveClient = null;
                liveResult.Text = e.Error != null ? e.Error.ToString() : string.Empty;
            }
        }
        private void OnGetLiveData(object sender, LiveOperationCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                liveResult.Text = e.Error.ToString();
                return;
            }
            else
            {
                object name;
                if (e.Result.TryGetValue("name", out name))
                {
                    liveResult.Text = name.ToString();
                }
                else
                {
                    liveResult.Text = "name not found";
                }
            }
        }
    }
    public class SkyDrivePhoto
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string albumName { get; set; }
        private LiveConnectClient liveClient;
        private LiveConnectSession liveSession;
        private ObservableCollection<SkyDrivePhoto> photos =
        new ObservableCollection<SkyDrivePhoto>();
        public ObservableCollection<SkyDrivePhoto> Photos
        {
            get { return photos; }
            private set { }
        }
        private void liveSignIn_SessionChanged(object sender, LiveConnectSessionChangedEventArgs e)
        {
            if (e.Session != null)
            {
                liveSession = e.Session;
                liveClient = new LiveConnectClient(e.Session);
                liveClient.GetCompleted += OnGetLiveData;
                liveClient.GetAsync("me/albums", null);
            }
            else
            {
                liveClient = null;
                albumName.Text = String.Empty;
                photos.Clear();
            }
        }
        private void OnGetLiveData(object sender, LiveOperationCompletedEventArgs e)
        {
            if (e.Error != null)
                return;
            if (!e.Result.ContainsKey("data"))
                return;
            List<object> data = (List<object>)e.Result["data"];
            IDictionary<string, object> album = (IDictionary<string, object>)data[0];
            albumName.Text = (string)album["name"];
            String albumId = (string)album["id"];
            LiveConnectClient albumClient = new LiveConnectClient(liveSession);
            albumClient.GetCompleted +=
            new EventHandler<LiveOperationCompletedEventArgs>(
            albumClient_GetCompleted);
            albumClient.GetAsync(albumId + "/files", null);
        }
        private void albumClient_GetCompleted(object sender, LiveOperationCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                List<object> data = (List<object>)e.Result["data"];
                foreach (IDictionary<string, object> item in data)
                {
                    SkyDrivePhoto photo = new SkyDrivePhoto();
                    photo.Title = (string)item["name"];
                    photo.Url = (string)item["source"];
                    photos.Add(photo);
                }
            }
        }
    }
}