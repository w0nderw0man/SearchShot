using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace SearchShot.Controls
{
    public partial class PhotoThumbnail : INotifyPropertyChanged
    {
        private WriteableBitmap _bitmap;
        private string _text;

        public WriteableBitmap Bitmap
        {
            get
            {
                return _bitmap;
            }

            set
            {
                if (_bitmap != value)
                {
                    _bitmap = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Bitmap"));
                    }
                }
            }
        }

        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
                if (_text != value)
                {
                    _text = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Text"));
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public PhotoThumbnail()
        {
            InitializeComponent();

            DataContext = this;
        }
    }
}
