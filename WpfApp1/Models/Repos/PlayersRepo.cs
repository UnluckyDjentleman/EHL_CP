using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Models;
using WpfApp1.Models.DBModels;
using WpfApp1.Models.Records;
using WpfApp1.ViewModels;

namespace WpfApp1.Models.Repos
{
    public class PlayersRepo
    {
        private EHLModel ehl;
        public PlayersRepo()
        {
            ehl = new EHLModel();
        }
		public PLAYERS Get(Guid id)
		{
			return ehl.PLAYERS.Find(id);
		}
		public List<PLAYERS> GetAll()
		{
			return ehl.PLAYERS.ToList();
		}
		public bool AddPlayer(int teamidd, int _Number, string _photo, string _name, string _PositionID,string _Country, Guid _PlayerID,int _height,int _weight)
		{
			var teamid = new SqlParameter("@teamid", teamidd);
			var num = new SqlParameter("@num", _Number);
			var photo = new SqlParameter("@photo", _photo);
			var name = new SqlParameter("@name", _name);
			var pos = new SqlParameter("@pos", _PositionID);
			var country = new SqlParameter("@country", _Country);
			var playerID = new SqlParameter("@idp", _PlayerID);
			var height = new SqlParameter("@heig", _height);
			var weight = new SqlParameter("@weig", _weight);
            if (CheckNumber(_Number, teamidd)&&CheckParameters(_Number,_name,_height,_weight))
            {
				var context = new EHLModel();
				context.Database.ExecuteSqlCommand("insert into PLAYERS values (@teamid,@num,@photo,@name,@pos,@country,@idp,@heig,@weig)", teamid,num,photo,name,pos,country,playerID,height,weight);
				context.SaveChanges();
				foreach (Window window in Application.Current.Windows)
				{
					if (window.GetType() == typeof(AdminLookRosters))
					{
						(window as AdminLookRosters).table.Items.Refresh();
					}
				}
				return true;
			}
			else
			{
				return false;
			}
		}
		public bool CheckNumber(int number,int teamid)
        {
			List<PLAYERS> pl = ehl.PLAYERS.Where(x => x.Number == number && x.teamID == teamid).ToList();
            if (pl.Count != 0)
            {
				MessageBox.Show("Players with such number already exists!");
				return false;
            }
            else
            {
				return true;
            }
        }
		public bool CheckParameters(int number, string Name, int height, int weight)
        {
			var regex = new Regex(@"^[A-Z][a-z]+\s[A-Z][a-z]+(-'[A-Z][a-z]+)?$");
			if ((number > 0 && number < 100) && (height > 170 && height <= 200) && (weight > 65 && weight < 110)&& regex.IsMatch(Name))
            {
				return true;
            }
			return false;
        }
		public void UpdatePlayer(PLAYERS m)
		{
			var playersFind = this.Get(m.PlayerId);
			if (playersFind != null)
			{
				playersFind.teamID = m.teamID;
				playersFind.Photo = m.Photo;
				playersFind.Name = m.Name;
				playersFind.Number = m.Number;
				playersFind.PositionID = m.PositionID;
				playersFind.Country = m.Country;
				playersFind.Height = m.Height;
				playersFind.Weight = m.Weight;
			}
		}

		public void RemovePlayer(Guid id)
		{
			var studObj = ehl.PLAYERS.Find(id);
			if (studObj != null)
			{
				ehl.PLAYERS.Remove(studObj);
				ehl.SaveChanges();
			}
		}
	}
}
