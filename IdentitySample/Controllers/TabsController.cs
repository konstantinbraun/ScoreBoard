using IdentitySample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdentitySample.Resx;
using IdentitySample.DAL;
using PagedList;
using System.Linq.Expressions;
using IdentitySample.Entities;
using IdentitySample.Helpers;
using Microsoft.AspNet.Identity.Owin;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report;

namespace IdentitySample.Controllers
{

    public class Tab: Entity<string>
    {
        public Tab()
        {
            Action = "GetTable";
        }
        public string TemplateName { get; set; }
        public string Caption { get; set; }
        public bool IsActive { get; set; }
        public bool Enabled { get; set; }
        public string Controller { get; set; }
        public string Group { get; set; }
        public string Action { get; set; }
//        public PartialViewDelegate PartialView { get; set; }
    }

    public class BreadCrumbItem
    {
        public BreadCrumbItem()
        {
            Routes = new Dictionary<int, string>();
        }
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public Dictionary<int, string> Routes { get; set; }
    }

    public class Button: Entity<int>
    {
        public string Caption { get; set;}
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Target { get; set; }
        public string Type { get; set; }
    }

    public class PageHeader
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string GroupName { get; set; }
        public List<string> Description { get; set; }
        public List<Button> Buttons { get; set; }
        public List<BreadCrumbItem> BreadCrumbs { get; set; }

        public PageHeader() 
        {
            Description= new List<string>();
            Buttons = new List<Button>();
        }
    }

    public class PageElements {
        public Tab[] Tabs {get; set;}
    }

    public class TabsController1 : BaseController
    {
        private Dictionary<string, PageElements> tabsController;
        private UnitOfWork unitOfWork = new UnitOfWork();
        private int pageSize;
        List<BreadCrumbItem> list = new List<BreadCrumbItem>();
        List<Button> buttons = new List<Button>();

        public TabsController()
        {
            pageSize = 5;
            tabsController = new Dictionary<string, PageElements>();

            tabsController.Add("Home", new PageElements(){
                Tabs = new Tab[] { 
                new Tab() { Id="Tournaments", Caption =AppResource.Tournaments, IsActive = true, Group = "Tournaments",  
                    TemplateName ="_Tournaments", Controller ="Tournaments" },
                new Tab() { Id="Groups", Caption =AppResource.Groups, IsActive = true, Group = "Admin",  
                    TemplateName ="_Groups", Controller ="Groups" },
                new Tab() { Id="Reports", Caption =AppResource.Reports, IsActive = false, Group = "Admin",  
                    TemplateName ="_Reports", Controller ="Reports", Action ="Index"},                
                new Tab() { Id="Roles", Caption =AppResource.Roles, IsActive = false, Group = "Admin",  
                    TemplateName ="_Roles", Controller ="RolesAdmin" },
                new Tab() { Id="Users", Caption =AppResource.Users, IsActive = false, Group = "Admin",  
                    TemplateName ="_Users", Controller ="UsersAdmin"},
                }
            }); 

            tabsController.Add("Tournaments", new PageElements() {
                Tabs =  new Tab[] {
                new Tab() { Id = "Teams", Caption = AppResource.Teams, Enabled = true,
                            TemplateName ="_Teams", IsActive = true,  Controller= "Teams"  },
                new Tab() { Id = "Categories", Caption = AppResource.Categories, Enabled = true, 
                            TemplateName ="_Categories", Controller ="Categories"},
                new Tab() { Id = "Referees", Caption = AppResource.Referees, Enabled = true, 
                            TemplateName ="_Referees", Controller="Referees" },
                new Tab() { Id = "Maps", Caption = AppResource.Map, Enabled = true, Controller ="Tournaments", Action= "GetMap", 
                            TemplateName ="_Map"}
                }
            });

            tabsController.Add("Categories", new PageElements() { 
                Tabs = new Tab[] {
                new Tab() { Id = "ParticipantsCategory", Caption= AppResource.Participants, Action ="GetParticipantsForCategory", 
                            TemplateName ="_Participants", Controller="Categories", IsActive= true},
                new Tab() { Id = "Competitions" , Caption =AppResource.Fights , 
                            TemplateName ="_Competitions", Controller="Categories", Action ="GetFightsForCategory"  }
                }
            });

            tabsController.Add("Teams", new PageElements() {
                Tabs = new Tab[] {
                new Tab() { Id = "Participants", Caption = AppResource.Participants, Enabled = true, 
                            TemplateName ="_Participants", IsActive =true,  Controller ="Participants"  }
                }
            });

            tabsController.Add("Referees", new PageElements() {
                Tabs = new Tab[] {
                new Tab() { Id =  "OpenedCompetitions", Caption = AppResource.UpcomingFights, Enabled = true, 
                            TemplateName ="_Competitions", IsActive=true, Action ="OpenedFights", Controller ="Referees" },
                new Tab() { Id =  "FinishedCompetitions" , Caption = AppResource.FinishedFights , Enabled = true, 
                            TemplateName ="_Competitions",  Action ="FinishedFights", Controller ="Referees"  },
                new Tab() { Id = "Timeline", Caption = AppResource.Timeline,  Enabled =true,
                            TemplateName ="_Timeline", Controller ="Timelines"  }
                }
            });

            tabsController.Add("Groups", new PageElements() {
                Tabs = new Tab[] {
                new Tab() {Id ="Country", Caption = AppResource.Country, Enabled =true, Group = "Public",
                            TemplateName ="_Units", IsActive = true,  Controller="GeneralUnits"  },
                new Tab() {Id ="Level", Caption = AppResource.Round, Enabled =true, Group = "Public",
                            TemplateName ="_Units",   Controller="GeneralUnits" },
                new Tab() {Id ="Ranking", Caption =  AppResource.Ranking, Enabled =true, Group = "Sport",
                            TemplateName ="_Units", IsActive = true,  Controller="SportUnits" },
                new Tab() {Id ="Position", Caption = AppResource.Position, Enabled =true, Group = "Sport",
                            TemplateName ="_Units",  Controller ="SportUnits" },
                new Tab() {Id ="Distinction", Caption = AppResource.Distinction, Enabled =true, Group = "Sport",
                            TemplateName ="_Units", Controller ="SportUnits" }
                }

            });

            tabsController.Add("GeneralUnits", new PageElements() {
                Tabs = new Tab[] {
                new Tab() {Id="Translation", Caption =AppResource.Translation, Enabled = true,
                               TemplateName ="_Translation", IsActive = true,  Controller ="Translations" }
                }
            });

            tabsController.Add("SportUnits", new PageElements()
            {
                Tabs = new Tab[] {
                new Tab() {Id="Translation", Caption =AppResource.Translation, Enabled = true, 
                               TemplateName ="_Translation", IsActive = true, Controller ="Translations" }
                }
            });

        }

        [AllowAnonymous]
        [ChildActionOnly]
        public PartialViewResult GetTabs(string controller, int? id = null, string search = null, int page = 1, string active = "", string group = "")
        {
            List<Tab> tabs;
            Tab activeTab;

            if (string.IsNullOrEmpty(group))
            {
                tabs = tabsController[controller].Tabs.ToList();
            }
            else
            {
                tabs = tabsController[controller].Tabs.Where(x => x.Group == group).ToList();
            }
            if (!string.IsNullOrEmpty(active))
            {
                tabs[0].IsActive = false;
                activeTab = tabs.Where(x => x.Id == active).Single();
            }
            else
            {
                activeTab = tabs.Where(x => x.IsActive).Single();
                active = activeTab.Id;
            }
            activeTab.IsActive = true;
            return PartialView("_Tabs", new TabViewModel() { Id = id, Controller = controller, Search = search, Tabs = tabs, Active=active });
        }

//        [AllowAnonymous]
//        [ChildActionOnly]
//        public PartialViewResult GetTable(int? id, string active, string search = null, int page = 1)
//        {
//            ViewData["search"] = search;
//            var contr = tabsController.Where(x => x.Value.Tabs.Any(y => y.Id == active)).FirstOrDefault();
//            var tab = contr.Value.Tabs.Where(x=>x.Id == active).FirstOrDefault();

////            return PartialView()

//            if (id!=null)
//            {
//                return tab.PartialView((int)id, page, active, tab.TemplateName, contr.Key, tab.Controller);
//            }
//            else
//            {
//                return tab.PartialView(null, page, active, tab.TemplateName, contr.Key, tab.Controller);
//            }
//        }

 

#region -- Functions for tabs delegate --
        private PartialViewResult GetUsersView(int? id, int page, string active, string viewName, string controller, string childController)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var users = unitOfWork.GroupRepository.Get();
            var tab = new TableViewModel<ApplicationUser>() { TableData = userManager.Users.OrderBy(x=>x.FirstName).ToPagedList(page, pageSize), Active = active, Controller = controller, ChildController = childController };
            return PartialView(viewName, tab);
        }
        private PartialViewResult GetRolesView(int? id, int page, string active, string viewName, string controller, string childController)
        {
            var userManager = HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            var users = unitOfWork.GroupRepository.Get();
            var tab = new TableViewModel<RoleViewModel>() { TableData = userManager.Roles.OrderBy(x => x.Name).Select(a => new RoleViewModel() { Id = a.Id, Name =a.Name  }).ToPagedList(page, pageSize), Active = active, Controller = controller, ChildController = childController };
            return PartialView(viewName, tab);
        }
#endregion

#region -- Report options
        public ActionResult GetReportSnapshotViewer()
        {
            StiReport report = new StiReport();
            string path = Server.MapPath("~/App_Data/Reports/ApprovalSheet.mrt");
            report.Load(path);
            report.Compile();
            report.CompiledReport.DataSources[0].Parameters[0].ParameterValue = 1024;
            return StiMvcViewer.GetReportSnapshotResult(report);
        }

        public ActionResult ViewerEvent()
        {
            return StiMvcViewer.ViewerEventResult(this.HttpContext);
        }
#endregion
    }
}