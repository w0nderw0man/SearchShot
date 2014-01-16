using Hammock.Authentication.OAuth;
using Hammock.Web;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;

namespace SearchShot
{
    public class TweetAppSettings
    {
        // twitter
        public static string RequestTokenUri = "https://api.twitter.com/oauth/request_token";
        public static string AuthorizeUri = "https://api.twitter.com/oauth/authorize";
        public static string AccessTokenUri = "https://api.twitter.com/oauth/access_token";
        public static string CallbackUri = "http://www.google.com";  

        public static string ConsumerKey = "DOCz5wEis3RZAJa5vY10sA";
        public static string ConsumerKeySecret = "Y8vOrxKVhv47of2TbEJIXuuqCD4hcPgtAiQWOGuGTEQ";

        public static string OAuthVersion = "1.0a";
    }
    public class TweetUtil
    {
        public static Dictionary<string, string> GetQueryParameters(string response)
        {
            var nameValueCollection = new Dictionary<string, string>();
            var items = response.Split('&');

            foreach (var nameValue in from item in items where item.Contains("=") select item.Split('='))
            {
                if (nameValue[0].Contains("?"))
                    nameValue[0] = nameValue[0].Replace("?", "");
                nameValueCollection.Add(nameValue[0], System.Net.HttpUtility.UrlDecode(nameValue[1]));
            }
            return nameValueCollection;
        }

        internal static T GetKeyValue<T>(string key)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(key))
                return (T)IsolatedStorageSettings.ApplicationSettings[key];
            return default(T);
        }

        internal static void SetKeyValue<T>(string key, T value)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(key))
                IsolatedStorageSettings.ApplicationSettings[key] = value;
            else
                IsolatedStorageSettings.ApplicationSettings.Add(key, value);
            IsolatedStorageSettings.ApplicationSettings.Save();
        }
    }
    public class OAuthUtil
    {
        internal static OAuthWebQuery GetRequestTokenQuery()
        {
            var oauth = new OAuthWorkflow
            {
                ConsumerKey = TweetAppSettings.ConsumerKey,
                ConsumerSecret = TweetAppSettings.ConsumerKeySecret,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ParameterHandling = OAuthParameterHandling.HttpAuthorizationHeader,
                RequestTokenUrl = TweetAppSettings.RequestTokenUri,
                Version = TweetAppSettings.OAuthVersion,
                CallbackUrl = TweetAppSettings.CallbackUri
            };

            var info = oauth.BuildRequestTokenInfo(WebMethod.Get);
            var objOAuthWebQuery = new OAuthWebQuery(info, false)
            {
                HasElevatedPermissions = true,
                SilverlightUserAgentHeader = "Hammock"
            };
            return objOAuthWebQuery;
        }

        internal static OAuthWebQuery GetAccessTokenQuery(string requestToken, string requestTokenSecret, string oAuthVerificationPin)
        {
            var oauth = new OAuthWorkflow
            {
                AccessTokenUrl = TweetAppSettings.AccessTokenUri,
                ConsumerKey = TweetAppSettings.ConsumerKey,
                ConsumerSecret = TweetAppSettings.ConsumerKeySecret,
                ParameterHandling = OAuthParameterHandling.HttpAuthorizationHeader,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                Token = requestToken,
                Verifier = oAuthVerificationPin,
                Version = TweetAppSettings.OAuthVersion
            };

            var info = oauth.BuildAccessTokenInfo(WebMethod.Post);
            var objOAuthWebQuery = new OAuthWebQuery(info, false)
            {
                HasElevatedPermissions = true,
                SilverlightUserAgentHeader = "Hammock"
            };
            return objOAuthWebQuery;
        }
    }

}
