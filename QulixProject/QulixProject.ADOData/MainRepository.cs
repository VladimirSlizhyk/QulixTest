using QulixProject.DALInterfaces;

namespace QulixProject.ADOData
{
    public class MainRepository : IMainRepository
    {
        protected QulixContext Context { get; set; }

        public MainRepository(QulixContext context)
        {
            Context = context;
        }


    }
}
