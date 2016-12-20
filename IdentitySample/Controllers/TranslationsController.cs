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
using IdentitySample.Filters;
using IdentitySample.Service;
using PagedList;

namespace IdentitySample.Controllers
{
    [CustomAuthorize(Roles="Admin")]
    public class TranslationsController : BaseController
    {
        ITranslationService translationService;
        IUnitService unitService;

        public TranslationsController(ITranslationService _translationService, IUnitService _unitService)
        {
            translationService = _translationService;
            unitService = _unitService;
        }

        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Unit unit = unitService.GetById((int)id);
 
            if (unit == null)
            {
                return HttpNotFound();
            }

            Translation translation = new Translation() { UnitId = (int)id, Unit = unit };
            return View(translation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UnitId,Language,Name")] Translation translation)
        {
            if (ModelState.IsValid)
            {
                translationService.Create(translation);
                return RedirectToAction("Details", "Units", new { id = translation.UnitId });
            }
            return View(translation);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Translation translation = translationService.GetById((int)id);
            if (translation == null)
            {
                return HttpNotFound();
            }
   
            return View(translation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UnitId,Language,Name")] Translation translation)
        {
            if (ModelState.IsValid)
            {
                translationService.Update(translation);
                return RedirectToAction("Details", "Units", new { id = translation.UnitId });
            }

            return View(translation);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Translation translation = translationService.GetById((int)id);
            if (translation == null)
            {
                return HttpNotFound();
            }

            return View(translation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Translation translation = translationService.GetById(id);
            translationService.Delete(translation);
            return RedirectToAction("Details", "Units", new { id = translation.UnitId });
        }
    }
}
