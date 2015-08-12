using System.Collections.Generic;
using System.Data;

namespace QulixProject.DALInterfaces
{
    public interface IRepositoryGeneric<TEntity, in TKey> : IMainRepository where TEntity : class
    {
        void Create(TEntity value);
        void Update(TEntity value);
        void Remove(TKey id);
        TEntity GetEntityById(TKey id);
        List<TEntity> GetAllEntities();
        List<TEntity> ToList(IDbCommand command);
        void Map(IDataRecord record, TEntity entity);
    }
}
