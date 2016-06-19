using SoccerHighlightsStore.Common.Extensions;
using SoccerHighlightsStore.BusinessLayer.Entities;
using SoccerHighlightsStore.DataAccessLayer.ORM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using SoccerHighlightsStore.Common.Contracts;
using BusinessLayer.Helpers;
using PagedList;

namespace SoccerHighlightsStore.DataAccessLayer.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private SoccerVideoDbContext db;

        public VideoRepository()
        {
            db = new SoccerVideoDbContext();
            db.Database.Log = (query => Debug.WriteLine(query));
        }

        public IPagedList<Video> Videos
        {
            get
            {
                return db.Videos.OrderByDescending(v => v.Added).ToPagedList(Consts.defaultPageNumber, Consts.defaultPageSize);
            }
        }

        public IEnumerable<string> Categories
        {
            get
            {
                return db.Categories.Select(c => c.Name).ToList();
            }
        }

        public IEnumerable<string> AdminCategories
        {
            get
            {
                return db.Categories.Select(c => c.Name).ToList();
            }
        }

        public int TotalClips
        {
            get
            {
                return db.Videos.Count();
            }
        }

        public VideoDataResult GetVideos(string sortBy = "Added", bool isDescending = true, int page = Consts.defaultPageNumber, int limit = Consts.defaultPageSize)
        {
            var result = db.Videos.OrderBy(sortBy, isDescending);
            return new VideoDataResult { Videos = result.ToPagedList(page, limit), TotalVideos = result.Count() };
        }

        public Video Get(int id)
        {
            return db.Videos.SingleOrDefault(v => v.VideoID == id);
        }

        public IEnumerable<Video> GetCartVideos(ISet<int> ids)
        {
            return db.Videos.Where(v => ids.Contains(v.VideoID));
        }

        public VideoDataResult Search(string category, string content, string sortBy, bool isDescending, int page, int limit, bool includeTotal)
        {
            bool hasCategory = !(category.Equals("All") || string.IsNullOrWhiteSpace(category));
            bool hasContent = !string.IsNullOrWhiteSpace(content);
            var result = new List<Video>().AsQueryable();
            if (!hasCategory && !hasContent)
            {
                return GetVideos(sortBy, isDescending, page, limit);
            }
            else if(hasCategory && !hasContent)
            {
                result = db.Videos.Where(v => v.Category.Equals(category)).OrderBy(sortBy, isDescending);
            }
            else if (!hasCategory && hasContent)
            {
                result = db.Videos.Where(v => v.Category.Contains(content)
                        || v.Title.Contains(content)
                        || v.Description.Contains(content)).OrderBy(sortBy, isDescending);
            }
            else
            {
                result = db.Videos.Where(v => (v.Category.Equals(category)) &&
                        (v.Category.Contains(content)
                        || v.Title.Contains(content)
                        || v.Description.Contains(content))).OrderBy(sortBy, isDescending);
            }
            return new VideoDataResult { Videos = result.ToPagedList(page, limit), TotalVideos = includeTotal ?  result.Count() : -1};
        }

        public void Add(Video video)
        {
            video.Added = DateTime.Now;
            db.Videos.Add(video);
            Save();
        }

        public void AddCategory(Category category)
        {
            db.Categories.Add(category);
            Save();
        }

        public void Update(Video video)
        {
            Video videoToUpdate = db.Videos.FirstOrDefault(v => v.VideoID == video.VideoID);
            videoToUpdate.Title = video.Title;
            videoToUpdate.Category = video.Category;
            videoToUpdate.Description = video.Description;
            videoToUpdate.Price = video.Price;
            videoToUpdate.Size = video.Size;
            Save();
        }

        public void Remove(int id)
        {
            Video videoToRemove = db.Videos.SingleOrDefault(v => v.VideoID == id);
            db.Videos.Remove(videoToRemove);
            Save();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}