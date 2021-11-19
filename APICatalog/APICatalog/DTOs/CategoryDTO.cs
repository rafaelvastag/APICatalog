using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalog.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string imageUrl { get; set; }
        public ICollection<ProductDTO> Products { get; set; } = new List<ProductDTO>();
    }
}
