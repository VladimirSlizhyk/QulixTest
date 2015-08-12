using System;

namespace QulixProject.BLInterfaces.Exceptions
{
    public class PerformerServiceException : Exception
    {
        public PerformerServiceException()
        {

        }

        public PerformerServiceException(string message)
            : base(message)
        {

        }

        public PerformerServiceException(Exception exception)
            : base("Some exception occured!", exception)
        {

        }
    }
}
