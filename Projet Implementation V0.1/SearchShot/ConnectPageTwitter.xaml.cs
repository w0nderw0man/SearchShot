using System;
using System.Windows.Navigation;
using Hammock.Web;
using System.IO;
using Hammock;
using Hammock.Authentication.OAuth;
using Microsoft.Phone.Controls;
using System.Windows;
using System.Net;

namespace SearchShot

{
    public partial class ConnectPageTwitter
    {
        private string _oAuthTokenKey = string.Empty;
        private string _tokenSecret = string.Empty;
        private string _accessToken = string.Empty;
        private string _accessTokenSecret = string.Empty;
        private string _userId = string.Empty;
        public string UserScreenName = string.Empty;
        private Uri _lasturl;

        public ConnectPageTwitter()
        {
            InitializeComponent();
            mWebBrowser.ClearCookiesAsync();
            if (IsAlreadyLoggedIn())
            {
                UserLoggedIn();
            }
            else
            {
                var requestTokenQuery = OAuthUtil.GetRequestTokenQuery();
                requestTokenQuery.RequestAsync(TweetAppSettings.RequestTokenUri, null);
                requestTokenQuery.QueryResponse += requestTokenQuery_QueryResponse;
            }
        }

        private Boolean IsAlreadyLoggedIn()
        {
            _accessToken = TweetUtil.GetKeyValue<string>("AccessToken");
            _accessTokenSecret = TweetUtil.GetKeyValue<string>("AccessTokenSecret");
            UserScreenName = TweetUtil.GetKeyValue<string>("ScreenName");
            ConnectionMode.Name = UserScreenName;
            if (string.IsNullOrEmpty(_accessToken) || string.IsNullOrEmpty(_accessTokenSecret))
                return false;
            return true;
        }

        private void UserLoggedIn()
        {
            ConnectionMode.ConnectWith = "Twitter";
            ConnectionMode.Token = _accessToken;
            Dispatcher.BeginInvoke(() =>
            {
                // var SignInMenuItem = (Microsoft.Phone.Shell.ApplicationBarMenuItem)this.ApplicationBar.MenuItems[0];
                // SignInMenuItem.IsEnabled = false;

                // var SignOutMenuItem = (Microsoft.Phone.Shell.ApplicationBarMenuItem)this.ApplicationBar.MenuItems[1];
                //SignOutMenuItem.IsEnabled = true;

                // TweetPanel.Visibility = Visibility.Visible;
                // txtUserName.Text = "Welcome " + _userScreenName;
                //Ajout infos
                ConnectionMode.ConnectWith = "Twitter";
                ConnectionMode.Token = _accessToken;
                UserScreenName = TweetUtil.GetKeyValue<string>("ScreenName");
                ConnectionMode.Name = UserScreenName;
                NavigationService.Navigate(new Uri("/Accueil.xaml", UriKind.Relative));
            });
        }

        private void MenuItemSignOut_Click(object sender, EventArgs e)
        {
            TweetUtil.SetKeyValue("AccessToken", string.Empty);
            TweetUtil.SetKeyValue("AccessTokenSecret", string.Empty);
            Dispatcher.BeginInvoke(() =>
            {
                // var SignInMenuItem = (Microsoft.Phone.Shell.ApplicationBarMenuItem)this.ApplicationBar.MenuItems[0];
                //SignInMenuItem.IsEnabled = true;

                //var SignOutMenuItem = (Microsoft.Phone.Shell.ApplicationBarMenuItem)this.ApplicationBar.MenuItems[1];
                // SignOutMenuItem.IsEnabled = false;

                //TweetPanel.Visibility = Visibility.Collapsed;

                MessageBox.Show("You have been signed out successfully.");
            });
        }
    

        /*private void btnPostTweet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var credentials = new OAuthCredentials
                {
                    Type = OAuthType.ProtectedResource,
                    SignatureMethod = OAuthSignatureMethod.HmacSha1,
                    ParameterHandling = OAuthParameterHandling.HttpAuthorizationHeader,
                    ConsumerKey = TweetAppSettings.ConsumerKey,
                    ConsumerSecret = TweetAppSettings.ConsumerKeySecret,
                    Token = _accessToken,
                    TokenSecret = _accessTokenSecret,
                    Version = "1.0"
                };

                var restClient = new RestClient
                {
                    Authority = "https://api.twitter.com",
                    HasElevatedPermissions = true
                };

                var restRequest = new RestRequest
                {
                    Credentials = credentials,
                    Path = "/1.1/statuses/update.json",
                    Method = WebMethod.Post
                };

                restRequest.AddParameter("status", txtTweetContent.Text);
                restClient.BeginRequest(restRequest, PostTweetRequestCallback);
            }
            catch (Exception ex)
            {
                Dispatcher.BeginInvoke(() => MessageBox.Show(ex.Message));
            }
        }

        private void PostTweetRequestCallback(RestRequest request, RestResponse response, object obj)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    MessageBox.Show("TWEET_POSTED_SUCCESSFULLY");
                }
                else if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    MessageBox.Show("TWEET_POST_ERR_UPDATE_LIMIT");
                }
                else
                {
                    MessageBox.Show("WEET_POST_ERR_FAILED");
                }
                txtTweetContent.Text = "";
            });
            //var requestTokenQuery = OAuthUtil.GetRequestTokenQuery();
            //requestTokenQuery.RequestAsync(AppSettings.RequestTokenUri, null);
            //requestTokenQuery.QueryResponse += new EventHandler<WebQueryResponseEventArgs>(requestTokenQuery_QueryResponse);
        } */

        void requestTokenQuery_QueryResponse(object sender, WebQueryResponseEventArgs e)
        {
            try
            {
                var reader = new StreamReader(e.Response);
                var strResponse = reader.ReadToEnd();
                var parameters = TweetUtil.GetQueryParameters(strResponse);
                _oAuthTokenKey = parameters["oauth_token"];
                _tokenSecret = parameters["oauth_token_secret"];
                var authorizeUrl = TweetAppSettings.AuthorizeUri + "?oauth_token=" + _oAuthTokenKey;

                Dispatcher.BeginInvoke(() => mWebBrowser.Navigate(new Uri(authorizeUrl, UriKind.RelativeOrAbsolute)));
            }
            catch (Exception ex)
            {
                Dispatcher.BeginInvoke(() => MessageBox.Show(ex.Message));
            }
        }


        private void mWebBrowser_Navigated(object sender, NavigationEventArgs e)
        {
            _lasturl = e.Uri;
            mWebBrowser.Visibility = Visibility.Visible;
            mWebBrowser.Navigated -= mWebBrowser_Navigated;
        }

        private void mWebBrowser_Navigating(object sender, NavigatingEventArgs e)
        {
            try
            {
                if (!e.Uri.ToString().StartsWith(TweetAppSettings.CallbackUri)) return;
                var authorizeResult = TweetUtil.GetQueryParameters(e.Uri.ToString());
                var verifyPin = authorizeResult["oauth_verifier"];
                mWebBrowser.Visibility = Visibility.Collapsed;
                var accessTokenQuery = OAuthUtil.GetAccessTokenQuery(_oAuthTokenKey, _tokenSecret, verifyPin);

                accessTokenQuery.QueryResponse += AccessTokenQuery_QueryResponse;
                accessTokenQuery.RequestAsync(TweetAppSettings.AccessTokenUri, null);
            }
            catch (Exception ex)
            {
                Dispatcher.BeginInvoke(() => MessageBox.Show(ex.Message));
            }
        }

        void AccessTokenQuery_QueryResponse(object sender, WebQueryResponseEventArgs e)
        {
            try
            {
                var reader = new StreamReader(e.Response);
                string strResponse = reader.ReadToEnd();
                var parameters = TweetUtil.GetQueryParameters(strResponse);
                _accessToken = parameters["oauth_token"];
                _accessTokenSecret = parameters["oauth_token_secret"];
                _userId = parameters["user_id"];
                UserScreenName = parameters["screen_name"];

                TweetUtil.SetKeyValue("AccessToken", _accessToken);
                TweetUtil.SetKeyValue("AccessTokenSecret", _accessTokenSecret);
                TweetUtil.SetKeyValue("ScreenName", UserScreenName);

                UserLoggedIn();
            }
            catch (Exception ex)
            {
                Dispatcher.BeginInvoke(() => MessageBox.Show(ex.Message));
            }
        }
    }
}
