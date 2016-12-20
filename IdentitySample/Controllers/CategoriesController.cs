using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdentitySample.Entities;
using IdentitySample.Models;
using System.Linq.Expressions;
using PagedList;
using IdentitySample.Filters;
using IdentitySample.Service;
using IdentitySample.Resx;
using IdentitySample.Helpers;

namespace IdentitySample.Controllers
{
    [CustomAuthorize]
    public class CategoriesController : BaseController
    {
        ICategoryService categoryService;
        ITournamentService tournamentService;
        IFightService fightService;
        INavigationService navigationService;
        IFighterService fighterService;

        public CategoriesController(ICategoryService _categoryService, 
                                    ITournamentService _tournamentService,
                                    IFightService _fightService, 
                                    IFighterService _fighterService,
                                    INavigationService _navigationService)
        {
            categoryService = _categoryService;
            tournamentService = _tournamentService;
            fightService = _fightService;
            navigationService = _navigationService;
            fighterService = _fighterService;
        }

        [ChildActionOnly]
        [AllowAnonymous]
        public PartialViewResult GetParticipantsForCategory(int id, int? page = 1, string active = "")
        {
            var category = categoryService.GetById(id);
            var participants = categoryService.GetParticipants(id, false);
            HttpCookie pageSizeCookie = Request.Cookies["_pageSize"];
            int pageSize = Convert.ToInt16(pageSizeCookie.Value);

            var tab = new TableViewModel<Participant>()
            {
                TableData = participants.OrderBy(x => x.FirstName).ToPagedList((int)page, pageSize ),
                Active = active,
                Controller = "Categories",
                ChildController = "Participants",
                TournamentOrganizer = category.Tournament.Organizer,
                TournamentStatus = category.Tournament.Status
            };
            return PartialView("_Participants", tab);
        }

        [ChildActionOnly]
        [AllowAnonymous]
        public PartialViewResult GetFightsForCategory(int? id, int? page=1, string active="")
        {
            Category category = categoryService.GetById((int)id);
            HttpCookie pageSizeCookie = Request.Cookies["_pageSize"];
            int pageSize = Convert.ToInt16(pageSizeCookie.Value);

            var tab = new TableViewModel<Fight>() 
            {
                TableData = category.Fights.OrderByDescending(x => x.Level.Description).ThenByDescending(y => y.FightInLevel).ToPagedList((int)page, pageSize), 
                Active = active, 
                Controller = "Categories", 
                ChildController = "Fights",
                TournamentOrganizer = category.Tournament.Organizer 
            };
            return PartialView("_Competitions", tab);
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public int GetParticipantsCount(int id)
        {
            return categoryService.GetParticipants(id, false).Count(); 
        }

        private PageHeader GetPageHeader(Category category)
        {
            List<BreadCrumbItem> list = new List<BreadCrumbItem>();
            List<Button> buttons = new List<Button>();

            var descList = new List<string>();
            list.Add(new BreadCrumbItem() { Controller = "Home", Name = AppResource.StartPage, Action = "Index" });
            list.Add(new BreadCrumbItem() { Controller = "Tournaments", Name = AppResource.Tournaments, Action = "Index" });
            list.Add(new BreadCrumbItem() { Id = category.Tournament.Id, Name = category.Tournament.Name, Controller = "Tournaments", Action = "Details" });
            list.Add(new BreadCrumbItem()
            {
                Id = category.Id,
                Name = String.Format("{0} [{1}] [{2}]", category.Gender.GetDisplayName(), category.Age, category.Weight),
                Controller = "Categories",
                Action = "Details",
                Routes = category.Tournament.Categories.ToDictionary(x => x.Id, y => String.Format("{0} [{1}] [{2}]", y.Gender.GetDisplayName(), y.Age, y.Weight))
            });

            descList.Add(string.Format("<strong>{0}:</strong> <em>{1}</em>", AppResource.Description, category.OneThirdPlace ? AppResource.OneThirdPlace : AppResource.TwoThirdPlaces));

            var finalFight = fightService.GetWithFilter(x => x.CategoryId == category.Id && x.Level.Description.Equals("level_1") && x.Fighters.Any(s => s.IsWinner)).SingleOrDefault();
            if (finalFight != null)
            {
                var winner = finalFight.Fighters.Where(x => x.IsWinner).Single();
                var winner2 = finalFight.Fighters.Where(x => !x.IsWinner).Single();
                descList.Add(GetWinnerListItem(1, winner));
                descList.Add(GetWinnerListItem(2, winner2));
            }

            if (category.OneThirdPlace)
            {
                var thirdPlaceFight = fightService.GetWithFilter(filter: x => x.CategoryId == category.Id && x.Level.Description.Equals("level_0") && x.Fighters.Any(s => s.IsWinner)).SingleOrDefault();
                if (thirdPlaceFight != null)
                {
                    var winner3 = thirdPlaceFight.Fighters.Where(x => x.IsWinner).Single();
                    descList.Add(GetWinnerListItem(3, winner3));
                }
            }
            else
            {
                var semiFinalFights = fightService.GetWithFilter(filter: x => x.CategoryId == category.Id && x.Level.Description.Equals("level_2") && x.Fighters.Any(s => s.IsWinner));
                foreach (Fight semiFinalFight in semiFinalFights)
                {
                    var winner3 = semiFinalFight.Fighters.Where(x => x.IsWinner).Single();

                    descList.Add(GetWinnerListItem(3, winner3));
                }
            }
            PageHeader pageHeader = new PageHeader()
            {
                BreadCrumbs = list,
                Title = category.Gender.GetDisplayName(),
                Subtitle = string.Format("[{0}] [{1}]", category.Age, category.Weight),
                Description = descList
            };            
            
            return pageHeader;
        }

        [AllowAnonymous]
        public ActionResult Details(int? id, string search = null, int page = 1, string active = "")
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryService.GetById((int)id) ;
            if (category == null)
            {
                return HttpNotFound();
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PageTabs", navigationService.GetTabs(RouteData, active));
            }
            return View(GetPageHeader(category));
        }

        public ActionResult Replace(int first, int second)
        {
            if (Request.IsAjaxRequest())
            {
                int CategoryId = fighterService.Replace(first, second);
                return RedirectToAction("Details", new { id = CategoryId, active ="Competitions"});
            }
            return null;
        }

        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tournament = tournamentService.GetById((int)id);
            if (tournament == null)
            {
                return HttpNotFound();
            }

            Category category = new Category() { TournamentId = (int)id };
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AgeFrom,AgeTo,TournamentId,WeightFrom,WeightTo,Gender,OneThirdPlace")]Category category)
        {
            if (ModelState.IsValid)
            {
                categoryService.Create(category);
                return RedirectToAction("Details", "Tournaments", new {id = category.TournamentId, active="Categories"});
            }
            return View(category);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryService.GetById((int)id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Gender,AgeFrom,AgeTo,WeightFrom,WeightTo,TournamentId,OneThirdPlace")]Category category)
        {
            if (ModelState.IsValid)
            {
                categoryService.Update(category);
                return RedirectToAction("Details", "Tournaments", new { id = category.TournamentId, active="Categories" });
            }
            return View(category);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryService.GetById((int)id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category =  categoryService.GetById((int)id);
            categoryService.Delete(category);
            return RedirectToAction("Details", "Tournaments", new { id = category.TournamentId, active = "Categories" });
        }

        private string GetWinnerListItem(int place, Fighter fighter)
        {
            if (fighter.Participant.Team.Country.Image != null)
            {
                return string.Format("{0}.{1}: <strong>{2}</strong> <em>{3} {4}</em> <img src='/en/Images/Details/{5}' alt='{6}' />", place, AppResource.Place, fighter.Participant.FullName, AppResource.From, fighter.Participant.Team.Name, fighter.Participant.Team.CountryId, fighter.Participant.Team.Country.Name);
            }
            else
            {
                return string.Format("{0}.{1}: <strong>{2}</strong> <em>{3} {4} ({5})</em>", place, AppResource.Place, fighter.Participant.FullName, AppResource.From, fighter.Participant.Team.Name, fighter.Participant.Team.Country.Name);
            }
        }
    }
}
