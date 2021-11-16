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

        [Required]
        [MaxLength(80)]
        [Column("NAME")]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        [Column("DESCRIPTION")]
        public string Description { get; set; }

        [Required]
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
