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
    public class PayrollService
    {
        PayrollRepo payrollRepo;
        EmployeeRepo employeeRepo;
        AttendanceRepo attendanceRepo;
        Mapper mapper;

        public PayrollService(PayrollRepo payrollRepo, EmployeeRepo employeeRepo, AttendanceRepo attendanceRepo)
        {
            this.payrollRepo = payrollRepo;
            this.employeeRepo = employeeRepo;
            this.attendanceRepo = attendanceRepo;
            mapper = MapperConfig.GetMapper();
        }

        public List<PayrollDTO> Get()
        {
            var data = payrollRepo.Get();
            var payrolls = mapper.Map<List<PayrollDTO>>(data);

            for (int i = 0; i < data.Count; i++)
            {
                payrolls[i].EmployeeName = data[i].Employee.FirstName + " " + data[i].Employee.LastName;
            }

            return payrolls;
        }

        public PayrollDTO Get(int id)
        {
            var data = payrollRepo.Get(id);
            var payroll = mapper.Map<PayrollDTO>(data);

            if (data != null && data.Employee != null)
            {
                payroll.EmployeeName = data.Employee.FirstName + " " + data.Employee.LastName;
            }

            return payroll;
        }

        public bool GeneratePayroll(int empId, int month, int year)
        {
            var check = payrollRepo.GetByEmployeeMonthYear(empId, month, year);

            if (check != null)
            {
                return false;
            }

            var employee = employeeRepo.Get(empId);

            if (employee == null)
            {
                return false;
            }

            var attendances = attendanceRepo.GetMonthly(empId, month, year);

            int workingDays = attendances.Count;
            int presentDays = attendances.Where(a => a.Status == "Present").Count();

            if (workingDays == 0)
            {
                return false;
            }

            decimal grossSalary = employee.BasicSalary;
            decimal netSalary = (grossSalary / workingDays) * presentDays;

            PayrollRecord p = new PayrollRecord();
            p.EmployeeId = empId;
            p.Month = month;
            p.Year = year;
            p.WorkingDays = workingDays;
            p.PresentDays = presentDays;
            p.GrossSalary = grossSalary;
            p.NetSalary = netSalary;
            p.GeneratedAt = DateTime.Now;

            return payrollRepo.Create(p);
        }

        public PayrollDTO GetPayroll(int empId, int month, int year)
        {
            var data = payrollRepo.GetByEmployeeMonthYear(empId, month, year);
            var payroll = mapper.Map<PayrollDTO>(data);

            if (data != null && data.Employee != null)
            {
                payroll.EmployeeName = data.Employee.FirstName + " " + data.Employee.LastName;
            }

            return payroll;
        }

        public List<PayrollDTO> GetByEmployee(int empId)
        {
            var data = payrollRepo.GetByEmployee(empId);
            var payrolls = mapper.Map<List<PayrollDTO>>(data);

            for (int i = 0; i < data.Count; i++)
            {
                payrolls[i].EmployeeName = data[i].Employee.FirstName + " " + data[i].Employee.LastName;
            }

            return payrolls;
        }

        public bool Delete(int id)
        {
            return payrollRepo.Delete(id);
        }
    }
}