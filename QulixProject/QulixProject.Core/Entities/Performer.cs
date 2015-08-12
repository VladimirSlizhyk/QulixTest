using System.Collections.Generic;

namespace QulixProject.Core.Entities
{
    public class Performer : MainEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PatronymicName { get; set; }

        public override string ToString()
        {
            return LastName + " " + FirstName + " " + PatronymicName;
        }
    }
}
