using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class NotificationService
    {
        NotificationRepo repo;
        Mapper mapper;

        public NotificationService(NotificationRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public bool CreateForAdmin(string message)
        {
            Notification n = new Notification();
            n.Role = "Admin";
            n.Message = message;
            n.IsRead = false;
            n.CreatedAt = DateTime.Now;

            return repo.Create(n);
        }

        public bool CreateForEmployee(int empId, string message)
        {
            Notification n = new Notification();
            n.EmployeeId = empId;
            n.Message = message;
            n.IsRead = false;
            n.CreatedAt = DateTime.Now;

            return repo.Create(n);
        }

        public List<NotificationDTO> GetForEmployee(int empId)
        {
            var data = repo.GetByEmployee(empId);
            return mapper.Map<List<NotificationDTO>>(data);
        }

        public List<NotificationDTO> GetForAdmin()
        {
            var data = repo.GetByRole("Admin");
            return mapper.Map<List<NotificationDTO>>(data);
        }

        public int CountUnreadForEmployee(int empId)
        {
            return repo.CountUnreadByEmployee(empId);
        }

        public int CountUnreadForAdmin()
        {
            return repo.CountUnreadByRole("Admin");
        }

        public bool MarkAsRead(int id)
        {
            return repo.MarkAsRead(id);
        }
    }
}