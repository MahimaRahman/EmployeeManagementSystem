using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.AuthFilter 
{
    public class AdminAccess : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var role = context.HttpContext.Session.GetString("Role");

            if (role == null)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
            }
            else if (role != "Admin")
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Auth", null);
            }

            base.OnActionExecuting(context);
        }
    }
}