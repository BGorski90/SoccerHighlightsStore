using Storefront.BusinessLayer.Entities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Storefront.Areas.Admin.ViewModels
{
    public class AdminHomeViewModel
    {
        public IEnumerable<Video> Videos { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}