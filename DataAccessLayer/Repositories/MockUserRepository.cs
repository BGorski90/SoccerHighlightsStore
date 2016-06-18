//using Common.Contracts;
//using SoccerHighlightsStore.BusinessLayer.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using PagedList;

//namespace SoccerHighlightsStore.DataAccessLayer.Repositories
//{
//    public class MockUserRepository : IUserRepository
//    {
//        public static readonly List<User> _users = new List<User>();
//        private static int _nextID = 1;

//        public IPagedList<User> GetAll(string sortBy = "RegistrationTime", string sortOrder = "Descending", int page = 1, int limit = int.MaxValue)
//        {
//            var sortByProperty = typeof(User).GetProperty(sortBy);
//            var result = sortOrder == "Descending" ? _users.OrderByDescending(u => sortByProperty.GetValue(u)).AsQueryable().Take(limit)
//                                            : _users.OrderBy(u => sortByProperty.GetValue(u)).AsQueryable().Take(limit);
//            return result;
//        }

//        public User Get(int id)
//        {
//            return _users.Find(u => u.Id == id);
//        }

//        public void Add(User user)
//        {
//            if (!Exists(user.Email))
//            {
//                user.Id = _nextID++;
//                user.RegistrationTime = DateTime.Now;
//                if (user.Email.Equals("admin@admin.com"))
//                    user.Roles = "Admin";
//                else
//                    user.Roles = "Customer";

//                _users.Add(user);
//            }
//        }

//        public bool Exists(string email)
//        {
//            if (_users.Any(u => u.Email == email))
//                return true;
//            return false;
//        }

//        public User FindByEmail(string email)
//        {
//            return _users.Find(u => u.Email == email);
//        }

//        public User FindByUsername(string username)
//        {
//            return _users.Find(u => u.UserName== username);
//        }

//        public void AddOrderInfo(Order order)
//        {
//            User buyer = Get(order.UserID);
//            int orderPoints = (int)order.OrderValue + 1 + (order.Videos.Count - 1) * SpecialOffers.pointsForExtraVideo;
//            buyer.TotalOrders++;
//            buyer.TotalSpending += order.OrderValue;
//            buyer.TotalPoints += orderPoints;
//            if (buyer.Orders == null)
//                buyer.Orders = new List<Order>();
//            buyer.Orders.Add(order);
//        }

//        public void AddVideoToWishlist(string username, Video video)
//        {
//            User user = FindByUsername(username);
//            if (user.Wishlist == null)
//                user.Wishlist = new List<Video>();
//            user.Wishlist.Add(video);
//        }

//        public void RemoveVideoFromWishlist(string username, Video video)
//        {
//            User user = FindByUsername(username);
//            user.Wishlist.Remove(video);
//        }

//        public bool FindWishlist(string username, Video video)
//        {
//            return _users.Exists(u => u.Wishlist != null && u.Wishlist.Contains(video));
//        }
//    }
//}
