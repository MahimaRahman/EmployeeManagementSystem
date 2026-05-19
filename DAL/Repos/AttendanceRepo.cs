

using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;


namespace EMS.DAL.Repos
{
    public class AttendanceRepo
    {
        EmployeeDbContext db;

        public AttendanceRepo(EmployeeDbContext db)
        {
            this.db = db;
        }


        public bool Create(Attendance a)
        {
            db.Attendances.Add(a);
            return db.SaveChanges() > 0;
        }

        public Attendance Get(int id)
        {
            return db.Attendances.Find(id);
        }

        public List<Attendance> Get()
        {
            return db.Attendances.Include(a => a.Employee).ToList();
        }


        public bool Update(Attendance a)
        {
            var exobj = Get(a.AttendanceId);

            if (exobj == null)
            {
                return false;
            }

            db.Entry(exobj).CurrentValues.SetValues(a);
            return db.SaveChanges() > 0;
        }


        public bool Delete(int id)
        {
            var exobj = Get(id);

            if (exobj == null)
            {
                return false;
            }

            db.Attendances.Remove(exobj);
            return db.SaveChanges() > 0;
        }

        public List<Attendance> GetByEmployee(int empId)
        {
            return db.Attendances
                .Include(a => a.Employee)
                .Where(a => a.EmployeeId == empId)
                .ToList();
        }

        public Attendance GetByEmployeeAndDate(int empId, DateOnly date)
        {
            return db.Attendances.FirstOrDefault(a => a.EmployeeId == empId && a.AttendanceDate == date);
            
        }

        public List<Attendance> GetMonthly(int empId, int month, int year)
        {
            return db.Attendances
                .Include(a => a.Employee).Where(a => a.EmployeeId == empId && a.AttendanceDate.Month == month && a.AttendanceDate.Year == year)
                .ToList();
        }
    }
}
