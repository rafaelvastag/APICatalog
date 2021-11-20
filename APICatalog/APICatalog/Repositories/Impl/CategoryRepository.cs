using APICatalog.Context;
using APICatalog.Entities;
using APICatalog.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalog.Repositories.Impl
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(CatalogDbContext context) : base(context)
        {
        }

        public async Task<PagedList<Category>> GetCategoriesAsync(PageParameters page)
        {
            return await PagedList<Category>.PagedListBuilderAsync(Get().OrderBy(p => p.CategoryId), page.PageNumber, page.PageSize);
        }

        public async Task<IEnumerable<Category>> GetCategoryWithProductsAsync()
        {
            return await Get().Include(c => c.Products).ToListAsync();
        }
    }
}
