using APICatalog.Context;
using APICatalog.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace APICatalog.Repositories.Impl
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(CatalogDbContext context) : base(context)
        {
        }

        public IEnumerable<Category> GetCategoryWithProducts()
        {
            return Get().Include(c => c.Products);
        }
    }
}
