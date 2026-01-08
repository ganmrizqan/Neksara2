using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace NeksaraArief.Controllers.Base
{
    public class BaseAuthenticatedController : Controller
    {
        public override void OnActionExecuting(
            ActionExecutingContext context)
        {
            var adminEmail = context.HttpContext.Session.GetString("AdminEmail");

            if (string.IsNullOrEmpty(adminEmail))
            {
                context.Result =
                    new RedirectToActionResult("Login", "Auth", null);
            }

            base.OnActionExecuting(context);
        }
    }
}
