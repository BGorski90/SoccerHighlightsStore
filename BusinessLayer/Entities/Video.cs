using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Storefront.BusinessLayer.Entities
{
    public class Video
    {
        public Video()
        {
            this.Orders = new HashSet<Order>();
            this.Wishlist = new HashSet<User>();
        }

        public int VideoID { get;  set; }
        [Required]
        [MaxLength(50)]
        [Index("IX_Title", IsUnique=true)]
        public string Title { get;  set; }
        [Required]
        [MaxLength(20)]
        public string Category { get;  set; }
        [Required]
        public int Size { get;  set; }
        [Required]
        [MaxLength(10)]
        public string Format { get; set; }
        [Required]
        [DisplayFormat(DataFormatString="{0:c}")]
        public decimal Price { get;  set; }
        [Required]
        [MaxLength(500)]
        public string Description { get;  set; }
        [Required]
        public int Length { get;  set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Added { get;  set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<User> Wishlist{ get; set; }
    }
}