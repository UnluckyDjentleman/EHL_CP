using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Models.DBModels;
using WpfApp1.Models.Repos;
using WpfApp1.ViewModels.Command;

namespace WpfApp1.Models.Records
{
    public class MatchRecord:ViewModels.ViewModelBase
    {
        private int _matchid;
        public int matchid
        {
            get
            {
                return _matchid;
            }
            set
            {
                _matchid = value;
                OnPropertyChanged("matchid");
            }
        }
        private int _goals1;
        public int Goals1
        {
            get
            {
                return _goals1;
            }
            set
            {
                _goals1 = value;
                OnPropertyChanged("Goals1");
            }
        }
        private int _goals2;
        public int Goals2
        {
            get
            {
                return _goals2;
            }
            set
            {
                _goals2 = value;
                OnPropertyChanged("Goals2");
            }
        }
        private string _logo1;
        public string Logo1
        {
            get
            {
                return _logo1;
            }
            set
            {
                _logo1 = value;
            }
        }
        private string _logo2;
        public string Logo2
        {
            get
            {
                return _logo2;
            }
            set
            {
                _logo2 = value;
            }
        }
        private DateTime _gameDate;
        public DateTime GAME_DATE
        {
            get
            {
                return _gameDate;
            }
            set
            {
                _gameDate = value;
            }
        }
        private ObservableCollection<MatchRecord> playRecords;
        public ObservableCollection<MatchRecord> PlayRecords
        {
            get
            {
                return playRecords;
            }
            set
            {
                playRecords = value;
                OnPropertyChanged("MatchRecord");
            }
        }
        private void EHLModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("MatchRecord");
        }
        private ICommand _editCommand;
        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                    _editCommand = new RelayCommand(param => EditData((int)param), null);

                return _editCommand;
            }
        }
        private MatchesRepo _repository = new MatchesRepo();
        public void EditData(int id)
        {
            var model = _repository.Get(id);
            this.matchid = model.matchid;
            this.Goals1 = (int)model.Goals1;
            this.Goals2 = (int)model.Goals2;
            this.Logo1 = model.Logo1;
            this.Logo2 = model.Logo2;
            this.GAME_DATE = (DateTime)model.GAME_DATE;
            var window = new UpdatingMatch(id);
            EHLModel ehl = new EHLModel();
            window.DataContext = this;
            window.Show();
            //this moment cancels setting of points and status of game (victory,tie or lose). It needs for update the match and set new values.
            //From 138 to 151
            var match = new SqlParameter("@id",id);
            if (Goals1 > Goals2)
            {
                ehl.Database.ExecuteSqlCommand("update tournament set W=W-1,P=P-2 from MATCHES where teamid=MATCHES.Team1 and matchid=@id " +
                    "update tournament set L = L - 1 from MATCHES where teamid = MATCHES.Team2 and matchid = @id",match);
            }
            else if (Goals1 == Goals2&&Goals1!=0&&Goals2!=0)
            {
                ehl.Database.ExecuteSqlCommand("update tournament set T = T - 1, P = P - 1 from MATCHES where (teamid = MATCHES.Team1 or teamid = MATCHES.Team2) and matchid = @id", match);
            }
            else if(Goals1<Goals2)
            {
                ehl.Database.ExecuteSqlCommand("update tournament set W=W-1,P=P-2 from MATCHES where teamid=MATCHES.Team2 and matchid=@id " +
                   "update tournament set L = L - 1 from MATCHES where teamid = MATCHES.Team1 and matchid = @id", match);
            }
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
        public MATCHES MatchEntity = new MATCHES();
        public void SaveData()
        {
            if (this != null)
            {
                MatchEntity.matchid = this.matchid;
                MatchEntity.Goals1 = this.Goals1;
                MatchEntity.Goals2 = this.Goals2;
                MatchEntity.Logo1 = this.Logo1;
                MatchEntity.Logo2 = this.Logo2;
                MatchEntity.GAME_DATE = this.GAME_DATE;
                try
                {
                    MatchEntity.matchid = this.matchid;
                    _repository.UpdateMatch(MatchEntity);
                    MessageBox.Show("Record successfully updated.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while saving. " + ex.InnerException);
                }
                finally
                {
                    _repository.GetAll();
                }
            }
        }
    }
}
