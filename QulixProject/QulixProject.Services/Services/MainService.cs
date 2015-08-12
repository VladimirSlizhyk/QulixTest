using QulixProject.BLInterfaces.BLInterfaces;
using QulixProject.DALInterfaces;

namespace QulixProject.Services.Services
{
    public abstract class MainService : IMainService
    {
        protected IUnitOfWork UnitOfWork { get; set; }
        protected IRepositoryFactory RepositoryFactory { get; set; }

        protected MainService(IUnitOfWork unitOfWork, IRepositoryFactory repositoryFactory)
        {
            UnitOfWork = unitOfWork;
            RepositoryFactory = repositoryFactory;
        }
    }
}
