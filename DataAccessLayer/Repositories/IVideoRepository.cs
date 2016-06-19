using SoccerHighlightsStore.BusinessLayer.Entities;
using System.Collections.Generic;
using BusinessLayer.Helpers;
using PagedList;

namespace SoccerHighlightsStore.DataAccessLayer.Repositories
{
    public interface IVideoRepository
    {
        IPagedList<Video> Videos { get; }
        VideoDataResult GetVideos(string sortBy = "Added", bool isDescending = true, int page = 1, int limit = int.MaxValue);
        Video Get(int id);
        IEnumerable<Video> GetCartVideos(ISet<int> ids);
        IEnumerable<string> Categories { get; }
        IEnumerable<string> AdminCategories { get; }
        int TotalClips { get; }

        VideoDataResult Search(string category, string content, string sortBy, bool isDescending, int page, int limit, bool includeTotal);
        void Add(Video video);
        void AddCategory(Category category);
        void Update(Video video);
        void Remove(int id);
    }
}
