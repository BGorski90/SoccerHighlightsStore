using Storefront.BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storefront.BusinessLayer.Repositories
{
    public interface IVideoRepository
    {
        IQueryable<Video> GetAll(string sortBy = "Added", bool isDescending = true, int limit = int.MaxValue);
        Video Get(int id);
        IQueryable<Video> Search(string category, string content, string sortBy = "Added", bool isDescending = true);
        void Add(Video video);
        void Update(Video video);
        void Remove(int id);
    }
}
