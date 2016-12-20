using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IdentitySample.Filters
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string controller = filterContext.RouteData.Values["controller"].ToString();
            string culture = filterContext.RouteData.Values["culture"].ToString();

            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login", culture = culture, ReturnUrl = filterContext.HttpContext.Request.Url.AbsolutePath }));
        }
    }
}