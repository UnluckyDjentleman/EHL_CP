using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;
using WpfApp1.Models.DBModels;

namespace WpfApp1.Models.Repos
{
    public class ArticlesRepo
    {
        private EHLModel ehl;
        public ArticlesRepo()
        {
            ehl = new EHLModel();
        }
        public List<Articles> GetAll()
        {
            return ehl.Articles.ToList();
        }
        public void AddArticle(Articles article)
        {
            if (article != null)
            {
                ehl.Articles.Add(article);
                ehl.SaveChanges();            
            }
        }
    }
}
