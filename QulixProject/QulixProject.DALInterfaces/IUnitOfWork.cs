using System;
using System.Data;
using System.Runtime.InteropServices;

namespace QulixProject.DALInterfaces
{
    public interface IUnitOfWork: IDisposable
    {
        void SaveChanges();
        void Dispose();
    }
}
