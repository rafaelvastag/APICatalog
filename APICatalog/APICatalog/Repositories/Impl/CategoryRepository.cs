using APICatalog.Context;
using APICatalog.Entities;
using APICatalog.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace APICatalog.Repositories.Impl
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(CatalogDbContext context) : base(context)
        {
        }

        public PagedList<Category> GetCategories(PageParameters page)
        {
            return PagedList<Category>.PagedListBuilder(Get().OrderBy(p => p.CategoryId), page.PageNumber, page.PageSize);
        }

        public IEnumerable<Category> GetCategoryWithProducts()
        {
            return Get().Include(c => c.Products);
        }
    }
}
