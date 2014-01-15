using System;
using System.Collections.Generic;
using System.Linq;
using Hammock.Web;
using System.IO;
using Hammock;
using System.Text;
using Hammock.Authentication.OAuth;
using Microsoft.Phone.Controls;
using System.Windows;
using System.Net;

namespace TwitterPost
{
    public partial class MainPage : PhoneApplicationPage
    {
        //http://developer.nokia.com/Community/Wiki/Twitter:_OAuth_1.0_Authentication_using_Hammock
        string OAuthTokenKey = string.Empty;
        string tokenSecret = string.Empty;
        string accessToken = string.Empty;
        string accessTokenSecret = string.Empty;
        string userID = string.Empty;
        string userScreenName = string.Empty;
        Uri lasturl ;
        public MainPage()
        {
            InitializeComponent();
            if (isAlreadyLoggedIn())
            {
                userLoggedIn();
            }
            else
            {
                var requestTokenQuery = OAuthUtil.GetRequestTokenQuery();
                requestTokenQuery.RequestAsync(AppSettings.RequestTokenUri, null);
                requestTokenQuery.QueryResponse += new EventHandler<WebQueryResponseEventArgs>(requestTokenQuery_QueryResponse);
            }
           
        }

        private Boolean isAlreadyLoggedIn()
        {
            accessToken = MainUtil.GetKeyValue<string>("AccessToken");
            accessTokenSecret = MainUtil.GetKeyValue<string>("AccessTokenSecret");
            userScreenName = MainUtil.GetKeyValue<string>("ScreenName");

            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(accessTokenSecret))
                return false;
            else
                return true;
        }

        private void userLoggedIn()
        {
            Dispatcher.BeginInvoke(() =>
            {
               // var SignInMenuItem = (Microsoft.Phone.Shell.ApplicationBarMenuItem)this.ApplicationBar.MenuItems[0];
               // SignInMenuItem.IsEnabled = false;

               // var SignOutMenuItem = (Microsoft.Phone.Shell.ApplicationBarMenuItem)this.ApplicationBar.MenuItems[1];
                //SignOutMenuItem.IsEnabled = true;

                TweetPanel.Visibility = System.Windows.Visibility.Visible;
                txtUserName.Text = "Welcome " + userScreenName;
            });
        }
        private void MenuItemSignOut_Click(object sender, EventArgs e)
        {
            MainUtil.SetKeyValue<string>("AccessToken", string.Empty);
            MainUtil.SetKeyValue<string>("AccessTokenSecret", string.Empty);
            Dispatcher.BeginInvoke(() =>
            {
               // var SignInMenuItem = (Microsoft.Phone.Shell.ApplicationBarMenuItem)this.ApplicationBar.MenuItems[0];
                //SignInMenuItem.IsEnabled = true;

                //var SignOutMenuItem = (Microsoft.Phone.Shell.ApplicationBarMenuItem)this.ApplicationBar.MenuItems[1];
               // SignOutMenuItem.IsEnabled = false;

                TweetPanel.Visibility = System.Windows.Visibility.Collapsed;

                MessageBox.Show("You have been signed out successfully.");
            });
        }
        private void btnPostTweet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var credentials = new OAuthCredentials
                {
                    Type = OAuthType.ProtectedResource,
                    SignatureMethod = OAuthSignatureMethod.HmacSha1,
                    ParameterHandling = OAuthParameterHandling.HttpAuthorizationHeader,
                    ConsumerKey = AppSettings.consumerKey,
                    ConsumerSecret = AppSettings.consumerKeySecret,
                    Token = this.accessToken,
                    TokenSecret = this.accessTokenSecret,
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
                restClient.BeginRequest(restRequest, new RestCallback(PostTweetRequestCallback));
            }
            catch
            {
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
        }

        void requestTokenQuery_QueryResponse(object sender, WebQueryResponseEventArgs e)
        {
            try
            {
                StreamReader reader = new StreamReader(e.Response);
                string strResponse = reader.ReadToEnd();
                var parameters = MainUtil.GetQueryParameters(strResponse);
                OAuthTokenKey = parameters["oauth_token"];
                tokenSecret = parameters["oauth_token_secret"];
                var authorizeUrl = AppSettings.AuthorizeUri + "?oauth_token=" + OAuthTokenKey;

                Dispatcher.BeginInvoke(() =>
                {
                    this.loginBrowserControl.Navigate(new Uri(authorizeUrl, UriKind.RelativeOrAbsolute));
                });
            }
            catch (Exception ex)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    MessageBox.Show(ex.Message);
                });
            }
        }
 

        private void loginBrowserControl_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            lasturl = e.Uri;
            this.loginBrowserControl.Visibility = Visibility.Visible;
            this.loginBrowserControl.Navigated -= loginBrowserControl_Navigated;
        }

        private void loginBrowserControl_Navigating(object sender, NavigatingEventArgs e)
        {
            try
            {
                if (e.Uri.ToString().StartsWith(AppSettings.CallbackUri))
                {
                    var AuthorizeResult = MainUtil.GetQueryParameters(e.Uri.ToString());
                    var VerifyPin = AuthorizeResult["oauth_verifier"];
                    this.loginBrowserControl.Visibility = Visibility.Collapsed;
                    var AccessTokenQuery = OAuthUtil.GetAccessTokenQuery(OAuthTokenKey, tokenSecret, VerifyPin);

                    AccessTokenQuery.QueryResponse += new EventHandler<WebQueryResponseEventArgs>(AccessTokenQuery_QueryResponse);
                    AccessTokenQuery.RequestAsync(AppSettings.AccessTokenUri, null);
                }
            }
            catch
            {
            }
        }

        void AccessTokenQuery_QueryResponse(object sender, WebQueryResponseEventArgs e)
        {
            try
            {
                StreamReader reader = new StreamReader(e.Response);
                string strResponse = reader.ReadToEnd();
                var parameters = MainUtil.GetQueryParameters(strResponse);
                accessToken = parameters["oauth_token"];
                accessTokenSecret = parameters["oauth_token_secret"];
                userID = parameters["user_id"];
                userScreenName = parameters["screen_name"];

                MainUtil.SetKeyValue<string>("AccessToken", accessToken);
                MainUtil.SetKeyValue<string>("AccessTokenSecret", accessTokenSecret);
                MainUtil.SetKeyValue<string>("ScreenName", userScreenName);

                userLoggedIn();
            }
            catch (Exception ex)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    MessageBox.Show(ex.Message);
                });
            }
        }

        private void btnPostTweet_Click1(object sender, RoutedEventArgs e)
        {
           
            //this.loginBrowserControl.Visibility = Visibility.Collapsed;
            //this.loginBrowserControl.Navigate(lasturl);
            //this.loginBrowserControl.InvokeScript("eval", "history.go(-1)");
            //var requestTokenQuery = OAuthUtil.GetRequestTokenQuery();
            //requestTokenQuery.RequestAsync(AppSettings.RequestTokenUri, null);
            //requestTokenQuery.QueryResponse += new EventHandler<WebQueryResponseEventArgs>(requestTokenQuery_QueryResponse);
        }
    }
}