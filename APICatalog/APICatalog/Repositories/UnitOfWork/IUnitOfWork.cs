namespace APICatalog.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get;  }

        ICategoryRepository CategoryRepository { get; }

        void Commit();

        void Dispose();
    }
}
