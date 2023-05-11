using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.ViewModels.Command;

namespace WpfApp1.Models.Records
{
    public class BonusRecord:ViewModels.ViewModelBase
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
        private ObservableCollection<BonusRecord> _bonusRecords;
        public ObservableCollection<BonusRecord> bonusRecords
        {
            get
            {
                return _bonusRecords;
            }
            set
            {
                _bonusRecords = value;
                OnPropertyChanged("BonusRecords");
            }
        }

        private void StudentModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("BonusRecords");
        }

        private ICommand _getID;
		public ICommand GetID
        {
			get
			{
				if (_getID == null)
					_getID = new RelayCommand(param => ReturnID((Guid)param), null);

				return _getID;
			}
		}
        public void ReturnID(Guid guid)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainFrame))
                {
                    if ((window as MainFrame).Main.Content.GetType() == typeof(Bonus))
                    {
                        var tt = window as MainFrame;
                        var pageGet = tt.Main.Content as Bonus;
                        try
                        {
                            var result = pageGet.unitOfWork.OrdersRepo.AddOrders(guid, pageGet.user.id, "Ordered" + DateTime.Now.ToString("dd/MM/yy"));
                            if (result)
                            {
                                MessageBox.Show("Bonus ordered. Please wait for our call", "Succeed!", MessageBoxButton.OK);
                            }
                            else throw new Exception();
                        }
                        catch
                        {
                            MessageBox.Show("Oops");
                        }
                    }
                }
            }
        }
    }
}
