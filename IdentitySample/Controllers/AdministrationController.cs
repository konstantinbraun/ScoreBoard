using IdentitySample.Entities;
using IdentitySample.Filters;
using IdentitySample.Models;
using IdentitySample.Resx;
using IdentitySample.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;

namespace IdentitySample.Controllers
{
    [CustomAuthorize(Roles="Admin")]
    public class AdministrationController : BaseController
    {
        IGroupService groupService;
        INavigationService navigationService;

        public AdministrationController(IGroupService _groupService, INavigationService _navigationService)
        {
            groupService = _groupService;
            navigationService = _navigationService;
        }

        [ChildActionOnly]
        public PartialViewResult GetGroups(int? page = 1, string active = "")
        {
            var groups = groupService.GetAll().OrderBy(x => x.Name);
            HttpCookie pageSizeCookie = Request.Cookies["_pageSize"];
            int pageSize = Convert.ToInt16(pageSizeCookie.Value);

            var tab = new TableViewModel<Group>()
            {
                TableData = groups.ToPagedList((int)page, pageSize),
                Active = active,
                Controller = "Administration",
                ChildController = "Groups"
            };
            return PartialView("_Groups", tab);
        }

        [ChildActionOnly]
        public PartialViewResult GetUsers(int? page = 1, string active = "")
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var users = userManager.Users.OrderBy(x => x.FirstName);
            HttpCookie pageSizeCookie = Request.Cookies["_pageSize"];
            int pageSize = Convert.ToInt16(pageSizeCookie.Value);

            var tab = new TableViewModel<ApplicationUser>()
            {
                TableData = users.ToPagedList((int)page, pageSize),
                Active = active,
                Controller = "Administration",
                ChildController = "UsersAdmin"
            };
            return PartialView("_Users", tab);
        }

        public ActionResult Index(string search = null, int? page = 1, string active = "")
        {
            IEnumerable<Group> groups = groupService.GetAll();

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PageTabs", navigationService.GetTabs(RouteData, active));
            }
            return View(GetPageHeader());
        }

        private PageHeader GetPageHeader()
        {
            List<BreadCrumbItem> list = new List<BreadCrumbItem>();
            List<Button> buttons = new List<Button>();
            string title = "";

            list.Add(new BreadCrumbItem() { Controller = "Home", Name = AppResource.StartPage, Action = "Index" });
            list.Add(new BreadCrumbItem() { Controller = "Administration", Name = AppResource.Administration, Action = "Index" });
            
            buttons.Add(new Button() { Target = "_self", Caption = AppResource.AddGroup, Action = "Create", Controller = "Groups" });
            buttons.Add(new Button() { Target = "_self", Caption = "Add report", Action = "Create", Controller = "Reports" });
            buttons.Add(new Button() { Target = "_self", Caption = "Manage report", Action = "Manage", Controller = "Reports" });
            title = AppResource.Administration;

            PageHeader pageHeader = new PageHeader()
            {
                Title = title,
                Buttons = buttons,
                BreadCrumbs = list
            };

            return pageHeader;
        }
    }
}