using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
