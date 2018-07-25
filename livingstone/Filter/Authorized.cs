using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using livingstone.Models;

namespace livingstone.Filter
{
    public class Authorized : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            User US = (User)filterContext.HttpContext.Session["user"];
            if (US == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary{{"controller","Account"},
                                    {"action","Login"}

                    });
            }
            base.OnActionExecuted(filterContext);
        }
    }
}