using APICatalog.Context;
using APICatalog.Entities;
using APICatalog.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalog.Repositories.Impl
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(CatalogDbContext context) : base(context)
        {
        }

        public IEnumerable<Product> GetByPrice()
        {
            return Get().OrderBy(p => p.Price).ToList();
        }

        public PagedList<Product> GetProducts(PageParameters page)
        {
            return PagedList<Product>.PagedListBuilder(Get().OrderBy(p => p.ProductId), page.PageNumber, page.PageSize);
        }
    }
}
