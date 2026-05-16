using AutoMapper;
using BLL.DTOs;
using BLL.Helpers;
using DAL.EF.Tables;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class EmployeeService
    {
        EmployeeRepo repo;
        Mapper mapper;

        public EmployeeService(EmployeeRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

       

        public bool Create(EmployeeDTO e)
        {
            e.Password = PasswordHelper.GetMd5(e.Password);

            var data = mapper.Map<Employee>(e);

            return repo.Create(data);
        }




        public List<EmployeeDTO> Get()
        {
            var data = repo.Get();
            var employees = mapper.Map<List<EmployeeDTO>>(data);

            for (int i = 0; i < data.Count; i++)
            {
                employees[i].DepartmentName = data[i].Department.DepartmentName;
            }

            return employees;
        }

        public EmployeeDTO Get(int id)
        {
            var data = repo.Get(id);
            var employee = mapper.Map<EmployeeDTO>(data);

            if (data != null && data.Department != null)
            {
                employee.DepartmentName = data.Department.DepartmentName;
            }

            return employee;
        }




        public bool Update(EmployeeDTO e)
        {
            if (!string.IsNullOrEmpty(e.Password))
            {
                e.Password = PasswordHelper.GetMd5(e.Password);
            }

            var data = mapper.Map<Employee>(e);

            return repo.Update(data);
        }



        //update pass

        public bool ChangePassword(int id, ChangePasswordDTO c)
        {
            var employee = repo.Get(id);

            if (employee == null)
            {
                return false;
            }

            var oldHash = PasswordHelper.GetMd5(c.OldPassword);

            if (employee.Password != oldHash && employee.Password != c.OldPassword)
            {
                return false;
            }

            var newHash = PasswordHelper.GetMd5(c.NewPassword);

            return repo.UpdatePassword(id, newHash);
        }


        public bool Delete(int id)
        {
            return repo.Delete(id);
        }

        public List<EmployeeDTO> Search(string name, int? deptId, decimal? minSalary, decimal? maxSalary)
        {
            var data = repo.Search(name, deptId, minSalary, maxSalary);
            var employees = mapper.Map<List<EmployeeDTO>>(data);

            for (int i = 0; i < data.Count; i++)
            {
                employees[i].DepartmentName = data[i].Department.DepartmentName;
            }

            return employees;
        }
    }
}