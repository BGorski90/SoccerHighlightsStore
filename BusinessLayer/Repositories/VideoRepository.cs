using Common.Extensions;
using Storefront.BusinessLayer.Entities;
using Storefront.BusinessLayer.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Storefront.BusinessLayer.Repositories
{
    public class VideoRepository: IVideoRepository
    {
        private SoccerVideoDbContext db;
        
        public VideoRepository()
        {
            db = new SoccerVideoDbContext();
        }

        public IQueryable<Video> GetAll(string sortBy = "Added", bool isDescending = true, int limit = int.MaxValue)
        {
            return db.Videos.AsNoTracking().OrderBy<Video>(sortBy, isDescending);
        }

        public Video Get(int id)
        {
            return db.Videos.SingleOrDefault(v => v.VideoID == id);
        }

        public IQueryable<Video> Search(string category, string content, string sortBy = "Added", bool isDescending = true)
        {
            if (string.IsNullOrEmpty(content))
                return GetAll(sortBy, isDescending);
            return db.Videos.Where(v => v.Category.Contains(content)
                        || v.Title.Contains(content)
                        || v.Description.Contains(content)).AsQueryable()
                        .OrderBy(sortBy, isDescending);
        }

        public void Add(Video video)
        {
            video.Added = DateTime.Now;
            db.Videos.Add(video);
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