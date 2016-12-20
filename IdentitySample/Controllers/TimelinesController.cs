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
using IdentitySample.Helpers;
using IdentitySample.Filters;
using PagedList;
using IdentitySample.Service;

namespace IdentitySample.Controllers
{
    [CustomAuthorize]
    public class TimelinesController : BaseController
    {
        ITimelineService timelineService;
        IRefereeService refereeService;
        IGeneralUnitService generalUnitService;
        ITournamentService tournamentService;

        public TimelinesController(ITimelineService _timelineService, IRefereeService _refereeService,
                                    IGeneralUnitService _generalUnitService, ITournamentService _tournamentService)
        {
            timelineService = _timelineService;
            refereeService = _refereeService;
            generalUnitService = _generalUnitService;
            tournamentService = _tournamentService;
        }

        IList<SelectListItem> GetCategoryList(int TournamentId)
        {
            return tournamentService.GetById(TournamentId).Categories.ToSelectList(c => c.Id, s => String.Format("{0} [{1}] [{2}]", s.Gender.GetDisplayName(), s.Age, s.Weight));
        }

        IList<SelectListItem> GetLevelList()
        {
            return generalUnitService.GetByType(GeneralUnitType.Level).ToSelectList(a => a.Id, b => b.Translate());
        }

        public ActionResult Create(int? id)
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
            var timelineItem = new Timeline() { RefereeId = (int)id };
            TimelineViewModel _timelineViewModel = new TimelineViewModel()
            {
                TimelineItem = timelineItem,
                CategoryList = GetCategoryList(referee.TournamentId),
                LevelList = GetLevelList()  
            };
            return View(_timelineViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TimelineItem")] TimelineViewModel _timelineViewModel)
        {
            if (ModelState.IsValid)
            {
                timelineService.Create(_timelineViewModel.TimelineItem);
                return RedirectToAction("Details", "Referees", new { id = _timelineViewModel.TimelineItem.RefereeId, active = "Timeline" });
            }
            _timelineViewModel.CategoryList = GetCategoryList(_timelineViewModel.TimelineItem.Referee.TournamentId);
            _timelineViewModel.LevelList = GetLevelList();
            return View(_timelineViewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timeline timeline = timelineService.GetById((int)id);
            if (timeline == null)
            {
                return HttpNotFound();
            }
            TimelineViewModel timelineViewModel = new TimelineViewModel()
            {
                TimelineItem = timeline,
                CategoryList = GetCategoryList(timeline.Referee.TournamentId),
                LevelList = GetLevelList()
            };
            return View(timelineViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TimelineItem")] TimelineViewModel _timelineViewModel)
        {
            if (ModelState.IsValid)
            {
                timelineService.Update(_timelineViewModel.TimelineItem);
                return RedirectToAction("Details", "Referees", new { id = _timelineViewModel.TimelineItem.RefereeId, active = "Timeline" });
            }
            _timelineViewModel.CategoryList = GetCategoryList(_timelineViewModel.TimelineItem.Referee.TournamentId);
            _timelineViewModel.LevelList = GetLevelList();
            return View(_timelineViewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timeline timeline = timelineService.GetById((int)id);
            if (timeline == null)
            {
                return HttpNotFound();
            }
            return View(timeline);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Timeline timelineItem = timelineService.GetById((int)id);
            timelineService.Delete(timelineItem);
            return RedirectToAction("Details", "Referees", new { id = timelineItem.RefereeId, active = "Timeline" });
        }
    }
}
