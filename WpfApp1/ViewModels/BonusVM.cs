using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.Models.DBModels;
using WpfApp1.Models.Records;
using WpfApp1.Models.Repos;
using WpfApp1.ViewModels.Command;

namespace WpfApp1.ViewModels
{
    public class BonusVM
    {
		private ICommand _saveCommand;
		private BonusRepo _repository;
		private bonus BEntity = null;
		public BonusRecord BRecord { get; set; }
		public EHLModel EHLEntities { get; set; }
      //  private ICommand _getID;
      //  public ICommand GetID
      //  {
      //      get
      //      {
      //          if (_getID == null)
      //              _getID = new RelayCommand(param => ReturnID((Guid)param), null);

      //          return _getID;
      //      }
      //  }
      //  public void ReturnID(Guid guid)
      //  {
      //      foreach (Window window in Application.Current.Windows)
      //      {
      //          if (window.GetType() == typeof(MainFrame))
      //          {
      //              if ((window as MainFrame).Main.Content.GetType() == typeof(Bonus))
      //              {
      //                  var tt = window as MainFrame;
      //                  var pageGet = tt.Main.Content as Bonus;
						//pageGet.bonuS.id = guid;
      //                  try
      //                  {
      //                      var result = pageGet.unitOfWork.OrdersRepo.AddOrders(pageGet.bonuS.id, pageGet.user.id, "Ordered" + DateTime.Now.ToString());
      //                      if (result)
      //                      {
      //                          MessageBox.Show("Bonus ordered. Please wait for our call", "Succeed!", MessageBoxButton.OK);
      //                      }
      //                      else throw new Exception();
      //                  }
      //                  catch
      //                  {
      //                      MessageBox.Show("Oops");
      //                  }
      //              }
      //          }
      //      }
      //  }
        public ICommand SaveCommand
		{
			get
			{
				if (_saveCommand == null)
					_saveCommand = new RelayCommand(param => SaveData(), null);

				return _saveCommand;
			}
		}
		public BonusVM()
		{
			BEntity = new bonus();
			_repository = new BonusRepo();
			BRecord = new BonusRecord();
			GetAll();
		}


		public void SaveData()
		{
			if (BRecord != null)
			{
				BEntity.id = Guid.NewGuid();
				BEntity.image = BRecord.ImagePath;
				BEntity.header = BRecord.Header;
				try
				{
					_repository.AddBonus(BEntity);
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
			BRecord.bonusRecords = new ObservableCollection<BonusRecord>();
			_repository.GetAll().ForEach(data => BRecord.bonusRecords.Add(new BonusRecord()
			{
				id = data.id,
				Image = data.image,
				Header = data.header
			}));
		}
	}
}
