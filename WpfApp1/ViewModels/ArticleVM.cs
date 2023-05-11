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
    public class ArticleVM
    {
		private ICommand _saveCommand;
		private ArticlesRepo _repository;
		private Articles AEntity = null;
		public ArticleRecord ARecord { get; set; }
		public EHLModel EHLEntities { get; set; }

		public ICommand SaveCommand
		{
			get
			{
				if (_saveCommand == null)
					_saveCommand = new RelayCommand(param => SaveData(), null);

				return _saveCommand;
			}
		}
		public ArticleVM()
		{
			AEntity = new Articles();
			_repository = new ArticlesRepo();
			ARecord = new ArticleRecord();
			GetAll();
		}


		public void SaveData()
		{
			if (ARecord != null)
			{
				AEntity.id = Guid.NewGuid();
				AEntity.Image = ARecord.ImagePath;
				AEntity.Header = ARecord.Header;
				AEntity.Creation_Date = (DateTime)DateTime.Now;
				try
				{
						_repository.AddArticle(AEntity);
						MessageBox.Show("New record successfully saved.");
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error occured while saving. " + ex.InnerException);
				}
				finally
				{
					GetAll();
				}
			}
		}

		public void GetAll()
		{
			ARecord.mangaRecords = new ObservableCollection<ArticleRecord>();
			_repository.GetAll().ForEach(data => ARecord.mangaRecords.Add(new ArticleRecord()
			{
				id = data.id,
				Image = data.Image,
				Header = data.Header,
				Creation_Date = (DateTime)data.Creation_Date
			})) ;
		}
	}
}
