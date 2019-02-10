using SoccerHighlightsStore.Common.Contracts;
using SoccerHighlightsStore.BusinessLayer.Entities;
using SoccerHighlightsStore.DataAccessLayer.Repositories;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Infrastructure;
using System.Text;
using SoccerHighlightsStore.Common.Infrastructure;
using System.Web.Caching;
using System.Diagnostics;
using DataAccessLayer.Helpers;
using SevenZip;

namespace SoccerHighlightsStore.Storefront.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class VideosController : BaseController
    {
        private VideoCacheManager _cacheManager;
        private IVideoRepository _videoRepository;

        public VideosController(IVideoRepository repo)
        {
            _videoRepository = repo;

        }
        // GET: Admin/Videos
        public ActionResult Index()
        {
            if (_cacheManager == null)
                _cacheManager = new VideoCacheManager(HttpContext, _videoRepository);
            var model = _cacheManager.Get("All") ?? _videoRepository.Videos;
            return View(model);
        }

        // GET: Video/Create
        [HttpGet]
        public ActionResult Create()
        {
            if (_cacheManager == null)
                _cacheManager = new VideoCacheManager(HttpContext, _videoRepository);
            ViewBag.Categories = CategoriesFormatter.FormatCategoriesForSearch(_videoRepository.AdminCategories);
            return View();
        }

        // POST: Video/Create
        [HttpPost]
        public ActionResult Create(Video video, HttpPostedFileBase previewZip)
        {
            if (!ModelState.IsValid || previewZip == null || !(previewZip.ContentLength > 0))
            {
                return View();
            }
            else
            {
                using (var tmp = new SevenZipExtractor(previewZip.InputStream))
                {
                    for (int i = 0; i < tmp.ArchiveFileData.Count; i++)
                    {
                        var fileData = tmp.ArchiveFileData[i];
                        if (fileData.FileName.EndsWith("png") || fileData.FileName.EndsWith("jpg"))
                        {
                            tmp.ExtractFiles(Server.MapPath("~/Content/Images/"), fileData.Index);
                        }
                        else if (fileData.FileName.EndsWith("mp4") || fileData.FileName.EndsWith("webm"))
                        {
                            tmp.ExtractFiles(Server.MapPath("~/Content/VideoPreviews/"), fileData.Index);
                        }
                        else
                        {
                            ModelState.AddModelError("", new Exception("Wrong file format"));
                            return View();
                        }
                    }
                }
                _videoRepository.Add(video);
                //var fileName = video.Title;
                //var path = Path.Combine(Server.MapPath("~/Content/Images/"), fileName + Path.GetExtension(previewZip.FileName));
                //previewZip.SaveAs(path);
                _cacheManager.Add(video);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (_cacheManager == null)
                _cacheManager = new VideoCacheManager(HttpContext, _videoRepository);
            return View(_videoRepository.Get(id));
        }

        [HttpPost]
        public ActionResult Edit(Video video)
        {
            if (ModelState.IsValid)
            {
                _videoRepository.Update(video);
                _cacheManager.Update(video);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _videoRepository.Remove(id);
            _cacheManager.Remove(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult AddCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                var message = new StringBuilder();
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        message.Append(error.ErrorMessage);
                    }
                }
                return Json("Error" + message);
            }
            _videoRepository.AddCategory(category);
            return Json("OK");
        }
    }
}