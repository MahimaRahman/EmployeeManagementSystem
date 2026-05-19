using App.AuthFilter;
using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [AdminAccess]
    public class DepartmentController : Controller
    {
        DepartmentService departmentService;

        public DepartmentController(DepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }


        public IActionResult Index()
        {
            var data = departmentService.Get();
            return View(data);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentDTO d)
        {
            if (ModelState.IsValid)
            {
                var res = departmentService.Create(d);

                if (res)
                {
                    TempData["Msg"] = "Department Created Successfully";
                    TempData["Class"] = "alert-success";
                    return RedirectToAction("Index");
                }
            }

            TempData["Msg"] = "Department Create Failed";
            TempData["Class"] = "alert-danger";
            return View(d);
        }


        public IActionResult Details(int id)
        {
            var data = departmentService.Get(id);
            return View(data);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var data = departmentService.Get(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(DepartmentDTO d)
        {
            if (ModelState.IsValid)
            {
                var res = departmentService.Update(d);

                if (res)
                {
                    TempData["Msg"] = "Department Updated Successfully";
                    TempData["Class"] = "alert-success";
                    return RedirectToAction("Index");
                }
            }

            TempData["Msg"] = "Department Update Failed";
            TempData["Class"] = "alert-danger";
            return View(d);
        }



        [HttpPost]
        public IActionResult Delete(int id)
        {
            var res = departmentService.Delete(id);

            if (res)
            {
                TempData["Msg"] = "Department Deleted Successfully";
                TempData["Class"] = "alert-success";
            }
            else
            {
                TempData["Msg"] = "Department Delete Failed";
                TempData["Class"] = "alert-danger";
            }

            return RedirectToAction("Index");
        }
    }
}