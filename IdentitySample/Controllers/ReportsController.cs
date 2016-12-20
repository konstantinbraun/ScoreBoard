using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdentitySample.Entities;
using IdentitySample.Models;
//using Stimulsoft.Report.Mvc;
//using Stimulsoft.Report;
using IdentitySample.Reports;
using System.IO;
using IdentitySample.Service;
using PagedList;

namespace IdentitySample.Controllers
{
    [Authorize(Roles="Admin")]
    public class ReportsController : BaseController
    {
       // private UnitOfWork unitOfWork = new UnitOfWork();
        IReportService reportService;
        ITournamentService tournamentService;

        public ReportsController(IReportService _reportService, ITournamentService _tournamentService)
        {
            reportService = _reportService;
            tournamentService = _tournamentService;
        }

        [ChildActionOnly]
        public PartialViewResult GetTable(int? page=1, string active="")
        {
            var reports = reportService.GetAll();
            var tab = new TableViewModel<Report>() 
            { 
                TableData = reports.OrderBy(x=>x.Name).ToPagedList((int)page, 5), 
                Active = active, 
                Controller = "Home", 
                ChildController = "Reports" 
            };
            return PartialView("_Reports", tab);
        }


        //public ActionResult Index(string search = null, int page = 1, string active="")
        //{
        //    var reports =  unitOfWork.ReportRepository.Get().ToList();

        //    if (Request.IsAjaxRequest())
        //    {
        //        TabsController tabs = new TabsController();
        //        return tabs.GetTabs("Home", null, search, page, active, "Reports");
        //    }
        //    return View(reports);
        //}

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = reportService.GetById((int)id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }
        
        public IEnumerable<SelectListItem> GetFiles()
        {
            DirectoryInfo info = new System.IO.DirectoryInfo(Server.MapPath("~/App_Data/Reports"));
            IEnumerable<SelectListItem> files = info.GetFiles("*.mrt").Select(x => new SelectListItem() { Text = x.Name, Value = x.Name });
            return files;
        }

        public ActionResult Create()
        {
           DirectoryInfo info = new System.IO.DirectoryInfo(Server.MapPath("~/App_Data/Reports"));
            IEnumerable<SelectListItem> files = info.GetFiles("*.mrt").Select(x => new SelectListItem(){ Text= x.Name, Value = x.Name });

            ReportViewModel reportViewModel = new ReportViewModel() { FileList = files };
            return View(reportViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Report")] ReportViewModel _reportViewModel)
        {
            if (ModelState.IsValid)
            {
                reportService.Create(_reportViewModel.Report);
                return RedirectToAction("Details", "Home", new {@active = "Reports"} );
            }
            _reportViewModel.FileList = GetFiles();
            return View(_reportViewModel);
        }

        public  ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = reportService.GetById((int)id);
            if (report == null)
            {
                return HttpNotFound();
            }

            return View(new ReportViewModel() { Report = report, FileList = GetFiles()});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Report")] ReportViewModel _reportViewModel)
        {
            if (ModelState.IsValid)
            {
                reportService.Update(_reportViewModel.Report);
                return RedirectToAction("Details", "Home", new {@active ="Reports" });
            }
            _reportViewModel.FileList = GetFiles();
            return View(_reportViewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report =reportService.GetById((int)id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(new ReportViewModel() { Report = report, FileList= GetFiles() });
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Report report= reportService.GetById(id);
            reportService.Delete(report);
            return RedirectToAction("Details", "Home", new { @active = "Reports" });
        }

        public ActionResult Manage()
        {
            return View();
        }

        //public ActionResult GetReportSnapshot()
        //{
            //var tournament = tournamentService.GetAll().FirstOrDefault();

            //List<TeamR> teams = tournament.Teams
            //        .Select(s => new TeamR()
            //        {
            //            Name = s.Name,
            //            Coach = s.Coach,
            //            City = s.City,
            //            Country = s.Country.Name,
            //            IsConfirmed = s.ParticipationConfirmed,
            //            Ranking = s.CoachRanking.Name,
            //            Participants = s.Participants
            //                .Select(k => new ParticipantR() 
            //                { 
            //                    FullName = k.FullName,
            //                    Age =  k.Age,
            //                    BirthDate = k.BirthDate,
            //                    Gender = k.Gender.ToString(),
            //                    Ranking =k.Ranking.Name,
            //                    Weight = k.Weight
            //                }).ToList()
            //        }).ToList();
            //StiReport report = StiMvcDesigner.GetReportObject(this.Request);

            //report.ReportName = "Name";
            //report.ReportDescription = tournament.Name;
            
            //report.RegBusinessObject("Teams", teams);
            //report.Dictionary.SynchronizeBusinessObjects(2);

            //return StiMvcDesigner.GetReportSnapshotResult(this.Request, report);

        //}

        //public ActionResult GetReportTemplate()
        //{
        //    List<TeamR> teams = new List<TeamR>();
        //    StiReport report = new StiReport();
        //    report.RegBusinessObject("Teams", teams);
        //    report.Dictionary.SynchronizeBusinessObjects(2);
        //    return StiMvcDesigner.GetReportTemplateResult(report);
        //}

        //public FileResult ExportReport()
        //{
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
            return RedirectToAction("Details", "Home", new { @active = "Reports" });
        }
    }
}
