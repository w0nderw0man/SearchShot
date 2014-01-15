using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Runtime.Serialization.Json;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using FacebookConnect.Resources;

using FacebookUtils;
using Tools;

namespace FacebookConnect
{
    public partial class MainPage : PhoneApplicationPage
    {
        public static string TextToPost = "Hello 4 SIM 3 ! ";

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void SetLoggedInState(bool loggedIn)
        {
            if (loggedIn)
            {
                mTextBlockAccessToken.Text = "-";
                BtnConnect.IsEnabled = true;
                BtnDisconnect.IsEnabled = false;
                BtnPostOnWall.IsEnabled = false;
            }
            else
            {
                mTextBlockAccessToken.Text = FacebookClient.Instance.AccessToken;
                BtnConnect.IsEnabled = false;
                BtnDisconnect.IsEnabled = true;
                BtnPostOnWall.IsEnabled = true;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Update Buttons' status everytime this page is navigated to
            if ("".Equals(FacebookClient.Instance.AccessToken))
            {
                SetLoggedInState(true);
            }
            else
            {
                SetLoggedInState(false);
            }
        }

        private void BtnDisconnect_Click(object sender, RoutedEventArgs e)
        {
            // Disconnect from Facebook
            FacebookClient.Instance.AccessToken = "";
            SetLoggedInState(true);
        }

        private void BtnPostOnWall_Click(object sender, RoutedEventArgs e)
        {
            FacebookClient.Instance.PostMessageOnWall(TextToPost, new UploadStringCompletedEventHandler(PostMessageOnWallCompleted));
        }

        void PostMessageOnWallCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            if (e.Cancelled)
                return;
            if (e.Error != null)
            {
                MessageBox.Show("Error Occurred: " + e.Error.Message);
                return;
            }

            System.Diagnostics.Debug.WriteLine(e.Result);

            string result = e.Result;
            byte[] data = Encoding.UTF8.GetBytes(result);
            MemoryStream memStream = new MemoryStream(data);
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ResponseData));
            ResponseData responseData = (ResponseData)serializer.ReadObject(memStream);

            if (responseData.id != null && !responseData.id.Equals(""))
            {
                // Shared successfully
                MessageBox.Show("Message Posted!");
            }
            else if (responseData.error != null && responseData.error.code == 190)
            {
                if (responseData.error.error_subcode == 463)
                {
                    // need to get new token
                    FacebookClient.Instance.ExchangeAccessToken(new UploadStringCompletedEventHandler(ExchangeAccessTokenCompleted));
                }
                else
                {
                    // need to clear the Access Token
                    FacebookClient.Instance.AccessToken = "";
                    SetLoggedInState(false);
                }
            }
            else
            {
                // Error
            }
        }

        void ExchangeAccessTokenCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            // Acquire access_token and expires timestamp
            IEnumerable<KeyValuePair<string, string>> pairs = UriToolKits.ParseQueryString(e.Result);
            string accessToken = KeyValuePairUtils.GetValue(pairs, "access_token");

            if (accessToken != null && !accessToken.Equals(""))
            {
                MessageBox.Show("Access Token Exchange Failed");
                return;
            }

            // Save access_token
            FacebookClient.Instance.AccessToken = accessToken;
            FacebookClient.Instance.PostMessageOnWall(TextToPost, new UploadStringCompletedEventHandler(PostMessageOnWallCompleted));
        }

    }
}