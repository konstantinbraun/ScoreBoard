using IdentitySample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
//using Stimulsoft.Report.Mvc;
//using Stimulsoft.Report;
using IdentitySample.Filters;
using IdentitySample.Helpers;
using IdentitySample.Resx;
using IdentitySample.Entities;

namespace IdentitySample.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your app description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}

        //[CustomAuthorize]
        //public ActionResult Report()
        //{
        //    return View();
        //}

        //public ActionResult ViewerEvent()
        //{
        //    return StiMvcViewer.ViewerEventResult(this.HttpContext);
        //}

        //public ActionResult GetReportSnapshotViewer()
        //{
        //    StiReport report = new StiReport();
        //    string path = Server.MapPath("~/App_Data/Reports/ApprovalSheet.mrt");
        //    report.Load(path);
        //    report.Compile();
        //    report.CompiledReport.DataSources[0].Parameters[0].ParameterValue = 1024;               
        //    return StiMvcViewer.GetReportSnapshotResult(report);
        //}

        //public ActionResult GetReportSnapshot()
        //{
        //    return StiMvcDesigner.GetReportSnapshotResult(this.Request);

        //}
        
        //public ActionResult GetReportTemplate()
        //{
        //    StiReport report = new StiReport();
        //    report.Load(Server.MapPath("~/App_Data/Reports/TaxesInCountries.mrt"));
        //    return StiMvcDesigner.GetReportTemplateResult(report);

        //}

        //public FileResult ExportReport()
        //{
        //    //StiReport report = StiMvcDesigner.GetReportObject(this.Request);
        //    return StiMvcDesigner.ExportReportResult(this.Request);
        //}

        //public ActionResult DataProcessing()
        //{
        //    return StiMvcDesigner.DataProcessingResult(this.Request);
        //}

        //public ActionResult GetReportCode()
        //{

        //    return StiMvcDesigner.GetReportCodeResult(this.Request);

        //}

        //public ActionResult GoogleDocs()
        //{

        //    return StiMvcDesigner.GoogleDocsResult(this.Request);

        //}

        public ActionResult ExitDesigner()
        {

            return RedirectToAction("Index");
        }

        public ActionResult SetCulture(string culture, string ReturnUrl)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            RouteData.Values["culture"] = culture;  // set culture
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            string[] retUrl = ReturnUrl.Split('/');

            retUrl[1] = culture;

            string newUrl = string.Join("/", retUrl);

            return Redirect(newUrl);
        }  
    }
}
