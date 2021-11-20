using APICatalog.Entities;
using APICatalog.Pagination;
using System.Collections.Generic;

namespace APICatalog.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> GetCategoryWithProducts();

        PagedList<Category> GetCategories(PageParameters page);
    }
}
