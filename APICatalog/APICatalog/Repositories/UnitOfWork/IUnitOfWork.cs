using System.Threading.Tasks;

namespace APICatalog.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get;  }

        ICategoryRepository CategoryRepository { get; }

        Task Commit();

        void Dispose();
    }
}
