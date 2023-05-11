using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models.DBModels;

namespace WpfApp1.Models.Repos
{
    public class OrdersRepo
    {
        private EHLModel ehl;
        public OrdersRepo()
        {
            ehl = new EHLModel();
        }
        public List<orders> GetAll()
        {
            return ehl.orders.ToList();
        }
        public bool AddOrders(Guid bonusId,Guid userId, string status)
        {
            try
            {
                var id = new SqlParameter("@id", bonusId);
                var id_user = new SqlParameter("@id_user", userId);
                var s = new SqlParameter("@status", status);

                ehl.Database.ExecuteSqlCommand("insert into orders values(@id,@id_user,@status)",id,id_user,s);
                ehl.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
