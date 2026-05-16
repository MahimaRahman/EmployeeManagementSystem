using App.AuthFilter;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Logged]
    public class NotificationController : Controller
    {
        NotificationService notificationService;

        public NotificationController(NotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");
            var empId = HttpContext.Session.GetString("EmployeeId");

            if (role == "Admin")
            {
                var data = notificationService.GetForAdmin();
                return View(data);
            }
            else
            {
                var data = notificationService.GetForEmployee(Convert.ToInt32(empId));
                return View(data);
            }
        }



        [HttpPost]
        public IActionResult MarkAsRead(int id)
        {
            notificationService.MarkAsRead(id);
            return RedirectToAction("Index");
        }
    }
}