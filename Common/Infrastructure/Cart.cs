using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoccerHighlightsStore.Common.Infrastructure
{
    public class Cart
    {
        public HashSet<int> Videos { get; }

        public Cart(IEnumerable<int> videos)
        {
            Videos = new HashSet<int>(videos);
        }

        public bool Contains(int id)
        {
            return Videos.Contains(id);
        }
    }
}