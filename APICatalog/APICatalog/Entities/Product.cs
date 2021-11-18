using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalog.Entities
{
    [Table("RGV01_PRODUCTS")]
    public class Product
    {
        [Key]
        [Column("ID")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        [MaxLength(80)]
        [Column("NAME")]
        public string Name { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "The description must not have more than {1} characters")]
        [Column("DESCRIPTION")]
        public string Description { get; set; }

        [Required]
        [Range(1,10000, ErrorMessage = "The price must be between {1} and {2}")]
        [Column("PRICE")]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(300)]
        [Column("IMAGE_URL")]
        public string ImagemUrl { get; set; }

        public float  Inventory { get; set; }

        public DateTime CreateDate { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
