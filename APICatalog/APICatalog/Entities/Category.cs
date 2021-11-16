using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalog.Entities
{
    [Table("RGV01_CATEGORIES")]
    public class Category
    {
        [Key]
        [Column("ID")]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(80)]
        [Column("NAME")]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        [Column("IMAGE_URL")]
        public string imageUrl { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
