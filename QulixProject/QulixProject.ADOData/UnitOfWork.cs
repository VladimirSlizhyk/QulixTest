using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QulixProject.DALInterfaces;

namespace QulixProject.ADOData
{
    public class UnitOfWork : IUnitOfWork
    {
         private IDbTransaction _transaction;
         private readonly Action<UnitOfWork> _rolledBack;
         private readonly Action<UnitOfWork> _committed;
         
         public UnitOfWork(IDbTransaction transaction, Action<UnitOfWork> rolledBack, Action<UnitOfWork> committed)
         {
             Transaction = transaction;
             _transaction = transaction;
             _rolledBack = rolledBack;
             _committed = committed;
         }
         
         public IDbTransaction Transaction { get; private set; }
         
         public void Dispose() //освобождение транзакции
         {
             if (_transaction == null) 
                 return;
         
             _transaction.Rollback();
             _transaction.Dispose();
             _rolledBack(this);
             _transaction = null;
         }
         
         public void SaveChanges() //сохранение в базу
         {
             if (_transaction == null)
                 throw new InvalidOperationException("May not call save changes twice.");
         
             _transaction.Commit();
             _committed(this);
             _transaction = null;
         }
    }
}
