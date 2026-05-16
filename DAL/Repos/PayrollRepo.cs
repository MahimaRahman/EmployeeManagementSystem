using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repos
{
    public class PayrollRepo
    {
        EmployeeDbContext db;

        public PayrollRepo(EmployeeDbContext db)
        {
            this.db = db;
        }

        public bool Create(PayrollRecord p)
        {
            db.PayrollRecords.Add(p);
            return db.SaveChanges() > 0;
        }


        public PayrollRecord Get(int id)
        {
            return db.PayrollRecords.Include(p => p.Employee).FirstOrDefault(p => p.PayrollId == id);
        }

        public List<PayrollRecord> Get()
        {
            return db.PayrollRecords.Include(p => p.Employee).ToList();
        }

        

        public bool Update(PayrollRecord p)
        {
            var exobj = Get(p.PayrollId);

            if (exobj == null)
            {
                return false;
            }

            db.Entry(exobj).CurrentValues.SetValues(p);
            return db.SaveChanges() > 0;
        }


        public bool Delete(int id)
        {
            var exobj = Get(id);

            if (exobj == null)
            {
                return false;
            }

            db.PayrollRecords.Remove(exobj);
            return db.SaveChanges() > 0;
        }

        public List<PayrollRecord> GetByEmployee(int empId)
        {
            return db.PayrollRecords
                .Include(p => p.Employee)
                .Where(p => p.EmployeeId == empId)
                .ToList();
        }

        public PayrollRecord GetByEmployeeMonthYear(int empId, int month, int year)
        {
            return db.PayrollRecords
                .Include(p => p.Employee)
                .FirstOrDefault(p =>
                    p.EmployeeId == empId &&
                    p.Month == month &&
                    p.Year == year);
        }
    }
}
