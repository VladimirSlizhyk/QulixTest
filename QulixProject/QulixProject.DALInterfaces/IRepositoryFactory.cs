using QulixProject.Core.Entities;

namespace QulixProject.DALInterfaces
{
    public interface IRepositoryFactory
    {
        IRepositoryGeneric<Core.Entities.Task, int> GetTaskRepository();
        IRepositoryGeneric<Performer, int> GetPerformerRepository();
    }
}
