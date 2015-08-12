using System.Data;

namespace QulixProject.DALInterfaces
{
    public interface IConnectionFactory
    {
        IDbConnection Create();
    }
}
