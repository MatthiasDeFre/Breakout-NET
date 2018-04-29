using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BreakOutGame.Filters
{
    public class GroupFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            int? groupId = context.HttpContext.Session.GetInt32("groupId");
            if (groupId.HasValue)
                context.ActionArguments["groupId"] = groupId.Value;
            else
                //Rip
                context.Result = null;
            base.OnActionExecuting(context);
        }
    }
}
