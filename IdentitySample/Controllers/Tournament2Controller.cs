using IdentitySample.ViewModels;
using IdentitySample.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace IdentitySample.Controllers
{
    public class Tournament2Controller : BaseController
    {
        ITournamentViewModel viewModel;
        public Tournament2Controller(ITournamentViewModel _viewModel)
        {
            viewModel = _viewModel;
        }
        // GET: Team2
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            viewModel.Initialize(id); 
            return View(viewModel);
        }

        public ActionResult Index()
        {
            viewModel.Initialize(null);
            return View(viewModel);
        }
    }
}