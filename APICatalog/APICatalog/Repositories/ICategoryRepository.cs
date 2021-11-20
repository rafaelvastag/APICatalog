using APICatalog.Entities;
using APICatalog.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APICatalog.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetCategoryWithProductsAsync();

        Task<PagedList<Category>> GetCategoriesAsync(PageParameters page);
    }
}
