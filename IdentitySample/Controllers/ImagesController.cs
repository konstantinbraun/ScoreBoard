using IdentitySample.Entities;
using IdentitySample.Filters;
using IdentitySample.Models;
using IdentitySample.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace IdentitySample.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class ImagesController : BaseController
    {
        IUnitService unitService;
        public ImagesController(IUnitService _unitService)
        {
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

            return View(new UploadViewModel() { UnitName = unit.Name, Id = (int)id, ToInsert = true });
        }

        public ActionResult Edit(int? id)
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

            return  View("Create", new UploadViewModel() { UnitName = unit.Name, Id = (int)id, ToInsert = false });
        }

        [HttpPost]
        public ActionResult Upload(UploadViewModel _viewModel)
        {
            if (_viewModel.File != null)
            {
                Unit unit = unitService.GetById(_viewModel.Id);
                byte[] array;
                using (MemoryStream ms = new MemoryStream())
                {
                    _viewModel.File.InputStream.CopyTo(ms);
                    array = ms.GetBuffer();
                }
                unit.Image = array;
                unitService.Update(unit); 
            }
            return RedirectToAction("Details", "Units", new { id = _viewModel.Id });
        }

        public ActionResult Delete(int? id)
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

            return View(new UploadViewModel() { UnitName = unit.Name, Id = (int)id });
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(UploadViewModel _viewModel)
        {
            Unit unit = unitService.GetById(_viewModel.Id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            unit.Image = null;
            unitService.Update(unit);

            return RedirectToAction("Details", "Units", new { id = _viewModel.Id });
        }

        [AllowAnonymous]
        public ActionResult Details(int id, int width = 50)
        {
            byte[] image = unitService.GetById(id).Image;
            if (image != null)
            {
                return File(ImageResizer.ResizeImage(image, width), "image/jpg");
            }
            else
            {
                return null;
            }
        }
    }
}