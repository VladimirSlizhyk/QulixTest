using System.Collections.Generic;
using QulixProject.Core.Entities;

namespace QulixProject.BLInterfaces.BLInterfaces
{
    public interface IPerformerService : IMainService
    {
        Performer CreatePerformer(string firstName, string lastName, string patronymicName);
        void UpdatePerformer(Performer performer);
        void RemovePerformer(int id);
        Performer GetPerformerById(int id);
        List<Performer> GetAllPerformers();
    }
}
