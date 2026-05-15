using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class LeaveService
    {
        LeaveRepo repo;
        Mapper mapper;

        public LeaveService(LeaveRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public bool ApplyLeave(LeaveRequestDTO l)
        {
            l.Status = "Pending";
            var data = mapper.Map<LeaveRequest>(l);
            return repo.Create(data);
        }

        public List<LeaveRequestDTO> Get()
        {
            var data = repo.Get();
            var leaves = mapper.Map<List<LeaveRequestDTO>>(data);

            for (int i = 0; i < data.Count; i++)
            {
                leaves[i].EmployeeName = data[i].Employee.FirstName + " " + data[i].Employee.LastName;
            }

            return leaves;
        }

        public LeaveRequestDTO Get(int id)
        {
            var data = repo.Get(id);
            var leave = mapper.Map<LeaveRequestDTO>(data);

            if (data != null && data.Employee != null)
            {
                leave.EmployeeName = data.Employee.FirstName + " " + data.Employee.LastName;
            }

            return leave;
        }

        public List<LeaveRequestDTO> GetPending()
        {
            var data = repo.GetPending();
            var leaves = mapper.Map<List<LeaveRequestDTO>>(data);

            for (int i = 0; i < data.Count; i++)
            {
                leaves[i].EmployeeName = data[i].Employee.FirstName + " " + data[i].Employee.LastName;
            }

            return leaves;
        }

        public List<LeaveRequestDTO> GetByEmployee(int empId)
        {
            var data = repo.GetByEmployee(empId);
            var leaves = mapper.Map<List<LeaveRequestDTO>>(data);

            for (int i = 0; i < data.Count; i++)
            {
                leaves[i].EmployeeName = data[i].Employee.FirstName + " " + data[i].Employee.LastName;
            }

            return leaves;
        }

        public bool ReviewLeave(int id, string status, string reviewedBy)
        {
            var data = repo.Get(id);

            if (data == null)
            {
                return false;
            }

            data.Status = status;
            data.ReviewedBy = reviewedBy;
            data.ReviewedAt = DateTime.Now;

            return repo.Update(data);
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}