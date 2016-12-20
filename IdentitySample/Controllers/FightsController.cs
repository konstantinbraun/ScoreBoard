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
using IdentitySample.Service;

namespace IdentitySample.Controllers
{
    public class FightsController : BaseController
    {
        IFighterService fighterService;
        ITimelineService timelineService;
        IFightService fightService;
        ISportUnitService sportUnitService;

        public FightsController(IFighterService _fighterService, 
                                IFightService _fightService, 
                                ITimelineService _timelineService, 
                                ISportUnitService _sportUnitService)
        {
            fighterService = _fighterService;
            timelineService = _timelineService;
            fightService = _fightService;
            sportUnitService = _sportUnitService;
        }

        public ActionResult GetFighter(int id, string active, bool showWinnerItem)
        {
            Fighter fighter = fighterService.GetById(id);
            Timeline timeline = timelineService.GetWithFilter(x => x.CategoryId == fighter.Fight.CategoryId && x.LevelId == fighter.Fight.LevelId).SingleOrDefault();

            int refId;
            if (timeline == null)
            {
                refId = -1;
            }
            else
            {
                refId = timeline.RefereeId;
            }

            Tournament tournament = fighter.Fight.Category.Tournament;
            List<Fighter> fighters = new List<Fighter>();
            switch (active)
            {
                case "Competitions":
                    int catId = fighter.Fight.CategoryId;

                    if (fightService.GetWithFilter(x=>x.CategoryId ==catId && x.Fighters.Any(s => s.IsWinner)).Count() == 0 && (User.Identity.Name.Equals(tournament.Organizer))) 
                    {
                        fighters = fighterService.GetFightersToReplace(fighter).ToList();
                    }
                    showWinnerItem = false;
                    break;
                case "UpcomingFights":
                    //showWinnerItem = true;
                    break;
                case "FinishedFights":
                    showWinnerItem = false;
                    break;
            }
            return PartialView("_Fighter", new FighterViewModel { Fighter = fighter, FightersToReplace = fighters, ShowWinnerItem = showWinnerItem, RefereeId = refId });
        }

    }
}
