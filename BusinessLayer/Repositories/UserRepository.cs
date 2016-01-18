using BusinessLayer.Identity;
using Common.Contracts;
using Storefront.BusinessLayer.Entities;
using Storefront.BusinessLayer.ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storefront.BusinessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private SoccerVideoDbContext db;

        public UserRepository()
        {
            db = new SoccerVideoDbContext();
        }

        public IQueryable<User> GetAll(string sortBy = "RegistrationTime", string sortOrder = "desc", int limit = int.MaxValue)
        {
            return db.Users.Include(u => u.Wishlist).OrderByDescending(u => u.TotalSpending);
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

        public User FindById(string ID)
        {
            return db.Users.Include(u => u.Wishlist).FirstOrDefault(u => u.Id == ID);
        }

        public User FindByEmail(string email)
        {
            return db.Users.Include(u => u.Wishlist).FirstOrDefault(u => u.Email == email);
        }

        public User FindByUsername(string username)
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
            User user = db.Users.First(u => u.UserName == username);
            return user.Wishlist.Any(w => w.VideoID == video.VideoID);  
        }
    }
}
