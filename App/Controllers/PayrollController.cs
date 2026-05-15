using App.AuthFilter;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Logged]
    public class PayrollController : Controller
    {
        PayrollService payrollService;
        EmployeeService employeeService;

        public PayrollController(PayrollService payrollService, EmployeeService employeeService)
        {
            this.payrollService = payrollService;
            this.employeeService = employeeService;
        }

        [AdminAccess]
        public IActionResult Index()
        {
            var data = payrollService.Get();
            return View(data);
        }

        [AdminAccess]
        [HttpGet]
        public IActionResult Generate()
        {
            ViewBag.Employees = employeeService.Get();
            return View();
        }

        [AdminAccess]
        [HttpPost]
        public IActionResult Generate(int empId, int month, int year)
        {
            var res = payrollService.GeneratePayroll(empId, month, year);

            if (res)
            {
                TempData["Msg"] = "Payroll Generated Successfully";
                TempData["Class"] = "alert-success";
                return RedirectToAction("Index");
            }

            ViewBag.Employees = employeeService.Get();
            TempData["Msg"] = "Payroll Generate Failed or Already Exists";
            TempData["Class"] = "alert-danger";
            return View();
        }

        [AdminAccess]
        public IActionResult Details(int id)
        {
            var data = payrollService.Get(id);
            return View(data);
        }

        public IActionResult MyPayroll()
        {
            var empId = HttpContext.Session.GetString("EmployeeId");

            if (empId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = payrollService.GetByEmployee(Convert.ToInt32(empId));
            return View(data);
        }

        [AdminAccess]
        public IActionResult Delete(int id)
        {
            var res = payrollService.Delete(id);

            if (res)
            {
                TempData["Msg"] = "Payroll Deleted Successfully";
                TempData["Class"] = "alert-success";
            }
            else
            {
                TempData["Msg"] = "Payroll Delete Failed";
                TempData["Class"] = "alert-danger";
            }

            return RedirectToAction("Index");
        }
    }
}