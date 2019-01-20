using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoccerHighlightsStore.BusinessLayer.Entities
{
    public class Order
    {
        public int OrderID { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime OrderTime { get; set; }

        public decimal OrderValue { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
        public ICollection<Video> Videos { get; set; }
    }
}