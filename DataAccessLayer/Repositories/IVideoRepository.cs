using SoccerHighlightsStore.BusinessLayer.Entities;
using System.Collections.Generic;
using PagedList;

namespace SoccerHighlightsStore.DataAccessLayer.Repositories
{
    public interface IVideoRepository
    {
        IEnumerable<Video> Videos { get; }
        Video Get(int id);
        IEnumerable<Video> GetCartVideos(ISet<int> ids);
        IEnumerable<string> Categories { get; }
        IEnumerable<string> AdminCategories { get; }

        IEnumerable<Video> Search(string category = null, string content = null, string sortBy = "Added", bool isDescending = true, int page = 1, int limit = int.MaxValue);
        void Add(Video video);
        void AddCategory(Category category);
        void Update(Video video);
        void Remove(int id);
    }
}
