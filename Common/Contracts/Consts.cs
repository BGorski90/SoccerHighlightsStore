using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerHighlightsStore.Common.Contracts
{
    public static class Consts
    {
        public const int defaultPageNumber = 1;
        public const int defaultPageSize = 10;
        public const string anonymousUserID = "";
        public const string anonymousUserName = "Anonymous";
        public const string cartCookieName = "Cart";
        public static readonly string[] sortProperties = new string[] { "Added", "Price", "Length" };
    }
}
