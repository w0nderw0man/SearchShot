using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Hammock;
using Hammock.Authentication.OAuth;
using Hammock.Web;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SearchShot.FacebookUtils;
using SearchShot.Tools;

namespace SearchShot
{
    public partial class Post : PhoneApplicationPage
    {
        private string _accessTokenSecret;
        private string _accessToken;

        public Post()
        {
            InitializeComponent();
        }

        private void BtnPost_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectionMode.ConnectWith.Equals("Twitter"))
            {
                PostTweet(sender, e);
            }
            else if (ConnectionMode.ConnectWith.Equals("Facebook"))
            {
                PostFacebook(sender, e);
            }
        }

        private void PostTweet(object sender, RoutedEventArgs e)
        {
            _accessToken = TweetUtil.GetKeyValue<string>("AccessToken");
            _accessTokenSecret = TweetUtil.GetKeyValue<string>("AccessTokenSecret");
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

                restRequest.AddParameter("status", "Test");
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
            });
            //var requestTokenQuery = OAuthUtil.GetRequestTokenQuery();
            //requestTokenQuery.RequestAsync(AppSettings.RequestTokenUri, null);
            //requestTokenQuery.QueryResponse += new EventHandler<WebQueryResponseEventArgs>(requestTokenQuery_QueryResponse);
        }

        private void PostFacebook(object sender, RoutedEventArgs e)
        {
            FacebookClient.Instance.PostMessageOnWall("Test", new UploadStringCompletedEventHandler(PostMessageOnWallCompleted));
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
                    FacebookClient.Instance.ExchangeAccessToken(new UploadStringCompletedEventHandler(ExchangeAccessTokenCompleted));
                }
                else
                {
                    FacebookClient.Instance.AccessToken = "";
                }
            }
            else
            {
                //error
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
            FacebookClient.Instance.PostMessageOnWall("Test", new UploadStringCompletedEventHandler(PostMessageOnWallCompleted));
        }
    }
}