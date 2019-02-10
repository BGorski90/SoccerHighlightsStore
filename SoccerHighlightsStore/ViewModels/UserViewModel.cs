using System.Collections.Generic;
using System.Linq;
using SoccerHighlightsStore.BusinessLayer.Entities;
using SoccerHighlightsStore.Common.Infrastructure;

namespace SoccerHighlightsStore.Storefront.ViewModels
{
    public class UserViewModel
    {
        public string Username { get; set; }
        public Cart Cart { get; set; }
        public HashSet<int> Wishlist { get; set; } 

        private UserViewModel(string userName, Cart cart, HashSet<int> wishList)
        {
            Username = userName;
            Cart = cart;
            Wishlist = wishList;
        }

        public static UserViewModel Empty()
        {
            return new UserViewModel(
                string.Empty,
                new Cart(Enumerable.Empty<int>()),
                new HashSet<int>(Enumerable.Empty<int>()));
        }

        public static UserViewModel Create(string userName, Cart cart, IEnumerable<int> wishList)
        {
            return new UserViewModel(
                userName,
                cart,
                new HashSet<int>(wishList));
        }
    }
}