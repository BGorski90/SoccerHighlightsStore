using SoccerHighlightsStore.Common.Extensions;
using SoccerHighlightsStore.BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoccerHighlightsStore.Common.Contracts;
using BusinessLayer.Helpers;
using PagedList;

namespace SoccerHighlightsStore.DataAccessLayer.Repositories
{
    public class MockVideoRepository : IVideoRepository
    {
        public static readonly List<Video> _videos = new List<Video>();
        private static int _nextID = 1;

        static MockVideoRepository()
        {
            _videos.Add(new Video
            {
                VideoID = _nextID++,
                Title = "How I Like My Feet Licked",
                Category = "Foot domination",
                Size = 949,
                Format = "wmv",
                Price = 22.99m,
                Description = "Gabriela obediently smells and worships Bruna's delicious feet",
                Length = 32,
                Added = DateTime.Now
            });
            _videos.Add(new Video
            {
                VideoID = _nextID++,
                Title = "Kneeling Under Lara Feet",
                Category = "Foot domination",
                Size = 935,
                Format = "wmv",
                Price = 21.99m,
                Description = "Gabriela obediently smells and worships Lara's delicious feet",
                Length = 32,
                Added = DateTime.Now
            });
            _videos.Add(new Video
            {
                VideoID = _nextID++,
                Title = "Lick Sharon Sweaty Black Feet",
                Category = "Ass worship",
                Size = 965,
                Format = "wmv",
                Price = 22.99m,
                Description = "Gabriela obediently smells and worships Sharon's delicious feet",
                Length = 33,
                Added = DateTime.Now
            });
        }

        public IPagedList<Video> Videos
        {
            get
            {
                return _videos.ToPagedList(Consts.defaultPageNumber, Consts.defaultPageSize);
            }
        }

        public IEnumerable<string> Categories
        {
            get
            {
                return _videos.Select(v => v.Category).Distinct().ToList();
            }
        }
        public IEnumerable<string> AdminCategories
        {
            get
            {
                return _videos.Select(v => v.Category).Distinct().ToList();
            }
        }

        public int TotalClips
        {
            get
            {
                return _videos.Count;
            }
        }

        public VideoDataResult GetVideos(string sortBy = "Added", bool isDescending = true, int page = 1, int limit = int.MaxValue)
        {
            //var sortByProperty = typeof(Video).GetProperty(sortBy);
            //var result = sortOrder == "desc" ? _videos.OrderByDescending(v => sortByProperty.GetValue(v)).AsQueryable().Take(limit)
            //                                : _videos.OrderBy(v => sortByProperty.GetValue(v)).AsQueryable().Take(limit);
            //return result;
            var result = _videos;
            return new VideoDataResult { Videos = result.ToPagedList(page, limit), TotalVideos = result.Count };
        }

        public VideoDataResult Search(string category, string content, string sortBy = "Added", bool isDescending = true, int page = 1, int limit = int.MaxValue, bool includeTotal = false)
        {
            var result = new List<Video>().AsQueryable();
            if (category == "All")
            {
                if (string.IsNullOrEmpty(content))
                {
                    result = _videos.AsQueryable();
                }
                else
                {
                    result = _videos.Where(v => v.Category.Contains(content)
                                    || v.Title.Contains(content)
                                    || v.Description.Contains(content)).AsQueryable()
                                    .OrderBy(sortBy, isDescending);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(content))
                {
                    result = _videos.Where(v => v.Category == category).AsQueryable();
                }
                else
                {
                    result = _videos.Where(v => (v.Category == category) &&
                                        (v.Category.Contains(content)
                                        || v.Title.Contains(content)
                                        || v.Description.Contains(content))).AsQueryable();
                }
            }
            return new VideoDataResult { Videos = result.ToPagedList(page, limit), TotalVideos = result.Count() };
        }

        public Video Get(int id)
        {
            return _videos.Find(v => v.VideoID == id);
        }

        public IEnumerable<Video> GetCartVideos(ISet<int> ids)
        {
            return _videos.Where(v => ids.Contains(v.VideoID));
        }

        public void Add(Video video)
        {
            video.VideoID = _nextID++;
            video.Added = DateTime.Now;
            _videos.Add(video);
        }

        public void AddCategory(Category category)
        {

        }

        public void Update(Video video)
        {
            Video videoToUpdate = _videos.FirstOrDefault(v => v.VideoID == video.VideoID);
            videoToUpdate.Title = video.Title;
            videoToUpdate.Category = video.Category;
            videoToUpdate.Description = video.Description;
            videoToUpdate.Price = video.Price;
            videoToUpdate.Size = video.Size;
            videoToUpdate.Format = video.Format;
        }

        public void Remove(int id)
        {
            _videos.RemoveAll(v => v.VideoID == id);
        }
    }
}