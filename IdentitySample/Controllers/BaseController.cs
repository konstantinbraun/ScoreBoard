using IdentitySample.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace IdentitySample.Controllers
{
    public class BaseController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = RouteData.Values["culture"] as string;
            if (cultureName == null)
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null; // obtain it from HTTP header AcceptLanguage
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
                        Request.UserLanguages[0] :  // obtain it from HTTP header AcceptLanguages
                        null;


            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            // Modify current thread's cultures           
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            HttpCookie pageSizeCookie;
            pageSizeCookie = Request.Cookies["_pageSize"];
            if (pageSizeCookie == null)
            {
                pageSizeCookie = new HttpCookie("_pageSize", "10");
                Response.AppendCookie(pageSizeCookie);
            }

            if (!string.IsNullOrEmpty(Request["records"] ) )
            {
                pageSizeCookie.Value = Request["records"];
                Response.AppendCookie(pageSizeCookie);
            }

            

            return base.BeginExecuteCore(callback, state);
        }

    }
}