
using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class MapperConfig
    {
        public static MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Department, DepartmentDTO>().ReverseMap();
            cfg.CreateMap<Employee, EmployeeDTO>().ReverseMap();
            cfg.CreateMap<Attendance, AttendanceDTO>().ReverseMap();
            cfg.CreateMap<LeaveRequest, LeaveRequestDTO>().ReverseMap();
            cfg.CreateMap<PayrollRecord, PayrollDTO>().ReverseMap();
            cfg.CreateMap<Notification, NotificationDTO>().ReverseMap();
        });

        public static Mapper GetMapper()
        {
            return new Mapper(config);
        }
    }
}
