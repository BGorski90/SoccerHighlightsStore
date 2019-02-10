using SoccerHighlightsStore.Common.Contracts;
using SoccerHighlightsStore.DataAccessLayer.Repositories;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoccerHighlightsStore.Common.Infrastructure;

namespace SoccerHighlightsStore.Storefront.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class UsersController : BaseController
    {
        private IUserRepository _userRepository;

        public UsersController(IUserRepository repo)
        {
            _userRepository = repo;
        }

        // GET: Admin/Orders
        public ActionResult Index()
        {
            var model = _userRepository.Users.ToPagedList(Consts.defaultPageNumber, Consts.defaultPageSize);
            return View(model);
        }
    }
}