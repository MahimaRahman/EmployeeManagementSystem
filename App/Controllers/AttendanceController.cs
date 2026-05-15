using App.AuthFilter;
using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Logged]
    public class AttendanceController : Controller
    {
        AttendanceService attendanceService;
        EmployeeService employeeService;

        public AttendanceController(AttendanceService attendanceService, EmployeeService employeeService)
        {
            this.attendanceService = attendanceService;
            this.employeeService = employeeService;
        }

        [AdminAccess]
        public IActionResult Index()
        {
            var data = attendanceService.Get();
            return View(data);
        }

        [AdminAccess]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Employees = employeeService.Get();
            return View();
        }

        [AdminAccess]
        [HttpPost]
        public IActionResult Create(AttendanceDTO a)
        {
            var res = attendanceService.MarkAttendance(a);

            if (res)
            {
                TempData["Msg"] = "Attendance Marked Successfully";
                TempData["Class"] = "alert-success";
                return RedirectToAction("Index");
            }

            ViewBag.Employees = employeeService.Get();
            TempData["Msg"] = "Attendance Already Exists";
            TempData["Class"] = "alert-danger";
            return View(a);
        }

        public IActionResult EmployeeAttendance()
        {
            var empId = HttpContext.Session.GetString("EmployeeId");

            if (empId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = attendanceService.GetByEmployee(Convert.ToInt32(empId));
            return View(data);
        }

        [AdminAccess]
        public IActionResult MonthlyReport(int empId, int month, int year)
        {
            ViewBag.Employees = employeeService.Get();

            if (empId == 0 || month == 0 || year == 0)
            {
                return View(new List<AttendanceDTO>());
            }

            var data = attendanceService.GetMonthly(empId, month, year);
            ViewBag.Percentage = attendanceService.GetPercentage(empId, month, year);

            return View(data);
        }

        [AdminAccess]
        public IActionResult Delete(int id)
        {
            var res = attendanceService.Delete(id);

            if (res)
            {
                TempData["Msg"] = "Attendance Deleted Successfully";
                TempData["Class"] = "alert-success";
            }
            else
            {
                TempData["Msg"] = "Attendance Delete Failed";
                TempData["Class"] = "alert-danger";
            }

            return RedirectToAction("Index");
        }
    }
}