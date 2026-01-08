using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NeksaraArief.Controllers
{
    public class BaseAuthenticatedController : Controller
    {
        public override void OnActionExecuting(
            ActionExecutingContext context)
        {
            var email = context.HttpContext.Session.GetString("AdminEmail");

            if (string.IsNullOrEmpty(email))
            {
                context.Result =
                    new RedirectToActionResult("Login", "Auth", null);
            }

            base.OnActionExecuting(context);
        }
    }
}
