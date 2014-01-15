using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SearchShot.ViewModels
{
    public class StreamPageViewModel
    {
        #region Private member

        private PictureCollection _pictures;
        private IEnumerator<Picture> _enumerator;
        private int _photoSetSize = Int32.MaxValue; // 3 * PhotoMosaicViewModel.MaxItems; // amount of items in a single set (sets are separated with "more")

        #endregion

        #region Properties

        public ObservableCollection<object> ListBoxItems { get; set; }

        #endregion

        public StreamPageViewModel()
        {
            ListBoxItems = new ObservableCollection<object>();
        }

        ~StreamPageViewModel()
        {
            ClearPhotoStream();
        }

        public void RefreshPhotoStream()
        {
            ClearPhotoStream();

            using (MediaLibrary library = new MediaLibrary())
            {
                foreach (PictureAlbum album in library.RootPictureAlbum.Albums)
                {
                    if (album.Name == "Camera Roll")
                    {
                        _pictures = album.Pictures;
                        _enumerator = _pictures.Reverse().GetEnumerator();
                        break;
                    }
                }
            }
        }

        public void ClearPhotoStream()
        {
            App.PhotoStreamHelper.RemoveAll();

            if (_pictures != null)
            {
                _pictures.Dispose();
                _pictures = null;
            }

            if (_enumerator != null)
            {
                _enumerator.Dispose();
                _enumerator = null;
            }

            try
            {
                ListBoxItems.Clear();
            }
            catch (Exception)
            {
            }

            GC.Collect();
        }

    }
}