using SoccerHighlightsStore.BusinessLayer.Entities;
using System.Collections.Generic;
using PagedList;
using SoccerHighlightsStore.DataAccessLayer.Helpers;

namespace SoccerHighlightsStore.DataAccessLayer.Repositories
{
    public interface IVideoRepository
    {
        VideoDataResult Videos { get; }
        VideoDataResult GetVideos(string sortBy = "Added", bool isDescending = true, int page = 1, int limit = int.MaxValue);
        Video Get(int id);
        IEnumerable<Video> GetCartVideos(ISet<int> ids);
        IEnumerable<string> Categories { get; }
        IEnumerable<string> AdminCategories { get; }
        int TotalClips { get; }

        VideoDataResult Search(string category = null, string content = null, string sortBy = "Added", bool isDescending = true, int page = 1, int limit = int.MaxValue, bool includeTotal = true);
        void Add(Video video);
        void AddCategory(Category category);
        void Update(Video video);
        void Remove(int id);
    }
}
