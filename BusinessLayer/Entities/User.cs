using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SoccerHighlightsStore.BusinessLayer.Entities
{
    public class User : IdentityUser
    {
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
    }
}