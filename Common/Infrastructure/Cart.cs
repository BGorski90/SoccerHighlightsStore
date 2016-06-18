using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoccerHighlightsStore.Common.Infrastructure
{
    public class Cart
    {
        private HashSet<int> _videos = new HashSet<int>();

        public HashSet<int> Videos
        {
            get { return _videos; }
        }

        public bool Contains(int id)
        {
            return _videos.Contains(id);
        }
    }
}