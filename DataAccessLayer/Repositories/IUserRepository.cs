using SoccerHighlightsStore.BusinessLayer.Entities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Functional.Option;

namespace SoccerHighlightsStore.DataAccessLayer.Repositories
{
    public interface IUserRepository
    {
        IPagedList<User> Users { get; }
        IPagedList<User> GetUsers(string sortBy = "RegistrationTime", string sortOrder = "Descending", int page = 1, int limit = int.MaxValue);
        void Add(User user);
        bool Exists(string email);
        Option<User> FindByEmail(string email);
        void AddVideoToWishlist(string userID, int videoID);
        Option<User> FindByUsername(string username);
        void RemoveVideoFromWishlist(string userID, int videoID);
        bool FindWishlist(string username, Video video);
    }
}
