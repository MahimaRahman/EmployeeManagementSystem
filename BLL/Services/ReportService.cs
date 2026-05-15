using BLL.DTOs;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class ReportService
    {
        ReportRepo repo;

        public ReportService(ReportRepo repo)
        {
            this.repo = repo;
        }

        public List<DepartmentSummaryDTO> DepartmentSummary()
        {
            var data = repo.DepartmentSummary();
            List<DepartmentSummaryDTO> list = new List<DepartmentSummaryDTO>();

            foreach (var item in data)
            {
                DepartmentSummaryDTO d = new DepartmentSummaryDTO();
                d.DepartmentName = item.DepartmentName;
                d.EmployeeCount = item.EmployeeCount;
                d.AverageSalary = item.AverageSalary;
                d.MinSalary = item.MinSalary;
                d.MaxSalary = item.MaxSalary;

                list.Add(d);
            }

            return list;
        }
    }
}