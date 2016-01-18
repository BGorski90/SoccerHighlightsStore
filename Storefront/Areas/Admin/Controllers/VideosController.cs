using Common.Contracts;
using Storefront.BusinessLayer.Entities;
using Storefront.BusinessLayer.Repositories;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Storefront.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class VideosController : Controller
    {
        private IVideoRepository _videoRepository;

        public VideosController(IVideoRepository repo)
        {
            _videoRepository = repo;
        }
        // GET: Admin/Videos
        public ActionResult Index()
        {
            var model = _videoRepository.GetAll().ToPagedList(Consts.defaultPageNumber, Consts.defaultPageSize);
            return View(model);
        }

        // GET: Video/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Video/Create
        [HttpPost]
        public ActionResult Create(Video video, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid && imageFile != null && imageFile.ContentLength > 0)
            {
                _videoRepository.Add(video);
                var fileName = video.Title;
                var path = Path.Combine(Server.MapPath("~/Content/Images/"), fileName + Path.GetExtension(imageFile.FileName));
                imageFile.SaveAs(path);
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Video/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(_videoRepository.Get(id));
        }

        // POST: Video/Edit/5
        [HttpPost]
        public ActionResult Edit(Video video)
        {
            if (ModelState.IsValid)
            {
                _videoRepository.Update(video);
                return RedirectToAction("Index");
            }
            return View();
        }

        // POST: Video/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _videoRepository.Remove(id);
            return RedirectToAction("Index");
        }
    }
}