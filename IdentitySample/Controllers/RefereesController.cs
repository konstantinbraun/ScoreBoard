using IdentitySample.Helpers;
using IdentitySample.Entities;
using IdentitySample.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdentitySample.Filters;
using IdentitySample.Service;
using IdentitySample.Resx;

namespace IdentitySample.Controllers
{
    [CustomAuthorize]
    public class RefereesController : BaseController
    {
        IRefereeService refereeService;
        ITournamentService tournamentService;
        ISportUnitService sportUnitService;
        IFightService fightService;
        IFighterService fighterService;
        INavigationService navigationService;
        
        public RefereesController(IRefereeService _refereeService, 
                                  ITournamentService _tournamentService, 
                                  ISportUnitService _sportUnitService, 
                                  IFightService _fightService, 
                                  IFighterService _fighterService,
                                  INavigationService _navigationService)
        {
            tournamentService = _tournamentService;
            refereeService = _refereeService;
            sportUnitService = _sportUnitService;
            fightService = _fightService;
            fighterService = _fighterService;
            navigationService = _navigationService;
        }

        IList<SelectListItem> GetPositionList(Tournament tournament)
        {
            return sportUnitService.GetByTypeForGroup(tournament.GroupId, SportUnitType.Position).ToSelectList(x => x.Id, x => x);
        }

        private PageHeader GetPageHeader(Referee referee)
        {
            List<BreadCrumbItem> list = new List<BreadCrumbItem>();
            List<Button> buttons = new List<Button>();

            if (referee.Tournament.Organizer == User.Identity.Name && referee.Tournament.DateFrom > DateTime.Now)
            {
                buttons.Add(new Button() { Target = "_self", Id = referee.Id, Caption = AppResource.AddTimeline, Action = "Create", Controller = "Timelines" });
            }
            buttons.Add(new Button() { Target = "_blank", Id = referee.Id, Caption = "Display", Action = "DisplayFullHD", Controller = "Referees" });
            list.Add(new BreadCrumbItem() { Controller = "Home", Name = AppResource.StartPage, Action = "Index" });
            list.Add(new BreadCrumbItem() { Controller = "Tournaments", Name = AppResource.Tournaments, Action = "Index" });
            list.Add(new BreadCrumbItem() { Id = referee.Tournament.Id, Name = referee.Tournament.Name, Controller = "Tournaments", Action = "Details" });
            list.Add(new BreadCrumbItem()
            {
                Id = referee.Id,
                Name = referee.Position.Translate(),
                Controller = "Referees",
                Action = "Details",
                Routes = referee.Tournament.Referees.ToDictionary(x => x.Id, y => y.Position.Translate())
            });

            PageHeader pageHeader = new PageHeader()
            {
                BreadCrumbs = list,
                Title = referee.Position.Translate(),
                Subtitle = referee.RefereeEmail.GetFullName(),
                Buttons = buttons
            };

            return pageHeader;
        }

        [ChildActionOnly]
        [AllowAnonymous]
        public PartialViewResult GetTimeline(int? id, int? page = 1, string active = "")
        {
            Referee referee = refereeService.GetById((int)id);
            HttpCookie pageSizeCookie = Request.Cookies["_pageSize"];
            int pageSize = Convert.ToInt16(pageSizeCookie.Value);


            var tab = new TableViewModel<Timeline>()
            {
                TableData = referee.Timelines.OrderBy(x => x.RefereeId).ToPagedList((int)page, pageSize),
                Active = active,
                Controller = "Referees",
                ChildController = "Timelines"
            };
            return PartialView("_Timeline", tab);
        }

        [ChildActionOnly]
        [AllowAnonymous]
        public PartialViewResult GetUpcomingFights(int id, int? page = 1, string active = "")
        {
            var referee = refereeService.GetById((int)id);
            var fights = fightService.GetWithFilter(x => x.Category.Timelines.Any(s => s.LevelId == x.LevelId && s.RefereeId == id) && x.Fighters.Where(y => y.IsWinner).Count() == 0);
            HttpCookie pageSizeCookie = Request.Cookies["_pageSize"];
            int pageSize = Convert.ToInt16(pageSizeCookie.Value);

            string email = string.Empty;
            if (referee != null)
            {
                email = referee.RefereeEmail;
            }
            var tab = new TableViewModel<Fight>() 
            {
                TableData = fights.OrderBy(x => x.Category.Timelines.Where(s => s.LevelId == x.LevelId && s.RefereeId == id).FirstOrDefault().Id).ThenByDescending(k => k.FightInLevel).ToPagedList((int)page, pageSize), 
                Active = active, 
                Controller = "Referees", 
                ChildController = "Test", 
                TournamentOrganizer = email
            };
            return PartialView("_Competitions", tab);
        }

        [ChildActionOnly]
        [AllowAnonymous]
        public PartialViewResult GetFinishedFights(int id, int? page = 1, string active = "")
        {
            var referee = refereeService.GetById((int)id);
            var fights = fightService.GetWithFilter(x => x.Category.Timelines.Any(s => s.LevelId == x.LevelId && s.RefereeId == id) && x.Fighters.Where(y=>y.IsWinner).Count()== 1);
            HttpCookie pageSizeCookie = Request.Cookies["_pageSize"];
            int pageSize = Convert.ToInt16(pageSizeCookie.Value);

            string email = string.Empty;
            if (referee != null)
            {
                email = referee.RefereeEmail;
            }
            var tab = new TableViewModel<Fight>()
            {
                TableData = fights.OrderBy(x => x.Category.Timelines.Where(s => s.LevelId == x.LevelId && s.RefereeId == id).FirstOrDefault().Id).ThenByDescending(k=>k.FightInLevel).ToPagedList((int)page, pageSize),
                Active = active,
                Controller = "Referees",
                ChildController = "Test",
                TournamentOrganizer = email
            };
            return PartialView("_Competitions", tab);
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
            RefereeViewModel refereeViewModel = new RefereeViewModel
            {
                Referee = new Referee() { TournamentId = (int)id },
                PositionList = GetPositionList(tournament)
            };
            return View(refereeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Referee")] RefereeViewModel _refereeViewModel)
        {
            if (ModelState.IsValid)
            {
                refereeService.Create(_refereeViewModel.Referee);
                return RedirectToAction("Details", "Tournaments", new { id = _refereeViewModel.Referee.TournamentId, active = "Referees" });
            }
            Tournament tournament = tournamentService.GetById(_refereeViewModel.Referee.TournamentId);
            _refereeViewModel.PositionList = GetPositionList(tournament);

            return View(_refereeViewModel);
        }

        public ActionResult DisplayFullHD(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Referee referee = refereeService.GetById((int)id);
            if (referee == null)
            {
                return HttpNotFound();
            }

            var currentFights = fightService.GetWithFilter(x => x.Category.Timelines.Any(s => s.LevelId == x.LevelId && s.RefereeId == id) && x.Fighters.Where(y => y.IsWinner).Count() == 0);

            var currentFight = currentFights.FirstOrDefault();
            var nextFight = currentFights.Skip(1).FirstOrDefault();
            var displayViewModel = new DisplayViewModel() { CurrentFight = currentFight, NextFight = nextFight };

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Display", displayViewModel);
            }
            return View(displayViewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Referee referee = refereeService.GetById((int)id);
            if (referee == null)
            {
                return HttpNotFound();
            }

            RefereeViewModel refereeEditViewModel = new RefereeViewModel() 
                { 
                    Referee = referee,
                    PositionList = GetPositionList(referee.Tournament)
                };

            return View(refereeEditViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Referee")] RefereeViewModel refereeEditViewModel)
        {
            if (ModelState.IsValid)
            {
                refereeService.Update(refereeEditViewModel.Referee);
                return RedirectToAction("Details", "Tournaments", new { id = refereeEditViewModel.Referee.TournamentId, active ="Referees" });
            }
            Tournament tournament = tournamentService.GetById(refereeEditViewModel.Referee.TournamentId);
            refereeEditViewModel.PositionList = GetPositionList(tournament); 
            return View(refereeEditViewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Referee referee = refereeService.GetById((int)id);   
            if (referee == null)
            {
                return HttpNotFound();
            }
            return View(referee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Referee referee = refereeService.GetById((int)id);
            refereeService.Delete(referee);
            return RedirectToAction("Details", "Tournaments", new {id= referee.TournamentId, active ="Referees" });
        }

        [AllowAnonymous]
        public ActionResult Details(int? id, string search = null, int page = 1, string active = "")
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Referee referee = refereeService.GetById((int)id);
            if (referee == null)
            {
                return HttpNotFound();
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PageTabs", navigationService.GetTabs(RouteData, active));
            }

            return View(GetPageHeader(referee));
        }

        public ActionResult SetWinner(int Id, int refereeId)
        {
            if (Request.IsAjaxRequest())
            {
                Fighter winner = fighterService.GetById(Id);
                winner.IsWinner = true;
                fighterService.Update(winner);

                SportUnit[] distinctions = sportUnitService.GetByTypeForGroup(winner.Fight.Category.Tournament.GroupId, SportUnitType.Distinction).ToArray();

                var nextWinnerFight = winner.Fight.FightRelationsChildFights.Where(x => x.IsWinner).SingleOrDefault();
                if (nextWinnerFight != null)
                {
                    int fighterCount = nextWinnerFight.FightParent.Fighters.Count();
                    Fighter fighter = new Fighter() { FightId = (int)nextWinnerFight.FightParentId, ParticipantId = winner.ParticipantId, DistinctionId = distinctions[fighterCount].Id };
                    fighterService.Create(fighter);
                }

                var nexLoserFight = winner.Fight.FightRelationsChildFights.Where(x => !x.IsWinner).SingleOrDefault();
                if (nexLoserFight != null)
                {
                    var loser = winner.Fight.Fighters.Where(x => x.Id != Id).Single();
                    int fighterCount = nexLoserFight.FightParent.Fighters.Count();
                    Fighter fighter = new Fighter() { FightId = (int)nexLoserFight.FightParentId, ParticipantId = loser.ParticipantId, DistinctionId = distinctions[fighterCount].Id };
                    fighterService.Create(fighter);
                }
                return RedirectToAction("Details", "Referees", new { id = refereeId });
            }
            return null;
        }
    }
}
