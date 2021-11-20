using APICatalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICatalog.Pagination;

namespace APICatalog.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetByPriceAsync();

        Task<PagedList<Product>> GetProductsAsync(PageParameters page);
    }
}
