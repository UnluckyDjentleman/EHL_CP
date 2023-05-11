using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.ViewModels.Command;

namespace WpfApp1.Models.Records
{
    public class ArticleRecord:ViewModels.ViewModelBase
    {
        private Guid _id;
        public Guid id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged("id");
            }
        }
        private string _header;
        public string Header
        {
            get
            {
                return _header;
            }
            set
            {
                _header = value;
                OnPropertyChanged("Header");
            }
        }
        private string _image;
        public string Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }
        #region Image
        private string _imagePath;
        public string ImagePath
        {
            get
            {
                return _imagePath;
            }
            set
            {
                if (!(value is string))
                    return;

                _imagePath = value.ToString();
                OnPropertyChanged("ImagePath");
            }
        }
        #endregion
        #region CommandForPhoto
        private readonly RelayCommand _openFileDialog;
        public RelayCommand ChoosePhoto
        {
            get
            {
                return _openFileDialog ?? (new RelayCommand(obj =>
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Multiselect = false;
                    ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.jfif";
                    ofd.ShowDialog();
                    ImagePath = ofd.FileName;
                }));
            }
        }
        #endregion
        private DateTime _datetime;
        public DateTime Creation_Date
        {
            get
            {
                return _datetime;
            }
            set
            {
                _datetime = value;
                OnPropertyChanged("Creation_Date");
            }
        }
        private ObservableCollection<ArticleRecord> _mangaRecords;
        public ObservableCollection<ArticleRecord> mangaRecords
        {
            get
            {
                return _mangaRecords;
            }
            set
            {
                _mangaRecords = value;
                OnPropertyChanged("ArticleRecords");
            }
        }

        private void StudentModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("ArticleRecords");
        }
    }
}
