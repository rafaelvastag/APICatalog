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
        IEnumerable<Product> GetByPrice();

        PagedList<Product> GetProducts(PageParameters page);
    }
}
