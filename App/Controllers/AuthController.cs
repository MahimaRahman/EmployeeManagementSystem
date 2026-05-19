using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class AuthController : Controller
    {
        
        AuthService authService;
        NotificationService notificationService;

        public AuthController(AuthService authService, NotificationService notificationService)
        {
            this.authService = authService;
            this.notificationService = notificationService;
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginDTO login)
        {
            if (ModelState.IsValid)
            {
                var data = authService.Login(login);

                if (data != null)
                {
                    HttpContext.Session.SetString("EmployeeId", data.EmployeeId.ToString());
                    HttpContext.Session.SetString("Uname", data.FirstName + " " + data.LastName);
                    HttpContext.Session.SetString("Email", data.Email);
                    HttpContext.Session.SetString("Role", data.Role);

                    TempData["Msg"] = "Login Successful";
                    TempData["Class"] = "alert-success";

                    return RedirectToAction("Dashboard");
                }

                TempData["Msg"] = "Invalid email or password";
                TempData["Class"] = "alert-danger";
            }

            return View(login);
        }


        public IActionResult Dashboard()
        {
            var empId = HttpContext.Session.GetString("EmployeeId");
            var role = HttpContext.Session.GetString("Role");

            if (empId == null)
            {
                return RedirectToAction("Login");
            }

            if (role == "Admin")
            {
                ViewBag.UnreadNotification = notificationService.CountUnreadForAdmin();
            }
            else
            {
                ViewBag.UnreadNotification = notificationService.CountUnreadForEmployee(Convert.ToInt32(empId));
            }

            return View();
        }


        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            TempData["Msg"] = "Logout Successful";
            TempData["Class"] = "alert-success";

            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}