using System.ComponentModel;

namespace SearchShot.Models
{
    public class StreamItemModel : INotifyPropertyChanged
    {
        #region Members

        private Microsoft.Xna.Framework.Media.Picture _picture;
        private FilterModel _filter;

        #endregion

        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;

        public FilterModel Filter
        {
            get
            {
                return _filter;
            }

            private set
            {
                if (_filter != value)
                {
                    _filter = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Filter"));
                    }
                }
            }
        }

        public Microsoft.Xna.Framework.Media.Picture Picture
        {
            get
            {
                return _picture;
            }

            set
            {
                if (_picture != value)
                {
                    _picture = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Picture"));
                    }
                }
            }
        }

        #endregion

    }
}