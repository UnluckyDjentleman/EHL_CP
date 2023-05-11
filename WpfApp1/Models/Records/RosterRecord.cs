using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Models.DBModels;
using WpfApp1.Models.Repos;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Command;

namespace WpfApp1.Models.Records
{
    public class RosterRecord:ViewModelBase
    {
        private int _teamid;
        public int teamID
        {
            get
            {
                return _teamid;
            }
            set
            {
                _teamid = value;
            }
        }
        private Guid _playerId;
        public Guid PlayerId
        {
            get
            {
                return _playerId;
            }
            set
            {
                _playerId = value;
            }
        }
        private string _photo;
        public string Photo
        {
            get
            {
                return _photo;
            }
            set
            {
                _photo = value;
                OnPropertyChanged("Image");
            }
        }
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Image");
            }
        }
        private string _positionID;
        public string PositionID
        {
            get
            {
                return _positionID;
            }
            set
            {
                _positionID = value;
                OnPropertyChanged("PositionID");
            }
        }
        private int _number;
        public int Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
                OnPropertyChanged("Number");
            }
        }
        private string _country;
        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
                OnPropertyChanged("Country");
            }
        }
        private int _weight;
        public int Weight
        {
            get
            {
                return _weight;
            }
            set
            {
                _weight = value;
                OnPropertyChanged("Weight");
            }
        }
        private int _height;
        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                OnPropertyChanged("Height");
            }
        }
        private ObservableCollection<RosterRecord> _playersRecords;
        public ObservableCollection<RosterRecord> PlayersRecords
        {
            get
            {
                return _playersRecords;
            }
            set
            {
                _playersRecords = value;
                OnPropertyChanged("RosterRecord");
            }
        }
        private void EHLModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("RosterRecord");
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
                    ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.jfif;";
                    ofd.ShowDialog();
                    ImagePath = ofd.FileName;
                }));
            }
        }
        #endregion
        private PlayersRepo _repository = new PlayersRepo();
        public PLAYERS playerEntity = new PLAYERS();
        private ICommand _editCommand;
        private ICommand _deleteCommand;
        public RosterRecord GetRecord(Guid id)
        {
            return PlayersRecords.Where(x => x.PlayerId == id).FirstOrDefault();
        }
        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                    _editCommand = new RelayCommand(param => EditData((Guid)param), null);

                return _editCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                    _deleteCommand = new RelayCommand(param => DeletePlayer((Guid)param), null);

                return _deleteCommand;
            }
        }
        public void DeletePlayer(Guid id)
        {
            if (MessageBox.Show("Confirm delete of this record?", "Player", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                try
                {
                    _repository.RemovePlayer(id);
                    MessageBox.Show("Record successfully deleted.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while saving. " + ex.InnerException);
                }
                finally
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(AdminLookRosters))
                        {
                            (window as AdminLookRosters).table.Items.Refresh();
                            (window as AdminLookRosters).table.ItemsSource = (new PlayerVM()).EHLRecord.PlayersRecords.Where(x => x.teamID == this.teamID);
                        }
                    }
                    _repository.GetAll();
                }
            }
        }
        public void EditData(Guid data)
        {
            var model = _repository.Get(data);
            this.teamID = (int)model.teamID;
            this.Number = (int)model.Number;
            this.ImagePath = model.Photo;
            this.Photo = model.Photo;
            this.Name = model.Name;
            this.PositionID = model.PositionID;
            this.Country = model.Country;
            this.PlayerId = model.PlayerId;
            this.Height = model.Height;
            this.Weight = model.Weight;
            var window = new UpdatingPlayer(this.teamID);
            window.DataContext = this;
            window.Show();
        }
        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new RelayCommand(param => SaveData(), null);

                return _saveCommand;
            }
        }
        public void SaveData()
        {
            if (this != null)
            {
                playerEntity.teamID = this.teamID;
                playerEntity.Photo = this.ImagePath;
                playerEntity.Number = this.Number;
                playerEntity.Name = this.Name;
                playerEntity.PlayerId = this.PlayerId;
                playerEntity.PositionID = this.PositionID;
                playerEntity.Country = this.Country;
                playerEntity.Weight = this.Weight;
                playerEntity.Height = this.Height;
                try
                {
                    _repository.RemovePlayer(PlayerId);
                    if (_repository.AddPlayer(playerEntity.teamID, playerEntity.Number, playerEntity.Photo,
                        playerEntity.Name, playerEntity.PositionID, playerEntity.Country, playerEntity.PlayerId,
                        playerEntity.Height, playerEntity.Weight))
                    {
                        _repository.UpdatePlayer(playerEntity);
                        MessageBox.Show("Record successfully updated.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while saving. " + ex.InnerException);
                }
                finally
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(AdminLookRosters))
                        {
                            (window as AdminLookRosters).table.Items.Refresh();
                            (window as AdminLookRosters).table.ItemsSource = (new PlayerVM()).EHLRecord.PlayersRecords.Where(x => x.teamID == playerEntity.teamID);
                        }
                    }
                    _repository.GetAll();
                }
            }
        }
        public void GetAll()
        {
            this.PlayersRecords = new ObservableCollection<RosterRecord>();
            _repository.GetAll().ForEach(data => this.PlayersRecords.Add(new RosterRecord()
            {
                teamID = data.teamID,
                Number = Convert.ToInt32(data.Number),
                Photo = data.Photo,
                Name = data.Name,
                PositionID = data.PositionID,
                PlayerId = data.PlayerId,
                Country = data.Country,
                Height = data.Height,
                Weight = data.Weight
            }));
        }
    }
}
