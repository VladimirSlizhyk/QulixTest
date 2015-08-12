using QulixProject.Core.Entities;
using QulixProject.DALInterfaces;

namespace QulixProject.ADOData
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly QulixContext _context;
        private IRepositoryGeneric<Task, int> _taskRepository;
        private IRepositoryGeneric<Performer, int> _performerRepository;

        public RepositoryFactory(QulixContext context)
        {
            _context = context;
        }

        public IRepositoryGeneric<Task, int> GetTaskRepository() //получение Task репозитория
        {
            return _taskRepository ?? (_taskRepository = new TaskRepository(_context));
        }

        public IRepositoryGeneric<Performer, int> GetPerformerRepository() //получение Performer репозитория
        {
            return _performerRepository ?? (_performerRepository = new PerformerRepository(_context));
        }
    }
}
