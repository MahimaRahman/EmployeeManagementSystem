using System;
using System.Collections.Generic;
using System.Text;

using DAL.EF.Tables;
using DAL.EF;

namespace DAL.Repos
{
    public class DepartmentRepo
    {

            EmployeeDbContext db;

            public DepartmentRepo(EmployeeDbContext db)
            {
                this.db = db;
            }

        
            public bool Create(Department d)
            {
                db.Departments.Add(d);
                return db.SaveChanges() > 0;
            }

            public Department Get(int id)
            {
                return db.Departments.Find(id);
            }

            public List<Department> Get()
            {
                return db.Departments.ToList();
            }



        public bool Update(Department d)
        {
            var exobj = Get(d.DepartmentId);

            if (exobj == null)
            {
                return false;
            }

            db.Entry(exobj).CurrentValues.SetValues(d);
            return db.SaveChanges() > 0;
        }


        public bool Delete(int id)
        {
            var exobj = Get(id);

            if (exobj == null)
            {
                return false;
            }

            db.Departments.Remove(exobj);
            return db.SaveChanges() > 0;
        }
    }
    }

