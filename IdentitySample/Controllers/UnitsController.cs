using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using IdentitySample.Entities;
using IdentitySample.Models;
using IdentitySample.Filters;
using IdentitySample.Service;
using IdentitySample.Resx;
using IdentitySample.Helpers;

namespace IdentitySample.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class UnitsController : BaseController
    {
        IUnitService unitService;
        IGroupService groupService;
        INavigationService navigationService;
        ISportUnitService sportUnitService;
        IGeneralUnitService generalUnitService;

        public UnitsController( IUnitService _unitService, 
                                ISportUnitService _sportUnitService,
                                IGeneralUnitService _generalUnitService,
                                IGroupService _groupService, 
                                INavigationService _navigationService)
        {
            unitService = _unitService;
            sportUnitService = _sportUnitService;
            groupService = _groupService;
            navigationService = _navigationService;
            generalUnitService = _generalUnitService;
        }

        [ChildActionOnly]
        public PartialViewResult GetTranslations(int id, int? page = 1, string active = "")
        {
            var unit = unitService.GetById(id);
            
            HttpCookie pageSizeCookie = Request.Cookies["_pageSize"];
            int pageSize = Convert.ToInt16(pageSizeCookie.Value);

            var tab = new TableViewModel<Translation>()
            {
                TableData = unit.Translations.OrderBy(x => x.Unit.Name).ToPagedList((int)page, pageSize),
                Active = active,
                Controller = "Units",
                ChildController = "Translations"
            };
            return PartialView("_Translation", tab);
        }

        private PageHeader GetPageHeader(Unit unit)
        {
            List<BreadCrumbItem> list = new List<BreadCrumbItem>();
            List<Button> buttons = new List<Button>();
            string Subtitle;

            var descList = new List<string>();
            
            list.Add(new BreadCrumbItem() { Controller = "Home", Name = AppResource.StartPage, Action = "Index" });
            list.Add(new BreadCrumbItem() { Controller = "Home", Name = AppResource.Administration, Action = "Details" });

            var sportUnit = sportUnitService.GetById(unit.Id);
            if (sportUnit!=null)
            {
                list.Add(new BreadCrumbItem() 
                { 
                    Id = sportUnit.SportGroup_Id, 
                    Name = sportUnit.SportGroup.Name, 
                    Controller = "Groups", 
                    Action = "Details" 
                });
                
                list.Add(new BreadCrumbItem()
                {
                    Id = unit.Id,
                    Name = unit.Name,
                    Controller = "Units",
                    Routes = sportUnit.SportGroup.SportUnits.Where(a => a.SportType == sportUnit.SportType).ToDictionary(x => x.Id, y => y.Name)
                });
                Subtitle = sportUnit.SportType.GetDisplayName();
            }
            else
            {
                var generalUnit = generalUnitService.GetById(unit.Id);
                list.Add(new BreadCrumbItem()
                {
                    Id = generalUnit.GeneralGroup_Id,
                    Name = generalUnit.GeneralGroup.Name,
                    Controller = "Groups",
                    Action = "Details"
                });

                list.Add(new BreadCrumbItem()
                {
                    Id = unit.Id,
                    Name = unit.Name,
                    Controller = "Units",
                    Routes = generalUnit.GeneralGroup.GeneralUnits.Where(a => a.GeneralType  == generalUnit.GeneralType).ToDictionary(x => x.Id, y => y.Name)
                });
                Subtitle = generalUnit.GeneralType.GetDisplayName();
            }

            buttons.Add(new Button() { Target = "_self", Id = unit.Id, Caption = AppResource.AddTranslation, Action = "Create", Controller = "Translations" });

            if (unit.Image != null)
            {
                descList.Add(string.Format("<div class='thumbnail div-max-width'><img alt='{0}' src='/en/Images/Details/{1}?width=300'/></div>", unit.Name, unit.Id));
                buttons.Add(new Button() { Target = "_self", Id = unit.Id, Caption = AppResource.ChangeImage, Action = "Edit", Controller = "Images" });
                buttons.Add(new Button() { Target = "_self", Id = unit.Id, Caption = AppResource.DeleteImage, Action = "Delete", Controller = "Images" });
            }
            else
            {
                buttons.Add(new Button() { Target = "_self", Id = unit.Id, Caption = AppResource.AddImage, Action = "Create", Controller = "Images"});
            }

            PageHeader pageHeader = new PageHeader()
            {
                BreadCrumbs = list,
                Title = unit.Name,
                Buttons = buttons,
                Description = descList,
                Subtitle = Subtitle
            };
            return pageHeader;
        }

        public ActionResult Details(int? id, string search = null, int page = 1, string active = "")
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Unit unit = unitService.GetById((int)id);
            if (unit == null)
            {
                return HttpNotFound();
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PageTabs", navigationService.GetTabs(RouteData, active));
            }

            return View(GetPageHeader(unit));
        }

        public ActionResult CreateSport(int? id, string type)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var group =  groupService.GetById((int)id);
            if (group == null)
            {
                return HttpNotFound();
            }

            SportUnit unit = new SportUnit() { SportGroup_Id = (int)id, SportType = (SportUnitType)Enum.Parse(typeof(SportUnitType), type) };
            return View(unit);
        }

        public ActionResult CreateGeneral(int? id, string type)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var group = groupService.GetById((int)id);
            if (group == null)
            {
                return HttpNotFound();
            }

            GeneralUnit unit = new GeneralUnit() { GeneralGroup_Id = (int)id, GeneralType = (GeneralUnitType)Enum.Parse(typeof(GeneralUnitType), type) };
            return View(unit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSport(SportUnit unit)
        {
            if (ModelState.IsValid)
            {
                sportUnitService.Create(unit); 
                return RedirectToAction("Details", "Groups", new {id = unit.SportGroup_Id, active = unit.SportType.ToString() });
            }
            return View(unit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGeneral(GeneralUnit unit)
        {
            if (ModelState.IsValid)
            {
                generalUnitService.Create(unit);
                return RedirectToAction("Details", "Groups", new { id = unit.GeneralGroup_Id, active = unit.GeneralType.ToString() });
            }
            return View(unit);
        }

        public ActionResult EditSport(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SportUnit unit = sportUnitService.GetById((int)id);   
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        public ActionResult EditGeneral(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralUnit unit = generalUnitService.GetById((int)id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSport([Bind(Include = "Id,Name,SportGroup_Id,SportType,Description,Image")] SportUnit unit)
        {
            if (ModelState.IsValid)
            {

                unitService.Update(unit);
                return RedirectToAction("Details", "Groups", new { id = unit.SportGroup_Id, active = unit.SportType.ToString() });
            }
            return View(unit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGeneral([Bind(Include = "Id,Name,GeneralGroup_Id,GeneralType,Description,Image")] GeneralUnit unit)
        {
            if (ModelState.IsValid)
            {
                unitService.Update(unit);
                return RedirectToAction("Details", "Groups", new { id = unit.GeneralGroup_Id, active = unit.GeneralType.ToString() });
            }
            return View(unit);
        }

        public ActionResult DeleteGeneral(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralUnit unit = generalUnitService.GetById((int)id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        public ActionResult DeleteSport(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SportUnit unit = sportUnitService.GetById((int)id);   
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        [HttpPost, ActionName("DeleteGeneral")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteGeneralConfirmed(int id)
        {
            GeneralUnit gUnit = generalUnitService.GetById(id);
            generalUnitService.Delete(gUnit);
            return RedirectToAction("Details", "Groups", new { id = gUnit.GeneralGroup_Id, active = gUnit.GeneralGroup.ToString() });
        }

        [HttpPost, ActionName("DeleteSport")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSportConfirmed(int id)
        {
            SportUnit sUnit = sportUnitService.GetById(id);
            sportUnitService.Delete(sUnit);
            return RedirectToAction("Details", "Groups", new { id = sUnit.SportGroup_Id, active = sUnit.SportType.ToString() });
        }

    }
}
