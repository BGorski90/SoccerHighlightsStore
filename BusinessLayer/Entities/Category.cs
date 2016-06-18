using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerHighlightsStore.BusinessLayer.Entities
{
    public class Category
    {
        public int CategoryID { get; set; }
        [Required]
        [MaxLength(30)]
        [Index("IX_Name", IsUnique = true)]
        public string Name { get; set; }
    }
}
