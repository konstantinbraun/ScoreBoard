using IdentitySample.DAL;
using IdentitySample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentitySample.Controllers
{


    public class BreadCrumb
    {
        public int Id {get; set;}
        public BreadCrumbDeledate MyDelegate {get; set;}
    }

    public class BreadCrumbController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Dictionary<string, BreadCrumb> breadCrumb = new Dictionary<string,BreadCrumb>();

        public BreadCrumbController()
        {
            breadCrumb.Add("Teams", new BreadCrumb() { Id= 1});
            breadCrumbs.Add("Categories") 
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public PartialViewResult BreadCrumb(int id, string controller, string action)
        {
            var tournament = unitOfWork.TournamentRepository.GetByID(id);
            var breadCrumb = new List<BreadCrumbItem>();
            breadCrumb.Add(new BreadCrumbItem() { Id = tournament.Id, Name = tournament.Name, Controller = "Tournaments" });
            return PartialView("_BreadCrumb", breadCrumb);
        }


        private List<BreadCrumbItem> GetBreadCrumb(int)
    }
}