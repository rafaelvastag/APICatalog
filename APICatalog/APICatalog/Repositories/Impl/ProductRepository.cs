using APICatalog.Context;
using APICatalog.Entities;
using APICatalog.Pagination;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Product>> GetByPriceAsync()
        {
            return await Get().OrderBy(p => p.Price).ToListAsync();
        }

        public async Task<PagedList<Product>> GetProductsAsync(PageParameters page)
        {
            return await PagedList<Product>.PagedListBuilderAsync(Get().OrderBy(p => p.ProductId), page.PageNumber, page.PageSize);
        }
    }
}
