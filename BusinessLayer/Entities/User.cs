using Storefront.BusinessLayer.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Storefront.BusinessLayer.Entities
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Orders = new HashSet<Order>();
            this.Wishlist = new HashSet<Video>();
        }

        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime RegistrationTime { get; set; }
        [MaxLength(20)]
        public string Country { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        public int TotalPoints { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalSpending { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Video> Wishlist { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        //public User()
        //{
        //    Orders = new List<Order>();
        //    Wishlist = new List<Video>();
        //}
    }


}
