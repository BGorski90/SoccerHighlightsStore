using SoccerHighlightsStore.BusinessLayer.Identity;
using SoccerHighlightsStore.Common.Infrastructure;
using SoccerHighlightsStore.BusinessLayer.Entities;
using SoccerHighlightsStore.DataAccessLayer.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoccerHighlightsStore.Storefront.Controllers
{
    public class WishlistController : BaseController
    {
        private IUserRepository _userRepository;
        private IVideoRepository _videoRepository;

        public ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public User CurrentUser
        {
            get
            {
                return UserManager.FindByName(HttpContext.User.Identity.Name);
            }
        } 

        public WishlistController(IUserRepository users, IVideoRepository videos)
        {
            _userRepository = users;
            _videoRepository = videos;
        }

        public ActionResult AddToWishlist(int videoID)
        {
            _userRepository.AddVideoToWishlist(CurrentUser.Id, videoID);
            if (Request.IsAjaxRequest())
                return Json("OK");
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromWishlist(int videoID)
        {
            _userRepository.RemoveVideoFromWishlist(CurrentUser.Id, videoID);
            if (Request.IsAjaxRequest())
                return Json("OK");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ToggleWishlist(int videoID)
        {
            if (_userRepository.FindWishlist(User.Identity.Name, _videoRepository.Get(videoID)))
                return RemoveFromWishlist(videoID);
            else
                return AddToWishlist(videoID);
        }
    }
}