using Common.Contracts;
using Storefront.BusinessLayer.Repositories;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Storefront.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class UsersController : Controller
    {
        private IUserRepository _userRepository;

        public UsersController(IUserRepository repo)
        {
            _userRepository = repo;
        }

        // GET: Admin/Orders
        public ActionResult Index()
        {
            var model = _userRepository.GetAll().ToPagedList(Consts.defaultPageNumber, Consts.defaultPageSize);
            return View(model);
        }
    }
}