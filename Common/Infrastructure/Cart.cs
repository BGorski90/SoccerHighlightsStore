using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoccerHighlightsStore.Common.Infrastructure
{
    public class Cart
    {
        public HashSet<int> Videos { get; } = new HashSet<int>();

        public bool Contains(int id)
        {
            return Videos.Contains(id);
        }
    }
}