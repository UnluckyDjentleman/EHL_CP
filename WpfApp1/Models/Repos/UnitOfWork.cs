using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models.DBModels;

namespace WpfApp1.Models.Repos
{
    public class UnitOfWork : IDisposable
    {
        private EHLModel context = new EHLModel();
        private bool disposed = false;

        private UserRepo usersRepo;
        private MatchesRepo matchesRepo;
        private PlayersRepo playersRepo;
        private ArticlesRepo articlesRepo;
        private BonusRepo bonusRepo;
        private OrdersRepo ordersRepo;

        public BonusRepo BonusRepo
        {
            get
            {
                if (bonusRepo == null)
                    bonusRepo = new BonusRepo();
                return bonusRepo;
            }
        }


        public ArticlesRepo ArticlesRepo
        {
            get
            {
                if (articlesRepo == null)
                    articlesRepo = new ArticlesRepo();
                return articlesRepo;
            }
        }

        public MatchesRepo MatchesRepo
        {
            get
            {
                if (matchesRepo == null)
                    matchesRepo = new MatchesRepo();
                return matchesRepo;
            }
        }

        public PlayersRepo PlayersRepo
        {
            get
            {
                if (playersRepo == null)
                    playersRepo = new PlayersRepo();
                return playersRepo;
            }
        }


        public UserRepo UsersRepo
        {
            get
            {
                if (usersRepo == null)
                    usersRepo = new UserRepo();
                return usersRepo;
            }
        }

        public OrdersRepo OrdersRepo
        {
            get
            {
                if (ordersRepo == null)
                    ordersRepo = new OrdersRepo();
                return ordersRepo;
            }
        }


        public void Save() => context.SaveChanges();


        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    context.Dispose();
                this.disposed = true;
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
