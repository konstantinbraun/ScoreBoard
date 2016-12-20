using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentitySample.ViewModels.Common;
using IdentitySample.Entities;
using IdentitySample.ViewModels.Helpers;
using IdentitySample.Resx;
using IdentitySample.Service;
using IdentitySample.ViewModels.TabItems;

namespace IdentitySample.ViewModels
{
    public class TeamViewModel1 : ViewModelBase,  ITeamViewModel
    {
        ITeamService teamService;
        Team team;
        public TeamViewModel1(ITeamService _teamService) 
        {
            teamService = _teamService;
        }


        public string Subtitle
        {
            get
            {
                return "test";
            }
        }

        public string Title
        {
            get
            {
                return team.Name;
            }
        }

        public override  IList<BreadCrumbItem> GetBreadCrumbs()
        {
            IList<BreadCrumbItem> breadCrumbs = base.GetBreadCrumbs();
            breadCrumbs.Add(new BreadCrumbItem() { Controller = "Tournaments", Name = AppResource.Tournaments, Action = "Index" });
            breadCrumbs.Add(new BreadCrumbItem() { Controller = "Tournaments", Id =team.TournamentId,  Name = team.Tournament.Name , Action = "Details" });
            breadCrumbs.Add(new BreadCrumbItem() { Controller = "Teams", Id = team.Id, Name = team.Name, Action = "Details" });

            return breadCrumbs;

        }

        public void Initialize(int? id)
        {
            if (id != null)
                team = teamService.GetById((int)id);
        }

        public IList<Team> GetData()
        {
            throw new NotImplementedException();
        }

        public IList<Button> GetButtons()
        {
            List<Button> buttons = new List<Button>();

      //      if (team.Tournament.Status == TournamentStatus.OpenedForRegistration && team.Coach == User.Identity.Name)
        //    {
                buttons.Add(new Button() { Target = "_self", Id = team.Id, Caption = AppResource.AddParticipant, Action = "Create", Controller = "Participants" });
            //}
            return buttons;
        }

        //public IList<TabItem> GetTabs()
        //{
        //    List<TabItem> tabs = new List<TabItem>();

        //    tabs.Add(new TabItem() { LinkText = "Test1", IsActive = true ,  ActionName = "Action" });
        //    tabs.Add(new TabItem() { LinkText = "Test2", IsActive = false , ActionName = "Action2" });

        //    return tabs;
        //}

        public IList<string> GetDescription()
        {
            return new List<string>() { "First line", "Second line" };
        }

        public IList<ITabItem> GetTabs()
        {
            List<ITabItem> tabs = new List<ITabItem>();

            tabs.Add(new TeamTabItem());
            tabs.Add(new CategoryTabItem());

            return tabs;
        }

    }
}
