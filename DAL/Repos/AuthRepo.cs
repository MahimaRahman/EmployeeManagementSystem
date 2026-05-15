

using DAL.EF;
using DAL.EF.Tables;


namespace DAL.Repos
{
    public class AuthRepo
    {
        EmployeeDbContext db;

        public AuthRepo(EmployeeDbContext db)
        {
            this.db = db;
        }

        public Employee Login(string email, string password)
        {
            return db.Employees.FirstOrDefault(e =>
                e.Email == email &&
                e.Password == password &&
                e.IsActive == true);
        }
    }
}