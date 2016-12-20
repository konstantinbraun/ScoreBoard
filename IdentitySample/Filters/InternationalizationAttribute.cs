using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace IdentitySample.Filters
{
    public class InternationalizationAttribute : ActionFilterAttribute 
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //if ((string)filterContext.RouteData.Values["language"] == null || (string)filterContext.RouteData.Values["culture"] == null)
            //{

            //}
            //else
            //{
            //    string language = (string)filterContext.RouteData.Values["language"];
            //    string culture = (string)filterContext.RouteData.Values["culture"];

            //    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(string.Format("{0}-{1}", language, culture));
            //    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(string.Format("{0}-{1}", language, culture));
            //}
        }
    }
}