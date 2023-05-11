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
using WpfApp1.Models.Records;
using WpfApp1.Models.Repos;
using WpfApp1.ViewModels.Command;

namespace WpfApp1.ViewModels
{
    public class MatchVM:ViewModelBase
    {
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
                OnPropertyChanged("MatchVM");
            }
        }
        private void EHLModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("MatchVM");
        }
		private MatchesRepo _repository;
        public MatchVM()
        {
            _repository = new MatchesRepo();
            GetAll();
        }
        public void GetAll()
		{
			this.PlayRecords = new ObservableCollection<MatchRecord>();
			_repository.GetAll().ForEach(data => this.PlayRecords.Add(new MatchRecord()
			{
				matchid = data.matchid,
				Goals1 = Convert.ToInt32(data.Goals1),
				Goals2 = Convert.ToInt32(data.Goals2),
				Logo1 = data.Logo1,
				Logo2=data.Logo2,
				GAME_DATE= (DateTime)data.GAME_DATE
			})) ;
		}
	}
}
