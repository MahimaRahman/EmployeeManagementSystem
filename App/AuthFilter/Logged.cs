
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.AuthFilter 
{
    public class Logged : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var empId = context.HttpContext.Session.GetString("EmployeeId");

            if (empId == null)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
            }

            base.OnActionExecuting(context);
        }
    }
}