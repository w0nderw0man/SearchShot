using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Facebook.Client;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SearchShot.FacebookUtils;
using SearchShot.Tools;
using Facebook;


namespace SearchShot
{
    public partial class ConnectPageFacebook
    {
        public ConnectPageFacebook()
        {
            InitializeComponent();
            mWebBrowser.ClearCookiesAsync();
            mWebBrowser.Source = new Uri(FacebookUtils.FacebookClient.Instance.GetLoginUrl());

        }

    private void WebBrowser_Navigated(object sender, NavigationEventArgs e)
        {
            String uri = e.Uri.ToString();

            if (uri.StartsWith("https://www.facebook.com/connect/login_success.html") || uri.StartsWith("http://www.facebook.com/connect/login_success.html"))
            {
                if (uri.EndsWith("#_=_"))
                    uri.Substring(0, uri.Length - 4);

                var queryString = e.Uri.Query.ToString();

                var pairs = queryString.ParseQueryString();
                var code = pairs.GetValue("code");

                var client = new WebClient();
                client.DownloadStringCompleted += AccessTokenDownloadCompleted;
                client.DownloadStringAsync(new Uri(FacebookUtils.FacebookClient.Instance.GetAccessTokenRequestUrl(code)));
            }
        }

        void AccessTokenDownloadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            var data = e.Result;
            data = "?" + data;

            var pairs = data.ParseQueryString();
            var keyValuePairs = pairs as KeyValuePair<string, string>[] ?? pairs.ToArray();
            var accessToken = keyValuePairs.GetValue("access_token");
            var expires = keyValuePairs.GetValue("expires");

            // Ajout infos
            FacebookUtils.FacebookClient.Instance.AccessToken = accessToken;
            ConnectionMode.ConnectWith = "Facebook";
            ConnectionMode.Token = FacebookUtils.FacebookClient.Instance.AccessToken;

            // Retour debut
            //var rootFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            //if (rootFrame != null)
            //rootFrame.GoBack();
            NavigationService.Navigate(new Uri("/Accueil.xaml", UriKind.Relative));
        }

        

    }
}