using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ProstoAndVkusno.Filters
{
	public class RedirectAuthenticatedUser : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (context.HttpContext.User.Identity.IsAuthenticated)
			{
				context.Result = new RedirectToActionResult("Profile", "Login", null);
			}

			base.OnActionExecuting(context);
		}
	}
}
