using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;
using WpfApp1.Models.DBModels;

namespace WpfApp1.Models.Repos
{
    public class MatchesRepo
    {
        private EHLModel ehl;
        public MatchesRepo() => ehl = new EHLModel();
        public MATCHES Get(int id)
        {
            return ehl.MATCHES.Find(id);
        }
        public List<MATCHES> GetAll()
        {
            return ehl.MATCHES.OrderBy(n=>n.GAME_DATE).ToList();
        }
        public void UpdateMatch(MATCHES matches)
        {
            var matchFind = this.Get(matches.matchid);
            if (matchFind != null)
            {
                matchFind.matchid = matches.matchid;
                matchFind.Goals1 = matches.Goals1;
                matchFind.Goals2 = matches.Goals2;
            }
        }
    }
}
