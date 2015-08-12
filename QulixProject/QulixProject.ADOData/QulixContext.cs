using System.Collections.Generic;
using System.Data;
using System.Threading;
using QulixProject.DALInterfaces;

namespace QulixProject.ADOData
{
    public class QulixContext
    {
        private readonly IDbConnection _connection;
        private readonly IConnectionFactory _connectionFactory;
        private readonly ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();
        private readonly LinkedList<UnitOfWork> _uows = new LinkedList<UnitOfWork>();

        public QulixContext(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            _connection = _connectionFactory.Create();
        }

        public IUnitOfWork CreateUnitOfWork() //создание UnitOfWork
        {
            var transaction = _connection.BeginTransaction();
            var uow = new UnitOfWork(transaction, RemoveTransaction, RemoveTransaction);

            _rwLock.EnterWriteLock();
            _uows.AddLast(uow);
            _rwLock.ExitWriteLock();

            return uow;
        }

        public IDbCommand CreateCommand() //метод создания команды/запроса
        {
            var cmd = _connection.CreateCommand();

            _rwLock.EnterReadLock();
            if (_uows.Count > 0)
                cmd.Transaction = _uows.First.Value.Transaction;
            _rwLock.ExitReadLock();

            return cmd;
        }

        private void RemoveTransaction(UnitOfWork obj) //удаление транзакции
        {
            _rwLock.EnterWriteLock();
            _uows.Remove(obj);
            _rwLock.ExitWriteLock();
        }

        public void Dispose() //обсвобождение соединения
        {
            _connection.Dispose();
        }
    }
}
