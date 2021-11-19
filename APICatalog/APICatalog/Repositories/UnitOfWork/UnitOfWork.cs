using APICatalog.Context;
using APICatalog.Repositories.Impl;

namespace APICatalog.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private ProductRepository _productRepository;
        private CategoryRepository _categoryRepository;
        public CatalogDbContext _context;

        public UnitOfWork(CatalogDbContext context)
        {
            _context = context;
        }

        public IProductRepository ProductRepository 
        { 
            get
            {
                return _productRepository = _productRepository ?? new ProductRepository(_context);
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                return _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);
            }
        }


        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
