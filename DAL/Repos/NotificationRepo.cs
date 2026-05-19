using DAL.EF;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repos
{
    public class NotificationRepo
    {
        EmployeeDbContext db;

        public NotificationRepo(EmployeeDbContext db)
        {
            this.db = db;
        }

        public bool Create(Notification n)
        {
            db.Notifications.Add(n);
            return db.SaveChanges() > 0;
        }

        public List<Notification> GetByEmployee(int empId)
        {
            return db.Notifications
                .Where(n => n.EmployeeId == empId).OrderByDescending(n => n.CreatedAt).ToList();
        }

        public List<Notification> GetByRole(string role)
        {
            return db.Notifications.Where(n => n.Role == role).OrderByDescending(n => n.CreatedAt).ToList();
        }

        public int CountUnreadByEmployee(int empId)
        {
            return db.Notifications.Where(n => n.EmployeeId == empId && n.IsRead == false).Count();
        }

        public int CountUnreadByRole(string role)
        {
            return db.Notifications.Where(n => n.Role == role && n.IsRead == false).Count();
        }


        //mark notification and now I feel so tired😭haat betha hoye geche vaiiiii
        public bool MarkAsRead(int id)
        {
            var n = db.Notifications.Find(id);

            if (n == null)
            {
                return false;
            }

            n.IsRead = true;
            return db.SaveChanges() > 0;
        }
    }
}