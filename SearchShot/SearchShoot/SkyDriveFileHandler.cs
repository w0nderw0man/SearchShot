using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Microsoft.Live;
using System.IO.IsolatedStorage;
using System.IO;

namespace SearchShot
{
    public class SkyDriveFileHandler
    {
        private string _folderId;
        private readonly string _clientId; 
        private readonly string _folderName;
        public LiveConnectClient LiveClient { get; set; }
        public LiveAuthClient LiveAuth { get; set; }
        public LiveLoginResult LiveResult { get; set; }
        public LiveConnectSession LiveSession { get; set; }
        private readonly string[] _requiredScopes;

        public SkyDriveFileHandler(LiveConnectSession session, string clientID, string folder, string[] scopes = null)
        {
            _clientId = clientID;
            LiveSession = session;
            _folderName = folder;

            if (scopes == null)
            {
                _requiredScopes = new[]
                {"wl.basic", "wl.skydrive", "wl.offline_access", "wl.signin", "wl.skydrive_update"};
            }

           //GetSkyDriveFolder();
        }



        private async Task SignIn()
        {
            if (LiveSession == null)
            {
                LiveAuth = new LiveAuthClient(_clientId);
                LiveResult = await LiveAuth.InitializeAsync(_requiredScopes);

                if (LiveResult.Status != LiveConnectSessionStatus.Connected)
                {
                    LiveResult = await LiveAuth.LoginAsync(_requiredScopes);
                }

                LiveSession = LiveResult.Session;
                LiveClient = new LiveConnectClient(LiveSession);

            }
        }

        private async Task GetSkyDriveFolder()
        {
            if (LiveClient == null)
                await SignIn();

            if (LiveClient != null)
            {
                var result = await LiveClient.GetAsync("me/skydrive/files/");
                var data = (List<object>) result.Result["data"];
                foreach (IDictionary<string, object> content in data.Cast<IDictionary<string, object>>().Where(content => content["name"].ToString() == _folderName))
                {
                    _folderId = content["id"].ToString();
                }
            }
            if (_folderId == null)
                _folderId = await CreateSkydriveFolder();
        }

        private async Task<string> CreateSkydriveFolder()
        {
            var folderData = new Dictionary<string, object> {{"name", _folderName}};
            var operationResult =
                await LiveClient.PostAsync("me/skydrive", folderData);
            dynamic result = operationResult.Result;
            string id = string.Format("{0}", result.id);
            return id;
        }

        public async Task<OperationStatus> UploadFile(string fileName)
        {
            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var fileStream = store.OpenFile(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    try
                    {
                        if (LiveClient == null)
                            await GetSkyDriveFolder();

                        if (LiveClient != null)
                        {
                            await LiveClient.UploadAsync(_folderId,
                                fileName,
                                fileStream,
                                OverwriteOption.Overwrite
                                );
                        }
                        return OperationStatus.Completed;
                    }
                    catch
                    {
                        return OperationStatus.Failed;
                    }
                }
            }
        }

        public enum OperationStatus
        {
            Completed,
            Failed
        }
    }
}
