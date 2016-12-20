using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using IdentitySample.Entities;
using IdentitySample.Helpers;
using IdentitySample.Models;
using PagedList;
using System.Threading.Tasks;
using IdentitySample.Filters;
using IdentitySample.Service;
using IdentitySample.Resx;


namespace IdentitySample.Controllers
{
    [CustomAuthorize]
    public class TeamsController : BaseController
    {
        ITeamService teamService;
        ITournamentService tournamentService;
        IGeneralUnitService generalUnitService;
        ISportUnitService sportUnitService;
        INavigationService navigationService;

        public TeamsController( ITournamentService _tournamentService, 
                                ITeamService _teamService, 
                                IGeneralUnitService _generalUnitService, 
                                ISportUnitService _sportUnitService,
                                INavigationService _navigationService)
        {
            tournamentService = _tournamentService;
            teamService = _teamService;
            generalUnitService = _generalUnitService;
            sportUnitService = _sportUnitService;
            navigationService = _navigationService;
        }

        IList<SelectListItem> GetCountryList()
        {
            return generalUnitService.GetByType(GeneralUnitType.Country).ToSelectList(x => x.Id, x => x);
        }

        IList<SelectListItem> GetRankingList(Tournament tournament)
        {
            return sportUnitService.GetByTypeForGroup(tournament.GroupId, SportUnitType.Ranking).ToSelectList(x => x.Id, x => x);
        }

        private PageHeader GetPageHeader(Team team)
        {
            List<BreadCrumbItem> list = new List<BreadCrumbItem>();
            List<Button> buttons = new List<Button>();

            if (team.Tournament.Status == TournamentStatus.OpenedForRegistration && team.Coach == User.Identity.Name)
            {
                buttons.Add(new Button() { Target = "_self", Id = team.Id, Caption = AppResource.AddParticipant, Action = "Create", Controller = "Participants" });
            }

            list.Add(new BreadCrumbItem() { Controller = "Home", Name = AppResource.StartPage, Action = "Index" });
            list.Add(new BreadCrumbItem() { Controller = "Tournaments", Name = AppResource.Tournaments, Action = "Index" });
            list.Add(new BreadCrumbItem() { Id = team.Tournament.Id, Name = team.Tournament.Name, Controller = "Tournaments", Action = "Details" });
            list.Add(new BreadCrumbItem()
            {
                Id = team.Id,
                Name = team.Name,
                Controller = "Teams",
                Action = "Details",
                Routes = team.Tournament.Teams.ToDictionary(x => x.Id, y => string.Format("{0}, {1}", y.Name, y.City))
            });
            PageHeader pageHeader = new PageHeader()
            {
                BreadCrumbs = list,
                Title = team.Name,
                Buttons = buttons,
                Subtitle = string.Format("{0}, {1}", team.City, team.Country.Translate()),
                Description = new List<string>() { String.Format("<strong>{0}:</strong> <em>{1} ({2})</em>",AppResource.Coach, team.Coach.GetFullName(), team.CoachRanking.Translate() ),
                                                   String.Format("<strong class='text-{0}'>{1}</strong>",team.ParticipationConfirmed?"success":"danger",team.ParticipationConfirmed?AppResource.ParticipationConfirmed:AppResource.WaitingForConfirmation)}
            };
            return pageHeader;
        }

        [ChildActionOnly]
        [AllowAnonymous]
        public PartialViewResult GetParticipants(int id, int? page = 1, string active = "")
        {
            var team = teamService.GetById((int)id);
            HttpCookie pageSizeCookie = Request.Cookies["_pageSize"];
            int pageSize = Convert.ToInt16(pageSizeCookie.Value);
            
            var tab = new TableViewModel<Participant>()
            {
                TableData = team.Participants.OrderBy(x => x.FirstName).ToPagedList((int)page, pageSize),
                Active = active,
                Controller = "Teams",
                ChildController = "Participants",
                TournamentOrganizer = team.Tournament.Organizer,
                TournamentStatus = team.Tournament.Status
            };

            return PartialView("_Participants", tab);
        }

        [AllowAnonymous]
        public ActionResult Details(int? id, string search = null, int page = 1, string active = "", int? records = null)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Team team = teamService.GetById((int)id); 
            if (team == null)
            {
                return HttpNotFound();
            }

            if (Request.IsAjaxRequest() )
            {
                return PartialView("_PageTabs", navigationService.GetTabs(RouteData,active));
            }
            return View(GetPageHeader(team));
        }

        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tournament = tournamentService.GetById((int) id);
            if (tournament == null)
            {
                return HttpNotFound();
            }
            var team = new Team() { TournamentId = (int)id, Coach = User.Identity.Name };
            TeamViewModel _teamViewModel = new TeamViewModel()
                {
                    Team = team,
                    CountryList = GetCountryList(),
                    RankingList = GetRankingList(tournament)
                };
            return View(_teamViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Team")]TeamViewModel _teamViewModel)
        {
            Tournament tournament = tournamentService.GetById(_teamViewModel.Team.TournamentId);
            if (ModelState.IsValid)
            {
                Team team = teamService.Create(_teamViewModel.Team);
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

                ApplicationUser user = await userManager.FindByNameAsync(tournament.Organizer);
                string subject = string.Format("Team \"{0}\" wants to participate in your tournament \"{1}\".", team.Name, team.Tournament.Name);
                string body = MailTemplateBody.GetBody(user.FullName, String.Format("Team \"{0}\" with the coach {2} want to participate in your tournament \"{1}\". <br/> <br/>Please confirm participation.", team.Name, team.Tournament.Name, team.Coach.GetFullName()));

                await userManager.SendEmailAsync(user.Id, subject, body);
                return RedirectToAction("Details", "Teams", new { id = team.Id });
            }
            _teamViewModel.CountryList = GetCountryList();
            _teamViewModel.RankingList = GetRankingList(tournament);
            return View(_teamViewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = teamService.GetById((int)id);
            if (team == null || !team.Coach.Equals(User.Identity.Name ))
            {
                return HttpNotFound();
            }
            TeamViewModel teamViewModel = new TeamViewModel()
            {
                Team = team,
                CountryList = GetCountryList(),
                RankingList = GetRankingList(team.Tournament)
            };
            return View(teamViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Team")] TeamViewModel _teamViewModel)
        {
            if (ModelState.IsValid)
            {
                teamService.Update(_teamViewModel.Team);
                return RedirectToAction("Details", "Tournaments", new { id = _teamViewModel.Team.TournamentId });
            }
            Tournament tournament = tournamentService.GetById(_teamViewModel.Team.TournamentId); 

            _teamViewModel.CountryList = GetCountryList();
            _teamViewModel.RankingList = GetRankingList(tournament);
            return View(_teamViewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = teamService.GetById((int)id);
            if (team == null || !team.Coach.Equals(User.Identity.Name))
            {
                return HttpNotFound();
            }
            return View(team);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = teamService.GetById(id);
            teamService.Delete(team);
            return RedirectToAction("Details", "Tournaments", new {id = team.TournamentId});
        }

        [HttpPost]
        public async Task<ActionResult> Confirm(int id, string active, int? page = 1 )
        {
            if (Request.IsAjaxRequest())
            {
                var team = teamService.GetById(id);
                team.ParticipationConfirmed = true;
                teamService.Update(team);

                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

                ApplicationUser user = await userManager.FindByNameAsync(team.Coach);
                string subject = string.Format("Your participation in the tournament \"{0}\"  is confirmed.", team.Tournament.Name);
                string body = MailTemplateBody.GetBody(user.FullName, string.Format("Your participation in the tournament \"{0}\"  is confirmed.", team.Tournament.Name));

                await userManager.SendEmailAsync(user.Id, subject, body);
                return RedirectToAction("Details", "Tournaments", new { id = team.TournamentId, active = active, page= page });
            }
            return null;
        }
    }
}
