using App.AuthFilter;
using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Logged]
    public class LeaveController : Controller
    {
        LeaveService leaveService;
        NotificationService notificationService;

        public LeaveController(LeaveService leaveService, NotificationService notificationService)
        {
            this.leaveService = leaveService;
            this.notificationService = notificationService;
        }

        [AdminAccess]
        public IActionResult Pending()
        {
            var data = leaveService.GetPending();
            return View(data);
        }

        [HttpGet]
        public IActionResult Apply()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Apply(LeaveRequestDTO l)
        //{
        //    var empId = HttpContext.Session.GetString("EmployeeId");

        //    if (empId == null)
        //    {
        //        return RedirectToAction("Login", "Auth");
        //    }

        //    l.EmployeeId = Convert.ToInt32(empId);

        //    var res = leaveService.ApplyLeave(l);

        //    //if (res)
        //    //{
        //    //    TempData["Msg"] = "Leave Applied Successfully";
        //    //    TempData["Class"] = "alert-success";
        //    //    return RedirectToAction("MyLeaves");
        //    //}

        //    if (res)
        //    {
        //        var uname = HttpContext.Session.GetString("Uname");
        //        notificationService.CreateForAdmin(uname + " applied for leave");

        //        TempData["Msg"] = "Leave Applied Successfully";
        //        TempData["Class"] = "alert-success";
        //        return RedirectToAction("MyLeaves");
        //    }


        //    TempData["Msg"] = "Leave Apply Failed";
        //    TempData["Class"] = "alert-danger";
        //    return View(l);
        //}


        [HttpPost]
        public IActionResult Apply(LeaveRequestDTO l)
        {
            if (ModelState.IsValid)
            {
                var empId = HttpContext.Session.GetString("EmployeeId");

                if (empId == null)
                {
                    return RedirectToAction("Login", "Auth");
                }

                l.EmployeeId = Convert.ToInt32(empId);

                var res = leaveService.ApplyLeave(l);

                if (res)
                {
                    var uname = HttpContext.Session.GetString("Uname");
                    notificationService.CreateForAdmin(uname + " applied for leave");

                    TempData["Msg"] = "Leave Applied Successfully";
                    TempData["Class"] = "alert-success";
                    return RedirectToAction("MyLeaves");
                }

                TempData["Msg"] = "Leave Apply Failed";
                TempData["Class"] = "alert-danger";
            }

            return View(l);
        }




        public IActionResult MyLeaves()
        {
            var empId = HttpContext.Session.GetString("EmployeeId");

            if (empId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var data = leaveService.GetByEmployee(Convert.ToInt32(empId));
            return View(data);
        }

        //[AdminAccess]
        //public IActionResult Review(int id, string status)
        //{
        //    var reviewedBy = HttpContext.Session.GetString("Email");

        //    var res = leaveService.ReviewLeave(id, status, reviewedBy);

        //    if (res)
        //    {
        //        TempData["Msg"] = "Leave Reviewed Successfully";
        //        TempData["Class"] = "alert-success";
        //    }
        //    else
        //    {
        //        TempData["Msg"] = "Leave Review Failed";
        //        TempData["Class"] = "alert-danger";
        //    }

        //    return RedirectToAction("Pending");
        //}

        //[AdminAccess]
        //public IActionResult Review(int id, string status)
        //{
        //    var reviewedBy = HttpContext.Session.GetString("Email");

        //    var leave = leaveService.Get(id);

        //    var res = leaveService.ReviewLeave(id, status, reviewedBy);

        //    if (res)
        //    {
        //        notificationService.CreateForEmployee(
        //            leave.EmployeeId,
        //            "Your leave request has been " + status
        //        );

        //        TempData["Msg"] = "Leave Reviewed Successfully";
        //        TempData["Class"] = "alert-success";
        //    }
        //    else
        //    {
        //        TempData["Msg"] = "Leave Review Failed";
        //        TempData["Class"] = "alert-danger";
        //    }

        //    return RedirectToAction("Pending");
        //}


        [AdminAccess]
        [HttpPost]
        public IActionResult Review(int id, string status)
        {
            var reviewedBy = HttpContext.Session.GetString("Email");

            var leave = leaveService.Get(id);

            if (leave == null)
            {
                TempData["Msg"] = "Leave request not found";
                TempData["Class"] = "alert-danger";
                return RedirectToAction("Pending");
            }

            if (status != "Approved" && status != "Rejected")
            {
                TempData["Msg"] = "Invalid leave status";
                TempData["Class"] = "alert-danger";
                return RedirectToAction("Pending");
            }

            var res = leaveService.ReviewLeave(id, status, reviewedBy);

            if (res)
            {
                notificationService.CreateForEmployee(
                    leave.EmployeeId,
                    "Your leave request has been " + status
                );

                TempData["Msg"] = "Leave Reviewed Successfully";
                TempData["Class"] = "alert-success";
            }
            else
            {
                TempData["Msg"] = "Leave Review Failed";
                TempData["Class"] = "alert-danger";
            }

            return RedirectToAction("Pending");
        }





        [AdminAccess]
        public IActionResult Index()
        {
            var data = leaveService.Get();
            return View(data);
        }
    }
}