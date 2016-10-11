using SoccerHighlightsStore.BusinessLayer.Entities;
using SoccerHighlightsStore.DataAccessLayer.Helpers;
using SoccerHighlightsStore.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace DataAccessLayer.Helpers
{
    public class VideoCacheManager
    {
        private HttpContextBase _context;
        private IVideoRepository _videoRepository;
        private object _lock = new object();

        public VideoCacheManager(HttpContextBase context, IVideoRepository repo)
        {
            _context = context;
            _videoRepository = repo;
        }

        public VideoDataResult Get(string key)
        {
            return _context.Cache[key] as VideoDataResult;
        }

        public void Add(Video video)
        {
            if (_context.Cache[video.Category] == null)
                CacheCategory(video.Category);
            else
            {
                _context.Cache.Remove("All");
                _context.Cache.Remove(video.Category);
            }
        }

        public void Update(Video video)
        {
            _context.Cache.Remove("All");
            _context.Cache.Remove(video.Category);
        }

        public void Remove(int videoID)
        {
            _context.Cache.Remove("All");
            var video = _videoRepository.Get(videoID);
            _context.Cache.Remove(video.Category);
        }

        public void FillCache()
        {
            CacheAll();
            var categories = _videoRepository.Categories;
            foreach (string cat in categories)
            {
                CacheCategory(cat);
            }
        }

        private void OnVideoCacheCleared(string key, object value, CacheItemRemovedReason reason)
        {
            if (key.Equals("All"))
                CacheAll();
            else
                CacheCategory(key);
        }

        private void CacheAll()
        {
            lock(_lock)
            {
                _context.Cache.Insert("All", _videoRepository.Videos, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.High, new CacheItemRemovedCallback(OnVideoCacheCleared));
            }
        }

        private void CacheCategory(string category)
        {
            lock (_lock)
            {
                _context.Cache.Insert(category, _videoRepository.Search(category), null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.High, new CacheItemRemovedCallback(OnVideoCacheCleared));
            }
        }
    }
}
