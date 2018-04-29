using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BreakOutGame.Filters
{
    public class SessionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            int? sessionId = context.HttpContext.Session.GetInt32("SessionId");
            if (sessionId.HasValue)
                context.ActionArguments["sessionId"] = sessionId.Value;
            else
                //Rip
                context.Result = new RedirectToActionResult("Index", "Home", null);
            base.OnActionExecuting(context);
        }
    }
}
