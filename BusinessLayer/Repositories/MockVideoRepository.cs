using Common.Extensions;
using Storefront.BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Storefront.BusinessLayer.Repositories
{
    public class MockVideoRepository: IVideoRepository
    {
        public static readonly List<Video> _videos = new List<Video>();
        private static int _nextID = 1;

        static MockVideoRepository()
        {
            _videos.Add(new Video
            {
                VideoID = _nextID++,
                Title = "Liverpool FC - Manchester United",
                Category = "Premier League",
                Description = "The latest North Derby at Anfield",
                Size = 949,
                Price = 2.99m,
                Length = 32,
                Added = DateTime.Now,
                Format = "WMV"
            });
            _videos.Add(new Video
            {
                VideoID = _nextID++,
                Title = "Real Madrid - FC Barcelona",
                Category = "Primera Division",
                Description = "The latest Gran Derbi at Santiago Bernabeu",
                Size = 935,
                Price = 2.99m,
                Length = 32,
                Added = DateTime.Now,
                Format = "WMV"
            });
            _videos.Add(new Video
            {
                VideoID = _nextID++,
                Title = "Inter Milan - AC Milan",
                Category = "Serie A",
                Description = "The latest Milan derby at San Siro",
                Size = 965,
                Price = 2.99m,
                Length = 33,
                Added = DateTime.Now,
                Format = "WMV"
            });
        }

        public IQueryable<Video> GetAll(string sortBy = "Added", bool isDescending = true, int limit = int.MaxValue)
        {
            //var sortByProperty = typeof(Video).GetProperty(sortBy);
            //var result = sortOrder == "desc" ? _videos.OrderByDescending(v => sortByProperty.GetValue(v)).AsQueryable().Take(limit)
            //                                : _videos.OrderBy(v => sortByProperty.GetValue(v)).AsQueryable().Take(limit);
            //return result;
            return _videos.AsQueryable().OrderBy(sortBy, isDescending);
        }

        public IQueryable<Video> Search(string category, string content, string sortBy = "Added", bool isDescending = true)
        {
            var result = new List<Video>().AsQueryable();
            if(category ==  "All")
            {
                if (string.IsNullOrEmpty(content))
                    return GetAll();
                else
                    return _videos.Where(v => v.Category.Contains(content)
                                    || v.Title.Contains(content)
                                    || v.Description.Contains(content)).AsQueryable()
                                    .OrderBy(sortBy, isDescending); 
            }
            else
            {
                if (string.IsNullOrEmpty(content))
                    return _videos.Where(v => v.Category == category).AsQueryable();
                else
                    return _videos.Where(v => (v.Category == category) && 
                                        (v.Category.Contains(content)
                                        || v.Title.Contains(content)
                                        || v.Description.Contains(content))).AsQueryable();
            }
        }

        public Video Get(int id)
        {
            return _videos.Find(v => v.VideoID == id); 
        }

        public void Add(Video video)
        {
            video.VideoID = _nextID++;
            video.Added = DateTime.Now;
            _videos.Add(video);
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