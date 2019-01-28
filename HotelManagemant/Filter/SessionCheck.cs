using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HotelManagemant.Filters
{
    public class SessionCheck: ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["id"] == null)
            {
                //filterContext.Result = new RedirectResult("~/Account/Login");
                //return;

                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary 
                { 
                    { "controller", "Account" }, 
                    { "action", "Login" } 
                });

            }
           
            
            base.OnActionExecuting(filterContext);
        }
        
    }
}