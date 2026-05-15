

//using DAL.EF;


//namespace DAL.Repos 
//{
//    public class ReportRepo
//    {
//        EmployeeDbContext db;

//        public ReportRepo(EmployeeDbContext db)
//        {
//            this.db = db;
//        }

//        public dynamic DepartmentSummary()
//        {
//            var data = db.Employees
//                .GroupBy(e => e.Department.DepartmentName)
//                .Select(g => new
//                {
//                    DepartmentName = g.Key,
//                    EmployeeCount = g.Count(),
//                    AverageSalary = g.Average(e => e.BasicSalary),
//                    MinSalary = g.Min(e => e.BasicSalary),
//                    MaxSalary = g.Max(e => e.BasicSalary)
//                })
//                .ToList();

//            return data;
//        }
//    }
//}

using DAL.EF;
using DAL.Models;

namespace DAL.Repos
{
    public class ReportRepo
    {
        EmployeeDbContext db;

        public ReportRepo(EmployeeDbContext db)
        {
            this.db = db;
        }

        public List<DepartmentSummaryData> DepartmentSummary()
        {
            var data = db.Employees
                .GroupBy(e => e.Department.DepartmentName)
                .Select(g => new DepartmentSummaryData
                {
                    DepartmentName = g.Key,
                    EmployeeCount = g.Count(),
                    AverageSalary = g.Average(e => e.BasicSalary),
                    MinSalary = g.Min(e => e.BasicSalary),
                    MaxSalary = g.Max(e => e.BasicSalary)
                })
                .ToList();

            return data;
        }
    }
}