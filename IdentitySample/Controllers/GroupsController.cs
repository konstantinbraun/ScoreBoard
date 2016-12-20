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
    [CustomAuthorize(Roles="Admin")]
    public class GroupsController : BaseController
    {
        ISportUnitService sportUnitService;
        IGeneralUnitService generalUnitService;
        IGroupService groupService;
        INavigationService navigationService;

        public GroupsController(IGroupService _groupService, INavigationService _navigationService, ISportUnitService _sportUnitService, IGeneralUnitService _generalUnitService )
        {
            sportUnitService = _sportUnitService;
            generalUnitService = _generalUnitService;
            groupService = _groupService;
            navigationService = _navigationService;
        }
        
        private PageHeader GetPageHeader(Group group)
        {
            List<BreadCrumbItem> list = new List<BreadCrumbItem>();
            List<Button> buttons = new List<Button>();
            string groupName;
            string subtitle = "";

            list.Add(new BreadCrumbItem() { Controller = "Home", Name = AppResource.StartPage, Action = "Index" });
            list.Add(new BreadCrumbItem() { Controller = "Administration", Name = AppResource.Administration, Action = "Index" });
            list.Add(new BreadCrumbItem() { Id = group.Id, Name = group.Name, Controller = "Groups" });

            if (group.IsPublicGroup)
            {
                foreach (var unit in (GeneralUnitType[])Enum.GetValues(typeof(GeneralUnitType)))
                {
                    buttons.Add(new Button() { Id = group.Id, Caption = string.Format(AppResource.AddUnit, unit.GetDisplayName()), Target = "_self", Controller = "Units", Action = "CreateGeneral", Type = unit.ToString() });
                }
                groupName = "General";
            }
            else
            {
                foreach (var unit in (SportUnitType[])Enum.GetValues(typeof(SportUnitType)))
                {
                    buttons.Add(new Button() { Id = group.Id, Caption = string.Format(AppResource.AddUnit, unit.GetDisplayName()), Target = "_self", Controller = "Units", Action = "CreateSport", Type = unit.ToString() });
                }
                subtitle = AppResource.Sport;
                groupName = "Sport";
            }
            PageHeader pageHeader = new PageHeader()
            {
                BreadCrumbs = list,
                Buttons = buttons,
                Title = group.Name,
                Subtitle = subtitle,
                GroupName = groupName
            };

            return pageHeader;
        }

        [ChildActionOnly]
        public PartialViewResult GetGeneralUnits(string group, int? id, int? page = 1, string active = "")
        {
            var _group = groupService.GetById((int)id);
            HttpCookie pageSizeCookie = Request.Cookies["_pageSize"];
            int pageSize = Convert.ToInt16(pageSizeCookie.Value);
            var tab = new TableViewModel<Unit>()
            {
                TableData = _group.GeneralUnits.Where(x => x.GeneralType ==  (GeneralUnitType)Enum.Parse(typeof(GeneralUnitType), group)).ToPagedList((int)page, pageSize),
                Active = active,
                Controller = "Groups",
                ChildController = "Units"
            };
            return PartialView("_UnitsGeneral", tab);
        }

        [ChildActionOnly]
        public PartialViewResult GetSportUnits(string group, int? id, int? page = 1, string active = "")
        {
            var _group = groupService.GetById((int)id);
            HttpCookie pageSizeCookie = Request.Cookies["_pageSize"];
            int pageSize = Convert.ToInt16(pageSizeCookie.Value);
            var tab = new TableViewModel<Unit>()
            {
                TableData = _group.SportUnits.Where(x => x.SportType == (SportUnitType)Enum.Parse(typeof(SportUnitType), group)).ToPagedList((int)page, pageSize),
                Active = active,
                Controller = "Groups",
                ChildController = "Units"
            };
            return PartialView("_UnitsSport", tab);
        }

        public ActionResult Details(int? id, string search = null, int page = 1, string active = "")
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Group  group = groupService.GetById((int)id);
            if (group == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PageTabs", navigationService.GetTabs(RouteData, active, group.IsPublicGroup?"General":"Sport"));
            }
            return View(GetPageHeader(group));
        }

        public ActionResult Create()
        {
            bool isPublic = false;
            string groupName = string.Empty;

            if (groupService.GetAll().Count() == 0)
            {
                isPublic = true;
                groupName = "General";
            }
            var group = new Group() { IsPublicGroup = isPublic, Name = groupName };
            return View(group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,IsPublicGroup")] Group group)
        {
            if (ModelState.IsValid)
            {
                groupService.Create(group);
                return RedirectToAction("Index","Administration", null);
            }

            return View(group);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = groupService.GetById((int)id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Group group)
        {
            if (ModelState.IsValid)
            {
                groupService.Update(group);
                return RedirectToAction("Index", "Administration", null);
            }
            return View(group);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = groupService.GetById((int)id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Group group = groupService.GetById(id);
            groupService.Delete(group);
            return RedirectToAction("Index", "Administration", null);
        }
    }
}
