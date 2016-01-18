using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Storefront.BusinessLayer.Entities
{
    public class Order
    {
        public Order()
        {
            this.Videos= new HashSet<Video>();
        }

        public int OrderID { get; set; }
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime OrderTime { get; set; }
        public decimal OrderValue { get; set; }
        public string UserID { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
    }
}