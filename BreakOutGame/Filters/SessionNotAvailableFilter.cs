using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;

namespace BreakOutGame.Filters
{
    public class SessionNotAvailableFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
           
            int? sessionId = context.HttpContext.Session.GetInt32("sessionId");
            if (!sessionId.HasValue)
            {


            
              
                //redirect.RouteValues.Add("error", "Neem eerst deel aan een sessie");
                //TempData["groupchosen"] = "Kies een groep aub";
                RedirectToActionResult redirect = new RedirectToActionResult("Index", "Home", null);
                context.Result = redirect;
            }
        }
    }
}
