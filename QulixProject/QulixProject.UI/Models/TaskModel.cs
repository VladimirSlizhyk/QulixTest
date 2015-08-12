using System;
using System.Collections.Generic;
using QulixProject.Core.Entities;

namespace QulixProject.UI.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Workload { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status Status { get; set; }
        public int PerformerId { get; set; }
        public Performer Performer { get; set; }
    }
}