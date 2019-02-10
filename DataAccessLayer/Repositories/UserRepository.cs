using SoccerHighlightsStore.Common.Contracts;
using SoccerHighlightsStore.BusinessLayer.Entities;
using SoccerHighlightsStore.DataAccessLayer.ORM;
using System;
using System.Data.Entity;
using System.Linq;
using PagedList;
using Functional.Option;
using System.Collections.Generic;

namespace SoccerHighlightsStore.DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private SoccerVideoDbContext db;

        public UserRepository()
        {
            db = new SoccerVideoDbContext();
        }

        public IEnumerable<User> Users
        {
            get
            {
                return db.Users.OrderByDescending(u => u.RegistrationTime);
            }
        }

        public IEnumerable<User> GetUsers(string sortBy = "RegistrationTime", int limit = int.MaxValue)
        {
            var sortByProperty = typeof(Order).GetProperty(sortBy);
            return db.Users.Include(u => u.Wishlist).OrderByDescending(o => sortByProperty.GetValue(o)).Take(limit);
        }

        public IEnumerable<User> GetUsersAscending(string sortBy = "RegistrationTime", int limit = int.MaxValue)
        {
            var sortByProperty = typeof(Order).GetProperty(sortBy);
            return db.Users.Include(u => u.Wishlist).OrderBy(o => sortByProperty.GetValue(o)).Take(limit);
        }

        public void Add(User user)
        {
            if (!Exists(user.Email))
            {
                user.RegistrationTime = DateTime.Now;
                db.Users.Add(user);
            }
        }

        public bool Exists(string email)
        {
            if (db.Users.Any(u => u.Email == email))
                return true;
            return false;
        }

        public Option<User> FindByEmail(string email)
        {
            return db.Users.Include(u => u.Wishlist).FirstOrDefault(u => u.Email == email);
        }

        public Option<User> FindByUsername(string username)
        {
            return db.Users.Include(u => u.Wishlist).FirstOrDefault(u => u.UserName == username);
        }

        public void AddVideoToWishlist(string userID, int videoID)
        {
            var video = db.Videos.FirstOrDefault(v => v.VideoID == videoID);
            db.Users.Include(u => u.Wishlist).FirstOrDefault(u => u.Id == userID).Wishlist.Add(video);
            db.SaveChanges();
        }

        public void RemoveVideoFromWishlist(string userID, int videoID)
        {
            var video = db.Videos.FirstOrDefault(v => v.VideoID == videoID);
            db.Users.Include(u => u.Wishlist).FirstOrDefault(u => u.Id == userID).Wishlist.Remove(video);
            db.SaveChanges();
        }

        public bool FindWishlist(string username, Video video)
        {
            User user = db.Users.Include(u => u.Wishlist).First(u => u.UserName == username);
            return user.Wishlist.Any(w => w.VideoID == video.VideoID);  
        }
    }
}
