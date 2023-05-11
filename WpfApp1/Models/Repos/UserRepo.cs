using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models.DBModels;

namespace WpfApp1.Models.Repos
{
    public class UserRepo
    {
        private EHLModel ehl;
        public UserRepo() => ehl = new EHLModel();
        public USERS GetUserByNameAndSurname(string name, string surname)
        {
            try
            {
                List<USERS> users = ehl.USERS.Where(r=>r.NAME==name&&r.SURNAME==surname).ToList();
                var user = users.FirstOrDefault(x => x.NAME == name && x.SURNAME == surname);
                return user;
            }  
            catch
            {
                return new USERS();
            }
        }
        public bool AddNewUser(Guid idd, string namee, string surrname, string pass, string phonee, int favTeam)
        {
            try
            {
                var id = new SqlParameter("@id", idd);
                var name = new SqlParameter("@name", namee);
                var surname = new SqlParameter("@surname", surrname);
                var password = new SqlParameter("@password", pass);
                var role = new SqlParameter("@role", 2);
                var phone = new SqlParameter("@phone", phonee);
                var favteam = new SqlParameter("@favTeam", favTeam);
                var context = new EHLModel();
                context.Database.ExecuteSqlCommand("insert into USERS values (@id, @name, @surname, @password, @role, @phone, @favTeam)", id, name, surname, password, role, phone, favteam);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
