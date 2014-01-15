using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SkyDrive.Resources;
using Microsoft.Live;
using Microsoft.Live.Controls;
namespace SkyDrive
{
    public partial class MainPage : PhoneApplicationPage
    {
        private LiveConnectClient client;
        // Constructeur
        public MainPage()
        {
            InitializeComponent(); 
            CreateFileIntoIsolatedStorage();
        }

        private LiveConnectClient client;
        private void skydrive_SessionChanged(object sender, LiveConnectSessionChangedEventArgs e) 
        {
            if (e != null && e.Status == LiveConnectSessionStatus.Connected)
            {
                this.client = new LiveConnectClient(e.Session);
                this.GetAccountInformations();
            }
            else
            {
                this.client = null; InfoText.Text = e.Error != null ? e.Error.ToString() : string.Empty;
            } 
        }

        private async void GetAccountInformations()
        {
            try
            {
                LiveOperationResult operationResult = await this.client.GetAsync("me");
                var jsonResult = operationResult.Result as dynamic;
                string firstName = jsonResult.first_name ?? string.Empty;
                string lastName = jsonResult.last_name ?? string.Empty;
                InfoText.Text = "Welcome " + firstName + " " + lastName;
            }
            catch (Exception e)
            {
                InfoText.Text = e.ToString();
            }
        }
        private string fileName = "sample.dat";
        private IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();

        private void CreateFileIntoIsolatedStorage()
        {
            if (isf.FileExists(fileName))
            {
                isf.DeleteFile(fileName);
            }
            IsolatedStorageFileStream isfStream = new IsolatedStorageFileStream(fileName, FileMode.Create, IsolatedStorageFile.GetUserStoreForApplication());
            byte[] output = new byte[25];
            for (int i = 0; i < 25; i++)
            {
                output[i] = (byte)(i);
            } 
            isfStream.Write(output, 0, output.Length); isfStream.Close();
        }

        private async void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var fileStream = store.OpenFile(strSaveName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    try
                    {
                        LiveOperationResult res =
                            await
                                client.BackgroundUploadAsync("me/skydrive",
                                    new Uri("/shared/transfers/" + fileName, UriKind.Relative),
                                    OverwriteOption.Overwrite);
                        InfoText.Text = "File " + fileName + " uploaded";
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
            }
        }
    }
}