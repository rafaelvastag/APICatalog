using APICatalog.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalog.Entities
{
    [Table("RGV01_PRODUCTS")]
    public class Product : IValidatableObject
    {
        [Key]
        [Column("ID")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        [MaxLength(80)]
        [StartWithUpperCaseAttribute]
        [Column("NAME")]
        public string Name { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "The description must not have more than {1} characters")]
        [Column("DESCRIPTION")]
        public string Description { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "The price must be between {1} and {2}")]
        [Column("PRICE")]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(300)]
        [Column("IMAGE_URL")]
        public string ImagemUrl { get; set; }

        public float Inventory { get; set; }

        public DateTime CreateDate { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(this.Name))
            {
                if (this.Name.ToString()[0].ToString() != this.Name.ToString()[0].ToString().ToUpper())
                {
                    yield return new ValidationResult("The attribute must be start with Upper letter", new[] { nameof(this.Name) });
                }

                if (this.Inventory <= 0)
                {
                    yield return new ValidationResult("Inventory must be greater than 0", new[] { nameof(this.Inventory) });
                }
            }
        }
    }
}
