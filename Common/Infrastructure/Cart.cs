using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Infrastructure
{
    public class Cart
    {
        private HashSet<int> _videos = new HashSet<int>();

        public HashSet<int> Videos
        {
            get { return _videos; }
        }

        public void AddVideo(int videoID)
        {
            //TODO check if video is in DB
            //int? vid = _videos.FirstOrDefault(v => v == videoID);
            //if (vid == null)
            _videos.Add(videoID);
        }

        public void RemoveVideo(int videoID)
        {
            _videos.Remove(videoID);
        }

        public void ClearCart()
        {
            _videos.Clear();
        }

        public bool Contains(int videoID)
        {
            return _videos.Contains(videoID);
        }

    }
}