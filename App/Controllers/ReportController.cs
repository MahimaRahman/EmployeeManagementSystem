using App.AuthFilter;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [AdminAccess]
    public class ReportController : Controller
    {
        ReportService reportService;

        public ReportController(ReportService reportService)
        {
            this.reportService = reportService;
        }

        public IActionResult DepartmentSummary()
        {
            var data = reportService.DepartmentSummary();
            return View(data);
        }
    }
}