

using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repos
{
    public class LeaveRepo
    {
        EmployeeDbContext db;

        public LeaveRepo(EmployeeDbContext db)
        {
            this.db = db;
        }

        public bool Create(LeaveRequest l)
        {
            db.LeaveRequests.Add(l);
            return db.SaveChanges() > 0;
        }

        public LeaveRequest Get(int id)
        {
            return db.LeaveRequests.Find(id);
        }

        public List<LeaveRequest> Get()
        {
            return db.LeaveRequests.Include(l => l.Employee).ToList();
        }



        public bool Update(LeaveRequest l)
        {
            var exobj = Get(l.LeaveRequestId);

            if (exobj == null)
            {
                return false;
            }

            db.Entry(exobj).CurrentValues.SetValues(l);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var exobj = Get(id);

            if (exobj == null)
            {
                return false;
            }

            db.LeaveRequests.Remove(exobj);
            return db.SaveChanges() > 0;
        }

        public List<LeaveRequest> GetPending()
        {
            return db.LeaveRequests
                .Include(l => l.Employee)
                .Where(l => l.Status == "Pending")
                .ToList();
        }

        public List<LeaveRequest> GetByEmployee(int empId)
        {
            return db.LeaveRequests
                .Include(l => l.Employee)
                .Where(l => l.EmployeeId == empId)
                .ToList();
        }
    }
}