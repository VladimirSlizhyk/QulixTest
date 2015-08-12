using System;
using System.Runtime.CompilerServices;

namespace QulixProject.Core.Entities
{
    public class Task : MainEntity
    {
        public string Name { get; set; }
        public int Workload { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status Status { get; set; }
        public int PerformerId { get; set; }
    }
}
