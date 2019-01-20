using SoccerHighlightsStore.Common.Extensions;
using SoccerHighlightsStore.BusinessLayer.Entities;
using SoccerHighlightsStore.DataAccessLayer.ORM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SoccerHighlightsStore.Common.Contracts;
using PagedList;

namespace SoccerHighlightsStore.DataAccessLayer.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private SoccerVideoDbContext db;

        public VideoRepository()
        {
            db = new SoccerVideoDbContext();
            db.Database.Log = query => Debug.WriteLine(query);
        }

        public IEnumerable<Video> Videos
        {
            get
            {
                return db.Videos.OrderByDescending(v => v.Added).ToList();
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

        public Video Get(int id)
        {
            return db.Videos.Find(id);
        }

        public IEnumerable<Video> GetCartVideos(ISet<int> ids)
        {
            return db.Videos.Where(v => ids.Contains(v.VideoID));
        }

        public IEnumerable<Video> Search(string category = null, string content = null, string sortBy = "Added", bool isDescending = true, int page = 1, int limit = int.MaxValue)
        {
            bool hasCategory = !(category.Equals("All") || string.IsNullOrWhiteSpace(category));
            bool hasContent = !string.IsNullOrWhiteSpace(content);

            IQueryable<Video> query = db.Videos;

            if(hasCategory)
            {
                query = query.Where(v => v.Category.Equals(category));
            }

            if(hasContent)
            {
                query = query.Where(v => v.Category.Contains(content)
                        || v.Title.Contains(content)
                        || v.Description.Contains(content));;
            }

            return query.OrderBy(sortBy, isDescending).Skip(page - 1).Take(limit).ToList();
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