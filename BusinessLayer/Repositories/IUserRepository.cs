using Storefront.BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storefront.BusinessLayer.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> GetAll(string sortBy = "RegistrationTime", string sortOrder = "desc", int limit = int.MaxValue);
        void Add(User user);
        bool Exists(string email);
        User FindByEmail(string email);
        void AddVideoToWishlist(string userID, int videoID);
        User FindByUsername(string username);
        void RemoveVideoFromWishlist(string userID, int videoID);
        bool FindWishlist(string username, Video video);
    }
}
