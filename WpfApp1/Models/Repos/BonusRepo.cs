using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models.DBModels;

namespace WpfApp1.Models.Repos
{
    public class BonusRepo
    {
        private EHLModel ehl;
        public BonusRepo()
        {
            ehl = new EHLModel();
        }
        public bonus Get(Guid id)
        {
            return ehl.bonus.Find(id);
        }
        public List<bonus> GetAll()
        {
            return ehl.bonus.ToList();
        }
        public void AddBonus(bonus article)
        {
            if (article != null)
            {
                ehl.bonus.Add(article);
                ehl.SaveChanges();
            }
        }
    }
}
