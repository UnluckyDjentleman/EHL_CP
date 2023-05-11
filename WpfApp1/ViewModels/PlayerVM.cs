using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Models.DBModels;
using WpfApp1.Models.Records;
using WpfApp1.Models.Repos;
using WpfApp1.ViewModels.Command;

namespace WpfApp1.ViewModels
{
    public class PlayerVM:ViewModelBase
    {
        //it needs to get teamID to add player in roster of team
        private int _getTID;
        public int GetTID
        {
            get
            {
                return _getTID;
            }
            set
            {
                _getTID = value;
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
                OnPropertyChanged("PlayerVM");
            }
        }
        private ICommand _addCommand;
        private PlayersRepo _repository;
        public RosterRecord EHLRecord { get; set; }
        public PLAYERS playerEntity { get; set; }
        public EHLModel EHLEntities { get; set; }
        public PlayerVM()
        {
            _repository = new PlayersRepo();
            EHLRecord = new RosterRecord();
            playerEntity = new PLAYERS();
            GetAll();
        }
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                    _addCommand = new RelayCommand(param => AddData((int)param), null);

                return _addCommand;
            }
        }
        public void AddData(int id)
        {
            PlayerVM rr = new PlayerVM();
            var window = new AddingPlayer(id);
            window.DataContext = rr;
            rr.EHLRecord.teamID = id;
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
                playerEntity.teamID = EHLRecord.teamID;
                playerEntity.Photo = EHLRecord.ImagePath;
                playerEntity.Number = EHLRecord.Number;
                playerEntity.Name = EHLRecord.Name;
                playerEntity.PositionID = EHLRecord.PositionID;
                playerEntity.PlayerId = Guid.NewGuid();
                playerEntity.Country = EHLRecord.Country;
                playerEntity.Weight = EHLRecord.Weight;
                playerEntity.Height = EHLRecord.Height;
                try
                {
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
                    //_repository.GetAll();
                }
            }
        }
        public void GetAll()
		{
			EHLRecord.PlayersRecords = new ObservableCollection<RosterRecord>();
            _repository.GetAll().ForEach(data => EHLRecord.PlayersRecords.Add(new RosterRecord()
            {
                teamID = data.teamID,
                Number = Convert.ToInt32(data.Number),
                Photo = data.Photo,
                Name = data.Name,
                PositionID = data.PositionID,
                PlayerId=data.PlayerId,
                Country = data.Country,
                Height = data.Height,
                Weight = data.Weight
            }));
        }
	}
}
