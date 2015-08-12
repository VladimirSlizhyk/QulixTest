using System;

namespace QulixProject.BLInterfaces.Exceptions
{
    public class TaskServiceException : Exception
    {
        public TaskServiceException()
        {

        }

        public TaskServiceException(string message)
            : base(message)
        {

        }

        public TaskServiceException(Exception exception)
            : base("Some exception occured!", exception)
        {

        }
    }

}
