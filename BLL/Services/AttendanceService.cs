using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL.Repos;
using EMS.DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Services
{
    public class AttendanceService
    {
        AttendanceRepo repo;
        Mapper mapper;

        public AttendanceService(AttendanceRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public List<AttendanceDTO> Get()
        {
            var data = repo.Get();
            var attendances = mapper.Map<List<AttendanceDTO>>(data);

            for (int i = 0; i < data.Count; i++)
            {
                attendances[i].EmployeeName = data[i].Employee.FirstName + " " + data[i].Employee.LastName;
            }

            return attendances;
        }

        public AttendanceDTO Get(int id)
        {
            var data = repo.Get(id);
            var attendance = mapper.Map<AttendanceDTO>(data);

            if (data != null && data.Employee != null)
            {
                attendance.EmployeeName = data.Employee.FirstName + " " + data.Employee.LastName;
            }

            return attendance;
        }

        public bool MarkAttendance(AttendanceDTO a)
        {
            var check = repo.GetByEmployeeAndDate(a.EmployeeId, a.AttendanceDate);

            if (check != null)
            {
                return false;
            }

            var data = mapper.Map<Attendance>(a);
            return repo.Create(data);
        }

        public bool Update(AttendanceDTO a)
        {
            var data = mapper.Map<Attendance>(a);
            return repo.Update(data);
        }


        public bool Delete(int id)
        {
            return repo.Delete(id);
        }


        public List<AttendanceDTO> GetByEmployee(int empId)
        {
            var data = repo.GetByEmployee(empId);
            var attendances = mapper.Map<List<AttendanceDTO>>(data);

            for (int i = 0; i < data.Count; i++)
            {
                attendances[i].EmployeeName = data[i].Employee.FirstName + " " + data[i].Employee.LastName;
            }

            return attendances;
        }

        public List<AttendanceDTO> GetMonthly(int empId, int month, int year)
        {
            var data = repo.GetMonthly(empId, month, year);
            var attendances = mapper.Map<List<AttendanceDTO>>(data);

            for (int i = 0; i < data.Count; i++)
            {
                attendances[i].EmployeeName = data[i].Employee.FirstName + " " + data[i].Employee.LastName;
            }

            return attendances;
        }

        public double GetPercentage(int empId, int month, int year)
        {
            var data = repo.GetMonthly(empId, month, year);

            if (data.Count == 0)
            {
                return 0;
            }

            var present = data.Where(a => a.Status == "Present").Count();

            return (present * 100.0) / data.Count;
        }
    }
}