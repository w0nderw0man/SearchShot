using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Threading.Tasks;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using FacebookUtils;
using Tools;

namespace FacebookConnect
{
    public partial class ConnectPage : PhoneApplicationPage
    {
        public ConnectPage()
        {
            InitializeComponent();

            // remove current logged in user data
            mWebBrowser.ClearCookiesAsync();

            // Go to Login url
            mWebBrowser.Source = new Uri(FacebookClient.Instance.GetLoginUrl());
        }

        private void WebBrowser_Navigated(object sender, NavigationEventArgs e)
        {
            String uri = e.Uri.ToString();

            if (uri.StartsWith("https://www.facebook.com/connect/login_success.html") || uri.StartsWith("http://www.facebook.com/connect/login_success.html"))
            {
                // Remove junk text added by facebook from url
                if (uri.EndsWith("#_=_"))
                    uri = uri.Substring(0, uri.Length - 4);

                String queryString = e.Uri.Query.ToString();

                // Acquire the code from Query String
                IEnumerable<KeyValuePair<string, string>> pairs = UriToolKits.ParseQueryString(queryString);
                string code = KeyValuePairUtils.GetValue(pairs, "code");

                // Get access_token from code using Asynchronous HTTP Request
                WebClient client = new WebClient();
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(AccessTokenDownloadCompleted);
                client.DownloadStringAsync(new Uri(FacebookClient.Instance.GetAccessTokenRequestUrl(code)));
            }
        }

        void AccessTokenDownloadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            string data = e.Result;
            data = "?" + data;

            // Acquire access_token and expires timestamp
            IEnumerable<KeyValuePair<string, string>> pairs = UriToolKits.ParseQueryString(data);
            string accessToken = KeyValuePairUtils.GetValue(pairs, "access_token");
            string expires = KeyValuePairUtils.GetValue(pairs, "expires");

            // Save access_token
            FacebookClient.Instance.AccessToken = accessToken;

            // Navigate to MainPage
            var rootFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            if (rootFrame != null)
              rootFrame.GoBack();
        }
    }
}