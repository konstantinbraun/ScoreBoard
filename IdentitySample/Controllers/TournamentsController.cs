using System;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using IdentitySample.Entities;
using IdentitySample.Models;
using PagedList;
using IdentitySample.Helpers;
using IdentitySample.Filters;
using IdentitySample.Service;
using IdentitySample.ViewModels;
using IdentitySample.Resx;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using IdentitySample.ViewModels.Common;

namespace IdentitySample.Controllers
{
    [CustomAuthorize]
    public class TournamentsController : BaseController
    {
        ITournamentService tournamentService;
        IGeneralUnitService generalUnitService;
        ISportUnitService sportUnitService;
        IGroupService groupService;
        INavigationService navigationService;
        ITeamService teamService;
        ICategoryService categoryService;
        IFightService fightService;
        IFighterService fighterService;
        IFightRelationService fightRelationService;

        public TournamentsController(ITournamentService _tournamentService, 
                                     IGroupService _groupService, 
                                     IGeneralUnitService _generalUnitservice, 
                                     ISportUnitService _sportUnitService,
                                     ICategoryService _categoryService,
                                     INavigationService _navigationService,
                                     ITeamService _teamService,
                                     IFightService _fightService,
                                     IFighterService _fighterService,
                                     IFightRelationService _fightRelationService                                     )
        {
            tournamentService = _tournamentService;
            generalUnitService = _generalUnitservice;
            groupService = _groupService;
            navigationService = _navigationService;
            sportUnitService = _sportUnitService;
            categoryService = _categoryService;
            teamService = _teamService;
            fightService = _fightService;
            fighterService = _fighterService;
            fightRelationService = _fightRelationService;
        }

        IList<SelectListItem> GetCountryList()
        {
            return generalUnitService.GetByType(GeneralUnitType.Country).ToSelectList(x => x.Id, x => x);
        }

        IList<SelectListItem> GetSportGroupList()
        {
            return groupService.GetSports().ToSelectList(x => x.Id, x => x.Name);
        }

        [ChildActionOnly]
        [AllowAnonymous]
        public PartialViewResult GetTournaments(int? page = 1, string active = "")
        {
            var tournaments = tournamentService.GetAll().OrderBy(x => x.DateFrom);
            HttpCookie pageSizeCookie = Request.Cookies["_pageSize"];
            int pageSize = Convert.ToInt16(pageSizeCookie.Value);

            var tab = new TableViewModel<Tournament>()
            {
                TableData = tournaments.ToPagedList((int)page, pageSize),
                Active = active,
                Controller = "Tournaments",
                ChildController = "Tournaments",
                TournamentOrganizer = "",
                TournamentStatus = null
            };
            return PartialView("_Tournaments", tab);
        }

        [ChildActionOnly]
        [AllowAnonymous]
        public PartialViewResult GetTeams(int id, int? page = 1, string active = "")
        {
            var tournament = tournamentService.GetById(id);
            HttpCookie pageSizeCookie = Request.Cookies["_pageSize"];
            int pageSize = Convert.ToInt16(pageSizeCookie.Value);

            var tab = new TableViewModel<Team>()
            {
                TableData = tournament.Teams.OrderBy(x => x.Name).ToPagedList((int)page, pageSize),
                Active = active,
                Controller = "Tournaments",
                ChildController = "Teams",
                TournamentOrganizer = tournament.Organizer,
                TournamentStatus = tournament.Status
            };

            return PartialView("_Teams", tab);
        }

        [ChildActionOnly]
        [AllowAnonymous]
        public PartialViewResult GetCategories(int id, int? page = 1, string active = "")
        { 
            var tournament = tournamentService.GetById(id);
            HttpCookie pageSizeCookie = Request.Cookies["_pageSize"];
            int pageSize = Convert.ToInt16(pageSizeCookie.Value);

            var tab = new TableViewModel<Category>()
            {
                TableData = tournament.Categories.OrderBy(x => x.Id).ToPagedList((int)page, pageSize),
                Active = active,
                Controller = "Tournaments",
                ChildController = "Categories",
                TournamentOrganizer = tournament.Organizer,
                TournamentStatus = tournament.Status
            };

            return PartialView("_Categories", tab);
        }

        [ChildActionOnly]
        [AllowAnonymous]
        public PartialViewResult GetReferees(int id, int? page = 1, string active = "", int records = 5)
        {
            var tournament = tournamentService.GetById(id);
            HttpCookie pageSizeCookie = Request.Cookies["_pageSize"];
            int pageSize = Convert.ToInt16(pageSizeCookie.Value);

            var tab = new TableViewModel<Referee>()
            {
                TableData = tournament.Referees.OrderBy(x => x.RefereeEmail).ToPagedList((int)page, pageSize),
                Active = active,
                Controller = "Tournaments",
                ChildController = "Referees",
                TournamentOrganizer = tournament.Organizer,
                TournamentStatus = tournament.Status
            };

            return PartialView("_Referees", tab);
        }

        [ChildActionOnly]
        [AllowAnonymous]
        public PartialViewResult GetMap(int id, int? page = 1, string active = "")
        {
            var tournaments = tournamentService.GetById(id);
            return PartialView("_Map", tournaments);
        }

        private PageHeader GetPageHeader(Tournament tournament)
        {
            List<BreadCrumbItem> list = new List<BreadCrumbItem>();
            List<Button> buttons = new List<Button>();
            List<string> description = new List<string>();
            string title;
            string subtitle;
             
            list.Add(new BreadCrumbItem() { Controller = "Home", Name = AppResource.StartPage, Action = "Index"});
            list.Add(new BreadCrumbItem() { Controller = "Tournaments", Name = AppResource.Tournaments, Action = "Index" });

            if (tournament!=null)
            {
                if (tournament.Status == TournamentStatus.OpenedForRegistration)
                {
                    buttons.Add(new Button() { Target = "_self", Id = tournament.Id, Caption = AppResource.AddTeam, Action = "Create", Controller = "Teams" });
                }

                if (tournament.Organizer == User.Identity.Name)
                {
                    buttons.Add(new Button() { Target = "_self", Id = tournament.Id, Caption = AppResource.AddCategory, Action = "Create", Controller = "Categories" });
                    buttons.Add(new Button() { Target = "_self", Id = tournament.Id, Caption = AppResource.AddReferee, Action = "Create", Controller = "Referees" });
                    buttons.Add(new Button() { Target = "devider", Id = tournament.Id });
                    buttons.Add(new Button() { Target = "ajax", Id = tournament.Id, Caption = AppResource.GenerateTable, Action = "CreateTable", Controller = "Tournaments" });
                    if (User.IsInRole("Admin"))
                    {
                        buttons.Add(new Button() { Target = "_self", Id = tournament.Id, Caption = "Prepare sample data", Action = "SampleData", Controller = "Tournaments" });
                    }
                    //if (rep.Count() > 0)
                    //{
                    //    buttons.Add(new Button() { Target = "dropdown-header", Id = (int)id, Caption = AppResource.Reports });
                    //    buttons.AddRange(rep.Select(x => new Button() { Target = "_self", Controller = "Tournaments", Action = "Report", Type = x.FileName, Caption = x.Name, Id = (int)id }));
                    //}
                }

                list.Add(new BreadCrumbItem() { Controller = "Tournaments", Id = tournament.Id, Name = tournament.Name, Action = "Details" });
                title = tournament.Name;
                subtitle =  string.Format("{0}, {1}", tournament.City, tournament.Country.Translate());

                description.Add(string.Format("<strong>{0}:</strong> <em>{1:d} - {2:d}</em>", AppResource.Duration, tournament.DateFrom, tournament.DateTo));
                description.Add(string.Format("<strong>{0}:</strong> <em>{1}</em>", AppResource.Organizer, tournament.Organizer.GetFullName()));
            }
            else
            {
                title = AppResource.Tournaments;
                subtitle = "";
                buttons.Add(new Button() { Target = "_self", Caption = AppResource.AddTournament, Action = "Create", Controller = "Tournaments" });
            }

            PageHeader pageHeader = new PageHeader()
            {
                BreadCrumbs = list,
                Buttons = buttons,
                Title = title,
                Subtitle = subtitle,
                Description =description 
            };

            return pageHeader;
        }

        [AllowAnonymous]
        public ActionResult Index(string search = null, int page = 1, string active="")
        {
            List<Tournament> tournaments = tournamentService.GetAll().ToList();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PageTabs", navigationService.GetTabs(RouteData, active));
            }

            //TournamentViewManager manager = new TournamentViewManager();
            //manager.GetViewModel(null); 


            return View(GetPageHeader(null));
        }

        [AllowAnonymous]
        public ActionResult Details(int? id, string search = null, int page = 1, string active="")
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
 
            Tournament tournament = tournamentService.GetById((int)id);
            if (tournament == null)
            {
                return HttpNotFound();
            }
            
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PageTabs", navigationService.GetTabs(RouteData, active));
            }

            //TournamentViewManager manager = new TournamentViewManager();
            //manager.GetViewModel(tournament);

            return View(GetPageHeader(tournament));
        }

        public ActionResult Create()
        {
            var _tournamentVM = new TournamentViewModel() 
            { 
                Tournament = new Tournament() 
                {
                    Organizer = User.Identity.Name, 
                    DateFrom=DateTime.Now,
                    DateTo=DateTime.Now,
                    RegistrationEnd=DateTime.Now
                },
                CountryList = GetCountryList(),
                SportGroupList = GetSportGroupList()
            };
            return View(_tournamentVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TournamentViewModel _tournamentViewModel)
        {
            if (ModelState.IsValid)
            {
                Tournament tournament = tournamentService.Create(_tournamentViewModel.Tournament);
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser user = await userManager.FindByNameAsync(tournament.Organizer);  
                string subject = string.Format("Tournament \"{0}\" was successfully created.", tournament.Name);
                string body = MailTemplateBody.GetBody(user.FullName, String.Format("Congradulation! Your tournament \"{0}\" was successfully created.", tournament.Name));

                await userManager.SendEmailAsync(user.Id, subject, body);
                return RedirectToAction("Details", "Tournaments", new { id= tournament.Id });
            }
            _tournamentViewModel.CountryList = GetCountryList();
            _tournamentViewModel.SportGroupList = GetSportGroupList();

            return View(_tournamentViewModel);
        }

        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tournament _tournament = tournamentService.GetById((int)id);
            if (_tournament == null)
            {
                return HttpNotFound();
            }
            if (_tournament.Organizer != User.Identity.Name )
            {
                return RedirectToAction("Details", new {id=_tournament.Id });
            }

            var _tournamentViewModel = new TournamentViewModel()
            {
                Tournament = _tournament,
                CountryList = GetCountryList(),
                SportGroupList = GetSportGroupList()
            };
            return View(_tournamentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TournamentViewModel _tournamentViewModel)
        {
            if (ModelState.IsValid)
            {
                tournamentService.Update(_tournamentViewModel.Tournament);
                return RedirectToAction("Index");
            }
            return View(_tournamentViewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tournament tournament = tournamentService.GetById((int)id);  
            if (tournament == null)
            {
                return HttpNotFound();
            }
            return View(tournament);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tournament tournament = tournamentService.GetById((int)id);
            tournamentService.Delete(tournament);
            return RedirectToAction("Index");
        }

        public ActionResult CreateTable(int id)
        {
            Tournament tournament = tournamentService.GetById(id);
            if (tournament.Organizer == User.Identity.Name && Request.IsAjaxRequest())
            {
                SportUnit[] distinctions = sportUnitService.GetByTypeForGroup(tournament.GroupId, SportUnitType.Distinction).ToArray(); 
                var levels = generalUnitService.GetByType(GeneralUnitType.Level);
                Fight fight;
                GeneralUnit levelGU;
                foreach (var category in tournament.Categories)
                {
                    List<FightRelation> rels = fightRelationService.GetWithFilter(x => x.FightChild.CategoryId == category.Id || x.FightParent.CategoryId == category.Id).ToList();

                    foreach (var _rel in rels)
                    {
                        fightRelationService.Delete(_rel);
                    }

                    List<Fight> _fights = category.Fights.ToList();
                    foreach (var _fight in _fights)
                    {
                        fightService.Delete(_fight);
                    }

                    var participants = categoryService.GetParticipants(category.Id, true);
                    if (participants.Count() > 1 && participants.Count() < 9)
                    {
                        int fightsCount = participants.Count() - 1;
                        int itemInLevel = 0;
                        int level = 0;

                        for (int i = 0; i < fightsCount; i++)
                        {
                            int maxItemsInLevel = (int)Math.Pow(2, level);
                            levelGU = levels.Where(x => x.Description.Equals(string.Format("level_{0}", level + 1))).SingleOrDefault();
                            if (levelGU != null)
                            {
                                fight = new Fight() { LevelId = levelGU.Id, FightInLevel = itemInLevel, CategoryId = category.Id };
                                if (itemInLevel == maxItemsInLevel - 1)
                                {
                                    itemInLevel = 0;
                                    level += 1;
                                }
                                else
                                {
                                    itemInLevel += 1;
                                }
                                fightService.Create(fight);
                            }
                        }
                        if (category.OneThirdPlace)
                        {
                            levelGU = levels.Where(x => x.Description.Equals("level_0")).SingleOrDefault();
                            if (levelGU != null)
                            {
                                fight = new Fight() { LevelId = levelGU.Id, FightInLevel = 0, CategoryId = category.Id };
                                fightService.Create(fight); 
                            }
                        }

                        var fights = category.Fights;
                        int index = 0;
                        bool winnerDirection;
                        foreach (var item in fights)
                        {
                            int lev = Convert.ToInt16(item.Level.Description.Split('_')[1]);
                            if (lev == 0)
                            {
                                lev += 1;
                                winnerDirection = false;
                            }
                            else
                            {
                                winnerDirection = true;
                            }
                            levelGU = levels.Where(s => s.Description.Equals(String.Format("level_{0}", lev + 1))).SingleOrDefault();
                            if (levelGU != null)
                            {
                                var childFights = fights.Where(x => x.LevelId == levelGU.Id && x.FightInLevel >= item.FightInLevel * 2 && x.FightInLevel <= item.FightInLevel * 2 + 1);
                                foreach (Fight childFight in childFights)
                                {
                                    FightRelation fightRelation = new FightRelation() { FightChildId = childFight.Id, FightParentId = item.Id, IsWinner = winnerDirection };
                                    fightRelationService.Create(fightRelation);
                                }

                                if (winnerDirection)
                                {
                                    for (int i = 0; i < 2 - childFights.Count(); i++)
                                    {
                                        Fighter fighter = new Fighter() { FightId = item.Id, ParticipantId = participants.ElementAt(index).Id, DistinctionId = distinctions[i].Id };
                                        index += 1;
                                        fighterService.Create(fighter);
                                    }
                                }
                            }
                            //else if (winnerDirection)
                            //{
                            //    for (int i = 0; i < 2; i++)
                            //    {
                            //        Fighter fighter = new Fighter() { FightId = item.Id, ParticipantId = participants.ElementAt(index).Id, DistinctionId = distinctions[i].Id };
                            //        index += 1;
                            //        unitOfWork.FighterRepository.Insert(fighter);
                            //    }
                            //}
                        }
                    }
                }
            }
            return RedirectToAction("Details", RouteData);
        }

        public ActionResult SampleData(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tournament _tournament = tournamentService.GetById((int)id);
            if (_tournament == null)
            {
                return HttpNotFound();
            }
            SampleDataViewModel sampleDataViewModel = new SampleDataViewModel() { TournamentId = (int)id };
            return View(sampleDataViewModel);
        }

        [HttpPost, ActionName("SampleData")]
        public ActionResult SampleDataConfirmed([Bind(Include = "TournamentId,TeamCount,MaxParticipantsInTeam")]SampleDataViewModel _sampleDataViewModel)
        {
            if (ModelState.IsValid)
            {
                Tournament _tournament = tournamentService.GetById(_sampleDataViewModel.TournamentId);


                var country = generalUnitService.GetByType(GeneralUnitType.Country).FirstOrDefault();
                var ranking = sportUnitService.GetByTypeForGroup(_tournament.GroupId, SportUnitType.Ranking).FirstOrDefault();
                Gender participantGender;
                string FirstName;

                Random rnd = new Random();
                if (country != null && ranking != null)
                {
                    for (int i = 0; i < _sampleDataViewModel.TeamCount; i++)
                    {
                        List<Participant> participants = new List<Participant>();

                        int participantCount = rnd.Next(4, _sampleDataViewModel.MaxParticipantsInTeam);
                        for (int j = 0; j < participantCount; j++)
                        {
                            if (j % 4 == 0)
                            {
                                participantGender = Gender.Female;
                                FirstName = "Alla";
                            }
                            else
                            {
                                participantGender = Gender.Male;
                                FirstName = "Max";
                            }
                            var participant = new Participant()
                            {
                                FirstName = string.Format("{0}_{1}_{2}", FirstName, i, j),
                                LastName = "Muster",
                                Gender = participantGender,
                                BirthDate = new DateTime(rnd.Next(1992, 2005), rnd.Next(1, 12), rnd.Next(1, 28)),
                                Weight = rnd.Next(20, 50),
                                RankingId = ranking.Id
                            };
                            participants.Add(participant);
                        }

                        var team = new Team()
                        {
                            Name = "Team_" + i.ToString(),
                            Coach = User.Identity.Name,
                            City = "Berlin",
                            CountryId = country.Id,
                            TournamentId = _sampleDataViewModel.TournamentId,
                            CoachRankingId = ranking.Id,
                            Participants = participants
                        };
                        teamService.Create(team);
                    }
                }
                return RedirectToAction("Details", new { id = _tournament.Id});
            }
            return View(_sampleDataViewModel);
        }

        //public ActionResult GetReportSnapshot(int id)
        //{
        //    StiReport report = new StiReport();

        //    string fileName = (string)TempData["fileName"];
        //    var rep = unitOfWork.ReportRepository.Get(filter: x => x.FileName.Equals(fileName)).FirstOrDefault();

        //    if (rep!= null)
        //    {
        //        var tournament = unitOfWork.TournamentRepository.GetByID(id);
        //        List<TeamR> teams = tournament.Teams
        //                 .Select(s => new TeamR()
        //                 {
        //                     Name = s.Name,
        //                     Coach = s.Coach.GetFullName(),
        //                     City = s.City,
        //                     Country = s.Country.Name,
        //                     IsConfirmed = s.ParticipationConfirmed,
        //                     Ranking = s.CoachRanking.Name,
        //                     Participants = s.Participants
        //                         .Select(k => new ParticipantR()
        //                         {
        //                             FullName = k.FullName,
        //                             Age = k.Age,
        //                             BirthDate = k.BirthDate,
        //                             Gender = k.Gender.ToString(),
        //                             Ranking = k.Ranking.Name,
        //                             Weight = k.Weight
        //                         }).ToList()
        //                 }).ToList();

        //        report.RegBusinessObject("Teams", teams);
        //        report.Dictionary.SynchronizeBusinessObjects(2);
        //        report.Load(Server.MapPath(string.Format("~/App_Data/Reports/{0}", rep.FileName)));
        //        report.ReportName = tournament.Name;
        //        report.ReportDescription = "Description " + tournament.Name;

        //        return StiMvcViewer.GetReportSnapshotResult(report);
        //    }
        //    return null;
        //}

        //public ActionResult ViewerEvent()
        //{
        //    return StiMvcViewer.ViewerEventResult();
        //}

        //public ActionResult Report(int? id, string type)
        //{
        //    if (id == null || string.IsNullOrEmpty(type))
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    Tournament tournament = unitOfWork.TournamentRepository.GetByID(id);
        //    if (tournament == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    TempData["fileName"] = type;
        //    return View();
        //}
    }
}
