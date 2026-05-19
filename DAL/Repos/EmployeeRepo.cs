using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repos
{
    public class EmployeeRepo
    {
        EmployeeDbContext db;

        public EmployeeRepo(EmployeeDbContext db)
        {
            this.db = db;
        }

        public bool Create(Employee e)
        {
            db.Employees.Add(e);
            return db.SaveChanges() > 0;
        }

        public Employee Get(int id)
        {
            return db.Employees.Include(e => e.Department).FirstOrDefault(e => e.EmployeeId == id);
        }

        public List<Employee> Get()
        {
            return db.Employees.Include(e => e.Department).ToList();


        }




        public bool Update(Employee e)
        {
            var exobj = Get(e.EmployeeId);

            if (exobj == null)
            {
                return false;
            }

            var oldPassword = exobj.Password;

            db.Entry(exobj).CurrentValues.SetValues(e);

            if (string.IsNullOrEmpty(e.Password))
            {
                exobj.Password = oldPassword;
            }

            return db.SaveChanges() > 0;
        }



        //update password
        public bool UpdatePassword(int id, string password)
        {
            var exobj = db.Employees.FirstOrDefault(e => e.EmployeeId == id);

            if (exobj == null)
            {
                return false;
            }

            exobj.Password = password;

            return db.SaveChanges() > 0;
        }



        public bool Delete(int id)
        {
            var exobj = Get(id);

            if (exobj == null)
            {
                return false;
            }

            db.Employees.Remove(exobj);
            return db.SaveChanges() > 0;
        }



        public Employee GetByEmail(string email)
        {
            return db.Employees.Include(e => e.Department).FirstOrDefault(e => e.Email == email);
        }

        public List<Employee> Search(string name, int? deptId, decimal? minSalary, decimal? maxSalary)
        {
            var data = db.Employees.Include(e => e.Department).AsQueryable();

            if (name != null)
            {
                data = data.Where(e => e.FirstName.Contains(name) || e.LastName.Contains(name));
            }

            if (deptId != null && deptId > 0)
            {
                data = data.Where(e => e.DepartmentId == deptId);
            }

            if (minSalary != null)
            {
                data = data.Where(e => e.BasicSalary >= minSalary);
            }

            if (maxSalary != null)
            {
                data = data.Where(e => e.BasicSalary <= maxSalary);
            }

            return data.ToList();
        }
    }
}