
using IdentitySample.Helpers;
using IdentitySample.Entities;
using IdentitySample.Models;
using System;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdentitySample.Filters;
using IdentitySample.Service;

namespace IdentitySample.Controllers
{
    [CustomAuthorize]
    public class ParticipantsController : BaseController
    {
        IParticipantService participantService;
        ITeamService teamService;
        ISportUnitService sportUnitService;

        public ParticipantsController(IParticipantService _participantService, ITeamService _teamService, ISportUnitService _sportUnitService)
        {
            teamService = _teamService;
            participantService = _participantService;
            sportUnitService = _sportUnitService;
        }

        IList<SelectListItem> GetRankingList(Tournament tournament)
        {
            return sportUnitService.GetByTypeForGroup(tournament.GroupId, SportUnitType.Ranking).ToSelectList(x => x.Id, x => x);
        }

        public ActionResult Create(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Team team = teamService.GetById((int)id);
            if (team == null)
            {
                return HttpNotFound();
            }

            if ((User.Identity.Name.Equals(team.Coach) && team.Tournament.Status == TournamentStatus.OpenedForRegistration) ||
                (User.Identity.Name.Equals(team.Tournament.Organizer) && team.Tournament.Status == TournamentStatus.CheckingData  ))
            {
                var participantViewModel = new ParticipantViewModel()
                {
                    Participant = new Participant() { TeamId = (int)id },
                    RankingList = GetRankingList(team.Tournament)
                };
                return View(participantViewModel);
            }
            else
            {
                return RedirectToAction("Details", "Teams", new { id = team.TournamentId  });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ParticipantViewModel _participantViewModel)
        {
            if (ModelState.IsValid)
            {
                participantService.Create(_participantViewModel.Participant);
                return RedirectToAction("Details", "Teams", new { id = _participantViewModel.Participant.TeamId });
            }
            Team team = teamService.GetById(_participantViewModel.Participant.TeamId);
            _participantViewModel.RankingList = GetRankingList(team.Tournament);
            return View(_participantViewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Participant _participant = participantService.GetById((int)id);
            if (_participant == null)
            {
                return HttpNotFound();
            }
            
            if (!_participant.Team.Coach.Equals(User.Identity.Name))
            {
                return RedirectToAction("Details", "Teams", new { id = _participant.TeamId });
            }

            var participantViewModel = new ParticipantViewModel()
            {
                Participant = _participant,
                RankingList = GetRankingList(_participant.Team.Tournament)
            };

            return View(participantViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ParticipantViewModel _participantViewModel)
        {
            if (ModelState.IsValid)
            {
                participantService.Update(_participantViewModel.Participant);
                return RedirectToAction("Details", "Teams", new { id = _participantViewModel.Participant.TeamId });
            }
            Team team = teamService.GetById(_participantViewModel.Participant.TeamId);
            _participantViewModel.RankingList = GetRankingList(team.Tournament);
            return View(_participantViewModel);
        }

        public ActionResult Delete(int id)
        {
            Participant participant = participantService.GetById(id);
            if (participant == null)
            {
                return HttpNotFound();
            }
            return View(participant);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Participant participant = participantService.GetById(id);
            participantService.Delete(participant);
            return RedirectToAction("Details", "Teams", new { id = participant.TeamId });
        }

    }
}
