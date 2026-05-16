using App.AuthFilter;
using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Logged]
    public class EmployeeController : Controller
    {
        EmployeeService employeeService;
        DepartmentService departmentService;

        public EmployeeController(EmployeeService employeeService, DepartmentService departmentService)
        {
            this.employeeService = employeeService;
            this.departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var data = employeeService.Get();
            return View(data);
        }

        public IActionResult Details(int id)
        {
            var data = employeeService.Get(id);
            return View(data);
        }

        [AdminAccess]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = departmentService.Get();
            return View();
        }

        [AdminAccess]
        [HttpPost]
        public IActionResult Create(EmployeeDTO e)
        {
            if (ModelState.IsValid)
            {
                var res = employeeService.Create(e);

                if (res)
                {
                    TempData["Msg"] = "Employee Created Successfully";
                    TempData["Class"] = "alert-success";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Departments = departmentService.Get();
            TempData["Msg"] = "Employee Create Failed";
            TempData["Class"] = "alert-danger";
            return View(e);
        }

        [AdminAccess]
        [HttpGet]
        public IActionResult Update(int id)
        {
            var data = employeeService.Get(id);
            ViewBag.Departments = departmentService.Get();
            return View(data);
        }

        
        [AdminAccess]
        [HttpPost]
        public IActionResult Update(EmployeeDTO e)
        {
            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                var res = employeeService.Update(e);

                if (res)
                {
                    TempData["Msg"] = "Employee Updated Successfully";
                    TempData["Class"] = "alert-success";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Departments = departmentService.Get();
            TempData["Msg"] = "Employee Update Failed";
            TempData["Class"] = "alert-danger";
            return View(e);
        }




        [AdminAccess]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var res = employeeService.Delete(id);

            if (res)
            {
                TempData["Msg"] = "Employee Deleted Successfully";
                TempData["Class"] = "alert-success";
            }
            else
            {
                TempData["Msg"] = "Employee Delete Failed";
                TempData["Class"] = "alert-danger";
            }

            return RedirectToAction("Index");
        }


        //update password

        
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

       
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordDTO c)
        {
            var empId = HttpContext.Session.GetString("EmployeeId");

            if (empId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(empId);

                var res = employeeService.ChangePassword(id, c);

                if (res)
                {
                    TempData["Msg"] = "Password Changed Successfully";
                    TempData["Class"] = "alert-success";
                    return RedirectToAction("Dashboard", "Auth");
                }

                ViewBag.Error = "Old password is incorrect";
            }

            return View(c);
        }




        public IActionResult Search(string name, int? deptId, decimal? minSalary, decimal? maxSalary)
        {
            var data = employeeService.Search(name, deptId, minSalary, maxSalary);
            ViewBag.Departments = departmentService.Get();

            ViewBag.Name = name;
            ViewBag.DeptId = deptId;
            ViewBag.MinSalary = minSalary;
            ViewBag.MaxSalary = maxSalary;

            return View(data);
        }
    }
}